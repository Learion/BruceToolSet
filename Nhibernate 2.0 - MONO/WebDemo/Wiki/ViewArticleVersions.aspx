<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true"
    Inherits="Wiki_ViewArticleVersions" Title="View article versions" Codebehind="ViewArticleVersions.aspx.cs" %>

<%@ Register TagPrefix="uc" TagName="ArticleVersionList" Src="~/Controls/ArticleVersionList.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="Server">
    
    <h1>Versions</h1>
    
    <uc:ArticleVersionList ID="viewArticle" runat="server" />    
    
</asp:Content>
