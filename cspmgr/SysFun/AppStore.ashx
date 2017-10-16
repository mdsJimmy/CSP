<%@ WebHandler Language="C#" Class="AppStore" %>

using System.IO;
using System.Web;
using System.Net;
using System.Text;

public class AppStore : IHttpHandler {
    static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        //Get QueryString
        string whom = context.Request.QueryString["who"];

        //取得整個Request傳過來的InputStream資料
        //string result = GetFromInputStream(context);

        //取得整個form資料
        string result = context.Request.Form.ToString();
       
        //HttpContext.Current.Session["UserID"] = result;
        string token = MIPLibrary.TokenGenerator.GenerateToken("123","456", result);
            logger.Debug("result:"+result);
            logger.Debug("Session(服務ashx):"+token);
           
        
        
         context.Response.Write(token);
    }

    private static string GetFromInputStream(HttpContext context)
    {
        StreamReader reader = new StreamReader(context.Request.InputStream);
        string result = reader.ReadToEnd();

        return result;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}