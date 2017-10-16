using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MDSControl_BreadCrumbs : BaseUserControl
{
    public string SecureKey = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ContentLiteral.Text = LoadBreadCrumb(this.ModuleName,this.FunctionName);
        }
    }
    private string LoadBreadCrumb(string moduleTitle,string functionTitle)
    {
        SecureKey = Request.QueryString["SecureKey"];
        string mainPage = ResolveUrl("~/SysFun/MIPStart.aspx?SecureKey=" + System.Web.HttpUtility.HtmlEncode(SecureKey));
        return string.Format("<ul class=\"breadcrumb\"><li><i class=\"ace-icon fa fa-home home-icon\"></i><a href=\"{0}\">Home</a></li>{1}{2}</ul><!-- /.breadcrumb -->", mainPage,string.IsNullOrEmpty(moduleTitle)?"":WithLI(moduleTitle),string.IsNullOrEmpty(functionTitle)?"":WithLI(functionTitle));
    }
    private string WithLI(string str)
    {
        return string.Format("<li>{0}</li>", str);
    }
}