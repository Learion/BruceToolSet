<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" Inherits="NewsList" Title="News List" Codebehind="NewsList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="Server">
    <h1>
        News category list</h1>
    <div>
        <p>
            <a id="linkNewItem" runat="server" class="newitem">New category</a>
            <a id="linkShowNews" runat="server">Show news</a>
        </p>
        <asp:Repeater ID="listRepeater" runat="server" OnItemCommand="listRepeater_ItemCommand">
            <HeaderTemplate>
                <table class="datatable">
                    <thead>
                        <tr>
                            <th>
                                Name</th>
                            <th>
                                DisplayName</th>
                            <th>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Eval("Name") %>
                    </td>
                    <td>
                        <%# Server.HtmlEncode((string)Eval("DisplayName")) %>
                    </td>
                    <td>
                        <asp:LinkButton CssClass="edititem" ID="LinkButton1" runat="server"
                            CommandName="edit" CommandArgument='<%# Eval("Id") %>' >Edit</asp:LinkButton>
                        
                        <asp:LinkButton CssClass="deleteitem" OnClientClick="return confirm('Are you sure to delete news category?');"
                            ID="LinkButton2" runat="server" CommandName="delete" CommandArgument='<%# Eval("Id") %>' >Delete</asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody> </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
