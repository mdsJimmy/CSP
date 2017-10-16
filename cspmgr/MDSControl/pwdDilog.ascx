<%@ Control Language="C#" AutoEventWireup="true" CodeFile="pwdDilog.ascx.cs" Inherits="MDSControl_pwdDilog" %>
<link rel="stylesheet" type="text/css" href="<%=ResolveUrl("~/Content/themes/default/easyui.css")%>">
<link rel="stylesheet" type="text/css" href="<%=ResolveUrl("~/Content/themes/icon.css")%>">
<script type="text/javascript" src="<%=ResolveUrl("~/Plugins/jquery.min.js")%>"></script>
<script type="text/javascript" src="<%=ResolveUrl("~/Scripts/jquery.easyui-1.4.5.min.js")%>"></script>
<script>
    

</script>

   <div id="login-dialog" class="easyui-dialog" title="密碼重置" style="width:400px;height:220px;padding:10px 20px" buttons="#login-buttons">
    <div class="form-title">密碼重置</div>
    <form id="login_form" >
      <div class="form-item">
        <label>重置密碼:</label>
        <input id="pwd" name="pwd" type="password" class="easyui-textbox" required="true" data-options="iconCls:'icon-lock',missingMessage:'此欄位為必填'"  style="width:200px">
      </div>
      <div class="form-item">
        <label>重新輸入:</label>
        <input id="repwd" name="repwd" type="password" class="easyui-textbox" required="true" data-options="iconCls:'icon-lock',missingMessage:'此欄位為必填'" style="width:200px">
      </div>
    </form>
  </div>
  <div id="login-buttons">
    <a href="javascript:void(0)" class="easyui-linkbutton" iconCls="icon-ok" onclick="javascript:subbmit();" style="width:90px">確認</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" iconCls="icon-cancel" onclick="javascript:$('#login-dialog').dialog('close')" style="width:90px">取消</a>
  </div>
   

<script>
    $('#login-dialog').dialog({
        //title: 'My Dialog',
        width: 400,
        height: 200,
        closed: true,
        cache: false,
        //href: 'get_content.php',
        modal: true
    });
    
   
    function subbmit() {
      
        if ($("#repwd").val() == '' && $("#pwd").val() == '') {
            alert('未輸入密碼，請重新輸入');
            openDilog();
            return ;

        } else if ($("#repwd").val() != $("#pwd").val()) {
            alert('密碼不一致，請重新輸入');
            openDilog();
            return;
          
        }else {
            DoPwReset();
            
        }
    }
    
  
   
        function openDilog() {
            var AccountIDList = GetCheckBoxValue();
            if (AccountIDList == '') {
                parent.MasterCtrl.showErrMsg('<%=ParseWording("A0009")%>');
            } else {
                $('#login-dialog').dialog('open');
            }
           
           
           
        }

        function closeDilog() {

            $('#login-dialog').dialog('close');


        }

       
       
      
        

        

           

       
</script>