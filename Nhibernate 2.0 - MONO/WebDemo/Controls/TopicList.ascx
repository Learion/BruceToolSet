<%@ Import Namespace="WebDemo.code"%>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_TopicList" Codebehind="TopicList.ascx.cs" %>

<div class="pageNavigation">
    <span id="lblCurrentPage" runat="server" /> of 
        <span id="lblTotalPage" runat="server" />
    <asp:LinkButton ID="linkPrev" runat="server" CssClass="previous" OnClick="linkPrev_Click">Prev</asp:LinkButton>
    <asp:LinkButton ID="linkNext" runat="server" CssClass="next" OnClick="linkNext_Click">Next</asp:LinkButton>
</div>

<table class="datatable" width="100%">
    <thead>
        <tr>
            <th>Subject</th>
            <th>Replies</th>
            <th>Last Post</th>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater ID="listRepeater" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <a href="<%# GetViewTopicUrl( (string)Eval("Id") ) %>">
                            <%# Server.HtmlEncode((string)Eval("Title")) %>
                        </a><br />
                        &nbsp;&nbsp;by <%# Utilities.GetDisplayUser((string)Eval("Owner"))%>
                    </td>
                    <td align="center">
                        <%# GetRepliesCount((Eucalypto.Forum.Topic)Container.DataItem)%>
                    </td>
                    <td>
                        <small>
                            <%# GetLastPost((Eucalypto.Forum.Topic)Container.DataItem)%>
                        </small>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>