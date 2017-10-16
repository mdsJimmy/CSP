<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage/MainPage.master" CodeFile="DMSRoleManage_List.aspx.cs" Inherits="SysFun_DMSRoleManage_DMSRoleManage_List" %>
<%@ Register Src="~/DMSControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>

<asp:Content ID="Content4" ContentPlaceHolderID="PluginStylesPlaceHolder" Runat="Server">
 
    
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitlePlaceHolder" Runat="Server">
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ul id="Ul1" class="nav nav-tabs">
        <li id='tag1' class="active"><a data-toggle="tab"   href="#"  onclick="DoChangeTag('tag1_bu');"><i class="green fa bigger-120">
        </i> <%=ParseWording("D0028")%> </a></li>
        <li><a data-toggle="tab"  href="#"  onclick="DoChangeTag('tag2_bu');"><i class="orange fa bigger-120">
        </i>  <%=ParseWording("D0029")%> </a></li>
    </ul>

   <br />

 
        <%--最上面灰色的那一塊(固定25px) END--%>

        <%--ToolBar START--%>
        <table border="0" cellspacing="0" cellpadding="0" style="height:39px">
            <tr>
                <td>
                    <uc1:ToolBar ID="btnSave"   Text="<%$ Resources:DMSWording, A0005 %>" PostBack="false" Enabled="true" OnClientClick="DoEdt();" runat="server" />
                </td>
                <td>
                    <uc1:ToolBar ID="btnCancel"   Text="<%$ Resources:DMSWording, A0006 %>" PostBack="false" Enabled="true" OnClientClick="DoCancel();" runat="server" />
                </td>
            </tr>
        </table>
        <%--ToolBar END--%>
        
        <table width="100%" border="0" cellspacing="2" cellpadding="0"><tr><td align="right">&nbsp;</td></tr></table>
        
        <div id="DivRoleManage" name="DivRoleManage" runat="server" style="width:100%;height:90%;overflow:auto;" />
        <div id="DivRoleRelation" name="DivRoleRelation" runat="server" style="width:100%;height:90%;overflow:auto;display:none" />
        
        <asp:ScriptManager ID="myScriptManager" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>    
      
   
  
      
     
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PluginScriptsPlaceHolder" Runat="Server">


</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="InlineScriptsPlaceHolder" Runat="Server">


<script type="text/javascript">

    /*Page ready*/
    $(document).ready(function () {


        /*判斷有無修改功能頁籤權限, 若無則隱藏Toolbar按鈕*/
        if ('<%=IsAction1%>' == '1') {
            $('#btnSave').css('display', '');
            $('#btnCancel').css('display', '');
        }
        else {
            $('#btnSave').css('display', 'none');
            $('#btnCancel').css('display', 'none');
        }
    });


    /*頁籤切換*/
    function DoChangeTag(obj) {


        var StrClassName = obj;//  $(obj).attr('class');
         
        /*取到this.className為_bu(未點選), 則觸發動作;*/
        /*tag1 on*/
        if (StrClassName == 'tag1_bu') {
 
            /*切換div*/
            document.getElementsByName("DivRoleRelation")[0].style.display = "none";
            document.getElementsByName("DivRoleManage")[0].style.display = "";

            
            /*判斷有無修改功能頁籤權限, 若無則隱藏Toolbar按鈕*/
            if ('<%=IsAction1%>' == '1') {
                $('#btnSave').css('display', '');
                $('#btnCancel').css('display', '');
            }
            else {
                $('#btnSave').css('display', 'none');
                $('#btnCancel').css('display', 'none');
            }
        }
        /*tag2 on*/
        else if (StrClassName == 'tag2_bu') {
 
            /*切換div*/
          //  alert('DoChangeTag212' + $("#DivRoleManage"));
            document.getElementsByName("DivRoleRelation")[0].style.display = "";
            document.getElementsByName("DivRoleManage")[0].style.display = "none";
          
            /*判斷有無修改角色頁籤權限, 若無則隱藏Toolbar按鈕*/
            if ('<%=IsAction2%>' == '1') {
                $('#btnSave').css('display', '');
                $('#btnCancel').css('display', '');
            }
            else {
                $('#btnSave').css('display', 'none');
                $('#btnCancel').css('display', 'none');
            }
        }
    }


    /*選取/取消跟function相關的所有action*/
    function onclick_Function(thisobj, thisActionList, thisDMSRoleID) {
       
        var ArrAction = thisActionList.split("||");
        for (var i = 0; i < ArrAction.length; i++) {
            $("input[value='" + thisDMSRoleID + "^^" + ArrAction[i] + "']").attr('checked', thisobj.checked);
        }
    }
  

    /*Toolbar放棄;*/
    function DoCancel() {
        $.confirm({
            title: '<%=ParseWording("A0013")%>',
            content: false,
            //content: 'Simple confirm!',
            confirm: function () {
                location.replace('DMSRoleManage_List.aspx');
            },
            cancel: function () {
                //alert('Canceled!')
            }
        });
         
        
 
    }


    /*Toolbar儲存, 使用PageMethods(AJEX);*/
    function DoEdt() {
         
        var StrFunction = '';
        var StrAction = '';

        var StrRoleRelation = '';
         
        /*用第一個頁籤是否on來判斷被點選的頁籤*/
        if ($('#tag1').attr('class') == 'active') {
            
            /*Get Function string*/
            $("input[name='chkFunction']:checked").each(function () {
                StrFunction += $(this).val() + '||';
            });
           
            if (StrFunction.length > 0)
                StrFunction = StrFunction.substring(0, StrFunction.length - 2);
           
           // alert(StrFunction);
            /*Get Action string*/
            $("input[name='chkAction']:checked").each(function () {
                StrAction += $(this).val() + '||';
            });
            if (StrAction.length > 0)
                StrAction = StrAction.substring(0, StrAction.length - 2);
             
            

            PageMethods.EdtRoleProcess(StrFunction, StrAction, ProcessSuccess, ProcessError);
        }
        else {
           
            /*Get RoleRelation string*/
            $("input[name='chkRoleRelation']:checked").each(function () {
                StrRoleRelation += $(this).val() + '||';
            });
             
            if (StrRoleRelation.length > 0)
                StrRoleRelation = StrRoleRelation.substring(0, StrRoleRelation.length - 2);
             

            PageMethods.EdtRelationProcess(StrRoleRelation, ProcessSuccess, ProcessError);
        }
    }
    /*成功時彈出訊息;*/
    function ProcessSuccess(receiveData, userContext, methodName) {
        
        if ((methodName == 'EdtRoleProcess') || (methodName == 'EdtRelationProcess')) {
            
            if (receiveData.nRet == 0) {
               
                //myAlert('A0011', '<%=ParseWording("A0011")%>');
 
                MasterCtrl.showSucessMsg('<%=ParseWording("A0011")%>');
           }
            else {

                MasterCtrl.showErrMsg('<%=ParseWording("A0028")%>' + '\nnRet = ' + receiveData.nRet + '\noutMsg = ' + receiveData.outMsg);
            }

           
        }
    }
    /*失敗時彈出失敗訊息;*/
    function ProcessError(error, userContext, methodName) {
     

        if (error != null)
            MasterCtrl.showErrMsg(error.get_message());
             
    }
    
</script>
</asp:Content>
 
    
  