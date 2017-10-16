using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MDS.Database;
using System.Text;

public partial class DMSMainManage_Organization_Contact_add : BasePage
{
    protected string TargerGroupID = "";

    protected string PageNo = "0";
    protected string mySearch = "";
    protected string oGroupList = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        /*接收Request*/
        if (!string.IsNullOrEmpty(Request.QueryString["TargerGroupID"]))
            TargerGroupID = Request.QueryString["TargerGroupID"];
        /*接收Request*/
        if (!string.IsNullOrEmpty(Request.QueryString["PageNo"]))
            PageNo = Request.QueryString["PageNo"];
        /*接收Request*/
        if (!string.IsNullOrEmpty(Request.QueryString["StrSearch"]))
            mySearch = Request.QueryString["StrSearch"];

        /*取得並產生群組選項*/
        GetGroupList();
    }


    /// <summary>
    /// 取得並產生群組選項;
    /// </summary>
    /// <returns></returns>
    private void GetGroupList()
    {
        Database db = new Database();
        DataTable dt = new DataTable();

        int nRet = -1;

        string strParentGroupID = "";
        if (Session["ParentGroupID"]!= null) {
            strParentGroupID = Session["ParentGroupID"].ToString();
        }



        /*Get oGroupList SQL*/
        string StrSQL = "SELECT tblA.GroupID, tblA.GroupID + ' ' + tblA.GroupName AS GroupInfo " +
            "FROM SecurityGroup AS tblA " +
            "INNER JOIN dbo.fn_GetGroupTree(@strParentGroupID) AS tblT ON tblA.GroupID = tblT.GroupID ";

        StringBuilder options = new StringBuilder("");

        /*連線DB*/
        nRet = db.DBConnect();
        if (nRet == 0)
        {

            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(StrSQL, db.getOcnn());
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@strParentGroupID", strParentGroupID));
            nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);
 
            if (nRet == 0)
            {
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    options.Append(String.Format("<option value='{0}'>{1}</option>", dt.Rows[i]["GroupID"].ToString(), dt.Rows[i]["GroupInfo"].ToString()));
                    
                    //oGroupList.Items.Add(new ListItem(dt.Rows[i]["GroupInfo"].ToString(), dt.Rows[i]["GroupID"].ToString()));
                }
            }
            else
            {
                MessageBox(db.outMsg);
            }

            db.DBDisconnect();
        }


        //string filepath = @"c:\temp\posttestLog\";
        //System.IO.File.WriteAllText(filepath + "GetGroupList.txt"
        //   , "\n<>" + StrSQL);


        oGroupList = options.ToString();
       

        /*有錯誤則跳出警示視窗*/
      //  if (nRet != 0)
         //   MessageBox(nRet.ToString());
        
    }


    /// <summary>
    /// 新增程序(AJAX)
    /// </summary>
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
    public static CReturnData AddProcess(string Str_oGroupList_selected, string oContactName, string oTitle, string oTel1, string oTel2, string oTel3, string oEMail, string oMemo, string myAccountName, string myAccountID)
    {
        CReturnData myData = new CReturnData();

        Database db = new Database();
        DataTable dt = new DataTable();

        string myGUID = System.Guid.NewGuid().ToString();

        string ModuleDesc = ParseWording("B0136");
        string FunctionDesc = ParseWording("B0139");
        string ActionDesc = ParseWording("B0058");

        /*連線DB*/
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;
        if (myData.nRet == 0)
        {
            string StrSQL = "DECLARE @tmpContactID int /*取得MAX ContactID 加1 */ " +
                "SET @tmpContactID = (SELECT ISNULL(MAX(ContactID), 0) + 1 FROM SecurityGroup_Contact) " +
                "/*聯絡人資料 */ " +
                "INSERT INTO SecurityGroup_Contact(ContactID, ContactName, Title, Tel1, Tel2, Tel3, EMail, Memo) " +
                    "SELECT @tmpContactID, N'" + oContactName.Replace("'", "''") + "', N'" + oTitle.Replace("'", "''") + "', '" + oTel1 + "', '" + oTel2 + "', '" + oTel3 + "', '" + oEMail + "', N'" + oMemo.Replace("'", "''") + "' " +
                "/*群組與聯絡人關連表*/ " +
                "INSERT INTO SecurityGroup_ContactRelation(GroupID, ContactID) " +
                    "SELECT [Value], @tmpContactID FROM dbo.UTILfn_Split('" + Str_oGroupList_selected + "', ',') " +
                
                "/*操作紀錄-新增畫面 */ " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0012") + "', '', N'" + Str_oGroupList_selected.Replace("||", ",") + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0007") + "', '', N'" + oContactName.Replace("'", "''") + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0008") + "', '', N'" + oTitle.Replace("'", "''") + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("A0019") + "(1)', '', N'" + oTel1 + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("A0019") + "(2)', '', N'" + oTel2 + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("A0019") + "(3)', '', N'" + oTel3 + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("B0060") + "', '', N'" + oEMail.Replace("'", "''") + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() " +
                "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                    "SELECT '" + myGUID + "', N'" + ModuleDesc + "', N'" + FunctionDesc + "', N'" + ActionDesc + "', N'" + ParseWording("A0021") + "', '', N'" + oMemo.Replace("'", "''") + "', N'" + myAccountName + "', N'" + myAccountID + "', GETDATE() ";
                
            /*寫入DB*/
            db.BeginTranscation();
            db.AddDmsSqlCmd(StrSQL);
            db.CommitTranscation();

            myData.nRet = db.nRet;
            myData.outMsg = db.outMsg;

            db.DBDisconnect();
//            string filepath = @"c:\temp\posttestLog\";
//            System.IO.File.WriteAllText(filepath + "AddProcess.txt",
//                "\nStr_oGroupList_selected "+  Str_oGroupList_selected 
//+"\n  oContactName          "+  oContactName
//+"\n  oTitle                "+  oTitle
//+"\n  oTel1                 "+  oTel1
//+"\n  oTel2                 "+  oTel2
//+"\n  oTel3                 "+  oTel3
//+"\n  oEMail                "+  oEMail
//+"\n  oMemo                 "+  oMemo
//+"\n  myAccountName         "+  myAccountName
//+"\n  myAccountID           "+  myAccountID

//                +"\n<>" + StrSQL);
        }
        return myData;
    }



}
