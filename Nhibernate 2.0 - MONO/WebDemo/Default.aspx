<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" Title="Home" %>

<%@ Register TagPrefix="uc" TagName="ViewArticle" Src="~/Controls/ViewArticle.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="Server">

    <uc:ViewArticle ID="viewArticle" runat="server" ArticleName="homepage" ArticleVersion="0" SectionActionsVisible="false" SectionPropertiesVisible="false" />    

</asp:Content>
