<%@ Page Language="C#" AutoEventWireup="true"   MasterPageFile="~/MasterPage/MainPage.master" CodeFile="Organization_Main.aspx.cs" Inherits="DMSMainManage_Organization_Organization_Main" %>
 
<asp:Content ID="Content4" ContentPlaceHolderID="PluginStylesPlaceHolder" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitlePlaceHolder" Runat="Server">
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
        <iframe src="../../DMSControl/TreeView.aspx?TargerGroupID=<%:TargerGroupID%>" id="frmSel" name="frmSel" scrolling="no" marginwidth="0" marginheight="0"   width='250' height='700' hspace="0" vspace="0"   ></iframe>
        <iframe src="<%=MDS.Utility.NUtility.UrlEncode(tagUrl)%>?TargerGroupID=<%:TargerGroupID%>&PageNo=<%:PageNo%>&StrSearch=<%:mySearch%>" id="frmMain" name="frmMain" scrolling="yes" marginwidth="0" marginheight="0" topmargin="0" leftmargin="0" width='750'  height='700'></iframe>
   
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="PluginScriptsPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="InlineScriptsPlaceHolder" Runat="Server">
   
</asp:Content>

 
