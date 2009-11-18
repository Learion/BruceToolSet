<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" 
    Inherits="User_Default" Title="User" Codebehind="Default.aspx.cs" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" Runat="Server">
    <h1>User settings</h1>
    <ul>
        <li><a href="ChangePassword.aspx">Change password</a></li>
        <li><a href="ChangeUserInfo.aspx">Change user informations</a></li>
    </ul>
</asp:Content>

