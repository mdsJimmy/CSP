using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DMSMainManage_UserInfo_UserInfo_Main : BasePage
{
    string myGroupID = "";
    protected string TargerGroupID = "";
    protected string PageNo = "0";
    protected string mySearch = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        /*取得使用者GroupID*/
        myGroupID = Session["ParentGroupID"].ToString(); 

        /*接收Request*/
        if (!string.IsNullOrEmpty(Request.QueryString["TargerGroupID"]))
            TargerGroupID = Request.QueryString["TargerGroupID"];
        else
            TargerGroupID = myGroupID;
        if (!string.IsNullOrEmpty(Request.QueryString["PageNo"]))
            PageNo = Request.QueryString["PageNo"];
        if (!string.IsNullOrEmpty(Request.QueryString["StrSearch"]))
            mySearch = Request.QueryString["StrSearch"];

    }
}
