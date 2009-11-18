<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true"
    Inherits="Wiki_NewArticle" Title="New article" ValidateRequest="false" Codebehind="NewArticle.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="Server">

    <h1>New article</h1>
    
    <div>
        <table>
            <tr>
                <td>
                    <label for="txtName">Name:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" MaxLength="100"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validatorName" runat="server" ControlToValidate="txtName"
                        ErrorMessage="Name field is required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtTitle">Title:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server" MaxLength="200" Columns="50" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validatorTitle" runat="server" ControlToValidate="txtTitle"
                         ErrorMessage="Title field is required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtDescription">Description:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" MaxLength="500" Columns="50" TextMode="MultiLine" Rows="3"></asp:TextBox>
                </td>
            </tr>
        </table>
        
        <p>After you have created the article you can edit its content (xhtml, attachments, toc, ...).<br />
        <span id="sectionApprove" runat="server">Before other users can see your article the web site editor must approve it.</span></p>
        
        <p>
            <asp:Button ID="btSave" runat="server" Text="Save" OnClick="btSave_Click" />
            <asp:Button ID="btCancel" runat="server" Text="Cancel" OnClick="btCancel_Click" CausesValidation="false" />
        </p>
    </div>


</asp:Content>
