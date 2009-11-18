<%@ Import Namespace="WebDemo.code"%>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_ArticleList" Codebehind="ArticleList.ascx.cs" %>

<table class="datatable">
    <thead>
        <tr>
            <th>Title</th>
            <th>Author</th>
            <th>Date</th>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater ID="listRepeater" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <a href="<%# GetViewArticleUrl( (string)Eval("Name") ) %>">
                            <%# Server.HtmlEncode((string)Eval("Title")) %>
                        </a>
                        <small><%# Server.HtmlEncode(GetArticleStatus((bool)Eval("Enabled"), (bool)Eval("Approved"))) %></small>
                        <div>
                            <small>
                                 <%# Server.HtmlEncode((string)Eval("Description")) %>
                            </small>
                        </div>
                    </td>
                    <td>
                        <%# Utilities.GetDisplayUser((string)Eval("Author")) %>
                    </td>
                    <td>
                        <%# Utilities.GetDateTimeForDisplay((DateTime?)Eval("UpdateDate"))%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>