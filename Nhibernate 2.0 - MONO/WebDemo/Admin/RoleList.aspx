<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" Inherits="RoleList" Title="Roles List" Codebehind="RoleList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="Server">
    <h1>
        Roles list</h1>
    <div>
        <table>
            <tr>
                <td>
                    <label for="txtName">Role:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validatorName" runat="server" ControlToValidate="txtName"
                        Display="Static" ErrorMessage="Name field is required" ValidationGroup="RoleAdd"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <p>
            <asp:Button ID="btAdd" runat="server" Text="Add" OnClick="btAdd_Click" ValidationGroup="RoleAdd" />
        </p>


        <asp:Repeater ID="listRepeater" runat="server" OnItemCommand="listRepeater_ItemCommand">
            <HeaderTemplate>
                <table class="datatable">
                    <thead>
                        <tr>
                            <th>
                                Name</th>
                            <th>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Container.DataItem %>
                    </td>
                    <td>
                        <asp:LinkButton CssClass="deleteitem" OnClientClick="return confirm('Are you sure to delete role?');"
                            ID="LinkButton2" runat="server" CommandName="delete" CommandArgument='<%# Container.DataItem %>' >Delete</asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody> </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
