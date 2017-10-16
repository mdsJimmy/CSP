using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// BaseUserControl 的摘要描述
/// </summary>
public class BaseUserControl:System.Web.UI.UserControl
{
    public string FunctionID;
    public string FunctionName;
    public string ModuleName;
    //取得目前路徑,傳回根目錄相對位置包含"~"
    public string CurrentPage
    {
        get
        {
            if (Request.ApplicationPath == @"/")
            {
                return "~" + Request.ServerVariables.Get("Path_Info").ToString();
            }
            else
            {
                return Request.ServerVariables.Get("Path_Info").ToString().Replace(Request.ApplicationPath, "~");
            }
        }
    }
    protected override void OnLoad(EventArgs e)
    {
        if (this.Page is BasePage)
        {
            FunctionID = ((BasePage)this.Page).FunctionID;
            FunctionName = ((BasePage)this.Page).FunctionName;
            ModuleName = ((BasePage)this.Page).ModuleName;
        }
        base.OnLoad(e);
    }
}