<%@ Page Language="C#" AutoEventWireup="true"   MasterPageFile="~/MasterPage/ContentPage.master"  CodeFile="UserInfo_List.aspx.cs" Inherits="Class_UserInfo_List"%>
<%@ Register Src="~/DMSControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>
<%@ Register Src="~/DMSControl/oListView.ascx" TagName="oListView" TagPrefix="uc1" %>
<%@ Register Src="~/MDSControl/pwdDilog.ascx" TagName="pwdDilog" TagPrefix="uc1" %>

 

<asp:Content ID="Content1" ContentPlaceHolderID="PluginStylesPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitlePlaceHolder" runat="Server">
 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     

    <uc1:pwdDilog runat="server" ID="pwdDilog" />


  <%--最上面灰色的那一塊(固定25px) START--%>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td style="width:100%;height:25px;">
            


                   
                </td>
                <td  align="right">
                    <input type="text" id="StrSearch" runat="server" maxlength="50" value=""  clientidmode="Static"  placeholder="<%$ Resources:DMSWording, B0050 %>"  /> 
                </td>
                <td   align="left">
                    
                     <uc1:ToolBar ID="btnSearch"  Text="<%$ Resources:DMSWording, A0004 %>" PostBack="false" Enabled="true" OnClientClick="DoSearch()" runat="server" />
                </td>
            </tr>
        </table>
        <br />
        <%--最上面灰色的那一塊(固定25px) END--%>

          <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
            <td >
             <%--ToolBar START--%>
                   
                    <uc1:ToolBar ID="btnAdd"   Text="<%$ Resources:DMSWording, A0001 %>" PostBack="false" Enabled="true" OnClientClick="DoAdd()" runat="server" />
               
                    <uc1:ToolBar ID="btnDel"   Text="<%$ Resources:DMSWording, A0003 %>" PostBack="false" Enabled="true" OnClientClick="DoDel()" runat="server" />
               
                    <uc1:ToolBar ID="btnStartup"  Text="<%$ Resources:DMSWording, B0003 %>" PostBack="false" Enabled="true" OnClientClick="DoStartup()" runat="server" />
               
                    <uc1:ToolBar ID="btnDeny"  Text="<%$ Resources:DMSWording, B0001 %>" PostBack="false" Enabled="true" OnClientClick="DoDeny()" runat="server" />
               
                    <uc1:ToolBar ID="btnPwReset"  Text="<%$ Resources:DMSWording, B0002 %>" PostBack="false" Enabled="true" OnClientClick="openDilog()" runat="server" />

                    <uc1:ToolBar ID="btnUnlock"  Text="解除鎖定" PostBack="false" Enabled="true" OnClientClick="DoPwUnLock()" runat="server" />
                </td>
        <%--ToolBar END--%>



          
                <td align="right">
                    <%--所在位置 START--%>
       
 <asp:Label ID="Label_Address" runat="server" Text="" style='text-align:right'></asp:Label>
  </td>
                   
                     <%--所在位置 END--%>
             
            </tr>
        </table>
    
   <br />
        
        <%--ListView Start--%>
        <uc1:oListView ID="UserList" runat="server" />
        <%--ListView Start--%>
        
        <asp:ScriptManager ID="myScriptManager" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
            
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PluginScriptsPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="InlineScriptsPlaceHolder" runat="Server">
    
<script type="text/javascript">

   
    
    
    function DoEdt(chkPK) {
        if (chkPK != '')
        parent.location.replace("UserInfo_edt.aspx?AccountID=" + chkPK + "&TargerGroupID=<%:TargerGroupID%>&PageNo=<%:PageNo%>&StrSearch=<%:mySearch%>");
    }

    
    function DoAdd() {
        parent.location.replace("UserInfo_add.aspx?TargerGroupID=<%:TargerGroupID%>&PageNo=<%:PageNo%>&StrSearch=<%:mySearch%>");
    }
    
    
    function DoDel() {
        var AccountIDList = GetCheckBoxValue();
        if (AccountIDList == '') {
            parent.MasterCtrl.showErrMsg('<%=ParseWording("A0009")%>');
        }
        else {
            $.confirm({
                title: '<%=ParseWording("A0010")%>',
                content: false,
                //content: 'Simple confirm!',
                confirm: function () {
                    PageMethods.Delete_AccountID(AccountIDList, '<%=MDS.Utility.NUtility.checkNull(Session["Name"])%>', '<%=MIPLibrary.MIPUtil.checkSessionName(Session["Name"],Session["UserID"])%>', ProcessSuccess, ProcessError);
                },
                cancel: function () {
                    //alert('Canceled!')
                }
            }); 
            
        }
    }


    function DoStartup() {
        var AccountIDList = GetCheckBoxValue();
        if (AccountIDList == '') {
            parent.MasterCtrl.showErrMsg('<%=ParseWording("A0009")%>');
        }
        else {

            PageMethods.Startup_AccountID(AccountIDList, '<%=MDS.Utility.NUtility.checkNull(Session["Name"])%>', '<%=MIPLibrary.MIPUtil.checkSessionName(Session["Name"],Session["UserID"])%>', ProcessSuccess, ProcessError);
        }
    }
    

    function DoDeny() {
        var AccountIDList = GetCheckBoxValue();
        if (AccountIDList == '') {
            parent.MasterCtrl.showErrMsg('<%=ParseWording("A0009")%>');
        }
        else {

            PageMethods.Deny_AccountID(AccountIDList, '<%=MDS.Utility.NUtility.checkNull(Session["Name"])%>', '<%=MIPLibrary.MIPUtil.checkSessionName(Session["Name"],Session["UserID"])%>', ProcessSuccess, ProcessError);
        }
    }

   
   
    
    function DoPwReset() {
        var AccountIDList = GetCheckBoxValue();
        if (AccountIDList == '') {
            parent.MasterCtrl.showErrMsg('<%=ParseWording("A0009")%>');
        }
        else {
            
            PageMethods.PwReset_AccountID(AccountIDList, '<%=MDS.Utility.NUtility.checkNull(Session["Name"])%>', '<%=MIPLibrary.MIPUtil.checkSessionName(Session["Name"],Session["UserID"])%>', $('#repwd').val(), ProcessSuccess, ProcessError);
           
        }
    }

   

     function DoPwUnLock() {
        var AccountIDList = GetCheckBoxValue();
        if (AccountIDList == '') {
            parent.MasterCtrl.showErrMsg('<%=ParseWording("A0009")%>');
        }
        else {

            PageMethods.PwUnLock_AccountID(AccountIDList, '<%=MDS.Utility.NUtility.checkNull(Session["Name"])%>', '<%=MIPLibrary.MIPUtil.checkSessionName(Session["Name"],Session["UserID"])%>', ProcessSuccess, ProcessError);
            
        }
    }
    
    
    /*成功時彈出訊息;*/
    function ProcessSuccess(receiveData, userContext, methodName) {
                
        if ((methodName == 'Delete_AccountID')
            || (methodName == 'Startup_AccountID')
            || (methodName == 'Deny_AccountID')
            || (methodName == 'PwReset_AccountID')
            || (methodName == 'PwUnLock_AccountID')
        ) {
            
            if (receiveData.nRet == 0) {
                parent.MasterCtrl.showSucessMsg('<%=ParseWording("A0011")%>');
                parent.frmMain.location.replace("UserInfo_List.aspx?TargerGroupID=<%:TargerGroupID%>&PageNo=<%:PageNo%>&StrSearch=<%:mySearch%>");
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


    function DoSearch() {
        parent.frmMain.location.replace("UserInfo_List.aspx?TargerGroupID=<%:TargerGroupID%>&PageNo=<%:PageNo%>&StrSearch=" + $('#StrSearch').val());
    }
    /*供TreeView呼叫用*/
    function DoChangeTreeNode(TreeNode) {
        //alert('TreeNode>' + TreeNode);
        if (TreeNode.indexOf("document") == -1) {
            parent.frmMain.location.replace("../DMSMainManage/UserInfo/UserInfo_List.aspx?TargerGroupID=" + TreeNode);
        }



    }



   


</script>

</asp:Content>