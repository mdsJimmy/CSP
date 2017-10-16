using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MDS.Database;
using System.Data.SqlClient;
using System.Data;

namespace MIP.Utility
{

    /// <summary>
    /// MipCodesUtil 的摘要描述
    /// </summary>
    public class MIPCodesUtil
    {
        public MIPCodesUtil()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }


        public static List<CodeVo> getCodeListByLevel(string level)
        {

            List<CodeVo> codeVoList = new List<CodeVo>();

            string strSql = "SELECT * FROM MIP_CODES WHERE CLEVEL=@CLEVEL AND CSTATUS='0' ORDER BY CORDER ";
            Database db = new Database();
            DataTable dt = new DataTable();
            int nRet = db.DBConnect();

            try
            {
                if (nRet == 0)
                {
                    System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(strSql, db.getOcnn());
                    SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CLEVEL", MDS.Utility.NUtility.checkString(level)));
                    db.ExecQuerySQLCommand(SqlCom, ref dt);

                    foreach (DataRow row in dt.Rows)
                    {

                        CodeVo codeVo = new CodeVo();

                        codeVo.key = MDS.Utility.NUtility.trimBad(row["CKEY"].ToString());//代碼

                        codeVo.name = MDS.Utility.NUtility.trimBad(row["CNAME"].ToString());//名稱

                        codeVo.level = MDS.Utility.NUtility.trimBad(row["CLEVEL"].ToString());//父層級/分類

                        codeVo.status = int.Parse(row["CSTATUS"].ToString());//狀態 0:啟用、1:停用

                        codeVo.order = int.Parse(row["CORDER"].ToString());//排序用

                        codeVo.note = MDS.Utility.NUtility.trimBad(row["CNOTE"].ToString());//備註

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

        public static List<CodeVo> getCodeListByLevel()
        {

            List<CodeVo> codeVoList = new List<CodeVo>();

            string strSql = "SELECT * FROM MIP_CODES where CLEVEL='A10' or CLEVEL='A20' or CLEVEl='A30' ORDER BY CORDER ";
            Database db = new Database();
            DataTable dt = new DataTable();
            int nRet = db.DBConnect();

            try
            {
                if (nRet == 0)
                {
                    System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(strSql, db.getOcnn());
                    
                    db.ExecQuerySQLCommand(SqlCom, ref dt);

                    foreach (DataRow row in dt.Rows)
                    {

                        CodeVo codeVo = new CodeVo();

                        codeVo.key = MDS.Utility.NUtility.trimBad(row["CKEY"].ToString());//代碼

                        codeVo.name = MDS.Utility.NUtility.trimBad(row["CNAME"].ToString());//名稱

                        codeVo.level = MDS.Utility.NUtility.trimBad(row["CLEVEL"].ToString());//父層級/分類

                        codeVo.status = int.Parse(row["CSTATUS"].ToString());//狀態 0:啟用、1:停用

                        codeVo.order = int.Parse(row["CORDER"].ToString());//排序用

                        codeVo.note = MDS.Utility.NUtility.trimBad(row["CNOTE"].ToString());//備註

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

    }

    public class CodeVo
    {
       
        public string key { set; get; }//代碼

        public string name { set; get; }//名稱

        public string level { set; get; }//父層級/分類

        public int status { set; get; }//狀態 0:啟用、1:停用

        public int order { set; get; }//排序用

        public string note { set; get; }//備註



    }

}


