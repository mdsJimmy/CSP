using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MDS.Database;

public partial class DMSMainManage_Organization_Organization_List : BasePage
{
    string myGroupID = "";
    protected string TargerGroupID = "";

    Database db = new Database();
    DataTable dt = new DataTable();

    int nRet = -1;

    protected string SiteMapAddress = "";
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
            mySearch = TrimString.trimBad(Request.QueryString["StrSearch"]);

       
        if(OrganizationList == null){
            ContentPlaceHolder MySecondContent = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
            OrganizationList = MySecondContent.FindControl("OrganizationList") as ASP.dmscontrol_olistview_ascx;
            StrSearch = MySecondContent.FindControl("StrSearch") as System.Web.UI.HtmlControls.HtmlInputText;
        }

        if (StrSearch != null) {
            //StrSearch接Request;
            StrSearch.Value = mySearch;
        }


        if (OrganizationList != null) {

            //加入欄位Start
            OrganizationList.AddCol(ParseWording("B0051"), "GroupID", "CENTER");
            OrganizationList.AddCol(ParseWording("B0052"), "GroupName", "LEFT");
            OrganizationList.AddCol(ParseWording("A0020"), "Address", "LEFT");
            OrganizationList.AddCol(ParseWording("B0053"), "GroupTotal", "RIGHT");
            OrganizationList.AddCol(ParseWording("B0054"), "ContactTotal", "RIGHT");
            //加入欄位End

            //設定Key值欄位
            OrganizationList.DataKeyNames = "GroupID"; //Key以,隔開

            //設定是否顯示CheckBox(預設是true);
            if (CheckUserRight("5A48ECDA-7E32-4CF4-9B23-B0A4764A0775") == false)
            {
                OrganizationList.IsUseCheckBox = false;
            }

            //設定SQL
            strSQL = "SELECT tblA.GroupID, tblA.GroupName, tblA.[Address] "
                    + ",(SELECT COUNT(*) FROM dbo.fn_GetGroupTree(tblA.GroupID) WHERE GroupID <> tblA.GroupID) AS GroupTotal "
                    + ",(SELECT COUNT(*) FROM SecurityGroup_ContactRelation WHERE GroupID = tblA.GroupID) AS ContactTotal "
                + "FROM SecurityGroup AS tblA ";

            string mySearchTXT = "";
            if (mySearch == "")
            {
                strSQL += "INNER JOIN dbo.fn_GetGroupTree(@TargerGroupID) AS tblT ON tblA.GroupID = tblT.GroupID ";
                OrganizationList.putQueryParameter("TargerGroupID", ( TargerGroupID));
            }
            else
            {
                mySearchTXT = mySearch.Replace("'", "''");

                strSQL += "INNER JOIN dbo.fn_GetGroupTree(@ParentGroupID) AS tblT ON tblA.GroupID = tblT.GroupID "
                    + "WHERE tblA.GroupID LIKE '%'+@mySearchTXT_1+'%' OR tblA.GroupName LIKE '%'+@mySearchTXT_2+'%' ";

                OrganizationList.putQueryParameter("ParentGroupID", Session["ParentGroupID"].ToString());
                OrganizationList.putQueryParameter("mySearchTXT_1", mySearchTXT);
                OrganizationList.putQueryParameter("mySearchTXT_2", mySearchTXT);


            }
            strSQL += "ORDER BY tblT.[Rank], tblA.GroupID ";







            //取得SQL;
            OrganizationList.SelectString = strSQL;
            OrganizationList.prepareStatement();






            //設定每筆資料按下去的Javascript function
            OrganizationList.OnClickExecFunc = "DoEdt()";


            //設定每頁筆數
            OrganizationList.PageSize = 10;

            //接來自Request的排序欄位、排序方向、目前頁數
            ListViewSortKey = Request.Params["ListViewSortKey"];
            ListViewSortDirection = Request.Params["ListViewSortDirection"];
            PageNo = Request.Params["PageNo"];

            //設定排序欄位及方向
            if (!string.IsNullOrEmpty(ListViewSortKey) && !string.IsNullOrEmpty(ListViewSortDirection))
            {
                OrganizationList.ListViewSortKey = ListViewSortKey;
                OrganizationList.ListViewSortDirection = (SortDirection)Enum.Parse(typeof(SortDirection), ListViewSortDirection);
            }

            //設定目前頁數
            if (!string.IsNullOrEmpty(PageNo))
                OrganizationList.PageNo = int.Parse(PageNo);


        
        }
        
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        //權限確認;
        if (CheckUserRight("2B00E936-439A-4698-B0CD-E05336B6E7D2") == false)
        {
            btnAdd.Visible = false;
        }
        if (CheckUserRight("5A48ECDA-7E32-4CF4-9B23-B0A4764A0775") == false)
        {
            btnDel.Visible = false;
        }

        /*使用WebService的function來取得SiteMapAddress*/
        DMSWebService ws = new DMSWebService();
        string SiteMapAddress = ws.GET_SiteMapAddress(TargerGroupID);

        //string SiteMapAddress = "MMMMMMMMMMMMMMMMMMMMMMMMMMM";
        /*有搜尋條件則隱藏所在位置*/
        if (mySearch == "")
        {
            if (SiteMapAddress.ToString().Length >0)
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
            ListViewSortKey = OrganizationList.ListViewSortKey;
            ListViewSortDirection = OrganizationList.ListViewSortDirection.ToString();
            PageNo = OrganizationList.PageNo.ToString();
        }
    }


    /// <summary>
    /// 刪除群組(AJAX)
    /// </summary>
    /// <param name="GroupIDList">群組清單(GroupID1^^GroupID2^^GroupID3...)</param>
    /// <param name="myAccountName">操作人員姓名</param>
    /// <param name="myAccountID">操作人員帳號</param>
    /// <returns>nRet 0:刪除成功 -1, -2:失敗</returns>
    [System.Web.Services.WebMethod]
    public static CReturnData Delete_GroupID(string GroupIDList, string myAccountName, string myAccountID)
    {
        CReturnData myData = new CReturnData();
        Database db = new Database();
        DataTable dt = new DataTable();

        string ModuleDesc = ParseWording("B0136");
        string FunctionDesc = ParseWording("B0139");
        string ActionDesc = ParseWording("B0152");

        /*產生刪除對照表SQL*/
        string StrSQL = "SELECT tblA.[Value] + ' ' + SecurityGroup.GroupName AS GroupInfo " +
                ",(SELECT COUNT(*) FROM SecurityRelation WHERE ParentGroupID = tblA.[Value]) AS CountGroup " +
                ",(SELECT COUNT(*) FROM SecurityUserAccount WHERE GroupID = tblA.[Value]) AS CountUser " +
                ",(SELECT COUNT(*) FROM MCData WHERE GroupID = tblA.[Value]) AS CountDevice " +
            "FROM dbo.UTILfn_Split(@GroupIDList, '^^') AS tblA " +
            "INNER JOIN SecurityGroup ON tblA.[Value] = SecurityGroup.GroupID ";


        /*連線DB*/
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;
        if (myData.nRet == 0)
        {



            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(StrSQL, db.getOcnn());
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GroupIDList", GroupIDList));
            myData.nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);
    


            myData.outMsg = db.outMsg;
            if (myData.nRet == 0)
            {

                /*產生刪除帳號的SQL*/
                string Del_StrSQL = "DECLARE @tmpGroupID TABLE( " +
                        "myGUID varchar(50), " +
                        "GroupID varchar(10), " +
                        "GroupName nvarchar(100), " +
                        "CountGroup int, " +
                        "CountUser int, " +
                        "CountDevice int " +
                    ") " +
                    "INSERT INTO @tmpGroupID(myGUID, GroupID, GroupName, CountGroup, CountUser, CountDevice) " +
                        "SELECT newid(), tblA.[Value], SecurityGroup.GroupName " +
                            ",(SELECT COUNT(*) FROM SecurityRelation WHERE ParentGroupID = tblA.[Value]) " +
                            ",(SELECT COUNT(*) FROM SecurityUserAccount WHERE GroupID = tblA.[Value]) " +
                            ",(SELECT COUNT(*) FROM MCData WHERE GroupID = tblA.[Value]) " +
                        "FROM dbo.UTILfn_Split(@GroupIDList, '^^') AS tblA " +
                        "INNER JOIN SecurityGroup ON tblA.[Value] = SecurityGroup.GroupID " +
                    "/*組織資料表*/ " +
                    "DELETE FROM SecurityGroup WHERE GroupID IN (SELECT GroupID FROM @tmpGroupID WHERE CountGroup = 0 AND CountUser = 0 AND CountDevice = 0) " +
                    "/*從屬關係資料表*/ " +
                    "DELETE FROM SecurityRelation WHERE AccountID IN (SELECT GroupID FROM @tmpGroupID WHERE CountGroup = 0 AND CountUser = 0 AND CountDevice = 0) AND AccountType = '0' " +
                    "/*群組與聯絡人關連表*/ " +
                    "DELETE FROM SecurityGroup_ContactRelation WHERE GroupID IN (SELECT GroupID FROM @tmpGroupID WHERE CountGroup = 0 AND CountUser = 0 AND CountDevice = 0) " +
                    "/*聯絡人資料(補刪除SecurityGroup_ContactRelation筆數為0的資料即可)*/ " +
                    "DELETE FROM SecurityGroup_Contact WHERE (SELECT COUNT(*) FROM SecurityGroup_ContactRelation WHERE ContactID = SecurityGroup_Contact.ContactID) = 0 " +


                    "/*操作紀錄-刪除*/" +
                    "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                        "SELECT myGUID, @ModuleDesc1, @FunctionDesc1, @ActionDesc1, @B00121, GroupID, '', @myAccountName1, @myAccountID1, GETDATE() FROM @tmpGroupID WHERE CountGroup = 0 AND CountUser = 0 AND CountDevice = 0 " +
                    "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                        "SELECT myGUID, @ModuleDesc2, @FunctionDesc2, @ActionDesc2, @B0052, GroupName, '', @myAccountName2, @myAccountID12, GETDATE() FROM @tmpGroupID WHERE CountGroup = 0 AND CountUser = 0 AND CountDevice = 0 ";

                System.Data.SqlClient.SqlCommand SqlCom2 = new System.Data.SqlClient.SqlCommand(Del_StrSQL, db.getOcnn());

                SqlCom2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GroupIDList", GroupIDList));
 
                SqlCom2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModuleDesc1", ModuleDesc));
                SqlCom2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FunctionDesc1", FunctionDesc));
                SqlCom2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ActionDesc1", ActionDesc));
                SqlCom2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@B00121", ParseWording("B0051")));
                SqlCom2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@myAccountName1", myAccountName));
                SqlCom2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@myAccountID1", myAccountID));

                SqlCom2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModuleDesc2", ModuleDesc));
                SqlCom2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FunctionDesc2", FunctionDesc));
                SqlCom2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ActionDesc2", ActionDesc));
                SqlCom2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@B0052", ParseWording("B0052")));
                SqlCom2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@myAccountName2", myAccountName));
                SqlCom2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@myAccountID12", myAccountID));



                /*寫入DB*/
                myData.nRet = db.ExecQuerySQLCommand(SqlCom2, ref dt);

                myData.nRet = db.nRet;
                myData.outMsg = db.outMsg;
                /*組回傳結果字串*/
                if (myData.nRet == 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++) 
                    { 
                        if (dt.Rows[i]["CountGroup"].ToString() != "0") 
                        {
                            myData.returnData += dt.Rows[i]["GroupInfo"].ToString() + ParseWording("B0056") + "\n"; 
                        }
                        else if (dt.Rows[i]["CountUser"].ToString() != "0")
                        {
                            myData.returnData += dt.Rows[i]["GroupInfo"].ToString() + ParseWording("B0055") + "\n";
                        }
                        else if (dt.Rows[i]["CountDevice"].ToString() != "0")
                        {
                            myData.returnData += dt.Rows[i]["GroupInfo"].ToString() + ParseWording("B0067") + "\n";
                        }
                        else
                        {
                            myData.returnData += dt.Rows[i]["GroupInfo"].ToString() + ParseWording("B0057") + "\n";
                        }
                    }

                    /*去掉最後一個斷行符號*/
                    if (myData.returnData.Length != 0)
                        myData.returnData = myData.returnData.Substring(0, (myData.returnData.Length - 1));
                }
            }
            db.DBDisconnect();
        }
        return myData;
    }



}
