<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MainPage.master"  CodeFile="UserInfo_edt.aspx.cs" Inherits="DMSMainManage_UserInfo_UserInfo_edt" %>
<%@ Register Src="~/DMSControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PluginStylesPlaceHolder" runat="Server">
 
  </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
 
     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitlePlaceHolder" runat="Server">
 

         <uc1:ToolBar ID="btnSave"  Text="<%$ Resources:DMSWording, A0005 %>" PostBack="false" Enabled="true" OnClientClick="DoEdt();" runat="server" />
                
                    <uc1:ToolBar ID="btnCancel"  Text="<%$ Resources:DMSWording, A0006 %>" PostBack="false" Enabled="true" OnClientClick="DoCancel();" runat="server" />
               
                    <uc1:ToolBar ID="btnBack"  Text="<%$ Resources:DMSWording, A0026 %>" PostBack="false" Enabled="true" OnClientClick="DoBack();" runat="server" />
                
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
   
            
         
        <%--ToolBar END--%>
        
        <%--使用者資料-新增 START--%>
          <div id="UserData-form" class="form-horizontal">
              
                 <h4 class="header smaller lighter green">
                        <i class="ace-icon fa fa-check-square-o bigger-110"></i>
				        <b>使用者資料-修改</b>
                        
                    </h4>
                    <%=ParseWording("A0008")%>
                     <div class="form-group">
                       <label class="control-label col-sm-2 "   >所在群組</label>  
                       <asp:Label  ID="Label_Address" runat="server"   class="control-label col-sm-2 "  ></asp:Label>
                       
                       </div>
                     <div class="form-group">
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("A0007")%><%=ParseWording("B0012")%> ID:</label>&nbsp;&nbsp;
                      <div class="col-sm-3">
                   
                       <input name='GroupSel'  id="GroupSel" type="text" value='<%: TargerGroupID %>'  readonly='readonly'  />
                       <input name='GroupID'  id="GroupID" type="hidden" value='<%: TargerGroupID %>'  />
                   
                    </div>   </div>
                      <div class="form-group">
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("A0007")%><%=ParseWording("B0006")%>:</label>&nbsp;&nbsp;
                      <div class="col-sm-3">
              
                         <input name="AccountID"  id="AccountID" type="text" value="<%: _AccountID%>"  readonly='readonly' />
                    </div>

               </div>
                <div class="form-group">
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("A0007")%><%=ParseWording("B0007")%>:</label>&nbsp;&nbsp;
                      <div class="col-sm-2">
                            <input name='Name'  id="Name" type="text"   value="<%= _Name%>"  />
                      </div>
               </div>
                <div class="form-group">
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("B0008")%> :</label>&nbsp;&nbsp;
                      <div class="col-sm-2">
                        <input name='Description'  id="Description" type="text"   value="<%= _Description%>"  />
              
                    </div>
               </div>
                  <div class="form-group">
                    <label class="control-label col-sm-2 title  no-padding-top"   > <%=ParseWording("B0011")%> :</label>&nbsp;&nbsp;
                      <div class="col-sm-2">
                        
                          <input type="checkbox" name="Startup"  id="Startup"   <%= _Startup%>  />
                    </div>
               </div>

                <div class="form-group">
                    <label class="control-label col-sm-2 title"   > 密碼期限 :</label>&nbsp;&nbsp;
                      <div class="col-sm-4">
                        
                          <select   name="PWType" id='PWType'>
                                <option value="0"> <%=ParseWording("B0043")%>  </option>
                                 <option value="1"><%=ParseWording("B0044")%> </option>
                        </select> 
                    </div>
               </div>
                <div class="form-group">
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("B0010")%> :</label>&nbsp;&nbsp;
                      <div class="col-sm-4">
                        
                          <select   name="cRoleID" id='cRoleID'>
                               <%= cRoleID%>
                        </select> 
                        
       <button id="btnAdd" type="button" class="btn btn-round btn-primary btn-white" data-toggle="modal"
        data-target="#EditModal" onclick="RoleManage_open($('#cRoleID').val(), $('#cRoleID :selected').text());">
       <%=ParseWording("B0015")%>
    </button>

                    <input id="HiddenUserDefine"  name="HiddenUserDefine"  type="hidden"  value="0"/>
                    <input id="HiddenFunctionList"  name="HiddenFunctionList"  type="hidden"  value="<%= _HiddenFunctionList%>"/>
                    <input id="HiddenActionList"   name="HiddenActionList" type="hidden"  value="<%= _HiddenActionList%>"/>
                    </div>
               </div>
                <div class="form-group" style='display:none'>
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("B0014")%> :</label>&nbsp;&nbsp;
                      <div class="col-sm-2">
                         <input name='cCallID'  id="cCallID" type="hidden"  value="<%= _cCallID%>"  />
                        
                    </div>
               </div>
            </div>
            
        <%--使用者資料-新增 END--%>
        <asp:ScriptManager ID="myScriptManager" runat="server" EnablePageMethods="True">
            <Services>
                <asp:ServiceReference Path="~/DMSWebService.asmx" />
            </Services>
        </asp:ScriptManager>
      
 <div id="EditModal" class="modal fade" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true"  >
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                 <button type="button" id="modalclose" name="modalclose" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h3 class="modal-title" id="myModalLabel"> <%=ParseWording("B0015")%></h3>
                </div>
                <div class="modal-body">
                     
                </div>
                <div class="modal-footer">
                     
                </div>
            </div>
        </div>
    </div>

 

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PluginScriptsPlaceHolder" runat="Server">
  
        
        
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="InlineScriptsPlaceHolder" runat="Server">
 <script src="<%=ResolveUrl("~/Plugins/ace-assets/js/jquery-ui.js")%>" type="text/javascript"></script>
    
<script type="text/javascript">

    //啟用帳號:永久有效/定期變更
    $('#PWType').val(<%=_PWType%>);


    /*Toolbar放棄;*/
    function DoCancel() {
        $.confirm({
            title: '<%=ParseWording("A0013")%>',
            content: false,
            //content: 'Simple confirm!',
            confirm: function () {
                parent.location.replace("UserInfo_Main.aspx?TargerGroupID=<%:TargerGroupID%>&PageNo=<%:PageNo%>&StrSearch=<%:mySearch%>");
            },
            cancel: function () {
                //alert('Canceled!')
            }
        }); 

      }


    /*Toolbar返回;*/
    function DoBack() {
        parent.location.replace("UserInfo_Main.aspx?TargerGroupID=<%:TargerGroupID%>&PageNo=<%:PageNo%>&StrSearch=<%:mySearch%>");
    }


    /*Toolbar儲存, 驗證輸入項目*/
    function DoEdt() {
        if ($('#form1').valid()) {
            DoSave();
        } else {
            parent.MasterCtrl.showErrMsg('<%=ParseWording("A0029")%>');
        }
    }


    /*Toolbar儲存, 使用PageMethods(AJEX)修改使用者;*/
    function DoSave() {


        PageMethods.EdtProcess($("#AccountID").val()
            , $("#Name").val()
            , $("#Description").val()
            , $('#Startup').prop('checked') ? "1" : "0"
            , $("#PWType").val()
            , $("#cRoleID").val()
            , $("#HiddenUserDefine").val()
            , $("#HiddenFunctionList").val()
            , $("#HiddenActionList").val()
            , $("#cCallID").val()
            , $('#PWType').find(':selected').text()
            , $('#cRoleID').find(':selected').text()
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
                parent.location.replace("UserInfo_Main.aspx?TargerGroupID=" + $('#GroupID').val() + "&PageNo=<%:PageNo%>&StrSearch=<%:mySearch%>");
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
    
    
    /*下拉選單變更權限時, 清空上一個暫存權限微調資料;*/
    function RoleManage_onchange() {
        /*0:預設值 1:有下拉權限變更或權限微調時按下儲存;*/
        $("#HiddenUserDefine").val("1");
        $("#HiddenFunctionList").val("");
        $("#HiddenActionList").val("");
    }
    
    
    /*開啟權限微調選單;*/
    function RoleManage_open(RoldeID, RoleName) {
        var url = "RoleManage.aspx?TargerRoleID=" + RoldeID + "&TargerRoleName=" + escape(RoleName);
        $(".modal-body").html('<iframe width="500px" height="500px" frameborder="0" scrolling="yes" allowtransparency="true" src="' + url + '"></iframe>');
    }
    $(document).ready(function () {
 
 
        $("#form1").validate({
            errorClass: "validator-error",
            rules: {
 
                Name: {
                    required: true

                }
                //                     
            } ,
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