<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleManage.aspx.cs"   MasterPageFile="~/MasterPage/ContentPage.master"  Inherits="DMSMainManage_UserInfo_RoleManage" %>
<%@ Register Src="~/DMSControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PluginStylesPlaceHolder" runat="Server">
 
  </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
 
     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitlePlaceHolder" runat="Server">
   <%--ToolBar START--%>
        <table border="0" cellspacing="0" cellpadding="0" style="height:39px">
            <tr>
                <td>
                    <uc1:ToolBar ID="btnSave"  Text="<%$ Resources:DMSWording, A0005 %>" PostBack="false" Enabled="true" OnClientClick="DoSave();" runat="server" />
                </td>
                <td>
                 

                    <uc1:ToolBar ID="btnCancel"     Text="<%$ Resources:DMSWording, A0006 %>" PostBack="false" Enabled="true" OnClientClick="DoCancel();" runat="server" />
                </td>
            </tr>
        </table>
        <%--ToolBar END--%>
        


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
            
        
      
        
        <%--權限清單 START--%>
        <br/>
        <div id="RoleList" runat="server"  class="table-responsive"></div>
        <%--權限清單 END--%>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PluginScriptsPlaceHolder" runat="Server">
  
        
        
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="InlineScriptsPlaceHolder" runat="Server">
<script type="text/javascript">

    //get Parent Window;
    var pWindow = parent;

    //get StrFunction/StrAction;
    var StrFunctionCheckbox = pWindow.$("#HiddenFunctionList").val();
    var StrActionCheckbox = pWindow.$("#HiddenActionList").val();

    
    $(document).ready(function() {

        if (!((StrFunctionCheckbox == '') && (StrActionCheckbox == ''))) {
            /*將找不到的Function/Action取消選取 START;*/
            $('input[id="FunctionCheckbox"]').each(function() {
                if (StrFunctionCheckbox.indexOf($(this).val()) == -1)
                    $(this).attr('checked', false);
            });

            $('input[id="ActionCheckbox"]').each(function() {
                if (StrActionCheckbox.indexOf($(this).val()) == -1)
                    $(this).attr('checked', false);
            });
            /*將找不到的Function/Action取消選取 END;*/
        }
    });
    
    
    function DoSave() {
        //Clear Before Save Data;
        StrFunctionCheckbox = "";
        StrActionCheckbox = "";

        /*將使用者選取的Function/Action組成字串 START;*/
        $('input[id="FunctionCheckbox"]').each(function() {
            if (StrFunctionCheckbox.indexOf($(this).val()) == -1)
                if ($(this).prop('checked') == true)
                    StrFunctionCheckbox += $(this).val() + '||';
        });

        $('input[id="ActionCheckbox"]').each(function() {
            if (StrActionCheckbox.indexOf($(this).val()) == -1)
                if ($(this).prop('checked') == true)
                    StrActionCheckbox += $(this).val() + '||';
        });
        
        //Set value to pWindow hidden;
        if (StrFunctionCheckbox.length > 0)
            StrFunctionCheckbox = StrFunctionCheckbox.substring(0, StrFunctionCheckbox.length - 2);
        if (StrActionCheckbox.length > 0)
            StrActionCheckbox = StrActionCheckbox.substring(0, StrActionCheckbox.length - 2);
        /*將使用者選取的Function/Action組成字串 END;*/
        
        pWindow.$("#HiddenUserDefine").val("1");
        pWindow.$("#HiddenFunctionList").val(StrFunctionCheckbox);
        pWindow.$("#HiddenActionList").val(StrActionCheckbox);
        //alert(StrFunctionCheckbox);
        //alert(StrActionCheckbox);
        pWindow.$("#modalclose").click();
        
    }


    function DoCancel() {
        pWindow.$("#modalclose").click();
    }
    
</script>
</asp:Content>