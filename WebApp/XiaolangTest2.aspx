<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PageBase.Master" CodeBehind="XiaolangTest2.aspx.cs"
    Inherits="SEOToolSet.WebApp.XiaolangTest2" %>

<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="contentArea">
    <div>
        <asp:LinkButton ID="Button1" item_name="de" runat="server" Text="Button"></asp:LinkButton>
        <br />
        <br />
        <cc1:ConfirmMessageExt ID="ConfirmMessageExt1" runat="server" CancelButtonText="cancel"
            ConfirmMessage="1111" ConfirmTitle="aaaa" OkButtonText="ok" useConfirmMessageFromElement="true" Selector="#ctl00_contentArea_Button1" useItemNameInElement="true" />
    </div>
</asp:Content>
