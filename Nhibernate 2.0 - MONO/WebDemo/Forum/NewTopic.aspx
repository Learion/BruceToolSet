<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true"
    Inherits="Forum_NewTopic" Title="New topic" ValidateRequest="false" Codebehind="NewTopic.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="NewMessage" Src="~/Controls/NewMessage.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="Server">
    
    <h1>New topic</h1>

    <uc:NewMessage ID="newMessage" runat="server" />
    
    <p>
        <asp:Button ID="btSubmit" runat="server" Text="Submit" OnClick="btSubmit_Click" />
        <asp:Button ID="btCancel" runat="server" Text="Cancel" CausesValidation="False" OnClick="btCancel_Click" />
    </p>
    
</asp:Content>
