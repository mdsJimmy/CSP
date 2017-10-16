<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NaviMenu.ascx.cs" Inherits="MDSControl_NaviMenu" %>
<ul class="nav nav-list">
    <asp:Literal ID="ContentLiteral" runat="server"></asp:Literal>
    <li class=""><a href="<%=ResolveUrl("~/Verify.aspx?x=logout")%>" ><i class="menu-icon fa fa-sign-out"></i><span class="menu-text"> 登出 </span></a>
</ul>