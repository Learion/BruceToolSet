<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MonthYearPicker.ascx.cs"
    Inherits="SEOToolSet.WebApp.Controls.MonthYearPicker" EnableViewState="true" %>
<select id="<%= this.ClientID %>">
    <asp:Literal ID="MonthYearSelectionContent" runat="server"></asp:Literal>
</select>
<asp:LinkButton ID="HideCommand" runat="server" CausesValidation="false" OnClientClick="return false;"
    Style="display: none; left: 10px; position: relative; float: left;" CssClass="MiniCommand">
    <span>
        <asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:JavascriptMessages, HideCommand %>"></asp:Localize>
    </span>
</asp:LinkButton>
<asp:HiddenField ID="MonthYearSelected" runat="server" />
