<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true"
         Inherits="PasswordRecovery" Title="Password recovery" Codebehind="PasswordRecovery.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" Runat="Server">

    <asp:PasswordRecovery ID="passwordRecovery" runat="server">
    </asp:PasswordRecovery>

    <small>* To recovery the password your login must be configured with a valid e-mail. If you don't have a valid e-mail you must ask the administrator to reset the password.</small>
    
</asp:Content>

