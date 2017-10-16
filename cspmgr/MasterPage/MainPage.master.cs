using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data;
using MDS.Database;

public partial class MasterPage_MainPage : System.Web.UI.MasterPage
{
    public static string SHOW_PT_INFO = " style=\"display: none;\"";//" style=\"display: none;\"";
    //原為顯示IP，透過webconfig設定版本
    public string ipServer = System.Configuration.ConfigurationManager.AppSettings["Version"];
    public string SecureKey = "";


    protected void Page_Load(object sender, EventArgs e)
    {

        // 目前Server IP
        IPAddress server_ip = new IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);
        ipServer = server_ip.ToString().Substring(server_ip.ToString().Length - 3);
        ipServer = server_ip.ToString();

        SecureKey = MDS.Utility.NUtility.trimBad(Request.QueryString["SecureKey"]); 
        
    }


}
