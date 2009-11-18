<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_NewsListBox" Codebehind="NewsListBox.ascx.cs" %>

<div class="box">
    <div class="boxTitle" id="title" runat="server"></div>

    <div class="newsBoxContent">
        <p runat="server" id="sectionError" class="errorBox">News category '<%= CategoryName %>' not found</p>
        
        <a class="rss" id="linkRss" runat="server">RSS News</a>
        
        <asp:Repeater ID="listRepeater" runat="server">
            <ItemTemplate>
                <div class="newsBoxTitle">
                    <a href="<%# GetViewUrl( (Eucalypto.News.Item)Container.DataItem ) %>">
                        <%# Server.HtmlEncode((string)Eval("Title")) %>
                    </a>
                </div>
                <div class="newsBoxDescription">
                     <%# Server.HtmlEncode(GetShortDescription((Eucalypto.News.Item)Container.DataItem)) %>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
