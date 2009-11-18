<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" Inherits="Login" Title="Login" Codebehind="Login.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="Server">
    <h1>Login</h1>
    
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            <asp:Login ID="Login1" runat="server" CreateUserUrl="~/CreateUser.aspx" CreateUserText="Create new user"
                DestinationPageUrl="~/Default.aspx" PasswordRecoveryText="Lost password?" PasswordRecoveryUrl="~/PasswordRecovery.aspx">
            </asp:Login>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <p>Welcome <asp:LoginName ID="LoginName1" runat="server" /></p>
            <p><a href="Default.aspx">Home</a></p>
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>
