using System;
using System.Collections.Generic;
using MIP.Utility;
using MDS.Database;
using System.Data;
using System.Diagnostics;
using System.Text;
using MIPLibrary;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using mip.dao;

public partial class csp_web_CSP_0051M : BasePage
{

    //static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected string ListViewSortKey = "";
    protected string ListViewSortDirection = "";

    protected string PageNo = "0";
    protected string strQueryCondi = "";
    protected int HAPPY_ID = 0;
    public string uploadOK = "";
    int FileKind = 0;


    protected string strProId = "";
    protected string str_radType = "";
    protected string str_dlDataType = "";
    protected string str_dlDataClass = "";
    protected string str_txtTitle = "";
    protected string str_txtUrl = "";
    protected string str_txtOrder = "";
    protected string proIdPK = "";//網址參數取得 happyID
    protected string strDmIdx = "";//檔案索引
    protected string str_radStatus = "";
    protected string strSelectAll = "";

    protected string strIstester = "";

    protected string strDEPT_ID = "";
    protected string strDept = "";
    protected string strICRM = "";
    protected string strIC = "";
    protected string strRM = "";
    protected List<string> ICRM = new List<string>();
    protected List<string> MSG_TARGET_ID = new List<string>();
    protected List<string> PCAGROUP_ID = new List<string>();
    protected List<string> DEPT_ID = new List<string>();
    protected List<string> DTYPE = new List<string>();





    protected void Page_PreInit(object sendor, EventArgs e)
    {
        QueryList();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        callBtnController();
        ((DataPager)oListView.FindControl("_DataPager")).Visible = false;

        strQueryCondi = Request.QueryString["queryCondi"];
        strQueryCondi = HttpUtility.UrlDecode(strQueryCondi, Encoding.UTF8);

        string strMethod = _hidMethod.Value;
       

        string str_radType = "";
        if (!string.IsNullOrEmpty(Request.Params["_radType"])) 
            str_radType = MDS.Utility.NUtility.trimBad(Request.Params["_radType"].ToString().Trim());

        string proId = "";//由50q傳值過來，將資料記錄下來 刪除用
        if (!string.IsNullOrEmpty(Request.QueryString["proId"]))
        {
            proId = MDS.Utility.NUtility.trimBad(Request.QueryString["proId"]);
           
        }
        

        if (!string.IsNullOrEmpty(Request.Params["_dlDataType"]))
        {
            str_dlDataType = MDS.Utility.NUtility.trimBad(Request.Params["_dlDataType"].ToString().Trim());

            #region 判斷是否寫入資料
            switch (str_dlDataType)
                {
                    case "B1010":
                        FileKind = 1;
                        if (!string.IsNullOrEmpty(Request.Params["_txtTitle"]))
                            str_txtTitle = MDS.Utility.NUtility.trimBad(Request.Params["_txtTitle"].ToString().Trim());
                        if (!string.IsNullOrEmpty(Request.Params["_txtUrl"]))
                            str_txtUrl = MDS.Utility.NUtility.trimBad(Request.Params["_txtUrl"].ToString().Trim());
                        break;
                    case "B1020":
                            FileKind = 0;
                            if (!string.IsNullOrEmpty(Request.Params["_txtTitle"]))
                            str_txtTitle = MDS.Utility.NUtility.trimBad(Request.Params["_txtTitle"].ToString().Trim());
                       
                        break;
                    case "B2010":
                            FileKind = 0;
                            if (!string.IsNullOrEmpty(Request.Params["_txtUrl"])) { 
                                str_txtUrl = MDS.Utility.NUtility.trimBad(Request.Params["_txtUrl"].ToString().Trim());
                                str_txtTitle = "";
                            }
                            break;
                    case "B3010":
                            FileKind = 0;
                            if (!string.IsNullOrEmpty(Request.Params["_txtTitle"]))
                            str_txtTitle = MDS.Utility.NUtility.trimBad(Request.Params["_txtTitle"].ToString().Trim());
                        
                        break;
                    default:
                        if (!string.IsNullOrEmpty(Request.Params["_txtTitle"]))
                            str_txtTitle = MDS.Utility.NUtility.trimBad(Request.Params["_txtTitle"].ToString().Trim());
                        break;

                }
            #endregion
        }

        string str_dlDataClass = "";
        if (!string.IsNullOrEmpty(Request.Params["_dlDataClass"]))
            str_dlDataClass = MDS.Utility.NUtility.trimBad(Request.Params["_dlDataClass"].ToString().Trim());

        string str_txtOrder = "";
        if (!string.IsNullOrEmpty(Request.Params["_txtOrder"]))
            str_txtOrder = MDS.Utility.NUtility.trimBad(Request.Params["_txtOrder"].ToString().Trim());

        string str_radStatus = "";
        if (!string.IsNullOrEmpty(Request.Params["_radStatus"]))
            str_radStatus = MDS.Utility.NUtility.trimBad(Request.Params["_radStatus"].ToString().Trim());

        if (!string.IsNullOrEmpty(Request.Params["proId"]))
            proIdPK = MDS.Utility.NUtility.trimBad(Request.Params["proId"].ToString().Trim());

        string chk = null;
        if (!string.IsNullOrEmpty(Request.Params["_hid_chk"]))
            chk = Request.Params["_hid_chk"];

        //第一欄：僅供測試人員察看
        string str_chkTesterView = "";
        if (_chkTesterView.Checked)
        { str_chkTesterView = "0"; }
        else
        { str_chkTesterView = "1"; };


        if (strMethod.Equals("MODIFY"))
        {
       
            updateHappyMain(str_radType, str_dlDataType, str_dlDataClass, str_txtTitle, str_txtUrl, str_txtOrder, str_radStatus, chk, str_chkTesterView);

        }
        else
        {
            if (proId != null && !proId.Equals(""))
            {
                queryProductData(proId);//ok
                
                QueryListData(proId);//ok
            }
            



        }



       


    }

    #region 修改資料happymain
    private void updateHappyMain(
        string str_radType
        , string str_dlDataType
        , string str_dlDataClass
        , string str_txtTitle
        , string str_txtUrl
        , string str_txtOrder
        , string str_radStatus
        , string chk
        , string str_chkTesterView)
    {


        
        string isChkALL = null;
        if (Request.Params["_hidChkALL"] == "0") { isChkALL = "0"; } else { isChkALL = "1"; }

        string isRCorRM = "";
        if (!string.IsNullOrEmpty((Request.Params["_isRcOrRm"])))
        {
            isRCorRM = (Request.Params["_isRcOrRm"]);
          
        }
        else {
            isRCorRM = strDEPT_ID;
          
        }
        

        int nRet = -1;

        CReturnData myData = new CReturnData();
        
        Database db = new Database();
        DataTable dt = new DataTable();

        StringBuilder sbSQL = new StringBuilder();

        SqlTransaction sqlTrans = null;
        System.Data.SqlClient.SqlCommand cmd = null;

        /*連線DB*/
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;


        if (myData.nRet == 0)
        {
            System.Data.SqlClient.SqlConnection connection = db.getOcnn();
            sqlTrans = connection.BeginTransaction(); 

            try
            {
                using (cmd = new SqlCommand())
                {

                    cmd.Connection = connection;
                    cmd.Transaction = sqlTrans;
                    cmd.Parameters.Clear();

                    //新增
                    sbSQL.Append(" UPDATE MIP_Happy ");
                    sbSQL.Append(" SET ");
                    sbSQL.Append(" CSTATUS = @CSTATUS ");

                    sbSQL.Append(" , APPLY_TARGET = @APPLY_TARGET ");
                    sbSQL.Append(" , CKEY1 = @CKEY1 ");
                    sbSQL.Append(" , CKEY2 = @CKEY2 ");
                    sbSQL.Append(" , CKEY3 = @CKEY3 ");
                    sbSQL.Append(" , TITLE = @TITLE ");
                    sbSQL.Append(" , CORDER = @CORDER ");
                    sbSQL.Append(" , LDATE = @LDATE ");
                    sbSQL.Append(" , LUSER = @LUSER ");
                    sbSQL.Append(" , SELECTALL = @SELECTALL ");
                    sbSQL.Append(" , ISTESTER = @ISTESTER ");
                    sbSQL.Append(" where HAPPY_ID = @HAPPY_ID ");




                    /*連線DB*/

                    
                    cmd.Parameters.Add(new SqlParameter("@HAPPY_ID", proIdPK));//主表key
                    
                    cmd.Parameters.Add(new SqlParameter("@CSTATUS", str_radStatus));//啟用
                    cmd.Parameters.Add(new SqlParameter("@FILE_KIND", DBNull.Value));//0:檔案 1:連結 2:階有

                    cmd.Parameters.Add(new SqlParameter("@APPLY_TARGET", DBNull.Value));//
                    cmd.Parameters.Add(new SqlParameter("@CKEY1", str_radType));//資料分類一
                    cmd.Parameters.Add(new SqlParameter("@CKEY2", str_dlDataType));//資料分類二
                    cmd.Parameters.Add(new SqlParameter("@CKEY3", str_dlDataClass));//資料分類三
                    cmd.Parameters.Add(new SqlParameter("@TITLE", str_txtTitle));//

                    cmd.Parameters.Add(new SqlParameter("@CORDER", str_txtOrder));//排序
                    cmd.Parameters.Add(new SqlParameter("@SELECTALL", isChkALL));//全選 或 非全選
                    cmd.Parameters.Add(new SqlParameter("@LDATE", MIPUtil.getDataTimeNow()));
                    cmd.Parameters.Add(new SqlParameter("@LUSER", HttpContext.Current.Session["UserID"].ToString()));

                    cmd.Parameters.Add(new SqlParameter("@ISTESTER", str_chkTesterView));

                    cmd.CommandText = sbSQL.ToString();
                    nRet = cmd.ExecuteNonQuery();
                   
                    //新增成功 開始新增 發送對像
                    if (nRet != -1 )
                    {
                       
                        nRet = insert_MIP_MSG_TARGET(cmd, isChkALL, proIdPK, isRCorRM, chk);
                        if (nRet == -1)
                        {
                            MessageBox("新增資料失敗!!");
                            cmd.Transaction.Rollback();
                            
                        }

                    }
                    


                    //新增成功 開始新增檔案 若有檔案便刪除
                    if (nRet != -1)
                    {
                        //儲存上傳檔案 
                        
                        nRet= uploadFileProcess(cmd);
                        if (nRet == -1)
                        {
                            MessageBox("新增資料失敗!!");
                            cmd.Transaction.Rollback();

                        }
                        else {
                            cmd.Transaction.Commit();
                            uploadOK = "SUCCESS";

                        }

                      
                       
                    }
                    

                }

            }
            catch (Exception ex)
            {

                
                MessageBox("新增資料失敗!!");
               
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
    }
    #endregion

    //寫入db MIP_HAPPY_TARGET
    private int insert_MIP_MSG_TARGET(System.Data.SqlClient.SqlCommand cmd
                                                , string isChkALL
                                                , string HAPPY_ID
                                                , string isRCorRM
                                                , string IDsandDEP)
    {
        
        int nRet = 0;
        string isIc = "";
        string isRm = "";
        int length = 0;
       
        length = isRCorRM.Split(',').Length;
       
        if (length == 1) {
            isIc = isRCorRM.Split(',')[0].Equals("IC") ? "IC":"" ;
            isRm = isRCorRM.Split(',')[0].Equals("RM") ? "RM" : "";

        } else if (length == 2) {
            isIc = isRCorRM.Split(',')[0].Equals("IC") ? "IC" : "";
            isRm = isRCorRM.Split(',')[1].Equals("RM") ? "RM" : "";
          
        }
        cmd.Parameters.Clear();

        string delSql = " delete from MIP_HAPPY_TARGET where HAPPY_ID = @HAPPY_ID ";
        cmd.CommandText = delSql;
        cmd.Parameters.Add(new SqlParameter("@HAPPY_ID", HAPPY_ID));//tagertID編號
        nRet = cmd.ExecuteNonQuery();
        if (nRet == -1)
        {

            cmd.Transaction.Rollback();
            return nRet;
        }

        //新增角色:IC 或 RM
        cmd.Parameters.Clear();

        string strSQL = "INSERT INTO MIP_HAPPY_TARGET(HAPPY_TARGET_ID, HAPPY_ID,  DEPT_ID,DTYPE)VALUES (@HAPPY_TARGET_ID, @HAPPY_ID,@DEPT_ID,@DTYPE)";
        cmd.CommandText = strSQL;
        int HAPPY_TARGET_ID = 0;
        for (int c = 0; c < length; c++)
        {
            if (isIc != "")
            {
                
                cmd.Parameters.Clear();
                HAPPY_TARGET_ID = MIPLibrary.MIPUtil.getFILE_INDEX_SEQ(cmd.Connection, cmd.Transaction);
                cmd.Parameters.Add(new SqlParameter("@HAPPY_TARGET_ID", HAPPY_TARGET_ID));//tagertID編號
                cmd.Parameters.Add(new SqlParameter("@HAPPY_ID", HAPPY_ID));//MIP_HAPPY 編號
                cmd.Parameters.Add(new SqlParameter("@DEPT_ID", isIc));//資料為 RC 或 RM
                cmd.Parameters.Add(new SqlParameter("@DTYPE", "0"));//0:角色;1:單位
                nRet = cmd.ExecuteNonQuery();
                isIc = "";
                if (nRet == -1)
                {
                    cmd.Transaction.Rollback();
                    return nRet;
                }
            }
            if (isRm != "")
            {
                cmd.Parameters.Clear();
                HAPPY_TARGET_ID = MIPLibrary.MIPUtil.getFILE_INDEX_SEQ(cmd.Connection, cmd.Transaction);
                cmd.Parameters.Add(new SqlParameter("@HAPPY_TARGET_ID", HAPPY_TARGET_ID));//tagertID編號
                cmd.Parameters.Add(new SqlParameter("@HAPPY_ID", HAPPY_ID));//MIP_HAPPY 編號
                //cmd.Parameters.Add(new SqlParameter("@PCAGROUP_ID", DBNull.Value));//群組id
                cmd.Parameters.Add(new SqlParameter("@DEPT_ID", isRm));//資料為 RC 或 RM

                cmd.Parameters.Add(new SqlParameter("@DTYPE", "0"));//0:角色;1:單位
                nRet = cmd.ExecuteNonQuery();
                isRm = "";
                if (nRet == -1)
                {
                    cmd.Transaction.Rollback();
                    return nRet;
                }
            }


        }

        //新增單位
        if (isChkALL == "1")
        {

            string strSQL2 = "INSERT INTO MIP_HAPPY_TARGET(HAPPY_TARGET_ID, HAPPY_ID, PCAGROUP_ID, DEPT_ID,DTYPE)VALUES (@HAPPY_TARGET_ID, @HAPPY_ID, @PCAGROUP_ID, @DEPT_ID,@DTYPE)";
            string[] IDsandDEPs = IDsandDEP.Split(new String[] { "^^" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string IdDepID in IDsandDEPs)
            {
                cmd.Parameters.Clear();
                HAPPY_TARGET_ID = MIPLibrary.MIPUtil.getFILE_INDEX_SEQ(cmd);
                cmd.CommandText = strSQL2;

                string id = IdDepID.Split(new String[] { "##" }, StringSplitOptions.RemoveEmptyEntries)[0];
                string depID = IdDepID.Split(new String[] { "##" }, StringSplitOptions.RemoveEmptyEntries)[1];

                cmd.Parameters.Add(new SqlParameter("@HAPPY_TARGET_ID", HAPPY_TARGET_ID));//tagertID編號
                cmd.Parameters.Add(new SqlParameter("@HAPPY_ID", HAPPY_ID));//mipmsg 編號
                cmd.Parameters.Add(new SqlParameter("@PCAGROUP_ID", id));//群組id
                cmd.Parameters.Add(new SqlParameter("@DEPT_ID", depID));//單位id
                cmd.Parameters.Add(new SqlParameter("@DTYPE", "1"));
                nRet = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                if (nRet == -1)
                {

                    cmd.Transaction.Rollback();
                    return nRet;
                }
            }

            return nRet;
        }
        else if (isChkALL == "0")
        {
            //建立新的 command  承接 參數 避免 弄錯
            SqlCommand cmd1 = new SqlCommand();
            cmd1 = cmd;

            //取得 對像名單
            List<Dictionary<string, string>> targetList = getPCAGROUP_IDandDEPT_ID(cmd1);
            cmd.Parameters.Clear();

            string strSQL3 = " INSERT INTO MIP_HAPPY_TARGET (HAPPY_TARGET_ID, HAPPY_ID,PCAGROUP_ID, DEPT_ID, DTYPE)values(@HAPPY_TARGET_ID, @HAPPY_ID, @PCAGROUP_ID, @DEPT_ID, @DTYPE) ";
            cmd.CommandText = strSQL3;

            foreach (Dictionary<string, string> get in targetList)
            {

                cmd.Parameters.Clear();
                HAPPY_TARGET_ID = MIPLibrary.MIPUtil.getFILE_INDEX_SEQ(cmd.Connection, cmd.Transaction);

                cmd.Parameters.Add(new SqlParameter("@HAPPY_TARGET_ID", HAPPY_TARGET_ID));//tagertID編號
                cmd.Parameters.Add(new SqlParameter("@HAPPY_ID", HAPPY_ID));//mipmsg 編號
                cmd.Parameters.Add(new SqlParameter("@PCAGROUP_ID", get["PCAGROUP_ID"]));//群組id
                cmd.Parameters.Add(new SqlParameter("@DEPT_ID", get["DEPT_ID"]));//單位id
                cmd.Parameters.Add(new SqlParameter("@DTYPE", "1"));
                nRet = cmd.ExecuteNonQuery();

                cmd.Parameters.Clear();
                if (nRet == -1)
                {
                    cmd.Transaction.Rollback();
                    return nRet;
                }

            }
        }

        return nRet;
    }

    //全選的時後取得對像名單 table 值
    private List<Dictionary<string, string>> getPCAGROUP_IDandDEPT_ID(SqlCommand cmd)
    {
        //cmd.Parameters.Clear();
        DataTable dt = null;
        List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

        using (dt = new DataTable())
        {
            string PCAGROUP_ID = "";
            string DEPT_ID = "";



            string strSQL = "SELECT PCAGROUP_ID, DEPT_ID  FROM MIP_PCAGROUP_DEPT";
            //System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(strSQL, db.getOcnn());
            //db.ExecQuerySQLCommand(SqlCom, ref dt);
            cmd.CommandText = strSQL;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);




            foreach (DataRow row in dt.Rows)
            {
                Dictionary<string, string> MyDic = new Dictionary<string, string>();
                PCAGROUP_ID = row["PCAGROUP_ID"].ToString();
                DEPT_ID = row["DEPT_ID"].ToString();
                MyDic.Add("PCAGROUP_ID", PCAGROUP_ID);
                MyDic.Add("DEPT_ID", DEPT_ID);
                list.Add(MyDic);
            }


        }
        return list;


    }

    #region 修改上傳檔案
    /// <summary>
    /// 儲存上傳檔案 
    /// </summary>
    /// <param name="oConn"></param>
    /// <param name="proId"></param>
    private int uploadFileProcess(SqlCommand cmd)
    {
        FileManager fileManager = new FileManager();
        string strSQL = "";
        cmd.Parameters.Clear();
        
        int nRet = -1;
        if (FileKind == 0 || FileKind==2)
        {//檔案:0
           
            //如果是檔案且非尋寶圖一定經過這裡設定 URL、檔案類型
            strSQL = "UPDATE MIP_Happy set  FILE_KIND=@FILE_KIND ,URL=@URL WHERE HAPPY_ID=@HAPPY_ID ";
            cmd.CommandText = strSQL;
            
            cmd.Parameters.Add(new SqlParameter("@URL", DBNull.Value));//
            cmd.Parameters.Add(new SqlParameter("@FILE_KIND", FileKind));
            cmd.Parameters.Add(new SqlParameter("@HAPPY_ID", proIdPK));
            nRet = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            if (nRet == -1)
            {
                MessageBox("新增資料失敗!!");
                cmd.Transaction.Rollback();
                return nRet;
            }
            
            if (str_dlDataType == "B2010")
            {//如果是尋寶圖，標題為NULL
                strSQL = "UPDATE MIP_Happy set FILE_KIND=@FILE_KIND , TITLE=@TITLE,  URL=@URL WHERE HAPPY_ID=@HAPPY_ID ";
               
                cmd.Parameters.Add(new SqlParameter("@TITLE", DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@FILE_KIND", "2"));
                cmd.Parameters.Add(new SqlParameter("@HAPPY_ID", proIdPK));
                cmd.Parameters.Add(new SqlParameter("@URL", str_txtUrl));//
                cmd.CommandText = strSQL;
                nRet = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                if (nRet == -1)
                {
                    MessageBox("新增資料失敗!!");
                    cmd.Transaction.Rollback();
                    return nRet;
                }


            }
         

            if (FileUpload1.HasFile)//是檔案且有上傳新檔案 設定檔案索引值、檔案名; 不管是不尋寶圖都會經過判斷
            {
                byte[] byteArray = System.Text.Encoding.Default.GetBytes((FileUpload1.FileName));
                if (byteArray.Length < 50)
                {
                    int idxFile = fileManager.saveFileToDB(cmd.Connection, cmd.Transaction, FileUpload1.FileName, FileUpload1.FileBytes);
                    strSQL = "UPDATE MIP_Happy set  F_IDX=@F_IDX, F_NAME=@F_NAME  WHERE HAPPY_ID=@HAPPY_ID ;DELETE FROM MIP_FILE_STORE WHERE FILE_INDEX=@FILE_INDEX;";
                    cmd.CommandText = strSQL;

                    cmd.Parameters.Add(new SqlParameter("@F_IDX", idxFile));
                    cmd.Parameters.Add(new SqlParameter("@F_NAME", FileUpload1.FileName));
                    cmd.Parameters.Add(new SqlParameter("@HAPPY_ID", proIdPK));
                    cmd.Parameters.Add(new SqlParameter("@FILE_INDEX", sHidDmIdx.Value));
                    nRet = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    if (nRet == -1)
                    {
                        MessageBox("新增資料失敗!!");
                        cmd.Transaction.Rollback();
                        return nRet;
                    }
                }
                else
                {

                    MessageBox("您上傳的檔名最多50字元，請重新命名後再重新命名後上傳該檔案。");
                    return nRet = -1;
                }


            }
            if (FileUpload2.HasFile)
            {

                byte[] byteArray = System.Text.Encoding.Default.GetBytes((FileUpload2.FileName));

                if (byteArray.Length < 50)
                {
                    int idxFile = fileManager.saveFileToDB(cmd.Connection, cmd.Transaction, FileUpload2.FileName, FileUpload2.FileBytes);
                    strSQL = "UPDATE MIP_Happy set  F_IDX=@F_IDX, F_NAME=@F_NAME  WHERE HAPPY_ID=@HAPPY_ID ";//;DELETE FROM MIP_FILE_STORE WHERE FILE_INDEX=@FILE_INDEX;";
                    cmd.CommandText = strSQL;
                    cmd.Parameters.Add(new SqlParameter("@F_IDX", idxFile));
                    cmd.Parameters.Add(new SqlParameter("@F_NAME", FileUpload2.FileName));
                    cmd.Parameters.Add(new SqlParameter("@HAPPY_ID", proIdPK));
                    cmd.Parameters.Add(new SqlParameter("@FILE_INDEX", sHidDmIdx.Value));
                    nRet = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    if (nRet == -1)
                    {
                        cmd.Transaction.Rollback();
                        return nRet;//失敗
                    }
                }
                else
                {

                    MessageBox("您上傳的檔名最多50字元，請重新命名後再重新命名後上傳該檔案。");
                    return nRet = -1;
                }
            }



                return nRet;//成功
        }
        else
        {
            strSQL = "UPDATE MIP_Happy set TITLE=@TITLE, FILE_KIND=@FILE_KIND, F_IDX=@F_IDX,URL=@URL, F_NAME=@F_NAME WHERE HAPPY_ID=@HAPPY_ID ; DELETE FROM MIP_FILE_STORE WHERE FILE_INDEX=@FILE_INDEX;";
            cmd.CommandText = strSQL;
            cmd.Parameters.Add(new SqlParameter("@F_IDX", DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@TITLE", str_txtTitle));
            cmd.Parameters.Add(new SqlParameter("@FILE_KIND", FileKind));
            cmd.Parameters.Add(new SqlParameter("@URL", str_txtUrl));//
            cmd.Parameters.Add(new SqlParameter("@HAPPY_ID", proIdPK));
            cmd.Parameters.Add(new SqlParameter("@FILE_INDEX", sHidDmIdx.Value));
            cmd.Parameters.Add(new SqlParameter("@F_NAME", DBNull.Value));//
            nRet = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            if (nRet == -1)
            {
                MessageBox("新增資料失敗!!");
                cmd.Transaction.Rollback();
                return nRet;
            }
        }

        return nRet;





    }
    #endregion

    #region 資料類型
    [System.Web.Services.WebMethod]
    public static CReturnData getProKind(string strProKind)//B10
    {
        CReturnData myData = new CReturnData();
        MipSystemModule model = new MipSystemModule();
        model.SQL = "SELECT * FROM MIP_CODES WHERE CLEVEL=@CLEVEL AND CSTATUS='0' and CKEY!='B2020'   ORDER BY CORDER ";
        model.KEY = "CKEY";
        model.NAME = "CNAME";
        model.rowParameters = new SqlParameter("@CLEVEL", strProKind);
        List<Dictionary<string, string>> list = model.getProductList();
        //List<Dictionary<string, string>> list = MIPCode.getProductList(strProKind);

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //string jsonResult = fastJSON.JSON.ToJSON(list);
        string jsonResult = serializer.Serialize(list);
        myData.returnData = jsonResult;
        myData.nRet = 0;
        return myData;
    }
    #endregion

    #region 資料分類
    [System.Web.Services.WebMethod]
    public static CReturnData getProKind2(string strProKind2)//B1010
    {
        CReturnData myData = new CReturnData();

        List<Dictionary<string, string>> list = MIPCode.getProductList(strProKind2);


       
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        //string jsonResult = fastJSON.JSON.ToJSON(list);
        string jsonResult = serializer.Serialize(list);

        myData.returnData = jsonResult;
        myData.nRet = 0;

        return myData;
    }
    #endregion

    //判斷尋寶圖 的分類是否已存在資料
    [System.Web.Services.WebMethod]
    public static CReturnData getProKind3(string strProKind3)//B1010
    {
        CReturnData myData = new CReturnData();


        Database db = new Database();
        DataTable dt = new DataTable();
        StringBuilder sbSQL = new StringBuilder();
        System.Data.SqlClient.SqlCommand cmd = null;
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;
        if (myData.nRet == 0)
        {
            try
            {
                using (cmd = new System.Data.SqlClient.SqlCommand())
                {
                    cmd.Connection = db.getOcnn();
                    sbSQL.Append("select count(*) from MIP_HAPPY where CKEY3=@CKEY3");
                    cmd.CommandText = sbSQL.ToString();
                    cmd.Parameters.Add(new SqlParameter("@CKEY3", strProKind3));
                    db.ExecQuerySQLCommand(cmd, ref dt);
                    myData.outMsg = dt.Rows[0][0].ToString();



                }
            }
            catch (Exception)
            {

                throw;
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

    #region 連線db並撈資料出來
    /// <summary>
    /// 查詢商品資訊
    /// </summary>
    /// <param name="proId"></param>
    private void queryProductData(string HAPPY_ID)
    {
       
        Database db = new Database();
        DataTable dt = new DataTable();

        int nRet = -1;

        /*Query Data */
        //string strSQL = "SELECT　* FROM MIP_PRODUCT WHERE PRO_ID = " + proId;
        string strSQL = "select HAPPY_ID, CSTATUS, FILE_KIND, APPLY_TARGET, CKEY1, CKEY2, CKEY3, TITLE, URL, F_IDX, F_NAME, CORDER, LDATE, LUSER ,selectall, ISTESTER from MIP_Happy WHERE HAPPY_ID = @HAPPY_ID";

        try
        {
            //db.DBConnect();
            /*連線DB*/
            //nRet = db.ExecQuerySQLCommand(strSQL, ref dt);
            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@HAPPY_ID", HAPPY_ID)
            };

            dt = Database.GetDataTable(strSQL, parameter);
            foreach (DataRow row in dt.Rows)
            {
               
                strProId = row["HAPPY_ID"].ToString();
                str_radType= row["CKEY1"].ToString();
                str_dlDataType = row["CKEY2"].ToString();
                str_dlDataClass = row["CKEY3"].ToString();
                str_txtTitle = row["TITLE"].ToString();
                str_txtUrl = row["URL"].ToString();
                str_txtOrder = row["CORDER"].ToString();
                str_radStatus= row["CSTATUS"].ToString();
                strDmIdx = row["F_IDX"].ToString();
                strSelectAll= row["selectall"].ToString();
                strIstester = row["ISTESTER"].ToString();

                //檔案索引
                strDmIdx = row["F_IDX"].ToString();
                sHidDmIdx.Value = MDS.Utility.NUtility.checkString(strDmIdx);
                voMIP_FILE_STORE _DM_FILE_STORE = getFileStore(strDmIdx);
                if (_DM_FILE_STORE != null && _DM_FILE_STORE.FILE_ORI_NAME != null)
                {
                    if (str_dlDataType == "B2010")
                    {
                        _hypLinkIdx.Text = "檔名:" + MDS.Utility.NUtility.HtmlEncode(MDS.Utility.NUtility.checkString(_DM_FILE_STORE.FILE_ORI_NAME.Trim()));
                        _hypLinkIdx.NavigateUrl = Page.ResolveUrl("~/MDSAPI/FileProvider.ashx?FILE_INDEX=" + strDmIdx);
                    }
                    else {
                        HyperLink1.Text = "檔名:" + MDS.Utility.NUtility.HtmlEncode(MDS.Utility.NUtility.checkString(_DM_FILE_STORE.FILE_ORI_NAME.Trim()));
                        HyperLink1.NavigateUrl = Page.ResolveUrl("~/MDSAPI/FileProvider.ashx?FILE_INDEX=" + strDmIdx);

                    }
                }
            }

            selectDataType();
            selectDataClass();
            setData();


        }
        catch (Exception ex)
        {
            Debug.Write("YL0050M Exception :" + ex.Message);
            throw ex;
        }
        


    }
    #endregion

    #region 查詢 MIP_MSG_TARGET 編輯 滾表單
    private void QueryListData(string HAPPY_ID)
    {


        

            Database db = new Database();
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            int nRet = -1;

            /*Get Data START*/

            sb.Append(" select ");
            sb.Append(" Happy_TARGET_ID");
            sb.Append(" ,Happy_ID ");
            sb.Append(" ,PCAGROUP_ID ");
            sb.Append(" ,DEPT_ID ");
            sb.Append(" ,DTYPE  ");
            sb.Append(" from MIP_HAPPY_TARGET where Happy_ID=@Happy_ID ");
            try
            {

                /*連線DB*/
                //nRet = db.ExecQuerySQLCommand(strSQL, ref dt);
                SqlParameter[] parameter = new SqlParameter[]{
                    new SqlParameter("@Happy_ID", HAPPY_ID)
                };

                dt = Database.GetDataTable(sb.ToString(), parameter);


                foreach (DataRow row in dt.Rows)
                {

                    if (row["PCAGROUP_ID"].ToString() == "")
                    {
                        ICRM.Add(row["DEPT_ID"].ToString());
                        strIC = row["DEPT_ID"].ToString().Equals("IC") ? "IC" : "";
                        strRM = row["DEPT_ID"].ToString().Equals( "RM") ? "RM" : "";
                    }
                    else
                    {
                        DEPT_ID.Add(row["DEPT_ID"].ToString());
                        PCAGROUP_ID.Add(row["PCAGROUP_ID"].ToString());
                    }

                }

                if (DEPT_ID.Count != 0)
                {
                    strICRM = string.Join(",", ICRM);
                    
                    strDEPT_ID = DEPT_ID[0];
                    strDept = string.Join(",", DEPT_ID);
                }
                else
                {

                    strDEPT_ID = "";
                    strDept = "";
                   
                }

                


        }
            catch (Exception ex)
            {
                Debug.Write("PushService_edt Exception :" + ex.Message);
                throw ex;
            }
            finally
            {
                dt.Dispose();
                dt = null;
                db.getOcnn().Close();
                nRet = db.DBDisconnect();
            }



            /*有錯誤則跳出警示視窗*/
            if (nRet != 0)
                MessageBox(nRet.ToString());


        

        /*Get Data END*/
    }
    #endregion

    #region 從db撈資料並設定
    private void setData()
    {
        _radType.SelectedValue = str_radType;
        _dlDataType.SelectedValue = str_dlDataType;
        _dlDataClass.SelectedValue = str_dlDataClass;
        
        _txtTitle.Text = str_txtTitle;
        _txtOrder.Text = str_txtOrder;
        _radStatus.SelectedValue = str_radStatus;
        _txtUrl.Text = str_txtUrl;

    }
    #endregion

    #region 資料類型
    private void selectDataType()
    {
        
        List<CodeVo> codeVoList = MIPCodesUtil.getCodeListByLevel(str_radType);
        //設定商品類別           
        
        foreach (CodeVo codeVo in codeVoList)
        {
            string codeVo_name = MDS.Utility.NUtility.HtmlEncode(MDS.Utility.NUtility.trimBad(codeVo.name));
            string codeVo_key = MDS.Utility.NUtility.HtmlEncode(MDS.Utility.NUtility.trimBad(codeVo.key));

            if (codeVo.key.Equals(str_radType))
            {
                ListItem oListItem = new ListItem(codeVo_name, codeVo_key);
                oListItem.Selected = true;
                _dlDataType.Items.Add(oListItem);
            }
            else
            {
                _dlDataType.Items.Add(new ListItem(codeVo_name, codeVo_key));
            }

        }


    }
    private void selectDataClass()
    {
        
        List<CodeVo> codeVoList = MIPCodesUtil.getCodeListByLevel(str_dlDataType);
        //設定商品類別           
        
        foreach (CodeVo codeVo in codeVoList)
        {
            string codeVo_name = MDS.Utility.NUtility.HtmlEncode(MDS.Utility.NUtility.trimBad(codeVo.name));
            string codeVo_key = MDS.Utility.NUtility.HtmlEncode(MDS.Utility.NUtility.trimBad(codeVo.key));

            if (codeVo.key.Equals(str_dlDataClass))
            {
                ListItem oListItem = new ListItem(codeVo_name, codeVo_key);
                oListItem.Selected = true;
                _dlDataClass.Items.Add(oListItem);
            }
            else
            {
                _dlDataClass.Items.Add(new ListItem(codeVo_name, codeVo_key));
            }

        }
    }

    #endregion

    #region 資料分類
    private voMIP_FILE_STORE getFileStore(string fileIdx)
    {

        Database db = new Database();
        FileManager fileManager = new FileManager();
        voMIP_FILE_STORE _DM_FILE_STORE = null;

        try
        {
            db.DBConnect();

            _DM_FILE_STORE = fileManager.getFileByKey(fileIdx, db.getOcnn());

        }
        catch (Exception ex)
        {

            //throw ex;
        }
        finally
        {
            db.getOcnn().Close();
            db.DBDisconnect();
        }

        return _DM_FILE_STORE;
    }
    #endregion

    #region 有檔案就刪除
    /// <summary>
    /// 修改程序(AJAX)
    /// </summary>
    ///<param name="proid">產品主key</param>
    /// <param name="fileid">檔案編號</param>
    /// <param name="filetype">檔案類型</param>
    /// <returns>nRet 0:成功 -1, -2:失敗</returns>
    [System.Web.Services.WebMethod]
    public static CReturnData DelFileProcess(string proid, string fileid)
    {
        CReturnData myData = new CReturnData();
         Database db = new Database();
        DataTable dt = new DataTable();

        /*連線DB*/
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;
        if (myData.nRet == 0)
        {

            StringBuilder sql = new StringBuilder("UPDATE MIP_HAPPY ");
            
                sql.Append("SET F_IDX = null ,F_NAME=null");
            
           

            sql.Append(" where HAPPY_ID =").Append(proid);

            ///*寫入DB*/
            db.BeginTranscation();
            db.AddDmsSqlCmd(sql.ToString());
            db.CommitTranscation();

            myData.nRet = db.nRet;
            myData.outMsg = db.outMsg;
            new FileManager().deleteFileByKey(fileid, db.getOcnn());
            db.DBDisconnect();
        }
        return myData;
    }
    #endregion

    #region 群組表單
    private void QueryList()
    {

        //string strStatus = "";


        if (oListView == null)
        {
            ContentPlaceHolder MySecondContent = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
            oListView = MySecondContent.FindControl("oListView") as ASP.dmscontrol_olistview_ascx;
            //StrSearch = MySecondContent.FindControl("StrSearch") as System.Web.UI.HtmlControls.HtmlInputText;
        }

        if (oListView != null)
        {
            //加入欄位Start      
            oListView.AddCol("群組代號", "PCAGROUP_ID", "center", "20%");
            oListView.AddCol("群組名稱", "PCAGROUP_NAME", "center", "20%");
            oListView.AddCol("單位代號", "DEPT_ID", "center", "20%");
            oListView.AddCol("單位名稱", "DEPT_NAME", "left", "40%");


            //設定Key值欄位
            oListView.DataKeyNames = "PCAGROUP_ID,DEPT_ID"; //Key以,隔開

            //設定是否顯示CheckBox(預設是true);
            //BosomServiceList.IsUseCheckBox = false;

            StringBuilder sbSQL = new StringBuilder();
            //設定SQL        
            sbSQL.Append("select ");
            sbSQL.Append("c.PCAGROUP_ID as PCAGROUP_ID ");
            sbSQL.Append(",c.PCAGROUP_NAME as PCAGROUP_NAME ");
            sbSQL.Append(",b.DEPT_ID as DEPT_ID ");
            sbSQL.Append(",b.DEPT_NAME as DEPT_NAME ");
            sbSQL.Append("from MIP_PCAGROUP c,MIP_PCAGROUP_DEPT a, MIP_PCADEPT b ");
            sbSQL.Append(" where c.PCAGROUP_ID=a.PCAGROUP_ID and a.DEPT_ID=b.DEPT_ID and  c.LOGINLIST!=0 order  by c.corder ");





            //取得SQL;
            oListView.SelectString = sbSQL.ToString();
            oListView.prepareStatement();
            string showSql = sbSQL.ToString();
            Debug.Write(showSql);

            //設定每筆資料按下去的Javascript function
            //oListView.OnClickExecFunc = "DoEdt()";

            //設定每頁筆數
            oListView.PageSize = 1000000;

            //接來自Request的排序欄位、排序方向、目前頁數
            ListViewSortKey = Request.Params["ListViewSortKey"];
            ListViewSortDirection = Request.Params["ListViewSortDirection"];
            PageNo = Request.Params["PageNo"];

            //設定排序欄位及方向
            if (!string.IsNullOrEmpty(ListViewSortKey) && !string.IsNullOrEmpty(ListViewSortDirection))
            {
                oListView.ListViewSortKey = ListViewSortKey;
                oListView.ListViewSortDirection = (SortDirection)Enum.Parse(typeof(SortDirection), ListViewSortDirection);
            }

            //設定目前頁數
            if (!string.IsNullOrEmpty(PageNo))
                oListView.PageNo = int.Parse(PageNo);
        }



    }
    #endregion

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
    }
}