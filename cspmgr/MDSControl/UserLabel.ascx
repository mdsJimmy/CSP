<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserLabel.ascx.cs" Inherits="MDSControl_UserLabel" %>
<img class="nav-user-photo" src="<%=ResolveUrl("~/Plugins/ace-assets/avatars/user.jpg")%>" alt="Jason's Photo" style="display:none" />
<span class="mds-user-info">
	<small>歡迎使用,</small>
    <asp:Literal ID="LiteralUserName" runat="server"></asp:Literal>
</span>