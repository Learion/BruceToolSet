<%@ Page Language="C#" MasterPageFile="~/BC.Master" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs"
    Inherits="SEOToolSet.WebApp.LoginPage" Title="Login" %>

<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<%@ Register TagPrefix="user" TagName="PageTitle" Src="PageTitle.ascx" %>
<%@ Register Src="Controls/ToolTip.ascx" TagName="ToolTip" TagPrefix="uc3" %>
<%@ Register Src="Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc2" %>
<%@ Register Src="Controls/sIFR.ascx" TagName="sIFR" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentArea" runat="server">
    <uc3:ToolTip ID="ToolTip1" runat="server" Selector=".HasTitle" ToShow="300" FadeIn="'fast'"
        FadeOut="'fast'" ShowTitle="false" />
    <uc2:IncludeFile ID="includeFile1" runat="server" FilePath="~/css/Login.css" TypeOfFile="Css" />

    <script type="text/javascript">
        $(function() {
            var username = $.byId('<%= Login1.ClientID %>_UserName');
            var password = $.byId('<%= Login1.ClientID %>_Password');
            var btnName = '<%= Login1.ClientID %>_LinkButtonLogin'.replace(/_/g, '$');
            
            var chkRememberMe = $.byId('<%= Login1.ClientID %>_RememberMe');

            username.enterPressed(function() {
                if (username.val() != '') { password.focus() } else { username.focus(); }
            });
            
            chkRememberMe.enterPressed(function() {
                _raiseLogin();
            });
            
            password.enterPressed(function() {
                _raiseLogin();
            });
            
            function _raiseLogin() {
                if (typeof (WebForm_DoPostBackWithOptions) != 'undefined' && typeof (WebForm_PostBackOptions) != 'undefined') {
                    WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions(btnName, "", true, "Login1", "", false, true));
                }
                else {
                    WebForm_DoPostback(btnName, '', null, false, true, false, true, 'Login1');
                }
            }

        });
    </script>

    <%--<uc4:sIFR ID="sIFR1" runat="server" Selector="h2.sIFR" SwfFontToRender="~/scripts/Controls/sIFR/fonts/font_ps.swf" />--%>
    <!-- Control Used for the Page Title -->
    <cc1:RoundPanel ID="RoundPanel1" runat="server" DiscardBottom="False" DiscardTop="False">
        <div class="LoginSection">
            <div class="PromotionalMessage">
                <h2 class="sIFR">
                    <asp:Localize ID="LoginWelcomeMessage" runat="server" meta:resourcekey="LoginWelcomeMessage"></asp:Localize></h2>
                <p>
                    <asp:Localize ID="LoginComment" runat="server" meta:resourcekey="LoginComment"></asp:Localize>
                </p>
            </div>
            <div class="LoginBlock">
                <asp:Login ID="Login1" runat="server" OnLoggedIn="Login1_OnLoggedIn" meta:resourcekey="LoginForm">
                    <LayoutTemplate>
                        <div class="FormCSS">
                            <div class="Field">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" Text="<%$ Resources:CommonTerms, Login %>"></asp:Label>
                                <asp:TextBox CssClass="FormText" ID="UserName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="HasTitle" ID="UserNameRequired" runat="server"
                                    ControlToValidate="UserName" ErrorMessage="<%$ Resources:CommonTerms, LoginNameRequired %>"
                                    ValidationGroup="Login1" Display="Dynamic">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="Field">
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" Text="<%$ Resources:CommonTerms, Password %>"></asp:Label>
                                <asp:TextBox CssClass="FormText" ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="HasTitle" ID="PasswordRequired" runat="server"
                                    ControlToValidate="Password" ErrorMessage="<%$ Resources:CommonTerms, PasswordRequired %>"
                                    ValidationGroup="Login1" Display="Dynamic">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="Field">
                                <label>
                                    <asp:CheckBox ID="RememberMe" CssClass="FormCheck" runat="server" />
                                    <span>
                                        <asp:Localize ID="RememberMeLabel" runat="server" meta:resourcekey="RememberMe"></asp:Localize>
                                    </span>
                                </label>
                            </div>
                            <div class="Field">
                                <span class="NotificationMessage Error">
                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                    <asp:ValidationSummary ID="LoginValidationSummary" runat="server" ValidationGroup="Login1" />
                                </span>
                            </div>
                            <div class="ToolBar">
                                <div id="divBtnLogin" class="ToolBarItem Right">
                                    <asp:LinkButton ValidationGroup="Login1" ID="LinkButtonLogin" CssClass="LinkCommandRound"
                                        CommandName="Login" runat="server">
                                        <span>
                                            <asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:CommonTerms, Login %>"></asp:Localize>
                                        </span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="Field Separator">
                            </div>
                            <div class="Field">
                                <a class="InternalLink" href="SignUp.aspx"><span>
                                    <asp:Localize ID="NotAMemberYet" runat="server" meta:resourcekey="NotAMemberYet"></asp:Localize>
                                </span></a>
                            </div>
                            <div class="Field">
                                <a class="InternalLink" href="ForgotPassword.aspx"><span>
                                    <asp:Localize ID="ForgotMessage" runat="server" meta:resourcekey="ForgotMessage"></asp:Localize>
                                </span></a>
                            </div>
                        </div>
                        <div class="DoClear">
                        </div>
                    </LayoutTemplate>
                </asp:Login>
            </div>
        </div>
    </cc1:RoundPanel>
</asp:Content>
