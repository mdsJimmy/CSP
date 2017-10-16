using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Data;

using System.Runtime.CompilerServices;
using System.Collections;
using System.Globalization;  //for Hashtable object

namespace MDS.Utility
{
    public class Utils
    {
        private static string strLogFileLocalFolder = ""; /*Log資料夾名稱*/

        public Utils()
        {
            int nRet = -1;
        }


        /// <summary>
        /// 檢核統計報表查詢條件之日期區間起起訖，若超過30天，不允許查詢
        /// </summary>
        /// <param name="StartDT"></param>
        /// <param name="EndDT"></param>
        /// <returns></returns>
        public static int IsValidate(string strStartDT, string strEndDT)
        {
            DateTime StartDT;
            DateTime EndDT;
            TimeSpan ts;
            StartDT = Convert.ToDateTime(strStartDT);
            EndDT = Convert.ToDateTime(strEndDT);

            ts = EndDT - StartDT;
            if (Convert.ToInt32(ts.Days) > 32)
                return -1;
            else
                return 0;            
        }

        /// <summary>
        /// HashTable無Sort function，自行實作
        /// </summary>    
        public DataView SortHashtable(Hashtable oHash)
        {
            System.Data.DataTable oTable = new System.Data.DataTable();
            oTable.Columns.Add(new System.Data.DataColumn("key"));
            oTable.Columns.Add(new System.Data.DataColumn("value"));

            foreach (System.Collections.DictionaryEntry oEntry in oHash)
            {
                DataRow oDataRow = oTable.NewRow();
                oDataRow["key"] = oEntry.Key;
                oDataRow["value"] = oEntry.Value;
                oTable.Rows.Add(oDataRow);
            }

            DataView oDataView = new DataView(oTable);
            /* 自訂排序方向與欄位 */
            ////oDataView.Sort = "value DESC, key ASC";
            oDataView.Sort = "key ASC";

            return oDataView;
        }


        /// <summary>
        /// 指定Log資料夾名稱
        /// </summary>
        /// <param name="LocalFolder"></param>
        public static void LogFileLocalFolder(string LocalFolder)
        {
            if (LocalFolder != "")
            {
                strLogFileLocalFolder = LocalFolder;
            }
        }



        /// <summary>
        /// LogFile
        /// </summary>
        /// <param name="LogStringByLine"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void LogFile(string LogStringByLine)
        {
            int nRet = -1;

            string sFileName = "";
            string sTempFilePath = "";
            string sWriteFilePathName = "";

            try
            {
                if (LogStringByLine.Length > 0)
                {
                    LogStringByLine = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ").ToString() + LogStringByLine;

                    sFileName = DateTime.Now.ToString("yyyyMMdd").ToString() + ".txt";

                    sTempFilePath = strLogFileLocalFolder;

                    if (sTempFilePath.Substring(sTempFilePath.Length - 1, 1) == "\\")
                        sTempFilePath = sTempFilePath.Substring(0, sTempFilePath.Length - 2);

                    //checking root save file path
                    if (System.IO.Directory.Exists(sTempFilePath) != true)
                        System.IO.Directory.CreateDirectory(sTempFilePath);

                    sWriteFilePathName = sTempFilePath + "\\" + sFileName;

                    System.IO.StreamWriter m_StreamWriter = new System.IO.StreamWriter(sWriteFilePathName, true, System.Text.Encoding.GetEncoding("BIG5"));
                    m_StreamWriter.WriteLine(LogStringByLine);
                    m_StreamWriter.Close();

                    Console.WriteLine(LogStringByLine);
                }
            }
            catch
            {
                nRet = -1;
            }
        }


        /// <summary>
        /// LogFile2
        /// </summary>
        /// <param name="LogStringByLine"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void LogFile2(string LogFileName,string LogStringByLine)
        {
            int nRet = -1;

            string sFileName = "";
            string sTempFilePath = "";
            string sWriteFilePathName = "";

            try
            {
                if (LogStringByLine.Length > 0)
                {
                    LogStringByLine = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff ").ToString() + LogStringByLine;

                    sFileName = LogFileName + DateTime.Now.ToString("yyyyMMdd").ToString() + ".txt";

                    sTempFilePath = strLogFileLocalFolder;

                    if (sTempFilePath.Substring(sTempFilePath.Length - 1, 1) == "\\")
                        sTempFilePath = sTempFilePath.Substring(0, sTempFilePath.Length - 2);

                    //checking root save file path
                    if (System.IO.Directory.Exists(sTempFilePath) != true)
                        System.IO.Directory.CreateDirectory(sTempFilePath);

                    sWriteFilePathName = sTempFilePath + "\\" + sFileName;

                    System.IO.StreamWriter m_StreamWriter = new System.IO.StreamWriter(sWriteFilePathName, true, System.Text.Encoding.GetEncoding("BIG5"));
                    m_StreamWriter.WriteLine(LogStringByLine);
                    m_StreamWriter.Close();

                    Console.WriteLine(LogStringByLine);
                }
            }
            catch
            {
                nRet = -1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        public static void Delay(int m)
        {
            DateTime endTime = DateTime.Now.AddMilliseconds((double)m);
            while (endTime > DateTime.Now)
            {
                //Application.DoEvents();
                Thread.Sleep(1);
            }
        }

    }
}
