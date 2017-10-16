using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MDS.Database;

using System.IO;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Text;


public partial class SysFun_SqliteUpload_SqliteUpload_List : BasePage
{
    protected string mySearch = "";
    

    Database db = new Database();
    DataTable dt = new DataTable();

    /*ListView固定程式碼, 取得目前排序欄位, 排序方向, 頁數 START*/
    protected string ListViewSortKey = "";
    protected string ListViewSortDirection = "";
    protected string PageNo = "0";
    /*ListView固定程式碼, 取得目前排序欄位, 排序方向, 頁數 END*/


    /// <summary>
    /// 因為控制項的Init事件會比Page的Init事件還要早觸發, 
    /// 而ListView的欄位是在ListView控制項的Init事件時動態產生,
    /// 所以必須在Page_PreInit時指定有哪些欄位
    /// </summary>
    /// <param name="sendor"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sendor, EventArgs e)
    {
        string strSQL = "";

        /*取得使用者GroupID*/
        
        /*接收Request*/
        if (!string.IsNullOrEmpty(Request.QueryString["StrSearch"]))
            mySearch = Request.QueryString["StrSearch"];

        if (SqlliteList == null)
        {
            ContentPlaceHolder MySecondContent = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
            SqlliteList = MySecondContent.FindControl("SqlliteList") as ASP.dmscontrol_olistview_ascx;
         //   StrSearch = MySecondContent.FindControl("StrSearch") as System.Web.UI.HtmlControls.HtmlInputText;
        }


        if (SqlliteList != null) {

            //加入欄位Start
            SqlliteList.AddCol("類型代碼", "sqltype", "CENTER");
            SqlliteList.AddCol("檔案類型", "description", "LEFT");
            SqlliteList.AddCol("上傳檔名", "fileUploadOldName", "LEFT");
            //  SqlliteList.AddCol("檔案索引", "file_index", "CENTER");
            SqlliteList.AddCol("說明", "versionDESC", "CENTER");
            SqlliteList.AddCol("更新時間(時間戳記)", "datetime", "CENTER");
            //加入欄位End

            //設定Key值欄位
            SqlliteList.DataKeyNames = "version,fileUploadNewName,fileUploadOldName"; //Key以,隔開

            //設定是否顯示CheckBox(預設是true);
            //SqlliteList.IsUseCheckBox = false;

            //設定SQL
            strSQL = "SELECT * FROM ( "
                    + "SELECT tblA.[version] "
                //+ ", RIGHT('000000000' + CONVERT(varchar, tblA.[version]), 10) AS versionDESC "
                        + ", tblA.version_no AS versionDESC "
                        + ", tblA.sqltype "
                        + ", CAST(tblA.sqltype AS VARCHAR(10)) + '-' + tblB.[description] AS  description"
                        + ", tblA.fileUploadOldName "
                //+ ", tblA.file_index "
                        + ", tblA.fileUploadNewName "
                        + ", CONVERT(VARCHAR(24),[datetime],21) AS [datetime] "
                //+ ", CONVERT(varchar, tblA.[datetime], 111) + ' ' + CONVERT(varchar, tblA.[datetime], 108) AS [datetime] "
                    + "FROM userUploadLog AS tblA "
                    + "INNER JOIN SQLITE_CODE AS tblB ON tblA.sqltype = tblB.sqltype "
                + ") AS tblResult ";
            if (mySearch != "")
            {
                //土法煉鋼的全文檢索 = = .
                strSQL += "WHERE tblResult.versionDESC LIKE '%'+@versionDESC+'%' "
                    + "OR tblResult.[description] LIKE '%'+@description+'%' "
                    + "OR tblResult.fileUploadOldName LIKE '%'+@fileUploadOldName+'%' "
                    + "OR tblResult.[datetime] LIKE '%'+@datetime+'%' ";

                SqlliteList.putQueryParameter("versionDESC", mySearch);
                SqlliteList.putQueryParameter("description", mySearch);
                SqlliteList.putQueryParameter("fileUploadOldName", mySearch);
                SqlliteList.putQueryParameter("datetime", mySearch);

            }

            strSQL += "ORDER BY tblResult.[sqltype] ,versionDESC";


            //取得SQL;
            SqlliteList.SelectString = strSQL;
            SqlliteList.prepareStatement();

            //設定每筆資料按下去的Javascript function
            SqlliteList.OnClickExecFunc = "DoEdt()";

            //設定每頁筆數
            SqlliteList.PageSize = 26;

            //接來自Request的排序欄位、排序方向、目前頁數
            ListViewSortKey = Request.Params["ListViewSortKey"];
            ListViewSortDirection = Request.Params["ListViewSortDirection"];
            PageNo = Request.Params["PageNo"];

            //設定排序欄位及方向
            if (!string.IsNullOrEmpty(ListViewSortKey) && !string.IsNullOrEmpty(ListViewSortDirection))
            {
                SqlliteList.ListViewSortKey = ListViewSortKey;
                SqlliteList.ListViewSortDirection = (SortDirection)Enum.Parse(typeof(SortDirection), ListViewSortDirection);
            }

            //設定目前頁數
            if (!string.IsNullOrEmpty(PageNo))
                SqlliteList.PageNo = int.Parse(PageNo);
        }
       

    }


    protected void Page_Load(object sender, EventArgs e)
    {
        
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
            ListViewSortKey = SqlliteList.ListViewSortKey;
            ListViewSortDirection = SqlliteList.ListViewSortDirection.ToString();
            PageNo = SqlliteList.PageNo.ToString();
        }
    }



    /// <summary>
    /// 刪除檔案記錄(AJAX)
    /// </summary>
    /// <param name="versionList">帳號清單(version1^^version2^^version3...)</param>
    /// <returns>nRet 0:刪除成功 -1, -2:失敗</returns>
    [System.Web.Services.WebMethod]
    public static CReturnData Delete_Sqllite(string versionList)
    {
        CReturnData myData = new CReturnData();
        Database db = new Database();
        DataTable dt = new DataTable();

        /*產生刪除 sqllite檔案上傳記錄表 的SQL*/
        string StrSQL = "/*sqllite檔案上傳記錄表*/ " +
            "DELETE FROM userUploadLog WHERE CONVERT(varchar, [version]) + '##' + fileUploadNewName + '##' + fileUploadOldName IN ( " +
                "SELECT [Value] FROM dbo.UTILfn_Split('" + versionList + "', '^^') AS tbl_version " +
            ") ";

        /*連線DB*/
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;
        if (myData.nRet == 0)
        {
            /*寫入DB*/
            db.BeginTranscation();
            db.AddDmsSqlCmd(StrSQL);
            db.CommitTranscation();

            myData.nRet = db.nRet;
            myData.outMsg = db.outMsg;
        }
        db.DBDisconnect();
        return myData;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    public byte [] StreamFile(string filename)
    {   
        FileStream fs = new FileStream(filename, FileMode.Open,FileAccess.Read);   

        // Create a byte array of file stream length   
        byte[] ImageData = new byte[fs.Length];   

        //Read block of bytes from stream into the byte array  
        fs.Read(ImageData,0,System.Convert.ToInt32(fs.Length));   

        //Close the File Stream  
        fs.Close();

        //return the byte data
        return ImageData; 
        
    }



    /// <summary>
    /// 新增檔案記錄(AJAX)
    /// </summary>
    /// <param name="FileName">檔案名稱</param>
    /// <param name="OldFileName">原本的檔案名稱</param>
    /// <param name="sqltype">檔案類型</param>
    /// <returns>nRet 0:新增成功 -1, -2:失敗</returns>
    [System.Web.Services.WebMethod]
    public static CReturnData Add_Sqllite(string FileName, string OldFileName, string sqltype)
    {
        /* 20111005-modify  寫入動作改由 singleFileUpload.aspx.cs handle */
        CReturnData myData = new CReturnData();
        myData.nRet = 0;
        return myData;
    }


}