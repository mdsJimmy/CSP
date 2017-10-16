<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MainPage.master"  CodeFile="Organization_edt.aspx.cs" Inherits="DMSMainManage_Organization_Organization_edt" %>
<%@ Register Src="~/DMSControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PluginStylesPlaceHolder" runat="Server">
 
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
                   
                    <uc1:ToolBar ID="btnSave"  Text="<%$ Resources:DMSWording, A0005 %>" PostBack="false" Enabled="true" OnClientClick="DoEdt()" runat="server" />
            
                    <uc1:ToolBar ID="btnCancel" Text="<%$ Resources:DMSWording, A0006 %>" PostBack="false" Enabled="true" OnClientClick="DoCancel()" runat="server" />
               
                    <uc1:ToolBar ID="btnBack"  Text="<%$ Resources:DMSWording, A0026 %>" PostBack="false" Enabled="true" OnClientClick="DoBack();" runat="server" />
                
             
        <%--ToolBar END--%>
           </td>
                <td align="right">
                    <%--所在位置 START--%>
       
 <asp:Label ID="Label_Address" runat="server" Text="" style='text-align:right'></asp:Label>
  </td>
                   
                     <%--所在位置 END--%>
             
            </tr>
              
        </table>
        <%--ToolBar END--%>
        <div id="UserData-form" class="form-horizontal">
         <h4 class="header smaller lighter green">
                        <i class="ace-icon fa fa-check-square-o bigger-110"></i>
				        <b><%=ParseWording("B0046")%></b>
                        
                    </h4>
                    <%=ParseWording("A0008")%>
           <!--所屬群組-->
                 <div class="form-group">
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("A0007")%><%=ParseWording("B0012")%>:</label>&nbsp;&nbsp;
                      <div class="col-sm-2">
                   
                       <input name='oParentGroupID_Sel'  id="oParentGroupID_Sel" type="text" value='<%= _oParentGroupID_Sel %>' readonly="readonly" />
                       <input name='oParentGroupID'  id="oParentGroupID" type="hidden" value='<%= _oParentGroupID %>'  />
                   
                    </div>
               </div>
			     <!--代碼-->
                 <div class="form-group">
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("A0007")%><%=ParseWording("B0047")%>:</label>&nbsp;&nbsp;
                      <div class="col-sm-2">
              
                  <input name="oGroupID"  id="oGroupID" type="text"  value='<%= _oGroupID %>' readonly="readonly"  />
                    </div>
               </div>
			    <!--名稱-->
                <div class="form-group">
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("A0007")%><%=ParseWording("B0101")%>:</label>&nbsp;&nbsp;
                      <div class="col-sm-2">
              

                <input name='oGroupName'  id="oGroupName" type="text"  value='<%= _oGroupName %>'  />

                    </div>
               </div>
			     <!--電話-->
        <div class="form-group">
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("A0019")%> :</label>&nbsp;&nbsp;
                      <div class="col-sm-2">
                        <input name='oTEL'  id="oTEL" type="text"  value='<%= _oTEL %>' />
              
                    </div>
               </div>
	   
	   <!--地址-->
        <div class="form-group">
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("A0020")%> :</label>&nbsp;&nbsp;
                      <div class="col-sm-2">
             
                <input name='oAddress'  id="oAddress" type="text" value='<%= _oAddress %>'  />
                    </div>
               </div>
	     <!--備註-->
   <div class="form-group">
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("A0021")%> :</label>&nbsp;&nbsp;
                      <div class="col-sm-2">
              <textarea cols='40'  id="oMemo"  name="oMemo" rows="4" class="textbox" style="Width:300px;"  ><%= _oMemo %></textarea>
                    </div>
               </div>
       </div>
        <%--群組-修改 END--%>
        <asp:ScriptManager ID="myScriptManager" runat="server" EnablePageMethods="True">
            <Services>
                <asp:ServiceReference Path="~/DMSWebService.asmx" />
            </Services>
        </asp:ScriptManager>
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
           
            confirm: function () {
                parent.location.replace("Organization_Main.aspx?TargerGroupID=<%:TargerGroupID%>&PageNo=<%:PageNo%>&StrSearch=<%:mySearch%>");
            },
            cancel: function () {
                //alert('Canceled!')
            }
        });
       
    }

    /*Toolbar返回;*/
    function DoBack() {
        parent.location.replace("Organization_Main.aspx?TargerGroupID=<%:TargerGroupID%>&PageNo=<%:PageNo%>&StrSearch=<%:mySearch%>");
    }

    /*Toolbar儲存, 驗證輸入項目*/
    function DoEdt() {
        //alert($('#form1').valid());
        if ($('#form1').valid()) {
            DoSave();
        }else {
            parent.MasterCtrl.showErrMsg('<%=ParseWording("A0029")%>');
        }
    }


    /*Toolbar儲存, 使用PageMethods(AJEX)修改使用者;*/
    function DoSave() {


        PageMethods.EdtProcess($("#oParentGroupID_Sel").val()
            , $("#oParentGroupID").val()
            , $("#oGroupID").val()
            , $("#oGroupName").val()
            , $("#oTEL").val()
            , $("#oAddress").val()
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
                if (parent != null) {
                    parent.MasterCtrl.showSucessMsg('<%=ParseWording("A0011")%>');
                    parent.location.replace("Organization_Main.aspx?TargerGroupID=" + $("#oParentGroupID").val() + "&PageNo=<%:PageNo%>&StrSearch=<%:mySearch%>");
                }
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


        if (error != null) {
            parent.MasterCtrl.showErrMsg(error.get_message());

        }

    }

    $(document).ready(function () {
        /*所屬群組下拉選單*/

        $("#oParentGroupID_Sel").autocomplete({
            source: function (request, response) {
                $.ajax(
                         {
                             url: '../../DMSWebService.asmx/GetGroupList',
                             type: "POST",
                             //contentType: "text/xml; charset=utf-8 ",
                             dataType: 'xml',
                             data: "q=" + $("#oParentGroupID_Sel").val(),
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
                    $("#oParentGroupID").val(ui.item.value);
                    $("#oParentGroupID_Sel").val(ui.item.value + " " + ui.item.result);
                }
                else {
                    $("#oParentGroupID").val("");
                }

                /*使用WebService function取得SiteMapAddress*/
                DMSWebService.GET_SiteMapAddress($("#oParentGroupID").val(), ProcessSuccess, ProcessError);
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
                oParentGroupID_Sel: {
                    required: true,
                    remote: function () {
                        var r = {
                            type: "POST",
                            url: 'Organization_add.aspx/CheckParentGroupID',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: JSON.stringify({
                                'ParentGroupID': $('#oParentGroupID_Sel').val(),
                                'validateId': "", 'validateValue': $('#oParentGroupID_Sel').val(), 'validateError': "", 'oParentGroupID': $('#oParentGroupID').val()
                            }),
                            dataFilter: function (data) {
                                 
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
                oGroupID: {
                    required: true
                },
                oGroupName: {
                    required: true
                }
                ,
                oTEL: {
                    regex: "^[0-9-()#]+$"

                }
            },
            messages: {
                oParentGroupID_Sel: {
                    remote: function () { return ('*<%=ParseWording("B0049")%>').replace('#ReplaceString#', $("#oParentGroupID_Sel").val()); }
                },
                oTEL: {
                    regex: '<%=ParseWording("B0039")%>'
                },
                oGroupID: {
                    regex: '<%=ParseWording("B0140")%>'
//                    ,
//                    remote: '<%=ParseWording("B0048")%>'
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
 
  
