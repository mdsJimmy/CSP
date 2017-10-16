<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage/ContentPage.master"  CodeFile="Organization_List.aspx.cs" Inherits="DMSMainManage_Organization_Organization_List" %>
<%@ Register Src="~/DMSControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>
<%@ Register Src="~/DMSControl/oListView.ascx" TagName="oListView" TagPrefix="uc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="PluginStylesPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitlePlaceHolder" runat="Server">
 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <%--最上面灰色的那一塊(固定25px) START--%>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td style="width:100%;height:25px;">
                 <ul id="tab" class="nav nav-tabs">
                         <li class="active">
                          <a href="#">
                            <i class="green fa bigger-120"></i>
                              <%=ParseWording("B0005")%>
                                   </a>
                  </li>

                     <li>
                       <a  href="#"  onclick="DoChangeTag();">
                           <i class="orange fa bigger-120"></i>
                          <%=ParseWording("B0036")%>
                     </a>
                     </li>
                     </ul>



                   
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
                   
                    <uc1:ToolBar ID="btnAdd"  Text="<%$ Resources:DMSWording, A0001 %>" PostBack="false" Enabled="true" OnClientClick="DoAdd()" runat="server" />
            
                    <uc1:ToolBar ID="btnDel" Text="<%$ Resources:DMSWording, A0003 %>" PostBack="false" Enabled="true" OnClientClick="DoDel()" runat="server" />
             
        <%--ToolBar END--%>
           </td>
                <td align="right">
                    <%--所在位置 START--%>
       
 <asp:Label ID="Label_Address" runat="server" Text="" style='text-align:right'></asp:Label>
  </td>
                   
                     <%--所在位置 END--%>
             
            </tr>
        </table>
         
        
    <br>
       
        
        <%--ListView Start--%>
        <uc1:olistview ID="OrganizationList" runat="server" />
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
            parent.location.replace("Organization_edt.aspx?GroupID=" + chkPK + "&TargerGroupID=<%:TargerGroupID%>&PageNo=<%:PageNo%>&StrSearch=<%:mySearch%>");
    }


    function DoAdd() {
        parent.location.replace("Organization_add.aspx?TargerGroupID=<%:TargerGroupID%>&PageNo=<%:PageNo%>&StrSearch=<%:mySearch%>");
    }


    function DoDel() {
        var GroupIDList = GetCheckBoxValue();
        if (GroupIDList == '') { 
            parent.MasterCtrl.showErrMsg('<%=ParseWording("A0009")%>');
        }
        else {
            $.confirm({
                title: '<%=ParseWording("A0010")%>',
                content: false,
                //content: 'Simple confirm!',
                confirm: function () {
                    PageMethods.Delete_GroupID(GroupIDList, '<%=MDS.Utility.NUtility.checkNull(Session["Name"])%>', '<%=MIPLibrary.MIPUtil.checkSessionName(Session["Name"],Session["UserID"])%>', ProcessSuccess, ProcessError);
                },
                cancel: function () {
                    //alert('Canceled!')
                }
            });
         }
    }


    /*成功時彈出訊息;*/
    function ProcessSuccess(receiveData, userContext, methodName) {
        if (methodName == 'Delete_GroupID') {
            if (receiveData.nRet == 0) {
                parent.MasterCtrl.showSucessMsg(receiveData.returnData);
                parent.location.replace("Organization_Main.aspx");
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


    /*搜尋*/
    function DoSearch() {
        parent.frmMain.location.replace("Organization_List.aspx?TargerGroupID=<%:TargerGroupID%>&PageNo=<%:PageNo%>&StrSearch=" + $('#StrSearch').val());
    }


    /*頁籤切換*/
    function DoChangeTag() {
        parent.frmMain.location.replace("Contact_List.aspx?TargerGroupID=<%:TargerGroupID%>");
    }


    /*供TreeView呼叫用*/
    function DoChangeTreeNode(TreeNode) {
        //alert('TreeNode>' + TreeNode);
        if (TreeNode.indexOf("document") == -1) {
            parent.frmMain.location.replace("../DMSMainManage/Organization/Organization_List.aspx?TargerGroupID=" + TreeNode);
        }



    }
    
</script>
</asp:Content>
  
