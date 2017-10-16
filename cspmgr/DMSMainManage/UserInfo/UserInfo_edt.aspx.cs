using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MDS.Database;
using System.Text;

public partial class DMSMainManage_UserInfo_UserInfo_edt : BasePage
{
    protected string TargerGroupID = "";

    protected string PageNo = "0";
    protected string mySearch = "";
     protected string _GroupID = "";
     protected string _GroupInfo = "";
     protected string _AccountID = "";
     protected string _Name = "";
     protected string _Description = "";
     protected string _Startup = "";
     protected string _PWType = "";
     protected string _cRoleID = "";
     protected string _HiddenFunctionList = "";
     protected string _HiddenActionList = "";
     protected string _cCallID = "";
     protected string cRoleID = "";
    
    


    protected void Page_Load(object sender, EventArgs e)
    {
        string myGroupID = "";
        string myUserID = "";
        
        string AccountID = "";

        /*取得使用者GroupID*/
        myGroupID = Session["ParentGroupID"].ToString();
        /*取得使用者UserID*/
        myUserID = Session["UserID"].ToString();
        /*接收Request*/
        if (!string.IsNullOrEmpty(Request.QueryString["AccountID"]))
            AccountID = Request.QueryString["AccountID"];
        /*接收Request*/
        if (!string.IsNullOrEmpty(Request.QueryString["TargerGroupID"]))
            TargerGroupID = Request.QueryString["TargerGroupID"];
        /*接收Request*/
        if (!string.IsNullOrEmpty(Request.QueryString["PageNo"]))
            PageNo = Request.QueryString["PageNo"];
        /*接收Request*/
        if (!string.IsNullOrEmpty(Request.QueryString["StrSearch"]))
            mySearch = Request.QueryString["StrSearch"];
        


        /*取得使用者資料*/
        GetUserData(AccountID);

        /*取得並產生權限等級選項*/
        GetcRoleID(myUserID,this._cRoleID);


        //確認是否有目前登入的使用者是否有修改的權限
        if (CheckUserRight("DF634169-756E-41C2-B0D0-3E5E14207DEB") == false)
        {
            //不顯示儲存和放棄;
            btnSave.Visible = false;
            btnCancel.Visible = false;
        }
        else
        {
            //不顯示返回;
            btnBack.Visible = false;
        }

        /*判斷有無語音系統*/
       // if (CheckSysAttribute("System", "VoiceSystem") == "0")
           // tr_cCallID.Style.Add(HtmlTextWriterStyle.Display, "none");

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
    private void GetcRoleID(string myUserID,string roleId)
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
                "WHERE SecurityUserAccount.AccountID =@AccountID" +
            ") ";
        
        StringBuilder options = new StringBuilder("");

        System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(StrSQL, db.getOcnn());
        SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("AccountID", myUserID));


        /*連線DB*/
        nRet = db.DBConnect();
        
        if (nRet == 0)
        {
            nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);
            if (nRet == 0)
            {
                //下拉選單選項固定在使用者的roleId
                string strSelected = "";
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //選項與使用者roleID相同則為選取狀態
                    if (roleId.Equals(dt.Rows[i]["DMSRoleID"].ToString())) { strSelected = "Selected"; } else { strSelected = ""; }

                    options.Append(String.Format("<option value='{0}'  {2}>{1}</option>", dt.Rows[i]["DMSRoleID"].ToString(), dt.Rows[i]["DMSRoleName"].ToString(), strSelected));
                   // cRoleID.Items.Add(new ListItem(dt.Rows[i]["DMSRoleName"].ToString(), dt.Rows[i]["DMSRoleID"].ToString()));
                }
            }
            db.DBDisconnect();
        }

        /*有錯誤則跳出警示視窗*/
        if (nRet != 0)
            MessageBox(nRet.ToString());

        /*Get cRoleID END*/
        cRoleID = options.ToString();
    }


    /// <summary>
    /// 取得該使用者資料;
    /// </summary>
    /// <param name="AccountID">使用者AccountID</param>
    /// <returns></returns>
    private void GetUserData(string TargetAccountID)
    {
        Database db = new Database();
        DataTable dt = new DataTable();

        int nRet = -1;

        /*Get GetUserData START*/
        string StrSQL = "DECLARE @StrFunction varchar(max) " +
            "DECLARE @StrAction varchar(max) " +
            " SET @StrFunction = '' " +
            " SET @StrAction = '' " +
            " SELECT @StrFunction = @StrFunction + SysFuncID + '||' FROM SecurityUserAccount_FunctionRole WHERE AccountID = @TargetAccountID_1 " +
            " IF LEN(@StrFunction) <> 0 BEGIN " +
	            " SET @StrFunction = LEFT(@StrFunction, LEN(@StrFunction) - 2) " +
            " END " +
            " SELECT @StrAction = @StrAction + SysActionID + '||' FROM SecurityUserAccount_ActionRole WHERE AccountID =@TargetAccountID_2 " +
            " IF LEN(@StrAction) <> 0 BEGIN " +
	            " SET @StrAction = LEFT(@StrAction, LEN(@StrAction) - 2) " +
            " END " +
            " SELECT tblA.GroupID, tblA.GroupID + ' ' + SecurityGroup.GroupName AS GroupInfo, tblA.Name, tblA.[Description], tblA.Startup, ISNULL(tblA.PWType, 0) AS PWType, tblA.cRoleID, ISNULL(tblA.cCallID, '') AS cCallID, @StrFunction AS StrFunction, @StrAction AS StrAction " +
            " FROM SecurityUserAccount AS tblA " +
            " INNER JOIN SecurityGroup ON tblA.GroupID = SecurityGroup.GroupID " +
            " WHERE tblA.AccountID =@TargetAccountID_3 ";

        /*查詢DB*/
        System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(StrSQL, db.getOcnn());
        SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TargetAccountID_1", MDS.Utility.NUtility.checkString(TargetAccountID)));
        SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TargetAccountID_2", MDS.Utility.NUtility.checkString(TargetAccountID)));
        SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TargetAccountID_3", MDS.Utility.NUtility.checkString(TargetAccountID)));


        /*連線DB*/
        nRet = db.DBConnect();
        if (nRet == 0)
        {
            
            nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);


            if (nRet == 0)
            {
                if (dt.Rows.Count == 1)
                {
                    _GroupID = dt.Rows[0]["GroupID"].ToString();
                    _GroupInfo = dt.Rows[0]["GroupInfo"].ToString();
                    _AccountID = TargetAccountID;
                    _Name = dt.Rows[0]["Name"].ToString();
                    _Description = dt.Rows[0]["Description"].ToString();
                    if (dt.Rows[0]["Startup"].ToString() == "False")
                    {
                        _Startup = "";
                    }
                    else
                    {
                        _Startup = " checked='checked' ";
                    }

                    _PWType = dt.Rows[0]["PWType"].ToString();
                    _cRoleID = dt.Rows[0]["cRoleID"].ToString();
                    _HiddenFunctionList = dt.Rows[0]["StrFunction"].ToString();
                    _HiddenActionList = dt.Rows[0]["StrAction"].ToString();
                    _cCallID = dt.Rows[0]["cCallID"].ToString();
                }
                else
                {
                    /*查無此人*/
                    nRet = -3;
                }
            }
            db.DBDisconnect();
        }

        /*有錯誤則跳出警示視窗*/
        if (nRet != 0)
            MessageBox(nRet.ToString());

        /*Get GetUserData END*/
    }


    /// <summary>
    /// 確認cCallID是否重覆(formValidator ajax)
    /// </summary>
    /// <param name="validateId"></param>
    /// <param name="validateValue">CallID(語音查詢帳號)</param>
    /// <param name="validateError"></param>
    /// <param name="AccountID">使用者帳號</param>
    /// <returns>nRet true:語音查詢帳號未重覆 false:驗證失敗(-1, -2:失敗 -3:語音查詢帳號重覆)</returns>
    [System.Web.Services.WebMethod]
    public static string CheckcCallIDProcess(string validateId, string validateValue, string validateError, string AccountID)
    {
        int nRet = -1;
        Database db = new Database();
        DataTable dt = new DataTable();
        
        string cCallID = validateValue;
        
        /*產生語音查詢帳號check的SQL(排除自己)*/
        string StrSQL = "SELECT COUNT(*) AS CheckcCallID FROM SecurityUserAccount WHERE cCallID =@cCallID AND AccountID <> @AccountID";
        
        /*連線DB*/
        nRet = db.DBConnect();
        if (nRet == 0)
        {

            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(StrSQL, db.getOcnn());
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@cCallID", cCallID));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccountID", AccountID));
            nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);

            if (nRet == 0)
            {
                if (int.Parse(dt.Rows[0]["CheckcCallID"].ToString()) == 0)
                    /*此cCallID未重覆*/
                    nRet = 0;
                else
                    /*此cCallID重覆*/
                    nRet =  -3;
            }
            db.DBDisconnect();
        }

        //固定程式碼, 回傳驗證結果true/false;
        //return (nRet == 0) ? FormValidateResult(validateId, validateError, true) : FormValidateResult(validateId, validateError, false);
        return (nRet == 0).ToString();
    }


    /// <summary>
    /// 修改程序(AJAX)
    /// </summary>
    /// <param name="AccountID">使用者帳號</param>
    /// <param name="Name">*姓名</param>
    /// <param name="Description">職稱</param>
    /// <param name="Startup">啟用帳號</param>
    /// <param name="PWType">密碼類別(0:永久有效 1:定期變更)</param>
    /// <param name="cRoleID">權限等級</param>
    /// <param name="HiddenUserDefine">權限微調設定(0:預設未動作時 1:有改過下拉權限等級或是有按過權限微調按鈕畫面中的儲存)</param>
    /// <param name="HiddenFunctionList">自訂FunctionID List</param>
    /// <param name="HiddenActionList">自訂ActionID List</param>
    /// <param name="cCallID">語音查詢帳號</param>
    /// <param name="PWTypeDESC">密碼類別顯示字串</param>
    /// <param name="cRoleDESC">權限等級顯示字串</param>
    /// <param name="myAccountName">操作人員姓名</param>
    /// <param name="myAccountID">操作人員帳號</param>
    /// <returns>nRet 0:成功 -1, -2:失敗</returns>
    [System.Web.Services.WebMethod]
    public static CReturnData EdtProcess(string AccountID, string Name, string Description, string Startup, string PWType, string cRoleID, string HiddenUserDefine, string HiddenFunctionList, string HiddenActionList, string cCallID, string PWTypeDESC, string cRoleDESC, string myAccountName, string myAccountID)
    {
        CReturnData myData = new CReturnData();

        Database db = new Database();
        DataTable dt = new DataTable();
        
        string myGUID = System.Guid.NewGuid().ToString();
        
        string ModuleDesc = ParseWording("B0136");
        string FunctionDesc = ParseWording("B0137");
        string ActionDesc = ParseWording("A0002");
        string StrStartup = (Startup == "1") ? "V" : "";

        /*連線DB*/
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;
        if (myData.nRet == 0)
        {
            string StrSQL = "/*Keep修改前資料 START*/ " +
                "DECLARE @tmpSecurityUserAccount TABLE( " +
	                "Name nvarchar(50), " +
	                "[Description] nvarchar(100), " +
	                "StrStartup nvarchar(2), " +
	                "PWTypeDESC nvarchar(20), " +
	                "DMSRoleName nvarchar(100), " +
	                "cCallID nvarchar(10) " +
                ") " +
                "DECLARE @tmpSecurityUserAccount_FunctionRole TABLE( " +
	                "[SysFuncID] varchar(50) " +
                ") " +
                "DECLARE @tmpSecurityUserAccount_ActionRole TABLE( " +
	                "[SysActionID] varchar(50) " +
                ") " +
                "INSERT INTO @tmpSecurityUserAccount " +
	                "SELECT Name " +
		                ", [Description] " +
		                ", (CASE Startup " +
			                "WHEN 1 THEN 'V' " +
			                "ELSE '' " +
		                "END) AS StrStartup " +
		                ", (CASE PWType " +
                            "WHEN 0 THEN N'" + ParseWording("B0043") + "' " +
                            "ELSE N'" + ParseWording("B0044") + "' " +
		                "END) AS PWTypeDESC " +
		                ", DMSRole.DMSRoleName " +
		                ", cCallID " +
	                "FROM SecurityUserAccount " +
	                "INNER JOIN DMSRole ON SecurityUserAccount.cRoleID =  DMSRole.DMSRoleID " +
                    "WHERE SecurityUserAccount.AccountID = '" + AccountID + "' " +
                "INSERT INTO @tmpSecurityUserAccount_FunctionRole " +
                    "SELECT SysFuncID FROM SecurityUserAccount_FunctionRole WHERE AccountID = '" + AccountID + "' " +
                "INSERT INTO @tmpSecurityUserAccount_ActionRole " +
                    "SELECT SysActionID FROM SecurityUserAccount_ActionRole WHERE AccountID = '" + AccountID + "' " +
                "/*Keep修改前資料 END*/ " +

                "/*語音查詢帳號有變更時, 變更語音帳號/語音密碼預設等於帳號*/ " +
                "IF (SELECT cCallID FROM SecurityUserAccount WHERE AccountID = '" + AccountID + "') <> '" + cCallID + "' BEGIN " +
                    "UPDATE SecurityUserAccount SET cCallID = '" + cCallID + "' ,cPWD = '" + cCallID + "' WHERE AccountID = '" + AccountID + "' " +
                "END " +
                "/*更新基本資料*/ " +
                "UPDATE SecurityUserAccount " +
                "SET Name = '" + Name.Replace("'", "''") + "' " +
                    ",[Description] = '" + Description.Replace("'", "''") + "' " +
                    ",Startup = " + Startup + " " +
                    ",PWType = " + PWType + " " +
                    ",cRoleID = " + cRoleID + " " +
	                ",dModifyTime = GETDATE() " +
                "WHERE AccountID = '" + AccountID + "' " +
                "/*有改過下拉權限等級或是有按過權限微調按鈕畫面中的儲存*/ " +
                "IF (" + HiddenUserDefine + " = 1) BEGIN " +
	                "/*先刪掉舊的權限設定*/ " +
	                "DELETE FROM SecurityUserAccount_FunctionRole WHERE AccountID = '" + AccountID + "' " +
	                "DELETE FROM SecurityUserAccount_ActionRole WHERE AccountID = '" + AccountID + "' " +
	                "/*一定是預設權限(未按過權限微調按鈕)*/ " +
                    "IF ('" + HiddenFunctionList + "' = '') AND ('" + HiddenActionList + "' = '') BEGIN " +
	                    "INSERT INTO SecurityUserAccount_FunctionRole(AccountID, SysModID, SysFuncID) " +
			                "SELECT '" + AccountID + "', SysModID, SysFuncID " +
			                "FROM DMSRoleFunction " +
                            "WHERE DMSRoleID = " + cRoleID + " " +
		                "INSERT INTO SecurityUserAccount_ActionRole(AccountID, SysModID, SysFuncID, SysActionID) " +
			                "SELECT '" + AccountID + "', SysModID, SysFuncID, SysActionID " +
			                "FROM DMSRoleAction " +
                            "WHERE DMSRoleID = " + cRoleID + " " +
	                "END " +
	                "/*可能不是預設權限(有在權限微調畫面中按下儲存)*/ " +
	                "ELSE BEGIN " +
		                "INSERT INTO SecurityUserAccount_FunctionRole(AccountID, SysModID, SysFuncID) " +
			                "SELECT '" + AccountID + "', SystemFunction.SysModID, [Value] " +
                            "FROM dbo.UTILfn_Split('" + HiddenFunctionList + "', '||') AS tblFunction " +
			                "INNER JOIN SystemFunction ON tblFunction.Value = SystemFunction.SysFuncID " +
		                "INSERT INTO SecurityUserAccount_ActionRole(AccountID, SysModID, SysFuncID, SysActionID) " +
			                "SELECT '" + AccountID + "', SystemAction.SysModID, SystemAction.SysFuncID, [Value] " +
                            "FROM dbo.UTILfn_Split('" + HiddenActionList + "', '||') AS tblAction " +
			                "INNER JOIN SystemAction ON tblAction.Value = SystemAction.SysActionID " +
                    "END " +
                "END " +

                "/*操作紀錄-修改畫面*/" +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0006") + "', N'" + AccountID + "', N'" + AccountID + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0007") + "', Name, N'" + Name.Replace("'", "''") + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM @tmpSecurityUserAccount " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0008") + "', [Description], N'" + Description.Replace("'", "''") + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM @tmpSecurityUserAccount " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0011") + "', StrStartup, N'" + StrStartup + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM @tmpSecurityUserAccount " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0013") + "', PWTypeDESC, N'" + PWTypeDESC + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM @tmpSecurityUserAccount " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0010") + "', DMSRoleName, N'" + cRoleDESC + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM @tmpSecurityUserAccount " +
                "/*操作紀錄-新增畫面-權限微調*/" +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "'" +
                        ", N'" + ModuleDesc + "'" +
                        ", N'" + FunctionDesc + "'" +
                        ", N'" + ActionDesc + "'" +
                        ", N'" + ParseWording("B0015") + "' + '_' + SystemModule.ModuleDesc + '_' + SystemFunction.FunctionDesc " +
                        ", (CASE " +
                            "WHEN tblOld_FunctionRole.SysFuncID IS NULL THEN '' " +
                            "ELSE 'V' " +
                        "END) AS OldFunctionRoleCheck " +
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
                    "LEFT JOIN @tmpSecurityUserAccount_FunctionRole AS tblOld_FunctionRole ON DMSRoleFunction.SysFuncID = tblOld_FunctionRole.SysFuncID " +
                    "LEFT JOIN SecurityUserAccount_FunctionRole ON DMSRoleFunction.SysFuncID = SecurityUserAccount_FunctionRole.SysFuncID AND SecurityUserAccount_FunctionRole.AccountID = '" + AccountID + "' " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "'" +
                        ", N'" + ModuleDesc + "'" +
                        ", N'" + FunctionDesc + "'" +
                        ", N'" + ActionDesc + "'" +
                        ", N'" + ParseWording("B0015") + "' + '_' + SystemModule.ModuleDesc + '_' + SystemFunction.FunctionDesc + '_' + SystemAction.ActionDesc " +
                        ", (CASE " +
                            "WHEN tblOld_ActionRole.SysActionID IS NULL THEN '' " +
                            "ELSE 'V' " +
                        "END) AS OldActionRoleCheck " +
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
                    "LEFT JOIN @tmpSecurityUserAccount_ActionRole AS tblOld_ActionRole ON DMSRoleAction.SysActionID = tblOld_ActionRole.SysActionID " +
                    "LEFT JOIN SecurityUserAccount_ActionRole ON DMSRoleAction.SysActionID = SecurityUserAccount_ActionRole.SysActionID AND SecurityUserAccount_ActionRole.AccountID = '" + AccountID + "' ";

            /*判斷有無語音系統, 若有才寫入操作紀錄*/
            if (CheckSysAttribute("System", "VoiceSystem") == "1")
                StrSQL += "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0014") + "', cCallID, N'" + cCallID + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM @tmpSecurityUserAccount ";
        
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
