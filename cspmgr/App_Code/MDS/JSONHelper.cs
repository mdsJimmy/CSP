using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.IO;
using System.Collections.Generic;
using System.Web.Script.Serialization;

/// <summary>
/// JSONHelper 的摘要描述
/// </summary>
public static class JSONHelper
{
	/// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string Serialize<T>(T obj)
    {
        System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
        MemoryStream ms = new MemoryStream();
        serializer.WriteObject(ms, obj);
        string retVal = System.Text.Encoding.UTF8.GetString(ms.ToArray());
        ms.Dispose();
        return retVal;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="json"></param>
    /// <returns></returns>
    public static T Deserialize<T>(string json)
    {
        T obj = Activator.CreateInstance<T>();
        MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json));
        System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
        obj = (T)serializer.ReadObject(ms);
        ms.Close();
        ms.Dispose();
        return obj;
    }


    /// <summary> 
    /// DataTable轉為json 
    /// </summary> 
    /// <param name="dt">DataTable</param> 
    /// <returns>json字串</returns> 
    public static string DataTableToJson(ref DataTable dt)
    {
        string json = "";

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        Dictionary<string, object> row = new Dictionary<string, object>();
        List<Dictionary<string, object>> rowList = new List<Dictionary<string, object>>();

        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();

            foreach (DataColumn dc in dt.Columns)
            {
                row.Add(dc.ColumnName, dr[dc].ToString());
            }
            
            rowList.Add(row);
        }
        
        json = serializer.Serialize(rowList);
        return json;
    }


}
