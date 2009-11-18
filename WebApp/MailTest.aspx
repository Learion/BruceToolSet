<%@ Page Title="" Language="C#" MasterPageFile="~/BC.Master" AutoEventWireup="true"
    CodeBehind="MailTest.aspx.cs" Inherits="SEOToolSet.WebApp.MailTest" %>

<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<%@ Register Src="Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentArea" runat="server">
    <uc1:IncludeFile ID="IncludeFile9" TypeOfFile="Css" FilePath="~/css/DataEntry.css"
        runat="server" />
    <cc1:RoundPanel ID="RoundPanel1" runat="server">
        <div class="FormPanel" style="width:370px; margin-left:auto; margin-right:auto;">
            <asp:Literal ID="ResultLiteral" runat="server"></asp:Literal>
            <div class="FormCSS">
                <div class="Field OneLine">
                    <label>
                        <asp:Literal ID="MailLabel" runat="server" Text="Mail"></asp:Literal></label>
                    <asp:TextBox ID="MailTextBox" runat="server" CssClass="FormText"></asp:TextBox>
                </div>
                <div class="Field OneLine">
                    <label>
                        <asp:Literal ID="SubjectLabel" runat="server" Text="Subject"></asp:Literal>
                    </label>
                    <asp:TextBox ID="SubjectTextBox" runat="server" CssClass="FormText"></asp:TextBox>
                </div>
                <div class="Field OneLine">
                    <label>
                        <asp:Literal ID="ContentLabel" runat="server" Text="Content"></asp:Literal>
                    </label>
                    <asp:TextBox ID="ContentTextBox" runat="server" Rows="4" TextMode="MultiLine" CssClass="FormText"></asp:TextBox>
                </div>
            </div>
            <div class="DoClear">
            </div>
            <div class="WizardActions CenterWrapper">
                <ul>
                    <li>
                        <asp:LinkButton ID="SubmitLinkButton" runat="server" OnClick="SubmitLinkButton_Click"
                            CssClass="LinkCommandRound"><span>Submit</span></asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton ID="ResetLinkButton" runat="server" CssClass="LinkCommandRound"><span>Reset</span></asp:LinkButton>
                    </li>
                </ul>
            </div>
        </div>
    </cc1:RoundPanel>
</asp:Content>
