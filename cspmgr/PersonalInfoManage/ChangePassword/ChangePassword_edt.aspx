<%@ Page Language="C#"   AutoEventWireup="true"  MasterPageFile="~/MasterPage/MainPage.master"   CodeFile="ChangePassword_edt.aspx.cs" Inherits="ChangePassword_ChangePassword_edt" %>
<%@ Register Src="~/DMSControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>
 
<asp:Content ID="Content4" ContentPlaceHolderID="PluginStylesPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitlePlaceHolder" Runat="Server">
 
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
 

        <%--ToolBar START--%>
        <table border="0" cellspacing="0" cellpadding="0" style="height:39px" class="fixed-header">
            <tr>
                <td>
                 
                    <uc1:ToolBar ID="btnSave"   Text="<%$ Resources:DMSWording, A0005 %>" PostBack="false" Enabled="true" OnClientClick="DoEdt()" runat="server" />
                </td>
                <td>
                    <!--<uc1:ToolBar ID="btnCancel"  Text="<%$ Resources:DMSWording, A0006 %>" PostBack="false" Enabled="true" OnClientClick="DoCancel()" runat="server" />-->
                </td>
            </tr>
        </table>
        <%--ToolBar END--%>
        <%--使用者密碼變更-修改 START--%>
     
          <div id="UserData-form" class="form-horizontal">

       
  
         <h4 class="header smaller lighter green">
                        <i class="ace-icon fa fa-check-square-o bigger-110"></i>
				        <b><%=ParseWording("C0001")%></b>
                        
                    </h4>
                    <%=ParseWording("A0008")%>
          <div class="form-group">
                    <label class="control-label col-sm-2 title"   > 群組代號:</label>&nbsp;&nbsp;
                     <div class="col-sm-2 ">
                      
                         <label class="control-label"> <asp:Label ID="oGroupInfo" runat="server"   ></asp:Label></label>
                   
                    
                    </div>
               </div>

         <div class="form-group">
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("C0003")%>:</label>&nbsp;&nbsp;
                    <label class="control-label"> <asp:Label ID="oUserInfo" runat="server"   ></asp:Label></label>
                   
                     
               </div>
            <div class="form-group">
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("C0004")%>:</label>&nbsp;&nbsp;
                      <div class="col-sm-2">
                   <input name="oPasswordType" type="radio" checked  style='display:none'/>  <label class="control-label"><%=ParseWording("C0008")%></label>
                    &nbsp;<input name="oPasswordType" type="radio" style='display:none'/>  <!--  <%=ParseWording("C0009")%> -->
                  
                    </div>
               </div>
                 <div class="form-group">
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("A0007")%><%=ParseWording("C0005")%>:</label>&nbsp;&nbsp;
                      <div class="col-sm-2">
                 <input id="oOldPassword" name="oOldPassword" type="password"  />
                   <input type="hidden" name="oHiddenOldPassword" id="oHiddenOldPassword" runat="server"  clientidmode="Static" />
                    </div>
               </div>
                 <div class="form-group">
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("A0007")%><%=ParseWording("C0006")%>:</label>&nbsp;&nbsp;
                      <div class="col-sm-2">
                <input id="oNewPassword" name="oNewPassword" type="password"  />
                    </div>
               </div>
                <div class="form-group">
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("A0007")%><%=ParseWording("C0007")%>:</label>&nbsp;&nbsp;
                      <div class="col-sm-2">
               <input id="oCheckPassword" name="oCheckPassword" type="password"      />
                    </div>
               </div>
       

       </div>
       
        <%--使用者密碼變更-修改 END--%>
        <asp:ScriptManager ID="myScriptManager" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
  
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PluginScriptsPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="InlineScriptsPlaceHolder" Runat="Server">
 
<script type="text/javascript">
   
    //MasterCtrl.showErrMsg('<%=ParseWording("A0028")%>' + '\nnRet = ' + receiveData.nRet + '\noutMsg = ' + receiveData.outMsg);
    switch ('<%:strStatus%>') {
        case '-7':
            MasterCtrl.showErrMsg('密碼已超過九十天未更改密碼，請更改密碼');
            break;
        case '-8':
            MasterCtrl.showErrMsg('第一次登入，請更改密碼');
            break;
        case '-10':
            MasterCtrl.showErrMsg('使用者密碼未存在，請聯絡管理員');
            break;
        case '0':
            MasterCtrl.clearMsg;
            break;
        default:
            MasterCtrl.clearMsg;
            break;

    }
  

    /*Toolbar放棄;*/
    function DoCancel() {
        $("#oOldPassword").val("");
        $("#oNewPassword").val("");
        $("#oCheckPassword").val("");
    }


    /*Toolbar儲存, 驗證輸入項目*/
    function DoEdt() {
        // var FormValid = $('#edt').validationEngine({ returnIsValid: true });
      //  alert('DoEdt' + $("#form1").valid());

        if ($("#form1").valid()) {
 
            DoSave();
        } else {
             
            MasterCtrl.showErrMsg('<%=ParseWording("A0029")%>' + '\nnRet = ' + receiveData.nRet + '\noutMsg = ' + receiveData.outMsg);
        }
    }


    /*Toolbar儲存, 使用PageMethods(AJEX)修改使用者;*/
    function DoSave() {
        
        PageMethods.EdtProcess($("#oNewPassword").val(), '<%=myUserID%>', ProcessSuccess, ProcessError);
    }
    /*成功時彈出訊息;*/
    function ProcessSuccess(receiveData, userContext, methodName) {
        if (methodName == 'EdtProcess') {
            if (receiveData.nRet == 0) {

                MasterCtrl.showSucessMsg('<%=ParseWording("A0011")%>');
                alert('密碼變更成功，將回登入頁，請重新登入');
                parent.location.replace("../../Verify.aspx");
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



    $(document).ready(function () {

        $.validator.addMethod("notEqualTo", function (value, element, param) {
            var target = $(param);
            if (value) return value != target.val();
            else return this.optional(element);
        }, "Does not match");

       
 

        $.validator.addMethod("pwd",
            function (value, element, regexp) {
                var re = new RegExp(regexp);
                return this.optional(element) || re.test(value);
            },
             "密碼長度最少8位字元，且要有大寫、小寫、數字及特殊符號 。"
       );

        $("#form1").validate({
            errorClass: "validator-error",
            rules: {

                oOldPassword: {
                    maxlength: 30,
                    required: true,
                    equalTo: "#oHiddenOldPassword",
                   
                },
                oNewPassword: {
                    maxlength: 30,
                    required: true,
                    notEqualTo: "#oHiddenOldPassword",
                    pwd: /^(?=.*\d)(?=.*[a-zA-Z])(?=.*\W).{8,50}$/
                },
                oCheckPassword: {
                    maxlength: 30,
                    required: true,
                    equalTo: "#oNewPassword",
                    pwd: /^(?=.*\d)(?=.*[a-zA-Z])(?=.*\W).{8,50}$$/
                }
            },
            messages: {
                oOldPassword: {
                    equalTo: "與登入密碼不同！"
                },
                oNewPassword: {
                     notEqualTo: "與舊密碼相同！"
                },
                oCheckPassword: {
                    equalTo: "與新密碼不同！"
                }
            },
            errorPlacement: function (error, element) {

                var divId = element.attr('name') + "-validator";
                if ($('#' + divId).length) {
                    $('#' + divId).html(error);
                }
                else {
                    $(element).parent().append($("<div id='" + divId + "' class='validator-error' ></div>").append(error));
                }

            },
            highlight: function (e) {
                $(e).addClass("validator-error-background");
            },
            unhighlight: function (e) {
                $(e).removeClass("validator-error-background");
            }, submitHandler: function () { alert("is good!") }
        });
    });


</script>
</asp:Content>
  
 