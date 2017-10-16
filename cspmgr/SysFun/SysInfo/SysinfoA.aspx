<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainPage.master" AutoEventWireup="true"
    CodeFile="SysinfoA.aspx.cs" Inherits="yuantalife_web_YL0010A" %>

<%@ Register Src="~/DMSControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PluginStylesPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="<%=ResolveUrl("~/Plugins/ace-assets//css/bootstrap-datetimepicker.css")%>" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitlePlaceHolder" runat="Server">
    <uc1:ToolBar ID="btnSave" Text="<%$ Resources:DMSWording, A0005 %>" PostBack="false"
        Enabled="true" OnClientClick="DoAdd()" OnClick="btnUpload_Click" runat="server" />
    <uc1:ToolBar ID="btnCancel" Text="<%$ Resources:DMSWording, A0006 %>" PostBack="false"
        Enabled="true" OnClientClick="DoCancel();" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="UserData-form" class="form-horizontal">
        <h4 class="header smaller lighter green">
            <i class="ace-icon fa fa-check-square-o bigger-110"></i><b>最新公告資料上稿-新增 </b>
        </h4>
        <%=ParseWording("A0008")%>
        <div class="space-6">
        </div>
        <%--發布對象--%>
        <div class="form-group">
            <label class="col-sm-2 control-label">
                <%=ParseWording("A0007")%>代碼 :</label>
            <div class="col-sm-5  no-padding-top">
                <input type="text" id="_selAppType" name="_selAppType"  class="textbox" style="width: 50%"/>                    
                 
            </div>
        </div>
        <%--是否啟用--%>
        <div class="form-group">
            <label class="col-sm-2 control-label">
                <%=ParseWording("A0007")%>顯示位置 :</label>
            <div class="col-sm-5  no-padding-top">
                <select id="_selNewsKind" name="_selNewsKind" runat="server" style="width: 50%">
                </select>
            </div>
        </div>
         <%--是否要給app用--%>
        <div class="form-group"  style="display:none">
            <label class="col-sm-2 control-label">
                <%=ParseWording("A0007")%>提供 APP 使用 :</label>
            <div class="col-sm-5  no-padding-top">
                <select id="_selAPP4" name="_selAPP4" runat="server" style="width: 50%">
                </select>
            </div>
        </div>
        <%--資料內容--%>
        <div class="form-group">
            <label class="col-sm-2 control-label">
                <%=ParseWording("A0007")%> 說明 :</label>
            <div class="col-sm-8  no-padding-top">
                <input type="text" id="_txtNewsTitle" name="_txtNewsTitle" class="textbox" maxlength="200"
                    style="width: 100%" />
            </div>
        </div>
        <%--說明--%>
        <div class="form-group">
            <label class="col-sm-2 control-label">
                資料內容:</label>
            <div class="col-sm-8  no-padding-top">
                <textarea id="_strInfo" name="_strInfo" rows="10" style="width:100%" class="textbox"></textarea>
            </div>
        </div>
     

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
    <script type="text/javascript">

        jQuery(function ($) {
            var now = moment();
            var now_b = moment().add(+1, 'day');
            var startdate = document.getElementById("_txtStartDate");
            var enddate = document.getElementById("_txtEndDate");
            $(startdate).datetimepicker({
                //defaultDate: now,
                format: 'YYYY/MM/DD'
            });
            $(enddate).datetimepicker({
                //defaultDate: now_b,
                format: 'YYYY/MM/DD'
            });
            $(startdate).datetimepicker().next().on(ace.click_event, function () {
                $(this).prev().focus();
            });
            $(enddate).datetimepicker().next().on(ace.click_event, function () {
                $(this).prev().focus();
            });

            $("#form1").validate({
                errorClass: "validator-error",
                rules: {
                    _txtNewsTitle: {
                        required: true
                    },
                    _txtMainLinkUrl: {                    
                        url: true
                    },
                    _txtOneLinkUrl: {
                        url: true
                    },
                    _txtTwoLinkUrl: {
                        url: true
                    },
                    _txtThreeeLinkUrl: {
                        url: true
                    },
                    <%:_selNewsKind.ClientID%>: {
                        required: true
                    },
                    _txtOrder: {
                        required: true
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

        $(document).ready(function () {

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
                            '_txtOneLinkUrl': {
                                'regex': /^(https?:\/\/)[A-Za-z0-9\-]+\.[A-Za-z0-9\-]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/,
                                'alertText': '*URL格式不正確，應為http://xxx.xxx'
                            },
                            '_txtTwoLinkUrl': {
                                'regex': /^(https?:\/\/)[A-Za-z0-9\-]+\.[A-Za-z0-9\-]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/,
                                'alertText': '*URL格式不正確，應為http://xxx.xxx'
                            },
                            '_txtThreeeLinkUrl': {
                                'regex': /^(https?:\/\/)[A-Za-z0-9\-]+\.[A-Za-z0-9\-]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/,
                                'alertText': '*URL格式不正確，應為http://xxx.xxx'
                            }

                        }
                    }
                }
            })(jQuery);

            //DOM setup validate Class;
            //畫面上的必輸欄位"必填"資訊
            //$("#FileUpload1").addClass('validate[required]');
            //$("#FileDesc1").addClass('validate[required]');
            //$('#CreateStartDate1').addClass('validate[required]');
            //$('#CreateEndDate1').addClass('validate[required]');
            //$('#Url1').addClass('validate[required],custom[checkUrl]');

            //Startup formValidator;
            //$.validationEngineLanguage.newLang();
            //$("#add").validationEngine();

            /*formValidator END*/
        });


        /*Toolbar放棄;*/
        function DoCancel() {
            $.confirm({
                title: '<%=ParseWording("A0013")%>',
                content: false,
                //content: 'Simple confirm!',
                confirm: function () {
                    
                    parent.location.replace("SysinfoQ.aspx?a=a"+ unescape(decodeURI("<%=MDS.Utility.NUtility.UrlEncode(strQueryCondi)%>")));
                },
                cancel: function () {
                    //alert('Canceled!')
                }
            });

        }


        function DoAdd() {

            //DoSave();
            if ($('#form1').valid()) {
                DoSave();
            } else {
                parent.MasterCtrl.showErrMsg('<%=ParseWording("A0029")%>');
            }

        }


                /*Toolbar儲存, 使用PageMethods(AJEX)新增使用者;*/
        function DoSave() {
            //alert("DoSave");
            //$("#_selNewsKind.ClientID").val()
            //alert($('#<=_selNewsKind.ClientID>').val());
            //alert($("select[@name=_selNewsKind] option[@selected]").text());           
            //__doPostBack("<%= _selNewsKind.ClientID %>", "OnClick");           
            //$('#<= Button1.ClientID >').click();
            //var btn = document.getElementById("<= Button1.ClientID >");
            //alert("btn:" + btn);
            //btn.click(); 

            //alert($('#<%:_selNewsKind.ClientID%>').val());

            
            PageMethods.AddNewsProcess(
              $("#_selAppType").val()
            , $('#<%:_selNewsKind.ClientID%>').val()
            , $('#<%:_selAPP4.ClientID%>').val()
            , $("#_txtNewsTitle").val()
            , $("#_strInfo").val()
            , ProcessSuccess
            , ProcessError);
        }


        /*Toolbar儲存, 使用PageMethods(AJEX)新增使用者;*/
        function _DoSave() {
            //alert("DoSave");
            //$("#_selNewsKind.ClientID").val()
            //alert($('#<=_selNewsKind.ClientID>').val());
            //alert($("select[@name=_selNewsKind] option[@selected]").text());           
            //__doPostBack("<%= _selNewsKind.ClientID %>", "OnClick");           
            //$('#<= Button1.ClientID >').click();
            //var btn = document.getElementById("<= Button1.ClientID >");
            //alert("btn:" + btn);
            //btn.click(); 

            //alert($('#<%:_selNewsKind.ClientID%>').val());

        }

        /*成功時彈出訊息;*/
        function ProcessSuccess(receiveData, userContext, methodName) {
            if (methodName == 'AddNewsProcess') {
                if (receiveData.nRet == 0) {
                    parent.MasterCtrl.showSucessMsg('<%=ParseWording("A0011")%>');
                    
                    parent.location.replace("SysinfoQ.aspx?a=a"+ unescape(decodeURI("<%=MDS.Utility.NUtility.UrlEncode(strQueryCondi)%>")));
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
