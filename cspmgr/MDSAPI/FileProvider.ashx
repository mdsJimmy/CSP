<%@ WebHandler Language="C#" Class="FileProvider" %>

using System;
using System.Web;
using System.Text;
using System.Data;
using MDS.Database;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.IO;
using MIPLibrary;

public class FileProvider : IHttpHandler
{
    static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



    public void ProcessRequest (HttpContext context) {


        try
        {

            string index = context.Request.QueryString["file_index"];
            if (!MDS.Utility.NUtility.isInteger(index))
            {
                index = "0";
            }

            mip.dao.voMIP_FILE_STORE _voMIP_FILE_STORE = getFileFromDB(index);


            if (_voMIP_FILE_STORE.FILE_IMG == null || _voMIP_FILE_STORE.FILE_IMG.Equals(""))
            {

            }
            else
            {

                string fileName = MDS.Utility.NUtility.checkString(_voMIP_FILE_STORE.FILE_ORI_NAME);
                if (context.Request.Browser.Browser == "InternetExplorer"||context.Request.Browser.Browser == "IE") {
                    fileName = context.Server.UrlPathEncode(fileName);

                }



                context.Response.Clear();
                context.Response.Buffer = true;
                //X-Content-Type-Options: nosniff

                context.Response.AddHeader("X-Content-Type-Options", "nosniff");
                context.Response.AddHeader("content-disposition", String.Format("attachment;filename={0}",fileName));

                
                
                //context.Response.ContentType = "application/" + HttpUtility.UrlEncode(System.IO.Path.GetExtension(_voMIP_FILE_STORE.FILE_ORI_NAME).Substring(1));

                context.Response.ContentType = "image/jpeg";


                context.Response.Charset = Encoding.UTF8.WebName;
                context.Response.BinaryWrite(AES.MIPEncrypted.Decrypt(AES.MIPEncrypted.Encrypt(_voMIP_FILE_STORE.FILE_IMG)));
            }

            context.Response.Flush();
            context.Response.Close();
            //context.Response.End();

        }
        catch (System.Exception ex){

            Console.WriteLine(ex.Message);
            logger.Fatal(ex);

        }finally{
        }

    }



    private mip.dao.voMIP_FILE_STORE getFileFromDB(string key)
    {
        SqlConnection oConn = null;
        Database db = new Database();
        mip.dao.voMIP_FILE_STORE _voMIP_FILE_STORE = null;
        try
        {


            db.DBConnect();

            oConn = db.getOcnn();

            MIPLibrary.FileManager fp = new MIPLibrary.FileManager();


            _voMIP_FILE_STORE = fp.getFileByKey(key, oConn);
            //Console.WriteLine("_voMIP_FILE_STORE =" + _voMIP_FILE_STORE.FILE_NEW_NAME);
            //Console.WriteLine("_voMIP_FILE_STORE =" + _voMIP_FILE_STORE.FILE_ORI_NAME);
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nStackTrace ---\n{0}", ex.StackTrace);
        }
        finally
        {
            oConn.Close();
        }

        return _voMIP_FILE_STORE;
    }


    public bool IsReusable {
        get {
            return false;
        }
    }

}