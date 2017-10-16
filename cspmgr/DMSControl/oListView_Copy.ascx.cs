using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class oListView_Copy : System.Web.UI.UserControl
{

    static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



    List<CollumnCopy> ListViewCollumns = new List<CollumnCopy>(); //欄位清單

    int TotalRowCount = 0; //總筆數;

    int iPageSize = 0; //每頁size;
    int iPageCount = 0; //總頁數;
    protected int PageCount = 0;
    int iCurrentIndex = 0; //當前筆數;
    int iCurrentPage = 0; //當前頁面(從0開始);

    bool bIsUseCheckBox = true; //是否有CheckBox
    string sDataKeyNames = ""; //Key欄位
    int iFixedCols = 0; //固定的欄位數
    int iHeaderRows = 1; //固定的標題列數
    string sOnClickExecFunc = ""; //按下每一筆要執行的Javascript
    string sOnClickExecFunc_view = ""; //按下每一筆要執行的Javascript
    string sSortKey = ""; //按下去的排序欄位

    string sListViewLocation_Left = ""; //Left位置
    string sListViewBody_Height = ""; //Listview高度
    string sListViewBody_Width = ""; //ListView的div寬度
    string sListViewContent_Width = ""; //ListView的Table寬度

    //System.Data.SqlClient.SqlParameter[] sSqlParameter;//參數SQL[20160824.Young]



    /// <summary>
    /// 為何要在 Init 事件中就動態建立控制項，而不是在 Load 事件中呢？
    /// 主要的原因是 Init 事件在 LoadViewState 之前發生，而 Load 事件是在 LoadViewState 之後。
    /// 你要在 LoadViewState 之前就把控制項準備好，機制才能由 ViewState 中載入更新控制項的屬性值。
    /// </summary>
    /// <param name="sendor"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sendor, EventArgs e)
    {
        logger.Debug("Page_Init");


        //放在這裡會造成頁面必須在Page_Init事件中初始化_ListView
        init_ListView();
    }


    //將_ListView初始化程序彙整到這邊，以利調整初始化的時機。
    private void init_ListView()
    {

        logger.Debug("init_ListView");

        if (PageSize == 0) PageSize = 10;
        //產生ListView的Template
        _ListView.LayoutTemplate = new CreateTemplate_Copy(myListItemType_Copy.Header, ListViewCollumns, DataKeyNames, IsUseCheckBox, OnClickExecFunc, OnClickExecFunc_view, TRTitle, IsUseBorder);
        _ListView.ItemTemplate = new CreateTemplate_Copy(myListItemType_Copy.Item, ListViewCollumns, DataKeyNames, IsUseCheckBox, OnClickExecFunc, OnClickExecFunc_view, TRTitle, IsUseBorder);
        _ListView.AlternatingItemTemplate = new CreateTemplate_Copy(myListItemType_Copy.AlternatingItem, ListViewCollumns, DataKeyNames, IsUseCheckBox, OnClickExecFunc, OnClickExecFunc_view, TRTitle, IsUseBorder);
        _ListView.EmptyDataTemplate = new CreateTemplate_Copy(myListItemType_Copy.EmptyData, ListViewCollumns, DataKeyNames, IsUseCheckBox, OnClickExecFunc, OnClickExecFunc_view, TRTitle, IsUseBorder);

    }

    protected void Page_Load(object sender, EventArgs e)
    {

        logger.Debug("Page_Load");

        //init_ListView();

        ////Response.Write("<br> start:" + DateTime.Now.ToString("yyyyMMddHHmmssms"));
        // _SqlDataSource.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DMSWeb"].ConnectionString;
        _SqlDataSource.ConnectionString = MDS.Database.Database.strDBConnect;
        ////Response.Write("<br> end:" + DateTime.Now.ToString("yyyyMMddHHmmssms"));



        //!IsPostBack時, 為第一次進入頁面, 如果有指定排序的欄位及方向時, ListView進行排序動作
        if (!IsPostBack)
        {
            if (ListViewSortKey != "")
            {
                _ListView.Sort(ListViewSortKey, ListViewSortDirection);
            }
        }
        //PostBack時, 取得__EVENTTARGET是哪個物件觸發PostBack, 再判斷物件是否為LinkButton & CommandName=Sort
        //若為LinkButton & CommandName=Sort, 將觸發排序的欄位及排序方向存下來, 並將換頁導至第一頁
        else
        {
            string EVENTTARGET = Request.Params["__EVENTTARGET"];
            if (!string.IsNullOrEmpty(EVENTTARGET))
            {
                object o = Page.FindControl(EVENTTARGET);
                if (o.GetType() == typeof(LinkButton))
                {
                    LinkButton lnk = (LinkButton)o;
                    if (lnk.CommandName == "Sort")
                    {
                        ListViewSortKey = lnk.CommandArgument;
                        ListViewSortDirection = _ListView.SortDirection;
                        PageNo = 0;
                    }
                }
            }
        }

        //!IsPostBack時, 為第一次進入頁面, 如果有指定頁數, 定位到新的頁面位置
        if (!IsPostBack && PageNo != 0)
        {
            //定位到新的頁面位置;
            _DataPager.SetPageProperties(PageNo * PageSize, PageSize, true);
        }

    }

    /// <summary>
    /// ListView Data Bound時一定會執行的FN;
    /// 執行於每次postback;
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ListView_DataBound(object sender, EventArgs e)
    {
        logger.Debug("ListView_DataBound");


        try
        {
            /*取得TemplatePagerField成員控制項 Start*/
            DropDownList ddl = (DropDownList)_DataPager.Controls[0].FindControl("Jumper");
            LinkButton btnFirstPage = (LinkButton)_DataPager.Controls[0].FindControl("FirstPageButton");
            LinkButton btnPreviousPage = (LinkButton)_DataPager.Controls[0].FindControl("PreviousPageButton");
            LinkButton btnNextPageButton = (LinkButton)_DataPager.Controls[0].FindControl("NextPageButton");
            LinkButton btnLastPage = (LinkButton)_DataPager.Controls[0].FindControl("LastPageButton");
            Label PagerInfoLabel = (Label)_DataPager.Controls[0].FindControl("PagerInfoLabel");
            /*取得TemplatePagerField成員控制項 End*/

            //取得總筆數;
            TotalRowCount = _DataPager.TotalRowCount;

            //取得PageSize設定值;
            //iPageSize = DataPager1.PageSize;

            //取得總頁數(總頁數*每頁筆數 至少要等於 總比數);
            iPageCount = (TotalRowCount / PageSize);
            //為了記錄總頁數，所以先在記錄起來，若總頁數為0將值給前端讓 button 無效
            PageCount = iPageCount;

            if (iPageCount * PageSize != TotalRowCount)
                iPageCount = iPageCount + 1;

            //取得CurrentIndex;
            iCurrentIndex = PageNo * PageSize;

            //依總頁數產生跳頁下拉選單;
            for (int i = 0; i < iPageCount; i++)
            {
                ddl.Items.Add(new ListItem((i + 1).ToString(), i.ToString()));
                if (i == PageNo)
                    ddl.Items.FindByValue(i.ToString()).Selected = true; /*選取當前的頁面(從0開始);*/
            }

            //有資料才控制按鈕;
            if (TotalRowCount != 0)
            {
                //開啟跳頁下拉選單;
                ddl.Enabled = true;

                //產生PagerInfoLabel內容 START;
                //若資料筆數小於等於總數;
                if ((iCurrentIndex + PageSize) <= TotalRowCount)
                    PagerInfoLabel.Text = "( " + (iCurrentIndex + 1).ToString() + " - " + (iCurrentIndex + PageSize).ToString() + " / " + TotalRowCount.ToString() + " )";
                //若資料筆數大於總數, 則顯示起始筆數 到 總數;
                else
                    PagerInfoLabel.Text = "( " + (iCurrentIndex + 1).ToString() + " - " + TotalRowCount.ToString() + " / " + TotalRowCount.ToString() + " )";
                //產生PagerInfoLabel內容 END;

                /*跳頁相關控制START;*/
                //第一頁;
                if (PageNo == 0)
                {
                    //只有一頁時;
                    if (iPageCount == 1)
                    {
                        btnFirstPage.Enabled = false;
                        btnPreviousPage.Enabled = false;
                        btnNextPageButton.Enabled = false;
                        btnLastPage.Enabled = false;
                    }
                    //超過一頁;
                    else
                    {
                        btnFirstPage.Enabled = false;
                        btnPreviousPage.Enabled = false;
                        btnNextPageButton.Enabled = true;
                        btnLastPage.Enabled = true;
                    }
                }
                //最後一頁;
                else if (PageNo == (iPageCount - 1))
                {
                    btnFirstPage.Enabled = true;
                    btnPreviousPage.Enabled = true;
                    btnNextPageButton.Enabled = false;
                    btnLastPage.Enabled = false;
                }
                //其中一頁(非頭尾頁面);
                else
                {
                    btnFirstPage.Enabled = true;
                    btnPreviousPage.Enabled = true;
                    btnNextPageButton.Enabled = true;
                    btnLastPage.Enabled = true;
                }
                /*跳頁相關控制END;*/
            }
            else
            {
                PagerInfoLabel.Text = "";
            }
        }
        catch (Exception ex)
        {
            logger.Fatal("ex", ex);
        }

    }

    /// <summary>
    /// 跳頁控制項On SelectedIndexChanged;
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Jumper_SelectedIndexChanged(object sender, EventArgs e)
    {

        logger.Debug("跳頁控制項 Jumper_SelectedIndexChanged");


        //取得跳頁控制項;
        DropDownList ddl = (DropDownList)sender;

        //取得PageSize;
        //iPageSize = DataPager1.PageSize;

        //從控制項中取得新的當前頁面;
        PageNo = int.Parse(ddl.SelectedValue);

        //取得SortKey, SortDirection
        ListViewSortKey = _ListView.SortExpression;
        ListViewSortDirection = _ListView.SortDirection;

        //定位到新的頁面位置;
        _DataPager.SetPageProperties(PageNo * PageSize, PageSize, true);
    }


    /// <summary>
    /// 按下PagerTemplate樣板中的按鈕(第一頁/上一頁/下一頁/最末頁)後會觸發;
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TemplatePagerField_OnPagerCommand(object sender, DataPagerCommandEventArgs e)
    {

        logger.Debug("TemplatePagerField_OnPagerCommand");


        //取得總筆數;
        TotalRowCount = _DataPager.TotalRowCount;

        //取得PageSize;
        PageSize = _DataPager.PageSize;

        //取得CurrentIndex;
        iCurrentIndex = e.Item.Pager.StartRowIndex;

        //取得SortKey, SortDirection
        ListViewSortKey = _ListView.SortExpression;
        ListViewSortDirection = _ListView.SortDirection;

        switch (e.CommandName)
        {
            case "FirstPage":
                //取得第一頁;
                PageNo = 0;
                //導至新的Current頁面;
                e.NewStartRowIndex = 0;
                e.NewMaximumRows = e.Item.Pager.MaximumRows;
                break;
            case "PreviousPage":
                //取得Previous頁面;
                PageNo = (iCurrentIndex / PageSize) - 1;
                //導至新的Current頁面;
                e.NewStartRowIndex = PageNo * PageSize;
                e.NewMaximumRows = e.Item.Pager.MaximumRows;
                break;
            case "NextPage":
                //取得Next頁面;
                PageNo = (iCurrentIndex / PageSize) + 1;
                //導至新的Current頁面;
                e.NewStartRowIndex = PageNo * PageSize;
                e.NewMaximumRows = e.Item.Pager.MaximumRows;
                break;
            case "LastPage":
                //取得最末頁(筆數可整除每頁筆數, 總頁數減1. 反之, 不減1);
                if ((TotalRowCount % PageSize) == 0)
                    PageNo = (TotalRowCount / PageSize) - 1;
                else
                    PageNo = (TotalRowCount / PageSize);
                //導至新的Current頁面;
                e.NewStartRowIndex = PageNo * PageSize;
                e.NewMaximumRows = e.Item.Pager.MaximumRows;
                break;
        }
    }

    /// <summary>
    /// 排序後保留排序欄位
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ListView_Sorted(object sender, EventArgs e)
    {
        ListViewSortKey = _ListView.SortExpression;
        ListViewSortDirection = _ListView.SortDirection;
    }

    #region 自訂的屬性及方法
    /// <summary>
    /// 設定ListView的SQL字串
    /// </summary>
    public string SelectString
    {
        set
        {
            _SqlDataSource.SelectCommand = value;
        }
    }

    //參數式查詢的[name,value]資料集合
    System.Collections.ArrayList QueryParameterList = new System.Collections.ArrayList();
    /// <summary>
    /// 當查詢SQL以參數方式提供時，接著透過呼叫putQueryParameter設定各個參數值，最後再呼叫 prepareStatement()，完成查詢設定。
    /// [Nick Young@20160825]
    /// </summary>
    public void putQueryParameter(string key, string value)
    {
        QueryParameterList.Add(new String[] { key, value });
    }

    public bool prepareStatement()
    {
        string[] name_val = null;
        for (int i = 0; i < this.QueryParameterList.Count; i++)
        {
            name_val = (string[])this.QueryParameterList[i];
            _SqlDataSource.SelectParameters.Add(name_val[0], name_val[1]);
        }

        return true;
    }



    /// <summary>
    /// 是否顯示CheckBox(預設為true)
    /// </summary>
    public bool IsUseCheckBox
    {
        set { bIsUseCheckBox = value; }
        get { return bIsUseCheckBox; }
    }

    /// <summary>
    /// 設定或取得Key值欄位，多欄位間以,隔開
    /// </summary>
    public string DataKeyNames
    {
        set
        {
            sDataKeyNames = value;
            string[] arrTmp = sDataKeyNames.Split(',');
            for (int i = 0; i < arrTmp.Length; i++)
            {
                arrTmp[i] = arrTmp[i].Trim();
            }
            _ListView.DataKeyNames = arrTmp;

        }
        get { return sDataKeyNames; }
    }

    /// <summary>
    /// 固定欄位數
    /// </summary>
    public int FixedCols
    {
        set
        {
            if (value > 0)
                iFixedCols = value;
        }
        get { return iFixedCols; }
    }

    /// <summary>
    /// 表頭行數
    /// </summary>
    public int HeaderRows
    {
        set
        {
            if (value > 1)
                iHeaderRows = value;
        }
        get { return iHeaderRows; }
    }

    /// <summary>
    /// 按下每筆資料時要執行的Javascript function
    /// Ex: DoEdt()
    /// </summary>
    public string OnClickExecFunc
    {
        set { sOnClickExecFunc = value; }
        get { return sOnClickExecFunc; }
    }

    /// <summary>
    /// 按下每筆資料時要執行的Javascript function
    /// Ex: DoEdt()
    /// </summary>
    public string OnClickExecFunc_view
    {
        set { sOnClickExecFunc_view = value; }
        get { return sOnClickExecFunc_view; }
    }
    /// <summary>
    /// 設定每頁顯示的資料筆數
    /// </summary>
    public int PageSize
    {
        set
        {
            iPageSize = value;
            _DataPager.PageSize = value;
        }
        get { return iPageSize; }
    }

    /* 指定位置先停用
    /// <summary>
    /// 設定ListView的位置(上)
    /// 只給數字即可(不需加單位)
    /// </summary>
    public string ListViewLocation_Top
    {
        set;
        get;
    }

    /// <summary>
    /// 設定ListView的位置(左)
    /// 只給數字即可(不需加單位), 若不指定, 從0開始
    /// </summary>
    public string ListViewLocation_Left
    {
        set { sListViewLocation_Left = value; }
        get
        {
            if (sListViewLocation_Left == "")
                sListViewLocation_Left = "0";
            return sListViewLocation_Left;
        }
    }
    */

    /// <summary>
    /// 設定ListView的高度(預設為填滿頁面)
    /// 設定時不需給單位
    /// </summary>
    public string ListViewBody_Height
    {
        set { sListViewBody_Height = value; }
        get
        {
            if (sListViewBody_Height == "")
                sListViewBody_Height = "($(window).height() - $('#div_ListView').position().top - 25) + 'px'";
            return sListViewBody_Height;
        }
    }

    /// <summary>
    /// 設定ListView外層Div的寬度(預設為頁面寬度)
    /// 設定時不需給單位
    /// </summary>
    public string ListViewBody_Width
    {
        set { sListViewBody_Width = value; }
        get
        {
            if (sListViewBody_Width == "")
                sListViewBody_Width = "'100%'";
            return sListViewBody_Width;
        }
    }

    /// <summary>
    /// 設定ListView內容Table的寬度(預設和Div同寬度)
    /// 使用情況：當ListView需要橫向捲軸時
    /// </summary>
    public string ListViewContent_Width
    {
        set { sListViewContent_Width = value; }
        get
        {
            if (sListViewContent_Width == "")
                sListViewContent_Width = "'100%'";
            return sListViewContent_Width;
        }
    }

    /// <summary>
    /// 設定或取得目前停留在第幾頁
    /// </summary>
    public int PageNo
    {
        set
        {
            iCurrentPage = value;
        }
        get
        {
            return iCurrentPage;
        }
    }

    /// <summary>
    /// 設定或取得使用者按下欄位排序時，該欄位的名稱
    /// </summary>
    public string ListViewSortKey
    {
        set
        {
            sSortKey = value;
        }
        get
        {
            return sSortKey;
        }
    }

    /// <summary>
    /// 設定或取得使用者按下欄位排序時，該欄位的排序方向
    /// </summary>
    public SortDirection ListViewSortDirection
    {
        set;
        get;
    }

    /// <summary>
    /// 當游標移到每一筆資料時，會顯示的提示
    /// </summary>
    public string TRTitle
    {
        set;
        get;
    }

    /// <summary>
    /// 是否有格線
    /// </summary>
    public bool IsUseBorder
    {
        set;
        get;
    }

    public void AddCol(int age)
    {
    }
   

    /// <summary>
    /// 加入欄位
    /// </summary>
    /// <param name="_TitleName">欄位的字</param>
    /// <param name="_SourceName">SQL的資料行</param>
    /// <param name="_Align">LEFT, CENTER, RIGHT</param>
    public void AddCol(string _TitleName, string _SourceName, string _Align)
    {
        CollumnCopy my_Collumn = new CollumnCopy();
        my_Collumn.TitleName = _TitleName.Trim();
        my_Collumn.SourceName = _SourceName.Trim();
        my_Collumn.Align = _Align.Trim();
        my_Collumn.Width = "";
        ListViewCollumns.Add(my_Collumn);
    }

    /// <summary>
    /// 加入欄位
    /// </summary>
    /// <param name="_TitleName">欄位的字</param>
    /// <param name="_SourceName">SQL的資料行</param>
    /// <param name="_Align">LEFT, CENTER, RIGHT</param>
    /// <param name="_Width">寬度 ex:20%, 100px, 不指定長度請給""</param>
    public void AddCol(string _TitleName, string _SourceName, string _Align, string _Width)
    {
        CollumnCopy my_Collumn = new CollumnCopy();
        my_Collumn.TitleName = _TitleName.Trim();
        my_Collumn.SourceName = _SourceName.Trim();
        my_Collumn.Align = _Align.Trim();
        my_Collumn.Width = _Width.Trim();
        ListViewCollumns.Add(my_Collumn);
    }

    /// <summary>
    /// 加入欄位
    /// </summary>
    /// <param name="_TitleName">欄位的字</param>
    /// <param name="_SourceName">SQL的資料行</param>
    /// <param name="_Align">LEFT, CENTER, RIGHT</param>
    /// <param name="_Width">寬度 ex:20%, 100px, 不指定長度請給""</param>
    /// <param name="_FormulaDefine">
    /// 格式:被取代的值1::取代成1^^被取代的值2::取代成2 ex: 0::XX^^1::YY, 當值為0時取代成XX, 當值為1時取代成YY
    /// </param>
    public void AddCol(string _TitleName, string _SourceName, string _Align, string _Width, string _FormulaDefine)
    {
        CollumnCopy my_Collumn = new CollumnCopy();

        my_Collumn.TitleName = _TitleName.Trim();
        my_Collumn.SourceName = _SourceName.Trim();
        my_Collumn.Align = _Align.Trim();
        my_Collumn.Width = _Width.Trim();

        if (!string.IsNullOrEmpty(_FormulaDefine))
        {
            string[] arrFormulaDefine = _FormulaDefine.Split(new string[] { "^^" }, StringSplitOptions.RemoveEmptyEntries);
            string[,] tmp = new string[arrFormulaDefine.Length, 2];
            for (int i = 0; i < arrFormulaDefine.Length; i++)
            {
                tmp[i, 0] = arrFormulaDefine[i].Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries)[0];
                tmp[i, 1] = arrFormulaDefine[i].Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries)[1];
            }
            my_Collumn.FormulaDefine = tmp;
        }

        ListViewCollumns.Add(my_Collumn);
    }
    #endregion
}

/// <summary>
/// 欄位設定
/// TitleName:欄位上的字
/// SourceName:DB欄位
/// Align:LEFT, CENTER, RIGHT
/// </summary>
struct CollumnCopy
{
    public string TitleName;
    public string SourceName;
    public string Align;
    public string Width;
    public string[,] FormulaDefine;
}

// 摘要:
//     
/// <summary>
/// 指定清單控制項中項目的型別。(擴充自System.Web.UI.WebControls.ListItemType)
/// </summary>
public enum myListItemType_Copy
{
    // 摘要:
    //     清單控制項的頁首。這不是資料繫結的。
    Header = 0,
    //
    // 摘要:
    //     清單控制項的頁尾 (Footer)。這不是資料繫結的。
    Footer = 1,
    //
    // 摘要:
    //     清單控制項中的項目。這是資料繫結的。
    Item = 2,
    //
    // 摘要:
    //     替代 (以零起始的偶數索引) 儲存格中的項目。這是資料繫結的。
    AlternatingItem = 3,
    //
    // 摘要:
    //     清單控制項中選取的項目。這是資料繫結的。
    SelectedItem = 4,
    //
    // 摘要:
    //     清單控制項中目前為編輯模式的項目。這是資料繫結的。
    EditItem = 5,
    //
    // 摘要:
    //     清單控制項中項目之間的分隔符號。這不是資料繫結的。
    Separator = 6,
    //
    // 摘要:
    //     顯示巡覽至與 System.Web.UI.WebControls.DataGrid 控制項相關不同網頁的控制項之頁面巡覽區。這不是資料繫結的。
    Pager = 7,
    //
    // 摘要:
    //     清單控制項中無資料的項目。這是資料繫結的。
    EmptyData = 8,
}


public class CreateTemplate_Copy : System.Web.UI.ITemplate
{
    //System.Web.UI.WebControls.ListItemType templateType;
    myListItemType_Copy templateType;
    List<CollumnCopy> ListViewCollumns = new List<CollumnCopy>();
    bool IsUseCheckBox = true;
    string DataKeyNames = "";
    string OnClickExecFunc = "";
    string OnClickExecFunc_view = "";
    string TRTitle = "";
    bool IsUseBorder = false;

    public CreateTemplate_Copy(myListItemType_Copy type, object o, string sDataKeyNames, bool bIsUseCheckBox, string sOnClickExecFunc, string sOnClickExecFunc_view, string sTRTitle, bool bIsUseBorder)
    {
        templateType = type;
        ListViewCollumns = (List<CollumnCopy>)o;
        DataKeyNames = sDataKeyNames;
        IsUseCheckBox = bIsUseCheckBox;
        OnClickExecFunc = sOnClickExecFunc;
        OnClickExecFunc_view = sOnClickExecFunc_view;
        TRTitle = sTRTitle;
        IsUseBorder = bIsUseBorder;
    }

    #region ITemplate 成員
    public void InstantiateIn(System.Web.UI.Control container)
    {




        PlaceHolder ph = new PlaceHolder();

        LinkButton lnk = new LinkButton(); //欄位標題
        Literal l_trID = new Literal(); //內容tr的ID
        System.Web.UI.HtmlControls.HtmlInputCheckBox chk = new System.Web.UI.HtmlControls.HtmlInputCheckBox(); //每一行的checkbox
        Literal l_javascript = new Literal(); //每一行按下去的javascript
        Literal l = new Literal(); //每一個td的內容
        string td_Class = "";

        if (IsUseBorder)
            td_Class = "Cell_Border";
        else
            td_Class = "Cell";

        switch (templateType)
        {
            case myListItemType_Copy.Header:
                //ph.Controls.Add(new LiteralControl(" <div class='table-responsive' style='border:0px;'>"));
                ph.Controls.Add(new LiteralControl("<table id='tb_ListView'  class='table table-striped table-bordered table-hover no-wrap'  >"));
                ph.Controls.Add(new LiteralControl("<thead><tr>"));

                if (IsUseCheckBox)
                {
                    ph.Controls.Add(new LiteralControl("<th  >"));
                    //ph.Controls.Add(new LiteralControl("&nbsp;<input id='chkAll' type='checkbox'  align='center' />"));
                    ph.Controls.Add(new LiteralControl("</th>"));
                }

                for (int i = 0; i < ListViewCollumns.Count; i++)
                {
                    ph.Controls.Add(new LiteralControl("<th  align=\"center\" "));
                    if (!string.IsNullOrEmpty(ListViewCollumns[i].Width))
                        ph.Controls.Add(new LiteralControl(" width=" + ListViewCollumns[i].Width));

                    ph.Controls.Add(new LiteralControl(">"));
                    lnk = new LinkButton();
                    lnk.CommandName = "Sort";
                    lnk.CommandArgument = ListViewCollumns[i].SourceName;
                    lnk.Text = ListViewCollumns[i].TitleName;
                    ph.Controls.Add(lnk);
                    ph.Controls.Add(new LiteralControl("</th>"));
                }


                if (OnClickExecFunc_view != "")
                {
                    ph.Controls.Add(new LiteralControl("<th >"));
                    ph.Controls.Add(new LiteralControl("&nbsp;"));
                    ph.Controls.Add(new LiteralControl("</th>"));
                }


                if (OnClickExecFunc != "")
                {
                    ph.Controls.Add(new LiteralControl("<th >"));
                    ph.Controls.Add(new LiteralControl("&nbsp;"));
                    ph.Controls.Add(new LiteralControl("</th>"));
                }

               

                ph.Controls.Add(new LiteralControl("</tr></thead>"));

                ph.Controls.Add(new LiteralControl("<tfoot><tr>"));

                if (IsUseCheckBox)
                {
                    ph.Controls.Add(new LiteralControl("<th >"));
                    ph.Controls.Add(new LiteralControl(""));
                    ph.Controls.Add(new LiteralControl("</th>"));
                }

                for (int i = 0; i < ListViewCollumns.Count; i++)
                {
                    ph.Controls.Add(new LiteralControl("<th "));
                    if (!string.IsNullOrEmpty(ListViewCollumns[i].Width))
                        ph.Controls.Add(new LiteralControl(" width=" + ListViewCollumns[i].Width));

                    ph.Controls.Add(new LiteralControl(">"));
                    lnk = new LinkButton();
                    lnk.CommandName = "Sort";
                    lnk.CommandArgument = ListViewCollumns[i].SourceName;
                    lnk.Text = ListViewCollumns[i].TitleName;
                    ph.Controls.Add(lnk);
                    ph.Controls.Add(new LiteralControl("</th>"));
                }

                if (OnClickExecFunc != "")
                {
                    ph.Controls.Add(new LiteralControl("<th >"));
                    ph.Controls.Add(new LiteralControl("&nbsp;"));
                    ph.Controls.Add(new LiteralControl("</th>"));
                }
               

                ph.Controls.Add(new LiteralControl("</tr></tfoot>"));
                PlaceHolder p = new PlaceHolder();
                p.ID = "itemPlaceholder";
                ph.Controls.Add(p);
                ph.Controls.Add(new LiteralControl("</table>"));
                // ph.Controls.Add(new LiteralControl("</div>"));
                break;
            case myListItemType_Copy.Item:
                l_trID.ID = "_tr";
                ph.Controls.Add(new LiteralControl("<tr "));
                ph.Controls.Add(l_trID);
                if (TRTitle != "")
                    ph.Controls.Add(new LiteralControl(" title='" + TRTitle + "' "));
                ph.Controls.Add(new LiteralControl(">"));

                if (IsUseCheckBox)
                {
                    chk = new System.Web.UI.HtmlControls.HtmlInputCheckBox();
                    chk.ID = "chk";
                    ph.Controls.Add(new LiteralControl("<td "));
                    if (IsUseBorder)
                        ph.Controls.Add(new LiteralControl("class='CheckCell_Border' "));
                    ph.Controls.Add(new LiteralControl("align='center'>"));
                    ph.Controls.Add(chk);
                    ph.Controls.Add(new LiteralControl("</td>"));
                }

                for (int i = 0; i < ListViewCollumns.Count; i++)
                {

                    ph.Controls.Add(new LiteralControl("<td   align='" + ListViewCollumns[i].Align + "' "));

                    ph.Controls.Add(new LiteralControl(">"));
                    ph.Controls.Add(new LiteralControl("<span style='white-space: normal;'>"));
                    l = new Literal();
                    l.ID = "_item" + ListViewCollumns[i].SourceName;
                    ph.Controls.Add(l);
                    ph.Controls.Add(new LiteralControl("</span>"));
                    ph.Controls.Add(new LiteralControl("</td>"));



                }

                if (OnClickExecFunc_view != "")
                {
                    ph.Controls.Add(new LiteralControl("<td  >"));
                    ph.Controls.Add(new LiteralControl("<a id='view-row' name='view-row' class='green' href='#' "));
                    l_javascript = new Literal();
                    l_javascript.ID = "_funonclickView";
                    ph.Controls.Add(l_javascript);

                    ph.Controls.Add(new LiteralControl(" >檢視</a>"));
                    ph.Controls.Add(new LiteralControl("</td>"));
                }

                if (OnClickExecFunc != "")
                {
                    ph.Controls.Add(new LiteralControl("<td  >"));
                    ph.Controls.Add(new LiteralControl("<a id='edit-row' name='edit-row' class='green' href='#' "));
                    l_javascript = new Literal();
                    l_javascript.ID = "_funonclick";
                    ph.Controls.Add(l_javascript);

                    ph.Controls.Add(new LiteralControl(" ><i class='ace-icon fa fa-edit bigger-130'></i></a>"));
                    ph.Controls.Add(new LiteralControl("</td>"));
                }

                

                ph.Controls.Add(new LiteralControl("</tr>"));
                ph.DataBinding += new EventHandler(Item_DataBinding);
                break;
            case myListItemType_Copy.AlternatingItem:
                l_trID.ID = "_tr";
                ph.Controls.Add(new LiteralControl("<tr "));
                ph.Controls.Add(l_trID);
                if (TRTitle != "")
                    ph.Controls.Add(new LiteralControl(" title='" + TRTitle + "' "));
                ph.Controls.Add(new LiteralControl(">"));

                if (IsUseCheckBox)
                {
                    chk = new System.Web.UI.HtmlControls.HtmlInputCheckBox();
                    chk.ID = "chk";
                    ph.Controls.Add(new LiteralControl("<td "));
                    if (IsUseBorder)
                        ph.Controls.Add(new LiteralControl("class='CheckCell_Border' "));
                    ph.Controls.Add(new LiteralControl("align='center'>"));
                    ph.Controls.Add(chk);
                    ph.Controls.Add(new LiteralControl("</td>"));
                }
                for (int i = 0; i < ListViewCollumns.Count; i++)
                {
                    ph.Controls.Add(new LiteralControl("<td   align='" + ListViewCollumns[i].Align + "' "));

                    ph.Controls.Add(new LiteralControl(">"));

                    l = new Literal();
                    l.ID = "_item" + ListViewCollumns[i].SourceName;
                    ph.Controls.Add(l);
                    ph.Controls.Add(new LiteralControl("</td>"));
                }

                if (OnClickExecFunc_view != "")
                {



                    ph.Controls.Add(new LiteralControl("<td >"));
                    ph.Controls.Add(new LiteralControl("<a id='view-row' name='view-row' class='green' href='#' "));
                    l_javascript = new Literal();
                    l_javascript.ID = "_funonclickView";
                    ph.Controls.Add(l_javascript);

                    ph.Controls.Add(new LiteralControl(" >檢視</a>"));
                    ph.Controls.Add(new LiteralControl("</td>"));
                }

                if (OnClickExecFunc != "")
                {



                    ph.Controls.Add(new LiteralControl("<td >"));
                    ph.Controls.Add(new LiteralControl("<a id='edit-row' name='edit-row' class='green' href='#' "));
                    l_javascript = new Literal();
                    l_javascript.ID = "_funonclick";
                    ph.Controls.Add(l_javascript);

                    ph.Controls.Add(new LiteralControl(" ><i class='ace-icon fa fa-edit bigger-130'></i></a>"));
                    ph.Controls.Add(new LiteralControl("</td>"));
                }

               

                ph.Controls.Add(new LiteralControl("</tr>"));
                ph.DataBinding += new EventHandler(Item_DataBinding);
                break;
            case myListItemType_Copy.Footer:



                ph.Controls.Add(new LiteralControl("</table>"));
                break;
            case myListItemType_Copy.EmptyData:
                int td_colspan = ListViewCollumns.Count;

                ph.Controls.Add(new LiteralControl("<table id='tb_ListView' cellspacing=0 cellpadding=0 style='border-top:GRAY 1PX SOLID;width:100%;'><tr>"));

                if (IsUseCheckBox)
                {
                    ph.Controls.Add(new LiteralControl("<td  >"));
                    ph.Controls.Add(new LiteralControl("<input id='chkAll' type='checkbox' disabled />"));
                    ph.Controls.Add(new LiteralControl("</td>"));
                    td_colspan++;
                }

                for (int i = 0; i < ListViewCollumns.Count; i++)
                {
                    ph.Controls.Add(new LiteralControl("<td  "));
                    if (!string.IsNullOrEmpty(ListViewCollumns[i].Width))
                        ph.Controls.Add(new LiteralControl(" width=" + ListViewCollumns[i].Width));
                    ph.Controls.Add(new LiteralControl(">"));
                    ph.Controls.Add(new LiteralControl(ListViewCollumns[i].TitleName + "</td>"));
                }
                ph.Controls.Add(new LiteralControl("</tr>"));
                ph.Controls.Add(new LiteralControl("<tr><td class='Cell' colspan='" + td_colspan.ToString() + "'><font color='red'>無資料符合搜尋條件，請重新設定搜尋條件</font></td></tr>"));
                ph.Controls.Add(new LiteralControl("</table>"));
                break;
        }
        container.Controls.Add(ph);
    }
    #endregion

    public void Item_DataBinding(object sender, System.EventArgs e)
    {
        PlaceHolder ph = (PlaceHolder)sender;
        ListViewDataItem ri = (ListViewDataItem)ph.NamingContainer;
        String KeyValue = "";

        //組每筆資料的Key值, 用在checkbox和每一筆onclick回傳的value Start
        string[] tmpDataKey = DataKeyNames.Split(',');

        for (int i = 0; i < tmpDataKey.Length; i++)
        {
            KeyValue += (DataBinder.Eval(ri.DataItem, tmpDataKey[i]) + "##").ToString();
        }

        if (KeyValue.Length > 0)
            KeyValue = KeyValue.Substring(0, KeyValue.Length - 2);
        //組每筆資料的Key值, 用在checkbox和每一筆onclick回傳的value End

        //組tr的ID及Class
        ((Literal)ph.FindControl("_tr")).Text = "id='_tr_" + ri.DisplayIndex.ToString() + "' ";

        //改成bootstrap css 不用加了
        //+
        //"class=\"Column_" + (ri.DisplayIndex % 2 == 0 ? "1" : "2") + "\" " +
        //"onmouseover=\"$('tr[id=_tr_" + ri.DisplayIndex.ToString() + "]').attr('className', 'Column_Over');\" " +
        //"onmouseout=\"$('tr[id=_tr_" + ri.DisplayIndex.ToString() + "]').attr('className', 'Column_" + (ri.DisplayIndex % 2 == 0 ? "1" : "2") + "');\"";

        //把checkbox的value設成key值
        if (IsUseCheckBox)
        {
            ((System.Web.UI.HtmlControls.HtmlInputCheckBox)ph.NamingContainer.FindControl("chk")).Attributes.Add("value", KeyValue);
        }

        //填入每一個欄位的內容
        for (int i = 0; i < ListViewCollumns.Count; i++)
        {


            String itemValue = (DataBinder.Eval(ri.DataItem, ListViewCollumns[i].SourceName)).ToString();

            //有要取代的字時 Start
            if (ListViewCollumns[i].FormulaDefine != null)
            {
                if (ListViewCollumns[i].FormulaDefine.Length != 0)
                {
                    for (int j = 0; j < ListViewCollumns[i].FormulaDefine.GetLength(0); j++)
                    {
                        itemValue = itemValue.Replace(ListViewCollumns[i].FormulaDefine[j, 0], ListViewCollumns[i].FormulaDefine[j, 1]);
                    }

                }
            }
            //有要取代的字時 End
            if (OnClickExecFunc != "")
            {
                // ((Literal)ph.FindControl("_funonclick")).Text = "onclick=\"" + OnClickExecFunc.Substring(0, OnClickExecFunc.Length - 1) + "'" + KeyValue.ToString() + "');\"";
                //

                ((Literal)ph.FindControl("_funonclick")).Text = String.Format("{0}=\"{2}'{1}');\"", "onclick", MDS.Utility.NUtility.HtmlEncode(MDS.Utility.NUtility.checkString(KeyValue.ToString())), MDS.Utility.NUtility.HtmlEncode(MDS.Utility.NUtility.checkString(OnClickExecFunc.Substring(0, OnClickExecFunc.Length - 1))));

            }

            if (OnClickExecFunc_view != "")
            {
                // ((Literal)ph.FindControl("_funonclick")).Text = "onclick=\"" + OnClickExecFunc.Substring(0, OnClickExecFunc.Length - 1) + "'" + KeyValue.ToString() + "');\"";
                //

                ((Literal)ph.FindControl("_funonclickView")).Text = String.Format("{0}=\"{2}'{1}');\"", "onclick", MDS.Utility.NUtility.HtmlEncode(MDS.Utility.NUtility.checkString(KeyValue.ToString())), MDS.Utility.NUtility.HtmlEncode(MDS.Utility.NUtility.checkString(OnClickExecFunc_view.Substring(0, OnClickExecFunc_view.Length-1))));

            }

            //放入欄位內容
            ((Literal)ph.FindControl("_item" + ListViewCollumns[i].SourceName)).Text = MDS.Utility.NUtility.HtmlEncode(MDS.Utility.NUtility.checkString(itemValue.ToString()));
        }
    }

}

