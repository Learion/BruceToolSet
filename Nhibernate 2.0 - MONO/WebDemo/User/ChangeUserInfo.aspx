<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" Inherits="User_ChangeUserInfo" Title="Change user settings" Codebehind="ChangeUserInfo.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" Runat="Server">
    <h1>User settings</h1>

    <table>
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
                <label for="chkReceiveNotification">Receive notifications:</label>
            </td>
            <td>
                <asp:CheckBox ID="chkReceiveNotification" runat="server"></asp:CheckBox>
            </td>
        </tr>
        <tr>
            <td>
                <label for="txtFavoriteColor">Favorite color:</label>
            </td>
            <td>
                <asp:TextBox ID="txtFavoriteColor" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    
    <p>
        <asp:Button ID="btSave" runat="server" Text="Save" OnClick="btSave_Click" />
        <asp:Button ID="btCancel" runat="server" Text="Cancel" OnClick="btCancel_Click" CausesValidation="false" />
    </p>

</asp:Content>

