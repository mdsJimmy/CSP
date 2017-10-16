using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// TrimString 的摘要描述
/// </summary>
public class TrimString
{
    public TrimString()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //
    }

    public static string trimBad(string escaped)
    {
        string val = "";
        if (string.IsNullOrEmpty(escaped) || string.IsNullOrWhiteSpace(escaped))
        {
            val = "";
        }
        else
        {
            val = escaped;

        }
        string resultStr = val.Replace("&lt;", "")
                      .Replace("&gt;", "")
                      .Replace("&quot;", "")
                      .Replace("&apos;", "")
                      .Replace("&amp;", "")
                      .Replace("&lt;/b&gt;", "")
                      .Replace("&lt;i&gt;", "")
                      .Replace("&lt;/i&gt;", "")
                      .Replace("'", "＇")

                      .Replace("<", "")
                      .Replace(">", "")
                      .Replace("&", "")
                      //.Replace(" ", "")
                      .Replace("\"", "");


        return MDS.Utility.NUtility.checkString(resultStr);
    }
}