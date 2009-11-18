<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" Inherits="UserDetails" Title="User Details" Codebehind="UserDetails.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="Server">
    <h1>
        Login details</h1>
    <div>
        <table>
            <tr>
                <td>
                    <label for="txtName">Name:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validatorName" runat="server" ControlToValidate="txtName"
                        Display="Static" ErrorMessage="Name field is required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtEMail">E-Mail:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtEMail" runat="server" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtPassword">Choose a password:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" MaxLength="100" TextMode="Password"></asp:TextBox>
                    <asp:PlaceHolder ID="lblHelpPassword" runat="server"><small>Leave these fields empty to leave the previous password</small></asp:PlaceHolder>
                    <asp:CompareValidator ID="validatorPassword" runat="server" ControlToCompare="txtPassword2"
                        ControlToValidate="txtPassword" Display="Static" ErrorMessage="Password not valid"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtPassword2">Re-enter password:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtPassword2" runat="server" MaxLength="100" TextMode="Password"></asp:TextBox>
                    <asp:CompareValidator ID="validatorPassword2" runat="server" ControlToCompare="txtPassword"
                        ControlToValidate="txtPassword2" Display="Static" ErrorMessage="Password not valid"></asp:CompareValidator>
                </td>
            </tr>
        </table>

        <h2>Roles</h2>        
        <asp:CheckBoxList ID="chkListRoles" runat="server">
        </asp:CheckBoxList>
        
        <h2>Status</h2>
        <asp:CheckBox ID="chkApproved" runat="server" Text="Approved" />
        <asp:CheckBox ID="chkLocked" runat="server" Text="Locked" />
        
        <p>
            <asp:Button ID="btSave" runat="server" Text="Save" OnClick="btSave_Click" />
            <asp:Button ID="btCancel" runat="server" Text="Cancel" OnClick="btCancel_Click" CausesValidation="false" />
        </p>
    </div>
</asp:Content>
