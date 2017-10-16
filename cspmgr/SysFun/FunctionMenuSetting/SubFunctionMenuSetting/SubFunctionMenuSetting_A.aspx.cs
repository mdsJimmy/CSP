using MDS.Database;

using MIP.Utility;
using MIPLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SysFun_FunctionMenuSetting_SubFunctionMenuSetting_SubFunctionMenuSetting_A : BasePage
{
    protected string strQueryCondi = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strQueryCondi = Request.QueryString["queryCondi"];
        strQueryCondi = HttpUtility.UrlDecode(strQueryCondi, Encoding.UTF8);
        builderID();

    }

    public void builderID() {
      
       
            Database db = new Database();
            DataTable dt = new DataTable();
            int nRet = 0;
            nRet = db.DBConnect();

            if (nRet != 0)
            {
                throw new Exception("伺服器無法連線，請稍候在試");
            }

            string sql = " select newId() as id; ";
            using (SqlCommand sqlcmd = new SqlCommand(sql, db.getOcnn()))
            {
                db.ExecQuerySQLCommand(sqlcmd, ref dt);
                string id = dt.Rows[0][0].ToString();
                TextBox3.Text = id.ToUpper();

            };
            db.DBDisconnect();

      
       
    }

    [System.Web.Services.WebMethod]
    public static CReturnData getProKind(string strProKind)
    {
        CReturnData myData = new CReturnData();
        MipSystemModule m = new MipSystemModule();
        m.SQL = "select FunctionDesc,SysFuncID,b.ModuleDesc,a.SysModID from SystemFunction a, SystemModule b where a.SysModID=b.SysModID and SysFuncID=@SysFuncID  order by a.iOrder ";
        m.KEY = "SysModID";
        m.NAME = "ModuleDesc";
        m.ps = new System.Data.SqlClient.SqlParameter("@SysFuncID", strProKind);

         List<Dictionary<string, string>> list = m.getProductList();

       
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        //string jsonResult = fastJSON.JSON.ToJSON(list);
        string jsonResult = serializer.Serialize(list);

        myData.returnData = jsonResult;

        return myData;



    }


    [System.Web.Services.WebMethod]
    public static CReturnData AddNewsProcess(string strActionID, string strActionDesc,string strButtonID, string strFunctionID, string strModID)
    {
        int nRet = -1;
        Database db = new Database();
        DataTable dt = new DataTable();
        CReturnData myData = new CReturnData();
        string StrSQL = " ";

        /*連線DB*/
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;


        if (myData.nRet == 0)
        {
            //key           
            

            try
            {
                ////功能設定維護
                StrSQL = @"INSERT INTO SystemAction (SysActionID,SysFuncID,ButtonID,SysModID,ActionDesc) 
                            values(
                                @strActionID
                                ,@strFunctionID
                                ,@strButtonID
                                ,@strModID
                                ,@strActionDesc

                            )";





                SqlCommand SqlCom = new SqlCommand(StrSQL, db.getOcnn());

                SqlCom.Parameters.Add(new SqlParameter("@strActionID", strActionID));
                SqlCom.Parameters.Add(new SqlParameter("@strFunctionID", strFunctionID));
                SqlCom.Parameters.Add(new SqlParameter("@strButtonID", strButtonID));
                SqlCom.Parameters.Add(new SqlParameter("@strModID", strModID));
                SqlCom.Parameters.Add(new SqlParameter("@strActionDesc", strActionDesc));








                nRet = SqlCom.ExecuteNonQuery();
                //nRet = db.ExecQuerySQLCommand(StrSQL, ref dt);

                string outMsg = db.outMsg;

             

            }
            catch (Exception ex)
            {
                

                throw ex;
            }
            finally
            {
                dt.Dispose();
                dt = null;
                db.getOcnn().Close();
                db.DBDisconnect();
            }

        }

        return myData;
    }
}