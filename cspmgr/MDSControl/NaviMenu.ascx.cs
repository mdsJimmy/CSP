using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using MDS.Utility;
using MDS.Database;
using System.Data;
/// <summary>
/// 主選單
/// </summary>
public partial class MDSControl_NaviMenu : BaseUserControl
{
    //static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    List<MenuItem> menuItems;

    public class MenuItem
    {
        public string ID
        {
            get;
            private set;
        }
        public string Title
        {
            get;
            private set;
        }
        public string Url
        {
            get;
            private set;
        }
        public string Icon
        {
            get;
            private set;
        }
        public bool Active
        {
            get;
            set;
        }
        public bool Enable
        {
            get;
            set;
        }
        public List<MenuItem> SubItems;
        public MenuItem(string id,string title, string url, string icon,bool active)
        {
            this.ID = id;
            this.Title = title;
            this.Url = url;
            this.Icon = icon;
            this.Active = active;
            this.Enable = false;
        }
        public void AddSubItem(string id,string title,string url,bool active)
        {
            if (this.SubItems == null)
            {
                this.SubItems = new List<MenuItem>();
            }
            string addIdentifyInfo = "_actionType=fromMenu";
            if (url.IndexOf("?") == -1)
            {
                addIdentifyInfo = "?" + addIdentifyInfo;
            }
            else {
                addIdentifyInfo = "&" + addIdentifyInfo;
            }
            this.SubItems.Add(new MenuItem(id, title, url + addIdentifyInfo, "", active));
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            menuItems = new List<MenuItem>();

            if (Session["UserID"] != null)
            {
                LoadDBUser(this.CurrentPage, Session["UserID"].ToString());
            }

           
            
            ContentLiteral.Text = LoadMenu();
        }
    }

   
    private void LoadDBUser(string currentPage,string usersn)
    {
        //using (BastogneModel.BastogneEntities DB = new BastogneModel.BastogneEntities())
        //{
        //    foreach (var module in from m in DB.sys_module orderby m.sort_number select m)
        //    {
        //        MenuItem item = new MenuItem(module.C_id, module.name, "", module.icon, false);
        //        menuItems.Add(item);
        //    }
        //    foreach (MenuItem item in menuItems)
        //    {
        //        foreach (var function in (from r in DB.sys_security_user_role
        //                                 where r.user_sn == usersn
        //                                 join p in DB.sys_role_permission on new { r.root_id, r.role_id } equals new { p.root_id, p.role_id } into rolePermission
        //                                 from p in rolePermission
        //                                 where p.enabled == "Y" && (p.act_id == "V" || p.act_id=="E")
        //                                 join f in DB.sys_function on p.fun_id equals f.C_id
        //                                 where (f.mod_id == item.ID)
        //                                  select f).Distinct().OrderBy(x=>x.sort_number))
        //        {
        //            item.Enable = true;
        //            bool selected = !string.IsNullOrEmpty(function.url) && ResolveUrl(function.url) == currentPage;
        //            if (selected)
        //            {
        //                item.Active = true;
        //            }
        //            item.AddSubItem(function.C_id, function.name, function.url, selected);
        //        }
        //    }
        //}

                Database db = new Database();
        DataTable dt = new DataTable();

        int nRet = -1;
        string outMsg = "";

        string strSQL = "";

        //string output = "<ul class='nav nav-list' >";
        string output = "";

        string AccountID = Session["UserID"].ToString();

        int iModuleCount = 0;
        

        nRet = db.DBConnect();
        if (nRet == 0)
        {
            strSQL = "DECLARE @tmpSysModID varchar(50) /*暫存ModID用來判斷用*/ " +
                "DECLARE @SysModID varchar(50) " +
                "DECLARE @ModuleDesc nvarchar(50) " +
                "DECLARE @SysFuncID varchar(50) " +
                "DECLARE @FunctionDesc nvarchar(100) " +
                "DECLARE @SysModPic varchar(50) " +
                "DECLARE @SysFuncPic varchar(50) " +
                "DECLARE @SysModPageLink varchar(100) " +
                "DECLARE @SysFuncPageLink varchar(100) " +
                "DECLARE @isDefault int " +
                "DECLARE @tmpTable TABLE( " +
                    "thisType int, /*1: ModID 2:FuncID 3:ActionID */ " +
                    "SysModID varchar(50), " +
                    "SysFuncID varchar(50), " +
                    "ModuleDesc nvarchar(100), " +
                    "FunctionDesc nvarchar(100), " +
                    "Pic varchar(50), " +
                    "PageLink varchar(100), " +
                    "isDefault int " +
                ") " +
                "SET @tmpSysModID = '' " +
                "DECLARE CurTemp CURSOR FOR " +
                            @"SELECT tblA.SysModID, SystemModule.ModuleDesc, tblA.SysFuncID, SystemFunction.FunctionDesc, SystemModule.Pic, SystemFunction.Pic, SystemModule.PageLink, SystemFunction.PageLink, SystemModule.isDefault 
                               FROM SecurityUserAccount_FunctionRole AS tblA  
                               INNER JOIN SystemModule ON tblA.SysModID = SystemModule.SysModID AND SystemModule.iDisplay = 1  
                               INNER JOIN SystemFunction ON tblA.SysFuncID = SystemFunction.SysFuncID AND SystemFunction.iDisplay = 1 
                   INNER JOIN SecurityUserAccount on tbla.AccountID=SecurityUserAccount.AccountID 
                               WHERE tblA.AccountID = '" + AccountID +@"' AND SecurityUserAccount.Startup=1
                               ORDER BY SystemModule.iOrder, SystemModule.SysModID, SystemFunction.iOrder  " +
                //"SELECT tblA.SysModID, SystemModule.ModuleDesc, tblA.SysFuncID, SystemFunction.FunctionDesc, SystemModule.Pic, SystemFunction.Pic, SystemModule.PageLink, SystemFunction.PageLink, SystemModule.isDefault " +
                //"FROM SecurityUserAccount_FunctionRole AS tblA " +
                //"INNER JOIN SystemModule ON tblA.SysModID = SystemModule.SysModID AND SystemModule.iDisplay = 1 " +
                //"INNER JOIN SystemFunction ON tblA.SysFuncID = SystemFunction.SysFuncID AND SystemFunction.iDisplay = 1 " +
                //"WHERE tblA.AccountID = '" + AccountID + "'" +
                //"ORDER BY SystemModule.iOrder, SystemModule.SysModID, SystemFunction.iOrder " +
                "OPEN CurTemp " +
                "FETCH NEXT FROM CurTemp INTO @SysModID, @ModuleDesc, @SysFuncID, @FunctionDesc, @SysModPic, @SysFuncPic, @SysModPageLink, @SysFuncPageLink, @isDefault " +
                "WHILE (@@Fetch_Status = 0 ) " +
                "BEGIN " +
                    "/*第一次取得SysModID*/ " +
                    "IF @tmpSysModID <> @SysModID BEGIN " +
                        "SET @tmpSysModID = @SysModID " +
                        "INSERT INTO @tmpTable(thisType, SysModID, ModuleDesc, Pic, PageLink, isDefault) VALUES(1, @SysModID, @ModuleDesc, @SysModPic, @SysModPageLink, @isDefault) " +
                    "END " +
                    "/*取得這筆SysFuncID*/ " +
                    "INSERT INTO @tmpTable(thisType, SysModID, SysFuncID, ModuleDesc, FunctionDesc, Pic, PageLink) VALUES(2, @SysModID, @SysFuncID, @ModuleDesc, @FunctionDesc, @SysFuncPic, @SysFuncPageLink) " +
                "FETCH NEXT FROM CurTemp INTO @SysModID, @ModuleDesc, @SysFuncID, @FunctionDesc, @SysModPic, @SysFuncPic, @SysModPageLink, @SysFuncPageLink, @isDefault " +
                "END " +
                "CLOSE CurTemp " +
                "DEALLOCATE CurTemp " +

                "/*輸出結果*/ " +
                "SELECT thisType, SysModID, SysFuncID, ModuleDesc, FunctionDesc, Pic, PageLink, isDefault FROM @tmpTable sv";
            //如果 Session["CHANGE_PASSWORD"] =1 則需要使用者變更密碼,所以只撈出變更密碼的功能
            if (Session["CHANGE_PASSWORD"].ToString() == "1")
            {
                strSQL += " where sv.SysModID='14A9929B-B17B-43D8-9411-6FD28F47007C'";
            }

            System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(strSQL, db.getOcnn());
            nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);

            MenuItem item = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                switch (dt.Rows[i]["thisType"].ToString())
                {
                    //Module
                    case "1":
                       

                        item = new MenuItem(dt.Rows[i]["SysModID"].ToString(), dt.Rows[i]["ModuleDesc"].ToString(), "", dt.Rows[i]["Pic"].ToString(), false);
                        menuItems.Add(item);

                        break;
                    //Function
                    case "2":

                        if (item != null)
                        {
                            item.Enable = true;
                            if (this.ModuleName == this.FunctionName)
                            {
                            }


                            bool selected =
                                !string.IsNullOrEmpty(dt.Rows[i]["PageLink"].ToString())
                                && ResolveUrl(dt.Rows[i]["PageLink"].ToString()) == ResolveUrl(currentPage);
                            if (selected || this.FunctionName == dt.Rows[i]["FunctionDesc"].ToString())
                            {
                                item.Active = true;
                            }
                            else
                            {

                            }

                            item.AddSubItem(dt.Rows[i]["SysFuncID"].ToString(), dt.Rows[i]["FunctionDesc"].ToString(), dt.Rows[i]["PageLink"].ToString(), selected);
                


                        }

        
                        break;
                    default:
                        break;
                }
            }
        }
    }

    
 
    private string LoadMenu()
    {
        StringBuilder sb = new StringBuilder();
        foreach (MenuItem item in menuItems)
        {
            if (item.Enable)
            {
                sb.Append(CreateModuleHtml(item.Title, item.Url, item.Icon, item.SubItems, item.Active));
            }
        }
        return sb.ToString();
    }

    private string CreateModuleHtml(string ModuleName,string Url,string Icon,List<MenuItem>subItems,bool active)
    {
        bool hasSubMenu = false;
        StringBuilder sb = new StringBuilder();
        if (subItems!=null && subItems.Count>0)
        {
            hasSubMenu = true;
            sb.Append("<b class=\"arrow\"></b><ul class=\"submenu\">");
            foreach (MenuItem item in subItems)
            {
                sb.Append(CreateSubMenuItem(item.Title, item.Url,item.Active));
            }
            sb.Append("</ul>");
        }
        return string.Format(
            "<li class=\"{6}\"><a href=\"{1}\" {5} ><i class=\"menu-icon fa {4}\"></i><span class=\"menu-text\"> {0} </span>{2}</a>{3}"
            , ModuleName, Url.Length > 0 ? ResolveUrl(Url) : "#"
            , hasSubMenu ? "<b class=\"arrow fa fa-angle-down\"></b>" : ""
            , sb.ToString(), Icon,Url.Length > 0?"":"class=\"dropdown-toggle\"",active?"open":"");
    }
    private string CreateSubMenuItem(string FunctionName, string Url,bool active)
    {
        if (FunctionName == this.FunctionName)
        {
            active = true;
        }
        return string.Format(
            "<li class=\"{2}\"><a href=\"{1}\"><i class=\"menu-icon fa fa-caret-right\"></i>{0}</a><b class=\"arrow\"></b></li>"
            , FunctionName, string.IsNullOrEmpty(Url)?"#":ResolveUrl(Url)
            , string.IsNullOrEmpty(Url)?"disabled":(active?"active":""));
    }

}