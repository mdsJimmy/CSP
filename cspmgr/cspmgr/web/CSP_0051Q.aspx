<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainPage.master" AutoEventWireup="true" CodeFile="CSP_0051Q.aspx.cs" Inherits="csp_web_CSP_0051Q" %>

<%@ Register Src="~/DMSControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>
<%@ Register Src="~/DMSControl/oListView.ascx" TagName="oListView" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PluginStylesPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitlePlaceHolder" Runat="Server">
    <uc1:ToolBar ID="btnAdd" Text="<%$ Resources:DMSWording, A0001 %>" PostBack="false"
        Enabled="true" OnClientClick="DoAdd()" runat="server"  />

   <uc1:ToolBar ID="btnDel" Text="<%$ Resources:DMSWording, A0003 %>" PostBack="false"
        Enabled="true" OnClientClick="DoDel()" runat="server" />

   <uc1:ToolBar ID="btnStartup" Text="<%$ Resources:DMSWording, B0003 %>" PostBack="false"
        Enabled="true" OnClientClick="DoStartup()" runat="server" />
    <uc1:ToolBar ID="btnDeny" Text="停用" PostBack="false" Enabled="true" OnClientClick="DoDeny()"
        runat="server" />
     </asp:Content>
    <asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
<div class="space-4"></div>
<div class="row">
    <div class="col-xs-4">  
        <label>表單類別</label>&nbsp;
        <asp:DropDownList ID="_dlType" runat="server" ClientIDMode="Static" DataSourceID="SqlDataSource1" DataTextField="CNAME" DataValueField="CKEY">
            
            
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="with src as(
	                        select NULL as CKEY, '請選擇' as CNAME
                        )
                        select * from src 
                        UNION ALL
                        SELECT CKEY,CNAME FROM MIP_CODES  WHERE (CLEVEL = 'B3030') AND (CNOTE &lt;&gt; 1) ORDER BY CKEY"></asp:SqlDataSource>
    </div>
</div>
<div class="space-4"></div>

<div class="row">
    <div class="col-xs-5" > 
        <label>表單名稱</label>&nbsp;<input name='_txtSrch'  id="_txtSrch" type="text"    />
        <label class="checkbox-inline">
            <input class="from-control" id="_chkIC" type="checkbox" value="IC" /> IC
        </label>
        <label class="checkbox-inline">
            <input class="from-control" id="_chkRM" type="checkbox" value="RM" /> RM
        </label>
    </div>
    <div class="row">
        
    </div>
</div>


<div class="space-4"></div>
<uc1:ToolBar ID="ToolBar1"  Text="<%$ Resources:DMSWording, A0004 %>" PostBack="false" Enabled="true" OnClientClick="DoSearch();" runat="server" />
<div class="space-4"></div>                     
<uc1:oListView runat="server" ID="oListView" />
<asp:ScriptManager ID="myScriptManager" runat="server" EnablePageMethods="True"></asp:ScriptManager>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PluginScriptsPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="InlineScriptsPlaceHolder" Runat="Server">
    
    <script type="text/javascript" >
        console.debug("前端資料");
       if ('<%:Request.QueryString["_chkIC"]%>' == 'IC' )
        {
            $('#_chkIC').prop('checked', true);
        }
        if ('<%:Request.QueryString["_chkRM"]%>'=='RM')
        {
            $('#_chkRM').prop('checked', true);
        }
       

        jQuery(function ($) {
            
            if ($("#_txtSrch").val() != null)
                $("#_txtSrch").val('<%:MDS.Utility.NUtility.checkString(str_txtSrch)%>');
        })

         /* 新增按鈕按下*/
            function DoAdd() {
                
                 var url = "CSP_0051A.aspx?a=a&queryCondi=<%=strQueryCondi%>";
                parent.location.replace(url);
            }

        /*點擊ListView某一筆記錄後帶出詳細資訊;*/
        function DoEdt(chkPK) {
            ////alert('DoEdt---chkPK:' + chkPK)
            if (chkPK != '') {
                var tmpArr = chkPK.split('##');
            }
            var url = "CSP_0051M.aspx?a=a&queryCondi=<%=strQueryCondi %>&proId=" + tmpArr[0];
            parent.location.replace(url);
        }

        /*搜尋按鈕按下;*/
        function DoSearch() {
            var chkIC = '';
            var chkRM = '';
            chkIC = $('#_chkIC').prop('checked') ? 'IC' : '';
            chkRM = $('#_chkRM').prop('checked') ? 'RM' : '';

            parent.location.replace("CSP_0051Q.aspx?PageNo=<%:(PageNo)%>&_dlType=" + $("#_dlType option:selected").text() + "-" + $("#_dlType option:selected").val() + "&_txtSrch=" + $("#_txtSrch").val() + "&_chkIC=" + chkIC + "&_chkRM=" + chkRM);                                 
            
        }


    /*Toolbar刪除;*/
    function DoDel() {
        var seleDelList = GetCheckBoxValue();
        if (seleDelList == '') {
            parent.MasterCtrl.showErrMsg('<%=ParseWording("A0009")%>');
        }
        else {
            $.confirm({
                title: '<%=ParseWording("A0010")%>',
                content: false,
                //content: 'Simple confirm!',
                confirm: function () {
                    PageMethods.Delete(seleDelList, ProcessSuccess, ProcessError);
                },
                cancel: function () {
                    //alert('Canceled!')
                }
            }); 
             
        }
    }

    function DoStartup() {
        var dataList = GetCheckBoxValue();
        var array = dataList.split("^^");
        if (array == '') {
            parent.MasterCtrl.showErrMsg('<%:ParseWording("A0009")%>');
        
        } else {
            PageMethods.StartupData(dataList, ProcessSuccess, ProcessError);
        }
    }

    function DoDeny() {
        var dataList = GetCheckBoxValue();
        var array = dataList.split("^^");
        if (array == '') {
            parent.MasterCtrl.showErrMsg('<%:ParseWording("A0009")%>');
        
        } else {
            PageMethods.DenyData(dataList, ProcessSuccess, ProcessError);
        }
    }

        /*成功時彈出訊息;*/
    function ProcessSuccess(receiveData, userContext, methodName) {

        if ((methodName == 'Delete')
        || (methodName == 'StartupData')
        || (methodName == 'DenyData')) {

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

