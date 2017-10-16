<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MainPage.master"  CodeFile="SqlliteUpload_List.aspx.cs" Inherits="SysFun_SqliteUpload_SqliteUpload_List" %>

<%@ Register Src="~/DMSControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>
<%@ Register Src="~/DMSControl/oListView.ascx" TagName="oListView" TagPrefix="uc1" %>



<asp:Content ID="Content4" ContentPlaceHolderID="PluginStylesPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitlePlaceHolder" Runat="Server">
                   <button id="Button1" type="button" class="btn btn-round btn-primary btn-white" data-toggle="modal"
        data-target="#EditModal" onclick="DoUpload();"> <i class="ace-icon fa fa-cloud-upload"></i>檔案上傳</button>
                    <uc1:ToolBar ID="btnAdd"  Text="<%$ Resources:DMSWording, A0001 %>"    PostBack="false" Enabled="true" OnClientClick="DoUpload()" Visible="false" runat="server" />
                
                    <uc1:ToolBar ID="btnDel"  Text="<%$ Resources:DMSWording, A0003 %>" PostBack="false" Enabled="true" OnClientClick="DoDel()" runat="server" />
               
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div id="EditModal" class="modal fade" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true"  >
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                 <button type="button" id="modalclose" name="modalclose" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h3 class="modal-title" id="myModalLabel">檔案上傳</h3>
                </div>
                <div class="modal-body">
                     
                </div>
                <div class="modal-footer">
                     
                </div>
            </div>
        </div>
    </div>
   <div class="row">
          <div class="col-xs-5">
                   請輸入查詢關鍵字&nbsp;
                       <input type="text" name='StrSearch' id="StrSearch"   value="<%:mySearch %>"   /> 
                       <uc1:ToolBar ID="btnSearch"  Text="<%$ Resources:DMSWording, A0004 %>" PostBack="false" Enabled="true" OnClientClick="DoSearch();" runat="server" />
                    </div>
       
       </div>
         <div class="space-4"></div>
        
        <%--ListView Start--%>
        <uc1:oListView ID="SqlliteList" runat="server" />
        <%--ListView Start--%>

        <asp:ScriptManager ID="myScriptManager" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
              

 
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PluginScriptsPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="InlineScriptsPlaceHolder" Runat="Server">

<script type="text/javascript">
 


    /*搜尋按鈕按下;*/
    function DoSearch() {
        parent.location.replace("SqlliteUpload_List.aspx?PageNo=<%:PageNo%>&StrSearch=" + $('#StrSearch').val());
    }


    /*清單按下，下載該筆檔案;*/
    function DoEdt(chkPK) {
        if (chkPK != '') {
            var ArrayValue = chkPK.split("##");
            var version = ArrayValue[0];
            var filename = ArrayValue[1];
            var oldfilename = ArrayValue[2];
            $.confirm({
                title: '下載檔案' + filename,
                content: false,
                //content: 'Simple confirm!',
                confirm: function () {
                    try {
                        parent.location.replace("../SqlliteUpload/getNewVersion.aspx?version=" + version + "&filename=" + filename + "&oldfilename=" + oldfilename);
                    } catch (err) {
                        parent.location.href = "../SqlliteUpload/getNewVersion.aspx?version=" + version + "&filename=" + filename + "&oldfilename=" + oldfilename;
                    }
                },
                cancel: function () {
                    //alert('Canceled!')
                }
            });
        }
       


        //alert('chkPK>'+chkPK);
      
        
       
        
    }

    /*Toolbar新增;*/
    function DoUpload() {
      
        var url = "singleFileUpload.aspx?UploadPath=UserUpLoad/Sqlite&FileType=*.sqlite^^*.zip&IsReturn=1&IsNewFileName=1"
        $(".modal-body").html('<iframe width="500px" height="500px" frameborder="0" scrolling="yes" allowtransparency="true" src="' + url + '"></iframe>');

 
    }


    /*Add資料庫動作*/
    function DoAdd(FileName, OldFileName, sqltype) {
 
        PageMethods.Add_Sqllite(FileName, OldFileName, sqltype, ProcessSuccess, ProcessError);
    }


    /*Toolbar刪除;*/
    function DoDel() {
        var versionList = GetCheckBoxValue();
        if (versionList == '') {
            MasterCtrl.showErrMsg('<%=ParseWording("A0009")%>');
        }
        else {
 
            $.confirm({
                title: '<%=ParseWording("A0010")%>',
                content: false,
                //content: 'Simple confirm!',
                confirm: function () {
                    PageMethods.Delete_Sqllite(versionList, ProcessSuccess, ProcessError);
                },
                cancel: function () {
                    //alert('Canceled!')
                }
            });
        }
    }



    /*成功時彈出訊息;*/
    function ProcessSuccess(receiveData, userContext, methodName) {
        
        if ((methodName == 'Add_Sqllite') 
            || (methodName == 'Delete_Sqllite') ) {

            if (receiveData.nRet == 0) {
                MasterCtrl.showSucessMsg('<%=ParseWording("A0011")%>');
                parent.location.replace("SqlliteUpload_List.aspx?PageNo=<%:PageNo%>&StrSearch=<%:mySearch%>");
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