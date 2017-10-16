using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// OpenLoggerSetting 的摘要描述
/// </summary>
public class Logger
{
    //static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    static log4net.ILog loggerr = null;
    static string isOpen = System.Configuration.ConfigurationManager.AppSettings["ISOPENBUGER"];
    public Logger()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //
       
    }
    public static void buger(log4net.ILog logger, string log)
    {
        loggerr = logger;
       
           
    }
}