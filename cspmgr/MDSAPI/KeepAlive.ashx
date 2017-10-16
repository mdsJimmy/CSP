<%@ WebHandler Language="C#" Class="KeepAlive" %>

using System;
using System.Web;
using System.Web.SessionState;
using MDS.Bastogne.Objs;
 

public class KeepAlive : IHttpHandler, IRequiresSessionState
{
    static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



    ReturnBaseObj returnInfo = new ReturnBaseObj();
    public void ProcessRequest (HttpContext context) {

     //logger.Debug("KeepAlive ProcessRequest");


        if (context.Session["UserID"] == null)
        {
            returnInfo.setValues("-2", "001", "網頁逾時(session_timeout)");

            logger.Debug("網頁逾時(session_timeout)");
        }
        else
        {
            //logger.Debug("sesion OK");

            context.Session["KeepSessionAlive"] = DateTime.Now;
            returnInfo.setValues("0", "000", "KeepAlive");
        }
        fastJSON.JSONParameters para = new fastJSON.JSONParameters();
        para.UseEscapedUnicode = false;
        para.UseExtensions = false;
        context.Response.ContentType = "application/json";
        context.Response.Write(fastJSON.JSON.ToNiceJSON(returnInfo, para));
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}