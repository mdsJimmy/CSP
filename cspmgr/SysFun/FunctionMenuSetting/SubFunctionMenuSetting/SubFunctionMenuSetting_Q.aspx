<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainPage.master" AutoEventWireup="true" CodeFile="SubFunctionMenuSetting_Q.aspx.cs" Inherits="SysFun_FunctionMenuSetting_Q" %>
<%@ Register Src="~/DMSControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>
<%@ Register Src="~/DMSControl/oListView.ascx" TagName="oListView" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PluginStylesPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitlePlaceHolder" Runat="Server">
    <uc1:ToolBar ID="btnAdd" Text="<%$ Resources:DMSWording, A0001 %>" PostBack="false"
        Enabled="true" OnClientClick="DoAdd()" runat="server" />
    <uc1:ToolBar ID="btnDel" Text="<%$ Resources:DMSWording, A0003 %>" PostBack="false"
        Enabled="true" OnClientClick="DoDel()" runat="server" />    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-xs-4">
            <asp:Label ID="Label1" runat="server" Text="Label">功能名稱：</asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="FunctionDesc" DataValueField="SysFuncID" AppendDataBoundItems="True" ClientIDMode="Static">
                <asp:ListItem  Value="">全部</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="select  DISTINCT b.FunctionDesc,b.SysFuncID from SystemAction a, SystemFunction b ,SystemModule c where a.SysFuncID=b.SysFuncID and a.SysModID=c.SysModID"></asp:SqlDataSource>
    
    <div class="space-4"></div>
        <uc1:ToolBar ID="btnSearch" Text="<%$ Resources:DMSWording, A0004 %>" PostBack="false"
            Enabled="true" OnClientClick="DoSearch();" runat="server" />
    <div class="space-4"></div>

    <%--ListView Start--%>
    <uc1:oListView ID="SubFunctionList" runat="server" />
    <%--ListView Start--%>
    <asp:ScriptManager ID="myScriptManager" runat="server" EnablePageMethods="True">
    </asp:ScriptManager>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PluginScriptsPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="InlineScriptsPlaceHolder" Runat="Server">
    <script>
        
        /*搜尋按鈕按下;*/
        function DoSearch() {
            parent.location.replace("SubFunctionMenuSetting_Q.aspx?PageNo=<%=MDS.Utility.NUtility.UrlEncode(""+SubFunctionList.PageNo)%>&function=" + $('#DropDownList1 :selected').val());
        }
        /* 新增按鈕按下*/
        function DoAdd() {
            var url = "SubFunctionMenuSetting_A.aspx?a=a&queryCondi=<%=MDS.Utility.NUtility.UrlEncode(strQueryCondi)%>";
            parent.location.replace(url);
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
                        PageMethods.Delete_Action(seleDelList, ProcessSuccess, ProcessError);
                    },
                    cancel: function () {
                        //alert('Canceled!')
                    }
                }); 
             
            }
        }

         /*成功時彈出訊息;*/
        function ProcessSuccess(receiveData, userContext, methodName) {

            if ((methodName == 'Delete_Action'))
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

