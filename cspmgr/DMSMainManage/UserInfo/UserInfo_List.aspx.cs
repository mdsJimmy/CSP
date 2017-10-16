using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using MDS.Database;

public partial class Class_UserInfo_List : BasePage
{
    string myGroupID = "";
    protected string TargerGroupID = "";

    Database db = new Database();
    DataTable dt = new DataTable();

    protected string mySearch = "";

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
        myGroupID = Session["ParentGroupID"].ToString();

        /*接收Request*/
        if (!string.IsNullOrEmpty(Request.QueryString["TargerGroupID"]))
            TargerGroupID = Request.QueryString["TargerGroupID"];
        else
            TargerGroupID = myGroupID;
        if (!string.IsNullOrEmpty(Request.QueryString["StrSearch"]))
            mySearch = Request.QueryString["StrSearch"];
        
        if (UserList == null)
        {
            ContentPlaceHolder MySecondContent = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
            UserList = MySecondContent.FindControl("UserList") as ASP.dmscontrol_olistview_ascx;
            StrSearch = MySecondContent.FindControl("StrSearch") as System.Web.UI.HtmlControls.HtmlInputText;
        }
        //StrSearch接Request;
        if (StrSearch!=null)
        {
          StrSearch.Value = mySearch;
        }


        if (UserList != null)
        {
            //加入欄位Start
            UserList.AddCol(ParseWording("B0005"), "GroupDesc", "LEFT");
            UserList.AddCol(ParseWording("B0006"), "AccountID", "LEFT");
            UserList.AddCol(ParseWording("B0007"), "Name", "LEFT");
            UserList.AddCol(ParseWording("B0008"), "Description", "LEFT");
            UserList.AddCol(ParseWording("B0009"), "PWLastUpdateTime", "LEFT");
            UserList.AddCol(ParseWording("B0010"), "DMSRoleName", "LEFT");
            UserList.AddCol(ParseWording("B0011"), "IsStartup", "CENTER", "", "True::" + ParseWording("B0070") + "^^False::" + ParseWording("B0071"));
            UserList.AddCol("使用狀況", "situation", "CENTER");
            //加入欄位End

            //設定Key值欄位
            UserList.DataKeyNames = "AccountID"; //Key以,隔開

            //設定是否顯示CheckBox(預設是true);
            if ((CheckUserRight("2F8CFA99-A316-4688-B1A7-EE01B96B2D7A") == false)
                && (CheckUserRight("7AE558CE-BEDE-4123-A5E7-264A623E095A") == false)
                && (CheckUserRight("A8CB6021-BCE7-4DBC-ADB6-A666419B58A2") == false)
                && (CheckUserRight("49E15601-D0F6-4D03-A7D8-C0140FC6C9F3") == false))
            {
                UserList.IsUseCheckBox = false;
            }

            //設定SQL
            strSQL = @"SELECT SecurityGroup.GroupID + ' ' + SecurityGroup.GroupName AS GroupDesc
                    ,tblA.AccountID
                    ,tblA.Name
                    ,tblA.[Description]
                    ,tblA.PWLastUpdateTime
					,(case when tblA.dLockTime is null then '正常' else '鎖定' end) as situation
                    ,DMSRole.DMSRoleName
                    ,tblA.Startup AS IsStartup
                FROM SecurityUserAccount AS tblA
                INNER JOIN SecurityGroup ON tblA.GroupID = SecurityGroup.GroupID
                INNER JOIN DMSRole ON tblA.cRoleID = DMSRole.DMSRoleID "


                ;
            if (mySearch == "")
            {
                strSQL += "WHERE tblA.GroupID =@TargerGroupID";
                UserList.putQueryParameter("TargerGroupID", TargerGroupID);
            }
            else
            {
                strSQL += " INNER JOIN dbo.fn_GetGroupTree(@ParentGroupID) AS tblT ON tblA.GroupID = tblT.GroupID "
                    + " WHERE tblA.AccountID LIKE '%'+@mySearch_1+'%' OR tblA.Name LIKE '%'+@mySearch_2+'%' ";

                UserList.putQueryParameter("ParentGroupID", TargerGroupID);
                UserList.putQueryParameter("mySearch_1", mySearch.Replace("'", "''"));
                UserList.putQueryParameter("mySearch_2", mySearch.Replace("'", "''"));

            }
            strSQL += " ORDER BY tblA.AccountID ";

            //取得SQL;
            UserList.SelectString = strSQL;
            UserList.prepareStatement();

            //設定每筆資料按下去的Javascript function
            UserList.OnClickExecFunc = "DoEdt()";

            //設定每頁筆數
            UserList.PageSize = 10;

            //UserList.IsUseBorder = true;

            //接來自Request的排序欄位、排序方向、目前頁數
            ListViewSortKey = Request.Params["ListViewSortKey"];
            ListViewSortDirection = Request.Params["ListViewSortDirection"];
            PageNo = Request.Params["PageNo"];

            //設定排序欄位及方向
            if (!string.IsNullOrEmpty(ListViewSortKey) && !string.IsNullOrEmpty(ListViewSortDirection))
            {
                UserList.ListViewSortKey = ListViewSortKey;
                UserList.ListViewSortDirection = (SortDirection)Enum.Parse(typeof(SortDirection), ListViewSortDirection);
            }

            //設定目前頁數
            if (!string.IsNullOrEmpty(PageNo))
                UserList.PageNo = int.Parse(PageNo);
            
        }

       

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        /*Check DMSRole START*/
        if (CheckUserRight("26399114-A01D-40C9-9070-8C12799CFA68") == false)
        {
            btnAdd.Visible = false;
        }
        if (CheckUserRight("2F8CFA99-A316-4688-B1A7-EE01B96B2D7A") == false)
        {
            btnDel.Visible = false;
        }
        if (CheckUserRight("7AE558CE-BEDE-4123-A5E7-264A623E095A") == false)
        {
            btnStartup.Visible = false;
        }
        if (CheckUserRight("A8CB6021-BCE7-4DBC-ADB6-A666419B58A2") == false)
        {
            btnDeny.Visible = false;
        }
        if (CheckUserRight("49E15601-D0F6-4D03-A7D8-C0140FC6C9F3") == false)
        {
            btnPwReset.Visible = false;
            btnUnlock.Visible = false;
        }
        
        /*Check DMSRole END*/

        //if (!Page.IsPostBack)
        //{
            //MessageBox(Page.IsPostBack.ToString());

            //使用WebService的function來取得SiteMapAddress;
            DMSWebService ws = new DMSWebService();
            string SiteMapAddress = ws.GET_SiteMapAddress(TargerGroupID);

            //有搜尋條件則隱藏所在位置;
            if (mySearch == "")
            {
                SiteMapAddress = ParseWording("A0014") + " " + SiteMapAddress;
                Label_Address.Text = SiteMapAddress;
                
            }
            else
            {
                Label_Address.Text = "&nbsp;";
            }

        //}
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
            ListViewSortKey = UserList.ListViewSortKey;
            ListViewSortDirection = UserList.ListViewSortDirection.ToString();
            PageNo = UserList.PageNo.ToString();
        }
    }


    /// <summary>
    /// 刪除帳號(AJAX)
    /// </summary>
    /// <param name="AccountIDList">帳號清單(AccountID1^^AccountID2^^AccountID3...)</param>
    /// <param name="myAccountName">操作人員姓名</param>
    /// <param name="myAccountID">操作人員帳號</param>
    /// <returns>nRet 0:刪除成功 -1, -2:失敗</returns>
    [System.Web.Services.WebMethod]
    public static CReturnData Delete_AccountID(string AccountIDList, string myAccountName, string myAccountID)
    {
        CReturnData myData = new CReturnData();
        Database db = new Database();
        DataTable dt = new DataTable();

        string ModuleDesc = ParseWording("B0136");
        string FunctionDesc = ParseWording("B0137");
        string ActionDesc = ParseWording("A0003");
        
        /*產生刪除帳號的SQL*/
        string StrSQL = "DECLARE @tmpAccountID TABLE( " +
                "myGUID varchar(50), " +
                "AccountID varchar(20), " +
                "Name nvarchar(50) " +
            ") " +
            "INSERT INTO @tmpAccountID(myGUID, AccountID, Name) " +
                "SELECT newid(), tblA.[Value], SecurityUserAccount.Name " +
                "FROM dbo.UTILfn_Split('" + AccountIDList + "', '^^') AS tblA " +
                "INNER JOIN SecurityUserAccount ON tblA.[Value] = SecurityUserAccount.AccountID " +
            "/*使用者資料表*/ " +
            "DELETE FROM SecurityUserAccount WHERE AccountID IN (SELECT AccountID FROM @tmpAccountID) " +
            "/*連離線狀態表*/ " +
            "DELETE FROM LinetStatus WHERE AccountID IN (SELECT AccountID FROM @tmpAccountID) " +
            "/*個人密碼紀錄表*/ " +
            "DELETE FROM SecurityUserPwd WHERE AccountID IN (SELECT AccountID FROM @tmpAccountID) " +
            "/*使用者權限*/ " +
            "DELETE FROM SecurityUserAccount_FunctionRole WHERE AccountID IN (SELECT AccountID FROM @tmpAccountID) " +
            "DELETE FROM SecurityUserAccount_ActionRole WHERE AccountID IN (SELECT AccountID FROM @tmpAccountID) " +

            "/*操作紀錄-刪除*/" +
            "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                "SELECT myGUID, N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0006") + "', AccountID, '', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM @tmpAccountID " +
            "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                "SELECT myGUID, N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0007") + "', Name, '', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM @tmpAccountID ";
            

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

            db.DBDisconnect();
        }
        return myData;
    }


    /// <summary>
    /// 啟用帳號(AJAX)
    /// </summary>
    /// <param name="AccountIDList">帳號清單(AccountID1^^AccountID2^^AccountID3...)</param>
    /// <param name="myAccountName">操作人員姓名</param>
    /// <param name="myAccountID">操作人員帳號</param>
    /// <returns>nRet 0:啟用成功 -1, -2:失敗</returns>
    [System.Web.Services.WebMethod]
    public static CReturnData Startup_AccountID(string AccountIDList, string myAccountName, string myAccountID)
    {
        CReturnData myData = new CReturnData();
        Database db = new Database();
        DataTable dt = new DataTable();

        string ModuleDesc = ParseWording("B0136");
        string FunctionDesc = ParseWording("B0137");
        string ActionDesc = ParseWording("B0003");
        
        /*產生啟用帳號的SQL*/
        string StrSQL = "DECLARE @tmpAccountID TABLE( " +
                "myGUID varchar(50), " +
                "AccountID varchar(20), " +
                "Name nvarchar(50) " +
            ") " +
            "INSERT INTO @tmpAccountID(myGUID, AccountID, Name) " +
                "SELECT newid(), tblA.[Value], SecurityUserAccount.Name " +
                "FROM dbo.UTILfn_Split('" + AccountIDList + "', '^^') AS tblA " +
                "INNER JOIN SecurityUserAccount ON tblA.[Value] = SecurityUserAccount.AccountID " + 
                "WHERE SecurityUserAccount.Startup = 0 " +
            "/*更新使用者資料表*/ " +
            "UPDATE SecurityUserAccount " +
            "SET Startup = 1 " +
            "WHERE AccountID IN (SELECT AccountID FROM @tmpAccountID) " +

            "/*操作紀錄-啟用帳號*/" +
            "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                "SELECT myGUID, N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0006") + "', AccountID, '', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM @tmpAccountID " +
            "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                "SELECT myGUID, N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0007") + "', Name, '', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM @tmpAccountID ";
            

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

            db.DBDisconnect();
        }
        return myData;
    }


    /// <summary>
    /// 停用帳號(AJAX)
    /// </summary>
    /// <param name="AccountIDList">帳號清單(AccountID1^^AccountID2^^AccountID3...)</param>
    /// <param name="myAccountName">操作人員姓名</param>
    /// <param name="myAccountID">操作人員帳號</param>
    /// <returns>nRet 0:停用成功 -1, -2:失敗</returns>
    [System.Web.Services.WebMethod]
    public static CReturnData Deny_AccountID(string AccountIDList, string myAccountName, string myAccountID)
    {
        CReturnData myData = new CReturnData();
        Database db = new Database();
        DataTable dt = new DataTable();

        string ModuleDesc = ParseWording("B0136");
        string FunctionDesc = ParseWording("B0137");
        string ActionDesc = ParseWording("B0001");
        
        /*產生停用帳號的SQL*/
        string StrSQL = "DECLARE @tmpAccountID TABLE( " +
                "myGUID varchar(50), " +
                "AccountID varchar(20), " +
                "Name nvarchar(50) " +
            ") " +
            "INSERT INTO @tmpAccountID(myGUID, AccountID, Name) " +
                "SELECT newid(), tblA.[Value], SecurityUserAccount.Name " +
                "FROM dbo.UTILfn_Split('" + AccountIDList + "', '^^') AS tblA " +
                "INNER JOIN SecurityUserAccount ON tblA.[Value] = SecurityUserAccount.AccountID " + 
                "WHERE SecurityUserAccount.Startup = 1 " +
            "/*更新使用者資料表*/ " +
            "UPDATE SecurityUserAccount " +
            "SET Startup = 0 " +
            "WHERE AccountID IN (SELECT AccountID FROM @tmpAccountID) " +

            "/*操作紀錄-停用帳號*/" +
            "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                "SELECT myGUID, N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0006") + "', AccountID, '', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM @tmpAccountID " +
            "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                "SELECT myGUID, N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0007") + "', Name, '', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM @tmpAccountID ";
            

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

            db.DBDisconnect();
        }
        return myData;
    }


    /// <summary>
    /// 帳號密碼重置(AJAX)
    /// </summary>
    /// <param name="AccountIDList">帳號清單(AccountID1^^AccountID2^^AccountID3...)</param>
    /// <param name="myAccountName">操作人員姓名</param>
    /// <param name="myAccountID">操作人員帳號</param>
    /// <returns>nRet 0:密碼重置成功 -1, -2:失敗</returns>
    [System.Web.Services.WebMethod]
    public static CReturnData PwReset_AccountID(string AccountIDList, string myAccountName, string myAccountID,string repwd)
    {
        CReturnData myData = new CReturnData();
        Database db = new Database();
        DataTable dt = new DataTable();

        string ModuleDesc = ParseWording("B0136");
        string FunctionDesc = ParseWording("B0137");
        string ActionDesc = ParseWording("B0002");

        /*產生帳號密碼重置的SQL*/
        string StrSQL = "DECLARE @tmpAccountID TABLE( " +
                "myGUID varchar(50), " +
                "AccountID varchar(20), " +
                "Name nvarchar(50) " +
            ") " +
            "INSERT INTO @tmpAccountID(myGUID, AccountID, Name) " +
                "SELECT newid(), tblA.[Value], SecurityUserAccount.Name " +
                "FROM dbo.UTILfn_Split('" + AccountIDList + "', '^^') AS tblA " +
                "INNER JOIN SecurityUserAccount ON tblA.[Value] = SecurityUserAccount.AccountID " +
            "/*更新使用者資料表*/ " +
            "UPDATE SecurityUserAccount " +
            "set [Password] = CONVERT(varbinary,'"+repwd+"') " +
            ", PWLastUpdateTime = NULL " +
            "WHERE AccountID IN (SELECT AccountID FROM @tmpAccountID) " +

            "/*操作紀錄-密碼重置*/" +
            "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                "SELECT myGUID, N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0006") + "', AccountID, '', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM @tmpAccountID " +
            "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                "SELECT myGUID, N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0007") + "', Name, '', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM @tmpAccountID ";

            

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

            db.DBDisconnect();
        }
        return myData;
    }


    //解除鎖定
    [System.Web.Services.WebMethod]
    public static CReturnData PwUnLock_AccountID(string AccountIDList, string myAccountName, string myAccountID)
    {
        CReturnData myData = new CReturnData();
        Database db = new Database();
        DataTable dt = new DataTable();

        string ModuleDesc = ParseWording("B0136");
        string FunctionDesc = ParseWording("B0137");
        string ActionDesc = "解除鎖定";

        /*產生帳號解除鎖定的SQL*/
        string StrSQL = "DECLARE @tmpAccountID TABLE( " +
                "myGUID varchar(50), " +
                "AccountID varchar(20), " +
                "Name nvarchar(50) " +
            ") " +
            "INSERT INTO @tmpAccountID(myGUID, AccountID, Name) " +
                "SELECT newid(), tblA.[Value], SecurityUserAccount.Name " +
                "FROM dbo.UTILfn_Split('" + AccountIDList + "', '^^') AS tblA " +
                "INNER JOIN SecurityUserAccount ON tblA.[Value] = SecurityUserAccount.AccountID " +
            "/*更新使用者資料表*/ " +
            "UPDATE SecurityUserAccount " +
            "set ifailTimes = 0 " +
            ", dLockTime= NULL " +
            ", PWLastUpdateTime = NULL " +
            " WHERE AccountID IN (SELECT AccountID FROM @tmpAccountID) " +

            "/*操作紀錄-密碼重置*/" +
            "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                 "SELECT myGUID, N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0006") + "', AccountID, '', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM @tmpAccountID " +
            "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                "SELECT myGUID, N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0006") + "', AccountID, '', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM @tmpAccountID ";



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

            db.DBDisconnect();
        }
        return myData;
    }


}
