<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_ViewArticle" Codebehind="ViewArticle.ascx.cs" %>

<p runat="server" id="sectionError" class="errorBox">Article '<%= ArticleName %>' not found</p>

<div runat="server" id="controlDiv" class="article">
    <div class="articleProperties" id="sectionProperties" runat="server">
        <div>
            Title: <span class="articleTitle" id="lblArticleTitle" runat="server"></span>
        </div>
        <div class="articleAuthor">
            <a class="user" id="linkAuthor" runat="server">by <span id="lblAuthor" runat="server"></span></a>
        </div>
        <div class="articleDate">
            Date modified: <span id="lblDate" runat="server"></span>
        </div>
        <div class="articleVersion">
            Version: <span id="lblVersion" runat="server"></span>
            <small>(<a id="linkLatestVersion" runat="server">Latest version</a>
            <a id="linkBrowseVersions" runat="server">Browse versions</a>)</small>
        </div>
        <div>
            <span class="articleDescription" id="lblArticleDescription" runat="server"></span>
        </div>
    </div>
    <div class="articleActions" id="sectionActions" runat="server">
        <a id="linkEdit" runat="server" class="edititem">Edit</a>
        <a id="linkPrint" runat="server" class="printitem" target="_blank">Print</a>
    </div>
    
    <div class="articleBody" runat="server" id="sectionBody">
    </div>
</div>