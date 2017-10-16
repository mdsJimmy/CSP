using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MDS.Database;
using System.Text;

public partial class DMSMainManage_UserInfo_UserInfo_add : BasePage
{
    static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected string TargerGroupID = "";
    protected string PageNo = "0";
    protected string mySearch = "";

    protected string myGroupID = "";
 
    protected string cRoleID = "";    
    protected void Page_Load(object sender, EventArgs e)
    {
        string myUserID = "";
        /*取得使用者GroupID*/
        myGroupID = Session["ParentGroupID"].ToString(); 
        /*取得使用者UserID*/
        myUserID = Session["UserID"].ToString();
        
        /*接收Request*/
        if (!string.IsNullOrEmpty(Request.QueryString["TargerGroupID"]))
            TargerGroupID = Request.QueryString["TargerGroupID"];
        else
            TargerGroupID = myGroupID;
        /*接收Request*/
        if (!string.IsNullOrEmpty(Request.QueryString["PageNo"]))
            PageNo = Request.QueryString["PageNo"];
        /*接收Request*/
        if (!string.IsNullOrEmpty(Request.QueryString["StrSearch"]))
            mySearch = Request.QueryString["StrSearch"];

       

        /*取得並產生權限等級選項*/
        GetcRoleID(myUserID);

        /*判斷有無語音系統*/
      //  if (CheckSysAttribute("System", "VoiceSystem") == "0")
          //  tr_cCallID.Style.Add(HtmlTextWriterStyle.Display, "none");

        /*使用WebService的function來取得SiteMapAddress*/
        DMSWebService ws = new DMSWebService();
        string SiteMapAddress = ws.GET_SiteMapAddress(TargerGroupID);
        /*有搜尋條件則隱藏所在位置*/
        if (mySearch == "")
        {
            
            Label_Address.Text = SiteMapAddress;

        }
        else
        {
            Label_Address.Text = "&nbsp;";
        }
    }


    /// <summary>
    /// 取得並產生權限等級選項;
    /// </summary>
    /// <param name="myUserID">操作人員AccountID</param>
    /// <returns></returns>
    private void GetcRoleID(string myUserID)
    {
        Database db = new Database();
        DataTable dt = new DataTable();
        
        int nRet = -1;

        /*Get cRoleID START*/
        string StrSQL = "SELECT tblA.DMSRoleID,  tblA.DMSRoleName " +
            "FROM DMSRole AS tblA " +
            "WHERE tblA.DMSRoleID IN ( " +
                "SELECT DMSRoleManage.DMSRoleIDManaged " +
                "FROM DMSRoleManage " +
                "INNER JOIN SecurityUserAccount ON DMSRoleManage.DMSRoleID = SecurityUserAccount.cRoleID " +
                "WHERE SecurityUserAccount.AccountID =@myUserID " +
            ") ";
         StringBuilder options = new StringBuilder("");
        /*連線DB*/
        nRet = db.DBConnect();
        if (nRet == 0)
        {

            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(StrSQL, db.getOcnn());
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@myUserID", myUserID));
            nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);

            if (nRet == 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    options.Append(String.Format("<option value='{0}'>{1}</option>", dt.Rows[i]["DMSRoleID"].ToString(), dt.Rows[i]["DMSRoleName"].ToString()));
                   // cRoleID.Items.Add(new ListItem(dt.Rows[i]["DMSRoleName"].ToString(), dt.Rows[i]["DMSRoleID"].ToString()));
                }
            }
            db.DBDisconnect();
        }

        /*有錯誤則跳出警示視窗*/
        if (nRet != 0)
            MessageBox(nRet.ToString());
        cRoleID = options.ToString();
        /*Get cRoleID END*/
    }


    /// <summary>
    /// 檢核使用者輸入的群組是否存在
    /// </summary>
    /// <param name="validateId"></param>
    /// <param name="validateValue"></param>
    /// <param name="validateError"></param>
    /// <param name="ParentGroupID"></param>
    /// <param name="GroupID"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string CheckGroupID(string validateId, string validateValue, string validateError, string ParentGroupID, string GroupID)
    {
        int nRet = -1;
        Database db = new Database();
        DataTable dt = new DataTable();

        string strSQL = "";

        strSQL = "SELECT tblA.GroupID, GroupName " +
                "FROM SecurityGroup AS tblA " +
                "INNER JOIN dbo.fn_GetGroupTree(@ParentGroupID) AS tblT ON tblA.GroupID = tblT.GroupID " +
                "WHERE tblA.GroupID = @GroupID";

        /*連線DB*/
        nRet = db.DBConnect();
        if (nRet == 0)
        {

            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(strSQL, db.getOcnn());
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ParentGroupID", ParentGroupID));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GroupID", GroupID));
            nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);
 
            if (nRet == 0)
            {
                if (dt.Rows.Count > 0)
                    /*此GroupID存在*/
                    nRet = 0;
                else
                    /*此GroupID不存在*/
                    nRet = -3;
            }
            db.DBDisconnect();
        }

        //固定程式碼, 回傳驗證結果true/false;
        //return (nRet == 0) ? FormValidateResult(validateId, validateError, true) : FormValidateResult(validateId, validateError, false);
        return (nRet == 0) .ToString();
    }


    /// <summary>
    /// 確認AccountID是否重覆(AJAX)
    /// </summary>
    /// <param name="validateId"></param>
    /// <param name="validateValue">AccountID(使用者帳號)</param>
    /// <param name="validateError"></param>
    /// <returns>nRet true:帳號未重覆 false:(-1, -2:失敗 -3:帳號重覆)</returns>
    [System.Web.Services.WebMethod]
    public static string CheckAccountIDProcess(string validateId, string validateValue, string validateError)
    {
        int nRet = -1;
        Database db = new Database();
        DataTable dt = new DataTable();
        string AccountID = validateValue;

        /*產生帳號check的SQL*/
        string StrSQL = "SELECT COUNT(*) AS CheckAccountID FROM SecurityUserAccount WHERE AccountID = @AccountID";

        /*連線DB*/
        nRet = db.DBConnect();
        if (nRet == 0)
        {

            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(StrSQL, db.getOcnn());
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccountID", AccountID));
            nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);

            if (nRet == 0)
            {
                if (int.Parse(dt.Rows[0]["CheckAccountID"].ToString()) == 0)
                    /*此AccountID未重覆*/
                    nRet = 0;
                else
                    /*此AccountID重覆*/
                    nRet = - 3;
            }
            db.DBDisconnect();
        }

        //固定程式碼, 回傳驗證結果true/false;
        return (nRet == 0).ToString();

    }


    /// <summary>
    /// 確認cCallID是否重覆(formValidator ajax)
    /// </summary>
    /// <param name="validateId"></param>
    /// <param name="validateValue">CallID(語音查詢帳號)</param>
    /// <param name="validateError"></param>
    /// <returns>nRet true:語音查詢帳號未重覆 false:驗證失敗(-1, -2:失敗 -3:語音查詢帳號重覆)</returns>
    [System.Web.Services.WebMethod]
    public static string CheckcCallIDProcess(string validateId, string validateValue, string validateError)
    {
        int nRet = -1;
        Database db = new Database();
        DataTable dt = new DataTable();
        string cCallID = validateValue;

        /*產生語音查詢帳號check的SQL*/
        string StrSQL = "SELECT COUNT(*) AS CheckcCallID FROM SecurityUserAccount WHERE cCallID = @cCallID ";

        /*連線DB*/
        nRet = db.DBConnect();
        if (nRet == 0)
        {

            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(StrSQL, db.getOcnn());
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@cCallID", cCallID));
            nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);
 
            if (nRet == 0)
            {
                if (int.Parse(dt.Rows[0]["CheckcCallID"].ToString()) == 0)
                    /*此cCallID未重覆*/
                    nRet = 0;
                else
                    /*此cCallID重覆*/
                    nRet = -3;
            }
            db.DBDisconnect();
        }

        //固定程式碼, 回傳驗證結果true/false;
        return (nRet == 0).ToString();
    }


    /// <summary>
    /// 新增程序(AJAX)
    /// </summary>
    /// <param name="GroupID">*所屬群組</param>
    /// <param name="AccountID">*使用者帳號</param>
    /// <param name="Name">*姓名</param>
    /// <param name="Description">職稱</param>
    /// <param name="Startup">啟用帳號</param>
    /// <param name="PWType">密碼類別(0:永久有效 1:定期變更)</param>
    /// <param name="cRoleID">權限等級</param>
    /// <param name="HiddenUserDefine">權限微調設定(0:未設定權限微調 1:有設定)</param>
    /// <param name="HiddenFunctionList">自訂FunctionID List</param>
    /// <param name="HiddenActionList">自訂ActionID List</param>
    /// <param name="cCallID">語音查詢帳號</param>
    /// <param name="GroupDESC">所屬群組顯示字串</param>
    /// <param name="PWTypeDESC">密碼類別顯示字串</param>
    /// <param name="cRoleDESC">權限等級顯示字串</param>
    /// <param name="myAccountName">操作人員姓名</param>
    /// <param name="myAccountID">操作人員帳號</param>
    /// <returns>nRet 0:成功 -1, -2:失敗</returns>
    [System.Web.Services.WebMethod]
    public static CReturnData AddProcess(
        string GroupID, string AccountID,string rePwd
        , string Name, string Description
        , string Startup, string PWType
        , string cRoleID, string HiddenUserDefine
        , string HiddenFunctionList, string HiddenActionList, string cCallID
        , string GroupDESC, string PWTypeDESC, string cRoleDESC, string myAccountName, string myAccountID)
    {
        CReturnData myData = new CReturnData();

        Database db = new Database();
        DataTable dt = new DataTable();

        string myGUID = System.Guid.NewGuid().ToString();

        string ModuleDesc = ParseWording("B0136");
        string FunctionDesc = ParseWording("B0137");
        string ActionDesc = ParseWording("A0001");
        string StrStartup = (Startup == "1") ? "V" : "";

        /*連線DB*/
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;
        if (myData.nRet == 0)
        {
            string StrSQL = "/*使用者資料表*/ " +
                "INSERT INTO SecurityUserAccount(GroupID, AccountID, Name, Description, Password, Startup, PWLastUpdateTime, PWType, CreateTime, dModifyTime, iFailTimes, dLockTime, cRoleID, AD_CheckFlag, cCallID, cPWD) " +
                    "SELECT '" + GroupID + "', '" + MDS.Utility.NUtility.trimBad(AccountID)+ "', N'" + Name.Replace("'", "''") + "', N'" + Description.Replace("'", "''") + "', CONVERT(varbinary, '" +rePwd + "'), '" + Startup + "', NULL, " + PWType + ", GETDATE(), GETDATE(), 0, NULL, " + cRoleID + ", 0, '" + cCallID + "', '" + cCallID + "' " +
                "/*連離線狀態表*/ " +
                "INSERT INTO LinetStatus(DateTime, AccountID, Type, Status, TxDateTime, TransDateTime) " +
                    "SELECT GETDATE(), '" + AccountID + "', 1, 2, null, null " +
                "/*個人密碼紀錄表*/ " +
                "INSERT INTO SecurityUserPwd(AccountID, bPassword, iOrder, kind, Ldate ) " +
                    "SELECT '" + AccountID + "', CONVERT(varbinary, '" + AccountID + System.DateTime.Now.ToString("yyyymmdd") + "'), 1 , 0, GETDATE() " +
                "/*使用者進行權限微調*/ " +
                "IF (" + HiddenUserDefine + " = 1) BEGIN " +
                    "INSERT INTO SecurityUserAccount_FunctionRole(AccountID, SysModID, SysFuncID) " +
                        "SELECT '" + AccountID + "', SystemFunction.SysModID, [Value] " +
                        "FROM dbo.UTILfn_Split('" + HiddenFunctionList + "', '||') AS tblFunction " +
                        "INNER JOIN SystemFunction ON tblFunction.Value = SystemFunction.SysFuncID " +
                    "INSERT INTO SecurityUserAccount_ActionRole(AccountID, SysModID, SysFuncID, SysActionID) " +
                        "SELECT '" + AccountID + "', SystemAction.SysModID, SystemAction.SysFuncID, [Value] " +
                        "FROM dbo.UTILfn_Split('" + HiddenActionList + "', '||') AS tblAction " +
                        "INNER JOIN SystemAction ON tblAction.Value = SystemAction.SysActionID " +
                "END " +
                "/*預設權限*/ " +
                "ELSE BEGIN " +
                    "INSERT INTO SecurityUserAccount_FunctionRole(AccountID, SysModID, SysFuncID) " +
                        "SELECT '" + AccountID + "', SysModID, SysFuncID " +
                        "FROM DMSRoleFunction " +
                        "WHERE DMSRoleID = " + cRoleID + " " +
                    "INSERT INTO SecurityUserAccount_ActionRole(AccountID, SysModID, SysFuncID, SysActionID) " +
                        "SELECT '" + AccountID + "', SysModID, SysFuncID, SysActionID " +
                        "FROM DMSRoleAction " +
                        "WHERE DMSRoleID = " + cRoleID + " " +
                "END " +

                "/*操作紀錄-新增畫面*/" +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0012") + "', '', N'" + GroupDESC + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0006") + "', '', N'" + AccountID + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0007") + "', '', N'" + Name.Replace("'", "''") + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0008") + "', '', N'" + Description.Replace("'", "''") + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0011") + "', '', N'" + StrStartup + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0013") + "', '', N'" + PWTypeDESC + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0010") + "', '', N'" + cRoleDESC + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() " +
                "/*操作紀錄-新增畫面-權限微調*/" +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "'" +
                        ", N'" + ModuleDesc + "'" +
                        ", N'" + FunctionDesc + "'" +
                        ", N'" + ActionDesc + "'" +
                        ", N'" + ParseWording("B0015") + "' + '_' + SystemModule.ModuleDesc + '_' + SystemFunction.FunctionDesc " +
                        ", ''" +
                        ", (CASE " +
                            "WHEN SecurityUserAccount_FunctionRole.SysFuncID IS NULL THEN '' " +
                            "ELSE 'V' " +
                        "END) AS FunctionRoleCheck " +
                        ", N'" + myAccountName + "'" +
                        ", N'" + myAccountID + "'" +
                        ", GETDATE() " +
                    "FROM SystemModule " +
                    "INNER JOIN SystemFunction ON SystemModule.SysModID = SystemFunction.SysModID " +
                    "INNER JOIN DMSRoleFunction ON SystemFunction.SysFuncID = DMSRoleFunction.SysFuncID AND DMSRoleFunction.DMSRoleID = " + cRoleID + " " +
                    "LEFT JOIN SecurityUserAccount_FunctionRole ON DMSRoleFunction.SysFuncID = SecurityUserAccount_FunctionRole.SysFuncID AND SecurityUserAccount_FunctionRole.AccountID = '" + AccountID + "' " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "'" +
                        ", N'" + ModuleDesc + "'" +
                        ", N'" + FunctionDesc + "'" +
                        ", N'" + ActionDesc + "'" +
                        ", N'" + ParseWording("B0015") + "' + '_' + SystemModule.ModuleDesc + '_' + SystemFunction.FunctionDesc + '_' + SystemAction.ActionDesc " +
                        ", ''" + 
                        ", (CASE " +
                            "WHEN SecurityUserAccount_ActionRole.SysActionID IS NULL THEN '' " +
                            "ELSE 'V' " +
                        "END) AS ActionRoleCheck " +
                        ", N'" + myAccountName + "'" +
                        ", N'" + myAccountID + "'" +
                        ", GETDATE() " +
                    "FROM SystemModule " +
                    "INNER JOIN SystemFunction ON SystemModule.SysModID = SystemFunction.SysModID " +
                    "INNER JOIN SystemAction ON SystemFunction.SysFuncID = SystemAction.SysFuncID " +
                    "INNER JOIN DMSRoleAction ON SystemAction.SysActionID = DMSRoleAction.SysActionID AND DMSRoleAction.DMSRoleID = " + cRoleID + " " +
                    "LEFT JOIN SecurityUserAccount_ActionRole ON DMSRoleAction.SysActionID = SecurityUserAccount_ActionRole.SysActionID AND SecurityUserAccount_ActionRole.AccountID = '" + AccountID + "' ";
            
            /*判斷有無語音系統, 若有才寫入操作紀錄*/
            if (CheckSysAttribute("System", "VoiceSystem") == "1")
                StrSQL += "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0014") + "', '', N'" + cCallID + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() ";
        
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
