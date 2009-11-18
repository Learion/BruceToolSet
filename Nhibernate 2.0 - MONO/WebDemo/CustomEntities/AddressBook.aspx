<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="True" Inherits="CustomEntities_AddressBook" Title="Untitled Page" Codebehind="AddressBook.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" Runat="Server">
    <h1>Address Book</h1>
    <p>This page show an example of a custom entity, a contact list implemented using Eucalypto business layer.</p>
    
    <div>
        <table>
            <tr>
                <td>
                    <label for="txtName">Display Name:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtDisplayName" runat="server" MaxLength="100"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validatorName" runat="server" ControlToValidate="txtDisplayName"
                        Display="Static" ErrorMessage="Display Name field is required" ValidationGroup="ContactAdd"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtFirstName">First Name:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtFirstName" runat="server" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtLastName">Last Name:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtLastName" runat="server" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtAddress">Address:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtAddress" runat="server" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtTelephone">Telephone:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtTelephone" runat="server" MaxLength="100"></asp:TextBox>
                </td>
            </tr>                        
        </table>
        <p>
            <asp:Button ID="btAdd" runat="server" Text="Add" OnClick="btAdd_Click" ValidationGroup="ContactAdd" />
        </p>


        <asp:Repeater ID="listRepeater" runat="server" OnItemCommand="listRepeater_ItemCommand">
            <HeaderTemplate>
                <table class="datatable">
                    <thead>
                        <tr>
                            <th>
                                Display Name</th>
                            <th>
                                FirstName</th>
                            <th>
                                LastName</th>
                            <th>
                                Address</th>
                            <th>
                                Telephone</th>
                            <th>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# HttpUtility.HtmlEncode((string)Eval("DisplayName")) %>
                    </td>
                    <td>
                        <%# HttpUtility.HtmlEncode((string)Eval("FirstName")) %>
                    </td>
                    <td>
                        <%# HttpUtility.HtmlEncode((string)Eval("LastName")) %>
                    </td>
                    <td>
                        <%# HttpUtility.HtmlEncode((string)Eval("Address")) %>
                    </td>
                    <td>
                        <%# HttpUtility.HtmlEncode((string)Eval("Telephone1")) %>
                    </td>
                    <td>
                        <asp:LinkButton CssClass="deleteitem" OnClientClick="return confirm('Are you sure to delete the contact?');"
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

