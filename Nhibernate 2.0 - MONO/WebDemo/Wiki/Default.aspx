<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" 
        Inherits="Wiki_Default" Title="Articles" Codebehind="Default.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" Runat="Server">
    <h1>
        Wiki list</h1>
    <div>
        <p>
            <a class="rss" id="linkRss" runat="server">RSS News</a>
            <a class="search" id="linkSearch" runat="server">Search</a>    
        </p>
    
        <asp:Repeater ID="listRepeater" runat="server">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <a href="<%# GetCategoryLink((string)Eval("Name")) %>"><%# Server.HtmlEncode((string)Eval("DisplayName")) %></a> - <%# Server.HtmlEncode((string)Eval("Description")) %>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

