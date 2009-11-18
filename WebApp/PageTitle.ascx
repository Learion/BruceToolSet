<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="PageTitle.ascx.cs" Inherits="SEOToolSet.WebApp.PageTitle" %>
<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<div class="PageHeader">
    <div class="PageTitle">
        <h2>
            <asp:Literal ID="litTitle" runat="server"></asp:Literal></h2>
        <cc1:RoundPanel ID="panelContainer" runat="server">
            <p>
                <asp:Literal ID="litDescription" runat="server"></asp:Literal></p>
            <P class="accountinfo"><label class="strong">Account:</label> <asp:Literal ID="litAccount" runat="server"></asp:Literal></P>
        </cc1:RoundPanel>
        <div id="HelpDocumentation" class="HelpSection" runat="server">
            <asp:HyperLink ID="hlnkDocumentation" Target="_blank" NavigateUrl="#" runat="server">
                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:CommonTerms, HelpAndDocumentation %>"></asp:Label>
            </asp:HyperLink>
        </div>
    </div>
</div>
