<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainPage.master" AutoEventWireup="true"
    CodeFile="SysinfoM.aspx.cs" Inherits="yuantalife_web_YL0010M" %>

<%@ Register Src="~/DMSControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PluginStylesPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="<%=ResolveUrl("~/Plugins/ace-assets//css/bootstrap-datetimepicker.css")%>" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitlePlaceHolder" runat="Server">
    <uc1:ToolBar ID="btnSave" Text="<%$ Resources:DMSWording, A0005 %>" PostBack="false"
        Enabled="true" OnClientClick="DoCheckSave()" OnClick="btnUpload_Click" runat="server" />
    <uc1:ToolBar ID="btnCancel" Text="<%$ Resources:DMSWording, A0006 %>" PostBack="false"
        Enabled="true" OnClientClick="DoCancel();" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="UserData-form" class="form-horizontal">
        <h4 class="header smaller lighter green">
            <i class="ace-icon fa fa-check-square-o bigger-110"></i><b>SYSINFO維護作業</b>
        </h4>
        <%=ParseWording("A0008")%>
        <div class="space-6">
        </div>
        <%--memo--%>
        <div class="form-group" style="display:none">
            <label class="col-sm-2 control-label">
                <%=ParseWording("A0007")%>備註 :</label>
            <div class="col-sm-5  no-padding-top">
                <label id="_memo"   class="textbox" style="width: 50%"><%:MDS.Utility.NUtility.checkString(memo)%></label>                    
                
            </div>
        </div>
        <%--發布對象--%>
        <div class="form-group">
            <label class="col-sm-2 control-label">
                <%=ParseWording("A0007")%>代碼 :</label>
            <div class="col-sm-5  no-padding-top">
                <label id="_selAppType"   class="textbox" style="width: 50%" ><%:MDS.Utility.NUtility.checkString(strAppType)%></label>                    
                
            </div>
        </div>
        <%--資料類型--%>
        <div class="form-group">
            <label class="col-sm-2 control-label">
                <%=ParseWording("A0007")%>顯示位置 :</label>
            <div class="col-sm-5  no-padding-top">
                <select id="_selNewsKind" name="_selNewsKind" runat="server" style="width: 50%">
                </select>
            </div>
        </div>
        <%--提供 APP 使用--%>
        <div class="form-group"   style="display:none">
            <label class="col-sm-2 control-label">
                <%=ParseWording("A0007")%>提供 APP 使用 :</label>
           <div class="col-sm-5  no-padding-top">
                <select id="_selAPP4" name="_selAPP4" runat="server" style="width: 50%">
                </select>
            </div>

        </div>
        <%--說明--%>
        <div class="form-group">
            <label class="col-sm-2 control-label">
                 <%=ParseWording("A0007")%>說明 :</label>
            <div class="col-sm-8  no-padding-top">
                <input type="text" id="_txtNewsTitle" name="_txtNewsTitle" class="textbox" maxlength="200"
                    style="width: 100%" value='<%:MDS.Utility.NUtility.checkString(strNewsTitle)%>' >
            </div>
        </div>
        <%--資料內容--%>
        <div class="form-group">
            <label class="col-sm-2 control-label">
                <%=ParseWording("A0007")%>資料內容 :</label>
            <div class="col-sm-8  no-padding-top">
                <textarea  id="_strInfo" name="_strInfo" rows="10" style="width:100%" class="textbox"><%:MDS.Utility.NUtility.checkString(strInfo)%></textarea>
            </div>
        </div>
        
        <input type="hidden" id="method" name="method" runat="server" value="" />
        <input type="hidden" id="_hidNewsId" name="_hidNewsId" value="" />
    </div>
    <%--新增 END--%>
    <asp:ScriptManager ID="myScriptManager" runat="server" EnablePageMethods="True">
    </asp:ScriptManager>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PluginScriptsPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="InlineScriptsPlaceHolder" runat="Server">
    <script src="<%=ResolveUrl("~/Plugins/ace-assets/js/ace-elements.js")%>"></script>
    <script src="<%=ResolveUrl("~/Plugins/ace-assets/js/ace.js")%>"></script>
    <script src="<%=ResolveUrl("~/Plugins/ace-assets/js/jquery-ui.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Plugins/ace-assets/js/date-time/moment.js")%>"></script>
    <script src="<%=ResolveUrl("~/Plugins/ace-assets/js/date-time/bootstrap-datetimepicker.js")%>"></script>
    <script type="text/javascript">
        //strAppType = row["CKEY"].ToString();//代碼
        //strNewsKind = row["CSTATUS"].ToString();//狀態
        //strApp4= row["APPLY4"].ToString();//啟動app使用
        //strNewsTitle = row["CNOTE"].ToString();//說明
        //strInfo = row["CVALUE"].ToString();//內容

       

        

            


     

        /*Toolbar放棄;*/
        function DoCancel() {
            $.confirm({
                title: '<%=ParseWording("A0013")%>',
                content: false,
                //content: 'Simple confirm!',
                confirm: function () {
                    
                    parent.location.replace("SysinfoQ.aspx?a=a"+ unescape(decodeURI("<%=MDS.Utility.NUtility.UrlEncode(strQueryCondi)%>")));
                },
                cancel: function () {
                    //alert('Canceled!')
                }
            });

        }

        function DoCheckSave() {
            
            

            if ($('#form1').valid()) {
                //$('#form1').submit();
                
                DoSave();
            } else {
                parent.MasterCtrl.showErrMsg('<%=ParseWording("A0029")%>');
            }

        }

        /*Toolbar儲存, 使用PageMethods(AJEX)新增使用者;*/
        function DoSave() {
            
            PageMethods.UpdateNewsProcess(            
              
           
             $("#_selAppType").html()
            , $('#<%:_selNewsKind.ClientID%>').val()
            , $('#<%:_selAPP4.ClientID%>').val()
            , $("#_txtNewsTitle").val()
            , $("#_strInfo").val()
            , ProcessSuccess
            , ProcessError);
           
        }

        /*成功時彈出訊息;*/
        function ProcessSuccess(receiveData, userContext, methodName) {
            if (methodName == 'UpdateNewsProcess') {
                if (receiveData.nRet == 0) {
                    parent.MasterCtrl.showSucessMsg('<%=ParseWording("A0011")%>');
                   
                    parent.location.replace("SysinfoQ.aspx?a=a"+ unescape(decodeURI("<%=MDS.Utility.NUtility.UrlEncode(strQueryCondi)%>")));
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
