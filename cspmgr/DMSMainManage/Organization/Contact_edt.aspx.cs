using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MDS.Database;
using System.Text;

public partial class DMSMainManage_Organization_Contact_edt : BasePage
{
    protected string TargerGroupID = "";

    protected string PageNo = "0";
    protected string mySearch = "";

    protected string ContactID = "";
     protected string _oContactName = "";
     protected string _oTitle = "";
     protected string _oTel1 = "";
     protected string _oTel2 = "";
     protected string _oTel3 = "";
     protected string _oEMail = "";
     protected string _oMemo = "";
     protected string oGroupList = "";
    
     
    protected void Page_Load(object sender, EventArgs e)
    {
        /*接收Request*/
        if (!string.IsNullOrEmpty(Request.QueryString["TargerGroupID"]))
            TargerGroupID = MDS.Utility.NUtility.trimBad(Request.QueryString["TargerGroupID"]);
        /*接收Request*/
        if (!string.IsNullOrEmpty(Request.QueryString["PageNo"]))
            PageNo = MDS.Utility.NUtility.trimBad(Request.QueryString["PageNo"]);
        /*接收Request*/
        if (!string.IsNullOrEmpty(Request.QueryString["StrSearch"]))
            mySearch = MDS.Utility.NUtility.trimBad(Request.QueryString["StrSearch"]);
        /*接收Request*/
        if (!string.IsNullOrEmpty(Request.QueryString["ContactID"]))
            ContactID = MDS.Utility.NUtility.trimBad(Request.QueryString["ContactID"]);


        /*取得並產生群組選項*/
        GetGroupList(ContactID);

        /*取得聯絡人詳細資料*/
        GetContactData(ContactID);

        //確認是否有目前登入的使用者是否有修改的權限
        if (CheckUserRight("9F887083-859F-46FE-80C3-5CC4FFC9211D") == false)
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
    }


    /// <summary>
    /// 取得並產生群組選項;
    /// </summary>
    /// <param name="ContactID">要修改的聯絡人ID</param>
    private void GetGroupList(string ContactID)
    {

        /*建立DB*/
        CReturnData myData = new CReturnData();
        Database db = new Database();
        DataTable dt = new DataTable();


        /*連線DB*/
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;

        string strParentGroupID = Session["ParentGroupID"] == null ? "" : Session["ParentGroupID"].ToString();
        
        strParentGroupID = MDS.Utility.NUtility.trimBad(strParentGroupID);

        /*Get oGroupList SQL*/
        string StrSQL = "SELECT tblA.GroupID, tblA.GroupID + ' ' + tblA.GroupName AS GroupInfo, ISNULL(SecurityGroup_ContactRelation.ContactID, 0) AS ContactID " +
            "FROM SecurityGroup AS tblA " +
            "INNER JOIN dbo.fn_GetGroupTree(@ParentGroupID) AS tblT ON tblA.GroupID = tblT.GroupID " +
            "LEFT JOIN SecurityGroup_ContactRelation ON tblA.GroupID = SecurityGroup_ContactRelation.GroupID AND SecurityGroup_ContactRelation.ContactID =@ContactID";

        StringBuilder options = new StringBuilder("");


        try
        {

            /*查詢DB*/
            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(StrSQL, db.getOcnn());
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ParentGroupID", strParentGroupID));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ContactID", ContactID));

            //呼叫傳入參數式sqlcmd的方法
            myData.nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);
            myData.outMsg = db.outMsg;


            if (myData.nRet == 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ContactID"].ToString() == "0")
                    {
                        options.Append(String.Format("<option value='{0}'>{1}</option>", dt.Rows[i]["GroupID"].ToString(), dt.Rows[i]["GroupInfo"].ToString()));
                        // oGroupList.Items.Add(new ListItem(dt.Rows[i]["GroupInfo"].ToString(), dt.Rows[i]["GroupID"].ToString()));
                    }
                    else
                    {
                        options.Append(String.Format("<option value='{0}' selected='selected'>{1}</option>", dt.Rows[i]["GroupID"].ToString(), dt.Rows[i]["GroupInfo"].ToString()));
                        //oGroupList_selected.Items.Add(new ListItem(dt.Rows[i]["GroupInfo"].ToString(), dt.Rows[i]["GroupID"].ToString()));
                    }
                }
            }
            else
            {
                MessageBox(db.outMsg);
            }

           

        }
        catch (Exception ex)
        {


        }
        finally
        {

            db.DBDisconnect();
        }
       

               

 

 
        /*有錯誤則跳出警示視窗*/
        if (myData.nRet != 0)
            MessageBox(myData.nRet.ToString());

        oGroupList = options.ToString();
    }


    /// <summary>
    /// 取得聯絡人詳細資料;
    /// </summary>
    /// <param name="ContactID">要修改的聯絡人ID</param>
    private void GetContactData(string ContactID)
    {
        Database db = new Database();
        DataTable dt = new DataTable();

        int nRet = -1;

        /*Get SecurityGroup_Contact Data SQL*/
        string StrSQL = "SELECT ContactID, ContactName, ISNULL(Title, '') AS Title, ISNULL(Tel1, '') AS Tel1, ISNULL(Tel2, '') AS Tel2, ISNULL(Tel3, '') AS Tel3, ISNULL(EMail, '') AS EMail, ISNULL(Memo, '') AS Memo " +
            "FROM SecurityGroup_Contact " +
            "WHERE ContactID = @ContactID";

        /*連線DB*/
        nRet = db.DBConnect();
        if (nRet == 0)
        {

            /*查詢DB*/
            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(StrSQL, db.getOcnn());
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ContactID", ContactID));

            nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);


            if (nRet == 0)
            {
                if (dt.Rows.Count > 0)
                {
                    _oContactName  = dt.Rows[0]["ContactName"].ToString();
                    _oTitle  = dt.Rows[0]["Title"].ToString();
                    _oTel1  = dt.Rows[0]["Tel1"].ToString();
                    _oTel2  = dt.Rows[0]["Tel2"].ToString();
                    _oTel3  = dt.Rows[0]["Tel3"].ToString();
                    _oEMail  = dt.Rows[0]["EMail"].ToString();
                    _oMemo  = dt.Rows[0]["Memo"].ToString();
                }
            }
            else
            {
                MessageBox(db.outMsg);
            }

            db.DBDisconnect();
        }

        /*有錯誤則跳出警示視窗*/
        if (nRet != 0)
            MessageBox(nRet.ToString());

    }


    /// <summary>
    /// 修改程序(AJAX)
    /// </summary>
    /// <param name="ContactID">要修改的聯絡人ID</param>
    /// <param name="Str_oGroupList_selected">*STR所屬群組(GroupID1||GroupID2||GroupID3||GroupID4)</param>
    /// <param name="oContactName">姓名</param>
    /// <param name="oTitle">職稱</param>
    /// <param name="oTel1">電話(1)</param>
    /// <param name="oTel2">電話(2)</param>
    /// <param name="oTel3">電話(3)</param>
    /// <param name="oEMail">E-Mail</param>
    /// <param name="oMemo">備註</param>
    /// <param name="myAccountName">操作人員姓名</param>
    /// <param name="myAccountID">操作人員帳號</param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static CReturnData EdtProcess(string ContactID, string Str_oGroupList_selected, string oContactName, string oTitle, string oTel1, string oTel2, string oTel3, string oEMail, string oMemo, string myAccountName, string myAccountID)
    {
        CReturnData myData = new CReturnData();

        Database db = new Database();
        DataTable dt = new DataTable();

        string myGUID = System.Guid.NewGuid().ToString();

        string ModuleDesc = ParseWording("B0136");
        string FunctionDesc = ParseWording("B0139");
        string ActionDesc = ParseWording("B0059");

        /*連線DB*/
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;
        if (myData.nRet == 0)
        {
            string StrSQL = "/*取得修改前的 群組與聯絡人關連表 字串 */ " +
                "DECLARE @STR_GroupID varchar(max) " +
                "SET @STR_GroupID = '' " +
                "SELECT @STR_GroupID = @STR_GroupID + GroupID + ',' FROM SecurityGroup_ContactRelation WHERE ContactID = " + ContactID + " " +
                "IF LEN(@STR_GroupID) > 0 BEGIN SET @STR_GroupID = LEFT(@STR_GroupID, LEN(@STR_GroupID) - 1) END " +
                "/*操作紀錄-修改畫面 */ " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0012") + "', @STR_GroupID, N'" + Str_oGroupList_selected.Replace("||", ",") + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0007") + "', ContactName, N'" + oContactName.Replace("'", "''") + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM SecurityGroup_Contact WHERE ContactID = " + ContactID + " " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0008") + "', Title, N'" + oTitle.Replace("'", "''") + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM SecurityGroup_Contact WHERE ContactID = " + ContactID + " " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("A0019") + "(1)', Tel1, N'" + oTel1 + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM SecurityGroup_Contact WHERE ContactID = " + ContactID + " " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("A0019") + "(2)', Tel2, N'" + oTel2 + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM SecurityGroup_Contact WHERE ContactID = " + ContactID + " " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("A0019") + "(3)', Tel3, N'" + oTel3 + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM SecurityGroup_Contact WHERE ContactID = " + ContactID + " " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0060") + "', EMail, N'" + oEMail.Replace("'", "''") + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM SecurityGroup_Contact WHERE ContactID = " + ContactID + " " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("A0021") + "', Memo, N'" + oMemo.Replace("'", "''") + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() FROM SecurityGroup_Contact WHERE ContactID = " + ContactID + " " +
                "/*聯絡人資料 更新 */ " +
                "UPDATE SecurityGroup_Contact " +
                "SET ContactName = N'" + oContactName.Replace("'", "''") + "' " +
                    ", Title = N'" + oTitle.Replace("'", "''") + "' " +
                    ", Tel1 = '" + oTel1 + "' " +
                    ", Tel2 = '" + oTel2 + "' " +
                    ", Tel3 = '" + oTel3 + "' " +
                    ", EMail = '" + oEMail + "' " +
                    ", Memo = N'" + oMemo.Replace("'", "''") + "' " +
                "WHERE ContactID = " + ContactID + " " +
                "/*群組與聯絡人關連表 更新 */ " +
                "DELETE FROM SecurityGroup_ContactRelation WHERE ContactID = " + ContactID + " " +
                "INSERT INTO SecurityGroup_ContactRelation(GroupID, ContactID) " +
                    "SELECT [Value], " + ContactID + " FROM dbo.UTILfn_Split('" + Str_oGroupList_selected + "', ',') ";
                
            /*寫入DB*/
            db.BeginTranscation();
            db.AddDmsSqlCmd(StrSQL);
            db.CommitTranscation();

            myData.nRet = db.nRet;
            myData.outMsg = db.outMsg;

            db.DBDisconnect();
//            string filepath = @"c:\temp\posttestLog\";
//            System.IO.File.WriteAllText(filepath +  "EDTProcess.txt",
//                 "\nContactID " + ContactID 
//               + "\nStr_oGroupList_selected " + Str_oGroupList_selected
//+ "\n  oContactName          " + oContactName
//+ "\n  oTitle                " + oTitle
//+ "\n  oTel1                 " + oTel1
//+ "\n  oTel2                 " + oTel2
//+ "\n  oTel3                 " + oTel3
//+ "\n  oEMail                " + oEMail
//+ "\n  oMemo                 " + oMemo
//+ "\n  myAccountName         " + myAccountName
//+ "\n  myAccountID           " + myAccountID

//                + "\n<>" + StrSQL);
        }
        return myData;
    }


}
