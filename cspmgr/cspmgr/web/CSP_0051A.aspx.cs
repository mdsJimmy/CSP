using System;
using System.Collections.Generic;
using MDS.Database;
using System.Data;
using System.Diagnostics;
using System.Text;
using MIPLibrary;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using mattraffel.com.CodeGenTest;
using MIP.Utility;

public partial class csp_web_CSP_0051A : BasePage
{
    static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected string ListViewSortKey = "";
    protected string ListViewSortDirection = "";

    protected string PageNo = "0";
    protected string jsonResult0 = "";
    protected string strQueryCondi = "";
    protected int HAPPY_ID = 0;
    string str_txtUrl = "";
    string str_dlDataType = "";
    string str_txtTitle = "";

    public string uploadOK = "";
    int FileKind = 0;//1：無檔案 
   







    //初始化資料
    protected void Page_PreInit(object sendor, EventArgs e)
    {
        QueryList();
    }

    //抓網頁的#id的值
    protected void Page_Load(object sender, EventArgs e)
    {
       
        ((DataPager)oListView.FindControl("_DataPager")).Visible = false;
        strQueryCondi = Request.QueryString["queryCondi"];
        strQueryCondi = HttpUtility.UrlDecode(strQueryCondi, Encoding.UTF8);


        string strMethod = "";
        if (!string.IsNullOrEmpty(Request.Params["_hidMethod"]))
            strMethod = MDS.Utility.NUtility.trimBad(Request.Params["_hidMethod"].ToString().Trim());


        string str_radType = "";
        if (!string.IsNullOrEmpty(Request.Params["_radType"])) 
            str_radType = MDS.Utility.NUtility.trimBad(Request.Params["_radType"].ToString().Trim());

        
        //判斷資料類型 是否要寫入資料至畫面上
        if (!string.IsNullOrEmpty(Request.Params["_dlDataType"]))
        {
            str_dlDataType = MDS.Utility.NUtility.trimBad(Request.Params["_dlDataType"].ToString().Trim());
            
            
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
                    if (!string.IsNullOrEmpty(Request.Params["_txtUrl"]))
                            str_txtUrl = MDS.Utility.NUtility.trimBad(Request.Params["_txtUrl"].ToString().Trim());
                        break;

                    case "B3010":
                    FileKind = 0;
                    if (!string.IsNullOrEmpty(Request.Params["_txtTitle"]))
                            str_txtTitle = MDS.Utility.NUtility.trimBad(Request.Params["_txtTitle"].ToString().Trim());
                        break;

                    case "B3020":
                    FileKind = 0;
                    if (!string.IsNullOrEmpty(Request.Params["_txtTitle"]))
                        str_txtTitle = MDS.Utility.NUtility.trimBad(Request.Params["_txtTitle"].ToString().Trim());
                    break;

                    case "B3030":
                    FileKind = 0;
                    if (!string.IsNullOrEmpty(Request.Params["_txtTitle"]))
                        str_txtTitle = MDS.Utility.NUtility.trimBad(Request.Params["_txtTitle"].ToString().Trim());
                    break;


                default:
                        break;

                }
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

        string chk = null;
        if (!string.IsNullOrEmpty(Request.Params["_hid_chk"]))
            chk = Request.Params["_hid_chk"];

        //第一欄：僅供測試人員察看
        string str_chkTesterView = "";
        if (_chkTesterView.Checked)
        { str_chkTesterView = "0"; }
        else
        { str_chkTesterView = "1"; };

       
        //兩個欄位寫在 addMipLifeMain 方法裡
        //string isChkALL = null;
        //if (Request.Params["chkALL"] == "on") { isChkALL = "0"; } else { isChkALL = "1"; }
        //string isRCorRM = "";
        //if (!string.IsNullOrEmpty(_chkRoler.SelectedValue))
        //    isRCorRM = _chkRoler.SelectedValue;


        if (!Page.IsPostBack)
        {
           
        }
        else if (strMethod != null && !strMethod.Equals(""))
        {
            addMipLifeMain(str_radType, str_dlDataType, str_dlDataClass, str_txtTitle, str_txtUrl, str_txtOrder, str_radStatus, chk, str_chkTesterView);
        }
       

    }
   
    //群組表單
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
            oListView.AddCol("群組代號", "PCAGROUP_ID", "center", "16%");
            oListView.AddCol("群組名稱", "PCAGROUP_NAME", "center", "17%");
            oListView.AddCol("單位代號", "DEPT_ID", "center", "16%");
            oListView.AddCol("簡稱", "NICK_NAME", "center", "16%");
            oListView.AddCol("單位名稱", "DEPT_NAME", "left", "35%");


            //設定Key值欄位
            oListView.DataKeyNames = "PCAGROUP_ID,DEPT_ID"; //Key以,隔開

            //設定是否顯示CheckBox(預設是true);
            //BosomServiceList.IsUseCheckBox = false;

            StringBuilder sbSQL = new StringBuilder();
            //設定SQL        
            sbSQL.Append(@"select
                            c.PCAGROUP_ID as PCAGROUP_ID
                            ,c.PCAGROUP_NAME as PCAGROUP_NAME
                            ,b.DEPT_ID as DEPT_ID
                            ,b.DEPT_NAME as DEPT_NAME
                            ,b.NICK_NAME as NICK_NAME
                            from MIP_PCAGROUP c,MIP_PCAGROUP_DEPT a, MIP_PCADEPT b
                            where c.PCAGROUP_ID=a.PCAGROUP_ID and a.DEPT_ID=b.DEPT_ID and  c.LOGINLIST!=0 order  by c.corder
                             ");
           





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

    //透過 ajax 動態產生 資料類型
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

        list.Insert(0, (new Dictionary<string, string>() { { "CKEY", "" }, { "CNAME", "請選擇" } }));
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        string jsonResult = serializer.Serialize(list);
        myData.returnData = jsonResult;
        myData.nRet = 0;
        return myData;
    }
    //透過 ajax 動態產生 資料分類
    [System.Web.Services.WebMethod]
    public static CReturnData getProKind2(string strProKind2)//B1010
    {
        CReturnData myData = new CReturnData();

        //MipSystemModule model = new MipSystemModule();
        //model.SQL = "SELECT * FROM MIP_CODES WHERE CLEVEL=@CLEVEL AND CSTATUS='0' and CKEY!='B2020'   ORDER BY CORDER ";
        //model.KEY = "CKEY";
        //model.NAME = "CNAME";
        //model.rowParameters= new SqlParameter("@CLEVEL", strProKind2);
        //List<Dictionary<string, string>> list = model.getProductList();

        List<Dictionary<string, string>> list = MIPCode.getProductList(strProKind2);


        list.Insert(0, (new Dictionary<string, string>() { { "CKEY", "" }, { "CNAME", "請選擇" } }));
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        //string jsonResult = fastJSON.JSON.ToJSON(list);
        string jsonResult = serializer.Serialize(list);

        myData.returnData = jsonResult;
        myData.nRet = 0;

        return myData;
    }


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
                    myData.outMsg =dt.Rows[0][0].ToString();
                    
                      
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally {
                dt.Dispose();
                dt = null;
                db.getOcnn().Close();
                db.DBDisconnect();

            }


        }
            return myData;
    }



    //寫入db MIP_HAPPY
    private void addMipLifeMain(
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
        if (!string.IsNullOrEmpty((Request.Params["_isRcOrRm"]))) {            
            isRCorRM = (Request.Params["_isRcOrRm"]);           
        }

        

        int nRet = -1;

        CReturnData myData = new CReturnData();
        //string mlireply = "";
        Database db = new Database();
        DataTable dt = new DataTable();
        StringBuilder sbSQL = new StringBuilder();
        SqlTransaction sqlTrans = null;
        System.Data.SqlClient.SqlCommand cmd = null;
        /*連線DB*/
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;

        HAPPY_ID = MIPUtil.getFILE_INDEX_SEQ(db.getOcnn());

        if (myData.nRet == 0)
        {
            System.Data.SqlClient.SqlConnection connection = db.getOcnn();
            sqlTrans = connection.BeginTransaction();

            try
            {
                using (cmd = new System.Data.SqlClient.SqlCommand())
                {
                    
                    cmd.Connection = connection;
                    cmd.Transaction = sqlTrans;
                    cmd.Parameters.Clear();
                    //key
                    //int strLifeId = MIPUtil.getFILE_INDEX_SEQ(db.getOcnn(), sqlTrans);

                    //新增
                    sbSQL.Append(" insert into ");
                    sbSQL.Append(" MIP_HAPPY ");
                    sbSQL.Append(" (HAPPY_ID, CSTATUS , APPLY_TARGET, CKEY1, CKEY2, CKEY3, TITLE, CORDER, LDATE, LUSER, SELECTALL, ISTESTER) ");
                    sbSQL.Append(" values(@HAPPY_ID, @CSTATUS, @APPLY_TARGET, @CKEY1, @CKEY2, @CKEY3, @TITLE,  @CORDER, @LDATE, @LUSER, @SELECTALL, @ISTESTER) ");

                    // 宣告DAO
                    MIP_HAPPY mip_happy_impl = new MIP_HAPPY();

                    // 設定參數
                    mip_happy_impl.HAPPY_ID = HAPPY_ID;
                    mip_happy_impl.CSTATUS = int.Parse(str_radStatus);

                    mip_happy_impl.APPLY_TARGET = 0;
                    mip_happy_impl.CKEY1 = str_radType;
                    mip_happy_impl.CKEY2 = str_dlDataType;
                    mip_happy_impl.CKEY3 = str_dlDataClass;
                    mip_happy_impl.TITLE = str_txtTitle;

                    mip_happy_impl.CORDER = int.Parse(str_txtOrder);
                    mip_happy_impl.SELECTALL = int.Parse(isChkALL);
                    mip_happy_impl.LDATE = DateTime.Now;
                    mip_happy_impl.LUSER = HttpContext.Current.Session["UserID"].ToString();
                    mip_happy_impl.ISTESTER = int.Parse(str_chkTesterView);

                    // 執行
                    nRet = mip_happy_impl.Insert(cmd);

                    //新增成功
                    if (nRet != -1)
                    {
                        nRet = insert_MIP_MSG_TARGET(cmd, isChkALL, HAPPY_ID, isRCorRM, chk);
                        if (nRet == -1)
                        {

                            MessageBox("新增資料失敗!!");
                            cmd.Transaction.Rollback();
                        }
                    }

                    //新增成功
                    if (nRet != -1)
                    {
                        //儲存上傳檔案 
                        nRet = uploadFileProcess(cmd);

                        if (nRet == -1)
                        {
                            MessageBox("新增資料失敗!!");

                            cmd.Transaction.Rollback();
                        }
                        else
                        {
                            cmd.Transaction.Commit();
                            uploadOK = "SUCCESS";
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
                MessageBox("新增資料失敗!!");
               

               
                cmd.Transaction.Rollback();

                
                    
                
                
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

    //寫入db MIP_HAPPY_TARGET
    private int insert_MIP_MSG_TARGET(System.Data.SqlClient.SqlCommand cmd
                                                , string isChkALL
                                                , int HAPPY_ID
                                                , string isRCorRM
                                                , string IDsandDEP)
    {
        int nRet = 0;
        string isIc = "";
        string isRm = "";
        int length = 0;
        isIc = isRCorRM.Split('.')[0];
        isRm = isRCorRM.Split('.')[1];
        length = isRCorRM.Split('.').Length;

        // 宣告DAO
        MIP_HAPPY_TARGET mip_happy_target_impl = new MIP_HAPPY_TARGET();

        //新增角色:IC 或 RM
        cmd.Parameters.Clear();

        string strSQL = "INSERT INTO MIP_HAPPY_TARGET(HAPPY_TARGET_ID, HAPPY_ID,  DEPT_ID,DTYPE)VALUES (@HAPPY_TARGET_ID, @HAPPY_ID,@DEPT_ID,@DTYPE)";
        cmd.CommandText = strSQL;
        int HAPPY_TARGET_ID = 0;
        for (int c = 0; c < length; c++) {
            if (isIc != "") {
                cmd.Parameters.Clear();
                HAPPY_TARGET_ID = MIPLibrary.MIPUtil.getFILE_INDEX_SEQ(cmd.Connection, cmd.Transaction);
                mip_happy_target_impl.HAPPY_TARGET_ID = HAPPY_TARGET_ID;//tagertID編號
                mip_happy_target_impl.HAPPY_ID = HAPPY_ID;//MIP_HAPPY 編號
                mip_happy_target_impl.DEPT_ID = isIc;//資料為 IC 或 RM
                mip_happy_target_impl.DTYPE = 0;//0:角色;1:單位
                nRet = mip_happy_target_impl.Insert(cmd);

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
                mip_happy_target_impl.HAPPY_TARGET_ID = HAPPY_TARGET_ID;//tagertID編號
                mip_happy_target_impl.HAPPY_ID = HAPPY_ID;//MIP_HAPPY 編號
                mip_happy_target_impl.DEPT_ID = isRm;//資料為 IC 或 RM
                mip_happy_target_impl.DTYPE = 0;//0:角色;1:單位
                mip_happy_target_impl.Insert(cmd);

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

                mip_happy_target_impl.HAPPY_TARGET_ID = HAPPY_TARGET_ID;//tagertID編號
                mip_happy_target_impl.HAPPY_ID = HAPPY_ID;//mipmsg 編號
                mip_happy_target_impl.PCAGROUP_ID = id;//群組id
                mip_happy_target_impl.DEPT_ID = depID;//單位id
                mip_happy_target_impl.DTYPE = 1;//0:角色;1:單位
                nRet = mip_happy_target_impl.Insert_all(cmd);

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
                HAPPY_TARGET_ID = MIPLibrary.MIPUtil.getFILE_INDEX_SEQ(cmd.Connection,cmd.Transaction);

                mip_happy_target_impl.HAPPY_TARGET_ID = HAPPY_TARGET_ID;//tagertID編號
                mip_happy_target_impl.HAPPY_ID = HAPPY_ID;//mipmsg 編號
                mip_happy_target_impl.PCAGROUP_ID = get["PCAGROUP_ID"];//群組id
                mip_happy_target_impl.DEPT_ID = get["DEPT_ID"];//單位id
                mip_happy_target_impl.DTYPE = 1;//0:角色;1:單位
                nRet = mip_happy_target_impl.Insert_all(cmd);

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
    private  List<Dictionary<string, string>> getPCAGROUP_IDandDEPT_ID(SqlCommand cmd)
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

        int nRet = 0;
        if (FileKind == 0 )//0:檔案連結 1:url連結
        {
           


            strSQL = "UPDATE MIP_Happy set FILE_KIND=@FILE_KIND ,URL=@URL WHERE HAPPY_ID=@HAPPY_ID ";
            cmd.CommandText = strSQL;
            cmd.Parameters.Add(new SqlParameter("@FILE_KIND", FileKind));
            cmd.Parameters.Add(new SqlParameter("@URL", str_txtUrl));//
            cmd.Parameters.Add(new SqlParameter("@HAPPY_ID", HAPPY_ID));
            nRet = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            
            if (nRet == -1)
            {
                cmd.Transaction.Rollback();
                return nRet;//失敗
            }


            if (str_dlDataType == "B2010")//是藏寶圖 含檔案  及url連結 無標題
            {
                strSQL = "UPDATE MIP_Happy set FILE_KIND=@FILE_KIND ,TITLE=@TITLE,  URL=@URL  WHERE HAPPY_ID=@HAPPY_ID ";
                cmd.CommandText = strSQL;

                cmd.Parameters.Add(new SqlParameter("@TITLE", DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@FILE_KIND", "2"));
                cmd.Parameters.Add(new SqlParameter("@URL", str_txtUrl));//
                cmd.Parameters.Add(new SqlParameter("@HAPPY_ID", HAPPY_ID));

                nRet = cmd.ExecuteNonQuery();
              
                cmd.Parameters.Clear();
                if (nRet == -1)
                {

                    cmd.Transaction.Rollback();
                    return nRet;
                }
            }









            //含檔案 無url 只要是有檔案 必會經過這裡

            //Logger.buger(logger,"準備經過檔案區");

            if (FileUpload1.FileBytes.Length == 0 && FileUpload2.FileBytes.Length == 0) {
                MessageBox("您上傳的檔案無資料內容，請確認您的檔案內容資料是否存在。");
                
                return -1;//失敗
            }

            if (FileUpload1.HasFile)
            {
                byte[] byteArray = System.Text.Encoding.Default.GetBytes((FileUpload1.FileName));

                if (byteArray.Length < 50)
                {
                    int idxFile = fileManager.saveFileToDB(cmd.Connection, cmd.Transaction, FileUpload1.FileName, FileUpload1.FileBytes);
                    strSQL = "UPDATE MIP_Happy set  F_IDX=@F_IDX, F_NAME=@F_NAME  WHERE HAPPY_ID=@HAPPY_ID ";//;DELETE FROM MIP_FILE_STORE WHERE FILE_INDEX=@FILE_INDEX;";
                    cmd.CommandText = strSQL;
                    cmd.Parameters.Add(new SqlParameter("@F_IDX", idxFile));
                    cmd.Parameters.Add(new SqlParameter("@F_NAME", FileUpload1.FileName));
                    cmd.Parameters.Add(new SqlParameter("@HAPPY_ID", HAPPY_ID));
                    //SqlCom.Parameters.Add(new SqlParameter("@FILE_INDEX", sHidDmIdx.Value));
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
            if (FileUpload2.HasFile) {

                byte[] byteArray = System.Text.Encoding.Default.GetBytes((FileUpload2.FileName));
                
                if (byteArray.Length < 50)
                {
                    int idxFile = fileManager.saveFileToDB(cmd.Connection, cmd.Transaction, FileUpload2.FileName, FileUpload2.FileBytes);
                    strSQL = "UPDATE MIP_Happy set  F_IDX=@F_IDX, F_NAME=@F_NAME  WHERE HAPPY_ID=@HAPPY_ID ";//;DELETE FROM MIP_FILE_STORE WHERE FILE_INDEX=@FILE_INDEX;";
                    cmd.CommandText = strSQL;
                    cmd.Parameters.Add(new SqlParameter("@F_IDX", idxFile));
                    cmd.Parameters.Add(new SqlParameter("@F_NAME", FileUpload2.FileName));
                    cmd.Parameters.Add(new SqlParameter("@HAPPY_ID", HAPPY_ID));
                    //SqlCom.Parameters.Add(new SqlParameter("@FILE_INDEX", sHidDmIdx.Value));
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
            
            strSQL = "UPDATE MIP_Happy set FILE_KIND=@FILE_KIND, F_IDX=@F_IDX,URL=@URL, F_NAME=@F_NAME WHERE HAPPY_ID=@HAPPY_ID ";//; DELETE FROM MIP_FILE_STORE WHERE FILE_INDEX=@FILE_INDEX;";
            cmd.CommandText = strSQL;
            cmd.Parameters.Add(new SqlParameter("@F_IDX", DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@FILE_KIND", FileKind));
            cmd.Parameters.Add(new SqlParameter("@URL", str_txtUrl));//
            cmd.Parameters.Add(new SqlParameter("@HAPPY_ID", HAPPY_ID));
            //SqlCom.Parameters.Add(new SqlParameter("@FILE_INDEX", sHidDmIdx.Value));
            cmd.Parameters.Add(new SqlParameter("@F_NAME", DBNull.Value));//
            nRet = cmd.ExecuteNonQuery();
           
            cmd.Parameters.Clear();
        
            if (nRet == -1)
            {
                cmd.Transaction.Rollback();
                return nRet;//失敗
            }
            return nRet;
        }



        
        


    }




    protected void Page_LoadComplete(object sender, EventArgs e)
    {
    }
}