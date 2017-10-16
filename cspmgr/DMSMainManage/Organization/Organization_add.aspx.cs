using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MDS.Database;

public partial class DMSMainManage_Organization_Organization_add : BasePage
{
    protected string TargerGroupID = "";

    protected string PageNo = "0";
    protected string mySearch = "";

    protected string myGroupID = ""; 
    
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
        /*接收Request*/
        if (!string.IsNullOrEmpty(Request.QueryString["PageNo"]))
            PageNo = Request.QueryString["PageNo"];
        /*接收Request*/
        if (!string.IsNullOrEmpty(Request.QueryString["StrSearch"]))
            mySearch = Request.QueryString["StrSearch"];

     

        /*使用WebService的function來取得SiteMapAddress*/
        DMSWebService ws = new DMSWebService();
        string SiteMapAddress = ws.GET_SiteMapAddress(TargerGroupID);
        /*有搜尋條件則隱藏所在位置*/
        if (mySearch == "")
        {
         
            if (SiteMapAddress.ToString().Length > 0)
            {
                SiteMapAddress = ParseWording("A0014") + " " + SiteMapAddress;
                Label_Address.Text = SiteMapAddress;
            }

        }
        else
        {
            Label_Address.Text = "&nbsp;";
        }
    }


    /// <summary>
    /// 檢核使用者輸入的所屬群組是否存在(AJAX)
    /// </summary>
    /// <param name="validateId"></param>
    /// <param name="validateValue"></param>
    /// <param name="validateError"></param>
    /// <param name="ParentGroupID">使用者所屬群組</param>
    /// <param name="oParentGroupID">使用者輸入的所屬群組</param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string CheckParentGroupID(string validateId, string validateValue, string validateError, string ParentGroupID, string oParentGroupID)
    {
        int nRet = -1;
        Database db = new Database();
        DataTable dt = new DataTable();

        string strSQL = "";

        strSQL = "SELECT tblA.GroupID, GroupName " +
                "FROM SecurityGroup AS tblA " +
                "INNER JOIN dbo.fn_GetGroupTree(@ParentGroupID) AS tblT ON tblA.GroupID = tblT.GroupID " +
                "WHERE tblA.GroupID =@oParentGroupID";

        /*連線DB*/
        nRet = db.DBConnect();
        if (nRet == 0)
        {
            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(strSQL, db.getOcnn());
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ParentGroupID", ParentGroupID));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@oParentGroupID", oParentGroupID));

            nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);

            if (nRet == 0)
            {
                if (dt.Rows.Count > 0)
                    /*此GroupID存在*/
                    nRet = 0;
                else
                    /*此GroupID不存在*/
                    nRet = -1;
            }
            db.DBDisconnect();
        }
        //string filepath = @"c:\temp\posttestLog\";
        //System.IO.File.WriteAllText(filepath + "CheckParentGroupID.txt"
        //    , "\nParentGroupID>" + ParentGroupID 
        //    + "\n<validateValue>" + validateValue 
        //    + "\n<validateId>" + validateId 
        //    + "\n<validateError>" + validateError
        //     + "\n<oParentGroupID>" + oParentGroupID 
        //    + "\n<>" + strSQL);
        //固定程式碼, 回傳驗證結果true/false;
       // return (nRet == 0) ? FormValidateResult(validateId, validateError, true) : FormValidateResult(validateId, validateError, false);
        return (nRet == 0).ToString();
    }


    /// <summary>
    /// 檢核使用者輸入的代碼是否重覆(AJAX)
    /// </summary>
    /// <param name="validateId"></param>
    /// <param name="validateValue">使用者輸入的代碼</param>
    /// <param name="validateError"></param>
    /// <param name="ParentGroupID">使用者所屬群組</param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string CheckGroupID(string validateId, string validateValue, string validateError, string ParentGroupID)
    {
        //Request.QueryString["TargerGroupID"];
        int nRet = -1;
        Database db = new Database();
        DataTable dt = new DataTable();
        string GroupID = validateValue;

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
                if (dt.Rows.Count == 0)
                    /*此GroupID不重覆*/
                    nRet = 0;
                else
                    /*此GroupID重覆*/
                    nRet = -1;
            }
            db.DBDisconnect();
        }
        //string filepath = @"c:\temp\posttestLog\";
        //System.IO.File.WriteAllText(filepath + "CheckGroupID.txt", "ParentGroupID>" + ParentGroupID + "\n<validateValue>" + validateValue + "\n<validateId>" + validateId + "\n<validateError>" + validateError + "\n<>" + strSQL);
        //固定程式碼, 回傳驗證結果true/false;
        //return (nRet == 0) ? FormValidateResult(validateId, validateError, true) : FormValidateResult(validateId, validateError, false);
        return   (nRet == 0).ToString();
    }
    


    /// <summary>
    /// 新增程序(AJAX)
    /// </summary>
    /// <param name="oParentGroupID_Sel">*STR所屬群組</param>
    /// <param name="oParentGroupID">*所屬群組</param>
    /// <param name="oGroupID">*代碼</param>
    /// <param name="oGroupName">*名稱</param>
    /// <param name="oTEL">電話</param>
    /// <param name="oAddress">地址</param>
    /// <param name="oMemo">備註</param>
    /// <param name="myAccountName">操作人員姓名</param>
    /// <param name="myAccountID">操作人員帳號</param>
    /// <returns>nRet 0:成功 -1, -2:失敗</returns>
    [System.Web.Services.WebMethod]
    public static CReturnData AddProcess(string oParentGroupID_Sel, string oParentGroupID, string oGroupID, string oGroupName, string oTEL, string oAddress, string oMemo, string myAccountName, string myAccountID)
    {
        CReturnData myData = new CReturnData();

        Database db = new Database();
        DataTable dt = new DataTable();

        string myGUID = System.Guid.NewGuid().ToString();

        string ModuleDesc = ParseWording("B0136");
        string FunctionDesc = ParseWording("B0139");
        string ActionDesc = ParseWording("B0045");
        
        /*連線DB*/
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;
        if (myData.nRet == 0)
        {
            string StrSQL = "/*群組資料表*/ " +
                "INSERT INTO SecurityGroup(GroupID, GroupName, Address, TEL, Memo) " +
                    "SELECT '" + oGroupID + "', N'" + oGroupName.Replace("'", "''") + "', N'" + oAddress.Replace("'", "''") + "', '" + oTEL + "', N'" + oMemo.Replace("'", "''") + "' " +
                "/*從屬關係資料表*/ " +
                "INSERT INTO SecurityRelation(ParentGroupID, AccountType, AccountID) " +
                    "SELECT '" + oParentGroupID + "', '0', '" + oGroupID + "' " +

                "/*操作紀錄-新增畫面*/" +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0012") + "', '', N'" + oParentGroupID_Sel.Replace("'", "''") + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0047") + "', '', N'" + oGroupID + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0101") + "', '', N'" + oGroupName.Replace("'", "''") + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("A0019") + "', '', N'" + oAddress.Replace("'", "''") + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("A0020") + "', '', N'" + oTEL + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("A0021") + "', '', N'" + oMemo.Replace("'", "''") + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() ";
                
            /*寫入DB*/
            db.BeginTranscation();
            db.AddDmsSqlCmd(StrSQL);
            db.CommitTranscation();

            myData.nRet = db.nRet;
            myData.outMsg = db.outMsg;

            db.DBDisconnect();
            //string filepath = @"c:\temp\posttestLog\";
            //System.IO.File.WriteAllText(filepath + "AddProcess.txt", StrSQL);
        }
      
        return myData;
    }

}
