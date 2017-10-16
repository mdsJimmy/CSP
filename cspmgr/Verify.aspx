<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Verify.aspx.cs" Inherits="Verify" %>
<!DOCTYPE html>
<html lang="en">

<head  runat="server" id="Head1">
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
<meta charset="utf-8" /><meta name="description" content="User login page" />
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <!-- bootstrap & fontawesome -->
    <link rel="stylesheet" href="./Plugins/ace-assets/css/bootstrap.css" />
    <link rel="stylesheet" href="./Plugins/ace-assets/css/font-awesome.css" />
     <!-- text fonts -->
    <link rel="stylesheet" href="./Plugins/ace-assets/css/ace-fonts.css" />

    <!-- ace styles -->
    <link rel="stylesheet" href="./Plugins/ace-assets/css/ace.css" class="ace-main-stylesheet"   />
    <!-- mds styles -->
    <link rel="stylesheet" href="./Plugins/mds/css/mds.css" />
    <!--[if lte IE 9]>
	    <link rel="stylesheet" href="./Plugins/ace-assets/css/ace-part2.css" class="ace-main-stylesheet" />
    <![endif]-->

    <!--[if lte IE 9]>
	    <link rel="stylesheet" href="./Plugins/ace-assets/css/ace-ie.css" />
    <![endif]-->
        <!-- ace settings handler -->
   
    <script src="./Plugins/ace-assets/js/ace-extra.js"></script>
    <link href="./Plugins/toastr/toastr.css" rel="stylesheet"/>
    <style>
.systemTitle{font-family: '微軟正黑體', 'Microsoft JhengHei', sans-serif;}
</style>
    <title>
	元大通路服務平台
</title>
 
    <link rel="stylesheet" type="text/css" href="mds.css" />
    <script type="text/javascript" language="javascript">
       
    // <!CDATA[
        function checkKey(e) {
            var key = window.event ? e.keyCode : e.which;
            //alert(key);
            //var keychar = String.fromCharCode(key);
            //alert(keychar);
            //reg = /\n/;
            //alert(reg.test(keychar));

			try{
			    if (key == 13) {
					check();
				}
            } catch (err) {
                alert('System Error,Code=' + (err.number & 0xFFFF));
			    alert(err.message);
			}
		}

    	function CheckResolution() {
			if(screen.height < 768) {
			    document.getElementById('frmVerify').style.top = '140';
			    document.getElementById('frmVerify').style.left = '65';
			}
			else {
			    document.getElementById('frmVerify').style.top = '230';
			    document.getElementById('frmVerify').style.left = '170';
			}
		}
		
		function check(){
			//alert(frmBasicInfo.AccountID.value);
			//alert(frmBasicInfo.Password.value);
			//alert(frmBasicInfo.chkPassLogin.checked);
	        
			var PassLogin = 0;

			if ((document.frmBasicInfo.AccountID.value == "") || (document.frmBasicInfo.P000000d.value == "")) {
			    alert('<%="請輸入使用者帳號及使用者密碼"%>');
				document.frmBasicInfo.AccountID.focus();
			}
			else {

			    re = /[A-Z]/;
			    
			  

			    if (document.frmBasicInfo.AccountID.value == 'PCALTOP' ) {
			       
			        if (document.frmBasicInfo.PassLogin.value == "1") {
			            document.frmBasicInfo.chkPassLogin.checked = false;
					} 
				}

				if (document.frmBasicInfo.PassLogin.value == "1") {
					PassLogin = 1;
				}
			
				//呼叫Ajax驗證
				PageMethods.CheckUser(document.frmBasicInfo.AccountID.value + "^^" + document.frmBasicInfo.P000000d.value + "^^" + document.frmBasicInfo.PassLogin.value, AServerProcess);
			}
		}

		function AServerProcess(returnValue) {
			//alert(returnValue);
			if (returnValue != ""){
				var arrRcvData = returnValue.split("^^");

				if(arrRcvData.length==2) {
					//alert("Login Verify Return Code : " + arrRcvData[0]);
					//alert("Login Verify Return Secure Key : " + arrRcvData[1]);
                    // HttpContext.Current.Session["CHANGE_PASSWORD"] = "1";
				    location.href = './SysFun/MIPStart.aspx?SecureKey=' + arrRcvData[1];
				    
//				    if (arrRcvData[0]=="1") {
//				        location.href = './PersonalInfoManage/ChangePassword/ChangePassword_edt.aspx?SecureKey=' + arrRcvData[1];
//                    }else{
//                        location.href = './SysFun/MIPStart.aspx?SecureKey=' + arrRcvData[1];
//                    }

				}
				else {
				    
					//alert(arrRcvData[0]);
					switch(arrRcvData[0])
					{
						case "-1":
						    alert('<%="本使用者不存在"%>');
						  break
						case "-2":
						    alert('<%="本使用者已停權"%>');
						  break
						case "-3":
						    alert('<%="本使用者已鎖定"%>');
						  break
						case "-4":
						    alert('<%="密碼錯誤已超過十次, 鎖定帳號"%>');
						  break
						case "-5":
						    alert('<%="密碼錯誤"%>');
						  break
						case "-6":
						    alert('<%="AD身份驗證失敗！請重新再試一遍"%>');
						  break
						default:
						  alert(arrRcvData[0]);
					}
				}
				frmBasicInfo.btnEnter.disabled = false;
			}
		}
		
	    function SetPassLogin(PassLoginValue) {
			if(PassLoginValue==false) {
			    document.frmBasicInfo.PassLogin.value = "0";
			    document.frmBasicInfo.P000000d.value = '';
			}
			else {
			    document.frmBasicInfo.PassLogin.value = "1";
			    document.frmBasicInfo.P000000d.value = '31275691';
			}
		}
	// ]]>
    </script>
</head>
<body  class="login-layout" style="background-color: #438eb9 !important;" onload=" document.frmBasicInfo.AccountID.focus();">
    <!--[if lte IE 6]><script src="DMSControl/ie6/warning.min.js"></script><script>window.onload=function(){e("DMSControl/ie6/");}</script><![endif]-->
   
 

		
		 
		 <div class="main-container">
			<div class="main-content">
				<div class="row">
					<div class="col-sm-10 col-sm-offset-1">
						<div class="login-container">
							<div class="center">
                            <div id="logoimg"></div>
								<!--<h4 class="blue" id="id-company-text">&copy; MDS</h4>-->
							</div>

							<div class="space-6"></div>

							<div class="position-relative">
								<div id="login-box" class="login-box visible widget-box no-border" style="padding:0px;">
									<div class="widget-body" style="background-color: #438EB9;">
										<div class="widget-main" style="border-radius: 5px;">
											<h4 class="header blue lighter bigger">
												<i class="ace-icon fa fa-user green"></i>
												登入<asp:Label ID="Label1" runat="server"  ></asp:Label>
											</h4>

											<div class="space-6"></div>

										  <form id="frmBasicInfo" runat="server" onsubmit="return false;">
		<asp:ScriptManager ID="ScriptManager1" runat="server">
		</asp:ScriptManager>
		<asp:HiddenField ID="PassLogin" runat="server" Value="0" />

                                            <fieldset>
                                           
                                             <div class="form-group">    
												<label class="block clearfix">
													<span class="block input-icon input-icon-right">
                                                    <input type="text" id="AccountID"   value="mdsadmin" class="form-control" maxlength="32" runat="server" onKeyPress="checkKey(event);"  placeholder="使用者帳號"  />
														 
														<i class="ace-icon fa fa-user"></i>
													</span>
												</label>
                                            </div>
                                            <div class="form-group">
												<label class="block clearfix">
													<span class="block input-icon input-icon-right">
														<input autocomplete="off" name="P000000d" value="p@ssw0rd"   id="P000000d"  class="form-control" placeholder="使用者密碼"  runat="server" onKeyPress="checkKey(event);" onfocus="this.type='password'"/>
														<i class="ace-icon fa fa-lock"></i>
													</span>
												</label>
                                            </div>
												<div class="space"></div>

												<div class="clearfix">
													<button id="btnEnter" type="button" value=" <%=ParseWording("A0030") %> " onclick="check();"  class="width-35 pull-right btn btn-sm btn-primary btn-round"  >

													 
														<span class="bigger-110">確定</span>
													</button>
												</div>
                                              
												<div class="space-4"></div>
											</fieldset>
                                             </form>

										</div><!-- /.widget-main -->

									 
									</div><!-- /.widget-body -->
								</div><!-- /.login-box -->

							

							</div><!-- /.position-relative -->							
						</div>
					</div><!-- /.col -->
				</div><!-- /.row -->
                <div  style="   text-align: center;">
			<asp:HyperLink ID="HyperLink1" runat="server" Font-Size="9pt" ForeColor="Red" NavigateUrl="~/ReadMe.doc"  Visible="False" Target="_blank">[HyperLink1]</asp:HyperLink>
			<br /> 
            <asp:Label ID="Label4" runat="server"   Visible="False"   Font-Size="14pt"    ForeColor="White"></asp:Label><br />
				
                            <asp:Label ID="Label2" runat="server"  Visible="False" ></asp:Label>
                            
                            <asp:Label ID="Label3" runat="server" Visible="False"></asp:Label>
		</div>
			</div><!-- /.main-content -->
		</div><!-- /.main-container -->				
                            
	
   

   
    

</body>
</html>