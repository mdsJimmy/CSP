using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Mytool 的摘要描述
/// </summary>
public class Mytool
{
    public Mytool()
    {
        
    }

    public static void ReseponseWrite(string r)
    {
        
        HttpContext.Current.Response.BufferOutput = true;
        HttpContext.Current.Response.Write(r);
        HttpContext.Current.Response.Flush();
    }
}