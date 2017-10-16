<%@ WebHandler Language="C#" Class="MIPSE_FileProvider" %>

using System;
using System.Web;

using System.Data;
using MDS.Database;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.IO;
using MIPLibrary;

public class MIPSE_FileProvider : IHttpHandler
{
    static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    
    public void ProcessRequest (HttpContext context) {
 
            
        try
        {

            mip.dao.voMIP_FILE_STORE _voMIP_FILE_STORE = getFileFromDB(context.Request.QueryString["FILE_INDEX"]);

            logger.Info(_voMIP_FILE_STORE.FILE_ORI_NAME + "FILE_INDEX="  + _voMIP_FILE_STORE.FILE_INDEX);

            if (_voMIP_FILE_STORE.FILE_IMG == null || _voMIP_FILE_STORE.FILE_IMG.Equals(""))
            {

            }
            else
            {
                context.Response.Clear();
                context.Response.Buffer = true;
                context.Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", HttpUtility.UrlEncode(MDS.Utility.NUtility.checkString(_voMIP_FILE_STORE.FILE_ORI_NAME))));
               context.Response.ContentType = "application/" + System.IO.Path.GetExtension(_voMIP_FILE_STORE.FILE_ORI_NAME).Substring(1);

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

             MIPLibrary.SEFileManager fp = new MIPLibrary.SEFileManager();

         
             _voMIP_FILE_STORE = fp.getFileByKey(key, oConn);
             Console.WriteLine("_voMIP_FILE_STORE =" + _voMIP_FILE_STORE.FILE_NEW_NAME);
             Console.WriteLine("_voMIP_FILE_STORE =" + _voMIP_FILE_STORE.FILE_ORI_NAME);
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