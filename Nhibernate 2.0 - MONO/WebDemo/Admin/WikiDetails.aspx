<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" Inherits="WikiDetails" Title="Wiki Details" Codebehind="WikiDetails.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="Server">
    <h1>
        Wiki details</h1>
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
        <h2>File Attachments</h2>
        <p>Here you can enable or disable attachments, set the accepted extensions and a maximum upload size.</p>
        <table>
            <tr>
                <td>
                    <label for="chkEnabledAttach">Enable attachments:</label>
                </td>
                <td>
                    <asp:CheckBox ID="chkEnabledAttach" runat="server"></asp:CheckBox></td>
            </tr>
            <tr>
                <td>
                    <label for="txtAttachExtensions">Accepted attachments extensions *:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtAttachExtensions" runat="server" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtAttachMaxSize">Maximum attachments size (Kb):</label>
                </td>
                <td>
                    <asp:TextBox ID="txtAttachMaxSize" runat="server"></asp:TextBox></td>
            </tr>                                    
        </table>
        <small>* Use a list of extension separated by comma like: '.zip,.doc,.jpg'. You can also use some special constants to include the most common extensions: 'ALL_IMAGES,ALL_DOCS,ALL_ARCHIVE,ALL_CODE'. Use '*' to accept all extensions.</small>
        
        <h2>Access control permissions</h2>
        <p>Use '?' to allow all users, '*' to allow authenticated users or specify a role name. You can use multiple roles separating each role by ','.
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
            <tr>
                <td>
                    <label for="txtApprovePermissions">Approve Permissions:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtApprovePermissions" runat="server" MaxLength="100"></asp:TextBox></td>
            </tr>
        </table>
        
        <h2>Approval settings</h2>
        <p>Set 'Auto approve' to true to automatically approve the submitted articles. In this case there is no need to manually approve each submitted article.</p>
        <table>
            <tr>
                <td>
                    <label for="chkAutoApprove">Auto approve:</label>
                </td>
                <td>
                    <asp:CheckBox ID="chkAutoApprove" runat="server"></asp:CheckBox></td>
            </tr>
        </table>

        <h2>XHTML Validation</h2>
        <p>Set the xhtml validation mode.</p>
        <table>
            <tr>
                <td>
                    <label for="cmbXhtmlMode">XHTML Validation:</label>
                </td>
                <td>
                    <asp:DropDownList ID="cmbXhtmlMode" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>

        <h2>Versioning</h2>
        <p>Set the versioning/backup mode.</p>
        <table>
            <tr>
                <td>
                    <label for="cmbBackupMode">Versioning mode:</label>
                </td>
                <td>
                    <asp:DropDownList ID="cmbBackupMode" runat="server">
                    </asp:DropDownList>
                </td>
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
