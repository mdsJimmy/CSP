using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data;
using MDS.Database;
using System.Xml;
using System.Text;
using System.IO;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using MDS.Utility;
using System.Web.Configuration;
using MDS.MSMQ;
using MIPLibrary;
using MDS.Utility;

/// <summary>
/// DMSWebService 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下一行。
[System.Web.Script.Services.ScriptService]
public class DMSWebService : System.Web.Services.WebService
{


    static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public DMSWebService()
    {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }




  
    /// <summary>
    /// [20160818改為參數式執行]
    /// Sitemap String取得;
    /// </summary>
    /// <param name="TargerGroupID">目前的群組位置</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string GET_SiteMapAddress(string TargerGroupID)
    {
        Database db = new Database();
        DataTable dt = new DataTable();
        int nRet = -1;
        string SiteMapAddress = "";

        /*連線DB*/
        nRet = db.DBConnect();
        if (nRet == 0)
        {
            string strParentGroupID = NUtility.trimBad(Session["ParentGroupID"].ToString());
            string srtTargerGroupID = NUtility.trimBad(TargerGroupID);



            string SiteMap_SQL = @"DECLARE @Sitemap nvarchar(4000)  
                SET @Sitemap = ''  
                /*取得Sitmap*/  
                SELECT @Sitemap = @Sitemap + SecurityGroup.GroupName + '/'  
                FROM dbo.UTILfn_Split(  
                    (SELECT GroupSearchKey + '/' + GroupID FROM dbo.fn_GetGroupTree(@ParentGroupID_1) WHERE GroupID = @TargerGroupID),  
                    '/') AS tblA  
                INNER JOIN SecurityGroup ON tblA.[Value] = SecurityGroup.GroupID  
                WHERE tblA.[Value] <> (  
                    SELECT ParentGroupID FROM SecurityRelation WHERE AccountID =@ParentGroupID_2
                )  
                SELECT @Sitemap AS Sitemap ";

            
            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(SiteMap_SQL, db.getOcnn());
           
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ParentGroupID_1", MDS.Utility.NUtility.trimBad(strParentGroupID)));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TargerGroupID", MDS.Utility.NUtility.trimBad(srtTargerGroupID)));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ParentGroupID_2", MDS.Utility.NUtility.trimBad(strParentGroupID)));
            
            //呼叫傳入參數式sqlcmd的方法
            nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);



            if (nRet == 0)
            {
                if (dt.Rows.Count > 0)
                    SiteMapAddress = dt.Rows[0]["Sitemap"].ToString();
            }
            
            db.DBDisconnect();
        }

        return SiteMapAddress;
    }

    public CReturnData AddPushProcessToMSMQForPCAMIP(string mIP_MSG_NO, string mSG_TARGET, string rESERVATION, string oNLINE_TIME, string oFFLINE_TIME, string dOPUSH, string mSG_KIND_1, string mSG_KIND_2, string mSG_TITLE, string mSG_CONTENT, string lDATE, string lUSER)
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="QueName"></param>
    /// <param name="strXML"></param>
    /// <returns>0:成功 -1:失敗</returns>
    [WebMethod]
    public CReturnData SendMSMQ(string QueName, object strXML)
    {
        CReturnData myData = new CReturnData();
        //SendMSMQ START==========================================
        try
        {
            MDSQueue queue = new MDSQueue(".", string.Format("{0}\\{1}", "Private$", QueName)); //2015-01-21 modify
            queue.SendMesageQueue((object)strXML);
            myData.nRet = 0;
            myData.outMsg = "SendMSMQ操作成功";
        }
        catch (Exception ex)
        {
            myData.nRet = -1;
            myData.outMsg = string.Format("SendMessageQueue failed, 錯誤訊息: {0}", ex.Message);
        }

        //SendMSMQ END==========================================

        return myData;
    }



    /// <summary>
    /// [20160818:改為參數式]
    /// 元大MIP 推播
    /// 由MCP新增推播、並分批送至MSMQ中
    /// </summary>
    /// <param name="appname"></param>
    /// <param name="phonetype"></param>
    /// <param name="limtKind"></param>
    /// <param name="limtCndi"></param>
    /// <param name="msgalert"></param>
    /// <param name="msgsound"></param>
    /// <param name="status"></param>
    /// <param name="MQueueName"></param>
    /// <returns></returns>
    [WebMethod]
    public CReturnData AddPushProcessToMSMQForICare(string pushType, string phonetype, string limtKind, string limtCndi, string msgalert, string msgsound, string status, string MQueueName)
    {
        CReturnData myData = new CReturnData();

        Database db = new Database();
        DataTable dt = new DataTable();

        string jobid = System.Guid.NewGuid().ToString();
        string Strmsgsound = "";
        if (msgsound == "1")
        {
            Strmsgsound = "default";
        }

        /*
        if (phonetype == "IOS")
        {
            phonetype = "iphone";
        }
        */

        if (limtKind.Equals("0"))
        {
            limtCndi = "";
        }

        /*連線DB*/
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;
        if (myData.nRet == 0)
        {
            /*
            string StrSQL = "INSERT INTO PushServiceData(jobid, appname, phonetype, msgalert, msgbadge, msgsound, status, createdatetime) "
                + "SELECT '" + jobid + "', '" + appname + "', '" + phonetype + "', '" + msgalert.Replace("'", "''") + "', NULL, '" + Strmsgsound + "', " + status + ", GETDATE() ";
            */




            string appname = "";
            if (pushType.StartsWith("iCare"))
            {
                appname = "icare";
            }
            else if (pushType.StartsWith("iAgent"))
            {
                appname = "iagent";
            }
            else
            {
                //全部
                appname = "iall";
            }

            string StrSQL = "INSERT INTO PushServiceData(jobid, appname, phonetype, msgalert, msgbadge, msgsound, status, createdatetime,pushtype,limtKind,limtCndi) "
                        + "SELECT  @jobid ,  @appname ,   @phonetype  , " 
                        +" @msgalert , NULL,   @Strmsgsound ,   @status , GETDATE(),  @pushType ,  @limtKind  , @limtCndi ";

            /*寫入DB*/

            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(StrSQL, db.getOcnn());

            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@jobid", jobid));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@appname", MDS.Utility.NUtility.trimBad(appname)));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@phonetype", MDS.Utility.NUtility.trimBad(phonetype)));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@msgalert", MDS.Utility.NUtility.trimBad(msgalert)));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Strmsgsound", MDS.Utility.NUtility.trimBad(Strmsgsound)));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@status", MDS.Utility.NUtility.trimBad(status)));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@pushType", MDS.Utility.NUtility.trimBad(pushType)));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@limtKind", MDS.Utility.NUtility.trimBad(limtKind)));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@limtCndi", MDS.Utility.NUtility.trimBad(limtCndi)));

            //呼叫傳入參數式sqlcmd的方法
            int affectRows = 0;
            myData.nRet = db.ExecNonQuerySQLCommand(SqlCom, ref affectRows);

            myData.outMsg = db.outMsg;

            //如果新增的資料為啟用, 進行MSMQ動作 .
            if (status == "1")
            {
                //產生推播服務所需要的XML .
                StringBuilder sb = new StringBuilder("");
                sb.Append(@"DECLARE @tempSourceData TABLE(RowID int identity(1,1) primary key not null, PushID varchar(max),phonetype varchar(50))                        
                             INSERT INTO @tempSourceData (PushID,phonetype)

                             SELECT DISTINCT pushid,phonetype
                             FROM MobileDeviceData 
                             WHERE 1=1                               
                                AND status = 1 
                                AND pushid <> '' ");

                if (appname.ToLower().Equals("icare"))
                {
                    if (pushType.Equals("iCareAll"))
                    {//iCare全部
                        sb.Append(" AND appname = 'icare' ");

                    }
                    else if (pushType.Equals("iCareInsured"))
                    {//iCare保戶 有AccountID
                        sb.Append(" AND ( appname = 'icare' AND AccountID IS NOT NULL AND AccountID <> '' ) ");
                    }
                    else if (pushType.Equals("iCarePublic"))
                    {//iCare大眾 無AccountID
                        sb.Append(" AND ( appname = 'icare' AND ( AccountID IS NULL OR AccountID = '' )   ) ");
                    }

                }
                else if (appname.ToLower().Equals("iagent"))
                {
                    sb.Append(" AND appname = 'iagent' ");
                }
                else
                {
                    //全部

                }

                if (phonetype != null && !phonetype.Equals(""))
                {
                    if (phonetype.ToLower().ToString().Equals("ios"))
                    {
                        sb.Append(" AND LOWER(phonetype) in ('ios','iphone','ipad')  ");
                    }
                    else if (phonetype.ToLower().ToString().Equals("android"))
                    {
                        sb.Append(" AND LOWER(phonetype) in ('android')  ");
                    }
                    else
                    {
                        sb.Append(" AND LOWER(phonetype) in ('ios','iphone','ipad','android')  ");
                    }
                }

                if (limtKind.Equals("1"))
                {
                    sb.Append(" AND AccountID=@limtCndi");
                }

                if (limtKind.Equals("2"))
                {
                    sb.Append(" AND deviceid=@limtCndi");
                }


                logger.Debug("推播對象:" + sb.ToString());




                sb.Append(" SELECT * from @tempSourceData ");


                StrSQL = sb.ToString();

     

                System.Data.SqlClient.SqlCommand SqlCom2 = new System.Data.SqlClient.SqlCommand(MDS.Utility.NUtility.checkString(StrSQL), db.getOcnn());
                SqlCom2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@limtCndi", MDS.Utility.NUtility.trimBad(limtCndi)));



                //呼叫傳入參數式sqlcmd的方法
                myData.nRet = db.ExecQuerySQLCommand(SqlCom2, ref dt);

                myData.outMsg = db.outMsg;




                try
                {
                    List<PushVo> pushVoList = new List<PushVo>();
                    foreach (DataRow row in dt.Rows)
                    {
                        PushVo pushVo = new PushVo();

                        //傳入參數1.可識別ID 2.推播的內容 3,目的的Queue名稱
                        //payload.DeviceToken = row["PushID"].ToString();
                        //payload.Message = msgalert.Replace("'", "''");
                        //PushHelper.SendNotification(jobID, payload, "icare");                      
                        pushVo.sound = Strmsgsound;
                        pushVo.deviceToken = row["PushID"].ToString();
                        pushVo.message = msgalert.Replace("'", "''");


                        string qphonetype = row["phonetype"].ToString();

                        if (qphonetype.ToLower().ToString().Equals("ios"))
                        {
                            pushVo.deviceType = "ios";
                        }
                        else
                        {
                            pushVo.deviceType = "android";
                        }

                        if (appname.Equals("icare"))
                        {
                            pushVo.mQueueName = "icare";
                        }
                        else
                        {
                            pushVo.mQueueName = "iagent";
                        }


                        //logger.Debug("qphonetype=" + qphonetype);
                        //logger.Debug("pushVo.message=" + pushVo.message);
                        //logger.Debug("pushVo.deviceType=" + pushVo.deviceType);
                        //logger.Debug("pushVo.mQueueName=" + pushVo.mQueueName);
                        //logger.Debug("pushVo.deviceToken=" + pushVo.deviceToken);
                        //logger.Debug("pushVo.sound=" + pushVo.sound);






                        pushVoList.Add(pushVo);

                    }//end foreach

                    MIPPushUtil.pushMessage(pushVoList);



                    int rowCnt = dt.Rows.Count;


                    /*未啟用過的才動作*/
                    StrSQL = "UPDATE PushServiceData "
                                + " SET status = 1,rowcnt= @rowCnt WHERE  "
                                    + " jobid =@jobid";                    

                    System.Data.SqlClient.SqlCommand SqlCom3 = new System.Data.SqlClient.SqlCommand(StrSQL, db.getOcnn());

                    SqlCom3.Parameters.Add(new System.Data.SqlClient.SqlParameter("@rowCnt", rowCnt));
                    SqlCom3.Parameters.Add(new System.Data.SqlClient.SqlParameter("@jobid", jobid));

                    //呼叫傳入參數式sqlcmd的方法
                    int affectedRowCnt = 0;
                    myData.nRet = db.ExecNonQuerySQLCommand(SqlCom3, ref affectedRowCnt);

                    myData.outMsg = db.outMsg;

                    myData.nRet = 0;
                    myData.outMsg = "成功將推播資訊送至MSMQ！";

                }
                catch (Exception ex)
                {
                    //this._txtInfo.Text += "error=" + ex.Message;
                    myData.outMsg = ex.Message;
                    myData.nRet = -1;
                }
                finally
                {
                    dt.Clear();
                    dt.Dispose();
                    dt = null;
                    db.DBDisconnect();
                }

            }
            else
            {
                myData.nRet = 0;
                myData.outMsg = "沒有立即需要推播的資料!!";
            }

        }

        return myData;
    }
    
    
    

    /// <summary>
    /// 所屬群組下拉選單(For AutoComplete 使用的格式)
    /// </summary>
    /// <param name="q">使用者輸入的查詢字串</param>
    /// <returns>JSON格式字串</returns>
    [WebMethod(EnableSession = true)]
    private string GetGroupList(string q)
    {
        //ParentGroupID = "Console";
        List<string> lst = new List<string>();

        string strSQL = "";
        Database db = new Database();
        DataTable dt = new DataTable();
        int nRet = -1;

        string json = "";

        nRet = db.DBConnect();
        if (nRet == 0)
        {
            strSQL = "SELECT tblA.GroupID, GroupName " +
                "FROM SecurityGroup AS tblA " +
                "INNER JOIN dbo.fn_GetGroupTree(@fn_GetGroupTree) AS tblT ON tblA.GroupID = tblT.GroupID ";

            if (q != "") {
                strSQL += "WHERE UPPER(tblA.GroupID + ' ' + GroupName) LIKE '%'+@GroupName+'%'";
            }
            

            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(MDS.Utility.NUtility.checkString(strSQL), db.getOcnn());
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@fn_GetGroupTree", Session["ParentGroupID"].ToString()));

            if (q != "")
            {
                SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GroupName", q.ToUpper()));
            }

            
            
            nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);

            if (nRet == 0)
            {

                // Serialize
                json = JSONHelper.DataTableToJson(ref dt);

                //無論是否有資料, 都要回傳陣列字串
                //strGroupList = "[" + string.Join(",", lst.ToArray()) + "]";
            }
            else
            {
                return db.outMsg;
            }

            db.DBDisconnect();
        }
        else
        {
            return db.outMsg;
        }
        //string filepath = @"c:\temp\posttestLog\";
        //System.IO.File.WriteAllText(filepath + "mdstest.txt", json + ">" + strSQL + ">"+q);
        return json;
    }


}

/// <summary>
/// 可指定編碼類型的StringWriter
/// 避免XML使用內定的UTF-16編碼
/// </summary>
public class StringWriterWithEncoding : StringWriter
{
    public StringWriterWithEncoding(StringBuilder sb, Encoding encoding)
        : base(sb)
    {
        this.m_Encoding = encoding;
    }
    private readonly Encoding m_Encoding;
    public override Encoding Encoding
    {
        get
        {
            return this.m_Encoding;
        }
    }
}

