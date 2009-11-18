<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" Inherits="NewsDetails" Title="News Details" Codebehind="NewsDetails.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="Server">
    <h1>
        News details</h1>
    <div>
        <asp:ValidationSummary ID="validationSummary" runat="server" DisplayMode="List" />
        <table>
            <tr>
                <td>
                    <label for="txtName">Name:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" MaxLength="100"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <label for="txtDisplayName">Display name:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtDisplayName" runat="server" MaxLength="100"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <label for="txtDescription">Description:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" MaxLength="300" Columns="50"></asp:TextBox></td>
            </tr>
        </table>

        <h2>Access control permissions</h2>
        <p>Use '?' to allow all users, '*' to allow authenticated users or specify a role nome. You can use multiple roles separating each role by ','.
        Remember that the author of the message can always delete or edit it (also if anonymous). Use the '!' prefix to deny a specific role.
         If you are not sure of to configure leave the default.</p>

        <table>
            <tr>
                <td>
                    <label for="txtReadPermissions">Read Permissions:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtReadPermissions" runat="server" MaxLength="100"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <label for="txtEditPermissions">Edit Permissions:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtEditPermissions" runat="server" MaxLength="100"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <label for="txtDeletePermissions">Delete Permissions:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtDeletePermissions" runat="server" MaxLength="100"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <label for="txtInsertPermissions">Insert Permissions:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtInsertPermissions" runat="server" MaxLength="100"></asp:TextBox></td>
            </tr>
        </table>
        
        <asp:RequiredFieldValidator ID="validatorName" runat="server" ControlToValidate="txtName"
            Display="None" ErrorMessage="Name field is required"></asp:RequiredFieldValidator>
        <p>
            <asp:Button ID="btSave" runat="server" Text="Save" OnClick="btSave_Click" />
            <asp:Button ID="btCancel" runat="server" Text="Cancel" OnClick="btCancel_Click" CausesValidation="false" />
        </p>
    </div>
</asp:Content>
