using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MDS.Database;
using System.Data;
using System.Text;
using System.Diagnostics;
using MIP.Utility;
using System.Data.SqlClient;

public partial class yuantalife_web_YL0010Q : BasePage
{
    static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    /*ListView固定程式碼, 取得目前排序欄位, 排序方向, 頁數 START*/
    protected string ListViewSortKey = "";
    protected string ListViewSortDirection = "";
    protected string PageNo = "0";
    /*ListView固定程式碼, 取得目前排序欄位, 排序方向, 頁數 END*/
    protected string strQueryCondi = "";
    protected string uploadOK = "";
    protected string _CreateStartDate = "";
    protected string _CreateEndDate = "";
    StringBuilder sb = new StringBuilder();
    protected string strDeployDT = "";//發佈日期

    /// <summary>
    /// 因為控制項的Init事件會比Page的Init事件還要早觸發, 
    /// 而ListView的欄位是在ListView控制項的Init事件時動態產生,
    /// 所以必須在Page_PreInit時指定有哪些欄位
    /// </summary>
    /// <param name="sendor"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sendor, EventArgs e)
    {
        QueryList();
        //showDeployDT();
        setNewKind();
        setAppType();

        //關閉發布按鈕
        Deploy_Auth_Control.checkDeployPower(Session["DMSRoleID"].ToString(), ToolBar2);
    }



    protected void Page_Load(object sender, EventArgs e)
    {

        StringBuilder sb = new StringBuilder();
        foreach (string key in Request.QueryString)
        {
            sb.Append("&" + key + "=" + MDS.Utility.NUtility.trimBad(Request.QueryString[key]));
        }
        strQueryCondi = HttpUtility.UrlEncode(sb.ToString(), Encoding.UTF8);
    }

    public void showDeployDT()
    {
        UserUploadLogVo oUserUploadLogVo = MIPUserUploadHandler.getUserUploadLogBySqltype(MIPLibrary.SQLLiteHelper.NEWS_SQLITE);

        strDeployDT = oUserUploadLogVo.datetime;
        Debug.Write(strDeployDT);

    }

    private void setAppType()
    {

        string strAppType = "";
        if (!string.IsNullOrEmpty(MDS.Utility.NUtility.trimBad(Request.QueryString["_selAppType"])))
            strAppType = MDS.Utility.NUtility.trimBad(Request.QueryString["_selAppType"]);

        

    }

    private void setNewKind()
    {
        string strNewsKind = "";
        if (!string.IsNullOrEmpty(MDS.Utility.NUtility.trimBad(Request.QueryString["_selNewsKind"])))
        {
            strNewsKind = MDS.Utility.NUtility.trimBad(Request.QueryString["_selNewsKind"]);
        }

        List<CodeVo> codeVoList = MIPCodesUtil.getCodeListByLevel("A");
        _selNewsKind.Items.Clear();
        //設定商品類別
        _selNewsKind.Items.Add(new ListItem("全部", ""));
        foreach (CodeVo codeVo in codeVoList)
        {
            string codeVo_name = MDS.Utility.NUtility.HtmlEncode(MDS.Utility.NUtility.trimBad(codeVo.name));
            string codeVo_key = MDS.Utility.NUtility.HtmlEncode(MDS.Utility.NUtility.trimBad(codeVo.key));

            if (strNewsKind != null && !strNewsKind.Equals(""))
            {
                if (strNewsKind.Equals(codeVo.key))
                {
                    ListItem listItem = new ListItem(codeVo_name, codeVo_key);
                    listItem.Selected = true;
                    _selNewsKind.Items.Add(listItem);
                }
                else
                {
                    _selNewsKind.Items.Add(new ListItem(codeVo_name, codeVo_key));
                }
            }
            else
            {
                _selNewsKind.Items.Add(new ListItem(codeVo_name, codeVo_key));
            }

        }
    }
    
    private void QueryList()
    {
        string strAppType = "";//發佈對像
        string strNewsKind = "";//種類
        string strStartDate = "";//上架起始時間
        string strEndDate = "";//上架結束時間

        if (!string.IsNullOrEmpty(MDS.Utility.NUtility.trimBad(Request.QueryString["_selAppType"])))
            strAppType = MDS.Utility.NUtility.trimBad(Request.QueryString["_selAppType"]);
        if (!string.IsNullOrEmpty(MDS.Utility.NUtility.trimBad(Request.QueryString["_selNewsKind"])))
            strNewsKind = MDS.Utility.NUtility.trimBad(Request.QueryString["_selNewsKind"]);
        if (!string.IsNullOrEmpty(MDS.Utility.NUtility.trimBad(Request.QueryString["_txtStartDate"])))
            strStartDate = MDS.Utility.NUtility.trimBad(Request.QueryString["_txtStartDate"]);
        if (!string.IsNullOrEmpty(MDS.Utility.NUtility.trimBad(Request.QueryString["_txtEndDate"])))
            strEndDate = MDS.Utility.NUtility.trimBad(Request.QueryString["_txtEndDate"]);


        if (NewsListView == null)
        {
            ContentPlaceHolder MySecondContent = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
            NewsListView = MySecondContent.FindControl("NewsListView") as ASP.dmscontrol_olistview_ascx;
            //StrSearch = MySecondContent.FindControl("StrSearch") as System.Web.UI.HtmlControls.HtmlInputText;
        }
        if (NewsListView != null)
        {
            //加入欄位Start     
            NewsListView.AddCol("代碼 ", "CKEY", "CENTER", "20");
            NewsListView.AddCol("說明", "CNOTE", "LEFT", "20%");
            NewsListView.AddCol("資料內容", "CVALUE", "LEFT", "50%");
            NewsListView.AddCol("顯示位置", "CSTATUSS", "CENTER", "10%");
            
         
            //NewsListView.AddCol("提供 APP 使用", "APPLY44", "CENTER", "10%");
            //NewsListView.AddCol("備註", "MEMO", "LEFT", "15%");
            //NewsListView.AddCol("下架日期", "E_DATE", "LEFT", "10%");
            ////BosomServiceList.AddCol("資料狀態", "status", "LEFT");
            //NewsListView.AddCol("最後更新日期", "LDATA", "LEFT", "20%");

            //加入欄位End

            //設定Key值欄位
            NewsListView.DataKeyNames = "CKEY"; //Key以,隔開

            //設定是否顯示CheckBox(預設是true);
            //BosomServiceList.IsUseCheckBox = false;

            StringBuilder sbSQL = new StringBuilder();
            //設定SQL        
            sbSQL.Append(" select CKEY, CVALUE, CNOTE, CSTATUS, APPLY4, "+
                " (case when CKEY = \'MIP_AD_02\' then \'系統值不可修改\' "+
                " when CKEY = \'CUSTOMER_MAIL_TIME\'  then \'系統值不可修改\' "+
                " when CKEY = \'MIP_TOLO_CNT_02\'  then \'系統值不可修改\' "+
                 " when CKEY = \'MIP_TOLO_CNT_01\'  then \'系統值不可修改\' " +
                " when CKEY = \'YL0132A\'  then \'系統值不可修改\' " +
                " when CKEY = \'MIP_AD_01\'  then \'系統值不可修改\' "+
                " else \'非系統值可修改\' end) as MEMO, "+
                " ( case when CSTATUS ='M' then '<置中>' when CSTATUS ='L' then '<左'  else \'右>\' end) CSTATUSS, " +
                " ( case when APPLY4 = \'1\' then \'否\' else \'是\' end) APPLY44 " +
                " from MIP_SYS_INFO order by CKEY ASC; "
                );

            //發布對象
            //if (!string.IsNullOrEmpty(strAppType) && !strAppType.Equals("0"))
            //{
            //    sbSQL.Append("AND n.CKEY = '" + strAppType + "'");
            //}

            ////商品類別
            //if (!string.IsNullOrEmpty(strNewsKind))
            //{
            //    sbSQL.Append("AND n.CVALUE = '" + strNewsKind + "'");
            //}

            ////上架時間
            //if (!string.IsNullOrEmpty(strStartDate))
            //{
            //    sbSQL.Append("AND n.S_DATE >= CONVERT(datetime, '" + strStartDate.Replace("/", "") + " 00:00:00') ");
            //    _CreateStartDate = strStartDate;
            //}

            ////下架時間
            //if (!string.IsNullOrEmpty(strEndDate))
            //{
            //    sbSQL.Append("AND n.S_DATE <= CONVERT(datetime, '" + strEndDate.Replace("/", "") + " 23:59:59') ");
            //    _CreateEndDate = strEndDate;
            //}

            //sbSQL.Append(" ORDER BY n.APP_TYPE,n.BULL_ID,SORT ");

            //取得SQL;
            NewsListView.SelectString = sbSQL.ToString();
            string showSql = sbSQL.ToString();
            Debug.Write(showSql);

            //設定每筆資料按下去的Javascript function
            NewsListView.OnClickExecFunc = "DoEdt()";

            //設定每頁筆數
            NewsListView.PageSize = 26;

            //接來自Request的排序欄位、排序方向、目前頁數
            ListViewSortKey = Request.Params["ListViewSortKey"];
            ListViewSortDirection = Request.Params["ListViewSortDirection"];
            PageNo = Request.Params["PageNo"];

            //設定排序欄位及方向
            if (!string.IsNullOrEmpty(ListViewSortKey) && !string.IsNullOrEmpty(ListViewSortDirection))
            {
                NewsListView.ListViewSortKey = ListViewSortKey;
                NewsListView.ListViewSortDirection = (SortDirection)Enum.Parse(typeof(SortDirection), ListViewSortDirection);
            }

            //設定目前頁數
            if (!string.IsNullOrEmpty(PageNo))
                NewsListView.PageNo = int.Parse(PageNo);
        }
    }

    /// <summary>
    /// 發佈最新公告資料
    /// </summary>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static CReturnData Deploy()
    {
        CReturnData myData = new CReturnData();

        Database db = new Database();

        int nRet = -1;

        /*連線DB*/
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;

        try
        {
            string appPath = HttpContext.Current.Request.PhysicalApplicationPath;
            
            
            string saveDir = @"UserUpLoad\tempFile\";

            string sqlliteFilePath = appPath + saveDir + MIPLibrary.SQLLiteHelper.SYSINFO_SQLITE_NAME;
            

            MIPLibrary.SQLLiteHelper _SQLLiteHelper = new MIPLibrary.SQLLiteHelper();

            //發版日
            string product_LastUpdateTime = "";

            product_LastUpdateTime = _SQLLiteHelper.execute(MIPLibrary.SQLLiteHelper.SYSINFO_SQLITE, sqlliteFilePath, db.getOcnn());

            string strInfo = "發佈最新公告資料完成:" + sqlliteFilePath + "\r\n 發版日：" + product_LastUpdateTime;
            Debug.Write(strInfo);

            myData.nRet = 0;
            myData.outMsg = strInfo;

        }
        catch (Exception ex)
        {
            Debug.Write("YL0140Q Exception :" + ex.Message);
            myData.outMsg = ex.Message;
            throw ex;
        }
        finally
        {
            db.getOcnn().Close();
            db.DBDisconnect();
        }

        return myData;
    }

    /// <summary>
    /// 刪除最新公告
    /// </summary>
    /// <param name="strNewsList">PK Key List</param>
    /// <returns>nRet 0:刪除成功 -1, -2:失敗</returns>
    [System.Web.Services.WebMethod]
    public static CReturnData Delete_NEWS(string strNewsList)
    {
        CReturnData myData = new CReturnData();
        Database db = new Database();
        DataTable dt = new DataTable();

        int nRet = -1;

        /*連線DB*/
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;
        SqlTransaction sqlTrans = null;

        try
        {

            string[] strDelProId = strNewsList.Split(new String[] { "^^" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string delId in strDelProId)
            {

                /*產生刪除 PushServiceData推播服務紀錄資料表的SQL*/
                string strSQL = "DELETE FROM MIP_SYS_INFO WHERE CKEY=@CKEY ";

                sqlTrans = db.getOcnn().BeginTransaction();

                /*連線DB*/
                SqlCommand SqlCom = new SqlCommand(strSQL, db.getOcnn(), sqlTrans);
                SqlCom.Parameters.Add(new SqlParameter("@CKEY", delId));           
                Debug.Write("Transaction:" + SqlCom.Transaction);
                nRet = SqlCom.ExecuteNonQuery();

                string outMsg = db.outMsg;

                Debug.Write("nRet:" + nRet);
                Debug.Write("outMsg:" + outMsg);

                myData.nRet = db.nRet;
                myData.outMsg = db.outMsg;
                sqlTrans.Commit();

                if (nRet == -1)//失敗
                {
                    throw new Exception(db.outMsg);
                }

            }//for 

            
        }
        catch (Exception ex)
        {
            Debug.Write("YL0140Q->Delete_KV Exception :" + ex.Message);
            if (sqlTrans != null)
            {
                sqlTrans.Rollback();
            }

            throw ex;
        }
        finally
        {
            dt.Dispose();
            dt = null;
            db.getOcnn().Close();
            db.DBDisconnect();
        }

        return myData;
    }

    /// <summary>
    /// Keep排序欄位, 排序方向, 頁數
    /// 要寫在Page_LoadComplete是因為Page_Load的執行會優先於控制項的Load事件
    /// 若在Page_Load中去取排序及換頁, 這時候ListView尚未Load完成
    /// 會造成抓不到ListView控制項排序或換頁的結果
    /// 而LoadComplete是所有控制項都載入完成
    /// 這時候才能抓到ListView執行排序/換頁後的屬性
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            ListViewSortKey = NewsListView.ListViewSortKey;
            ListViewSortDirection = NewsListView.ListViewSortDirection.ToString();
            PageNo = NewsListView.PageNo.ToString();
        }
    }

}