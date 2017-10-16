using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// test 的摘要描述
/// </summary>
public class GetWordingSet
{
    DataSet ds = new DataSet();

    public GetWordingSet()
    {
        ds.ReadXml(HttpContext.Current.Server.MapPath("~/App_GlobalResources/DMSWording.resx"));
    }

    public DataTable GetWordingList()
    {
        return ds.Tables["data"];

    }
}
