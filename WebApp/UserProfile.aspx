<%@ Page Language="C#" MasterPageFile="~/BC.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs"
    Inherits="SEOToolSet.WebApp.UserProfile" %>

<%@ MasterType TypeName="SEOToolSet.WebApp.BC" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<%@ Register TagPrefix="user" TagName="PageTitle" Src="PageTitle.ascx" %>
<%@ Register Src="Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentArea" runat="server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            var changePasswordCommand = $("#ctl00_contentArea_UserFormView_ChangePasswordCommand");
            $(".Password").css("display", "none");
            changePasswordCommand.click(function() {
                $(".Password").toggle("slow");
            });
        });
    </script>

    <uc1:IncludeFile ID="IncludeFile9" TypeOfFile="Css" FilePath="~/css/DataEntry.css"
        runat="server" />
    <uc1:IncludeFile ID="IncludeFile1" TypeOfFile="Css" FilePath="~/css/UserProfile.css"
        runat="server" />
    <user:PageTitle PanelContainerVisible="false" ID="PageHeading" runat="server" PageTitleText="<%$ Resources:CommonTerms, UserProfile %>">
    </user:PageTitle>
    <div class="WizardWrapper" style="width: 600px;">
        <h2 class="SectionTitle">
            <asp:Label ID="UserProfileHeading" runat="server" Text="<%$ Resources:CommonTerms, EditUserInformation %>"></asp:Label>
        </h2>
        <cc1:RoundPanel ID="RoundPanel1" runat="server">
            <div class="Step">
                <asp:FormView ID="UserFormView" runat="server" DataKeyNames="Id" DataSourceID="odsUser"
                    DefaultMode="Edit" Width="100%">
                    <EditItemTemplate>
                        <asp:ValidationSummary ID="UserValidationSummary" runat="server" />
                        <div class="FormPanel">
                            <div class="FormCSS">
                                <asp:HiddenField ID="Id" runat="server" Value='<%# Bind("Id") %>' />
                                <div class="Field OneLine">
                                    <label>
                                        <asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:CommonTerms, Login %>"></asp:Localize>&nbsp;</label>
                                    <asp:TextBox CssClass="FormText" ID="LoginTextBox" runat="server" Text='<%# Bind("Login") %>'
                                        Enabled="False" />
                                </div>
                                <div class="Field OneLine Password" style="display: none;">
                                    <label>
                                        <asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:CommonTerms, Password %>"></asp:Localize>
                                        &nbsp;</label>
                                    <asp:TextBox CssClass="FormText" ID="PasswordTextBox" runat="server" TextMode="Password" />
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="PasswordTextBox"
                                        Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, PasswordRequired %>"
                                        SetFocusOnError="True" ValidationGroup="Advanced">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="Field OneLine Password" style="display: none;">
                                    <label>
                                        <asp:Localize ID="Localize3" runat="server" Text="<%$ Resources:CommonTerms, RetypePassword %>"></asp:Localize>
                                        &nbsp;</label>
                                    <asp:TextBox CssClass="FormText" ID="RetypePasswordTextBox" runat="server" TextMode="Password" />
                                    <asp:CompareValidator ID="PasswordComparisonValidation" runat="server" ControlToCompare="PasswordTextBox"
                                        ControlToValidate="RetypePasswordTextBox" Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, PasswordNotCoincide %>"
                                        SetFocusOnError="True" ValidationGroup="Advanced">*</asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RetypePasswordRequired" runat="server" ControlToValidate="RetypePasswordTextBox"
                                        Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, RetypePasswordRequired %>"
                                        SetFocusOnError="True" ValidationGroup="Advanced">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="DoClear">
                                </div>
                                <div id="ChangePasswordCommand" runat="server" class="ChangePasswordCommand">
                                    <asp:HyperLink ID="HyperlynkChangePassword" CssClass="MiniCommand" runat="server"
                                        NavigateUrl="javascript:void(0);">
                                        <asp:Localize ID="Localize18" runat="server" Text="<%$ Resources:CommonTerms, ChangePassword %>"></asp:Localize>
                                    </asp:HyperLink>
                                </div>
                                <div class="Field OneLine">
                                    <label>
                                        <asp:Localize ID="Localize4" runat="server" Text="<%$ Resources:CommonTerms, UserRole %>"></asp:Localize>
                                        &nbsp;</label>
                                    <asp:DropDownList ID="UserRoleDropDownList" runat="server" AppendDataBoundItems="True"
                                        DataSourceID="odsUserRole" DataTextField="Name" DataValueField="Id" SelectedValue='<%# Eval("UserRole.Id") ?? -1 %>'>
                                        <asp:ListItem Value="-1" Text="<%$ Resources:CommonTerms, Choose %>"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="UserRoleRequired" runat="server" ControlToValidate="UserRoleDropDownList"
                                        Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, UserRoleRequired %>"
                                        InitialValue="-1" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="Separator">
                            </div>
                            <div class="FormCSS">
                                <div class="Field OneLine">
                                    <label>
                                        <asp:Localize ID="Localize5" runat="server" Text="<%$ Resources:CommonTerms, FirstName %>"></asp:Localize>
                                        &nbsp;</label>
                                    <asp:TextBox CssClass="FormText" ID="FirstNameTextBox" runat="server" Text='<%# Bind("FirstName") %>' />
                                    <asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" ControlToValidate="FirstNameTextBox"
                                        Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, FirstNameRequired %>"
                                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="Field OneLine">
                                    <label>
                                        <asp:Localize ID="Localize6" runat="server" Text="<%$ Resources:CommonTerms, LastName %>"></asp:Localize>
                                        &nbsp;</label>
                                    <asp:TextBox CssClass="FormText" ID="LastNameTextBox" runat="server" Text='<%# Bind("LastName") %>' />
                                    <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" ControlToValidate="LastNameTextBox"
                                        Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, LastNameRequired %>"
                                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="Field OneLine">
                                    <label>
                                        <asp:Localize ID="Localize7" runat="server" Text="<%$ Resources:CommonTerms, Email %>"></asp:Localize>
                                        &nbsp;</label>
                                    <asp:TextBox CssClass="FormText" ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' />
                                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="EmailTextBox"
                                        Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, EmailRequired %>"
                                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="Field OneLine">
                                    <label>
                                        <asp:Localize ID="Localize8" runat="server" Text="<%$ Resources:CommonTerms, Country %>"></asp:Localize>
                                        &nbsp;</label>
                                    <asp:DropDownList ID="CountryDropDownList" runat="server" AppendDataBoundItems="True"
                                        DataSourceID="odsCountry" DataTextField="Name" DataValueField="Id" SelectedValue='<%# Eval("Country.Id") ?? -1 %>'>
                                        <asp:ListItem Value="-1" Text="<%$ Resources:CommonTerms, Choose %>"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="Field OneLine">
                                    <label>
                                        <asp:Localize ID="Localize9" runat="server" Text="<%$ Resources:CommonTerms, Address %>"></asp:Localize>
                                        &nbsp;</label>
                                    <asp:TextBox CssClass="FormText" ID="Address1TextBox" runat="server" Text='<%# Bind("Address1") %>' />
                                </div>
                                <div class="Field OneLine">
                                    <label>
                                        &nbsp;
                                    </label>
                                    <asp:TextBox CssClass="FormText" ID="Address2TextBox" runat="server" Text='<%# Bind("Address2") %>' />
                                </div>
                                <div class="Field OneLine">
                                    <label>
                                        <asp:Localize ID="Localize10" runat="server" Text="<%$ Resources:CommonTerms, City %>"></asp:Localize>
                                        &nbsp;</label>
                                    <asp:TextBox CssClass="FormText" ID="CityTownTextBox" runat="server" Text='<%# Bind("CityTown") %>' />
                                </div>
                                <div class="Field OneLine">
                                    <label>
                                        <asp:Localize ID="Localize11" runat="server" Text="<%$ Resources:CommonTerms, State %>"></asp:Localize>
                                        &nbsp;</label>
                                    <asp:TextBox CssClass="FormText" ID="StateTextBox" runat="server" Text='<%# Bind("State") %>'
                                        Style="width: 65px;" />
                                    <label class="ShortLabel">
                                        <asp:Localize ID="Localize12" runat="server" Text="<%$ Resources:CommonTerms, Zip %>"></asp:Localize>
                                        &nbsp;</label>
                                    <asp:TextBox CssClass="FormText" ID="ZipTextBox" runat="server" Text='<%# Bind("Zip") %>'
                                        Style="width: 55px;" />
                                </div>
                                <div class="Field OneLine">
                                    <label>
                                        <asp:Localize ID="Localize13" runat="server" Text="<%$ Resources:CommonTerms, Telephone %>"></asp:Localize>
                                        &nbsp;</label>
                                    <asp:TextBox CssClass="FormText" ID="TelephoneTextBox" runat="server" Text='<%# Bind("Telephone") %>' />
                                </div>
                            </div>
                            <div class="DoClear">
                            </div>
                            <div class="WizardActions" style="width: 210px;">
                                <ul>
                                    <li>
                                        <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                            CssClass="LinkCommandRound">
                                            <asp:Label ID="UpdateLabel" runat="server" Text="<%$ Resources:CommonTerms, Save %>"></asp:Label>
                                        </asp:LinkButton>
                                    </li>
                                    <li>
                                        <asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CssClass="LinkCommandRound"
                                            CommandName="Cancel" OnClick="UpdateCancelButton_Click">
                                            <asp:Label ID="CancelLabel" runat="server" Text="<%$ Resources:CommonTerms, Cancel %>"></asp:Label>
                                        </asp:LinkButton>
                                    </li>
                                </ul>
                            </div>
                    </EditItemTemplate>
                </asp:FormView>
            </div>
            <asp:ObjectDataSource ID="odsUser" runat="server" SelectMethod="GetUserById" TypeName="SEOToolSet.Providers.SEOMembershipManager"
                UpdateMethod="UpdateUser" OnUpdating="odsUser_Updating" OnUpdated="odsUser_Updated">
                <UpdateParameters>
                    <asp:Parameter Name="id" Type="Int32" />
                    <asp:Parameter Name="firstName" Type="String" />
                    <asp:Parameter Name="lastName" Type="String" />
                    <asp:Parameter Name="email" Type="String" />
                    <asp:Parameter Name="address1" Type="String" />
                    <asp:Parameter Name="address2" Type="String" />
                    <asp:Parameter Name="cityTown" Type="String" />
                    <asp:Parameter Name="state" Type="String" />
                    <asp:Parameter Name="zip" Type="String" />
                    <asp:Parameter Name="telephone" Type="String" />
                    <asp:Parameter Name="login" Type="String" />
                    <asp:Parameter Name="password" Type="String" />
                    <asp:Parameter Name="passwordQuestion" Type="String" />
                    <asp:Parameter Name="passwordAnswer" Type="String" />
                    <asp:Parameter Name="lastFailedLoginDate" Type="DateTime" />
                    <asp:Parameter Name="lastActivityDate" Type="DateTime" />
                    <asp:Parameter Name="lastPasswordChangedDate" Type="DateTime" />
                    <asp:Parameter Name="isLockedOut" Type="Boolean" />
                    <asp:Parameter Name="lockedOutDate" Type="DateTime" />
                    <asp:Parameter Name="failedPasswordAttemptCount" Type="Int32" />
                    <asp:Parameter Name="expirationDate" Type="DateTime" />
                    <asp:Parameter Name="enabled" Type="Boolean" />
                    <asp:Parameter Name="account" Type="Object" />
                    <asp:Parameter Name="country" Type="Object" />
                    <asp:Parameter Name="userRole" Type="Object" />
                    <asp:Parameter Name="lastLoginDate" Type="DateTime" />
                </UpdateParameters>
                <SelectParameters>
                    <asp:QueryStringParameter ConvertEmptyStringToNull="False" DefaultValue="-1" Name="id"
                        QueryStringField="IdUser" Type="Int32" Direction="InputOutput" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="odsUserRole" runat="server" SelectMethod="GetRoles" TypeName="SEOToolSet.Providers.SEORolesManager">
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="odsCountry" runat="server" SelectMethod="GetCountries"
                TypeName="SEOToolSet.Providers.SEOMembershipManager"></asp:ObjectDataSource>
        </cc1:RoundPanel>
    </div>
</asp:Content>
