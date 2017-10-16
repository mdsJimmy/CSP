using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MDS.Utility;
/// <summary>
/// 主頁面上方的使用者資訊(圖示與ID)
/// </summary>
public partial class MDSControl_UserLabel : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LiteralUserName.Text = Session["NAME"]==null ? "" : Session["NAME"].ToString();
        }
    }
}