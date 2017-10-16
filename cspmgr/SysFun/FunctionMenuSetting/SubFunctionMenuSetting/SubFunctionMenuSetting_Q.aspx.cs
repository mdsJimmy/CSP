using MDS.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SysFun_FunctionMenuSetting_Q : BasePage
{

    /*ListView固定程式碼, 取得目前排序欄位, 排序方向, 頁數 START*/
    protected string ListViewSortKey = "";
    protected string ListViewSortDirection = "";
    protected string PageNo = "0";
    /*ListView固定程式碼, 取得目前排序欄位, 排序方向, 頁數 END*/

    protected string strQueryCondi = "";
  


    protected void Page_PreInit(object sendor, EventArgs e)
    {
        
        QueryList();



    }
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        foreach (string key in Request.QueryString)
        {
            sb.Append("&" + key + "=" + Request.QueryString[key]);
        }

        strQueryCondi = HttpUtility.UrlEncode(sb.ToString(), Encoding.UTF8);
        //html輸出的時候做urlEncode
        strQueryCondi = sb.ToString();

       

       

    }


  

    private void QueryList()
    {


        string strFunctionDesc = "";
       
       
        if (SubFunctionList == null)
        {
            ContentPlaceHolder MySecondContent = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
            SubFunctionList = MySecondContent.FindControl("SubFunctionList") as ASP.dmscontrol_olistview_ascx;

        }


        if (SubFunctionList != null)
        {
            //加入欄位Start      
            SubFunctionList.AddCol("Button名稱", "ActionDesc", "LEFT","14%");
            SubFunctionList.AddCol("Button ID", " SysActionID", "LEFT", "19%");
            SubFunctionList.AddCol("功能名稱", "FunctionDesc", "LEFT", "14%");
            SubFunctionList.AddCol("功能 ID", "SysFuncID", "LEFT", "19%");
            SubFunctionList.AddCol("模組", "ModuleDesc", "LEFT", "14%");
            SubFunctionList.AddCol("模組 ID", "SysModID", "LEFT", "20%");
           

            //加入欄位End

            //設定Key值欄位
            SubFunctionList.DataKeyNames = "SysActionID"; //Key以,隔開

            //設定是否顯示CheckBox(預設是true);
            //BosomServiceList.IsUseCheckBox = false;

            StringBuilder sbSQL = new StringBuilder();
            //設定SQL        
            sbSQL.Append(@"select 
                            a.ActionDesc as ActionDesc 
                            ,a.SysActionID as SysActionID
                            ,b.FunctionDesc as FunctionDesc
                            ,b.SysFuncID as SysFuncID
                            ,c.ModuleDesc as ModuleDesc
                            ,b.SysModID as SysModID
                            from SystemAction a
                            ,SystemFunction b 
                            ,SystemModule c 
                            where a.SysFuncID=b.SysFuncID 
                            and a.SysModID=c.SysModID ");

            if (!string.IsNullOrEmpty(Request.QueryString["function"])) {
                strFunctionDesc = MDS.Utility.NUtility.trimBad(Request.QueryString["function"]);
                DropDownList1.SelectedValue = Request.QueryString["function"];
            }

            if (strFunctionDesc != "") {
                sbSQL.Append("and b.SysFuncID =@strSysFuncID ");
                SubFunctionList.putQueryParameter("strSysFuncID", strFunctionDesc);
            }
          

            sbSQL.Append("order by sysfuncid ");

          

          

            //設定SQL;
            SubFunctionList.SelectString = sbSQL.ToString();
            //完成參數值填充
            SubFunctionList.prepareStatement();


            string showSql = sbSQL.ToString();
            

            //設定每筆資料按下去的Javascript function
            SubFunctionList.OnClickExecFunc = "";

            //設定每頁筆數
            SubFunctionList.PageSize = 26;

            //接來自Request的排序欄位、排序方向、目前頁數
            ListViewSortKey = Request.Params["ListViewSortKey"];
            ListViewSortDirection = Request.Params["ListViewSortDirection"];
            PageNo = Request.Params["PageNo"];

            //設定排序欄位及方向
            if (!string.IsNullOrEmpty(ListViewSortKey) && !string.IsNullOrEmpty(ListViewSortDirection))
            {
                SubFunctionList.ListViewSortKey = ListViewSortKey;
                SubFunctionList.ListViewSortDirection = (SortDirection)Enum.Parse(typeof(SortDirection), ListViewSortDirection);
            }

            //設定目前頁數
            if (!string.IsNullOrEmpty(PageNo))
                SubFunctionList.PageNo = int.Parse(PageNo);


        }


    }

    [System.Web.Services.WebMethod]
    public static CReturnData Delete_Action(string strNewsList)
    {
        CReturnData myData = new CReturnData();
        Database db = new Database();
       

        int nRet = -1;

        /*連線DB*/

        SqlTransaction sqlTrans = null;

        try
        {

            myData.nRet = db.DBConnect();
            myData.outMsg = db.outMsg;

            string[] strDelProId = strNewsList.Split(new String[] { "^^" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string delId in strDelProId)
            {

              
                string strSQL = "delete from SystemAction where SysActionID=@SysActionID ";

               /*連線DB*/
                SqlCommand SqlCom = new SqlCommand(strSQL, db.getOcnn());
                SqlCom.Parameters.Add(new SqlParameter("@SysActionID", delId));
              
                nRet = SqlCom.ExecuteNonQuery();

                strSQL = "delete from SecurityUserAccount_ActionRole where SysActionID=@SysActionID ";
                SqlCom = new SqlCommand(strSQL, db.getOcnn());
                SqlCom.Parameters.Add(new SqlParameter("@SysActionID", delId));
                nRet = SqlCom.ExecuteNonQuery();

                string outMsg = db.outMsg;

                
                myData.nRet = db.nRet;
                myData.outMsg = db.outMsg;
                

                if (nRet == -1)//失敗
                {
                    throw new Exception(db.outMsg);
                }

            }//for 


        }
        catch (Exception ex)
        {
            
            if (sqlTrans != null)
            {
                sqlTrans.Rollback();
            }

            throw ex;
        }
        finally
        {
            
            db.getOcnn().Close();
            db.DBDisconnect();
        }

        return myData;
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            ListViewSortKey = SubFunctionList.ListViewSortKey;
            ListViewSortDirection = SubFunctionList.ListViewSortDirection.ToString();
            PageNo = SubFunctionList.PageNo.ToString();
        }
    }
}