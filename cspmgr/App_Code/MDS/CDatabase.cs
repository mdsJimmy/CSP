using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections;


namespace MDS.Database
{
    public class Database
    {


        public static String strDBConnect = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public int nRet;
        public string outMsg;
        
        SqlConnection oConn = new SqlConnection();
        Queue queSQL = new Queue();

        public Database() 
        {
            nRet = -1;
            outMsg = "";
        }

        /// <summary>
        ///     檢查連線狀態
        /// </summary>
        /// <returns></returns>
        private int CheckSession()
        {
            return (int)oConn.State;
        }

        /// <summary>
        ///     與DB連線
        /// </summary>
        /// <returns>nRet</returns>
        public int DBConnect() 
        {
            //string strDBConnect = ConfigurationManager.ConnectionStrings["DMSWeb"].ConnectionString;
           
            try 
            {
                oConn.ConnectionString = strDBConnect;
                oConn.Open();

                nRet = 0;
                outMsg = "";
            }
            catch (Exception ex)
            {
                nRet = -1;
                outMsg = ex.Message;
            }
            return nRet;
        }

        /// <summary>
        ///     與DB斷線
        /// </summary>
        /// <returns></returns>
        public int DBDisconnect()
        {
            oConn.Close();
            oConn.Dispose();
            oConn = null;
            queSQL.Clear();
            queSQL = null;
            return 0;
        }

     


        /// <summary>
        ///     執行SQL查詢(SELECT)
        /// </summary>
        /// <param name="cmdLiming">2016:參數化的SqlCommand對象</param>
        /// <param name="dt">回傳的DataTable</param>
        /// <returns></returns>
        public int ExecQuerySQLCommand(SqlCommand cmdLiming, ref DataTable dt)
        {
            if (CheckSession() == 1)
            {
                if (cmdLiming.CommandText.Length != 0)
                {
                    try
                    {
                       
                        try
                        {
                            SqlDataReader oDataReader = cmdLiming.ExecuteReader();
                            try
                            {
                                try
                                {
                                    dt.Load(oDataReader, LoadOption.OverwriteChanges);
                                    nRet = 0;
                                }
                                catch (Exception ex)
                                {
                                    nRet = -1;
                                    outMsg = ex.Message;
                                }
                            }
                            finally
                            {
                                oDataReader.Close();
                                oDataReader.Dispose();
                            }
                        }
                        finally
                        {
                            cmdLiming.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        nRet = -1;
                        outMsg = ex.Message;
                    }
                }
                else
                {
                    outMsg = "QuerySQLCommand is Empty.";
                    nRet = -1;
                }
            }
            else
            {
                outMsg = "SQL Connection is Disconnect.";
                nRet = -1;
            }

            ////cmdLiming = null;
            ////if (oDataReader != null) oDataReader.Close();
            ////oDataReader = null;

            return nRet;
        }




        //新增參數式SQL的執行方法
        public int ExecNonQuerySQLCommand(System.Data.SqlClient.SqlCommand SqlComUpdate, ref int AffectedRowCount)
        {
            AffectedRowCount = 0;

            if (CheckSession() == 1)
            {
                
                try
                {

                    AffectedRowCount = SqlComUpdate.ExecuteNonQuery();
                    nRet = 0;
                }
                catch (Exception ex)
                {
                    nRet = -1;
                    outMsg = ex.Message;
                }
                finally
                {
                    SqlComUpdate.Dispose();
                    SqlComUpdate = null;
                }

            }
            else
            {
                nRet = -1;
                outMsg = "SQL Connection is Disconnect.";
            }
            return nRet;
        }






        /// <summary>
        ///     BeginTranscation
        /// </summary>
        /// <returns></returns>
        public int BeginTranscation()
        {
            queSQL.Clear();
            return 0;
        }

        /// <summary>
        ///     AddDmsSqlCmd
        /// </summary>
        /// <param name="strSQL">SQL字串</param>
        /// <returns></returns>
        public void AddDmsSqlCmd(string strSQL)
        {
            if (strSQL.Length > 0)
            {
                queSQL.Enqueue(strSQL);
            }
        }
        public SqlConnection getOcnn()
        {
            return oConn;
        }
        /// <summary>
        ///     CommitTranscation
        /// </summary>
        /// <returns></returns>
        public int CommitTranscation()
        {
            string strSQL = "";
            SqlCommand sqlCmd = new SqlCommand();
            SqlTransaction sqlTrans = null; ;

            if (CheckSession() == 1)
            {
                if (queSQL.Count > 0)
                {
                    sqlTrans = oConn.BeginTransaction();
                    sqlCmd.Connection = oConn;
                    sqlCmd.Transaction = sqlTrans;
                    try
                    {
                        while (queSQL.Count > 0)
                        {
                            strSQL = (string)queSQL.Dequeue();
                            sqlCmd.CommandText = MDS.Utility.NUtility.checkString(strSQL);
                            sqlCmd.ExecuteNonQuery();
                        }

                        sqlTrans.Commit();
                        nRet = 0;
                        outMsg = "";
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            sqlTrans.Rollback();
                            outMsg = ex.Message;
                            
                        }
                        catch (Exception ex2)
                        {
                            outMsg = ex2.Message ;
                        }
                        nRet = -1;
                    }
                }
            //    else if (queSQL.Count == 1)
            //    {
            //        sqlCmd.Connection = oConn;
            //        try
            //        {
            //            strSQL = (string)queSQL.Dequeue();
            //            sqlCmd.CommandText = strSQL;
            //            sqlCmd.ExecuteNonQuery();

            //            nRet = 0;
            //            outMsg = "";
            //        }
            //        catch (Exception ex)
            //        {
            //            outMsg = ex.Message;
            //            nRet = -1;
            //        }
            //        //sqlCmd.Dispose();
            //    }
            //    else
            //    {
            //        nRet = -1;
            //        outMsg = "No SQL Command.";
            //    }
            }
            else
            {
                nRet = -1;
                outMsg = "SQL Connection is Disconnect.";
            }
            sqlCmd.Dispose();
            if (sqlTrans != null) sqlTrans.Dispose();
            sqlTrans = null;
            sqlCmd = null;
            return nRet;
        }


        /// <summary>
        /// 由SQL QUERY 取得DataTable
        /// </summary>
        /// <param name="query">SQL Query</param>
        /// <param name="_param">查詢參數</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string query, SqlParameter[] parameter)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection sqlcon = new SqlConnection(strDBConnect))
                {
                    sqlcon.Open();
                    SqlDataAdapter sqlda = new SqlDataAdapter(MDS.Utility.NUtility.checkString(query), sqlcon);
                    sqlda.SelectCommand.Parameters.AddRange(parameter);
                    sqlda.Fill(dt);
                    sqlda.SelectCommand.Parameters.Clear();
                    sqlcon.Close();
                }
                return dt;
            }
            catch (System.Exception ex)
            {
                //logger.ErrorException(query, ex);
                throw ex;
                //return null;
            }
        }



        /// <summary>
        /// 執行SQL QUERY 傳回單一值
        /// </summary>
        /// <param name="query"></param>
        /// <param name="_param">查詢參數</param>
        /// <returns></returns>
        private static object GetScalar(string query, SqlParameter[] parameter)
        {
            try
            {
                object _return = null;
                using (SqlConnection sqlcon = new SqlConnection(strDBConnect))
                {
                    sqlcon.Open();
                    SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                    sqlcmd.Parameters.AddRange(parameter);
                    _return = sqlcmd.ExecuteScalar();
                    sqlcmd.Parameters.Clear();
                    sqlcon.Close();
                }
                return _return;
            }
            catch (System.Exception ex)
            {
                //logger.ErrorException(query, ex);
                throw ex;
                //return null;
            }
        }
    }
}
