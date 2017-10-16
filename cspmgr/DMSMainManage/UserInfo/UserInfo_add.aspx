<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage/MainPage.master"  CodeFile="UserInfo_add.aspx.cs" Inherits="DMSMainManage_UserInfo_UserInfo_add" %>
<%@ Register Src="~/DMSControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PluginStylesPlaceHolder" runat="Server">
 
  </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
 
     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitlePlaceHolder" runat="Server">
 
         <uc1:ToolBar ID="btnSave"  Text="<%$ Resources:DMSWording, A0005 %>" PostBack="false" Enabled="true" OnClientClick="DoAdd()" runat="server" />
            
                    <uc1:ToolBar ID="btnCancel" Text="<%$ Resources:DMSWording, A0006 %>" PostBack="false" Enabled="true" OnClientClick="DoCancel()" runat="server" />
        
    

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
      
            
        
        <%--使用者資料-新增 START--%>
          <div id="UserData-form" class="form-horizontal">
              
                 <h4 class="header smaller lighter green">
                        <i class="ace-icon fa fa-check-square-o bigger-110"></i>
				        <b><%=ParseWording("B0004")%></b>
                        
                    </h4>
                    <%=ParseWording("A0008")%>
                    <div class="form-group" >
                        <label class="control-label col-sm-2 "   >所在群組</label> 
                        <asp:Label  ID="Label_Address" runat="server"  CssClass=" col-sm-10  " ></asp:Label> 
                    </div>      
                       
                  
                     <div class="form-group">
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("A0007")%><%=ParseWording("B0012")%>ID:</label>&nbsp;&nbsp;
                      <div class="col-sm-3">
                   
                       <input name='GroupSel'  id="GroupSel" type="text" value='<%: TargerGroupID %>'  readonly='readonly'  />
                       <input name='GroupID'  id="GroupID" type="hidden" value='<%: TargerGroupID %>'  />
                   
                    </div>   </div>
                      <div class="form-group">
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("A0007")%><%=ParseWording("B0006")%>:</label>&nbsp;&nbsp;
                      <div class="col-sm-3">
              
                         <input name="AccountID"  id="AccountID" type="text"   />
                    </div>

               </div>

                <div class="form-group">
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("A0007")%><%=ParseWording("C0006")%>:</label>&nbsp;&nbsp;
                      <div class="col-sm-3">
              
                         <input name="pwd"  id="pwd" type="password"   />
                    </div>

               </div>

              <div class="form-group">
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("A0007")%><%=ParseWording("C0007")%>:</label>&nbsp;&nbsp;
                      <div class="col-sm-3">
              
                         <input name="repwd"  id="repwd"  type="password"   />
                    </div>

               </div>


                <div class="form-group">
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("A0007")%><%=ParseWording("B0007")%>:</label>&nbsp;&nbsp;
                      <div class="col-sm-2">
                            <input name='Name'  id="Name" type="text"    />
                      </div>
               </div>
                <div class="form-group">
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("B0008")%> :</label>&nbsp;&nbsp;
                      <div class="col-sm-2">
                        <input name='Description'  id="Description" type="text"  />
              
                    </div>
               </div>
                  <div class="form-group">
                    <label class="control-label col-sm-2 title  no-padding-top"   > <%=ParseWording("B0011")%> :</label>&nbsp;&nbsp;
                      <div class="col-sm-2">
                        
                          <input type="checkbox" name="Startup"  id="Startup" checked="checked" />
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

                    <input id="HiddenUserDefine"  name="HiddenUserDefine"  type="hidden" value="0"/>
                    <input id="HiddenFunctionList"  name="HiddenFunctionList"  type="hidden"/>
                    <input id="HiddenActionList"   name="HiddenActionList" type="hidden"/>
                    </div>
               </div>
                <div class="form-group" style='display:none'>
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("B0014")%> :</label>&nbsp;&nbsp;
                      <div class="col-sm-2">
                         <input name='cCallID'  id="cCallID" type="hidden"  />
                        
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


    function DoAdd() {
       
        if ($('#form1').valid()) {
            DoSave();
        }else{
            parent.MasterCtrl.showErrMsg('<%=ParseWording("A0029")%>');
        }
    }

    
    /*Toolbar儲存, 使用PageMethods(AJEX)新增使用者;*/
    function DoSave() {
        
        PageMethods.AddProcess($("#GroupID").val()
            , $("#AccountID").val()
            , $("#repwd").val()
            , $("#Name").val()
            , $("#Description").val()
            , $('#Startup').prop('checked') ? "1" : "0"
            , $("#PWType").val()
            , $("#cRoleID").val()
            , $("#HiddenUserDefine").val()
            , $("#HiddenFunctionList").val()
            , $("#HiddenActionList").val()
            , $("#cCallID").val()
            , $('#GroupSel').val()
            , $('#PWType').find(':selected').text()
            , $('#cRoleID').find(':selected').text()
            , '<%=MDS.Utility.NUtility.checkNull(Session["Name"])%>'
            , '<%=MIPLibrary.MIPUtil.checkSessionName(Session["Name"],Session["UserID"])%>'
            , ProcessSuccess
            , ProcessError);
    }
    /*成功時彈出訊息;*/
    function ProcessSuccess(receiveData, userContext, methodName) {
        if (methodName == 'AddProcess') { 
            if (receiveData.nRet == 0) {
                parent.MasterCtrl.showSucessMsg('<%=ParseWording("A0011")%>');
                parent.location.replace("UserInfo_Main.aspx?TargerGroupID=" + $('#GroupID').val() + "&PageNo=<%:PageNo%>&StrSearch=<%:mySearch%>");
            }
            else {
                parent.MasterCtrl.showErrMsg('<%=ParseWording("A0028")%>' + '\nnRet = ' + receiveData.nRet + '\noutMsg = ' + receiveData.outMsg);
            }

        }

        if (methodName == 'GET_SiteMapAddress') {
            $("#Label_Address").html('<%=ParseWording("A0014")%>' + ' ' + receiveData);
        }  
    }
    /*失敗時彈出失敗訊息;*/
    function ProcessError(error, userContext, methodName) {
 
       if (error != null)  
             MasterCtrl.showErrMsg(error.get_message());
    }
    
    
    /*下拉選單變更權限時, 清空上一個暫存權限微調資料;*/
    function RoleManage_onchange() {
        /*0:未設定權限微調 1:有設定;*/
        $('#HiddenUserDefine').val("0");
        $('#HiddenFunctionList').val("");
        $('#HiddenActionList').val("");
    }
    
    
    /*開啟權限微調選單;*/
    function RoleManage_open(RoldeID, RoleName) {
       // alert('RoldeID>' + RoldeID + '<RoleName>' + RoleName);
        var url =  "RoleManage.aspx?TargerRoleID=" + RoldeID + "&TargerRoleName=" + escape(RoleName) ;
        $(".modal-body").html('<iframe width="500px" height="500px" frameborder="0" scrolling="yes" allowtransparency="true" src="' + url + '"></iframe>');



        //showModalDialog("RoleManage.aspx?TargerRoleID=" + RoldeID + "&TargerRoleName=" + escape(RoleName), window, "center=yes;dialogWidth=600px;dialogHeight=600px;status=no;resizable=no;help=no;scroll=no;");
    }
    $(document).ready(function () {
        
        $("#GroupSel").autocomplete({
            source: function (request, response) {
                $.ajax(
                         {
                             url: '../../DMSWebService.asmx/GetGroupList',
                             type: "POST",
                             //contentType: "text/xml; charset=utf-8 ",
                             dataType: 'xml',
                             data: "q=" + $("#GroupSel").val(),
                             success: function (data, textStatus, jqXHR) {

                                 var parsed = [];
                                 // if (data) {
                                 $(data).find('string').each(function () {
                                     // alert($(this).text());
                                     data = $.parseJSON($(this).text());

                                     for (var i = 0; i < data.length; i++) {

                                         parsed[parsed.length] = {
                                             data: data[i],
                                             value: data[i].GroupID,
                                             result: data[i].GroupName
                                         };
                                     }

                                 });

                                 //alert(parsed.length);
                                 response(parsed);
                             },
                             error: function (jqXHR, textStatus, errorThrown) {
                                 //alert(errorThrown + '>' + textStatus);
                                 // Handle errors here
                             },

                             statusCode:
                             {

                         }
                     });

            },
            select: function (event, ui) {

                if (ui.item) {
                    $("#GroupID").val(ui.item.value);
                    $("#GroupSel").val(ui.item.value + " " + ui.item.result);
                }
                else {
                    $("#GroupID").val("");
                }

                /*使用WebService function取得SiteMapAddress*/
                DMSWebService.GET_SiteMapAddress($("#GroupID").val(), ProcessSuccess, ProcessError);
            },
            messages: {
                noResults: 'No results found.',
                results: function (count) {
                    return count + (count > 1 ? ' results' : ' result ') + ' found';
                }
            }

        });


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

                     GroupSel: {
                         required: true,
                         remote: function () {
                             var r = {
                                 type: "POST",
                                 url: 'UserInfo_add.aspx/CheckGroupID',
                                 contentType: "application/json; charset=utf-8",
                                 dataType: "json",
                                 data: JSON.stringify({
                                     'ParentGroupID': '<%=MDS.Utility.NUtility.checkNull(Session["ParentGroupID"])%>',
                                     'validateId': "", 'validateValue': "", 'validateError': "", 'GroupID': $('#GroupID').val()
                                 }),
                                 //string validateId, string validateValue, string validateError, string ParentGroupID,   GroupID
                                 dataFilter: function (data) {
                                     // alert('dataFilter>' + data);
                                     var json = JSON.parse(data);
                                     if (json.d == "True") {
                                         return true;
                                     } else {
                                         return false;
                                     }
                                 }
                             }
                             return r;
                         }
                     },
                     AccountID: {
                        required: true,
                        regex: "^[0-9a-zA-Z\.\-_]+$",
                        remote: function () {
                            var r = {
                                type: "POST",
                                url: 'UserInfo_add.aspx/CheckAccountIDProcess',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                data: JSON.stringify({

                                    'validateId': "", 'validateValue': $('#AccountID').val(), 'validateError': ""
                                }),
                                //string validateId, string validateValue, string validateError, string ParentGroupID,   GroupID
                                dataFilter: function (data) {
                                    // alert('dataFilter>' + data);
                                    var json = JSON.parse(data);
                                    if (json.d == "True") {
                                        return true;
                                    } else {
                                        return false;
                                    }
                                }
                            }
                            return r;
                        }
                     }
                ,
                     pwd: {
                         maxlength: 30,
                         required: true,
                         //notEqualTo: "#repwd",
                        // pwd: /^(?=.*\d)(?=.*[a-zA-Z])(?=.*\W).{8,50}$/
                     },

                     repwd: {
                         maxlength: 30,
                         required: true,
                         equalTo: "#pwd",
                         //pwd: /^(?=.*\d)(?=.*[a-zA-Z])(?=.*\W).{8,50}$$/
                     },

                     Name: {
                         required: true

                     }
//                     
                 },
                 messages: {

                     GroupSel: {
                         regex: '<%=ParseWording("B0039")%>',
                         remote: '<%=ParseWording("B0018")%>'
                     },
                     AccountID: {
                         regex: '<%=ParseWording("B0069")%>' ,
                         remote: '<%=ParseWording("B0068")%>'
                     },
                    
                     repwd: {
                         required:'請輸入密碼',
                         equalTo: "與新密碼不同！"
                     },
                     pwd: {
                         required: '請輸入密碼',
                         
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
  