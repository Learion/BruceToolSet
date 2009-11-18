<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true"
    Inherits="News_EditItem" Title="Edit news item" ValidateRequest="false" Codebehind="EditItem.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="Server">

    <h1>Edit news item</h1>

    <div>
        <table>
            <tr>
                <td>
                    <label for="txtTitle">Title:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server" MaxLength="100" Columns="50" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validatorTitle" runat="server" ControlToValidate="txtTitle"
                         ErrorMessage="Title field is required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtDescription">Description:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" MaxLength="300" Columns="50" TextMode="MultiLine" Rows="3"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtOwner">Owner:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtOwner" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtAuthor">Author:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtAuthor" runat="server" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtURL">URL:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtURL" runat="server" MaxLength="300" Columns="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtURL"
                         ErrorMessage="Url field is required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtURLName">URL Name:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtURLName" runat="server" MaxLength="100"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtURLName"
                         ErrorMessage="Url name field is required"></asp:RequiredFieldValidator>
                </td>
            </tr>            
            <tr>
                <td>
                    <label for="txtNewsDate">News date (yyyymmdd):</label>
                </td>
                <td>
                    <asp:TextBox ID="txtNewsDate" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNewsDate"
                         ErrorMessage="Date field is required"></asp:RequiredFieldValidator>
                </td>
            </tr>                
        </table>

        <p>
            <asp:Button ID="btSave" runat="server" Text="Save" OnClick="btSave_Click" />
            <asp:Button ID="btCancel" runat="server" Text="Cancel" OnClick="btCancel_Click" CausesValidation="false" />
        </p>
    </div>

    
</asp:Content>
