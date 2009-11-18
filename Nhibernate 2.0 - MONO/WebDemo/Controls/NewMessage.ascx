<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_NewMessage" Codebehind="NewMessage.ascx.cs" %>

<table class="formtable">
    <tr>
        <td><label for="txtSubject">Subject:</label></td>
        <td>
            <asp:TextBox ID="txtSubject" runat="server" Columns="60" MaxLength="100"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Subject field is required" ControlToValidate="txtSubject"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td><label for="txtBody">Text:</label></td>
        <td>
            <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Columns="60" Rows="20"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Text field is required" ControlToValidate="txtBody"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr runat="server" id="sectionAttach1">
        <td><label for="fAttachment">Attachment*:</label></td>
        <td>
            <asp:FileUpload ID="fAttachment" runat="server" />
        </td>
    </tr>
</table>

<small runat="server" id="sectionAttach2"> * Attachment - maximum file size: 
     <span id="lblMaxAttachSize" runat="server"></span> Kb, 
     accepted extensions: 
     <span id="lblAcceptedExtensions" runat="server"></span>
</small>
