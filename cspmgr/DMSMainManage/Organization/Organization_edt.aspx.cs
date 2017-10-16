using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MDS.Database;

public partial class DMSMainManage_Organization_Organization_edt : BasePage
{
    protected string TargerGroupID = "";
    protected string _oParentGroupID_Sel = "";
    protected string _oParentGroupID = "";
    protected string _oGroupID = "";
    protected string _oGroupName = "";
    protected string _oTEL = "";
    protected string _oAddress = "";
    protected string _oMemo = "";

    protected string PageNo = "0";
    protected string mySearch = "";

    protected string myGroupID = "";
       
       
      
    protected void Page_Load(object sender, EventArgs e)
    {
        string myUserID = "";
        string GroupID = "";

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
        /*接收Request*/
        if (!string.IsNullOrEmpty(Request.QueryString["GroupID"]))
            GroupID = Request.QueryString["GroupID"];

        /*給AutoComplete預設值*/
      //  oParentGroupID.Value = TargerGroupID;
      //  oParentGroupID_Sel.Text = TargerGroupID;

        /*取得組織資料*/
        GetSecurityGroupData(GroupID);
        //SELECT   [ParentGroupID]   FROM  [SecurityRelation] where AccountID = ''
        //確認是否有目前登入的使用者是否有修改的權限
        if (CheckUserRight("744519CF-500B-4926-9EE9-716C042A5B6E") == false)
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

        /*使用WebService的function來取得SiteMapAddress*/
        DMSWebService ws = new DMSWebService();
        string SiteMapAddress = ws.GET_SiteMapAddress(TargerGroupID);
        /*有搜尋條件則隱藏所在位置*/
        if (mySearch == "")
        {
            SiteMapAddress = ParseWording("A0014") + " " + SiteMapAddress;
            Label_Address.Text = SiteMapAddress;

        }
        else
        {
            Label_Address.Text = "&nbsp;";
        }
    }


    /// <summary>
    /// 取得組織資料;
    /// </summary>
    /// <param name="GroupID">組織ID</param>
    /// <returns></returns>
    private void GetSecurityGroupData(string GroupID)
    {
        Database db = new Database();
        DataTable dt = new DataTable();

        int nRet = -1;

        /*Get SecurityGroup Data SQL*/
        string StrSQL = "SELECT SecurityRelation.ParentGroupID, SecurityRelation.ParentGroupID + ' ' + tblA.GroupName AS ParentGroupInfo, tblA.GroupID, tblA.GroupName, tblA.[Address], tblA.[TEL], tblA.[Memo] " +
            "FROM SecurityGroup AS tblA " +
            "INNER JOIN SecurityRelation ON tblA.GroupID = SecurityRelation.AccountID AND SecurityRelation.AccountType = '0' " +
            //"INNER JOIN SecurityGroup ON SecurityRelation.ParentGroupID = SecurityGroup.GroupID " +
            "WHERE tblA.GroupID =@GroupID";


        /*查詢DB*/
        System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(StrSQL, db.getOcnn());
        SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("GroupID", MDS.Utility.NUtility.checkString(GroupID)));


        /*連線DB*/
        nRet = db.DBConnect();
        if (nRet == 0)
        {
            nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);

            if (nRet == 0)
            {
                if (dt.Rows.Count == 1)
                {
                    _oParentGroupID_Sel = dt.Rows[0]["ParentGroupInfo"].ToString();
                    _oParentGroupID = dt.Rows[0]["ParentGroupID"].ToString();
                    _oGroupID = dt.Rows[0]["GroupID"].ToString();
                    _oGroupName = dt.Rows[0]["GroupName"].ToString();
                    _oTEL = dt.Rows[0]["TEL"].ToString();
                    _oAddress = dt.Rows[0]["Address"].ToString();
                    _oMemo = dt.Rows[0]["Memo"].ToString();
                }
                else
                {
                    /*查無資料*/
                    nRet = -3;
                }
            }
            else {
                MessageBox(db.outMsg);
            }

            db.DBDisconnect();
        }

        /*有錯誤則跳出警示視窗*/
        if (nRet != 0)
            MessageBox(nRet.ToString());

        /*Get GetUserData END*/
    }


    /// <summary>
    /// 所屬群組下拉選單(AJAX)
    /// </summary>
    /// <param name="q">使用者輸入的查詢字串</param>
    /// <param name="ParentGroupID">使用者所屬群組</param>
    /// <returns>JSON格式字串</returns>
    [System.Web.Services.WebMethod()]
    public static string GetGroupList(string q, string ParentGroupID)
    {
        //ParentGroupID = "Console";
        string strGroupList = "";
        List<string> lst = new List<string>();

        string strSQL = "";
        Database db = new Database();
        DataTable dt = new DataTable();
        int nRet = -1;
         
        
        
        nRet = db.DBConnect();
        if (nRet == 0)
        {
            strSQL = "SELECT tblA.GroupID, GroupName " +
                "FROM SecurityGroup AS tblA " +
                "INNER JOIN dbo.fn_GetGroupTree(@ParentGroupID) AS tblT ON tblA.GroupID = tblT.GroupID ";

            if (q != "")
                strSQL += "WHERE UPPER(tblA.GroupID + ' ' + GroupName) LIKE '%'+@GroupName+'%'";


            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(strSQL, db.getOcnn());
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ParentGroupID", ParentGroupID));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GroupName", q.ToUpper()));
            nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);

           
            if (nRet == 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lst.Add(string.Format("{{\"GroupID\":\"{0}\", \"GroupName\":\"{1}\"}}",
                        dt.Rows[i]["GroupID"].ToString().Replace("\"", "\\\""),
                        dt.Rows[i]["GroupName"].ToString().Replace("\"", "\\\""))
                    );
                }

                //無論是否有資料, 都要回傳陣列字串
                strGroupList = "[" + string.Join(",", lst.ToArray()) + "]";
            }
            else
            {
                return db.outMsg;
            }

            db.DBDisconnect();
        }
        else
        {
            return db.outMsg;
        }

        return strGroupList;
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
                "WHERE tblA.GroupID = @GroupID";
      
        /*連線DB*/
        nRet = db.DBConnect();
        if (nRet == 0)
        {

            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(strSQL, db.getOcnn());
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ParentGroupID", ParentGroupID));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GroupID", oParentGroupID));
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

        //固定程式碼, 回傳驗證結果true/false;
        return (nRet == 0) ? FormValidateResult(validateId, validateError, true) : FormValidateResult(validateId, validateError, false);
    }


    /// <summary>
    /// 修改程序(AJAX)
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
    public static CReturnData EdtProcess(string oParentGroupID_Sel, string oParentGroupID, string oGroupID, string oGroupName, string oTEL, string oAddress, string oMemo, string myAccountName, string myAccountID)
    {
        CReturnData myData = new CReturnData();

        Database db = new Database();
        DataTable dt = new DataTable();

        string myGUID = System.Guid.NewGuid().ToString();

        string ModuleDesc = ParseWording("B0136");
        string FunctionDesc = ParseWording("B0139");
        string ActionDesc = ParseWording("B0046");
        

        /*連線DB*/
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;
        if (myData.nRet == 0)
        {
            string StrSQL = "/*Keep修改前資料(因為修改紀錄要靠JOIN出來, 所以先整理出來) START*/ " +
                "DECLARE @tmpTable TABLE( " +
                    "ParentGroupInfo nvarchar(110), " +
                    "GroupID varchar(10), " +
                    "GroupName nvarchar(100), " +
                    "[Address] nvarchar(100), " +
                    "[TEL] nvarchar(25), " +
                    "[Memo] nvarchar(1000) " +
                ") " +
                "INSERT INTO @tmpTable " +
                    "SELECT SecurityRelation.ParentGroupID + ' ' + SecurityGroup.GroupName AS ParentGroupInfo, tblA.GroupID, tblA.GroupName, tblA.[Address], tblA.[TEL], tblA.[Memo] " +
                    "FROM SecurityGroup AS tblA " +
                    "INNER JOIN SecurityRelation ON tblA.GroupID = SecurityRelation.AccountID AND SecurityRelation.AccountType = '0' " +
                    "INNER JOIN SecurityGroup ON SecurityRelation.ParentGroupID = SecurityGroup.GroupID " +
                    "WHERE tblA.GroupID = '" + oGroupID + "' " +
                "/*Keep修改前資料(因為修改紀錄要靠JOIN出來, 所以先整理出來) END*/ " +

                "/*更新組織資料 */ " +
                "UPDATE SecurityGroup " +
                "SET GroupName = N'" + oGroupName.Replace("'", "''") + "' " +
                    ",[Address] = N'" + oAddress.Replace("'", "''") + "' " +
                    ",[TEL] = '" + oTEL + "' " +
                    ",[Memo] = N'" + oMemo.Replace("'", "''") + "' " +
                "WHERE GroupID = '" + oGroupID + "' " +
                "/*從屬關係資料表 */ " +
                "UPDATE SecurityRelation SET ParentGroupID = '" + oParentGroupID + "' WHERE AccountID = '" + oGroupID + "' AND AccountType = '0' " +
                
                "/*操作紀錄-修改畫面*/" +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0012") + "', ParentGroupInfo, N'" + oParentGroupID_Sel.Replace("'", "''") + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM @tmpTable " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0101") + "', GroupName, N'" + oGroupName.Replace("'", "''") + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM @tmpTable " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("A0019") + "', [Address], N'" + oAddress.Replace("'", "''") + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM @tmpTable " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("A0020") + "', [TEL], N'" + oTEL + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM @tmpTable " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("A0021") + "', [Memo], N'" + oMemo.Replace("'", "''") + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM @tmpTable ";


         
            /*寫入DB*/
            db.BeginTranscation();
            db.AddDmsSqlCmd(StrSQL);
            db.CommitTranscation();

            myData.nRet = db.nRet;
            myData.outMsg = db.outMsg;

            db.DBDisconnect();

            //string filepath = @"c:\temp\posttestLog\";
            //System.IO.File.WriteAllText(filepath + "EdtProcess.txt", StrSQL);

        }
        return myData;
    }



}
