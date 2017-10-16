
using System;
using System.IO;

using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;

public partial class SysFun_AppStore : BasePage
{
    protected string userID = "";
    protected string appname = "";
    protected string s = "";
    protected string s1 = "";
    protected string s2 = "";
    static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected CReturnData myData = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        postSession();
       

        ////s = Request.Url.Scheme + "://" + Request.Url.Authority + "/" + "AppStore/" + "applogin.ashx?app=" + Request.ApplicationPath;
        

       
        //logger.Debug("appstore 開工");

        ////string s = context.Request.Url.Scheme + "://" + context.Request.Url.Authority + context.Request.QueryString["app"] + "/" +"SysFun/"+"AppStore.ashx" ;
        //string s1 = Request.Url.Scheme + "://" + Request.Url.Authority + "/" + "AppStore/" + "SysFun/" + "AppManage/" + "App_List.aspx?app=" + Request.ApplicationPath;
        //string s2 = Request.Url.Scheme + "://" + Request.Url.Authority + "/" + "AppStore/" + "applogin2.aspx?app=" + Request.ApplicationPath;
        
       


        //WebRequest request = WebRequest.Create(s2);
        //request.Method = "POST";
        //request.ContentType = "application/json";

        
        //Response.ContentType = "application/json";
        //Response.Write("");
        

        ////加上"data="，讓server端可以透過Request.Form["data"]讀取
        //string postData = "data=" + Session["UserID"];
        //logger.Debug("我有沒有資料:" + postData);
        //byte[] byteArray = Encoding.UTF8.GetBytes(postData);



        //// Set the ContentLength property of the WebRequest.
        //request.ContentLength = byteArray.Length;

        //// Get the request stream.
        //using (Stream dataStream = request.GetRequestStream())
        //{
        //    // Write the data to the request stream.
        //    dataStream.Write(byteArray, 0, byteArray.Length);
        //}

        //// Get the response.      
        //using (WebResponse response = request.GetResponse())
        //{
        //    using (Stream dataStream = response.GetResponseStream())
        //    {
        //        using (StreamReader reader = new StreamReader(dataStream))
        //        {
        //            string responseFromServer = reader.ReadToEnd();
        //            string sJavascript = "$('#frmMain').attr(\"src\",\"" + s2 + "\");";
        //            Response.Charset = "utf-8";
        //            logger.Debug("sJavascript：" + sJavascript);

        //            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), sJavascript, true);


        //        }
        //    }
        //}
    }

    protected void postSession()
    {
        string url = Request.Url.Scheme + "://" + Request.Url.Host + Request.ApplicationPath+ "/SysFun/" + "AppStore.ashx";
        myData = new CReturnData();
        //string uri = string.Format("{0}?who={1}", url, "joey");
        WebRequest request = WebRequest.Create(url);
        
        request.Method = "POST";
        

        //加上"data="，讓server端可以透過Request.Form["data"]讀取
        string postData = Session["UserID"].ToString();

        byte[] byteArray = Encoding.UTF8.GetBytes(postData);

        request.ContentType = "application/x-www-form-urlencoded";

        // Set the ContentLength property of the WebRequest.
        request.ContentLength = byteArray.Length;

        try
        {
            logger.Debug("請求網站："+ url);
            myData.nRet = 0;
            using (Stream dataStream = request.GetRequestStream())
            {
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            // Get the response. 
            logger.Debug("等待回應：" + request.GetResponse());
            using (WebResponse response = request.GetResponse())
            {

                using (Stream dataStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(dataStream))
                    {
                        string responseFromServer = reader.ReadToEnd();

                        s1 = Request.Url.Scheme + "://" + Request.Url.Authority +"/" + System.Configuration.ConfigurationManager.AppSettings["APPLICATION"] + "/SysFun/" + "AppManage/" + "App_List.aspx?app=" + Request.ApplicationPath + "&t=" + responseFromServer;
                        logger.Debug("回應的網址(appstore)：" + s1);

                    }
                }
            }
        }
        catch (Exception ex)
        {
            myData.nRet = 1;
            myData.outMsg = ex.Message;
            MessageBox("錯誤訊息:" + ex.Message + "請稍候在試");
            //throw ex;
        }

        // Get the request stream.
      

    }
}