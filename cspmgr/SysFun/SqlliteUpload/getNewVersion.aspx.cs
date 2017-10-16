using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Data;
using MDS.Database;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.IO;

public partial class SysFun_check_sqllite : System.Web.UI.Page
{
    static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public static string deviceid = ""; //設備的UDID
    public static string phonetype = ""; //iPhone, Android..etc.    
    public static string sqltype = ""; //檔案類別 1:體檢醫院 2:險種資料..etc
    public static string filename = ""; //檔名;
    public static string version = ""; //版本
    public static string oldfilename = ""; //原本的檔名;

    protected void Page_Load(object sender, EventArgs e)
    {

        /*接收Request*/
        if (!string.IsNullOrEmpty(Request.QueryString["deviceid"]))
            deviceid = Request.QueryString["deviceid"];
        if (!string.IsNullOrEmpty(Request.QueryString["phonetype"]))
            phonetype = Request.QueryString["phonetype"];
        if (!string.IsNullOrEmpty(Request.QueryString["sqltype"]))
            sqltype = Request.QueryString["sqltype"];
        if (!string.IsNullOrEmpty(Request.QueryString["filename"]))
            filename = Request.QueryString["filename"];
        if (!string.IsNullOrEmpty(Request.QueryString["version"]))
            version = Request.QueryString["version"];
        if (!string.IsNullOrEmpty(Request.QueryString["oldfilename"]))
            oldfilename = MDS.Utility.NUtility.trimBad(Request.QueryString["oldfilename"]);

        GetFile(MDS.Utility.NUtility.trimBad(filename), MDS.Utility.NUtility.trimBad(sqltype), MDS.Utility.NUtility.trimBad(oldfilename));
        
        //非後台網頁手動下載的，才寫Log
        if (oldfilename == "")
            DownLoadLog(deviceid, phonetype, sqltype, filename, version);
    }


    /// <summary>
    /// 下載檔案
    /// </summary>
    /// <param name="DownloadFileName">下載檔案名稱</param>
    /// <param name="sqltype">檔案類別(1:體檢醫院 2:險種資料...etc)</param>
    /// <param name="oldfilename">原本的檔名</param>
    public void GetFile(string DownloadFileName, string sqltype, string oldfilename)
    {     
        System.IO.Stream iStream = null;
        
        //// 制定文件路徑.
        byte[] buffer = new Byte[10000]; //以10K為單位暫存:
        long dataToRead; 
        Database db = new Database();
       // DataTable dt = new DataTable();

        string sqltype_filename = "";
        string filename = "";
        

         //Initialize SqlCommand object for select.
       // string StrSQL = "Select a.ImageData,b.filename from userUploadLog a , SQLITE_CODE b where a.sqltype=b.sqltype and a.fileUploadNewName = '" + DownloadFileName + "' and a.version = '" + version + "'";
        string StrSQL = "Select a.ImageData,b.filename from userUploadLog a , SQLITE_CODE b where a.sqltype=b.sqltype and a.version =@version";
      
        //Response.Write("getNewVersion StrSQL :" + StrSQL);
       
            db.DBConnect();

            SqlCommand SqlCom = new SqlCommand(StrSQL, db.getOcnn());
            SqlCom.Parameters.Add("@version", MDS.Utility.NUtility.checkString(version));
           
            byte[] results  =   new byte[0];

            SqlDataReader reader = SqlCom.ExecuteReader(CommandBehavior.CloseConnection);
            if (reader.Read())
            {
                  SqlBytes bytes = reader.GetSqlBytes(0);
                  //取得ImageData & filename field value
                  results = bytes.Buffer;
                  sqltype_filename = reader.GetSqlString(1).ToString();

            


        }
            SqlCom.Dispose();
            SqlCom = null;
            reader.Close();
            reader.Dispose();
            db.getOcnn().Close();



            // 從資料庫取得SQLITE_CODE.filename fileld END===============
            try
            {
                // 20111005-del 
                //打開文件.
                // iStream = new System.IO.b.FileStream("c:/temp/sssaferwtfas5.abc", System.IO.FileMode.Open,
               //             System.IO.FileAccess.Read, System.IO.FileShare.Read);


               // iStream = new System.IO.Stream();

                // 得到文件檔案長度:
                dataToRead = results.Length;

                Response.ContentType = "application/pdf;application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                //Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename));
                //依不同的檔案類型, 下載寫回不同的檔名
                //先防呆 或 後台進行網頁手動下載，下載成原始檔名 .
                if (sqltype_filename == "")
                {
                    //後台進行網頁手動下載，下載成原始檔名 .
                    if (oldfilename != "")
                    {
                        sqltype_filename = oldfilename;
                    }
                    //未知或未輸入sqltype, 防呆
                    else
                    {
                        sqltype_filename = filename;
                    }
                }


            Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(checkFileName(MDS.Utility.NUtility.checkString(sqltype_filename))));
                logger.Debug(HttpUtility.UrlEncode(checkFileName(MDS.Utility.NUtility.checkString(sqltype_filename))));
                    //保証client連接
                    if (Response.IsClientConnected)
                    {
                       // length = iStream.Read(buffer, 0, 10000);
                        /* 20120614 add Content-Length info. */
                        Response.AddHeader("Conthent-Length", results.Length.ToString());
                        Response.OutputStream.Write(AES.MIPEncrypted.Decrypt(AES.MIPEncrypted.Encrypt(results)), 0, results.Length);
                        Response.Flush();

                        buffer = new Byte[10000];
                        dataToRead = dataToRead - results.Length;
                    }
                    else
                    {
                        //结束循環
                        dataToRead = -1;
                    }
            }
            catch (Exception ex)
            {
                // error
                Response.Write("Error : " + ex.Message);
            }
            finally
            {
                if (iStream != null)
                {
                    //關閉文件
                    iStream.Close();
                }
            }
            reader.Close();
            reader = null;
    }


    private string checkFileName(string oriFileName)
    {
        string newFileName = "";
        if(oriFileName.IndexOf(".sqlite")>-1 || oriFileName.IndexOf(".pdf")>-1){
            newFileName=oriFileName;
        }else{
            newFileName="";
        }

        return newFileName;

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="deviceid"></param>
    /// <param name="phonetype"></param>
    /// <param name="sqltype"></param>
    /// <param name="filename"></param>
    /// <param name="version"></param>
    public void DownLoadLog(string deviceid, string phonetype, string sqltype, string filename, string version)
    {
        Database db = new Database();
       // DataTable dt = new DataTable();
        
        CReturnData myData = new CReturnData();
        /*連線DB*/
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;
        if (myData.nRet == 0)
        {
            //Log下載記錄
            //string StrSQL = "INSERT INTO userDownloadLog(deviceid, phonetype, [version], sqltype, [datetime]) "
	        //    + "SELECT '" + deviceid + "', '" + phonetype + "', '" + version + "', '" + sqltype + "', GETDATE() ";
           
            string StrSQL = "INSERT INTO userDownloadLog(deviceid, phonetype, [version], sqltype, [datetime]) "
                + "SELECT @deviceid, @phonetype, @version,@sqltype, GETDATE() ";


            System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand(StrSQL, db.getOcnn());
            sqlCmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@deviceid", MDS.Utility.NUtility.checkString(deviceid)));
            sqlCmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@phonetype", MDS.Utility.NUtility.checkString(phonetype)));
            sqlCmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@version", MDS.Utility.NUtility.checkString(version)));
            sqlCmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sqltype", MDS.Utility.NUtility.checkString(sqltype)));


            /*寫入DB*/
            db.BeginTranscation();
            db.AddDmsSqlCmd(StrSQL);
            db.CommitTranscation();

            int nAffectedRowCount=-1;
            myData.nRet = db.ExecNonQuerySQLCommand(sqlCmd, ref nAffectedRowCount);

            //myData.nRet = db.nRet;
            myData.outMsg = db.outMsg;

            db.DBDisconnect();
        }
        myData = null;
        db = null;
    }

}
