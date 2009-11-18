<%@ Import Namespace="WebDemo.code"%>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_NewsList" Codebehind="NewsList.ascx.cs" %>

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
                        <a href="<%# GetViewUrl( (Eucalypto.News.Item)Container.DataItem ) %>">
                            <%# Server.HtmlEncode((string)Eval("Title")) %>
                        </a>
                        <div>
                            <small>
                                 <%# Server.HtmlEncode(GetShortDescription((Eucalypto.News.Item)Container.DataItem)) %>
                            </small>
                        </div>
                    </td>
                    <td>
                        <%# Utilities.GetDisplayUser((string)Eval("Owner"))%>
                    </td>
                    <td>
                        <%# Utilities.GetDateTimeForDisplay( (DateTime?)Eval("NewsDate") ) %>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>