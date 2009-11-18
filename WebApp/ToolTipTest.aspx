<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/BC.Master" CodeBehind="ToolTipTest.aspx.cs"
    Inherits="SEOToolSet.WebApp.ToolTipTest" %>

<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<%@ Register TagPrefix="user" TagName="PageTitle" Src="PageTitle.ascx" %>
<%@ Register src="Controls/IncludeFile.ascx" tagname="IncludeFile" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentArea" runat="server">
    <uc1:IncludeFile ID="IncludeFile11" TypeOfFile="Css" FilePath="~/scripts/Controls/R3M.QuickTip/QuickTip.css" runat="server" />
    <uc1:IncludeFile ID="IncludeFile1" TypeOfFile="Javascript" FilePath="~/scripts/Controls/R3M.QuickTip/jQuery.QuickTip.js" runat="server" />
    <user:PageTitle PageTitleText="QuickToolTip Test" PageDescription="Quick Tooltip text"
        ID="WebUserControl0" runat="server"></user:PageTitle>
    <cc1:RoundPanel ID="RoundPanel1" runat="server">
        <div class="FormCSS">
            <div class="FormGroup">
                <div class="Legend">
                    <h2>
                        Account Information
                    </h2>
                </div>
                <div class="Field OneLine">
                    <asp:RequiredFieldValidator CssClass="Validator" ID="RequiredFieldValidator1" ErrorMessage="You Wont believe how require this element is" Text="This field so required!!!!" ControlToValidate="_account"
                        Display="Dynamic" ValidationGroup="TestGroup" fo runat="server" ></asp:RequiredFieldValidator>
                    <label>
                        Account Information*</label>
                    <input id="_account" runat="server" type="text" class="FormText Required QuickTip"
                        title="The account name identifies your account in the SEOtoolSet, and must be unique. Examples: your company name, your full name or other." />
                </div>
                <div class="Field OneLine">
                    <label>
                        Street Address*</label>
                    <input type="text" class="FormText Required QuickTip" title="This is a default tooltip that will show some more info in that regard." />
                </div>
                <div class="Field">
                    <div class="CenterWrapper">
                        <ul>
                            <li>
                                <cc1:LinkButtonRound ValidationGroup="TestGroup" ID="LinkButtonRound1" Text="Save"
                                    runat="server"></cc1:LinkButtonRound>
                            </li>
                            <li>
                                <cc1:LinkButtonRound ID="LinkButtonRound2" Text="Cancel" runat="server"></cc1:LinkButtonRound>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="DoClear">
            </div>
        </div>
    </cc1:RoundPanel>
    <script type="text/javascript">        
        $(function () {                                
            $('.QuickTip').quickTips();
        });
    </script>
</asp:Content>
