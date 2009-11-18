<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true"
    Inherits="Forum_ViewTopic" Title="View topic" Codebehind="ViewTopic.aspx.cs" %>

<%@ Register TagPrefix="uc" TagName="ViewTopic" Src="~/Controls/ViewTopic.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder" runat="Server">
    
    <h1>Topic: <span id="lblTopic" runat="server"></span></h1>

    <p>
        <a id="lnkForum" runat="server" >Back to forum</a>
    </p>
    
    <uc:ViewTopic ID="viewTopic" runat="server" />

</asp:Content>
