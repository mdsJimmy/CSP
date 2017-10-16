<%@ Page Language="C#" AutoEventWireup="true"   MasterPageFile="~/MasterPage/ContentPage.master"  CodeFile="singleFileUpload.aspx.cs" Inherits="DMSControl_singleFileUpload" %>
<%@ Register Src="~/DMSControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="PluginStylesPlaceHolder" runat="Server">
 
  </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
 
     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitlePlaceHolder" runat="Server">
 <uc1:ToolBar ID="btnSave"   Text="<%$ Resources:DMSWording, A0005 %>" PostBack="true" OnClientClick="DoCheckSave()" Enabled="true"   runat="server" />

 <uc1:ToolBar ID="btnCancel"   Text="<%$ Resources:DMSWording, A0006 %>" PostBack="false" Enabled="true" OnClientClick="DoCancel()" runat="server" />
       
        
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
                <div class="form-group" visibility:hidden>
                    <label class="control-label col-sm-2 title"   >
                     <%=ParseWording("A0007")%>說明:</label>&nbsp;&nbsp;
                      <div class="col-sm-3">
                   <asp:TextBox ID="input_version_no" runat="server"   MaxLength="50" class="textbox"></asp:TextBox>
                    </div>  
                 </div>

                  <div class="form-group">
                      <label class="control-label col-sm-2 title"   > <%=ParseWording("A0007")%>檔案類型:</label>&nbsp;&nbsp;
                      <div class="col-sm-3">
                         <select id="sqltype" runat="server"  ></select>
                    </div>  
                    </div>
                        <div class="form-group">
                    <label class="control-label col-sm-2 title"   > <%=ParseWording("A0007")%><%=ParseWording("A0040")%>:</label>&nbsp;&nbsp;
                      <div class="col-sm-3">
                      <asp:FileUpload ID="FileUpload1" runat="server"   />
            
                    </div>   </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PluginScriptsPlaceHolder" runat="Server">
  
        
        
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="InlineScriptsPlaceHolder" runat="Server">
 
          <script src="<%=ResolveUrl("~/Plugins/ace-assets/js/ace-elements.js")%>"></script>
        <script src="<%=ResolveUrl("~/Plugins/ace-assets/js/ace.js")%>"></script>
<script type="text/javascript">
    jQuery(function ($) {
        var $form = $('#form');
        //you can have multiple files, or a file input with "multiple" attribute
        var file_input = $form.find('input[type=file]');
        var upload_in_progress = false;

        file_input.ace_file_input({
            style: 'well',
            btn_choose: '請選擇檔案',
            btn_change: null,
            droppable: false,
            thumbnail: 'large',

            maxSize: 1100000, //bytes
            allowExt: ["sqlite",  "pdf"],
            allowMime: ["application/x-sqlite3", "application/pdf"],

            before_remove: function () {
                if (upload_in_progress)
                    return false; //if we are in the middle of uploading a file, don't allow resetting file input
                return true;
            },

            preview_error: function (filename, code) {
                //code = 1 means file load error
                //code = 2 image load error (possibly file is not an image)
                //code = 3 preview failed
            }
        })
        file_input.on('file.error.ace', function (ev, info) {

            if (info.error_count['ext'] || info.error_count['mime'])
            //if (info.error_count['ext'])
                MasterCtrl.showErrMsg('檔案類別有誤,只允許:sqlite", "pdf ');

            if (info.error_count['size'])
                MasterCtrl.showErrMsg('檔案過大! 超過 1M');  

            //you can reset previous selection on error
            //ev.preventDefault();
            //file_input.ace_file_input('reset_input');
        });

        if ('<%= uploadOK %>' != '') {
            callSuccess();
        } 
      

    });
 
    var pWindow = parent;

    function DoCheckSave() {
        var $form = $('#form');
        var _select = $form.find('input[type=select]');
        var _text = $form.find('input[type=text]');
        var _file = $form.find('input[type=file]');
        var files = _file.data('ace_input_files');
      //  alert(files.length);
        if (_text.val() == '' || files.length == 0 ) {
            MasterCtrl.showErrMsg('檔案未上傳或版號未填寫');
           
            return false;
        } else {
            MasterCtrl.showSucessMsg('檔案上傳成功');
            return true;
        }
         //return $('#form').valid();
    }


    function callSuccess() {
        MasterCtrl.showSucessMsg('檔案上傳成功');
        DoCancel();
        parent.location.replace("SqlliteUpload_List.aspx");
    }

    function DoCancel() {
        pWindow.$("#modalclose").click();
    }
    
</script>
</asp:Content>