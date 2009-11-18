<%@ Import Namespace="WebDemo.code"%>
<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Controls_ArticleVersionList" Codebehind="ArticleVersionList.ascx.cs" %>

<p>Versions for article: <span id="lblTitle" runat="server"></span>
</p>

<table class="datatable">
    <thead>
        <tr>
            <th>Version</th>
            <th>Author</th>
            <th>Date</th>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater ID="listRepeater" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <a href="<%# GetViewUrl( (Eucalypto.Wiki.ArticleBase)Container.DataItem ) %>">
                            <%# Eval("Version") %>
                        </a>
                    </td>
                    <td>
                        <%# Utilities.GetDisplayUser((string)Eval("UpdateUser"))%>
                    </td>
                    <td>
                        <%# Utilities.GetDateTimeForDisplay((DateTime?)Eval("UpdateDate"))%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>