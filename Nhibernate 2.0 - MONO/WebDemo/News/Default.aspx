<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" 
        Inherits="News_Default" Title="News" Codebehind="Default.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" Runat="Server">
    <h1>
        News list</h1>
    <div>
        <p>
            <a class="rss" id="linkRss" runat="server">RSS News</a>    
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

