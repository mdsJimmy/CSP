using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MIP.Utility;
using MDS.Database;
using System.Data;
using System.Diagnostics;
using MIPLibrary;
using System.Data.SqlClient;
using System.Text;

public partial class yuantalife_web_YL0010M : BasePage
{
    protected string strQueryCondi = "";
    protected string PageNo = "0";
    protected string strInfo = "";
    protected string result = "";
    protected string strApp4 = "";
    protected string strNewsId = "";
    protected string strAppType = "";
    protected string strBuildId = "";//
    protected string strNewsKind = "";//種類 A01 熱門,A02 新聞,A03 保戸,A04 新知,A05 重要
    protected string strNewsTitle="";//標題
    protected string strAreInfo="";//公告內容
    protected string strOrder="";//排序
    protected string strMainLinkUrl="";//主連結URL
    protected string strMainLinkTitle="";//主連結文字標題
    protected string strOneLinkUrl="";//連結1URL
    protected string strOneLinkTitle="";//連結1文字標題
    protected string strTwoLinkUrl="";//連結2URL
    protected string strTwoLinkTitle="";//連結2文字標題
    protected string strThreeeLinkUrl = "";//連結3URL
    protected string strThreeeLinkTitle="";//連結3文字標題
    protected string strStartDate="";//上架日期
    protected string strEndDate="";//下架日期
    protected string memo = "";//備註
    protected string strLDATA = "";//最後更新日期
    protected string strLUser = "";//使用者ID

    //protected string _CreateStartDate = "";
    //protected string _CreateEndDate = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        strQueryCondi = MDS.Utility.NUtility.trimBad(Request.QueryString["queryCondi"]);
        strQueryCondi = HttpUtility.UrlDecode(strQueryCondi, Encoding.UTF8);
        string buildId = "";
        if (!string.IsNullOrEmpty(Request.QueryString["buildId"]))
        {
            Response.Write(buildId);
            buildId = MDS.Utility.NUtility.trimBad(Request.QueryString["buildId"]);
            strNewsId = buildId;
        }
            
        
        if (buildId != null && !buildId.Equals(""))
        {
            queryNewsData(buildId);
        }

        //初始化設定種類
        setNewKind();
        //初始化設定發布對象 
        setApp4();
    }

   

    private void setNewKind()
    {
        _selNewsKind.Items.Add(new ListItem("MIP系統資料", "0"));
        _selNewsKind.Items.Add(new ListItem("測試用資料", "1"));
        //發布對象                    
        if (strNewsKind.Equals("0"))
        {
            _selNewsKind.Items[0].Selected = true;
        }
        else if (strNewsKind.Equals("1"))
        {
            _selNewsKind.Items[1].Selected = true;
        }
       


    }
    private void setApp4()//是否啟用
    {
        _selAPP4.Items.Add(new ListItem("請選擇", ""));
        _selAPP4.Items.Add(new ListItem("否", "1"));
        _selAPP4.Items.Add(new ListItem("是", "0"));
        
        //發布對象                    
        if (strApp4.Equals("1"))
        {
            _selAPP4.Items[1].Selected = true;
        }
        else if (strApp4.Equals("0"))
        {
            _selAPP4.Items[2].Selected = true;
        }
       
    }

    /// <summary>
    /// 查詢最新公告資訊
    /// </summary>
    /// <param name="proId"></param>
    private void queryNewsData(string CKEY)
    {
        Database db = new Database();
        DataTable dt = new DataTable();
        strAppType = CKEY;
        int nRet = -1;

        /*Query Data */
        //string strSQL = "SELECT　* FROM MIP_PRODUCT WHERE PRO_ID = " + proId;
        string strSQL = " select * ," +
                " (case when CKEY = \'MIP_AD_02\' then \'系統值不可修改\' " +
                " when CKEY = \'CUSTOMER_MAIL_TIME\'  then \'系統值不可修改\' " +
                " when CKEY = \'MIP_TOLO_CNT_02\'  then \'系統值不可修改\' " +
                " when CKEY = \'YL0132A\'  then \'系統值不可修改\' " +
                " when CKEY = \'MIP_AD_01\'  then \'系統值不可修改\' " +
                " else \'非系統值可修改\' end) as MEMO, " +
                " ( case when CSTATUS =\'1\' then \'啟用\' else \'停用\' end) CSTATUSS, " +
                " ( case when APPLY4 = \'1\' then \'啟用\' else \'停用\' end) APPLY44 " +
                " from MIP_KV where CKEY = @CKEY ; "; ;

        try
        {
            //db.DBConnect();
            /*連線DB*/
            //nRet = db.ExecQuerySQLCommand(strSQL, ref dt);
            SqlParameter[] parameter = new SqlParameter[]{
            	new SqlParameter("@CKEY", CKEY)   
            };

            dt = Database.GetDataTable(strSQL, parameter);
            
            foreach (DataRow row in dt.Rows)
            {
                
                strNewsKind = row["CSTATUS"].ToString();//狀態
                strApp4= row["APPLY4"].ToString();//提供app使用
                strNewsTitle = row["CNOTE"].ToString();//說明
                strInfo = row["CVALUE"].ToString();//內容
                memo = row["MEMO"].ToString();//備註
            }

        }
        catch (Exception ex)
        {
            Debug.Write("YL0104M Exception :" + ex.Message);
            throw ex;
        }
      

    }

    /// <summary>
    /// 新增最新公告
    /// </summary>
    /// <param name="strAppType">代碼</param> CKEY
    /// <param name="strNewsKind">是否啟用</param>CSTATUS
    /// <param name="strNewsTitle">說明</param>CNOTE
    /// <param name="strInfo">資料內容</param>CVALUE
    /// <param name="strAPP4">app顯示</param>APPLY4

    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static CReturnData UpdateNewsProcess(string strAppType, string strNewsKind, string strAPP4, string strNewsTitle, string strInfo)
    {
        int nRet = -1;
        Database db = new Database();
        DataTable dt = new DataTable();
        CReturnData myData = new CReturnData();
        string StrSQL = " ";
        





            /*連線DB*/
            myData.nRet = db.DBConnect();
            myData.outMsg = db.outMsg;


            if (myData.nRet == 0)
            {
                //key           
                int buildId = MIPUtil.getFILE_INDEX_SEQ(db.getOcnn());
                //strAppType = row["CKEY"].ToString();//SEQUENCE
                //strNewsKind = row["CSTATUS"].ToString();//種類
                //strApp4 = row["APPLY4"].ToString();//公告標題
                //strNewsTitle = row["CNOTE"].ToString();//公告內容
                //strInfo = row["CVALUE"].ToString();//分類項目排序
                try
                {
                    //最新公告更新
                    StrSQL = "UPDATE MIP_KV set " +
                            "CSTATUS=@CSTATUS," +
                            "APPLY4=@APPLY4," +
                            "CNOTE=@CNOTE," +
                            "CVALUE=@CVALUE" +
                            " WHERE "
                            + " CKEY = @CKEY";
                    //key           


                    SqlCommand SqlCom = new SqlCommand(StrSQL, db.getOcnn());


                    SqlCom.Parameters.Add(new SqlParameter("@CKEY", strAppType));//代碼

                    SqlCom.Parameters.Add(new SqlParameter("@CSTATUS", strNewsKind));//公告標題

                    SqlCom.Parameters.Add(new SqlParameter("@APPLY4", strAPP4));//公告內容

                    SqlCom.Parameters.Add(new SqlParameter("@CNOTE", strNewsTitle));//應用程式類別

                    SqlCom.Parameters.Add(new SqlParameter("@CVALUE", strInfo));//種類 A01 熱門,A02 新聞,A03 保戸,A04 新知,A05 重要



                    nRet = SqlCom.ExecuteNonQuery();
                    //nRet = db.ExecQuerySQLCommand(StrSQL, ref dt);

                    string outMsg = db.outMsg;

                    Debug.Write("nRet:" + nRet);
                    Debug.Write("outMsg:" + outMsg);

                }
                catch (Exception ex)
                {
                    Debug.Write("YL0010M Exception :" + ex.Message);

                    throw ex;
                }
                finally
                {
                    dt.Dispose();
                    dt = null;
                    db.getOcnn().Close();
                    db.DBDisconnect();
                }

            }


        

        return myData;
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {

    }
}