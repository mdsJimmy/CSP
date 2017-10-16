<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainPage.master" AutoEventWireup="true"
    CodeFile="FunctionMenuSetting_List_Q.aspx.cs" Inherits="yuantalife_web_YL0010Q" %>

<%@ Register Src="~/DMSControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>
<%@ Register Src="~/DMSControl/oListView.ascx" TagName="oListView" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PluginStylesPlaceHolder" runat="Server">
    <link rel="stylesheet" href="<%=ResolveUrl("~/Plugins/ace-assets//css/bootstrap-datetimepicker.css")%>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitlePlaceHolder" runat="Server">
    <uc1:ToolBar ID="btnAdd" Text="<%$ Resources:DMSWording, A0001 %>" PostBack="false"
        Enabled="true" OnClientClick="DoAdd()" runat="server" />
    <uc1:ToolBar ID="btnDel" Text="<%$ Resources:DMSWording, A0003 %>" PostBack="false"
        Enabled="true" OnClientClick="DoDel()" runat="server" />    
   
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <%--資料狀態--%>
        <div class="col-xs-4">
            模組 :&nbsp;
            <select id="_selSystemModule" name="_selSystemModule" runat="server"  style="width: 50%">
            </select>
        </div>
        <%--<div class="col-xs-4">
            功能名稱：&nbsp;<input type="text" id="_functionDesc" name="_functionDesc"  value="<%:  %>" style="width:50%" />
        </div>--%>
       
       
    </div>
    <div class="space-4">
    </div>    
    <div class="row">
       
    </div>
    <div class="space-4">
    </div>
    <uc1:ToolBar ID="btnSearch" Text="<%$ Resources:DMSWording, A0004 %>" PostBack="false"
        Enabled="true" OnClientClick="DoSearch();" runat="server" />
    <div class="space-4">
    </div>
    <div class="row">
        
    </div>
    <div class="space-4">       
    </div>
    <%--ListView Start--%>
    <uc1:oListView ID="NewsListView" runat="server" />
    <%--ListView Start--%>
    <asp:ScriptManager ID="myScriptManager" runat="server" EnablePageMethods="True">
    </asp:ScriptManager>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PluginScriptsPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="InlineScriptsPlaceHolder" runat="Server">
    <script src="<%=ResolveUrl("~/Plugins/ace-assets/js/date-time/moment.js")%>"></script>
    <script src="<%=ResolveUrl("~/Plugins/ace-assets/js/date-time/bootstrap-datetimepicker.js")%>"></script>
    <script type="text/javascript">

        

   
    /* 新增按鈕按下*/
    function DoAdd() {
        var url = "FunctionMenuSetting_List_A.aspx?a=a&queryCondi=<%=MDS.Utility.NUtility.UrlEncode(strQueryCondi)%>";
        parent.location.replace(url);
    }

    /*搜尋按鈕按下;*/
    function DoSearch() {
         parent.location.replace("FunctionMenuSetting_List_Q.aspx?PageNo=<%=MDS.Utility.NUtility.UrlEncode(""+NewsListView.PageNo)%>+ &_selSystemModule=" + $('#<%=MDS.Utility.NUtility.UrlEncode(_selSystemModule.ClientID)%>').val());
    }

 
    /*預設按鈕按下;*/
    function DoDefault() {
        parent.location.replace("FunctionMenuSetting_List_Q.aspx");
    }

    /*Toolbar刪除;*/
    function DoDel() {
        var seleDelList = GetCheckBoxValue();
        debugger;
        if (seleDelList == '') {
            parent.MasterCtrl.showErrMsg('<%=ParseWording("A0009")%>');
        }
        else {
            $.confirm({
                title: '<%=ParseWording("A0010")%>',
                content: false,
                //content: 'Simple confirm!',
                confirm: function () {
                    PageMethods.Delete_NEWS(seleDelList, ProcessSuccess, ProcessError);
                },
                cancel: function () {
                    //alert('Canceled!')
                }
            }); 
             
        }
    }

    

    /*點擊ListView某一筆記錄後帶出詳細資訊;*/
    function DoEdt(chkPK) {
        //alert('DoEdt---chkPK:' + chkPK)
        if (chkPK != '') {
            var tmpArr = chkPK.split('##');
            
        }
        /*******************************************************************
        chkPK : 內容為key field 陣列，來自於 MobileDevice_List.aspx.cs  DeviceList.DataKeyNames = "appname,deviceid" 定義 
        $('#appname').val() : 此指搜尋條件 "App名稱" 欄位
        $('#phonetype').val() : 此指搜尋條件之 "設備類型" 欄位
        **********************************************************************/
        //parent.location.replace("YL0010M.aspx?PageNo=<%=NewsListView.PageNo%>&buildId="+ tmpArr[0]);
        var url="FunctionMenuSetting_List_M.aspx?a=a&queryCondi=<%=MDS.Utility.NUtility.UrlEncode(strQueryCondi)%>&SysFuncID="+ tmpArr[0];
        parent.location.replace(url);
    }

    


    


    /*成功時彈出訊息;*/
    function ProcessSuccess(receiveData, userContext, methodName) {

        if ((methodName == 'Delete_NEWS'))
        {

            if (receiveData.nRet == 0) {
                parent.MasterCtrl.showSucessMsg('<%=ParseWording("A0011")%>');
                DoSearch();
            }
            else {
                parent.MasterCtrl.showErrMsg('<%=ParseWording("A0028")%>' + '\nnRet = ' + receiveData.nRet + '\noutMsg = ' + receiveData.outMsg);
            }


        }
    }
    /*失敗時彈出失敗訊息;*/
    function ProcessError(error, userContext, methodName) {
        if (error != null)
            parent.MasterCtrl.showErrMsg(error.get_message());
    }

    </script>
</asp:Content>
