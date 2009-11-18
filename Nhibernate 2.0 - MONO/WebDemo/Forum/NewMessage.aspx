<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true"
    Inherits="Forum_NewMessage" Title="New message"  ValidateRequest="false" Codebehind="NewMessage.aspx.cs" %>
<%@ Register TagPrefix="uc" TagName="NewMessage" Src="~/Controls/NewMessage.ascx" %>
<%@ Register TagPrefix="uc" TagName="ViewMessage" Src="~/Controls/ViewMessage.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="Server">

    <h1>New message</h1>

    <uc:ViewMessage ID="viewParentMessage" runat="server" ReplyLinkVisible="false" DeleteLinkVisible="false" />

    <br />

    <uc:NewMessage ID="newMessage" runat="server" />
    
    <p>
        <asp:Button ID="btSubmit" runat="server" Text="Submit" OnClick="btSubmit_Click" />
        <asp:Button ID="btCancel" runat="server" Text="Cancel" OnClick="btCancel_Click" CausesValidation="False" />
    </p>
</asp:Content>
