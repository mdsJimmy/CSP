using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using MDS.Database;


namespace MIP.Utility
{

    /// <summary>
    /// MIPUserUploadHandler 的摘要描述
    /// </summary>
    public class MIPUserUploadHandler
    {
        public MIPUserUploadHandler()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public static List<UserUploadLogVo> getUserUploadLogListBySqltype(int type)
        {

            List<UserUploadLogVo> userUploadLogVoList = new List<UserUploadLogVo>();

            string strSql = "SELECT * FROM userUploadLog WHERE sqltype=@sqltype ";

            Database db = new Database();
            DataTable dt = new DataTable();


            try
            {
                db.DBConnect();

                System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(strSql, db.getOcnn());
                SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sqltype", type));
                int nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);


                foreach (DataRow row in dt.Rows)
                {

                    UserUploadLogVo userUploadLogVo = new UserUploadLogVo();

                    userUploadLogVo.version = int.Parse(row["version"].ToString());

                    userUploadLogVo.sqltype = row["sqltype"].ToString();

                    userUploadLogVo.fileUploadOldName = row["fileUploadOldName"].ToString();

                    userUploadLogVo.fileUploadNewName = row["fileUploadNewName"].ToString();

                    DateTime datetime = (DateTime)row["datetime"];
                    if (datetime == null)
                    {
                        userUploadLogVo.datetime = "";
                    }
                    else
                    {
                        userUploadLogVo.datetime = datetime.ToString("yyyy/MM/dd HH:mm:ss");
                    }


                    userUploadLogVo.version_no = row["version_no"].ToString();

                    userUploadLogVo.imageData = (byte[])row["imageData"];

                    userUploadLogVoList.Add(userUploadLogVo);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dt.Dispose();
                dt = null;
                db.getOcnn().Close();
                db.DBDisconnect();
            }

           

            return userUploadLogVoList;
        }

        public static UserUploadLogVo getUserUploadLogBySqltype(int type)
        {

            List<UserUploadLogVo> userUploadLogVoList = getUserUploadLogListBySqltype(type);

            if (userUploadLogVoList.Count != 0)
            {
                return userUploadLogVoList[0];
            }
            else
            {
                return null;
            }
           
        }

    }

    public class UserUploadLogVo
    {
        public int version { set; get; }

        public string sqltype { set; get; }

        public string fileUploadOldName { set; get; }

        public string fileUploadNewName { set; get; }

        public string datetime { set; get; }

        public string version_no { set; get; }

        public byte[] imageData { set; get; }

    }

}