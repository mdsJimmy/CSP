<%@ Control Language="C#" AutoEventWireup="true" CodeFile="oListView_Button.ascx.cs" Inherits="oListView_Button" %>

<script src="<%=ResolveUrl("superTables/superTables.min.js")%>" type="text/javascript"></script>
<link href="<%=ResolveUrl("superTables/superTables.css")%>" rel="stylesheet" type="text/css" />

    <asp:ListView ID="_ListView" runat="server" DataSourceID="_SqlDataSource" 
        ondatabound="ListView_DataBound" onsorted="ListView_Sorted">
    </asp:ListView>
 
    <div id="div_ListView" style="overflow:hidden;height:100px;width:100px;display:none "></div>
<div style='width:100%;text-align:center'>
    
                <asp:DataPager ID="_DataPager" runat="server" PagedControlID="_ListView" 
                    PageSize="10">
                    <Fields>
                        <asp:TemplatePagerField OnPagerCommand="TemplatePagerField_OnPagerCommand">
                            <PagerTemplate>
                                <asp:LinkButton ID="FirstPageButton" runat="server" Enabled="False" CommandName="FirstPage" ClientIDMode="Static" CssClass="btn btn-round btn-primary btn-white" > << 首頁</asp:LinkButton>
                                <asp:LinkButton ID="PreviousPageButton" runat="server" Enabled="False" CommandName="PreviousPage"  ClientIDMode="Static" formtarget="_parent" CssClass="btn btn-round btn-primary btn-white" > < 上一頁</asp:LinkButton>
                                <asp:DropDownList ID="Jumper" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Jumper_SelectedIndexChanged" Enabled="False" ClientIDMode="Static"></asp:DropDownList>
                                <asp:LinkButton ID="NextPageButton" runat="server" Enabled="False" CommandName="NextPage" ClientIDMode="Static" CssClass="btn btn-round btn-primary btn-white">下一頁 ></asp:LinkButton>
                                <asp:LinkButton ID="LastPageButton" runat="server" Enabled="False" CommandName="LastPage" ClientIDMode="Static" CssClass="btn btn-round btn-primary btn-white">末頁 >> </asp:LinkButton>
                                &nbsp;<asp:Label ID="PagerInfoLabel" runat="server" Text=""></asp:Label>
                            </PagerTemplate>
                        </asp:TemplatePagerField>
                    </Fields>
                </asp:DataPager>
   
    </div>
<asp:SqlDataSource ID="_SqlDataSource" runat="server"></asp:SqlDataSource>


<script type ="text/javascript">

    <%--$(document).ready(function() {
        

	    $('#chkAll').click(function(){
        var _IsCheck = ($('#chkAll').prop('checked'));
        
	        if(<%=FixedCols%> > 0){
	            $('.sFData input[id$="_chk"]').each(function(){$(this).attr('checked', _IsCheck);});
              }
	        else{
          
	            $('input[id$="_chk"]').each(function(){
                  
                $(this).prop('checked', _IsCheck);

                });
              }
	    });
    });--%>
   
       
        if ('<%:PageCount%>' == '0') { 
               
            
           
         
           

            document.getElementById("FirstPageButton").setAttribute("href", "#");
            document.getElementById("FirstPageButton").setAttribute("disabled", "disabled");

            document.getElementById("PreviousPageButton").setAttribute("href", "#");
            document.getElementById("PreviousPageButton").setAttribute("disabled", "disabled");

            document.getElementById("NextPageButton").setAttribute("href", "#");
            document.getElementById("NextPageButton").setAttribute("disabled", "disabled");

            document.getElementById("LastPageButton").setAttribute("href", "#");
            document.getElementById("LastPageButton").setAttribute("disabled", "disabled");
         
         }
   
    

    
    function GetCheckBoxValue(){
        var str = '';
       //alert($('input[id$="_chk"]').length);
        
        if(<%=FixedCols%> > 0)
        {
      //  alert('here23332');
            $('.sFData input[id$="_chk"]').each(function(){
                if($(this).prop('checked') ){
					//alert($(this).val());
					str = str + ($(this).val()) + '^^';
				}
                    
            });
        }
        else
        {
        
            $('input[id$="_chk"]').each(function(){
            //alert('here555>'+$(this).prop('checked'));
                if($(this).prop('checked') ){
                    //alert($(this).val());
                    str = str + ($(this).val()) + '^^';
                    }
            });
        }
    
        if(str.length > 0)
            str = str.substring(0, str.length -2);
    
        return str;
    }

</script>

