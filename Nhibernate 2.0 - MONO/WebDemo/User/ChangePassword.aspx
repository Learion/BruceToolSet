<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" Inherits="ChangePassword" Title="Change password" Codebehind="ChangePassword.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" Runat="Server">
    <h1>Change password</h1>

    <asp:ChangePassword ID="ChangePassword1" runat="server" CancelDestinationPageUrl="~/Default.aspx" ContinueDestinationPageUrl="~/Default.aspx">
    </asp:ChangePassword>
</asp:Content>

