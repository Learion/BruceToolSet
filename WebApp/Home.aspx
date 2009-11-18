<%@ Page Language="C#" MasterPageFile="~/BC.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs"
    Inherits="SEOToolSet.WebApp.Home" MaintainScrollPositionOnPostback="false" %>

<%@ Register Src="PageTitle.ascx" TagName="PageTitle" TagPrefix="uc1" %>
<%@ Register Src="AccountProjectsList.ascx" TagName="AccountProjectsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentArea" runat="server">
    <uc1:PageTitle PanelContainerVisible="false" PageDescription="Manages the projects related to the user"
        ID="PageTitle1" runat="server" PageTitleText="<%$ Resources:CommonTerms, ProjectDashboard %>" />
    <uc2:AccountProjectsList ID="AccountProjectsList1" runat="server" OnProjectLaunchClick="DoProjectLaunch" 
        DisplayTitle="false" OnProjectDeleted="DoProjectDeleted"  OnProjectAdded="DoProjectAdded"  />
</asp:Content>
