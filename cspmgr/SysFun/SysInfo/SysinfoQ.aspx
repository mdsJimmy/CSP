<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainPage.master" AutoEventWireup="true"
    CodeFile="SysinfoQ.aspx.cs" Inherits="yuantalife_web_YL0010Q" %>

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
    <uc1:ToolBar ID="ToolBar2" Text="發佈" PostBack="false"
        Enabled="true" OnClientClick="DoDeploy()" runat="server" />


    <%--    <uc1:ToolBar ID="btnStartup" Text="<%$ Resources:DMSWording, B0003 %>" PostBack="false"
        Enabled="true" OnClientClick="DoStartup()" runat="server" />
    <uc1:ToolBar ID="btnDeny" Text="停用" PostBack="false" Enabled="true" OnClientClick="DoDeny()"
        runat="server" />--%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <%--資料狀態--%>
        <div class="col-xs-4">
            <%--種類 :&nbsp;--%>
            <select id="_selNewsKind" name="_selNewsKind" runat="server" style="width: 50%" visible="false">
            </select>
        </div>
        <%--<div class="col-sm-8" >
            上架時間&nbsp; <span class="input-icon input-icon-right" >
                <input id="_txtStartDate" name="_txtStartDate" type="text" />
                <i class="glyphicon glyphicon-calendar"></i></span>~ <span class="input-icon input-icon-right">
                    <input id="_txtEndDate" name="_txtEndDate" type="text" />
                    <i class="glyphicon glyphicon-calendar"></i></span>
        </div>--%>
    </div>
    <div class="space-4">
    </div>    
    <div class="row">
        <%--發布對象--%>
        <div class="col-xs-4">
            <%--發布對象&nbsp;--%>
            <select id="_selAppType" name="_selAppType" runat="server" style="width: 70%" visible="false">
            </select>
        </div>
    </div>
    <div class="space-4">
    </div>
    <uc1:ToolBar ID="btnSearch" Text="<%$ Resources:DMSWording, A0004 %>" PostBack="false"
        Enabled="true" OnClientClick="DoSearch();" runat="server" Visible="false" />
    <div class="space-4">
    </div>
    <div class="row">
        <%--資料狀態--%>
        <div class="col-xs-6" style="display:none">
            版本:&nbsp;
            <input type="text" id="_txtDeployDT" name="_txtDeployDT" style="width: 40%" readonly="readonly" />
        </div>        
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

        jQuery(function ($) {

        $("#_txtDeployDT").val("<%:MDS.Utility.NUtility.checkDataTime(MDS.Utility.NUtility.checkString(strDeployDT))%>");

        var now = moment();
        var now_b = moment().add(+1, 'day');
        var startdate = document.getElementById("_txtStartDate");
        var enddate = document.getElementById("_txtEndDate");
        $(startdate).datetimepicker({
            //defaultDate: now,
             defaultDate:"<%:MDS.Utility.NUtility.checkDataTime(_CreateStartDate)%>",
            format: 'YYYY/MM/DD'
        });
        $(enddate).datetimepicker({
            //defaultDate: now_b,
             defaultDate:"<%:MDS.Utility.NUtility.checkDataTime(_CreateEndDate)%>",
            format: 'YYYY/MM/DD'
        });
        $(startdate).datetimepicker().next().on(ace.click_event, function () {
            $(this).prev().focus();
        });
        $(enddate).datetimepicker().next().on(ace.click_event, function () {
            $(this).prev().focus();
        });


        var $form = $('#form1');
        var file_input = $form.find('input[type=file]');
        var upload_in_progress = false;




       

    });

    $(document).ready(function () {
       
        function getFormattedDate(date) {
            var day = date.getDate();
            var month = date.getMonth() + 1;
            var year = date.getFullYear().toString().slice(2);
            return day + '-' + month + '-' + year;
        }


    });
    /* 新增按鈕按下*/
    function DoAdd() {
      
        var url = "SysinfoA.aspx?a=a&queryCondi=<%=MDS.Utility.NUtility.UrlEncode( strQueryCondi)%>";
        //alert(url);
        
        parent.location.replace(url);
    }

    /*搜尋按鈕按下;*/
    function DoSearch() {
        
        parent.location.replace("SysinfoQ.aspx?PageNo=<%=MDS.Utility.NUtility.UrlEncode(""+NewsListView.PageNo)%>&_selAppType=" + $('#<%=MDS.Utility.NUtility.UrlEncode(""+_selAppType.ClientID)%>').val() + "&_selNewsKind=" + $('#<%=MDS.Utility.NUtility.UrlEncode(""+_selNewsKind.ClientID)%>').val()
            + "&_txtStartDate=" + $('#_txtStartDate').val()+"&_txtEndDate="+ $('#_txtEndDate').val() );
    }

    /*預設按鈕按下;*/
    function DoDefault() {
        parent.location.replace("SysinfoQ.aspx");
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
                    PageMethods.Delete_NEWS(seleDelList, ProcessSuccess, ProcessError);
                },
                cancel: function () {
                    //alert('Canceled!')
                }
            }); 
             
        }
    }

    /*Toolbar發佈;*/
    function DoDeploy() {

        $.confirm({
            title: '確定是否要發佈',
            content: false,
            //content: 'Simple confirm!',
            confirm: function () {
                PageMethods.Deploy(ProcessSuccess, ProcessError);
            },
            cancel: function () {
                //alert('Canceled!')
            }
        });

    }

    /*點擊ListView某一筆記錄後帶出詳細資訊;*/
    function DoEdt(chkPK) {
        ////alert('DoEdt---chkPK:' + chkPK)
        if (chkPK != '') {
            var tmpArr = chkPK.split('##');
        }
        /*******************************************************************
        chkPK : 內容為key field 陣列，來自於 MobileDevice_List.aspx.cs  DeviceList.DataKeyNames = "appname,deviceid" 定義 
        $('#appname').val() : 此指搜尋條件 "App名稱" 欄位
        $('#phonetype').val() : 此指搜尋條件之 "設備類型" 欄位
        **********************************************************************/
        
        
        debugger;
        if (tmpArr[0] == "MIP_TOLO_CNT_02" |
            tmpArr[0] == "MIP_AD_02" |
            tmpArr[0] == "CUSTOMER_MAIL_TIME" |
            tmpArr[0] == "MIP_TOLO_CNT_01" |
            tmpArr[0] == "YL0132A" |
            tmpArr[0] == "MIP_AD_01") {
            alert("系統值不可修改");
        } else {
            var url = "SysinfoM.aspx?a=a&queryCondi=<%=MDS.Utility.NUtility.UrlEncode(strQueryCondi)%>&buildId=" + tmpArr[0];
            parent.location.replace(url);
        }
        
    }

    function DoStartup() {
        var DeviceList = GetCheckBoxValue();
        if (DeviceList == '') {
            parent.MasterCtrl.showErrMsg('<%=ParseWording("A0009")%>');
        }
        else {

            PageMethods.Startup_Device(DeviceList, ProcessSuccess, ProcessError);
        }
    }


    function DoDeny() {
        var DeviceList = GetCheckBoxValue();
        if (DeviceList == '') {
            parent.MasterCtrl.showErrMsg('<%=ParseWording("A0009")%>');
        }
        else {

            PageMethods.Deny_Device(DeviceList, ProcessSuccess, ProcessError);
        }
    }


    /*成功時彈出訊息;*/
    function ProcessSuccess(receiveData, userContext, methodName) {

        if ((methodName =='Delete_NEWS')
        || (methodName == 'Deploy')
        || (methodName == 'Startup_Device')
        || (methodName == 'Deny_Device')) {

            if (receiveData.nRet == 0) {
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
