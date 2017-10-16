using MDS.Database;
using MIPLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class csp_web_CSP_0051Q : BasePage
{
    static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected string ListViewSortKey = "";
    protected string ListViewSortDirection = "";
    protected string PageNo = "0";
    protected string strQueryCondi = "";
    protected string str_dlType = "";
    protected string str_txtSrch = "";

    protected string str_hidInsertKey = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        callBtnController();
        StringBuilder sb = new StringBuilder();
        foreach (string key in Request.QueryString)
        {
            sb.Append("&" + key + "=" + Request.QueryString[key]);
        }
        strQueryCondi = HttpUtility.UrlEncode(sb.ToString(), Encoding.UTF8);
        //strQueryCondi = HttpUtility.UrlEncode(sb.ToString(), Encoding.UTF8);
        //html輸出的時候做urlEncode

        
    }

    protected void Page_PreInit(object sendor, EventArgs e)
    {
        QueryList();
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            ListViewSortKey = oListView.ListViewSortKey;
            ListViewSortDirection = oListView.ListViewSortDirection.ToString();
            PageNo = oListView.PageNo.ToString();
            
        }

       
    }

    private void QueryList()
    {
        Database db = new Database();
        try {
            
            db.DBConnect();
            str_hidInsertKey = MIPUtil.getFILE_INDEX_SEQ(db.getOcnn()).ToString();

        } catch (Exception ex) {
            throw ex;
        } finally {
            db.getOcnn().Close();
            db.DBDisconnect();
        }
        

        if (oListView == null)
        {
            ContentPlaceHolder MySecondContent = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
            oListView = MySecondContent.FindControl("oListView") as ASP.dmscontrol_olistview_ascx;
            //StrSearch = MySecondContent.FindControl("StrSearch") as System.Web.UI.HtmlControls.HtmlInputText;
        }

        if (oListView != null)
        {
            //加入欄位Start      
           
            oListView.AddCol("表單類別", "d_note", "CENTER", "auto");
            oListView.AddCol("表單名稱", "cont", "LEFT", "40%");
            oListView.AddCol("最後更新日期", " aldate", "CENTER", "auto");
            oListView.AddCol("排序", " Corder", "CENTER", "auto");
            oListView.AddCol("狀態", "cstatus", "CENTER", "auto");
          

            //加入欄位End

            //設定Key值欄位
            oListView.DataKeyNames = "HAPPY_ID"; //Key以,隔開

            ////設定是否顯示CheckBox(預設是true);
            ////BosomServiceList.IsUseCheckBox = false;

            StringBuilder sbSQL = new StringBuilder();
            //設定SQL        
            sbSQL.Append(@"SELECT
                            a.HAPPY_ID 
                            
                            , (CASE WHEN a.CSTATUS = 1 THEN '停用' WHEN a.CSTATUS = 0 THEN '啟用'  ELSE '未定義'END) cstatus  
                            ,a.Corder   
                            ,b.CNAME as b_note  
                            ,c.CNAME as c_note  
                            ,d.CNAME as d_note  
							,d.CNOTE as Cnote
                            ,(CASE WHEN a.FILE_KIND = 0 THEN '標題：'+a.TITLE+'　檔名：'+  a.F_NAME 　 WHEN a.FILE_KIND = 1 THEN '標題：'+  a.TITLE  +'　網址：'+ a.URL  ELSE '尋寶圖 URL：' +a.url  END) cont  
                            ,a.LDATE as aldate  
                            ,a.LUSER
                            ,(CASE WHEN a.ISTESTER = 1 THEN '否' WHEN a.ISTESTER = 0 THEN '是'  ELSE '未定義' END) as ISTESTER
                            FROM  MIP_HAPPY a, MIP_CODES b ,MIP_CODES c, MIP_CODES D  
                            where  
                            b.CKEY = a.CKEY1  
                            and c.CKEY = a.CKEY2  
                            and D.CKEY = a.CKEY3 
                            and a.CKEY2 != 'B2020'
                            and d.Cnote = '0' ");
            
            if (!string.IsNullOrEmpty(MDS.Utility.NUtility.trimBad(Request.QueryString["_dlType"]))) {
               
                if (MDS.Utility.NUtility.trimBad(Request.QueryString["_dlType"]) != "請選擇")
                {
                    str_dlType = MDS.Utility.NUtility.trimBad(Request.QueryString["_dlType"]);
                    string[] str_dlTypes = str_dlType.Split(new String[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                    //sbSQL.Append(" and b.CNAME=@b_note ");

                    //oListView.putQueryParameter("b_note", str_dlTypes[0]);
                    //sbSQL.Append(" and c.CNAME=@c_note ");
                    //oListView.putQueryParameter("Cnote", str_dlTypes[1]);
                    if (str_dlTypes.Length ==2) { 
                        sbSQL.Append(" and  d.CNAME =@dCNAME ");
                        oListView.putQueryParameter("dCNAME", str_dlTypes[0]);
                    
                        _dlType.SelectedValue=(MDS.Utility.NUtility.trimBad(str_dlTypes[1]))  ;
                    }
                }
             }
            if (!string.IsNullOrEmpty(MDS.Utility.NUtility.trimBad(Request.QueryString["_txtSrch"])))
            {

                str_txtSrch = MDS.Utility.NUtility.trimBad(Request.QueryString["_txtSrch"]);
                sbSQL.Append(" and ( a.TITLE like '%'+@txtSrch+'%'  or  a.F_NAME like '%'+@txtSrch+'%') ");
                oListView.putQueryParameter("txtSrch", str_txtSrch);

                

            }

            if (!string.IsNullOrEmpty(Request.QueryString["_chkIC"]) && string.IsNullOrEmpty(Request.QueryString["_chkRM"]))
            {
                sbSQL.Append("and exists(select * from MIP_HAPPY_TARGET e where e.HAPPY_ID=a.HAPPY_ID and e.DTYPE=0 and e.DEPT_ID='IC') ");
                oListView.putQueryParameter("DEPT_ID", Request.QueryString["_chkIC"]);
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["_chkRM"]) && string.IsNullOrEmpty(Request.QueryString["_chkIC"]))
            {
                sbSQL.Append("and exists(select * from MIP_HAPPY_TARGET e where e.HAPPY_ID=a.HAPPY_ID and e.DTYPE=0 and e.DEPT_ID='RM') ");
                oListView.putQueryParameter("DEPT_ID", Request.QueryString["_chkRM"]);
            }

            sbSQL.Append(" order by a.CKEY1, a.CKEY2, a.CKEY3, a.CORDER, a.LDATE ");



            //sbSQL.Append(" ORDER BY a.LDATA DESC,a.VDATE DESC ");

            //取得SQL;
            oListView.SelectString = sbSQL.ToString();
            oListView.prepareStatement();
            string showSql = sbSQL.ToString();
            Debug.Write(showSql);

            //設定每筆資料按下去的Javascript function
            oListView.OnClickExecFunc = "DoEdt()";

            //設定每頁筆數
            oListView.PageSize = 26;

            //接來自Request的排序欄位、排序方向、目前頁數
            ListViewSortKey = Request.Params["ListViewSortKey"];
            ListViewSortDirection = Request.Params["ListViewSortDirection"];
            PageNo = Request.Params["PageNo"];

            //設定排序欄位及方向
            if (!string.IsNullOrEmpty(ListViewSortKey) && !string.IsNullOrEmpty(ListViewSortDirection))
            {
                oListView.ListViewSortKey = ListViewSortKey;
                oListView.ListViewSortDirection = (SortDirection)Enum.Parse(typeof(SortDirection), ListViewSortDirection);
            }

            //設定目前頁數
            if (!string.IsNullOrEmpty(PageNo))
                oListView.PageNo = int.Parse(PageNo);

            
        }



    }

    [System.Web.Services.WebMethod]
    public static CReturnData Delete(string strProIdList)
    {
        CReturnData myData = new CReturnData();
        Database db = new Database();
        DataTable dt = new DataTable();

        int nRet = -1;

        /*連線DB*/
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;
        SqlTransaction sqlTrans = null;

        try
        {

            string[] strDelProId = strProIdList.Split(new String[] { "^^" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string proId in strDelProId)
            {
                StringBuilder sb = new StringBuilder("");

                //檔案索引_商品DM
                sb.Append(" DELETE FROM MIP_FILE_STORE WHERE FILE_INDEX=( ");
                sb.Append(" SELECT F_IDX FROM MIP_Happy WHERE Happy_ID=@Happy_ID );");
                sb.Append(" DELETE FROM MIP_HAPPY WHERE Happy_ID=@Happy_ID; delete from MIP_HAPPY_TARGET where Happy_ID=@Happy_ID ");


                string strSQL = sb.ToString();

                sqlTrans = db.getOcnn().BeginTransaction();


                /*連線DB*/
                SqlCommand SqlCom = new SqlCommand(strSQL, db.getOcnn(), sqlTrans);
                SqlCom.Parameters.Add(new SqlParameter("@Happy_ID", int.Parse(MDS.Utility.NUtility.checkString(proId))));
                Debug.Write("Transaction:" + SqlCom.Transaction);
                nRet = SqlCom.ExecuteNonQuery();

                string outMsg = db.outMsg;

                Debug.Write("nRet:" + nRet);
                Debug.Write("outMsg:" + outMsg);

                myData.nRet = db.nRet;
                myData.outMsg = db.outMsg;
                sqlTrans.Commit();

                if (nRet == -1)//失敗
                {
                    throw new Exception(db.outMsg);
                }

            }//for 


            return myData;
        }
        catch (Exception ex)
        {
            Debug.Write("PCA0050Q Exception :" + ex.Message);
            if (sqlTrans != null)
            {
                sqlTrans.Rollback();
            }

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

    /// <summary>
    /// 啟用資料(AJAX)
    /// </summary>
    /// <param name="dataList"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static CReturnData StartupData(string dataList)
    {
        CReturnData myData = new CReturnData();
        Database db = new Database();

        int nRet = -1;

        /*連線DB*/
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;
        SqlTransaction sqlTrans = null;

        try
        {
            sqlTrans = db.getOcnn().BeginTransaction();

            string[] strHAPPY_ID = dataList.Split(new String[] { "^^" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string HAPPY_ID in strHAPPY_ID)
            {
                string strSQL = "UPDATE MIP_HAPPY SET CSTATUS=0 WHERE HAPPY_ID=@HAPPY_ID; ";

                SqlCommand SqlCom = new SqlCommand(strSQL, db.getOcnn(), sqlTrans);
                SqlCom.Parameters.Add(new SqlParameter("@HAPPY_ID", HAPPY_ID));
                nRet = SqlCom.ExecuteNonQuery();

                string outMsg = db.outMsg;

                Debug.Write("nRet:" + nRet);
                Debug.Write("outMsg:" + outMsg);

                myData.nRet = db.nRet;
                myData.outMsg = db.outMsg;

               

            }//for      

            sqlTrans.Commit();

        }
        catch (Exception ex)
        {
            Debug.Write("PCA_0050Q->StartupData Exception :" + ex.Message);
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

    /// <summary>
    /// 停用資料(AJAX)
    /// </summary>
    /// <param name="dataList"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static CReturnData DenyData(string dataList)
    {
        CReturnData myData = new CReturnData();
        Database db = new Database();

        int nRet = -1;

        /*連線DB*/
        myData.nRet = db.DBConnect();
        myData.outMsg = db.outMsg;
        SqlTransaction sqlTrans = null;

        try
        {
            sqlTrans = db.getOcnn().BeginTransaction();

            string[] strCompanyArr = dataList.Split(new String[] { "^^" }, StringSplitOptions.RemoveEmptyEntries);

            

            foreach (string HAPPY_ID in strCompanyArr)
            {
                string strSQL = "UPDATE MIP_HAPPY SET CSTATUS=1 WHERE HAPPY_ID=@HAPPY_ID; ";

                SqlCommand SqlCom = new SqlCommand(strSQL, db.getOcnn(), sqlTrans);
                SqlCom.Parameters.Add(new SqlParameter("@HAPPY_ID", HAPPY_ID));
                nRet = SqlCom.ExecuteNonQuery();

                string outMsg = db.outMsg;

                Debug.Write("nRet:" + nRet);
                Debug.Write("outMsg:" + outMsg);

                myData.nRet = db.nRet;
                myData.outMsg = db.outMsg;

            }//for      

            sqlTrans.Commit();

        }
        catch (Exception ex)
        {
            
            Debug.Write("PCA_0050Q->DenyData Exception :" + ex.Message);
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
}