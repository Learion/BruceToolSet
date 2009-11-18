<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true"
    Inherits="News_ViewItem" Title="View news item" Codebehind="ViewItem.aspx.cs" %>

<%@ Register TagPrefix="uc" TagName="ViewNewsItem" Src="~/Controls/ViewNewsItem.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="Server">
    
    <h1>View news</h1>
    
    <uc:ViewNewsItem ID="viewItem" runat="server" />    
    
</asp:Content>
