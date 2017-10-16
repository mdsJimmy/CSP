using System;
using System.Web.UI;
using System.Web;
using System.util;
using System.Resources;
using System.Reflection;

public partial class ToolBar : System.Web.UI.UserControl, System.Web.UI.WebControls.IButtonControl
{

    private string _ImgUrl = "";
    private string _Text = "";
    private string _OnClientClick = "";
    private bool _PostBack = true;
    private bool _Enabled = true;
    private string _PostBackUrl = "";

    
    public event System.EventHandler Click;
    
    /// <summary> 
    /// Function that called by button click which
    /// in turns fires the buttonclicked event 
    /// trapped by the client 
    /// </summary> 
    protected virtual void OnClick(object sender)
    {
        // Raise the tabclicked event. 
        if (this.Click != null)
            this.Click(sender, new EventArgs());
    }

    /// <summary>
    /// 圖檔(含完整路徑)
    /// </summary>
    public string ImgUrl
    {
        get { return _ImgUrl; }
        set { _ImgUrl = value; }
    }

    /// <summary>
    /// 按鈕的字
    /// </summary>
    public string Text
    {
        get { return _Text; }
        set { _Text = value; }
    }

    /// <summary>
    /// 按下按鈕要執行的Javascript function
    /// </summary>
    public string OnClientClick
    {
        get {
            if (_OnClientClick.Length > 0)
            {
                if (_OnClientClick.Substring(_OnClientClick.Length - 1, 1) == ";")
                    _OnClientClick.Substring(0, _OnClientClick.Length - 1);
            }
            return _OnClientClick; 
        }
        set { _OnClientClick = value; }
    }

    /// <summary>
    /// 是否PostBack
    /// </summary>
    /// <value>
    /// true or false
    /// </value>
    public bool PostBack
    {
        get { return _PostBack; }
        set { _PostBack = value; }
    }

    /// <summary>
    /// 是否Enabled
    /// </summary>
    /// <value>
    /// true:可以按
    /// false:不能按
    /// </value>
    public bool Enabled
    {
        get { return _Enabled; }
        set { _Enabled = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            if ((Page.Request.Form["__EVENTTARGET"] ?? "") == this.ID)
            {
                OnClick(e);
            }
        }
    }

    protected override void Render(HtmlTextWriter output)
    {
        output.AddAttribute("id", this.ID);
        
        if (PostBack == true)
        {
            // Create a new PostBackOptions object and set its properties.
            PostBackOptions myPostBackOptions = new PostBackOptions(this);
            myPostBackOptions.ActionUrl = this.PostBackUrl;
            myPostBackOptions.AutoPostBack = false;
            myPostBackOptions.RequiresJavaScriptProtocol = false;
            
            if (OnClientClick != "")
                OnClientClick = "if(" + OnClientClick + " == false){return false;};";

            output.AddAttribute("onclick", OnClientClick + Page.ClientScript.GetPostBackEventReference(myPostBackOptions));
        }
        else
        {
            output.AddAttribute("onclick", OnClientClick + ";return false;");
        }

        if(Enabled == false)
            output.AddAttribute("disabled", "true");


        //output.AddAttribute("title", Text);

        output.AddAttribute(HtmlTextWriterAttribute.Class, "btn btn-round btn-primary btn-white");
        output.RenderBeginTag("button");
        ResourceManager rm = new ResourceManager("Resources.DMSWording",
                        System.Reflection.Assembly.Load("App_GlobalResources"));

       // rm = new ResourceManager("App_GlobalResources.DMSWording", Assembly.GetEntryAssembly());
        //設定上方按鈕圖案的css fa
        if (Text == rm.GetString("A0001")) 
        {
            //新增
            output.AddAttribute(HtmlTextWriterAttribute.Class, "fa fa-plus fa-lg");
           // output.AddAttribute(HtmlTextWriterAttribute.Style,"display:none");
        }
        else if (Text == rm.GetString("A0002"))
        {
            //修改
            output.AddAttribute(HtmlTextWriterAttribute.Class, "fa fa-file-o fa-lg");
        }
        else if (Text == rm.GetString("A0003"))
        {
            //刪除
            output.AddAttribute(HtmlTextWriterAttribute.Class, "fa fa-trash-o fa-lg");
        }
        else if (Text == rm.GetString("A0004"))
        {
            //蒐尋
            output.AddAttribute(HtmlTextWriterAttribute.Class, "fa fa-search fa-lg");
        }
        else if (Text == rm.GetString("A0005"))
        {
            //儲存
            output.AddAttribute(HtmlTextWriterAttribute.Class, "fa fa-save fa-lg");
        }
        else if (Text == rm.GetString("A0006"))
        {
            //放棄
            output.AddAttribute(HtmlTextWriterAttribute.Class, "fa fa-undo fa-lg");
        }
        else if (Text == rm.GetString("A0026"))
        {
            //回上
            output.AddAttribute(HtmlTextWriterAttribute.Class, "fa fa-undo fa-lg");
        }
        else if (Text == "停用")
        {
            //stop
            output.AddAttribute(HtmlTextWriterAttribute.Class, "fa fa-stop fa-lg");
        }
        else if (Text == rm.GetString("B0003") || Text ==  "立即推播" )
        {
            //play
            output.AddAttribute(HtmlTextWriterAttribute.Class, "fa fa-play fa-lg");
        }
        else if (Text.IndexOf("匯出") > -1)
        {
            //stop
            output.AddAttribute(HtmlTextWriterAttribute.Class, "fa fa-book fa-lg");
        }
        else
        {
            //未指定
        }

        output.RenderBeginTag("i");
       
        output.WriteEndTag("i");
        output.Write("&nbsp;");
        output.Write(Text);
        output.WriteEndTag("button");
 
    }


    #region IButtonControl 成員

    public bool CausesValidation
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            throw new NotImplementedException();
        }
    }

    public event System.Web.UI.WebControls.CommandEventHandler Command;

    public string CommandArgument
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            throw new NotImplementedException();
        }
    }

    public string CommandName
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            throw new NotImplementedException();
        }
    }

    public string PostBackUrl
    {
        get { return _PostBackUrl;}
        set { _PostBackUrl = HttpUtility.UrlPathEncode(ResolveClientUrl(value)); }
    }

    public string ValidationGroup
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}
