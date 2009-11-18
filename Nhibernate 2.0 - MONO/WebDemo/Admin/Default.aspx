<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" Inherits="Admin_Default" Title="Administration" Codebehind="Default.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" Runat="Server">
    <h1>Administration features</h1>
    
    <ul>
        <li><a href="UserList.aspx">Manage users</a></li>
        <li><a href="RoleList.aspx">Manage roles</a></li>
        <li><a href="ForumList.aspx">Manage forums</a></li>
        <li><a href="ManageProfiles.aspx">Manage profiles</a></li>
        <li><a href="WikiList.aspx">Manage articles</a></li>
        <li><a href="NewsList.aspx">Manage news</a></li>
    </ul>
</asp:Content>

