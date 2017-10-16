using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MIP.Utility;
using MDS.Database;
using System.Data;
using System.Diagnostics;
using MIPLibrary;
using System.Data.SqlClient;
using System.Text;

public partial class yuantalife_web_YL0010A : BasePage
{
    protected string strQueryCondi = "";
    protected string PageNo = "0";
    protected string result = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        Debug.Write("Page_Load");
        strQueryCondi = MDS.Utility.NUtility.trimBad(Request.QueryString["queryCondi"]);
        strQueryCondi = HttpUtility.UrlDecode(strQueryCondi, Encoding.UTF8);
        //初始化設定種類
        setNewKind();
        //初始化設定發布對象 
        
        setApp4();
    }

    private void setAppType()//代碼
    {
        
       
       
    }

    private void setNewKind()//是否啟用
    {

        _selNewsKind.Items.Add(new ListItem("訊息置中", "M"));
        _selNewsKind.Items.Add(new ListItem("訊息左靠", "L"));
        _selNewsKind.Items.Add(new ListItem("訊息右靠", "R"));

        //發布對象                    
        if (_selNewsKind.Value.Equals("M"))
        {
            _selNewsKind.Items[0].Selected = true;
        }
        else if (_selNewsKind.Value.Equals("L"))
        {
            _selNewsKind.Items[1].Selected = true;
        }
        else if (_selNewsKind.Value.Equals("R"))
        {
            _selNewsKind.Items[2].Selected = true;
        }

    }
    private void setApp4()//是否啟用
    {
 
        //_selAPP4.Items.Add(new ListItem("請選擇", ""));
        _selAPP4.Items.Add(new ListItem("是", "0"));
        _selAPP4.Items[0].Selected = true;

        // 發布對象

        /**
        if (_selAPP4.Value.Equals("1"))
        {
            _selAPP4.Items[1].Selected = true;
        }
        else if (_selAPP4.Value.Equals("0"))
        {
            _selAPP4.Items[2].Selected = true;
        }*/
    }



    /// <summary>
    /// 新增最新公告
    /// </summary>
    /// <param name="strAppType">代碼</param> CKEY
    /// <param name="strNewsKind">是否啟用</param>CSTATUS
    /// <param name="strNewsTitle">說明</param>CNOTE
    /// <param name="strInfo">資料內容</param>CVALUE
    /// <param name="strAPP4">app顯示</param>APPLY4

    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static CReturnData AddNewsProcess(string strAppType, string strNewsKind, string strAPP4, string strNewsTitle, string strInfo)
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
            int  buildId = MIPUtil.getFILE_INDEX_SEQ(db.getOcnn());

            try
            {
                //最新公告新增
                StrSQL = "insert into MIP_SYS_INFO(CKEY,CVALUE,CNOTE,CSTATUS,APPLY4)values" +
                        "(@CKEY,@CVALUE,@CNOTE,@CSTATUS,@APPLY4)";

                /*連線DB*/
                SqlCommand SqlCom = new SqlCommand(StrSQL, db.getOcnn());

                SqlCom.Parameters.Add(new SqlParameter("@CKEY", strAppType));//商品索引

                SqlCom.Parameters.Add(new SqlParameter("@CSTATUS", strNewsKind));//公告標題

                SqlCom.Parameters.Add(new SqlParameter("@APPLY4", strAPP4));//公告內容

                SqlCom.Parameters.Add(new SqlParameter("@CNOTE", strNewsTitle));//應用程式類別

                SqlCom.Parameters.Add(new SqlParameter("@CVALUE", strInfo));//種類 A01 熱門,A02 新聞,A03 保戸,A04 新知,A05 重要

               

                nRet = SqlCom.ExecuteNonQuery();
                //nRet = db.ExecQuerySQLCommand(StrSQL, ref dt);

                string outMsg = db.outMsg;

                Debug.Write("nRet:" + nRet);
                Debug.Write("outMsg:" + outMsg);
               
            }
            catch (Exception ex)
            {
                Debug.Write("YL0140A Exception :" + ex.Message);
             
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

    protected void btnUpload_Click(object sender, EventArgs e)
    {



    }


}