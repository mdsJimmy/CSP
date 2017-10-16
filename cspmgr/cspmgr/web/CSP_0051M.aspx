<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainPage.master" AutoEventWireup="true" CodeFile="CSP_0051M.aspx.cs" Inherits="csp_web_CSP_0051M" %>
<%@ Register Src="~/DMSControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>
<%@ Register Src="~/DMSControl/oListView.ascx" TagName="oListView" TagPrefix="uc1" %>
<%@ Register Src="~/DMSControl/MultiselectDropDown.ascx" TagPrefix="uc1" TagName="MultiselectDropDown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PluginStylesPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">

   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitlePlaceHolder" Runat="Server">

  <uc1:ToolBar ID="btnSave" Text="<%$ Resources:DMSWording, A0005 %>" PostBack="false"
        Enabled="true" OnClientClick="DoEdt()" runat="server" />
    <uc1:ToolBar ID="btnCancel" Text="<%$ Resources:DMSWording, A0006 %>" PostBack="false"
        Enabled="true" OnClientClick="DoCancel();" runat="server" />

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="UserData-form" class="form-horizontal">
        <h4 class="header smaller lighter green">
            <i class="ace-icon fa fa-check-square-o bigger-110"></i><b>常用表單-修改 </b>
        </h4>
        <%=ParseWording("A0008")%>
        <div class="space-6"></div>
        <%--發佈對象--%>
       
        <%--僅供測試人員察看--%>
        <div class="form-group hidden">
            <label class="col-sm-2 control-label no-padding-top" >僅供測試人員察看：</label>
            <div class="col-sm-2  no-padding-top " id="checkboxGroup1">                
                <%--<input id="_chkTesterView" type="checkbox" name="_chkTesterView"  style="display:inline" value="1"/>--%>
                <asp:CheckBox runat="server" ID="_chkTesterView" Text="" Checked="false" ClientIDMode="Static"/>
            </div>
        </div>
         

        <div class="form-group" style=" position: absolute;left: -9999px;">
            <label class="col-sm-2 control-label">
                    <%=ParseWording("A0007")%>銷售階段 :</label>
                <div class="col-sm-5  no-padding-top">
                <asp:RadioButtonList ID="_radType"  runat="server"  RepeatDirection="Horizontal" ClientIDMode="Static" Width="50%"  >
                    
                    <asp:ListItem Text="銷售後" Value="B30" Selected="True" ></asp:ListItem>
                </asp:RadioButtonList>
            </div>
           
        </div>

        <div class="form-group" style=" position: absolute;left: -9999px;">
            <label class="col-sm-2 control-label">
                    <%=ParseWording("A0007")%>資料類型 :</label>
            <div class="col-sm-5  no-padding-top">
                <asp:DropDownList ID="_dlDataType"  runat="server" ClientIDMode="Static" Width="30%">
                     <asp:ListItem Text="表單下載" Value="B3030" Selected="True" ></asp:ListItem>

                </asp:DropDownList>
            </div>
        </div>

         <div class="form-group">
            <label class="col-sm-2 control-label">
                 <%=ParseWording("A0007")%>資料分類 :</label>
            <div class="col-sm-5  no-padding-top">
                <asp:DropDownList ID="_dlDataClass"  runat="server" Width="30%" ClientIDMode="Static" DataSourceID="SqlDataSource2" DataTextField="CNAME" DataValueField="CKEY" >
                   

                </asp:DropDownList>
                 <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM MIP_CODES WHERE CLEVEL='B3030' and CNOTE='0'  ORDER BY CORDER"></asp:SqlDataSource>
            </div>
            
        </div>
        
         <%--↓透過尋寶圖類型不顯示標題--%>
         <div id="area_title"  style="display:none">
            <div class="form-group">
                <label class="col-sm-2 control-label">
                    標題 :</label>
                <div class="col-sm-5  no-padding-top">
                    <asp:TextBox ID="_txtTitle" name= "_txtTitle" runat="server" Width="40%" ClientIDMode="Static"></asp:TextBox>
                    <br/><small style="color:red">*標題長度限制為30個字以內</small>
                </div>
            </div>
        </div>
         <%--↑透過尋寶圖類型不顯示標題--%> 
          <%--↓透過資料類型顯示連結--%>
       <div id="area"  style="display:none">
            <div class="form-group " >
                <label class="col-sm-2 control-label">
                    <%=ParseWording("A0007")%>url :</label>
                <div class="col-sm-5  no-padding-top">
                    <asp:TextBox ID="_txtUrl" runat="server" Width="100%" ClientIDMode="Static"></asp:TextBox>
                </div>
            </div>
       </div>
        <%--↑透過資料類型顯示連結--%>  

          <%--↓透過資料類型顯示上傳檔案 jpg--%>
        <div id="area2"  style="display:none">
        <div class="form-group">
            <label class="col-sm-2 control-label">檔案上傳 :</label>
           
            <div class="col-sm-7  no-padding-top">
                <asp:FileUpload ID="FileUpload1" name="FileUpload1" runat="server" ClientIDMode="Static" />              
                <asp:HyperLink ID="_hypLinkIdx" name="_hypLinkIdx" runat="server"></asp:HyperLink>  
                <button id="_btndm" name="_btndm"  type="button" class="btn btn-sm btn-danger btn-red btn-round"  onclick='dmdelete()'>
                        <i class="ace-icon fa fa-trash-o"></i>
                        刪除    
                </button>            
                         
            </div>
        </div>
      
        </div>
        <%--↑透過資料類型顯示上傳檔案--%>  


           <%--↓透過資料類型顯示上傳檔案 doc--%>
        <div id="area3"  style="display:none">
        <div class="form-group">
            <label class="col-sm-2 control-label">檔案上傳 :</label>
           
            <div class="col-sm-7  no-padding-top">
                <asp:FileUpload ID="FileUpload2" name="FileUpload2" runat="server" ClientIDMode="Static" /> 
               <small style="color:red">*建議使用PDF檔案</small>                  
                <asp:HyperLink ID="HyperLink1" name="_hypLinkIdx2" runat="server"></asp:HyperLink>  
                 
                <button id="_btndm1" name="_btndm"  type="button" class="btn btn-sm btn-danger btn-red btn-round"  onclick='dmdelete()'>
                        <i class="ace-icon fa fa-trash-o"></i>
                        刪除    
                </button>           
                    
            </div>
            
        </div>
      
        </div>
        <%--↑透過資料類型顯示上傳檔案--%>  


         <div class="form-group">
            <label class="col-sm-2 control-label">
                排序 :</label>
            <div class="col-sm-5  no-padding-top">
                <asp:TextBox ID="_txtOrder" runat="server" ClientIDMode="Static"  ></asp:TextBox>
            </div>
        </div>

        <div class="form-group">
            <label class="col-sm-2 control-label">
                 <%=ParseWording("A0007")%>狀態:</label>
            <div class="col-sm-5  no-padding-top">
                <asp:RadioButtonList ID="_radStatus" runat="server" RepeatDirection="Horizontal" Width="200" ClientIDMode="Static">
                    <asp:ListItem Text="啟用" Value="0"></asp:ListItem>
                    <asp:ListItem Text="停用" Value="1"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>

         <div class="form-group ">
                <label class="col-sm-2 control-label no-padding-top" ><%=ParseWording("A0007")%>角色：</label>
                <div class="col-sm-2  no-padding-top" id="checkboxGroup">
                
                        <input  id="isIc" type="checkbox" name="roler"  style="display:inline" value="IC"  />IC <%--單位--%>
                        <input  id="isRm" type="checkbox" name="roler"  style="display:inline;margin-left:10%" value="RM"/>RM <%--角色--%>
               
                </div>
       
            </div>
        
      
         <div class="form-group">
            <label class="col-sm-2 control-label no-padding-top" >授權單位：</label>
            <div class="col-sm-6  no-padding-top">
                <input  id="chkAllList" type="checkbox" onclick="chkAll_()" name="chkAllList" checked="checked" />所有單位
            </div>
        </div>

         <div id="isChkall" style="display:none">
            <div class="form-group">
            <label class="col-sm-2 control-label" ></label> 
            <div class="col-sm-3 no-padding-top">
               <table id="tb_ListView" class="table table-striped table-bordered table-hover no-wrap">
                   <tbody>
                       <thead>
                            <tr>
                                <th></th>
                                <th align="center" ><a href="">群組名稱</a></th>
                            </tr>
                        </thead>
                        <asp:Repeater ID="Repeater" runat="server" DataSourceID="SqlDataSource1"  >
                            <ItemTemplate >
                                <tr id="_tr_0" title="">
                                    <td align="center" style="width:5%"><input type="checkbox" value='<%# MDS.Utility.NUtility.trimBad((string)(DataBinder.Eval(Container.DataItem, "PCAGROUP_id")))%>' class="chkG" name="chkG" onclick="chkClick()"/></td>
                                    <td align="center"><span style="white-space: normal;"> <%# MDS.Utility.NUtility.trimBad((string)(DataBinder.Eval(Container.DataItem, "PCAGROUP_NAME")))%></span></span></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                   </tbody>
                </table>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [PCAGROUP_ID], [PCAGROUP_NAME] FROM [MIP_PCAGROUP]  where LOGINLIST!=0  order by [corder]"></asp:SqlDataSource>
            </div>    
            <div class="col-sm-5 no-padding-top">
                <uc1:oListView runat="server" ID="oListView" />
            </div>
        </div>
    </div>
</div>
   
    
    <asp:HiddenField ID="_hidMethod"   runat="server" ClientIDMode="Static"/>
    <input type="hidden" id="sHidDmIdx" name="sHidDmIdx" runat="server" value="" /><%--//檔案索引--%>
    
    <input type="hidden" id="_hid_chk" name="_hid_chk" /><%--單位名單--%>
    <input type="hidden" id="_hidChkALL" name="_hidChkALL" /><%--全選--%>
    <input type="hidden" id="_isRcOrRm" name="_isRcOrRm" />

    <%--新增 END--%>

 
<asp:ScriptManager ID="myScriptManager" runat="server" EnablePageMethods="True"></asp:ScriptManager>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PluginScriptsPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="InlineScriptsPlaceHolder" Runat="Server">
    
   
    

    
     
    

    
    
   
<script type="text/javascript">
   

    //角色
    var isIc = "";
    var isRm = "";


    //群組資料
    var depObject = $('input[id$="_chk"]');
    var grupObject = $("input[name='chkG']");
    ////////////當按下單位 checkbox 時，群組的 checkbox 會被取消//////////////
    depObject.click(cc);
    var grup = null;
    var dep = null;

    function cc() {
        //組jason 資料 key value 的關係
        var arr = {
            data: {
                key: [],
                value: []
            }
        };

        grupObject.each(function () {//群組名單迴圈
            grup = $(this);//群組物件
            depObject.each(function () {
                dep = $(this);
                if (grup.val() == dep.val().split("##")[0]) {

                    arr.data.key.push(dep.val().split("##")[0]);
                    arr.data.value.push(dep.prop('checked'));
                }//end if
            });// 單位 迴圈 end

            var flag = arr.data.value.every(function (value, index, array) {//若value內的值 都為 true 群組才會被勾選
                return value == true;
            });
            grup.prop('checked', flag);
            arr.data.key.length = 0;//清除 data 內 key 值
            arr.data.value.length = 0;//清除 data 內 value 值
        });// 群組 迴圈 end

        checkcheck();//群組名單 全勾時 全選的 checkbox 會被勾起來
    }
    function checkcheck() {
        var isAllCheck = {
            //組陣列值，若全為true 時，要勾選 全部單位
            value: []
        }
        grupObject.each(function () {//群組名單迴圈
            isAllCheck.value.push($(this).prop('checked'));
           

        })
        var flags = isAllCheck.value.every(function (value, index, array) {//若value內的值 都為 true 群組才會被勾選
            return value == true;
        });
        
        $('#chkAllList').prop('checked', flags);
        
    }

    /////////////////////////////
    function chkClick() {
        // chkG 迴圈檢查群組 chkbox 裡打勾的值，並取值outVar
        // 在 chkG 內迴圈檢查 _chk 裡打勾的值, 並取值
        // 在判斷是否要打勾，否則不打勾
        $("input[name='chkG']").each(function () {

            if ($(this).prop("checked")) {
                var outVar = $(this).val();//取得群族的值

                $('input[id$="_chk"]').each(function () {
                    if (outVar == $(this).val().split("##")[0]) {//群組的值==單位的值
                        $(this).prop('checked', true);//單位的 checkbox 會被打勾

                    }
                });

            } else {
                var outVar = $(this).val();//取得群族的值
                $('input[id$="_chk"]').each(function () {
                    if (outVar == $(this).val().split("##")[0]) {//群組的值==單位的值
                        $(this).prop('checked', false);//單位的 checkbox 會被取消
                    }
                });
            }
            checkcheck();
        });
    }
    

    function chkAll_() {
        if ($('#chkAllList').prop('checked')) {
            
            $('#isChkall').css("display", "none");//消失
            //清空
            $('input[id$="_chk"]').each(function () {
                $(this).prop('checked', false);

            });
            //清空
            $("input[name='chkG']").each(function () {
                $(this).prop('checked', false);
            });

        } else {
           
            $('#isChkall').css("display", "inline");//顯示
            //清空
            $('input[id$="_chk"]').each(function () {
                $(this).prop('checked', false);

            });
            //清空
            $("input[name='chkG']").each(function () {
                $(this).prop('checked', false);
            });
        }


    }
   
    var str_radType = ""
    var str_dlDataType = "";
    var str_dlDataClass = "";
    var str_txtTitle=""; 
    var bl_isFile = "";
    var str_txtUrl = ""
    var str_txOrder = "";
    var str_radStatus = "";

    
    
    jQuery(function ($) {
        $('td').css({ "padding-top": "5px", "height": "0px" });
        $("td :nth-child(2)").css({ "padding-bottom": "5px" });
        $('#_radType_0').attr('name', '_radType');
        $('#_radType_1').attr('name', '_radType');
        $('#_radType_2').attr('name', '_radType');

        $('#_txtTitle').attr('name', '_txtTitle');
        $('#_txtUrl').attr('name', '_txtUrl');
        $('#_txtOrder').attr('name', '_txtOrder');

        $('#_dlDataClass').attr('name', '_dlDataClass');
        $('#_dlDataType').attr('name', '_dlDataType');
        $('#_radStatus_0').attr('name', '_radStatus');
        $('#_radStatus_1').attr('name', '_radStatus');

     
       





        $('#_chkTesterView').val('<%:MDS.Utility.NUtility.checkString(strIstester)%>');
        

        //
        //console.log("strIstester = " + '<%:MDS.Utility.NUtility.checkString(strIstester)%>');
        //console.log($('#_chkTesterView').val());
        //

        if ('<%:MDS.Utility.NUtility.checkString(strIstester)%>' == "0") {

            $("#_chkTesterView").prop('checked', true);
            //console.log("啟用 勾選值 = " + $('#_chkTesterView').val());

        } else if ('<%:MDS.Utility.NUtility.checkString(strIstester)%>' == "1") {

            $("#_chkTesterView").prop('checked', false);
            //console.log( "不啟用 勾選值 = " + $('#_chkTesterView').val());

        } 




       
      //取出資料 並填入空格
        '<%:strSelectAll%>' == "0" ? $('#chkAllList').prop("checked", true) : $('#chkAllList').prop("checked", false);
        chkAll_();
       
        //根據db的資料 判斷ic rm
        if('<%:strICRM.Split(',').Length%>'=='2'){
            '<%:strICRM.Split(',')[0]%>'=='IC'?  $('#checkboxGroup #isIc').prop('checked',true):  $('#checkboxGroup #isIc [value=IC]').prop('checked',false);
                <% string RM = strICRM.Split(',').Length==2 && strICRM.Split(',')[1] != null ? strICRM.Split(',')[1] : null;%>
            '<%:RM%>'=='RM'? $(" #checkboxGroup #isRm ").prop('checked',true): $(" #checkboxGroup #isRm ").prop('checked',false);
        }else{
            '<%:strICRM.Split(',')[0]%>'=='IC'?  $('#checkboxGroup #isIc').prop('checked',true):  $('#checkboxGroup #isRm').prop('checked',true);
        }




         //取出名單資料 全選不撈值
            if( '<%:strSelectAll%>'!='0'){
                var arrery = '<%:strDept%>'.split(",");
            
                $.each(arrery,function(idx,value){
                    var str = value;
                
                
                    $('input[id$="_chk"]').each(function () {
                    
                        if($(this).val().split("##")[1]==str) //key##value 取value   
                            $(this).prop('checked',true);
                    });
                })
                var depObject = $('input[id$="_chk"]');
                var grupObject = $("input[name='chkG']");
            
                //////////////////////////撈出來的資料 並勾選
                grupObject.each(function () {
                    var grup = $(this);
                    depObject.each(function () {
                        var dep = $(this);
                        if (grup.val() == dep.val().split("##")[0]) {
                            //alert(grup.val() + ".........." + dep.val().split("##")[0]);
                            grup.prop('checked', dep.prop('checked'))
                        } 
                    })
                })
            
               
            
            
        }

        if( $('#<%:sHidDmIdx.ClientID%>').val() == ''||$('#btnSave').length==0){
            $("#_btndm").hide();
            $("#_btndm1").hide();
        }

    var $form = $('#form1');
            var file_input = $form.find('input[id=FileUpload1]');
            var upload_in_progress = false;
          
                    file_input.ace_file_input({
                    style: 'well',
                    btn_choose: '請選擇檔案',
                    btn_change: null,
                    thumbnail: 'large',

                    maxSize: 10485760, //bytes
                    allowExt: ["jpg", "jpeg", "png", "gif"],
                    allowMime: ["image/jpg", "image/jpeg", "image/png", "image/gif"],
                    //pdf, doc, docx, xls, xlsx, ppt, pptx
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
                    if (info.error_count['ext'] || info.error_count['mime']) parent.MasterCtrl.showErrMsg('副檔名有誤（只能上傳： "jpg", "jpeg", "png", "gif")');
                    if (info.error_count['size']) parent.MasterCtrl.showErrMsg('檔案過大! 超過 10M');

                    //you can reset previous selection on error
                    ev.preventDefault();
                    file_input.ace_file_input('reset_input');
                });

                if ('<%: MDS.Utility.NUtility.checkString(uploadOK) %>' != '') {
                    callSuccess();
                }


        var $form = $('#form1');
        var file_input = $form.find('input[id=FileUpload2]');
        var upload_in_progress = false;
            

        
            file_input.ace_file_input(
                {
                style: 'well',
                btn_choose: '請選擇檔案',
                btn_change: null,
                thumbnail: 'large',

                maxSize: 10485760, //bytes
                allowExt: [ "pdf","PDF"
                            ,"doc"
                            , "docx"
                            , "xls"
                            , "xlsx"
                            , "ppt"
                            , "pptx"],
                allowMime: [ "application/pdf"
                            , "application/msword"
                            , "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                            , "application/vnd.ms-excel"
                            , "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                            , "application/vnd.ms-powerpoint"
                            , "application/vnd.openxmlformats-officedocument.presentationml.presentation"
                            ],
                //pdf, doc, docx, xls, xlsx, ppt, pptx
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
                if (info.error_count['ext'] || info.error_count['mime']) parent.MasterCtrl.showErrMsg('副檔名有誤（只能上傳： "pdf","PDF","doc", "docx", "xls", "xlsx", "ppt", "pptx")');
                if (info.error_count['size']) parent.MasterCtrl.showErrMsg('檔案過大! 超過 10M');

                //you can reset previous selection on error
                ev.preventDefault();
                file_input.ace_file_input('reset_input');
            });

            if ('<%: MDS.Utility.NUtility.checkString(uploadOK) %>' != '') {
                callSuccess();
            }
            

       
        $("#form1").validate({
            errorClass: "validator-error",
            success: "valid",
            rules: {
                _radType: {
                    required: true,

                },
                _dlDataClass: {
                    required: true,
                },
                _dlDataType: {
                    required: true,
                },
                _radStatus: {
                    required: true,
                },
                _txtOrder: {
                    required: true,
                    number: true,
                    range: [0, 99],
                },
                roler: {
                    required: true
                },
                _txtTitle: {
                    required: true,
                    rangelength: [0,30]
                },
            },
            messages: {

                _radType: {
                    required: '請選擇',
                },
                _dlDataClass: {
                    required: '請選擇',
                },
                _dlDataType: {
                    required: '請選擇',
                },
                _radStatus: {
                    required: '請選擇',
                },
                _txtOrder: {
                    required: '請輸入數字',
                    range:'只能輸入 0~99 ',
                },
                roler: {
                    required: "請勾選"
                },
                _txtTitle: {
                    required: '未輸入標題' ,
                    rangelength:'超過30字數'
                },
            },
            errorPlacement: function (error, element) {

                var divId = element.attr('name') + "-validator";

                if (element.attr("name") == "_radType") {
                    error.insertAfter("#_radType");
                } else if (element.attr("name") == "_radStatus") {
                    error.insertAfter("#_radStatus");
                
                } else {

                    if ($('#' + divId).length) {

                        $('#' + divId).html(error);
                    }
                    else {
                        $(element).parent().append($("<div id='" + divId + "' class='validator-error'></div>").append(error));
                    }
                }
            },
            highlight: function (e) {
                var _radType_0 = document.getElementById("_radType_0");
                var _radStatus = document.getElementById("_radStatus_0");
               

                if (e == _radType_0) {
                    $('#_radType').addClass("validator-error-background");
                } else if (e == _radStatus) {
                    $('#_radStatus').addClass("validator-error-background");
                }
                $(e).addClass("validator-error-background");


                //$('#_radStatus').addClass("validator-error-background");
            },
            unhighlight: function (e) {
                var _radType_0 = document.getElementById("_radType_0");
                var _radStatus = document.getElementById("_radStatus_0");
             
                if (e == _radType_0) {
                    $('#_radType').removeClass("validator-error-background");
                } else if (e == _radStatus) {
                    $('#_radStatus').removeClass("validator-error-background");
                } 
                $(e).removeClass("validator-error-background");


            }
        });
        
    })

    
    function callSuccess() {
        MasterCtrl.showSucessMsg('新增資料成功');
        parent.location.replace("CSP_0051Q.aspx?a=a" + unescape(decodeURI("<%=MDS.Utility.NUtility.UrlEncode(strQueryCondi)%>")));
    }

    function getProKind() {
                $('#<%=_dlDataType.ClientID%>').empty();
                $('#<%=_dlDataClass.ClientID%>').empty();
                 var value = $('#<%:_radType.ClientID%> input[type=radio]:checked').val();
                    // call AddProcess  no use  
                    PageMethods.getProKind(
                    value
                    , ProcessSuccess
                    , ProcessError);

            }

   
        
        var x = document.getElementById("<%:_dlDataType.ClientID%>").selectedIndex;
        //開啟上傳檔案或連結
        switch (document.getElementsByTagName("option")[x].value) {

            

            case 'B3030'://表單下載
                document.getElementById("area").style.display = "none";
                document.getElementById("area2").style.display = "none"
                document.getElementById("area3").style.display = "none"
                document.getElementById("area3").style.display = "inline";
                document.getElementById("area_title").style.display = "inline";
                
                break;

            default:
                break;
        }


      
        

  
    

        
   
    
        
        function DoEdt() {
            var chk = GetCheckBoxValue();
            if (!$('#chkAllList').prop("checked")) {
                $('#_hid_chk').val(chk);
                if (GetCheckBoxValue() == null || GetCheckBoxValue() == "") { alert("請選擇授權單位"); return };
            } else {
                $("#_hid_chk").val("");
                $('#_hidChkALL').val("0");
            }
            if ($('#<%:FileUpload1.ClientID%>').val() == '') $('#<%:FileUpload1.ClientID%>').val("");

            //角色 透過 提交設定值
            
        
                var arr = [];
                if ($('#checkboxGroup #isIc').prop('checked')) {
                    arr.push('IC');
                    $('#checkboxGroup #isIc').prop('checked', true)

                }
                if ( $('#checkboxGroup #isRm').prop('checked')) {
                    arr.push('RM');
                    $('#checkboxGroup #isRm').prop('checked', true)

                }
                $('#_isRcOrRm').val(arr.join(','));
             

            //第一欄：僅供測試人員察看        

                if ($('#_chkTesterView').prop("checked")) {

                    $('#_chkTesterView').val("0");
                    //console.log($('#_chkTesterView').val());

                } else if ($('#_chkTesterView').prop("checked", false)) {

                    $('#_chkTesterView').val("1");
                    //console.log($('#_chkTesterView').val());

                }

            //var str_chkTesterView = $('#_chkTesterView').val();
            //console.log(str_chkTesterView);
             
          

            if ($('#form1').valid()) {
                $('#_hidMethod').val("MODIFY");
               
                var $form = $('#form1');
                $('#form1').submit();
            }

        }
    

  

   

    function DoCancel() {
        $.confirm({
            title: '<%=ParseWording("A0013")%>',
            content: false,
            //content: 'Simple confirm!',
            confirm: function () {
                parent.location.replace("CSP_0051Q.aspx?a=a" + unescape(decodeURI("<%=MDS.Utility.NUtility.UrlEncode(strQueryCondi)%>")));
            },
            cancel: function () {
                //alert('Canceled!')
            }
        });

    }

    function dmdelete() {
             
             $.confirm({
                title: '刪除檔案',
                content: true,
                content: '刪除後檔案無法回復!',
                confirm: function () {
                    
                    delFile(   $('#<%:sHidDmIdx.ClientID%>').val(),'<%:strDmIdx%>');
                },
                cancel: function () {
                    //alert('Canceled!')
                }
            });
           /// 
        }

     function delFile( fileid,filetype){
                 
                 PageMethods.DelFileProcess( '<%:proIdPK%>',fileid, ProcessSuccess , ProcessError);
                
        }

    var frontData = $('#_dlDataClass').find(":selected").val();;

/*成功時彈出訊息;*/
    function ProcessSuccess(receiveData, userContext, methodName) {
        
            switch (methodName) {
                case 'getProKind':
                    if (receiveData.nRet == 0) {
                        var jsonDataArr = jQuery.parseJSON(receiveData.returnData);
                        $('#<%=_dlDataType.ClientID%>').empty();
                         $('#<%=_dlDataClass.ClientID%>').empty();
                        $('#<%=_dlDataClass.ClientID%>').append($("<option></option>").attr("value", "").text("請選擇"));
                        $('#<%=_dlDataType.ClientID%>').append($("<option></option>").attr("value", "").text("請選擇"));
                        for (var key in jsonDataArr) {
                            $('#<%=_dlDataType.ClientID%>').append($("<option></option>").attr("value", jsonDataArr[key].CKEY).text(jsonDataArr[key].CNAME));
                        };
                    }else{
                        parent.MasterCtrl.showErrMsg('<%=ParseWording("A0028")%>' + '\nnRet = ' + receiveData.nRet + '\noutMsg = ' + receiveData.outMsg);
                    }
                    break;
               

                case 'DelFileProcess':
                    
                    if (receiveData.nRet == 0) {
                       
                            MasterCtrl.showSucessMsg('檔案刪除完成');
                            location.reload();
                    }
                    else {
                        MasterCtrl.showErrMsg('<%=ParseWording("A0028")%>' + '\nnRet = ' + receiveData.nRet + '\noutMsg = ' + receiveData.outMsg);
                    }
            
                    
                    break;
            
                default:
                    
                    break;
            
        }
    }

    /*失敗時彈出失敗訊息;*/
    function ProcessError(error, userContext, methodName) {
        if (error != null)
            parent.MasterCtrl.showErrMsg(error.get_message());
    }



</script>
</asp:Content>

