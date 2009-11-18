<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" Inherits="Admin_ManageProfiles" Title="Profiles" Codebehind="ManageProfiles.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" Runat="Server">
    
    <h1>Manage profiles</h1>

    <table>
        <tr>
            <td>
                <label for="cbType">Type:</label>
            </td>
            <td>
                <asp:DropDownList ID="cbType" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <label for="txtInactiveSince">Inactive since (days):</label>
            </td>
            <td>
                <asp:TextBox ID="txtInactiveSince" runat="server" Text="365"></asp:TextBox></td>
        </tr>
    </table>

    <p>
        <asp:Button ID="btDeleteProfiles" runat="server" Text="Delete profiles" OnClick="btDeleteProfiles_Click" />
    </p>    
    
    
</asp:Content>

