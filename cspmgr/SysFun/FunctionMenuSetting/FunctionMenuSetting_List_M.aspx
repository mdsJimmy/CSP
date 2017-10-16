<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainPage.master" AutoEventWireup="true"
    CodeFile="FunctionMenuSetting_List_M.aspx.cs" Inherits="yuantalife_web_YL0010M" %>

<%@ Register Src="~/DMSControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PluginStylesPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="<%=ResolveUrl("~/Plugins/ace-assets//css/bootstrap-datetimepicker.css")%>" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitlePlaceHolder" runat="Server">
    <uc1:ToolBar ID="btnSave" Text="<%$ Resources:DMSWording, A0005 %>" PostBack="false"
        Enabled="true" OnClientClick="DoCheckSave()" OnClick="btnUpload_Click" runat="server" />
    <uc1:ToolBar ID="btnCancel" Text="<%$ Resources:DMSWording, A0006 %>" PostBack="false"
        Enabled="true" OnClientClick="DoCancel();" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="UserData-form" class="form-horizontal">
        <h4 class="header smaller lighter green">
            <i class="ace-icon fa fa-check-square-o bigger-110"></i><b>功能選單設定-維護 </b>
        </h4>
        <%=ParseWording("A0008")%>
        <div class="space-6">
        </div>
        <%--SysFuncID--%>
        <div class="form-group">
            <label class="col-sm-2 control-label">
                <%=ParseWording("A0007")%>SysFuncID :</label>
            <div class="col-sm-5  no-padding-top">
                <Label id="_sysFuncID"  runat="server" style="width: 80%"  >                    
                </Label>
            </div>
        </div>
        <%--模組--%>
        <div class="form-group">
            <label class="col-sm-2 control-label">
                <%=ParseWording("A0007")%>模組 :</label>
            <div class="col-sm-5  no-padding-top">
                <select id="_sysModID" name="_sysModID" runat="server" style="width: 50%" >
                </select>
            </div>
        </div>
         <%--系統名稱--%>
        <div class="form-group">
            <label class="col-sm-2 control-label">
                <%=ParseWording("A0007")%>系統名稱 :</label>
            <div class="col-sm-8  no-padding-top">
                <input type="text" id="_functionDesc" name="_functionDesc" class="textbox" 
                    style="width:50%" />
            </div>
        </div>
        <%--uri--%>
        <div class="form-group" id="tt">
            <label class="col-sm-2 control-label">
                <%=ParseWording("A0007")%>Url :</label>
            <div class="col-sm-8  no-padding-top">
                <input type="text" id="_pageLink" name="_pageLink" class="textbox" maxlength="200"
                    style="width: 100%" />
            </div>
        </div>
        <%--預設圖片--%>
        <div class="form-group">
            <label class="col-sm-2 control-label">
                預設圖片 :</label>
            <div class="col-sm-8  no-padding-top">
                <select id="_pic" name="_pic" runat="server" style="width: 50%" >
                </select>
            </div>
        </div>
        <%--排序--%>
        <div class="form-group">
            <label class="col-sm-2 control-label">
                <%=ParseWording("A0007")%>排序 :</label>
            <div class="col-sm-8  no-padding-top">
               <input id="_iOrder" name="_iOrder" runat="server" style="width: 50%" class="textbox" />
                
            </div>
        </div>
        <%--顯示--%>
        <div class="form-group">
            <label class="col-sm-2 control-label">
                <%=ParseWording("A0007")%>是否顯示 :</label>
            <div class="col-sm-8  no-padding-top">
                <select id="_iDisplay" name="_iDisplay" runat="server" style="width: 50%" >
                </select>
            </div>
        </div>
       
       
        <input type="hidden" id="method" name="method" runat="server" value="" />
        <input type="hidden" id="_hidNewsId" name="_hidNewsId" value="" />
    </div>
    <%--新增 END--%>
    <asp:ScriptManager ID="myScriptManager" runat="server" EnablePageMethods="True">
    </asp:ScriptManager>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PluginScriptsPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="InlineScriptsPlaceHolder" runat="Server">
    <script src="<%=ResolveUrl("~/Plugins/ace-assets/js/ace-elements.js")%>"></script>
    <script src="<%=ResolveUrl("~/Plugins/ace-assets/js/ace.js")%>"></script>
    <script src="<%=ResolveUrl("~/Plugins/ace-assets/js/jquery-ui.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Plugins/ace-assets/js/date-time/moment.js")%>"></script>
    <script src="<%=ResolveUrl("~/Plugins/ace-assets/js/date-time/bootstrap-datetimepicker.js")%>"></script>
    <script type="text/javascript">{}
       
        jQuery(function ($) {
            $.validator.addMethod(
                      "regex",
                      function(value, element,param) {
                          
                          return this.optional(element) || value.match(param);
                      },
                      "路徑格式錯誤，格式為../xxxx"
              );
            
           
            $('#<%:_sysFuncID.ClientID%>').html('<%:MDS.Utility.NUtility.checkString(strSysFuncID)%>')
           
            $("#_functionDesc").val('<%:MDS.Utility.NUtility.checkString(strFunctionDesc)%>'); //主連結URL
            $('#<%:_iOrder.ClientID%>').val('<%:MDS.Utility.NUtility.checkString(strOrder)%>'); //主連結URL
            $("#_pageLink").val('<%:MDS.Utility.NUtility.checkString(strPageLink)%>'); //主連結文字標題
           
           
           
           

            $("#form1").validate({
                errorClass: "validator-error",
                rules: {
                     <%:_sysFuncID.ClientID%>: {
                        required: true
                    },
                     <%:_sysModID.ClientID%>: {
                        required: true
                    },
                     <%:_iDisplay.ClientID%>: {
                        required: true
                    },
                    _pageLink: {
                       
                        required:true,
                        regex:/^(\.\.\/)[A-Za-z0-9\-\/_]*/
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

       <%-- $(document).ready(function () {
           
            /*formValidator START*/
            //Set formValidator rules;
            (function ($) {
                $.fn.validationEngineLanguage = function () { };
                $.validationEngineLanguage = {
                    newLang: function () {
                        $.validationEngineLanguage.allRules = {
                            'required': {
                                'regex': 'none',
                                'alertText': '*<%=ParseWording("A0012")%>'
                            },
                            'ckurl': {
                                "regex": /^(\.\.\/)[A-Za-z0-9\-\/_]*/,
                                'alertText': '*URL格式不正確，應為../xxx/xxx'
                            }

                        }
                    }
                }
            })(jQuery);--%>
          
            //DOM setup validate Class;
            //畫面上的必輸欄位"必填"資訊
            //$("#FileUpload1").addClass('validate[required]');
            //$("#FileDesc1").addClass('validate[required]');
            //$('#CreateStartDate1').addClass('validate[required]');
            //$('#CreateEndDate1').addClass('validate[required]');
           // $('#Url1').addClass('validate[required],custom[checkUrl]');
            
            //Startup formValidator;
            //$.validationEngineLanguage.newLang();
            //$("#add").validationEngine();

            /*formValidator END*/
        //});


        /*Toolbar放棄;*/
        function DoCancel() {
            $.confirm({
                title: '<%=ParseWording("A0013")%>',
                content: false,
                //content: 'Simple confirm!',
                confirm: function () {
                    parent.location.replace("FunctionMenuSetting_List_Q.aspx?a=a" + unescape("<%=MDS.Utility.NUtility.UrlEncode(strQueryCondi)%>"));
                },
                cancel: function () {
                    //alert('Canceled!')
                }
            });

        }

        function DoCheckSave() {
          
           <%-- if ($('#<%:_selAppType.ClientID%>').val() == '') {
                $('#<%:_selAppType.ClientID%>').addClass("validator-error-background");              
                return;
            } else {
                $('#<%:_selAppType.ClientID%>').removeClass("validator-error-background");
            }--%>

            if ($('#form1').valid()) {
                //$('#form1').submit();
                
                DoSave();
                
            } else {
                parent.MasterCtrl.showErrMsg('<%=ParseWording("A0029")%>');
            }

        }
        console.log('FFFFFFF')
        /*Toolbar儲存, 使用PageMethods(AJEX)新增使用者;*/
        function DoSave() {
            // UpdateNewsProcess(string strSysModID, string strSysFuncID, string strFunctionDesc, 
            //string strPageLInk, string strPic, string strOrder,string striDisplay)  

            console.log($('#<%:_iDisplay.ClientID%>').val())

            PageMethods.UpdateNewsProcess(            
              $('#<%:_sysModID.ClientID%>').val()  
            , $('#<%:_sysFuncID.ClientID%>').html()
            
            , $("#_functionDesc").val()
            , $("#_pageLink").val()
            , $('#<%:_pic.ClientID%>').val()
            , $('#<%:_iOrder.ClientID%>').val()
            , $('#<%:_iDisplay.ClientID%>').val()
            
            , ProcessSuccess
            , ProcessError);
        }

        /*成功時彈出訊息;*/
        function ProcessSuccess(receiveData, userContext, methodName) {
            if (methodName == 'UpdateNewsProcess') {
                if (receiveData.nRet == 0) {
                    parent.MasterCtrl.showSucessMsg('<%=ParseWording("A0011")%>');
                    //parent.location.replace("YL0010Q.aspx?PageNo=<%:PageNo%>");
                    parent.location.replace("FunctionMenuSetting_List_Q.aspx?a=a" + unescape("<%=MDS.Utility.NUtility.UrlEncode(strQueryCondi)%>"));
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
