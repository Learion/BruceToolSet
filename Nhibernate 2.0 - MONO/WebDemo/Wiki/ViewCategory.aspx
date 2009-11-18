<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true"
    Inherits="Wiki_ViewCategory" Title="View category" Codebehind="ViewCategory.aspx.cs" %>

<%@ Register TagPrefix="uc" TagName="ArticleList" Src="~/Controls/ArticleList.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="Server">
    
    <h1>Category: <span id="lblDisplayName" runat="server"></span></h1>
    <p id="lblDescription" runat="server">
    </p>

    <p>
        <a class="newitem" id="linkNew" runat="server" >New article</a>
        <a class="rss" id="linkRss" runat="server">RSS News</a>    
        <a class="search" id="linkSearch" runat="server">Search</a>    
    </p>
    
    <uc:ArticleList ID="list" runat="server" />    

    <div runat="server" id="sectionMyArticles">
        <p>My articles:</p>
        <uc:ArticleList ID="listMyArticles" runat="server" />    
    </div>
    
    <div runat="server" id="sectionNotApproved">
        <p>Articles waiting for approval or disabled:</p>
        <uc:ArticleList ID="listNotApproved" runat="server" />    
    </div>
</asp:Content>
