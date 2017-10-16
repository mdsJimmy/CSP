using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MDS.Database;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using iTextSharp.text;

namespace MIP.Utility
{
    /// <summary>
    /// MIPProduct 的摘要描述
    /// </summary>
    public class MIPCode
    {
        public MIPCode()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        

        
        public static List<Dictionary<string, string>> getProductList(string strProKind)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            DataTable dt = new DataTable();
            try
            {
                
                List<CodeVo> codeVoList = MIPCodesUtil.getCodeListByLevel(strProKind);

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

