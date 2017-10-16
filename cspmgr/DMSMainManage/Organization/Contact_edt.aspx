<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MainPage.master"  CodeFile="Contact_edt.aspx.cs" Inherits="DMSMainManage_Organization_Contact_edt" %>
<%@ Register Src="~/DMSControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PluginStylesPlaceHolder" runat="Server">
 
 <link href="<%=ResolveUrl("~/Plugins/ace-assets/css/bootstrap-duallistbox.css")%>" rel="stylesheet" type="text/css" media="all">
  </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
 
     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitlePlaceHolder" runat="Server">
 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
          <%--最上面灰色的那一塊(固定25px) END--%>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
            <td >
             <%--ToolBar START--%>
                   
                
                    <uc1:ToolBar ID="btnSave"  Text="<%$ Resources:DMSWording, A0005 %>" PostBack="false" Enabled="true" OnClientClick="DoEdt();" runat="server" />
              
                    <uc1:ToolBar ID="btnCancel"  Text="<%$ Resources:DMSWording, A0006 %>" PostBack="false" Enabled="true" OnClientClick="DoCancel();" runat="server" />
                
                    <uc1:ToolBar ID="btnBack"   Text="<%$ Resources:DMSWording, A0026 %>" PostBack="false" Enabled="true" OnClientClick="DoBack();" runat="server" />
                
            </tr>
        <%--ToolBar END--%>
           </td>
          
            </tr>
              
        </table>
        
        <%--聯絡人-新增 START--%>
         <div id="UserData-form" class="form-horizontal"  >
         <h4 class="header smaller lighter green">
                 <i class="ace-icon fa fa-check-square-o bigger-110"></i>
				    <b><%=ParseWording("B0058")%></b> 
         </h4>
        <%=ParseWording("A0008")%>
            <div class="space-6"></div>

                <div class="form-group">
                   <label class="col-sm-2 control-label" for="oGroupList">
                     <%=ParseWording("A0007")%><%=ParseWording("B0012")%>
                     :</label> 
                      <div class="col-sm-8  no-padding-top"  > 
                        <select multiple="multiple" size="5" name="oGroupList" id='oGroupList'>
                        <%= oGroupList%>
                        </select> 
                        <div class="hr hr-16 hr-dotted"></div>
                      </div>
               </div>
  
             <div class="form-group">
                   <label class="col-sm-2 control-label" >
                     <%=ParseWording("A0007")%><%=ParseWording("B0007")%>
                     :</label> 
                      <div class="col-sm-5  no-padding-top"  > 
                       <input name='oContactName'  id="oContactName" type="text"  value="<%=_oContactName%>"  />
                      </div>
               </div>
                <div class="form-group">
                   <label class="col-sm-2 control-label" >
                     <%=ParseWording("B0008")%>
                     :</label> 
                      <div class="col-sm-5  no-padding-top"  > 
                       <input name='oTitle'  id="oTitle" type="text" value="<%=_oTitle%>" />
                      </div>
               </div>
               <div class="form-group">
                   <label class="col-sm-2 control-label" >
                     <%=ParseWording("A0019") + "(1)"%>
                     :</label> 
                      <div class="col-sm-5  no-padding-top"  > 
                       <input name='oTel1'  id="oTel1" type="text" value="<%=_oTel1%>" />
                      </div>
               </div>
               <div class="form-group">
                   <label class="col-sm-2 control-label" >
                     <%=ParseWording("A0019") + "(2)"%>
                     :</label> 
                      <div class="col-sm-5  no-padding-top"  > 
                       <input name='oTel2'  id="oTel2" type="text" value="<%=_oTel2%>" />
                      </div>
               </div>
               <div class="form-group">
                   <label class="col-sm-2 control-label" >
                     <%=ParseWording("A0019") + "(3)"%>
                     :</label> 
                      <div class="col-sm-5  no-padding-top"  > 
                       <input name='oTel3'  id="oTel3" type="text" value="<%=_oTel3%>" />
                      </div>
               </div>
               <div class="form-group">
                   <label class="col-sm-2 control-label" >
                    <%=ParseWording("B0060")%>
                     :</label> 
                      <div class="col-sm-5  no-padding-top"  > 
                       <input name='oEMail'  id="oEMail" value="<%=_oEMail%>" type="text"  />
                      </div>
               </div>
                <div class="form-group">
                   <label class="col-sm-2 control-label" >
                   <%=ParseWording("A0021")%>
                     :</label> 
                      <div class="col-sm-5  no-padding-top"  > 
                          <textarea cols='40'  id="oMemo"  name="oMemo" rows="4" class="textbox" style="Width:300px;"  ><%= _oMemo %></textarea>
                      </div>
               </div>
       </div>
        <%--聯絡人-新增 END--%>
        <asp:ScriptManager ID="myScriptManager" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PluginScriptsPlaceHolder" runat="Server">
  
        
        
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="InlineScriptsPlaceHolder" runat="Server">
 <script src="<%=ResolveUrl("~/Plugins/ace-assets/js/jquery-ui.js")%>" type="text/javascript"></script>
<script src="<%=ResolveUrl("~/Plugins/ace-assets/js/jquery.bootstrap-duallistbox.js")%>"  type="text/javascript"></script>


<script type="text/javascript">

    var demo1 = $('select[name="oGroupList"]').bootstrapDualListbox();  

 

    /*Toolbar放棄;*/
    function DoCancel() {

        $.confirm({
            title: '<%=ParseWording("A0013")%>',
            content: false,
            //content: 'Simple confirm!',
            confirm: function () {
                parent.location.replace("Organization_Main.aspx?TargerGroupID=<%:TargerGroupID%>&PageNo=<%:PageNo%>&StrSearch=<%:mySearch%>&tagUrl=Contact_List.aspx");
            },
            cancel: function () {
                //alert('Canceled!')
            }
        }); 

       
    }

    /*Toolbar返回;*/
    function DoBack() {
        parent.location.replace("Organization_Main.aspx?TargerGroupID=<%:TargerGroupID%>&PageNo=<%:PageNo%>&StrSearch=<%:mySearch%>&tagUrl=Contact_List.aspx");
    }

    /*Toolbar儲存;*/
    function DoEdt() {
        if ($('#form1').valid()) {
            if ($('#oGroupList').val() == null) {
                MasterCtrl.showErrMsg('未選擇所屬群組');
            } else {
                //alert('DoSave');
                DoSave();
            }

        } else {
            parent.MasterCtrl.showErrMsg('<%=ParseWording("A0029")%>');
        }
    }


    /*Toolbar儲存, 使用PageMethods(AJEX)新增使用者;*/
    function DoSave() {
        var Str_oGroupList_selected = $('#oGroupList').val().toString();
   
        PageMethods.EdtProcess(
                '<%:ContactID%>'
            , Str_oGroupList_selected
            , $("#oContactName").val()
            , $("#oTitle").val()
            , $("#oTel1").val()
            , $("#oTel2").val()
            , $("#oTel3").val()
            , $("#oEMail").val()
            , $("#oMemo").val()
            , '<%=MDS.Utility.NUtility.checkNull(Session["Name"])%>'
            , '<%=MIPLibrary.MIPUtil.checkSessionName(Session["Name"],Session["UserID"])%>'
            , ProcessSuccess
            , ProcessError);
    }
    /*成功時彈出訊息;*/
    function ProcessSuccess(receiveData, userContext, methodName) {
        if (methodName == 'EdtProcess') {
            if (receiveData.nRet == 0) {
                parent.MasterCtrl.showSucessMsg('<%=ParseWording("A0011")%>');
                parent.location.replace("Organization_Main.aspx?TargerGroupID=<%:TargerGroupID%>&PageNo=<%:PageNo%>&StrSearch=<%:mySearch%>&tagUrl=Contact_List.aspx");
            }
            else {
                parent.MasterCtrl.showErrMsg('<%=ParseWording("A0028")%>' + '\nnRet = ' + receiveData.nRet + '\noutMsg = ' + receiveData.outMsg);
            }
        }
    }
    /*失敗時彈出失敗訊息;*/
    function ProcessError(error, userContext, methodName) {
        if (error != null)
            MasterCtrl.showErrMsg(error.get_message());
    }


    /*自訂驗證函式*/
    function check_oGroupList_selected() {
        if ($("#oGroupList_selected option").length == 0)
            return false
        else
            return true;
    }
    $(document).ready(function () {
        $.validator.addMethod("regex",
             function (value, element, regexp) {
                 var re = new RegExp(regexp);
                 return this.optional(element) || re.test(value);
             },
              "Please check your input."
        );



        $("#form1").validate({
            errorClass: "validator-error",
            rules: {

                oContactName: {
                    required: true
                },
                oEMail: {
                    regex: "^[a-zA-Z0-9_\.\-]+\@([a-zA-Z0-9\-]+\.)+[a-zA-Z0-9]{2,4}$"
                }
                ,
                oTel1: {
                    regex: "^[0-9-()#]+$"

                },
                oTel2: {
                    regex: "^[0-9-()#]+$"

                },
                oTel3: {
                    regex: "^[0-9-()#]+$"

                }
            },
            messages: {

                oTEL1: {
                    regex: '<%=ParseWording("B0039")%>'
                },
                oTEL2: {
                    regex: '<%=ParseWording("B0039")%>'
                },
                oTEL3: {
                    regex: '<%=ParseWording("B0039")%>'
                },
                oEMail: {
                    regex: '<%=ParseWording("B0040") %>'
                }
            },
            errorPlacement: function (error, element) {

                var divId = element.attr('name') + "-validator";
                if ($('#' + divId).length) {
                    $('#' + divId).html(error);
                }
                else {
                    $(element).parent().append($("<div id='" + divId + "' class='validator-error'></div>").append(error));
                }

            },
            highlight: function (e) {
                $(e).addClass("validator-error-background");
            },
            unhighlight: function (e) {
                $(e).removeClass("validator-error-background");
            }
        });

    });
</script>
</asp:Content>
 