using System;
using System.Web;
using System.Web.UI;
using System.Data;
using MDS.Database;
using System.IO;
using System.Collections;
using System.Web.UI.WebControls;

/// <summary>
/// BasePage 的摘要描述
/// </summary>
public class BasePage : System.Web.UI.Page
{
    static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private bool IsSessionAlive = false;
    private string userID = "";
    public BasePage()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//

        logger.Debug("client ip:" + GetIPAddress());
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


    #region 公開屬性
    public static string AuthenticityToken;
    public string FunctionID;
    public string FunctionName;
    public string ModuleName;
    //取得目前路徑,傳回根目錄相對位置包含"~"
    public string CurrentPage
    {
        get
        {
            if (Request.ApplicationPath == @"/")
            {
                return "~" + Request.ServerVariables.Get("Path_Info").ToString();
            }
            else
            {
                return Request.ServerVariables.Get("Path_Info").ToString().Replace(Request.ApplicationPath, "~");
            }
        }
    }

    #endregion
    protected override void OnPreInit(EventArgs e)
    {
        string sJavascript = "";
        string PageFileName = "";
        PageFileName = System.IO.Path.GetFileName(Request.PhysicalPath).ToUpper();

        Response.Charset = "utf-8";
        sJavascript = "alert('System timeout, please login again.');" + "window.top.location='" + Request.ApplicationPath + "/Verify.aspx';";

        if (PageFileName != "VERIFY.ASPX")
        {

            IsSessionAlive = CheckSession();
        }
        else
        {
            IsSessionAlive = true;
        }

        if (IsSessionAlive == true)
        {
            base.OnPreInit(e);

           // logger.Debug("session 有效!"); 
        }
        else
        {
            logger.Debug("session 失效!");

            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), sJavascript, true);
        }
    }


    /// <summary>
    /// 記錄使用者的動作:新增、修改、刪除、查詢....
    /// </summary>
    private void logUserAction(EventArgs e)
    {

        string loginfo = String.Format("SYSLOG-IP:{2}-user action-UserID:[{0}] FunctionName:[{1}]", Session["UserID"], Session["FunctionName"], GetIPAddress());

        logger.Debug(loginfo);
    }

    protected override void OnInit(EventArgs e)
    {
        if (IsSessionAlive == true)
        {
            base.OnInit(e);
        }
    }

    protected override void OnInitComplete(EventArgs e)
    {
        if (IsSessionAlive == true)
        {
            

 
            base.OnInitComplete(e);
        }
    }

    protected override void OnPreLoad(EventArgs e)   
    {
        if (IsSessionAlive == true)
        {
            base.OnPreLoad(e);
        }
        
    }

    protected override void OnLoad(EventArgs e)
    {


        
        if (IsSessionAlive == true)
        {
            Database db = new Database();
            DataTable dt = new DataTable();

            int nRetC = -1;
            int nRet = -1;
            
            try
            {
                nRetC = db.DBConnect();
                if (nRetC == 0)
                {
                    string strSQL = "SELECT   a.[SysFuncID] ,a.[SysModID]  ,a.[FunctionDesc]  ,b.[ModuleDesc]  FROM  [SystemFunction] a inner join [SystemModule] b   on a.[SysModID] = b.[SysModID] ";
                    strSQL += " where   a.[PageLink] like '%'+@PhysicalPath+'%' COLLATE SQL_Latin1_General_CP1_CI_AS"; //不分大小寫

                    System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(strSQL, db.getOcnn());

                    SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PhysicalPath", System.IO.Path.GetFileName(Request.PhysicalPath)));

                    nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        FunctionID = dt.Rows[i]["SysFuncID"].ToString();
                        FunctionName = dt.Rows[i]["FunctionDesc"].ToString();
                        ModuleName = dt.Rows[i]["ModuleDesc"].ToString();
                        if (Session["UserID"] != null)
                        {
                           
                            Session["ModuleName"] = ModuleName;
                            Session["FunctionName"] = FunctionName;
                        }


                    }

                    if (dt.Rows.Count ==0 && Session["ModuleName"] != null)
                    {
                        FunctionName = Session["FunctionName"].ToString();
                        ModuleName = Session["ModuleName"].ToString();
                    }



                    logUserAction(e);
                }
                 
                  
                
            }
            finally
            {
                if (nRetC == 0)
                {
                    dt.Dispose();
                    db.DBDisconnect();
                }
            }
            base.OnLoad(e);
        }
    }

    protected override void OnLoadComplete(EventArgs e)
    {
        if (IsSessionAlive == true)
        {
            base.OnLoadComplete(e);
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        if (IsSessionAlive == true)
        {
            base.OnPreRender(e);
        }
    }

    protected override void OnSaveStateComplete(EventArgs e)
    {
        if (IsSessionAlive == true)
        {
            base.OnSaveStateComplete(e);
        }
    }

    protected override void OnUnload(EventArgs e)
    {
        if (IsSessionAlive == true)
        {
            base.OnUnload(e);
        }
    }

    private bool CheckSession()
    {
        if (HttpContext.Current.Session["UserID"] != null)
        {
            if (HttpContext.Current.Session["UserID"].ToString() == "")
                return false;
            else
                return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// MessageBox(Server端)
    /// </summary>
    /// <param name="Message">要alert的文字</param>
    public void MessageBox(string Message)
    {
        ClientScriptManager cs = Page.ClientScript;
        string sScript = "";

        if (Message == null)
        {
            Message = "String is NULL.";
        }
        else
        {

        }


        Message = Message.Replace("'", "\\'");
        Message = Message.Replace("\r\n", "\\n");
        sScript = String.Format("alert('{0}');", MDS.Utility.NUtility.trimBad(MDS.Utility.NUtility.HtmlEncode(Message)));

        cs.RegisterClientScriptBlock(this.GetType(), System.Guid.NewGuid().ToString(), sScript, true);
    }


   

    /// <summary>
    /// 取得Wording
    /// </summary>
    /// <param name="WordingID"></param>
    /// <returns></returns>
    public static string ParseWording(string WordingID)
    {
        string sWording = "";

        /*
        object o_LocalRs = base.GetLocalResourceObject(WordingID);
        
        if (o_LocalRs != null)
        {
            if (o_LocalRs.GetType() == typeof(string))
                sWording = o_LocalRs.ToString();
        }
        else
        {
            object o_Global = base.GetGlobalResourceObject("DMSWording", WordingID);
            if (o_Global != null)
            {
                if (o_Global.GetType() == typeof(string))
                    sWording = o_Global.ToString();
            }
        }
         */

        System.Resources.ResourceSet rs = Resources.DMSWording.ResourceManager.GetResourceSet(System.Globalization.CultureInfo.CurrentCulture, true, true);

        object o = rs.GetObject(WordingID);


        if (o != null)
        {
            if (o.GetType() == typeof(string))
                sWording = o.ToString();
        }

        if (sWording == "")
            sWording = WordingID;
        
        return sWording;
    }


    /// <summary>
    /// 系統功能權限檢核
    /// </summary>
    /// <param name="inDMSRightValue">待確認的功能權限</param>
    /// <returns>true:有權限 false:無權限</returns>
    public bool CheckUserRight(string inDMSRightValue)
    {
        string inDMSRoleIDRight = Session["DMSRoleIDRight"].ToString();

        if ((inDMSRoleIDRight != "") && (inDMSRightValue != ""))
        {
            if (inDMSRoleIDRight.Contains(inDMSRightValue))
                return true;
            else
                return false;
        }
        else
            return false;
    }


    /// <summary>
    /// formValidator ajax的結果
    /// </summary>
    /// <param name="validateId">validateId</param>
    /// <param name="validateError">validateError</param>
    /// <param name="Result">validateResult</param>
    /// <returns></returns>
    public static string FormValidateResult(string validateId, string validateError, bool validateResult)
    {
        return ("{\"jsonValidateReturn\":[\"" + validateId + "\",\"" + validateError + "\",\"" + validateResult.ToString().ToLower() + "\"]}");
    }


    /// <summary>
    /// 傳回SysAttribute裡的值
    /// </summary>
    /// <param name="SECTION">SECTION</param>
    /// <param name="KEY">KEY</param>
    /// <returns>DATA</returns>
    public static string CheckSysAttribute(string SECTION, string KEY)
    {
        Database db = new Database();
        DataTable dt = new DataTable();

        string strSQL = "";
        int nRet = -1;
        

        string DATA = "";

        nRet = db.DBConnect();
        try
        {
            if (nRet == 0)
            {
                strSQL = "SELECT DATA FROM SysAttribute WHERE SECTION =@SECTION AND [KEY] =@KEY";

                System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(strSQL, db.getOcnn());
                SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SECTION", SECTION));
                SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@KEY", KEY));

                nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);

               
                if (nRet == 0)
                {
                    if (dt.Rows.Count > 0)
                        DATA = dt.Rows[0]["DATA"].ToString();
                }
            }
        }
        finally
        {
            dt.Clear();
            dt.Dispose();
            dt = null;
            db.DBDisconnect();
            db = null;
        }
        return DATA;
    }


    /// <summary>
    /// 確認系統主選單列表是否有該功能ID
    /// </summary>
    /// <param name="SysModID">SysModID</param>
    /// <returns>true:有 false:無</returns>
    public static bool CheckSystemModule(string SysModID)
    {
        Database db = new Database();
        DataTable dt = new DataTable();

        string strSQL = "";
        int nRet = -1;

        bool Result = false;

        nRet = db.DBConnect();
        try
        {
            if (nRet == 0)
            {
                strSQL = "SELECT COUNT(*) AS DATA FROM SystemModule WHERE SysModID =@SysModID";

                System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(strSQL, db.getOcnn());
                SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SysModID", SysModID));

                nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);

                if (nRet == 0)
                {
                    if (dt.Rows[0]["DATA"].ToString() == "1")
                        Result = true;
                }
            }
        }
        finally
        {
            dt.Clear();
            dt.Dispose();
            dt = null;
            db.DBDisconnect();
            db = null;
        }
        return Result;
    }


    /// <summary>
    /// 確認系統次選單列表是否有該功能ID
    /// </summary>
    /// <param name="SysFuncID">SysFuncID</param>
    /// <returns>true:有 false:無</returns>
    public static bool CheckSystemFunction(string SysFuncID)
    {
        Database db = new Database();
        DataTable dt = new DataTable();

        string strSQL = "";
        int nRet = -1;

        bool Result = false;

        nRet = db.DBConnect();
        try
        {
            if (nRet == 0)
            {
                strSQL = "SELECT COUNT(*) AS DATA FROM SystemFunction WHERE SysFuncID =@SysFuncID";


                System.Data.SqlClient.SqlCommand SqlCom = new System.Data.SqlClient.SqlCommand(strSQL, db.getOcnn());
                SqlCom.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SysFuncID", SysFuncID));
                nRet = db.ExecQuerySQLCommand(SqlCom, ref dt);

                if (nRet == 0)
                {
                    if (dt.Rows[0]["DATA"].ToString() == "1")
                        Result = true;
                }
            }
        }
        finally {
            dt.Clear();
            dt.Dispose();
            dt = null;
            db.DBDisconnect();
            db = null;
        }

        return Result;
    }

    public static string _getSQL(System.Data.SqlClient.SqlCommand _sqlCom)
    {
        string query = "fail to get sql!";
        try
        {
            query = _sqlCom.CommandText;
            foreach (System.Data.SqlClient.SqlParameter p in _sqlCom.Parameters)
            {
                query = query.Replace(p.ParameterName, p.Value.ToString());
            }
        }
        catch (Exception ex)
        {
            query = "fail to get sql:" + ex.Message;
        }

        return query;
    }


    public void callBtnController() {

        
       
        
        ContentPlaceHolder toolBar = (ContentPlaceHolder)this.Master.FindControl("PageTitlePlaceHolder");
        ButtonController btnController = new ButtonController(toolBar, Session["FunctionName"].ToString(), Session["UserID"].ToString());
        btnController.quarryDB_defaultButton_false();
        btnController.quarryDB_AccountID_Btuuon_true();

    }

    
  

}

public class CReturnData
{
    public int nRet = -1;
    public string outMsg = "";
    public string returnData = "";
}


