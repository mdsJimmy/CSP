using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MDS.Database;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.IO;
using System.Text;

public partial class DMSControl_singleFileUpload : BasePage
{

    protected string uploadOK = "";
    static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    protected void Page_Load(object sender, EventArgs e)
    {
      
 
        
        // 確保不是POSTBACK, 動態產生下拉選項 .s
        if (!IsPostBack) {
            
            // 從資料庫取得SQLITE_CODE.filename fileld START===============
            Database db = new Database();
            DataTable dt = new DataTable();
            int nRet = -1;

            string StrSQL = "SELECT DISTINCT sqltype, [description] FROM SQLITE_CODE ";

            /*連線DB*/
            nRet = db.DBConnect();
            if (nRet == 0)
            {


                System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(StrSQL, db.getOcnn());
                nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);

                if (nRet == 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sqltype.Items.Add(new ListItem(dt.Rows[i]["description"].ToString(), dt.Rows[i]["sqltype"].ToString()));
                    }
                }
            }
            db.DBDisconnect();
            // 從資料庫取得SQLITE_CODE.filename fileld END===============
            
        }
        uploadOK = "";
        //StringBuilder sb = new StringBuilder();
        
        //sb.Append("sqltype>").Append(sqltype.Value).Append("<").Append("\n");
        //sb.Append("input_version_no>").Append(input_version_no.Text).Append("<").Append("\n");
      
        //sb.Append("FileUpload1.FileName>").Append(FileUpload1.FileName).Append("<").Append("\n");
        //string filepath = @"c:\temp\posttestLog\";
        //System.IO.File.WriteAllText(filepath + "btnUpload_Click.txt", "" + sb.ToString());

        string _FileName = FileUpload1.FileName;

        //檢查副檔名避免攻擊
        if (FileUpload1.HasFile)
        {

            if (_FileName.Contains(".sqlite") || _FileName.Contains(".pdf"))
            {
                insert(FileUpload1.FileName, FileUpload1.FileBytes, input_version_no.Text, sqltype.Value);
                uploadOK = "YES";
            }
          

        }
        
 
         
    }

    public int insert(string fileName, byte[] imageData, string ver_no, string sqlType)
    {
        int ret = 0;
        string StrSQL = "INSERT INTO userUploadLog(sqltype, fileUploadOldName, fileUploadNewName, datetime,  version_no,imageData)"
                                                 + "values( @sqltype, @fileUploadOldName, @fileUploadNewName,GETDATE()  , @version_no,@imageData) ";
        Database db = new Database();
        DataTable dt = new DataTable();
        /*連線DB*/
        /* 20111004-add */
        //Initialize SqlCommand object for insert.
        SqlCommand SqlCom = new SqlCommand(StrSQL, db.getOcnn());
        CReturnData myData = new CReturnData();
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;


        SqlCom.Parameters.Add(new SqlParameter("@sqltype", sqlType));
        SqlCom.Parameters.Add(new SqlParameter("@fileUploadOldName", fileName));
        SqlCom.Parameters.Add(new SqlParameter("@fileUploadNewName", fileName));
        SqlCom.Parameters.Add(new SqlParameter("@version_no", MDS.Utility.NUtility.checkString(ver_no)));


        /*20160506-add 透過檔案管理平台歸檔取得索引值*/
        //SqlCom.Parameters.Add(new SqlParameter("@file_index", saveFile(fileName, MIPLibrary.MIPUtil.GetBytes(fileName))));
        SqlCom.Parameters.Add(new SqlParameter("@imageData", imageData));


        SqlCom.ExecuteNonQuery();
        db.getOcnn().Close();
        return myData.nRet;
    }



    /*20160506-add 透過檔案管理平台歸檔取得索引值*/
    private int saveFile(string fileName,  byte[] imageData)
    {
        int file_index = 0;
 

        Database db = new Database();
        

        try
        {

            db.DBConnect();
          


            MIPLibrary.FileManager fp = new MIPLibrary.FileManager();

            file_index = fp.saveFileToDB(db.getOcnn(), fileName, imageData);

        }
        catch (Exception ex)
        {
           log.Fatal("" + ex);
           throw ex;
        }
        finally
        {
            try {db.DBDisconnect();}
            catch (Exception ex) { log.Fatal("資料庫連線關閉發生錯誤：" + ex.Message); }
        }

        return file_index;
    }



}
