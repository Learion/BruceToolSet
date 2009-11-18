<%@ Page Title="" Language="C#" MasterPageFile="~/BC.Master" AutoEventWireup="true" CodeBehind="ComparisonReport.aspx.cs" Inherits="SEOToolSet.WebApp.ComparisonReport" %>
<%@ Register Src="PageTitle.ascx" TagName="PageTitle" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentArea" runat="server">
    <uc1:PageTitle ID="PageTitle1" RenderRoundPanelStyles="true" runat="server" PageTitleText="<%$ Resources:Navigation, ComparisonReport_Title %>"
        PageDescription="<%$ Resources:CommonTerms, SiteUnderConstruction %>" />
</asp:Content>
