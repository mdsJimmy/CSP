using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MDS.Database;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace MIP.Utility
{
    
    /// <summary>
    /// MipCodesUtil 的摘要描述
    /// </summary>
    public class MipSystemModule
    {
        protected string strSql = "";
        protected string rowKey = "";
        protected string rowName = "";
        public SqlParameter rowParameters = null;
        /*
         建立物件後，設定sql
             
             */
        public string SQL {  set { strSql = value; } }
        public string KEY { set { rowKey = value; } }
        public string NAME { set { rowName = value; } }
        public SqlParameter ps { set { rowParameters = value; } }
        public MipSystemModule()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }


        public  List<CodeVo> getCodeListByLevel()
        {

            List<CodeVo> codeVoList = new List<CodeVo>();

           ;
            Database db = new Database();
            DataTable dt = new DataTable();
            int nRet = db.DBConnect();
           
            try
            {
                if (nRet == 0)
                {
                    System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(strSql, db.getOcnn());
                    if (rowParameters != null)
                    {
                        
                        SqlCom.Parameters.Add(rowParameters);
                        
                        SqlCom.ExecuteNonQuery();
                    }
                    db.ExecQuerySQLCommand(SqlCom, ref dt);


                   
                

                    foreach (DataRow row in dt.Rows)
                    {

                        CodeVo codeVo = new CodeVo();
                        
                        codeVo.key = MDS.Utility.NUtility.trimBad(row[rowKey].ToString());//代碼

                        codeVo.name = MDS.Utility.NUtility.trimBad(row[rowName].ToString());//名稱

                       

                        codeVoList.Add(codeVo);

                    }


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


            

            return codeVoList;
        }

        public  List<Dictionary<string, string>> getProductList()
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            DataTable dt = new DataTable();
            try
            {

                List<CodeVo> codeVoList = getCodeListByLevel();

                foreach (CodeVo codeVo in codeVoList)
                {
                    Dictionary<string, string> oDictionary = new Dictionary<string, string>();


                    oDictionary.Add("CKEY", codeVo.key);
                    oDictionary.Add("CNAME", codeVo.name);
                    list.Add(oDictionary);
                }


            }
            catch (Exception ex)
            {
                Debug.Write("MIPProduct Exception :" + ex.Message);
                throw ex;
            }
            finally
            {
                dt.Dispose();
                dt = null;
            }

            return list;

        }
    }

   

}


