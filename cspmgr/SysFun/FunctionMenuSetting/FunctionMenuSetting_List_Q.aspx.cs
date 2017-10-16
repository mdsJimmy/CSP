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
    //protected string _CreateStartDate = "";
    //protected string _CreateEndDate = "";

    //protected string strDeployDT = "";//發佈日期

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
        selSysModID();
        //showDeployDT();
        //setNewKind();
        //setAppType();

        //關閉發布按鈕
        //Deploy_Auth_Control.checkDeployPower(Session["DMSRoleID"].ToString(), ToolBar2);
    }




    protected void Page_Load(object sender, EventArgs e)
    {
      

        


        StringBuilder sb = new StringBuilder();
        foreach (string key in Request.QueryString)
        {
            sb.Append("&" + key + "=" + Request.QueryString[key]);
        }
        
        strQueryCondi = HttpUtility.UrlEncode(sb.ToString(), Encoding.UTF8);
        //html輸出的時候做urlEncode
        strQueryCondi = sb.ToString();

        

    }

    

    private void selSysModID() {
        string strSysModID = "";
        MipSystemModule mip = new MipSystemModule();
        mip.SQL= " select * from SystemModule ORDER BY iOrder ";
        mip.KEY = "SysModID";
        mip.NAME = "ModuleDesc";
        if (!string.IsNullOrEmpty(Request.QueryString["_selSystemModule"])) {
            strSysModID = MDS.Utility.NUtility.trimBad(Request.QueryString["_selSystemModule"]);
           
        }
        List<CodeVo> codeVoList = mip.getCodeListByLevel();
        
        //設定模組
        _selSystemModule.Items.Add(new ListItem("全部", ""));
        
        foreach (CodeVo codeVo in codeVoList) {
           
            string codeVo_name = MDS.Utility.NUtility.HtmlEncode(MDS.Utility.NUtility.trimBad(codeVo.name));
            string codeVo_key = MDS.Utility.NUtility.HtmlEncode(MDS.Utility.NUtility.trimBad(codeVo.key));
            ListItem listItem = new ListItem(
                codeVo_name,
                codeVo_key
                );
            
            _selSystemModule.Items.Add(listItem);
        }

        if (!string.IsNullOrEmpty(Request.QueryString["_selSystemModule"]))
        {
            
            _selSystemModule.Items.FindByValue((Request.QueryString["_selSystemModule"])).Selected = true;
        }


    }
   

    private void QueryList()
    {
        
        string selSystemModuled = "";
        

        
        if (!string.IsNullOrEmpty(Request.QueryString["_selSystemModule"]))
            selSystemModuled = MDS.Utility.NUtility.trimBad(Request.QueryString["_selSystemModule"]);
        

        if (NewsListView == null)
        {
            ContentPlaceHolder MySecondContent = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
            NewsListView = MySecondContent.FindControl("NewsListView") as ASP.dmscontrol_olistview_ascx;
            
        }

        if (NewsListView != null)
        {
            //加入欄位Start      
            NewsListView.AddCol("功能ID", "SysFuncID", "LEFT", "30%");
            NewsListView.AddCol("系統模組名稱", "ModuleDesc", "CENTER", "10%");
            NewsListView.AddCol("功能名稱", "FunctionDesc", "CENTER", "20%");
            NewsListView.AddCol("Uri", "PageLink", "LEFT", "10%");
            NewsListView.AddCol("圖片", "Pic", "CENTER", "8%");
            NewsListView.AddCol("排序", "fOrder", "CENTER", "2%");
            NewsListView.AddCol("顯示", "Display", "CENTER", "2%");

            //加入欄位End

            //設定Key值欄位
            NewsListView.DataKeyNames = "SysFuncID"; //Key以,隔開

            //設定是否顯示CheckBox(預設是true);
            //BosomServiceList.IsUseCheckBox = false;

            StringBuilder sbSQL = new StringBuilder();
            //設定SQL        
            sbSQL.Append("  select f.SysFuncID , f.FunctionDesc, f.PageLink, f.pic , f.SysModID, m.ModuleDesc , m.iOrder , f.iOrder as fOrder , m.isDefault, f.iDisplay as Display from SystemModule m  FULL OUTER JOIN  SystemFunction f on m.SysModID = f.SysModID ");



            //模組
            if (!string.IsNullOrEmpty(selSystemModuled))
            {
                sbSQL.Append("  where f.SysModID=@SysModID ");

                NewsListView.putQueryParameter("SysModID", selSystemModuled);

            }

            sbSQL.Append(" order by ModuleDesc , f.iOrder");

            //設定SQL;
            NewsListView.SelectString = sbSQL.ToString();
            //完成參數值填充
            NewsListView.prepareStatement();


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
   

    /// <summary>
    /// 刪除最新公告
    /// </summary>
    /// <param name="strNewsList">PK Key List</param>
    /// <returns>nRet 0:刪除成功 -1, -2:失敗</returns>
    /// 

    [System.Web.Services.WebMethod]
    public static CReturnData Delete_NEWS(string strNewsList)
    {
        CReturnData myData = new CReturnData();
        Database db = new Database();
        DataTable dt = new DataTable();

        int nRet = -1;

        /*連線DB*/

        SqlTransaction sqlTrans = null;

        try
        {

            myData.nRet = db.DBConnect();
            myData.outMsg = db.outMsg;

            string[] strDelProId = strNewsList.Split(new String[] { "^^" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string delId in strDelProId)
            {

                /*產生刪除 PushServiceData推播服務紀錄資料表的SQL*/
                string strSQL = "DELETE FROM SystemFunction WHERE SysFuncID=@SysFuncID ";

                sqlTrans = db.getOcnn().BeginTransaction();

                /*連線DB*/
                SqlCommand SqlCom = new SqlCommand(strSQL, db.getOcnn(), sqlTrans);
                SqlCom.Parameters.Add(new SqlParameter("@SysFuncID", delId));           
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
            Debug.Write("FunctionMenuSetting_List_Q->Delete_NEWS Exception :" + ex.Message);
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