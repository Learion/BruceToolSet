<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true"
    Inherits="News_ViewCategory" Title="News category" Codebehind="ViewCategory.aspx.cs" %>

<%@ Register TagPrefix="uc" TagName="NewsList" Src="~/Controls/NewsList.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="Server">
    
    <h1>Category: <span id="lblDisplayName" runat="server"></span></h1>
    <p id="lblDescription" runat="server">
    </p>

    <p>
        <a class="newitem" id="linkNew" runat="server" >New</a>
        <a class="rss" id="linkRss" runat="server">RSS News</a>    
    </p>
    
    <uc:NewsList ID="list" runat="server" />    

</asp:Content>
