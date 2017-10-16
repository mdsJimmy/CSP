using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MDS.Database;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;
using AESCryptoIPhone;
using System.Web.Services;      //IPhone & C# AES 128bit 加解密

public partial class Verify : BasePage
{
    //Regex reg = new Regex("\w{3}");
    public string ipServer = "";
    static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



    [WebMethod(EnableSession = true)]
	[System.Web.Script.Services.ScriptMethod()]
	public static string CheckUser(string inputValue)
	{
         
		Database oDatabase = new Database();
		DataTable dt = new DataTable();
		int nRet = -1;
		string outMsg = "";
		string strSQL = "";

		string[] arrInput = inputValue.Split(new string[] { "^^" }, StringSplitOptions.None);

		string sAccountID = arrInput[0].ToString().Trim();
		string sPassword = arrInput[1].ToString().Trim();
		string sPassLogin = arrInput[2].ToString().Trim();
		string PswAvailablePeriod = "0";
		string SecurityUserAccountID = "";
		string SecureKey = "";
		string returnValue = "";
        

        oDatabase.DBConnect();
		nRet = oDatabase.nRet;
		outMsg = oDatabase.outMsg;
        try
        {
            if (sAccountID == "" || sPassword == "")		//判是否有無輸入帳號、密碼
            {
                //returnValue = ParseWording("ui_610");		//請輸入使用者帳號與使用者密碼
                returnValue = "請輸入使用者帳號與使用者密碼";
            }
            else
            {
                logger.Debug("SYSLOG-使用者登入：" + sAccountID);

                //取出密碼可用期限 Start
                if (nRet == 0)
                {
                    if (sAccountID != "" && sPassword != "")
                    {
                        strSQL = "SELECT * FROM SysAttribute WHERE [KEY] = 'PswAvailablePeriod'";
                        //strSQL = "SELECT * FROM SysAttribute WHERE [KEY] = '"+ password_changeDays+"";
                      
                        System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(strSQL, oDatabase.getOcnn());
                        nRet = oDatabase.ExecQuerySQLCommand(SqlCom, ref dt);


                        outMsg = oDatabase.outMsg;
                        if (dt.Rows.Count > 0)
                        {
                            PswAvailablePeriod = dt.Rows[0]["Data"].ToString();
                        }
                        dt.Reset();
                    }
                }
                else
                {
                    HttpContext.Current.Response.Write(outMsg);
                }
                //取出密碼可用期限 End

                //設定為90天透過 webconfig 設定
                PswAvailablePeriod = System.Configuration.ConfigurationManager.AppSettings["p000000d_changeDays"];
                //從DB撈出資訊

                //20160812:改為參數化的sql執行方式
                strSQL = "SELECT LTRIM(RTRIM(AccountID)), CONVERT(varchar(50), Password), LTRIM(RTRIM(Startup)), LTRIM(RTRIM(ISNULL(PWLastUpdateTime, GETDATE()))), LTRIM(RTRIM(PWType)), LTRIM(RTRIM(iFailTimes)), LTRIM(RTRIM(dLockTime)), LTRIM(RTRIM(cRoleID)), AD_CheckFlag,PWLastUpdateTime FROM SecurityUserAccount " +
                         "Where AccountID = @sAccountID1 collate Chinese_Taiwan_Stroke_CS_AS";

                System.Data.SqlClient.SqlCommand SqlCom2 = new System.Data.SqlClient.SqlCommand(strSQL, oDatabase.getOcnn());

                SqlCom2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sAccountID1", sAccountID ));

                //呼叫傳入參數式sqlcmd的方法
                nRet = oDatabase.ExecQuerySQLCommand(SqlCom2, ref dt);


                outMsg = oDatabase.outMsg;
                if (nRet == 0)
                {
                    if (dt.Rows.Count > 0)
                    {
                        SecurityUserAccountID = dt.Rows[0][0].ToString();

                        if (dt.Rows[0]["AD_CheckFlag"].ToString() != "True")	///本地端驗證
                        {
                            if (dt.Rows[0][2].ToString() != "1")
                            {

                                returnValue = "-2";		///本使用者已停權
                                //oDatabase.DBDisconnect();
                                //return returnValue;
                            }
                            else if (dt.Rows[0][6].ToString() != "")
                            {
                                returnValue = "-3";     ///本使用者已鎖定

                                //oDatabase.DBDisconnect();
                                //return returnValue;
                            }
                            else if (sPassLogin == "0")		///模擬登入則不判斷密碼, 直接通過
                            { 
                                
                                if (sPassword != dt.Rows[0][1].ToString())
                                {
                                    if (Convert.ToInt16(dt.Rows[0][5].ToString().Trim()) >= 10)		///密碼錯誤是否已超過10次
                                    {
                                        strSQL = "Update SecurityUserAccount Set iFailTimes = (isNull(iFailTimes,0) + 1), dLockTime = GETDATE(), dModifyTime = GETDATE() " +
                                                 "Where (AccountID= @sAccountID1 ) ";

                                        System.Data.SqlClient.SqlCommand SqlComUpdate = new System.Data.SqlClient.SqlCommand(strSQL, oDatabase.getOcnn());

                                        SqlComUpdate.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sAccountID1", sAccountID));
                                       
                                        int AffectedRowCount = 0;                                       
                                        oDatabase.ExecNonQuerySQLCommand(SqlComUpdate, ref AffectedRowCount);


                                        returnValue = "-4";			///密碼錯誤已超過十次, 鎖定帳號
                    
                                    }
                                    else
                                    {
                                        strSQL = "Update SecurityUserAccount Set iFailTimes = (isNull(iFailTimes,0) + 1), dModifyTime = GETDATE() " +
                                                 "Where (AccountID= @sAccountID1) ";

                                        System.Data.SqlClient.SqlCommand SqlComUpdate = new System.Data.SqlClient.SqlCommand(strSQL, oDatabase.getOcnn());

                                        SqlComUpdate.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sAccountID1", sAccountID));

                                        int AffectedRowCount = 0;
                                        oDatabase.ExecNonQuerySQLCommand(SqlComUpdate, ref AffectedRowCount);


                                        returnValue = "-5";			///密碼錯誤
                                    }
                                }
                                else
                                {
                                    ///驗證通過，清除失敗登入資訊
                                    strSQL = "Update SecurityUserAccount Set iFailTimes = 0, dLockTime = Null " +
                                              "Where (AccountID= @sAccountID1) ";
                                   
                                    
                                    System.Data.SqlClient.SqlCommand SqlComUpdate = new System.Data.SqlClient.SqlCommand(strSQL, oDatabase.getOcnn());

                                    SqlComUpdate.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sAccountID1", sAccountID));

                                    int AffectedRowCount = 0;
                                    oDatabase.ExecNonQuerySQLCommand(SqlComUpdate, ref AffectedRowCount);

                                    ///產生登入驗證碼
                                    SecureKey = CreateSecureKey(0, SecurityUserAccountID);

                                    ///判斷是否ds須變更密碼
                                    DateTime sDateTime = new DateTime(Convert.ToDateTime(dt.Rows[0][3].ToString()).Ticks);
                                    TimeSpan ts = DateTime.Now.Subtract(sDateTime);
                                    if (ts.TotalSeconds <= (Convert.ToInt16(PswAvailablePeriod) * 60 * 60 * 24))
                                    {
                                        //logger.Debug(dt.Rows[0][9].ToString());
                                        if (sAccountID != "PCALTOP" && dt.Rows[0][9].ToString()=="")
                                        {
                                            //須變更密碼
                                            HttpContext.Current.Session["CHANGE_PASSWORD"] = "1";
                                            returnValue = ("1^^" + SecureKey);		//登入成功
                                        }
                                        else
                                        {
                                            HttpContext.Current.Session["CHANGE_PASSWORD"] = "0";
                                            returnValue = ("0^^" + SecureKey);		//登入成功
                                        }
                                         
                                    }
                                    else
                                    {
                                        HttpContext.Current.Session["CHANGE_PASSWORD"] = "1";
                                        returnValue = ("1^^" + SecureKey);		//登入成功, 須變更密碼
                                    }
                                }
                            }
                            else
                            {
                                ///驗證通過，清除失敗登入資訊
                                strSQL = "Update SecurityUserAccount Set iFailTimes = 0, dLockTime = Null " +
                                         "Where AccountID Like '%[_]' + @sAccountID1 AND CONVERT(varbinary,@sAccountID2) = CONVERT(varbinary,SUBSTRING(AccountID, (CHARINDEX('_',AccountID)+1), LEN(AccountID)-CHARINDEX('_',AccountID)))";


                                System.Data.SqlClient.SqlCommand SqlComUpdate = new System.Data.SqlClient.SqlCommand(strSQL, oDatabase.getOcnn());
                                SqlComUpdate.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sAccountID1", sAccountID));
                                SqlComUpdate.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sAccountID2", sAccountID));

                                int AffectedRowCount = 0;
                                oDatabase.ExecNonQuerySQLCommand(SqlComUpdate, ref AffectedRowCount);



                                ///產生登入驗證碼
                                SecureKey = CreateSecureKey(0, SecurityUserAccountID);

                                ///判斷是否須變更密碼
                                DateTime sDateTime = new DateTime(Convert.ToDateTime(dt.Rows[0][3].ToString()).Ticks);
                                TimeSpan ts = DateTime.Now.Subtract(sDateTime);
                                if (ts.TotalSeconds <= (Convert.ToInt16(PswAvailablePeriod) * 60 * 60 * 24))
                                {
                                   
                                   
                                        HttpContext.Current.Session["CHANGE_PASSWORD"] = "0";
                                        returnValue = ("0^^" + SecureKey);		//登入成功
                                   
                                   
                                }
                                else
                                {
                                    HttpContext.Current.Session["CHANGE_PASSWORD"] = "1";
                                    returnValue = ("1^^" + SecureKey);		//登入成功, 須變更密碼
                                }
                            }
                        }
                        else		///--AD驗證登入
                        {

                            SecureKey = CreateSecureKey(1, SecurityUserAccountID);
                            if (sAccountID == sPassword)
                            {
                                //須變更密碼
                                HttpContext.Current.Session["CHANGE_PASSWORD"] = "1";
                                returnValue = ("1^^" + SecureKey);		//登入成功
                            }
                            else
                            {
                                HttpContext.Current.Session["CHANGE_PASSWORD"] = "0";
                                returnValue = ("0^^" + SecureKey);		//登入成功
                            }

                            logger.Debug("SYSLOG-登入結果：" + returnValue);
                            return returnValue;
                        }
                    }
                    else
                    {
                        returnValue = "-1";		///本使用者不存在
                        //return returnValue;
                    }
                }
                else
                {
                    returnValue = "DB Error";	///DB連線失敗
                                                ///
                    logger.Debug("SYSLOG-登入結果：DB連線失敗，" + returnValue);
                    return returnValue;
                }
                oDatabase.DBDisconnect();
            }
        }
        finally
        {
            dt.Clear();
            dt.Dispose();
            dt = null;
        }

        logger.Debug(String.Format("SYSLOG-IP:{0} ID:{1}-登入結果：" + returnValue, GetIPAddress(),sAccountID));

       
		return returnValue;
	}
   
    protected void Page_PreInit(object sender, EventArgs e)
    {
        string xAction = Request.QueryString["x"];

        if ("logout".Equals(xAction))
        {   //在MainPage.master增加登出參數:
            //<li><a href="<%=ResolveUrl("~/Verify.aspx?x=logout")%>"><i class="ace-icon fa fa-power-off"></i>

            //判斷登出參數:
            string loginfo = String.Format("SYSLOG-IP:{2}-user action-UserID:[{0}] FunctionName:[{1}]", Session["UserID"], "登出", GetIPAddress());
            logger.Debug(loginfo);
        }

        Session.Abandon();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      


        if (!IsPostBack)
        {
            Database oDatabase = new Database();
            DataTable dt = new DataTable();
            int nRet;
            string outMsg;

            int CheckByPassLoginList = 0;

            Page.Title = "[元大通路服務平台]";
            //Label1.Text = ParseWording("A0034");
            //Label2.Text = ParseWording("A0035");
            //AccountID.Attributes["onKeyPress"] = "checkKey();";
            //Password.Attributes["onKeyPress"] = "checkKey();";
            //btnEnter.Attributes["value"] = incFunction.ParseWording("ui_108");
            //btnEnter.Attributes["onmouseover"] = "this.";
            //btnEnter.Attributes["onclick"] = "check();";

            /* Robin add */
            //IPAddress server_ip = new IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);
            //ipServer = server_ip.ToString().Substring(server_ip.ToString().Length - 3);
            //Label1.Text = server_ip.ToString();
            ///最下面的文字 Start
            HyperLink1.Text = "下載本系統操作手冊請按此處<操作手冊>";
            if (Application["Visitors"] == null)
            {
                Label4.Text = "[CurrentVersion:v1.00 | OnLineVisitors: | Session Timeouot:" + Session.Timeout.ToString() + "]" + ipServer;
            }
            else
            {
                Label4.Text = "[CurrentVersion:v1.00 | OnLineVisitors:" + Application["Visitors"].ToString() + " | Session Timeouot:" + Session.Timeout.ToString() + "]" + ipServer;
            }
            //最下面的文字 End

            oDatabase.DBConnect();
            nRet = oDatabase.nRet;
            outMsg = oDatabase.outMsg;
            try
            {
                if (nRet == 0)
                {
                    System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand("SELECT * FROM ByPassLoginList", oDatabase.getOcnn());
                    nRet = oDatabase.ExecQuerySQLCommand(SqlCom, ref dt);

                    outMsg = oDatabase.outMsg;
                    if (nRet == 0)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 1; i <= dt.Rows.Count; i++)
                            {
                                if (dt.Rows[i - 1][1].ToString().Trim() == Request.ServerVariables["REMOTE_ADDR"].Trim())
                                {
                                    //Label3.Visible = true;
                                   // chkPassLogin.Visible = true;
                                   // Label3.Text = "模擬登入";
                                   // chkPassLogin.Attributes.Add("onClick", "SetPassLogin(this.checked);");
                                   // CheckByPassLoginList = 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        Response.Write(outMsg);
                    }
                }
                else
                {
                    Response.Write(outMsg);
                }
            }
            finally
            {
                dt.Clear();
                dt = null;
                oDatabase.DBDisconnect();
            }
        }
        else
        {
            MessageBox(Page.Request.Form["__EVENTARGUMENT"].ToString());
        }

		this.ScriptManager1.EnablePageMethods = true;
	}

	/// <summary>
	/// 產生DMS登入驗證碼,
	/// </summary>
	/// <param name="LoginType">登入狀態</param>
	/// <param name="AccountId">帳號</param>
	/// <returns>驗證碼</returns>
	private static string CreateSecureKey(int LoginType, string AccountID)
	{
		string SecureKey = "";
		int nAffectedRowCount = 0;
		string returnValue = "";

		Database oDatabase = new Database();
		int nRet;
		string outMsg, strSQL;

		SecureKey = GetNewID();

		strSQL = "INSERT INTO SecurityUserAccount_SecureKey(SecureKey, AccounID, LoginType, IPAddress, CreateDatetime, UsedDatetime, IsUsed) " +
                 "values(@SecureKey,@AccountID,@LoginType,@REMOTE_ADDR,GETDATE(),NULL,0)";

		oDatabase.DBConnect();
		nRet = oDatabase.nRet;
		outMsg = oDatabase.outMsg;
        try
        {
            if (nRet == 0)
            {
    

                System.Data.SqlClient.SqlCommand SqlComUpdate = new System.Data.SqlClient.SqlCommand(strSQL, oDatabase.getOcnn());

                SqlComUpdate.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SecureKey", SecureKey));
                SqlComUpdate.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccountID", AccountID));
                SqlComUpdate.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LoginType", LoginType));
                SqlComUpdate.Parameters.Add(new System.Data.SqlClient.SqlParameter("@REMOTE_ADDR", MDS.Utility.NUtility.HtmlEncode(MDS.Utility.NUtility.checkString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]))));
 
                oDatabase.ExecNonQuerySQLCommand(SqlComUpdate, ref nAffectedRowCount);


                if (nAffectedRowCount > 0)
                {
                    returnValue = SecureKey;
                }
                else
                {
                    returnValue = "";
                }
            }
        }
        finally
        {
            oDatabase.DBDisconnect();
        }
		return returnValue;
	}

    /// <summary>
    /// 依現在時間, 年、月、日、時、分、秒 組出14位元字串, 並回傳
    /// </summary>
    /// <returns>字串(例:20090202080505)</returns>
    public static string GetNewID()
    {
        DateTime oDateTime = new DateTime(DateTime.Now.Ticks);
        return oDateTime.Year.ToString() + oDateTime.Month.ToString().PadLeft(2, '0') + oDateTime.Day.ToString().PadLeft(2, '0') + oDateTime.Hour.ToString().PadLeft(2, '0') + oDateTime.Minute.ToString().PadLeft(2, '0') + oDateTime.Second.ToString().PadLeft(2, '0');
    }


    protected static string GetIPAddress()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses[0];
            }
        }

        //return HttpContext.Current.Request.Params["HTTP_CLIENT_IP"] ?? HttpContext.Current.Request.UserHostAddress;

        return context.Request.ServerVariables["REMOTE_ADDR"];
    }
}
