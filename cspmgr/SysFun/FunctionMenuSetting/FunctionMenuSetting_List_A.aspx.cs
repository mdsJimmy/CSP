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
    protected MipSystemModule mip;
    protected string result = "";

    protected string strNewsId = "";
    protected string strSysModID = "";//系統模組ID
    protected string strSysFuncID = "";//系統id
    protected string strFunctionDesc = "";//FunctionDesc
    protected string strPageLink = "";//url連結
    protected string strPic = "";//圖片
    protected string strOrder = "";//排序
    protected string striDisplay = "";//是否顯示




    //protected string _CreateStartDate = "";
    //protected string _CreateEndDate = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        strQueryCondi = Request.QueryString["queryCondi"];
        strQueryCondi = HttpUtility.UrlDecode(strQueryCondi, Encoding.UTF8);


        



        //初始化設定種類
        selSystemModule();
        selPic();
        //seliOrder();
        selIdisPlay();
        //初始化設定發布對象 

    }

   

    private void selSystemModule()
    {
        mip = new MipSystemModule();
        mip.SQL = " select * from SystemModule ORDER BY iOrder ";
        mip.KEY = "SysModID";
        mip.NAME = "ModuleDesc";
        List<CodeVo> codeVoList = mip.getCodeListByLevel();
        _sysModID.Items.Add(new ListItem(MDS.Utility.NUtility.HtmlEncode("請選擇"), MDS.Utility.NUtility.HtmlEncode("")));
        foreach (CodeVo codeVo in codeVoList)
        {
            ListItem listItem = new ListItem(MDS.Utility.NUtility.HtmlEncode(codeVo.name), MDS.Utility.NUtility.HtmlEncode(codeVo.key));
            if (codeVo.key.Equals(strSysModID))
            {
                listItem.Selected = true;
            }
            _sysModID.Items.Add(listItem);
        }
    }

    private void selPic()
    {
        mip = new MipSystemModule();
        mip.SQL = " select DISTINCT Pic , (case when Pic IS NULL  then '無' else Pic end)as Picc from SystemFunction ";

        mip.KEY = "Pic";
        mip.NAME = "Picc";

        List<CodeVo> codeVoList = mip.getCodeListByLevel();
        _pic.Items.Add(new ListItem(MDS.Utility.NUtility.HtmlEncode("請選擇"), MDS.Utility.NUtility.HtmlEncode("")));
        foreach (CodeVo codeVo in codeVoList)
        {
            ListItem listItem = new ListItem(MDS.Utility.NUtility.HtmlEncode(codeVo.name), MDS.Utility.NUtility.HtmlEncode(codeVo.key));
            if (codeVo.key.Equals(strPic))
            {
                listItem.Selected = true;
            }
            _pic.Items.Add(listItem);
        }
    }
    //private void seliOrder()
    //{
    //    mip = new MipSystemModule();
    //    mip.SQL = " select DISTINCT iOrder  from SystemFunction  ";

    //    mip.KEY = "iOrder";
    //    mip.NAME = "iOrder";

    //    List<CodeVo> codeVoList = mip.getCodeListByLevel();
    //    _iOrder.Items.Add(new ListItem(MDS.Utility.NUtility.HtmlEncode("請選擇"), MDS.Utility.NUtility.HtmlEncode("")));
    //    foreach (CodeVo codeVo in codeVoList)
    //    {
    //        ListItem listItem = new ListItem(MDS.Utility.NUtility.HtmlEncode(codeVo.name), MDS.Utility.NUtility.HtmlEncode(codeVo.key));
    //        if (codeVo.key.Equals(strOrder))
    //        {
    //            listItem.Selected = true;
    //        }
    //        _iOrder.Items.Add(listItem);
    //    }
    //}

    private void selIdisPlay()
    {
        mip = new MipSystemModule();
        mip.SQL = " select DISTINCT iDisplay ,(case when iDisplay = 1 then '是' else '否' end)as iDisplayy   from SystemModule ORDER BY iDisplay Desc  ";

        mip.KEY = "iDisplay";
        mip.NAME = "iDisplayy";

        List<CodeVo> codeVoList = mip.getCodeListByLevel();
        _iDisplay.Items.Add(new ListItem(MDS.Utility.NUtility.HtmlEncode("請選擇"), MDS.Utility.NUtility.HtmlEncode("")));
        
        foreach (CodeVo codeVo in codeVoList)
        {
            ListItem listItem = new ListItem(MDS.Utility.NUtility.HtmlEncode(codeVo.name), MDS.Utility.NUtility.HtmlEncode(codeVo.key));
            if (codeVo.key.Equals(striDisplay))
            {
                listItem.Selected = true;
            }
            _iDisplay.Items.Add(listItem);
        }
        _iDisplay.Items.Add(new ListItem(MDS.Utility.NUtility.HtmlEncode("否"), MDS.Utility.NUtility.HtmlEncode(1)));
    }

    /// <summary>
    /// 查詢最新公告資訊
    /// </summary>
    /// <param name="proId"></param>
   
    /* protected string strNewsId = "";
    protected string strSysModID = "";//系統模組ID
    protected string strSysFuncID = "";//系統id
    protected string strFunctionDesc = "";//FunctionDesc
    protected string strPageLInk = "";//url連結
    protected string strPic = "";//圖片
    protected string strOrder="";//排序
    protected string striDisplay = "";//是否顯示*/

    [System.Web.Services.WebMethod]
    public static CReturnData AddNewsProcess(string strSysModID , string strFunctionDesc, string strPageLInk, string strPic, string strOrder,
        string striDisplay)
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
            int buildId = MIPUtil.getFILE_INDEX_SEQ(db.getOcnn());

            try
            {
                ////功能設定維護
                StrSQL = " INSERT INTO SystemFunction(SysFuncID, SysModID, FunctionDesc, PageLink, Pic, iOrder, iDisplay) " +
                         " VALUES(NEWID(), @SysModID,@FunctionDesc, @PageLink, @Pic, @iOrder, @iDisplay); ";
        


               

                SqlCommand SqlCom = new SqlCommand(StrSQL, db.getOcnn());

                SqlCom.Parameters.Add(new SqlParameter("@SysModID", strSysModID));//模組

                SqlCom.Parameters.Add(new SqlParameter("@FunctionDesc", strFunctionDesc));//公告內容

                SqlCom.Parameters.Add(new SqlParameter("@PageLink", strPageLInk));//uri連結

                SqlCom.Parameters.Add(new SqlParameter("@pic", strPic));//預設圖片
                             
                SqlCom.Parameters.Add(new SqlParameter("@iOrder", strOrder));//排序

                SqlCom.Parameters.Add(new SqlParameter("@iDisplay", striDisplay));//公告內容

                

                





                nRet = SqlCom.ExecuteNonQuery();
                //nRet = db.ExecQuerySQLCommand(StrSQL, ref dt);

                string outMsg = db.outMsg;

                Debug.Write("nRet:" + nRet);
                Debug.Write("outMsg:" + outMsg);

            }
            catch (Exception ex)
            {
                Debug.Write("FunctionMenuSetting_List_A Exception :" + ex.Message);

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
   

    private void _DynamicallySetPersonProperty()
    {
        System.Reflection.FieldInfo[] _FieldInfoArr = GetType().GetFields();
        for (int i = 0; i < _FieldInfoArr.Length; i++)
        {
            System.Reflection.FieldInfo _FieldInfo = _FieldInfoArr[i];
            _FieldInfo.SetValue(this, MDS.Utility.NUtility.check(_FieldInfo.GetValue(this)));
        }

    }


    private void deleteDynamicallySetPersonProperty()
    {
        this.strQueryCondi = MDS.Utility.NUtility.checkString(strQueryCondi);
        this.PageNo = MDS.Utility.NUtility.checkString(PageNo);


        this.strSysModID = MDS.Utility.NUtility.checkString(strSysModID);
        this.strSysFuncID = MDS.Utility.NUtility.checkString(strSysFuncID);
        this.strFunctionDesc = MDS.Utility.NUtility.checkString(strFunctionDesc);//種類 A01 熱門,A02 新聞,A03 保戸,A04 新知,A05 重要
        this.strPageLink = MDS.Utility.NUtility.checkString(strPageLink);
        this.strPic = MDS.Utility.NUtility.checkString(strPic);
        this.strOrder = MDS.Utility.NUtility.checkString(strOrder);
        this.striDisplay = MDS.Utility.NUtility.checkString(striDisplay);



    }
}