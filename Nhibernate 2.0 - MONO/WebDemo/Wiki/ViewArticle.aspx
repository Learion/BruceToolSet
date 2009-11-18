<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true"
    Inherits="Wiki_ViewArticle" Title="View article" Codebehind="ViewArticle.aspx.cs" %>

<%@ Register TagPrefix="uc" TagName="ViewArticle" Src="~/Controls/ViewArticle.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="Server">
    
    <uc:ViewArticle ID="viewArticle" runat="server" />    
    
</asp:Content>
