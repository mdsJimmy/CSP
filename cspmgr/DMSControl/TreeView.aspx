<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/ContentPage.master"  CodeFile="TreeView.aspx.cs" Inherits="myTreeView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PluginStylesPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitlePlaceHolder" runat="Server">
 <div class="row" >
									<div class="col-sm-12"  >
										<div class="widget-box widget-color-blue2"  style='height:700px'>
											<div class="widget-header">
												<h4 class="widget-title lighter smaller"  >組織樹</h4>
											</div>

											<div class="widget-body"  style='height:800px;overflow: auto;'>
												<div class="widget-main padding-1">
													<ul id="tree1"></ul>
												</div>
											</div>
										</div>
									</div>

						 
								</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
            <!-- #section:plugins/fuelux.treeview -->
								
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PluginScriptsPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="InlineScriptsPlaceHolder" runat="Server">
 
		<script type="text/javascript"  src="<%=ResolveUrl("~/Plugins/ace-assets/js/fuelux/fuelux.tree.mds.js")%>"></script>
        	<script type="text/javascript"  src="<%=ResolveUrl("~/Plugins/ace-assets/js/ace/elements.treeview.js")%>"></script>

<script type="text/javascript">
    var call = false;
    var nowNode ='<%:Session["ParentGroupID"] %>';
    function callNode(text){
 
         var TreeNode = text;
         
         if(!call || nowNode== TreeNode ){
         }else{
             parent.frmMain.DoChangeTreeNode(TreeNode);
             nowNode = TreeNode;
         }
    }
    jQuery(function ($) {
                   var sampleData = initiateDemoData(); //see below


                   $('#tree1').ace_tree({
                       dataSource: sampleData['dataSource1'],
                       ///  multiSelect: true,
                       cacheItems: true,
                       'open-icon': 'ace-icon tree-minus',
                       'close-icon': 'ace-icon tree-plus',
                       'selectable': true,
                       'selected-icon': null,
                       'unselected-icon': null,
                       loadingHTML: '<div class="tree-loading"><i class="ace-icon fa fa-refresh fa-spin blue"></i></div>'
                   });

                   

                   //please refer to docs for more info
                   $('#tree1')
//                   .on('loaded.fu.tree', function (e) {  })
//                   .on('updated.fu.tree', function (e, result) { })
//                     .on('closed.fu.tree', function (e) { }
//                      .on('deselected.fu.tree', function (e) { })
                   .on('selected.fu.tree', function (e, data) {
                          //  alert(data.target.gid);
                             callNode(data.target.gid);
                   })
                  
                   .on('opened.fu.tree', function (e, data) {
                       //  alert(data.gid);
                        callNode(data.gid);    
                   });
                   /** */
                  

                   function initiateDemoData() {

                       <%=ReplaceString %>
                      

                       var dataSource1 = function (options, callback) {
                           var $data = null
                           if (!("text" in options) && !("type" in options)) {
                               $data = tree_data; //the root tree
                               callback({ data: $data });
                               return;
                           }
                           else if ("type" in options && options.type == "folder") {
                               if ("additionalParameters" in options && "children" in options.additionalParameters)
                                   $data = options.additionalParameters.children || {};
                               else $data = {}//no data
                           }

                           if ($data != null)//this setTimeout is only for mimicking some random delay
                           callback({ data: $data });
                              // setTimeout(function () { callback({ data: $data }); }, parseInt(Math.random() * 500) + 200);

                           //we have used static data here
                           //but you can retrieve your data dynamically from a server using ajax call
                           //checkout examples/treeview.html and examples/treeview.js for more info
                       } 
                        return { 'dataSource1': dataSource1 }
                   }
                   for (i = 0; i <=2; i++) {
                    $('.icon-folder.tree-plus').each(function (index, value) {
                        if (index > 0) {
                            $(this).trigger("click.fu.tree", this);
                        }
                    });
                    }
                    call = true;
               });
  
    
</script>

</asp:Content>