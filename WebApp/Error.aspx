<%@ Page Language="C#" MasterPageFile="~/BC.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs"
    Inherits="SEOToolSet.WebApp.Error" %>

<%@ Register TagPrefix="user" TagName="PageTitle" Src="PageTitle.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentArea" runat="server">
    <!-- Control Used for the Page Title -->
    <user:PageTitle PageTitleText="<%$ Resources:CommonTerms, Error %>" PageDescription="<%$ Resources:CommonTerms, PageErrorMessage %>"
        ID="WebUserControl0" runat="server"></user:PageTitle>
</asp:Content>
