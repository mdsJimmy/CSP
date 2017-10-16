using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;


namespace MLIServices
{
    public class SaveDatatoMLI
    {
        /// <summary>
        /// 儲存資訊至後台
        /// </summary>
        /// <param name="appName">appname</param>
        /// <param name="deviceId">deviceid</param>
        /// <param name="agId">account id</param>
        /// <param name="status">0 啟用 1 停用</param>
        /// <param name="in_flag">A 新增  U 修改  D 刪除</param>
        /// <returns></returns>
        public static string savePushDataToSd0(string appName, string deviceId, string agId, string status, string in_flag)
        {
            bool success = false;
            string toXserver = "";
            string reply = "";
            string[] record = null;
            try
            {
                toXserver = string.Format("{0}&&&{1}&&&{2}&&&{3}&&&{4}&&&", appName, deviceId, agId, status, in_flag);
                reply = Con_Authority.Connstr_SQL.Connstr_Sd0("push_service_process", toXserver);
                success = true;
            }
            catch (Exception)
            {
                reply = "-999&&&主機連線錯誤&&&";
            }
            try
            {
                record = Regex.Split(reply.Trim(), "&&&", RegexOptions.IgnoreCase);
                if (record[0].Trim() == "000")
                {
                    //成功
                }
                else
                {
                    //其它錯誤原因
                }
            }
            catch (Exception)
            {
                reply = "-900&&&解析失敗&&&";
            }
            return reply;
        }
    }
}
