<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainPage.master" AutoEventWireup="true" CodeFile="SubFunctionMenuSetting_A.aspx.cs" Inherits="SysFun_FunctionMenuSetting_SubFunctionMenuSetting_SubFunctionMenuSetting_A" %>
<%@ Register Src="~/DMSControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PluginStylesPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitlePlaceHolder" Runat="Server">
    <uc1:ToolBar ID="btnSave" Text="<%$ Resources:DMSWording, A0005 %>" PostBack="false"
        Enabled="true" OnClientClick="DoAdd()"  runat="server" />
    <uc1:ToolBar ID="btnCancel" Text="<%$ Resources:DMSWording, A0006 %>" PostBack="false"
        Enabled="true" OnClientClick="DoCancel();" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div id="UserData-form" class="form-horizontal">
        <h4 class="header smaller lighter green">
            <i class="ace-icon fa fa-check-square-o bigger-110"></i><b>子選單功能設定-新增 </b>
        </h4>
        <%=ParseWording("A0008")%>
        <div class="space-6"></div>

          <div class="form-group">
            <asp:Label ID="Label4" runat="server"  CssClass="col-sm-2 control-label">Button序號：</asp:Label>
            <asp:TextBox ID="TextBox3" runat="server" CssClass="col-sm-4  padding-top padding-left-10px "  Enabled="false" ClientIDMode="Static"></asp:TextBox>
        </div>
        <div class="form-group">
             <asp:Label ID="Label1" runat="server" Text="Label" CssClass="col-sm-2 control-label">Button名稱：</asp:Label>
             <asp:TextBox ID="TextBox1" runat="server" CssClass="col-sm-2  textbox" ClientIDMode="Static"></asp:TextBox>
        </div>
         <div class="form-group">
             <asp:Label ID="Label6" runat="server" Text="Label" CssClass="col-sm-2 control-label">ButtonID：</asp:Label>
             <asp:TextBox ID="TextBox4" runat="server" CssClass="col-sm-2  textbox" ClientIDMode="Static"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Label ID="Label2" runat="server" Text="Label" CssClass="col-sm-2 control-label">功能名稱：</asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="col-sm-2  no-padding-top"  DataSourceID="SqlDataSource1" DataTextField="FunctionDesc" DataValueField="SysFuncID"   onchange="getProKind()" ClientIDMode="Static" AppendDataBoundItems="True">
                <asp:ListItem  Value="" ></asp:ListItem>
            </asp:DropDownList>
        </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="select FunctionDesc,SysFuncID,b.ModuleDesc,a.SysModID from SystemFunction a, SystemModule b where a.SysModID=b.SysModID  order by a.iOrder"></asp:SqlDataSource>
       
        <div class="form-group">
            <asp:Label ID="Label3" runat="server"  CssClass="col-sm-2 control-label">模組名稱：</asp:Label>
            <asp:TextBox ID="TextBox0" runat="server" CssClass="col-sm-2  padding-top padding-left-10px "  Enabled="false" ClientIDMode="Static"></asp:TextBox>
        </div>

         <div class="form-group">
            <asp:Label ID="Label5" runat="server"  CssClass="col-sm-2 control-label">模組序號：</asp:Label>
            <asp:TextBox ID="TextBox2" runat="server" CssClass="col-sm-4  padding-top padding-left-10px "  Enabled="false" ClientIDMode="Static"></asp:TextBox>
        </div>
     

        </div>
     <asp:ScriptManager ID="myScriptManager" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PluginScriptsPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="InlineScriptsPlaceHolder" Runat="Server">
    <script>
        $('#TextBox1').attr("name", "TextBox1");
         $("#form1").validate({
                errorClass: "validator-error",
                rules: {
                    
                    TextBox1: {
                        required: true
                    },
                    
                   
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
            
            
       function DoAdd(){
           

             if ($('#form1').valid()) {
                DoSave();
            } else {
                parent.MasterCtrl.showErrMsg('<%=ParseWording("A0029")%>');
            }
       }

         /*Toolbar放棄;*/
        function DoCancel() {
            $.confirm({
                title: '<%=ParseWording("A0013")%>',
                content: false,
                //content: 'Simple confirm!',
                confirm: function () {
                    parent.location.replace("SubFunctionMenuSetting_Q.aspx?a=a" + unescape("<%=MDS.Utility.NUtility.UrlEncode(strQueryCondi)%>"));
                },
                cancel: function () {
                    //alert('Canceled!')
                }
            });

        }

         /*Toolbar儲存, 使用PageMethods(AJEX)新增使用者;*/
        function DoSave() {
           
            
            PageMethods.AddNewsProcess(            
                $('#TextBox3').val() //ActionID
                , $('#TextBox1').val()//ActionDec
                , $('#TextBox4').val()//ButtonID
                , $('#DropDownList1').val()//FuctionID
                , $('#TextBox2').val()//SysModID
            
            , ProcessSuccess
            , ProcessError);
        }

        


        function getProKind() {
            $('#<%=TextBox0.ClientID%>').empty();
            $('#<%=TextBox2.ClientID%>').empty();
            // call AddProcess  no use  
            PageMethods.getProKind(
            $('#DropDownList1').val()
            , ProcessSuccess
            , ProcessError);

        }


        /*成功時彈出訊息;*/
        function ProcessSuccess(receiveData, userContext, methodName) {
            if (methodName == 'AddNewsProcess') {
                if (receiveData.nRet == 0) {
                    
                    parent.MasterCtrl.showSucessMsg('<%=ParseWording("A0011")%>');
                    parent.location.replace("SubFunctionMenuSetting_Q.aspx?a=a" + unescape("<%=MDS.Utility.NUtility.UrlEncode(strQueryCondi)%>"));
                } else {
                    parent.MasterCtrl.showErrMsg('<%=ParseWording("A0028")%>' + '\nnRet = ' + receiveData.nRet + '\noutMsg = ' + receiveData.outMsg);
                }

            }else if (methodName == 'getProKind') {
                    
                    var jsonDataArr = jQuery.parseJSON(receiveData.returnData);

                    for (var key in jsonDataArr) {

                        $('#<%=TextBox0.ClientID%>').val(jsonDataArr[key].CNAME);
                        $('#<%=TextBox2.ClientID%>').val(jsonDataArr[key].CKEY);

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

