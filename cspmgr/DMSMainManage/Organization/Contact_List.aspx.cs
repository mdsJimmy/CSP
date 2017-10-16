using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MDS.Database;

public partial class DMSMainManage_Organization_Contact_List : BasePage
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
            mySearch = Request.QueryString["StrSearch"];
        if (ContactList == null)
        {
            ContentPlaceHolder MySecondContent = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
            ContactList = MySecondContent.FindControl("ContactList") as ASP.dmscontrol_olistview_ascx;
            StrSearch = MySecondContent.FindControl("StrSearch") as System.Web.UI.HtmlControls.HtmlInputText;
        }

        if (StrSearch != null) {
            //StrSearch接Request;
            StrSearch.Value = mySearch;
        }

        if (ContactList != null) {

            //加入欄位Start
            ContactList.AddCol(ParseWording("B0012"), "GroupInfo", "LEFT");
            ContactList.AddCol(ParseWording("B0008"), "Title", "CENTER");
            ContactList.AddCol(ParseWording("B0036"), "ContactName", "CENTER");
            ContactList.AddCol(ParseWording("B0065"), "ContactData", "LEFT");
            //加入欄位End

            //設定Key值欄位
            ContactList.DataKeyNames = "GroupID,ContactID"; //Key以,隔開

            //設定是否顯示CheckBox(預設是true);
            if (CheckUserRight("4914DFD1-D102-4A2F-A27E-7A501E71D18E") == false)
            {
                ContactList.IsUseCheckBox = false;
            }

            //設定SQL
            strSQL = "SELECT SecurityGroup.GroupID " +
                    ", SecurityGroup.GroupID + ' ' + SecurityGroup.GroupName AS GroupInfo " +
                    ", tblA.ContactID " +
                    ", tblA.ContactName " +
                    ", tblA.Title " +
                    ", ( " +
                        "(CASE WHEN ISNULL(tblA.Tel1, '') <> '' THEN N'" + ParseWording("A0019") + "(1):' + tblA.Tel1 + '<BR>' ELSE '' END) " +
                        "+ (CASE WHEN ISNULL(tblA.Tel2, '') <> '' THEN N'" + ParseWording("A0019") + "(2):' + tblA.Tel2 + '<BR>' ELSE '' END) " +
                        "+ (CASE WHEN ISNULL(tblA.Tel3, '') <> '' THEN N'" + ParseWording("A0019") + "(3):' + tblA.Tel3 + '<BR>' ELSE '' END) " +
                        "+ (CASE WHEN ISNULL(tblA.EMail, '') <> '' THEN N'EMail:' + tblA.EMail ELSE '' END) " +
                    ") AS ContactData " +
                "FROM SecurityGroup_Contact AS tblA " +
                "INNER JOIN SecurityGroup_ContactRelation ON tblA.ContactID = SecurityGroup_ContactRelation.ContactID " +
                "INNER JOIN SecurityGroup ON SecurityGroup_ContactRelation.GroupID = SecurityGroup.GroupID ";

            if (mySearch == "")
            {
                strSQL += "INNER JOIN dbo.fn_GetGroupTree(@TargerGroupID) AS tblT ON SecurityGroup.GroupID = tblT.GroupID ";
                ContactList.putQueryParameter("TargerGroupID", TargerGroupID);
            }
            else
            {
                strSQL += "INNER JOIN dbo.fn_GetGroupTree(@ParentGroupID) AS tblT ON SecurityGroup.GroupID = tblT.GroupID "
                    + "WHERE tblA.ContactName LIKE '%'+@mySearch+'%' ";

                ContactList.putQueryParameter("ParentGroupID", Session["ParentGroupID"].ToString());
                ContactList.putQueryParameter("mySearch", mySearch.Replace("'", "''"));

            }
            strSQL += "ORDER BY SecurityGroup.GroupID, tblA.ContactName ";


            //取得SQL;
            ContactList.SelectString = strSQL;
            ContactList.prepareStatement();

            //設定每筆資料按下去的Javascript function
            ContactList.OnClickExecFunc = "DoEdt()";

            //設定每頁筆數
            ContactList.PageSize = 10;

            //接來自Request的排序欄位、排序方向、目前頁數
            ListViewSortKey = Request.Params["ListViewSortKey"];
            ListViewSortDirection = Request.Params["ListViewSortDirection"];
            PageNo = Request.Params["PageNo"];

            //設定排序欄位及方向
            if (!string.IsNullOrEmpty(ListViewSortKey) && !string.IsNullOrEmpty(ListViewSortDirection))
            {
                ContactList.ListViewSortKey = ListViewSortKey;
                ContactList.ListViewSortDirection = (SortDirection)Enum.Parse(typeof(SortDirection), ListViewSortDirection);
            }

            //設定目前頁數
            if (!string.IsNullOrEmpty(PageNo))
                ContactList.PageNo = int.Parse(PageNo);
        }
        

    }


    protected void Page_Load(object sender, EventArgs e)
    {
        /*連線DB*/
        nRet = db.DBConnect();
        if (nRet == 0)
        {
            string SiteMap_SQL = "DECLARE @Sitemap nvarchar(4000) " +
                "SET @Sitemap = '' " +
                "/*取得Sitmap*/ " +
                "SELECT @Sitemap = @Sitemap + SecurityGroup.GroupName + '/' " +
                "FROM dbo.UTILfn_Split( " +
                    "(SELECT GroupSearchKey + '/' + GroupID FROM dbo.fn_GetGroupTree(@myGroupID_1) WHERE GroupID =@TargerGroupID), " +
                    "'/') AS tblA " +
                "INNER JOIN SecurityGroup ON tblA.[Value] = SecurityGroup.GroupID " +
                "WHERE tblA.[Value] <> ( " +
                    "SELECT ParentGroupID FROM SecurityRelation WHERE AccountID =@myGroupID_2" +
                ") " +
                "SELECT @Sitemap AS Sitemap ";

            /*查詢DB*/
            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(SiteMap_SQL, db.getOcnn());
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@myGroupID_1", myGroupID));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TargerGroupID", MDS.Utility.NUtility.checkString(TargerGroupID)));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@myGroupID_2", myGroupID));

            nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);



            if (nRet == 0)
            {
                if (dt.Rows.Count > 0)
                    SiteMapAddress = dt.Rows[0]["Sitemap"].ToString();
            }

            db.DBDisconnect();
        }

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

        //權限確認;
        if (CheckUserRight("2673D565-E0FA-48F4-B046-FBDFBAA76A6B") == false)
        {
            btnAdd.Visible = false;
        }
        if (CheckUserRight("4914DFD1-D102-4A2F-A27E-7A501E71D18E") == false)
        {
            btnDel.Visible = false;
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
            ListViewSortKey = ContactList.ListViewSortKey;
            ListViewSortDirection = ContactList.ListViewSortDirection.ToString();
            PageNo = ContactList.PageNo.ToString();
        }
    }


    /// <summary>
    /// 刪除聯絡人(AJAX)
    /// </summary>
    /// <param name="ContactIDList">聯絡人清單(GroupID1##ContactID1^^GroupID2##ContactID2^^GroupID3##ContactID3...)</param>
    /// <param name="myAccountName">操作人員姓名</param>
    /// <param name="myAccountID">操作人員帳號</param>
    /// <returns>nRet 0:刪除成功 -1, -2:失敗</returns>
    [System.Web.Services.WebMethod]
    public static CReturnData Delete_ContactID(string ContactIDList, string myAccountName, string myAccountID)
    {
        CReturnData myData = new CReturnData();
        Database db = new Database();
        DataTable dt = new DataTable();

        string ModuleDesc = ParseWording("B0136");
        string FunctionDesc = ParseWording("B0139");
        string ActionDesc = ParseWording("B0151");

        /*產生刪除帳號的SQL*/
        string StrSQL = "/*先取得要刪除的資料*/ " +
            "DECLARE @tmpTable TABLE(myGUID varchar(50), GroupInfo nvarchar(110), ContactName nvarchar(30)) " +
            "INSERT INTO @tmpTable " +
	            "SELECT newid(), tblA.GroupID + ' ' + SecurityGroup.GroupName, SecurityGroup_Contact.ContactName " +
	            "FROM SecurityGroup_ContactRelation AS tblA " +
	            "INNER JOIN SecurityGroup ON tblA.GroupID = SecurityGroup.GroupID " +
	            "INNER JOIN SecurityGroup_Contact ON tblA.ContactID = SecurityGroup_Contact.ContactID " +
	            "WHERE tblA.GroupID + '##' + CONVERT(varchar, tblA.ContactID) IN ( " +
                    "SELECT [Value] FROM dbo.UTILfn_Split(@ContactIDList, '^^') " +
	            ") " +
            "/*群組與聯絡人關連表*/ " +
            "DELETE FROM SecurityGroup_ContactRelation " +
            "WHERE GroupID + '##' + CONVERT(varchar, ContactID) IN ( " +
                "SELECT [Value] FROM dbo.UTILfn_Split(@ContactIDList2, '^^') " +
            ") " +
            "/*聯絡人資料(補刪除SecurityGroup_ContactRelation筆數為0的資料即可)*/ " +
            "DELETE FROM SecurityGroup_Contact WHERE (SELECT COUNT(*) FROM SecurityGroup_ContactRelation WHERE ContactID = SecurityGroup_Contact.ContactID) = 0 " +

            "/*操作紀錄-刪除*/" +
            "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                "SELECT myGUID, @ModuleDesc1, @FunctionDesc1, @ActionDesc1, @B00122, GroupInfo, '', @myAccountName1, @myAccountID1, GETDATE() FROM @tmpTable " +
            "INSERT INTO SecurityUserAccount_OperateRecord(RecordID, ModuleDesc, FunctionDesc, ActionDesc, FieldDesc, FieldBefore, FieldAfter, Name, AccountID, cDatetime) " +
                "SELECT myGUID, @ModuleDesc2, @FunctionDesc2, @ActionDesc2, @B0036, ContactName, '', @myAccountName2, @myAccountID12, GETDATE() FROM @tmpTable ";
                

        /*連線DB*/
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;
        if (myData.nRet == 0)
        {



            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(StrSQL, db.getOcnn());
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ContactIDList", ContactIDList));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ContactIDList2", ContactIDList));

            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModuleDesc1", ModuleDesc));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FunctionDesc1", FunctionDesc));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ActionDesc1", ActionDesc));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@B00122", ParseWording("B0012")));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@myAccountName1", myAccountName));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@myAccountID1", myAccountID));

            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModuleDesc2", ModuleDesc));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FunctionDesc2", FunctionDesc));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ActionDesc2", ActionDesc));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@B0036", ParseWording("B0036")));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@myAccountName2", myAccountName));
            SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@myAccountID12", myAccountID));

 

    
            
            myData.outMsg = db.outMsg;
            if (myData.nRet == 0)
            {
                /*寫入DB*/
                //db.BeginTranscation();
                //db.AddDmsSqlCmd(StrSQL);
               // db.CommitTranscation();

                myData.nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);

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
