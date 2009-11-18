<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true"
    Inherits="Forum_ViewForum" Title="View forum" Codebehind="ViewForum.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="TopicList" Src="~/Controls/TopicList.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="Server">
    
    <h1>Forum: <span id="lblForumName" runat="server"></span></h1>
    <p id="lblDescription" runat="server">
    </p>
    <p>
        <a class="newitem" id="linkNewTopic" runat="server" >New topic</a>
        <a class="rss" id="linkRss" runat="server">RSS News</a>    
        <a class="search" id="linkSearch" runat="server">Search</a>    
    </p>
    
    <uc:TopicList ID="topicList" runat="server" />
    
</asp:Content>
