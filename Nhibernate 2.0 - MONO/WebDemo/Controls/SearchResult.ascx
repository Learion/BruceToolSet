<%@ Import Namespace="WebDemo.code"%>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_SearchResult" Codebehind="SearchResult.ascx.cs" %>

<div class="pageNavigation">
    <span id="lblCurrentPage" runat="server" /> of 
        <span id="lblTotalPage" runat="server" />
    <asp:LinkButton ID="linkPrev" runat="server" CssClass="previous" OnClick="linkPrev_Click">Prev</asp:LinkButton>
    <asp:LinkButton ID="linkNext" runat="server" CssClass="next" OnClick="linkNext_Click">Next</asp:LinkButton>
</div>

<asp:Repeater ID="listRepeater" runat="server">
    <ItemTemplate>
        <div class="searchBox">
            <a href="<%# GetViewUrl((Eucalypto.ISearchResult)Container.DataItem ) %>">
                <h3 class="searchTitle"><%# HttpUtility.HtmlEncode( ((Eucalypto.ISearchResult)Container.DataItem).Title ) %></h3>
            </a>
            
            <div class="searchBody"><%# HttpUtility.HtmlEncode( ((Eucalypto.ISearchResult)Container.DataItem).Description ) %></div>
            
            <small class="searchFooter"><%# HttpUtility.HtmlEncode( ((Eucalypto.ISearchResult)Container.DataItem).Category ) %> -
             <%# Utilities.GetDateTimeForDisplay(((Eucalypto.ISearchResult)Container.DataItem).Date)%></small>
        </div>
    </ItemTemplate>
</asp:Repeater>
