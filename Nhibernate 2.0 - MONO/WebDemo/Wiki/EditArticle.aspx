<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true"
    Inherits="Wiki_EditArticle" Title="Edit article" ValidateRequest="false" Codebehind="EditArticle.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="Server">

    <h1>Edit article</h1>

    <div>
        <table>
            <tr>
                <td>
                    <label for="txtName">Name:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" MaxLength="100" Enabled="false"></asp:TextBox>
                </td>
            </tr>
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
                    <label for="chkEnabled">Status:</label>
                </td>
                <td>
                    <asp:CheckBox ID="chkEnabled" runat="server" Text="Enabled" /> <asp:CheckBox ID="chkApproved" runat="server" Text="Approved" />
                </td>
            </tr>
            <tr>
                <td><label for="txtBody">XHTML content *:</label></td>
                <td>
                    <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Columns="60" Rows="20" Wrap="false"></asp:TextBox>
                </td>
            </tr>
        </table>
        <small>* Must be a valid XHTML snippet without any of these tags: html, head, body, script.</small>


        <div id="sectionAttachments" runat="server">
            <h3>Attachments</h3>
            <p>To use an attachment inside the article content simply use the filename inside an anchor tag ('a') or an image tag ('img').
            For example: <br />
                <code>&lt;img src="Architecture.png" alt="Image description" /&gt;</code> 
                <br />
                or 
                <br />
                <code>&lt;a href="Architecture.png"&gt;Link text&lt;/a&gt;</code>
            </p>
           
            <asp:Repeater ID="listAttachments" runat="server" OnItemCommand="listAttachments_ItemCommand">
                <HeaderTemplate>
                    <table class="datatable">
                        <thead>
                            <tr>
                                <th>
                                    Url</th>
                                <th>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# GetAttachmentPlaceHolder((string)Container.DataItem) %>
                        </td>
                        <td>
                            <asp:LinkButton CssClass="deleteitem" OnClientClick="return confirm('Are you sure to delete the attach?');"
                                ID="LinkButton2" runat="server" CommandName="delete" CommandArgument='<%# Container.DataItem %>' >Delete</asp:LinkButton>
                            <a class="downloaditem" href="<%# GetAttachmentUrl((string)Container.DataItem) %>">Download</a>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody> </table>
                </FooterTemplate>
            </asp:Repeater>

            <p>
                <asp:FileUpload ID="uploadAttach" runat="server" />
                <asp:Button ID="btUpload" runat="server" Text="Upload" OnClick="btUpload_Click" />
            </p>
        </div>

        <h3>Links to other articles</h3>
        <p>You can link other articles using this syntax:</p>
        <code>&lt;a href="eucalypto:ARTICLENAME"&gt;Article title&lt;/a&gt;</code>
        <p>Where <code>ARTICLENAME</code> is the <b>Name</b> of article to link.</p>
        
        <h3>Table of contents (TOC)</h3>
        <label for="chkCreateTOC">Options:</label>
        <asp:CheckBox ID="chkCreateTOC" runat="server" Text="Create Table of Contents" Checked="true" />
        <p>You can insert an automatic generated TOC writing this code:</p>
        <code>&lt;div id="TOC"&gt;&lt;/div&gt;</code>
        <p>When Eucalypto render the article will insert inside the div the content of the TOC generated using the header tags (h1, h2, h3, ...).</p>

        <p>
            <asp:CheckBox ID="chkBackup" runat="server" Text="Create backup version" />
            <br />
            <asp:Button ID="btDelete" runat="server" Text="Delete" OnClick="btDelete_Click" OnClientClick="return confirm('Are you sure to delete the article?');" />
            <asp:Button ID="btSave" runat="server" Text="Save" OnClick="btSave_Click" />
            <asp:Button ID="btCancel" runat="server" Text="Cancel" OnClick="btCancel_Click" CausesValidation="false" />
        </p>
    </div>

    
</asp:Content>
