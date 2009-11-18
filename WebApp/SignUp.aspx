<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="SEOToolSet.WebApp.SignUp" %>

<%@ Register Src="Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc1" %>
<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<%@ Register TagPrefix="user" TagName="PageTitle" Src="PageTitle.ascx" %>
<%@ Register TagPrefix="ddlb" Assembly="OptionDropDownList" Namespace="OptionDropDownList" %>
<%@ Register Src="Controls/Popup.ascx" TagName="Popup" TagPrefix="uc3" %>
<%@ Register Src="Controls/CostConversion.ascx" TagName="CostConversion" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Subscription Process</title>
</head>
<body>
    <form id="aspnetForm" class="MainForm" runat="server">
    <uc1:IncludeFile ID="IncludeFile7" TypeOfFile="CSS" FilePath="~/css/app.css" runat="server" />
    <uc1:IncludeFile ID="IncludeFile8" TypeOfFile="Javascript" FilePath="~/scripts/jquery.js"
        runat="server" />
    <uc1:IncludeFile ID="IncludeFileJavascriptMessages" TypeOfFile="Javascript" FilePath="~/Handler/RetrieveResource.ashx"
        runat="server" />
    <uc1:IncludeFile ID="IncludeFile11" TypeOfFile="Javascript" FilePath="~/scripts/common.js"
        runat="server" />
    <uc1:IncludeFile ID="IncludeFile12" TypeOfFile="CSS" FilePath="~/scripts/Controls/R3M.RoundPanel/round_ctr.css"
        runat="server" />
    <uc1:IncludeFile ID="IncludeFile13" TypeOfFile="CSS" FilePath="~/scripts/Controls/R3M.RoundButton/RoundButton.css"
        runat="server" />
    <uc1:IncludeFile ID="IncludeFile1" TypeOfFile="Css" FilePath="~/scripts/jQuery.UI/themes/default/ui.all.css"
        runat="server" />
    <uc1:IncludeFile ID="IncludeFile2" TypeOfFile="Css" FilePath="~/scripts/jQuery.UI/themes/ui.theme.css"
        runat="server" />
    <uc1:IncludeFile ID="IncludeFile3" TypeOfFile="Css" FilePath="~/css/DataEntry.css"
        runat="server" />
    <uc1:IncludeFile ID="IncludeFile5" TypeOfFile="Javascript" FilePath="~/scripts/jQuery.UI/jquery.ui.all.js"
        runat="server" />
    <uc1:IncludeFile ID="IncludeFile6" FilePath="~/css/reset-fonts.css" TypeOfFile="CSS"
        runat="server" />
    <uc1:IncludeFile ID="IncludeFile4" TypeOfFile="Css" FilePath="~/scripts/Controls/R3M.QuickTip/QuickTip.css"
        runat="server" />
    <uc1:IncludeFile ID="IncludeFile9" TypeOfFile="Javascript" FilePath="~/scripts/Controls/R3M.QuickTip/jQuery.QuickTip.js"
        runat="server" />
    <uc1:IncludeFile ID="IncludeFile10" TypeOfFile="Css" FilePath="~/css/SignUp.css"
        runat="server" />
    <uc3:Popup ID="Popup1" runat="server" />
    <cc1:WebMonoFixScripts ID="WebMonoFixScripts1" runat="server" />
    <div class="mc_outer">
        <div id='mc_container'>
            <div id="bd">
                <div class="wrapper">
                    <div style="margin-right: auto; margin-left: auto; width: 750px;">
                        <asp:Wizard ID="SubscriptionWizard" DisplaySideBar="False" runat="server" EnableTheming="False"
                            OnActiveStepChanged="SubscriptionWizard_ActiveStepChanged">
                            <WizardSteps>
                                <asp:WizardStep StepType="Start" ID="CreateSubscriptionStep" AllowReturn="False"
                                    EnableTheming="False">
                                    <user:PageTitle ID="PageHeading" runat="server" PageTitleText="Subscribe" PageDescription="Sign up to use the SEOToolSet&reg;"
                                        HelpContainerVisible="false" RenderRoundPanelStyles="false"></user:PageTitle>
                                    <div class="WizardWrapper">
                                        <cc1:RoundPanel ID="RoundPanel1" runat="server" DiscardBottom="False" DiscardTop="False">
                                            <div class="WizardStep">
                                                <asp:FormView Width="100%" ID="CreateAccountFormView" runat="server" DataSourceID="odsGetAccountFromSession"
                                                    DefaultMode="Edit">
                                                    <EditItemTemplate>
                                                        <div class="FormPanel">
                                                            <div class="Legend">
                                                                <div style="float: right; line-height: 30px; color: #000000; font-size: x-small;">
                                                                    *Required fields</div>
                                                                <h2 style="width: 300px;">
                                                                    <asp:Label ID="CreateAccountHeading" runat="server" meta:resourcekey="CreateAccountHeadingLabel"></asp:Label>
                                                                </h2>
                                                            </div>
                                                            <div id="AccountInlineMessage">
                                                            </div>
                                                            <div class="FormCSS">
                                                                <div class="Field OneLine">
                                                                    <asp:RequiredFieldValidator ID="AccountNameRequired" runat="server" CssClass="Validator"
                                                                        ControlToValidate="NameCreateAccountInput" Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, NameRequired %>"></asp:RequiredFieldValidator>
                                                                    <label>
                                                                        <asp:Localize ID="Localize1" runat="server" Text='<%$ Resources:CommonTerms, AccountName %>'></asp:Localize>*</label>
                                                                    <asp:TextBox Width="350px" CssClass="FormText ActivationTrigger Required QuickTip"
                                                                        title="The account name identifies your account in the SEOToolSet, and must be unique. Examples: Your company name, your full name, or other."
                                                                        ID="NameCreateAccountInput" runat="server" Text='<%# Bind("Name") %>' MaxLength="64" />
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Street Address is required"
                                                                        ControlToValidate="CompanyAddressTextBox1" CssClass="Validator" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <label>
                                                                        <asp:Literal ID="Localize2" runat="server" Text="Street Address"></asp:Literal>*
                                                                    </label>
                                                                    <asp:TextBox CssClass="FormText Required QuickTip" title="A physical address is needed for the account record."
                                                                        ID="CompanyAddressTextBox1" runat="server" Text='<%# Bind("CompanyAddress1") %>'
                                                                        MaxLength="300" />
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <label>
                                                                        &nbsp;</label>
                                                                    <asp:TextBox CssClass="FormText" ID="CompanyAddressTextBox2" runat="server" Text='<%# Bind("CompanyAddress2") %>'
                                                                        MaxLength="300" />
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <asp:RequiredFieldValidator CssClass="Validator" ID="RequiredFieldValidator1" runat="server"
                                                                        ErrorMessage="The city is required" ControlToValidate="CompanyCityTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <label>
                                                                        <asp:Localize ID="Localize3" runat="server" Text="City"></asp:Localize>*</label>
                                                                    <asp:TextBox CssClass="FormText Required QuickTip" title="The Quick Help for the Street Address field above should remain visible while the cursor focus is on the City field."
                                                                        ID="CompanyCityTextBox" runat="server" Text='<%# Bind("CompanyCity") %>' MaxLength="100" />
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <label>
                                                                        <asp:Localize ID="Localize4" runat="server" Text="State or Province"></asp:Localize></label>
                                                                    <asp:TextBox Width="130px" CssClass="FormText QuickTip" title="Abbreviate your state or province. Examples: CA, NY, BC."
                                                                        ID="CompanyStateTextBox" runat="server" MaxLength="4" Text='<%# Bind("CompanyState") %>' />
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="Validator" runat="server"
                                                                        ErrorMessage="ZIP or Postal Code is required" ControlToValidate="CompanyZipTextBox"
                                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <label>
                                                                        <asp:Localize ID="Localize5" runat="server" Text="<%$ Resources:CommonTerms, Zip %>"></asp:Localize>*</label>
                                                                    <asp:TextBox Width="150px" CssClass="FormText Required QuickTip" title="Enter the ZIP code or postal code for the account’s address. Examples: 93065, 10021-1425, H3Z 2Y7, 75 001."
                                                                        ID="CompanyZipTextBox" runat="server" MaxLength="20" Text='<%# Bind("CompanyZip") %>' />
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Country is required"
                                                                        Display="Dynamic" CssClass="Validator" ControlToValidate="CountryDropDownList"
                                                                        InitialValue="-1"></asp:RequiredFieldValidator>
                                                                    <label>
                                                                        <asp:Localize ID="Localize49" runat="server" Text="<%$ Resources:CommonTerms, Country %>"></asp:Localize>*</label>
                                                                    <asp:DropDownList ID="CountryDropDownList" runat="server" AppendDataBoundItems="True"
                                                                        DataSourceID="odsCountries" DataTextField="Name" DataValueField="Id" SelectedValue='<%# Bind("CompanyIdCountry") %>'
                                                                        CssClass="Required">
                                                                        <asp:ListItem Value="-1" Text="<%$ Resources:CommonTerms, Choose %>"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <label>
                                                                        <asp:Localize ID="Localize6" runat="server" Text="<%$ Resources:CommonTerms, Telephone %>"></asp:Localize></label>
                                                                    <asp:TextBox CssClass="FormText QuickTip" title="A phone number for the account is helpful for contact purposes. Please enter a complete number with area code. Include country code if outside the U.S."
                                                                        ID="CompanyPhoneTextBox" runat="server" Text='<%# Bind("CompanyPhone") %>' MaxLength="25" />
                                                                </div>
                                                            </div>
                                                            <div class="Separator">
                                                            </div>
                                                            <div class="FormCSS">
                                                                <div class="Field OneLine">
                                                                    <label>
                                                                        <asp:Localize ID="Localize28" runat="server" Text="<%$ Resources:CommonTerms, SubscriptionLevel %>"></asp:Localize></label>
                                                                    <span>
                                                                        <cc1:CustomRepeater ID="CustomRepeaterSubscriptions" runat="server" DataSourceID="ocs">
                                                                            <ItemTemplate>
                                                                                <asp:RadioButton ID="subscriptionRadioButton" runat="server" Text='<%# String.Format("<strong>SEOToolSet&amp;reg; {0}</strong> -- ${1} USD per month", Eval("Name"), Eval("Price")) %>'
                                                                                    Checked='<%# (Int32)Eval("Id")==1 %>' price='<%# Eval("Price") %>' subscriptionId='<%# Eval("Id") %>' /><br />
                                                                            </ItemTemplate>
                                                                        </cc1:CustomRepeater>
                                                                        <asp:HiddenField ID="SubscriptionLevelHiddenField" runat="server" Value="1" />
                                                                        <asp:ObjectDataSource ID="ocs" runat="server" SelectMethod="GetSubscriptionLevelsForSignUp"
                                                                            TypeName="SEOToolSet.Providers.SubscriptionManager"></asp:ObjectDataSource>
                                                                    </span>
                                                                </div>
                                                                <div class="DoClear">
                                                                </div>
                                                                <div style="margin-left: auto; margin-right: auto; width: 300px;">
                                                                    <uc3:CostConversion ID="CostConversion1" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </EditItemTemplate>
                                                </asp:FormView>
                                            </div>
                                        </cc1:RoundPanel>
                                    </div>
                                    <div class="WizardWrapper">
                                        <cc1:RoundPanel ID="RoundPanel2" runat="server" DiscardBottom="False" DiscardTop="False">
                                            <div class="WizardStep">
                                                <asp:FormView ID="CreateUserFormView" runat="server" DefaultMode="Edit" DataSourceID="odsGetUserFromSession"
                                                    Width="100%">
                                                    <EditItemTemplate>
                                                        <div class="FormPanel">
                                                            <div class="Legend">
                                                                <h2>
                                                                    <asp:Label ID="CreateUserHeading" runat="server" Text="User Information"></asp:Label>
                                                                </h2>
                                                            </div>
                                                            <div id="UserInlineMessage">
                                                            </div>
                                                            <div class="FormCSS">
                                                                <div class="Field OneLine">
                                                                    <asp:RequiredFieldValidator ID="LoginNameRequired" runat="server" ControlToValidate="LgTexBox"
                                                                        CssClass="Validator" Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, LoginNameRequired %>"></asp:RequiredFieldValidator>
                                                                    <label>
                                                                        <asp:Localize ID="Localize7" runat="server" Text="Login ID"></asp:Localize>*</label>
                                                                    <asp:TextBox CssClass="FormText Required QuickTip" title="You will use your Login ID whenever you sign in to the SEOToolSet. It must be unique. Examples: Your e-mail address, or other."
                                                                        ID="LgTexBox" runat="server" Text='<%# Bind("Login") %>' MaxLength="100" />
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="PwTextBox"
                                                                        CssClass="Validator" Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, PasswordRequired %>"></asp:RequiredFieldValidator>
                                                                    <label>
                                                                        <asp:Localize ID="Localize17" runat="server" Text="<%$ Resources:CommonTerms, Password %>"></asp:Localize>*
                                                                    </label>
                                                                    <asp:TextBox CssClass="FormText Required QuickTip" title="Your password is case-sensitive and must be at least 6 characters long. It cannot contain your login ID. To create a stronger password, make it longer and combine uppercase letters, lowercase letters, numbers, and/or symbols."
                                                                        ID="PwTextBox" runat="server" Text='<%# Bind("Password") %>' MaxLength="100"
                                                                        TextMode="Password" />
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <asp:RequiredFieldValidator ID="RetypePasswordRequired" runat="server" ControlToValidate="RetypePasswordTextBox"
                                                                        CssClass="Validator" Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, RetypePasswordRequired %>"></asp:RequiredFieldValidator>
                                                                    <asp:CompareValidator CssClass="Validator" ID="PasswordComparisonValidation" runat="server"
                                                                        ControlToCompare="PwTextBox" ControlToValidate="RetypePasswordTextBox" Display="Dynamic"
                                                                        ErrorMessage="<%$ Resources:CommonTerms, PasswordNotCoincide %>"></asp:CompareValidator>
                                                                    <label>
                                                                        <asp:Localize ID="Localize76" runat="server" Text="<%$ Resources:CommonTerms, RetypePassword %>"></asp:Localize>*
                                                                    </label>
                                                                    <asp:TextBox CssClass="FormText Required QuickTip" title="The Quick Help for the Password field above should remain visible while the cursor focus is on the Retype Password field."
                                                                        ID="RetypePasswordTextBox" runat="server" TextMode="Password" MaxLength="100"></asp:TextBox>
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" CssClass="Validator"
                                                                        ControlToValidate="FirstNameTextBox" Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, FirstNameRequired %>"></asp:RequiredFieldValidator>
                                                                    <label>
                                                                        <asp:Localize ID="Localize8" runat="server" Text="<%$ Resources:CommonTerms, FirstName %>"></asp:Localize>*
                                                                    </label>
                                                                    <asp:TextBox ID="FirstNameTextBox" title="Enter your first name, then your last name (surname). Your name will be saved in your user record."
                                                                        runat="server" CssClass="FormText Required QuickTip" MaxLength="250" Text='<%# Bind("FirstName") %>' />
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" ControlToValidate="LastNameTextBox"
                                                                        CssClass="Validator" Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, LastNameRequired %>"></asp:RequiredFieldValidator>
                                                                    <label>
                                                                        <asp:Localize ID="Localize9" runat="server" Text="<%$ Resources:CommonTerms, LastName %>"></asp:Localize>*</label>
                                                                    <asp:TextBox CssClass="FormText Required QuickTip" title="The Quick Help for the First Name field above should remain visible while the cursor focus is on the Last Name field."
                                                                        ID="LastNameTextBox" runat="server" Text='<%# Bind("LastName") %>' MaxLength="250" />
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" CssClass="Validator"
                                                                        ControlToValidate="EmailTextBox" Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, EmailRequired %>"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="EmailFormatValidation" runat="server" CssClass="Validator"
                                                                        ControlToValidate="EmailTextBox" Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, EmailNotValid %>"
                                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                                    <label>
                                                                        <asp:Localize ID="Localize10" runat="server" Text="E-mail Address"></asp:Localize>*</label>
                                                                    <asp:TextBox CssClass="FormText Required QuickTip" title="Your e-mail address will be used to notify you that certain reports have completed, or to assist you if you forget your login ID or password."
                                                                        ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' MaxLength="300" />
                                                                </div>
                                                            </div>
                                                            <div class="Separator">
                                                            </div>
                                                                <div>
                                                                <div class="HeadingAddress">
                                                                    <h3>
                                                                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:CommonTerms, UserAddress %>"></asp:Literal></h3>
                                                            </div>
                                                                <div id="DifferentUserAddressCommand" class="AddressCommand">
                                                                    <a href="javascript:void(0);" id="CustomUserAddressCommand" class="MiniCommand">
                                                                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:CommonTerms, EnterDifferentAddressCommand %>"></asp:Literal>
                                                                    </a>
                                                                </div>
                                                            </div>
                                                            <div class="UserAddressSummary AddressSummary">
                                                                    <span class="UserAddress1">&nbsp;</span><br />
                                                                    <span class="UserAddress2">&nbsp;</span><br />
                                                                    <span><cite class="UserCity">&nbsp;</cite>&nbsp;<cite class="UserState">&nbsp;</cite><cite
                                                                        class="UserZip">&nbsp;</cite> </span>
                                                                    <br />
                                                                    <span class="UserCountrySpan">&nbsp;</span>
                                                                </div>
                                                            <div class="FormCSS UserAddressDetail" style="display: none">
                                                                <div class="Field OneLine">
                                                                    <label>
                                                                        <asp:Localize ID="Localize12" runat="server" Text="Street Address"></asp:Localize></label>
                                                                    <asp:TextBox CssClass="FormText UserAddress1" ID="Address1TextBox" runat="server"
                                                                        Text='<%# Bind("Address1") %>' MaxLength="300" />
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <label>
                                                                        &nbsp;
                                                                    </label>
                                                                    <asp:TextBox CssClass="FormText UserAddress2" ID="Address2TextBox" runat="server"
                                                                        Text='<%# Bind("Address2") %>' MaxLength="300" />
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <label>
                                                                        <asp:Localize ID="Localize13" runat="server" Text="<%$ Resources:CommonTerms, City %>"></asp:Localize></label>
                                                                    <asp:TextBox CssClass="FormText AddressField UserCity" ID="CityTextBox" runat="server"
                                                                        Text='<%# Bind("CityTown") %>' MaxLength="100" />
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <label>
                                                                        <asp:Localize ID="Localize14" runat="server" Text="State or Province"></asp:Localize></label>
                                                                    <asp:TextBox CssClass="FormText AddressField UserState" ID="StateTextBox" runat="server"
                                                                        Text='<%# Bind("State") %>' MaxLength="4" Style="width: 130px;" />
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <label>
                                                                        <asp:Localize ID="Localize15" runat="server" Text="<%$ Resources:CommonTerms, Zip %>"></asp:Localize>
                                                                    </label>
                                                                    <asp:TextBox CssClass="FormText AddressField UserZip" ID="ZipTextBox" runat="server"
                                                                        Text='<%# Bind("Zip") %>' MaxLength="20" Style="width: 150px;" />
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <label>
                                                                        <asp:Localize ID="Localize11" runat="server" Text="<%$ Resources:CommonTerms, Country %>"></asp:Localize></label>
                                                                    <asp:DropDownList ID="CountryDropDownList" runat="server" AppendDataBoundItems="True"
                                                                        DataSourceID="odsCountries" DataTextField="Name" DataValueField="Id" SelectedValue='<%# Eval("Country.Id") %>'
                                                                        CssClass="AddressField UserCountry">
                                                                        <asp:ListItem Value="-1" Text="<%$ Resources:CommonTerms, Choose %>"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <label>
                                                                        <asp:Localize ID="Localize16" runat="server" Text="<%$ Resources:CommonTerms, Telephone %>"></asp:Localize></label>
                                                                    <asp:TextBox CssClass="FormText" ID="PhoneTextBox" runat="server" Text='<%# Bind("Telephone") %>'
                                                                        MaxLength="255" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </EditItemTemplate>
                                                </asp:FormView>
                                            </div>
                                        </cc1:RoundPanel>
                                    </div>
                                    <div class="WizardWrapper">
                                        <cc1:RoundPanel ID="RoundPanel3" runat="server" DiscardBottom="False" DiscardTop="False">
                                            <div class="WizardStep">
                                                <asp:FormView ID="BillingInformationFormView" runat="server" DefaultMode="Edit" DataSourceID="odsGetAccountFromSession"
                                                    CssClass="CreditCardPanel" Width="100%">
                                                    <EditItemTemplate>
                                                        <div class="FormPanel">
                                                            <div class="Legend">
                                                                <h2>
                                                                    <asp:Label ID="BillingInformationHeading" runat="server" Text="<%$ Resources:CommonTerms, BillingInformation %>"></asp:Label>
                                                                </h2>
                                                            </div>
                                                            <div class="FormCSS">
                                                                <div class="Field OneLine">
                                                                    <asp:RequiredFieldValidator CssClass="Validator" ID="CreditCardTypeRequired" runat="server"
                                                                        ControlToValidate="DropDownListCreditCardType" InitialValue="-1" Display="Dynamic"
                                                                        ErrorMessage="<%$ Resources:CommonTerms, CreditCardTypeRequired %>"></asp:RequiredFieldValidator>
                                                                    <label>
                                                                        <asp:Localize ID="Localize26" runat="server" Text="<%$ Resources:CommonTerms, CardType %>"></asp:Localize>*</label>
                                                                    <asp:DropDownList ID="DropDownListCreditCardType" AppendDataBoundItems="true" DataTextField="Name"
                                                                        Style="width: 90px;" DataValueField="Id" SelectedValue='<%# Eval("CreditCardType.Id") ?? "-1" %>'
                                                                        runat="server" DataSourceID="odsCreditCardType" CssClass="Required QuickTip"
                                                                        title="Subscription fees can be paid by credit card. Please select the type of credit card you want to use from the drop-down list.">
                                                                        <asp:ListItem Text="<%$ Resources:CommonTerms, Choose %>" Value="-1"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <span>
                                                                        <img id="creditCardLogo" src="<%= ResolveClientUrl("~/images/spacer.gif") %>" alt="" />
                                                                    </span>
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <asp:RequiredFieldValidator ID="CreditCardRequired" runat="server" CssClass="Validator"
                                                                        ControlToValidate="CreditCardNumberTextBox" Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, CreditCardNumberRequired %>"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator CssClass="Validator" ID="CreditCardNumberRegexValidation"
                                                                        runat="server" ErrorMessage="Enter a valid credit card number" Display="Dynamic"
                                                                        ControlToValidate="CreditCardNumberTextBox" ValidationExpression="^(?:4\d{12}(?:\d{3})?$|^5[1-5]\d{14}$|^6(?:011|5\d\d)\d{12}$|^3[47]\d{13}$|^3(?:0[0-5]|[68]\d)\d{11}$|^(?:2131|1800|35\d{3})\d{11})$"
                                                                        Text="Enter a valid credit card number"></asp:RegularExpressionValidator>
                                                                    <label>
                                                                        <asp:Localize ID="Localize25" runat="server" Text="<%$ Resources:CommonTerms, CreditCardNumber %>"></asp:Localize>*
                                                                    </label>
                                                                    <asp:TextBox CssClass="FormText Required QuickTip" title="Enter the account number of your credit card (only the numbers, with no spaces or dashes)."
                                                                        ID="CreditCardNumberTextBox" runat="server" Text='<%# Bind("CreditCardNumber") %>'
                                                                        MaxLength="18" />
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="The expiration date is required"
                                                                        ControlToValidate="MonthExpirationDate" CssClass="Validator" Display="Dynamic"
                                                                        InitialValue="-1"></asp:RequiredFieldValidator>
                                                                    <label>
                                                                        <asp:Localize ID="Localize62" runat="server" Text="<%$ Resources:CommonTerms, ExpirationDate %>"></asp:Localize>*</label>
                                                                    </label>
                                                                    <ddlb:OptionGroupSelect ID="MonthExpirationDate" runat="server" AppendDataBoundItems="true"
                                                                        CssClass="FormText Required QuickTip" DataSourceID="OdsMonthsYears" title="Enter the month and year that your credit card will expire. This is shown on the front of your card.">
                                                                        <ddlb:OptionGroupItem ID="OptionGroupItem1" Value="-1" Text="Choose" runat="server"
                                                                            Selected="true" />
                                                                    </ddlb:OptionGroupSelect>
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <asp:RequiredFieldValidator CssClass="Validator" ID="CVSRequired" runat="server"
                                                                        ControlToValidate="CreditCardCVS" Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, CVSRequired %>"></asp:RequiredFieldValidator>
                                                                    <label>
                                                                        <asp:Localize ID="Localize27" runat="server" Text="Card Security Code"></asp:Localize>*</label>
                                                                    <asp:TextBox CssClass="FormText Required QuickTip" title="For Visa, MasterCard or Discover, the card security code is a 3-digit number printed on the back of the card, to the right of the signature strip. For American Express, it is a 4-digit number printed on the front of the card, on the right side."
                                                                        ID="CreditCardCVS" runat="server" Columns="6" Text='<%# Bind("CreditCardCvs") %>'
                                                                        MaxLength="10" Style="width: 40px;"></asp:TextBox>
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <asp:RequiredFieldValidator CssClass="Validator" ID="CardholderNameRequired" runat="server"
                                                                        ErrorMessage="<%$ Resources:CommonTerms, CardholderNameRequired %>" Text="Name on Card is required"
                                                                        ControlToValidate="CreditCardCardholderTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <label>
                                                                        <asp:Localize ID="Localize29" runat="server" Text="Name on Card"></asp:Localize>*</label>
                                                                    <asp:TextBox CssClass="FormText Required QuickTip" title="Enter the cardholder’s name as it appears on the front of the credit card."
                                                                        ID="CreditCardCardholderTextBox" runat="server" MaxLength="255" Text='<%# Bind("CreditCardCardholder") %>' />
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <label>
                                                                        <asp:Localize ID="PromoCode" runat="server" meta:resourcekey="PromoCodeLabel"></asp:Localize></label>
                                                                    <asp:TextBox CssClass="FormText QuickTip" title="If you have a promotion code or coupon for a subscription discount, enter the code in this field. Note: If the promotion code you enter is not valid, you will need to clear the field or enter a different code before trying to click Proceed again."
                                                                        ID="PromoCodeTextBox" runat="server" Text='<%# Bind("PromoCode") %>' MaxLength="255" />
                                                                </div>
                                                            </div>
                                                            <div class="Separator">
                                                            </div>
                                                            <div>
                                                                <div class="HeadingAddress">
                                                                    <h3>
                                                                        Billing Address</h3>
                                                                </div>
                                                                <div id="DifferentBillingAddressCommand" class="AddressCommand">
                                                                    <a href="javascript:void(0);" id="CustomBillingAddressCommand" class="MiniCommand">
                                                                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:CommonTerms, EnterDifferentAddressCommand %>"></asp:Literal>
                                                                    </a>
                                                                </div>
                                                            </div>
                                                            <div class="BillingAddressSummary AddressSummary">
                                                                    <span class="BillingAddress1">&nbsp;</span><br />
                                                                    <span class="BillingAddress2">&nbsp;</span><br />
                                                                    <span><cite class="BillingCity">&nbsp;</cite>&nbsp;<cite class="BillingState">&nbsp;</cite><cite
                                                                        class="BillingZip">&nbsp;</cite> </span>
                                                                    <br />
                                                                    <span class="BillingCountrySpan">&nbsp;</span>
                                                                </div>
                                                            <div class="FormCSS BillingAddressDetail" style="display: none">
                                                                <div class="Field OneLine">
                                                                    <asp:RequiredFieldValidator CssClass="Validator" ID="AddressRequired" runat="server"
                                                                        ControlToValidate="Address1TextBox" Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, AddressRequired %>"></asp:RequiredFieldValidator>
                                                                    <label>
                                                                        <asp:Localize ID="Localize21" runat="server" Text="Street Address"></asp:Localize>*</label>
                                                                    <asp:TextBox CssClass="FormText BillingAddress1 Required QuickTip" title="Enter the billing address used for your credit card account."
                                                                        ID="Address1TextBox" runat="server" MaxLength="300" Text='<%# Bind("CreditCardAddress1") %>' />
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <label>
                                                                        &nbsp;</label>
                                                                    <asp:TextBox CssClass="FormText BillingAddress2" ID="Address2TextBox" runat="server"
                                                                        MaxLength="300" Text='<%# Bind("CreditCardAddress2") %>' />
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <asp:RequiredFieldValidator CssClass="Validator" ID="CityTownRequired" runat="server"
                                                                        ControlToValidate="CityTextBox" Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, CityRequired %>"></asp:RequiredFieldValidator>
                                                                    <label>
                                                                        <asp:Localize ID="Localize20" runat="server" Text="<%$ Resources:CommonTerms, City %>"></asp:Localize>*</label>
                                                                    <asp:TextBox CssClass="FormText BillingCity Required QuickTip" title="The Quick Help for the Billing Address field above should remain visible while the cursor focus is on the City field."
                                                                        ID="CityTextBox" runat="server" Text='<%# Bind("CreditCardCity") %>' MaxLength="100" />
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <label>
                                                                        <asp:Localize ID="Localize19" runat="server" Text="State or Province"></asp:Localize></label>
                                                                    <asp:TextBox CssClass="FormText BillingState Quicktip" title="Enter the state or province that is used for your credit card account, as an abbreviation. Examples: CA, NY, BC."
                                                                        ID="StateTextBox" runat="server" Text='<%# Bind("CreditCardState") %>' MaxLength="4"
                                                                        Style="width: 130px;" />
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <asp:RequiredFieldValidator ID="CreditCardZipRequiredValidation" CssClass="Validator"
                                                                        runat="server" ErrorMessage="ZIP or Postal Code is required" ControlToValidate="ZipTextBox"
                                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <label>
                                                                        <asp:Localize ID="Localize23" runat="server" Text="Zip or Postal Code"></asp:Localize>*</label>
                                                                    <asp:TextBox CssClass="FormText BillingZip Required QuickTip" title="Enter the ZIP or postal code that is used in the billing address for your credit card account."
                                                                        ID="ZipTextBox" runat="server" Text='<%# Bind("CreditCardZip") %>' MaxLength="20"
                                                                        Columns="7" Style="width: 150px;" />
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <asp:RequiredFieldValidator ID="CountryRequired" CssClass="Validator" runat="server"
                                                                        ControlToValidate="CountryDropDownList" Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, CountryRequired %>"
                                                                        InitialValue="-1"></asp:RequiredFieldValidator>
                                                                    <label>
                                                                        <asp:Localize ID="Localize22" runat="server" Text="<%$ Resources:CommonTerms, Country %>"></asp:Localize>*
                                                                    </label>
                                                                    <asp:DropDownList ID="CountryDropDownList" runat="server" AppendDataBoundItems="True"
                                                                        DataSourceID="odsCountries" DataTextField="Name" DataValueField="Id" SelectedValue='<%# Bind("CreditCardIdCountry") %>'
                                                                        CssClass="BillingCountry Required">
                                                                        <asp:ListItem Value="-1" Text="<%$ Resources:CommonTerms, Choose %>"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="Field OneLine">
                                                                    <label>
                                                                        &nbsp;</label>
                                                                    <asp:TextBox CssClass="FormText" ID="TextBox1" runat="server" MaxLength="100" Text='<%# Bind("CreditCardPhone") %>' />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </EditItemTemplate>
                                                </asp:FormView>
                                            </div>
                                        </cc1:RoundPanel>
                                    </div>
                                    <div id="feedbackMsg" class="red">
                                    </div>
                                    <div class="CenterWrapper">
                                        <ul>
                                            <li>
                                                <cc1:LinkButtonRound ID="ProceedCommand" runat="server" OnClick="ProceedCommand_Click"><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:CommonTerms, Proceed %>"></asp:Literal>                                                
                                                </cc1:LinkButtonRound>
                                            </li>
                                            <li>
                                                <cc1:LinkButtonRound ID="ClearFormCommand" runat="server" CommandName="Reset" CausesValidation="false"><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:CommonTerms, ClearForm %>"></asp:Literal>
                                                
                                                </cc1:LinkButtonRound>
                                            </li>
                                        </ul>
                                    </div>
                                </asp:WizardStep>
                                <asp:WizardStep StepType="Finish" ID="ConfirmationStep" runat="server" EnableTheming="False">
                                    <user:PageTitle ID="PageTitle1" runat="server" PageDescription="Please review your information below and click Subscribe to confirm your order"
                                        PageTitleText="Confirm Subscription" RenderRoundPanelStyles="false" HelpContainerVisible="false" />
                                    <div class="WizardWrapper" style="width: 600px;">
                                        <cc1:RoundPanel ID="RoundPanel4" runat="server" DiscardBottom="False" DiscardTop="False">
                                            <div class="FormPanel">
                                                <asp:FormView ID="AccountConfirmationFormView" Width="100%" runat="server" DataSourceID="odsGetAccountFromSession"
                                                    DefaultMode="ReadOnly" EnableViewState="true" meta:resourcekey="AccountConfirmationFormView">
                                                    <ItemTemplate>
                                                        <div class="Legend">
                                                            <h2>
                                                                <asp:Label ID="AccountInformationConfirmationHeading" runat="server" Text="<%$ Resources:CommonTerms, AccountInformation %>"></asp:Label>
                                                            </h2>
                                                        </div>
                                                        <div class="FormCSS">
                                                            <div class="Field OneLine">
                                                                <label>
                                                                    <asp:Localize ID="Localize30" runat="server" Text="<%$ Resources:CommonTerms, AccountName %>"></asp:Localize>:
                                                                </label>
                                                                <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                                                            </div>
                                                            <div class="Field OneLine">
                                                                <label>
                                                                    <asp:Localize ID="Localize32" runat="server" Text="Address"></asp:Localize>:
                                                                </label>
                                                                <asp:Label ID="CompanyAddress1Label" runat="server" Text='<%# Eval("CompanyAddress1") %>' />
                                                            </div>
                                                            <div class="Field OneLine">
                                                                <label>
                                                                    &nbsp;
                                                                </label>
                                                                <asp:Label ID="CompanyAddress2Label" runat="server" Text='<%# Eval("CompanyAddress2") %>' />
                                                            </div>
                                                            <div class="Field OneLine">
                                                                <label>
                                                                    &nbsp;
                                                                </label>
                                                                <span>
                                                                    <asp:Literal ID="CompanyCityLabel" runat="server" Text='<%# Eval("CompanyCity") %>' />,&nbsp;
                                                                    <asp:Literal ID="CompanyStateLabel" runat="server" Text='<%# Eval("CompanyState") %>' />&nbsp;<asp:Literal
                                                                        ID="Label2" runat="server" Text='<%# Eval("CompanyZip") %>' />
                                                                </span>
                                                            </div>
                                                            <div class="Field OneLine">
                                                                <label>
                                                                    &nbsp;
                                                                </label>
                                                                <asp:Label ID="CompanyIdCountryLabel" runat="server" Text='<%#  GetCountryName((Int32) Eval("CompanyIdCountry")) %>' />
                                                            </div>
                                                            <div class="Field OneLine">
                                                                <label>
                                                                    <asp:Localize ID="Localize36" runat="server" Text="<%$ Resources:CommonTerms, Telephone %>"></asp:Localize>:
                                                                </label>
                                                                <asp:Label ID="CompanyPhoneLabel" runat="server" Text='<%# Eval("CompanyPhone") %>' />
                                                            </div>
                                                            <div class="DoClear">
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:FormView>
                                                <asp:FormView ID="UserConfirmationFormView" Width="100%" runat="server" DataSourceID="odsGetUserFromSession"
                                                    DefaultMode="ReadOnly" EnableViewState="true" meta:resourcekey="UserConfirmationFormView">
                                                    <ItemTemplate>
                                                        <div class="Separator">
                                                        </div>
                                                        <div class="Legend">
                                                            <h2>
                                                                <asp:Label ID="UserInformationConfirmationHeading" runat="server" meta:resourcekey="UserInformationConfirmationHeading"></asp:Label>
                                                            </h2>
                                                        </div>
                                                        <div class="FormCSS">
                                                            <div class="Field OneLine">
                                                                <label>
                                                                    <asp:Localize ID="Localize37" runat="server" Text="Login ID"></asp:Localize>:
                                                                </label>
                                                                <asp:Label ID="LoginLabel" runat="server" Text='Login ID' />
                                                            </div>
                                                            <div class="Field OneLine">
                                                                <label>
                                                                    <asp:Localize ID="Localize39" runat="server" Text="<%$ Resources:CommonTerms, Name %>"></asp:Localize>:
                                                                </label>
                                                                <asp:Label ID="FullNameLabel" runat="server" Text='<%# Eval("FirstName")+ " " + Eval("LastName") %>' />
                                                            </div>
                                                            <div class="Field OneLine">
                                                                <label>
                                                                    <asp:Localize ID="Localize41" runat="server" Text="E-mail Address"></asp:Localize>:
                                                                </label>
                                                                <asp:Label ID="EmailLabel" runat="server" Text='<%# Eval("Email") %>' />
                                                            </div>
                                                            <div class="Field OneLine">
                                                                <label>
                                                                    <asp:Localize ID="Localize32" runat="server" Text="<%$ Resources:CommonTerms, Address %>"></asp:Localize>:
                                                                </label>
                                                                <asp:Label ID="CompanyAddress1Label" runat="server" Text='<%# Eval("Address1") %>' />
                                                            </div>
                                                            <div class="Field OneLine">
                                                                <label>
                                                                    &nbsp;
                                                                </label>
                                                                <asp:Label ID="CompanyAddress2Label" runat="server" Text='<%# Eval("Address2") %>' />
                                                            </div>
                                                            <div class="Field OneLine">
                                                                <label>
                                                                    &nbsp;
                                                                </label>
                                                                <span>
                                                                    <asp:Literal ID="CompanyCityLabel" runat="server" Text='<%# Eval("CityTown") %>' />,&nbsp;
                                                                    <asp:Literal ID="CompanyStateLabel" runat="server" Text='<%# Eval("State") %>' />&nbsp;<asp:Literal
                                                                        ID="Label2" runat="server" Text='<%# Eval("Zip") %>' />
                                                                </span>
                                                            </div>
                                                            <div class="Field OneLine">
                                                                <label>
                                                                    &nbsp;
                                                                </label>
                                                                <asp:Label ID="CompanyIdCountryLabel" runat="server" Text='<%#  GetCountryName((Int32) Eval("Country.Id")) %>' />
                                                            </div>
                                                            <div class="Field OneLine">
                                                                <label>
                                                                    <asp:Localize ID="Localize47" runat="server" Text="<%$ Resources:CommonTerms, Telephone %>"></asp:Localize>:
                                                                </label>
                                                                <asp:Label ID="TelephoneLabel" runat="server" Text='<%# Eval("Telephone") %>' />
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:FormView>
                                                <asp:FormView ID="BillingConfirmationFormView" Width="100%" runat="server" DataSourceID="odsGetAccountFromSession"
                                                    EnableViewState="true" meta:resourcekey="BillingConfirmationFormView">
                                                    <ItemTemplate>
                                                        <div class="Separator">
                                                        </div>
                                                        <div class="Legend">
                                                            <h2>
                                                                <asp:Label ID="BillingInformationConfirmationHeading" runat="server" Text="<%$ Resources:CommonTerms, BillingInformation %>"></asp:Label>
                                                            </h2>
                                                        </div>
                                                        <div class="FormCSS">
                                                            <div class="Field OneLine">
                                                                <label>
                                                                    <asp:Localize ID="Localize57" runat="server" Text="<%$ Resources:CommonTerms, CardType %>"></asp:Localize>:
                                                                </label>
                                                                <asp:Label ID="CreditCardTypeLabel" runat="server" Text='<%# Eval("CreditCardType.Name") ?? "--"  %>' />
                                                            </div>
                                                            <div class="Field OneLine">
                                                                <label>
                                                                    <asp:Localize ID="Localize56" runat="server" Text="<%$ Resources:CommonTerms, CreditCardNumber %>"></asp:Localize>:
                                                                </label>
                                                                <asp:Label ID="CreditCardNumberLabel" runat="server" Text='<%# Eval("CreditCardNumber") %>' />
                                                            </div>
                                                            <div class="Field OneLine">
                                                                <label>
                                                                    <asp:Localize ID="Localize58" runat="server" Text="<%$ Resources:CommonTerms, ExpirationDate %>"></asp:Localize>:
                                                                </label>
                                                                <asp:Label ID="CreditCardExpirationLabel" runat="server" Text='<%# Eval("CreditCardExpiration", "{0:MM/yyyy}") %>' />
                                                            </div>
                                                            <div class="Field OneLine">
                                                                <label>
                                                                    <asp:Localize ID="Localize59" runat="server" Text="<%$ Resources:CommonTerms,  CardholderName%>"></asp:Localize>:
                                                                </label>
                                                                <asp:Label ID="CreditCardCardholderLabel" runat="server" Text='<%# Eval("CreditCardCardholder") %>' />
                                                            </div>
                                                            <div class="Field OneLine">
                                                                <label>
                                                                    <asp:Localize ID="Localize32" runat="server" Text="<%$ Resources:CommonTerms, Address %>"></asp:Localize>:
                                                                </label>
                                                                <asp:Label ID="CompanyAddress1Label" runat="server" Text='<%# Eval("CreditCardAddress1") %>' />
                                                            </div>
                                                            <div class="Field OneLine">
                                                                <label>
                                                                    &nbsp;
                                                                </label>
                                                                <asp:Label ID="CompanyAddress2Label" runat="server" Text='<%# Eval("CreditCardAddress2") %>' />
                                                            </div>
                                                            <div class="Field OneLine">
                                                                <label>
                                                                    &nbsp;
                                                                </label>
                                                                <span>
                                                                    <asp:Literal ID="CompanyCityLabel" runat="server" Text='<%# Eval("CreditCardCity") %>' />,&nbsp;
                                                                    <asp:Literal ID="CompanyStateLabel" runat="server" Text='<%# Eval("CreditCardState") %>' />&nbsp;<asp:Literal
                                                                        ID="Label2" runat="server" Text='<%# Eval("CreditCardZip") %>' />
                                                                </span>
                                                            </div>
                                                            <div class="Field OneLine">
                                                                <label>
                                                                    &nbsp;
                                                                </label>
                                                                <asp:Label ID="CompanyIdCountryLabel" runat="server" Text='<%#  GetCountryName((Int32) Eval("CreditCardIdCountry")) %>' />
                                                            </div>
                                                            <div class="Field OneLine">
                                                                <label>
                                                                    <asp:Localize ID="Localize47" runat="server" Text="<%$ Resources:CommonTerms, Telephone %>"></asp:Localize>:
                                                                </label>
                                                                <asp:Label ID="Label3" runat="server" Text='<%#  Eval("CreditCardPhone") %>' />
                                                            </div>
                                                            <div class="DoClear">
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:FormView>
                                                <div class="Separator">
                                                </div>
                                                <div class="Legend">
                                                    <h2>
                                                        Orden Information
                                                    </h2>
                                                </div>
                                                <div id="OrderInformationContent">
                                                    <div class="Field">
                                                        <asp:Literal ID="OrderInformationLiteral" runat="server"></asp:Literal>
                                                    </div>
                                                    <div id="OrderInformationFinalInstructionsSection">
                                                        <asp:Literal ID="OrderInformationFinalInstructions" runat="server"></asp:Literal>
                                                    </div>
                                                    <div id="AprovalTermsOfUseSection">
                                                        <input type="checkbox" id="AprovalTermsOfUse" runat="server" />
                                                        <span class="strong">I have read and agree to the SEOToolSet&reg; Terms of Use (<asp:HyperLink
                                                            ID="TermsOfUseLink" NavigateUrl="TermsOfUse.aspx" runat="server">See terms of use</asp:HyperLink>)</span>
                                                    </div>
                                                </div>
                                                <div class="WizardActions CenterWrapper">
                                                    <ul>
                                                        <li>
                                                            <asp:LinkButton ID="ConfirmCommand" runat="server" CssClass="LinkCommandRound" OnClick="ConfirmCommand_Click"
                                                                CausesValidation="False">
                                                                <asp:Label ID="ConfirmCommandLabel" runat="server" Text="Subscribe"></asp:Label>
                                                            </asp:LinkButton>
                                                        </li>
                                                        <li>
                                                            <asp:LinkButton ID="CancelCommand" runat="server" CssClass="LinkCommandRound" CausesValidation="False"
                                                                OnClick="BackCommand_Command">
                                                                <asp:Label ID="CancelStep" runat="server" Text="Edit Information"></asp:Label>
                                                            </asp:LinkButton>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </cc1:RoundPanel>
                                    </div>
                                </asp:WizardStep>
                                <asp:WizardStep StepType="Complete" AllowReturn="false" ID="ReceiptStep" EnableTheming="False">
                                    <user:PageTitle HelpContainerVisible="false" ID="PageTitle3" PageDescription="Thank you for your order! Below is the receipt for your subscription"
                                        PageTitleText="Order Complete" RenderRoundPanelStyles="false" runat="server" />
                                    <div class="WizardWrapper ReceiptWrapper">
                                        <cc1:RoundPanel ID="RoundPanel5" runat="server">
                                            <div class="FormPanel ReceiptContent">
                                                <div class="Legend">
                                                    <h2>
                                                        Receipt
                                                    </h2>
                                                </div>
                                                <div class="ReceiptSection">
                                                    <div class="ReceiptSummary">
                                                        <ul class="ItemsSummary">
                                                            <li class="Item Header"><span class="Name">Product </span><span>Price </span></li>
                                                            <li class="Item"><span class="Name">
                                                                <asp:Literal ID="SubscriptionType" runat="server"></asp:Literal>
                                                            </span><span>
                                                                $<asp:Literal ID="SubscriptionPrice" runat="server"></asp:Literal> USD
                                                            </span></li>
                                                            <li class="Item"><span class="Name">
                                                                <asp:Literal ID="PromotionType" runat="server" Text="Promotion Discount"></asp:Literal>
                                                            </span><span>
                                                                $<asp:Literal ID="PromotionDiscount" runat="server"></asp:Literal> USD
                                                            </span></li>
                                                            <li class="Item Total"><span class="Name">
                                                                <asp:Literal ID="TotalToChargeLiteral" runat="server" Text="Total"></asp:Literal></span>
                                                                <span>
                                                                    $<asp:Literal ID="TotalToCharge" runat="server"></asp:Literal> USD</span> </li>
                                                        </ul>
                                                    </div>
                                                    <div class="ReceiptDetails">
                                                        <p class="ReceiptFinalInstructions">
                                                            <asp:Literal ID="ReceiptFinalInstructionsLiteral" runat="server"></asp:Literal>
                                                        </p>
                                                        <p class="ReceiptFinalInstructions Strong">
                                                            You may print this receipt page for your records. A receipt for the initial payment
                                                            transaction will also be sent by e-mail as soon as it is processed, which may be
                                                            on the next business day.
                                                        </p>
                                                        <p class="ReceiptFinalInstructions">
                                                            Your subscription is now active. Click Login to access the SEOToolSet now.</p>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ReceiptBillingInformation">
                                                <asp:FormView ID="ReceiptFormView" runat="server" DataSourceID="odsGetAccountFromSession">
                                                    <ItemTemplate>
                                                        <div class="SubscriptionSummary">
                                                            <ul>
                                                                <li>
                                                                    <label>
                                                                        Order Date</label>
                                                                    <asp:Label ID="Label17" runat="server" Text='<%# DateTime.Now.ToString("MM/dd/yyyy hh:mmTT") %>'></asp:Label></li>
                                                                <li>
                                                                    <label>
                                                                        Order Number
                                                                    </label>
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("SubscriptionId") %>'></asp:Label></li>
                                                                <li>
                                                                    <label>
                                                                        Billing Information</label>
                                                                    <ul>
                                                                        <li>
                                                                            <label>
                                                                                Payment type:</label>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("CreditCardType.Name") ?? "--" %>'></asp:Label>
                                                                        </li>
                                                                        <li>
                                                                            <label>
                                                                                Card Number:</label>
                                                                            <asp:Label ID="Label15" runat="server" Text='<%# Eval("CreditCardNumber") %>'></asp:Label>
                                                                        </li>
                                                                        <li>
                                                                            <label>
                                                                                Exp. Date:
                                                                            </label>
                                                                            <asp:Label ID="Label16" runat="server" Text='<%# Eval("CreditCardExpiration", "{0:MM/yyyy}") %>'></asp:Label>
                                                                        </li>
                                                                        <li>
                                                                            <label>
                                                                                Bill to:</label>
                                                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("CreditCardCardholder") %>'></asp:Label>
                                                                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("CreditCardAddress1") %>'></asp:Label>
                                                                            <asp:Label ID="Label9" runat="server" Text='<%# String.Format(@"{0} {1} {2}", Eval("CreditCardCity"), (Eval("CreditCardState")==null? string.Empty: ", " + Eval("CreditCardState")), Eval("CreditCardZip")) %>'></asp:Label>
                                                                            <asp:Label ID="Label7" runat="server" Text='<%# GetCountryName((Int32) Eval("CreditCardIdCountry")) %>' />
                                                                            <asp:Label ID="Label8" runat="server" Text='<%# Eval("CreditCardPhone") %>' />
                                                                    </ul>
                                                                </li>
                                                                <li>
                                                                    <label>
                                                                        Account Information
                                                                    </label>
                                                                    <ul>
                                                                        <li>
                                                                            <label>
                                                                                Account Name:
                                                                            </label>
                                                                            <asp:Label ID="Label10" runat="server" Text='<%# Eval("Name") %>'></asp:Label></li>
                                                                        <li>
                                                                            <label>
                                                                                Address:
                                                                            </label>
                                                                            <asp:Label ID="Label12" runat="server" Text='<%# Eval("CompanyAddress1") %>'></asp:Label>
                                                                            <asp:Label ID="Label13" runat="server" Text='<%# String.Format(@"{0} {1} {2}", Eval("CompanyCity"), (Eval("CompanyState")==null? string.Empty: ", " + Eval("CompanyState")), Eval("CompanyZip")) %>'></asp:Label>
                                                                            <asp:Label ID="Label14" runat="server" Text='<%# GetCountryName((Int32) Eval("CompanyIdCountry")) %>' />
                                                                        </li>
                                                                        <li>
                                                                            <label>
                                                                                Phone Number:
                                                                            </label>
                                                                            <asp:Label ID="Label11" runat="server" Text='<%# Eval("CompanyPhone") %>'></asp:Label></li>
                                                                    </ul>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:FormView>
                                            </div>
                                        </cc1:RoundPanel>
                                        <div class="WizardActions CenterWrapper">
                                            <ul>
                                                <li>
                                                    <asp:LinkButton ID="LoginCommand" runat="server" CssClass="LinkCommandRound" PostBackUrl="LoginPage.aspx"
                                                        CausesValidation="False">
                                                        <asp:Label ID="LoginCommandLabel" runat="server" Text="<%$ Resources:CommonTerms, Login %>"></asp:Label>
                                                    </asp:LinkButton>
                                                </li>
                                                <li><a class="LinkCommandRound" href="javascript:void(0);">
                                                    <asp:Label ID="PrintLabel" runat="server" Text="<%$ Resources:CommonTerms, Print %>"></asp:Label>
                                                </a></li>
                                            </ul>
                                        </div>
                                    </div>
                                </asp:WizardStep>
                            </WizardSteps>
                            <StartNavigationTemplate>
                            </StartNavigationTemplate>
                            <FinishNavigationTemplate>
                            </FinishNavigationTemplate>
                        </asp:Wizard>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:ObjectDataSource ID="odsGetAccountFromSession" runat="server" SelectMethod="GetAccount"
        UpdateMethod="UpdateAccount" TypeName="SEOToolSet.WebApp.odsClass.AccountODS"
        OnUpdating="odsGetAccountFromSession_Updating">
        <UpdateParameters>
            <asp:Parameter Name="name" Type="String" />
            <asp:Parameter Name="maxNumberOfUser" Type="Int32" />
            <asp:Parameter Name="maxNumberOfDomainUser" Type="Int32" />
            <asp:Parameter Name="maxNumberOfProjects" Type="Int32" />
            <asp:Parameter Name="companyName" Type="String" />
            <asp:Parameter Name="companyAddress1" Type="String" />
            <asp:Parameter Name="companyAddress2" Type="String" />
            <asp:Parameter Name="companyCity" Type="String" />
            <asp:Parameter Name="companyState" Type="String" />
            <asp:Parameter Name="companyZip" Type="String" />
            <asp:Parameter Name="creditCardNumber" Type="String" />
            <asp:Parameter Name="creditCardType" Type="Object" />
            <asp:Parameter Name="creditCardAddress1" Type="String" />
            <asp:Parameter Name="creditCardAddress2" Type="String" />
            <asp:Parameter Name="creditCardCity" Type="String" />
            <asp:Parameter Name="creditCardZip" Type="String" />
            <asp:Parameter Name="recurringBill" Type="Boolean" />
            <asp:Parameter Name="enabled" Type="Boolean" />
            <asp:Parameter Name="accountExpirationDate" Type="DateTime" />
            <asp:Parameter Name="creditCardExpiration" Type="DateTime" />
            <asp:Parameter Name="owner" Type="Object" />
            <asp:Parameter Name="creditCardEmail" Type="String" />
            <asp:Parameter Name="creditCardIdCountry" Type="Int32" />
            <asp:Parameter Name="creditCardState" Type="String" />
            <asp:Parameter Name="creditCardCardholder" Type="String" />
            <asp:Parameter Name="promoCode" Type="String" />
            <asp:Parameter Name="companyIdCountry" Type="Int32" />
            <asp:Parameter Name="creditCardCVS" Type="String" />
            <asp:Parameter Name="companyPhone" Type="String" />
            <asp:Parameter Name="creditCardPhone" Type="String" />
            <asp:Parameter Name="IdSubscriptionLevel" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsGetUserFromSession" runat="server" SelectMethod="GetUser"
        TypeName="SEOToolSet.WebApp.odsClass.AccountODS" UpdateMethod="UpdateUser" OnUpdating="odsGetUserFromSession_Updating">
        <UpdateParameters>
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
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsCountries" runat="server" SelectMethod="GetCountries"
        TypeName="SEOToolSet.Providers.SEOMembershipManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsCreditCardType" runat="server" SelectMethod="GetCreditCardTypes"
        TypeName="SEOToolSet.Providers.AccountManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsMonthsYears" runat="server" SelectMethod="CreateMonthYearItems"
        TypeName="SEOToolSet.WebApp.SignUp"></asp:ObjectDataSource>
    </form>

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            addAjaxValidation();
            $('#SubscriptionWizard_ClearFormCommand').click(function() {
                if (confirm("Are you sure you want to remove your entries and clear the form?")) {
                    $('#form1').each(function() {
                        // iterate the elements within the form
                        $(':input', this).each(function() {
                            var type = this.type, tag = this.tagName.toLowerCase();
                            if (type == 'text' || type == 'password' || tag == 'textarea')
                                this.value = '';
                            else if (type == 'checkbox')
                                this.checked = false;
                            else if (type == 'radio')
                                this.checked = $(this).val() == 1;
                            else if (tag == 'select')
                                $(this).val(-1);
                        });
                    });
                    $('#SubscriptionWizard_CreateAccountFormView_SubscriptionLevelHiddenField').val(1);
                    $('#SubscriptionWizard_CreateAccountFormView_NameCreateAccountInput').focus();
                }
                return false;
            });
            $('#SubscriptionWizard_ConfirmCommand').live("click", function() {
                if (!$('#SubscriptionWizard_AprovalTermsOfUse').is(':checked')) {
                    $.showBigDialog('<p>Please read the SEOToolSet Terms of Use and agree by checking the box.</p>', { showOk: true,
                        showCancel: false
                    });
                    return false;
                }
            });
            $('#SubscriptionWizard_BillingInformationFormView_DropDownListCreditCardType').bind('click change', function() {
                var paymentTypeSelect = $(this);
                if (paymentTypeSelect.val().length > 0) {
                    $.post('<%= ResolveClientUrl("~/Handler/GetCreditCardTypeLogo.ashx") %>', { creditCardType: paymentTypeSelect.val() }, function(data) {
                        if (data.result && data.result.length > 0) {
                            $('#creditCardLogo').attr('src', data.result);
                        }
                        else {
                            $('#creditCardLogo').attr('src', '<%= ResolveClientUrl("~/images/spacer.gif") %>)');
                        }
                    }
                , 'json');
                }
                else {
                    $('#creditCardLogo').attr('src', '<%= ResolveClientUrl("~/images/spacer.gif") %>)');
                }
            });
            $('input:radio').attr('name', 'SubscriptionLevel').bind('change click', function() {
                if (this.checked) {
                    $('#SubscriptionWizard_CreateAccountFormView_SubscriptionLevelHiddenField').val($(this).parent().attr('subscriptionId'));
                }
            });

            $('a, :input, .TabMePlease').fixTabIndex();

            $('a.button').disableTextSelection();

            $('.CenterWrapper').fitToChildrenWidth();

            $('.AddressDetail').slideUp();

            $('#CustomUserAddressCommand').click(function() {
                $('.UserAddressDetail').slideDown();
                $('#DifferentUserAddressCommand').hide();
                $('.UserAddressSummary').slideUp();
            });

            $('#CustomBillingAddressCommand').click(function() {
                $('.BillingAddressDetail').slideDown();
                $('#DifferentBillingAddressCommand').hide();
                $('.BillingAddressSummary').slideUp();
            });

            $('#SubscriptionWizard_ProceedCommand').click(function() {
                var promoCodeText = $('#SubscriptionWizard_BillingInformationFormView_PromoCodeTextBox').val();
                $.hideInLineMessage($('#feedbackMsg'));
                if ($.trim(promoCodeText).length > 0) {
                    $.post('<%= ResolveClientUrl("~/Handler/GetPromoCode.ashx") %>', { action: 'Validate', code: promoCodeText, subscriptionLevelId: $('#SubscriptionWizard_CreateAccountFormView_SubscriptionLevelHiddenField').val() },
                        function(promoCodeStatus) {
                            var warningMessageValidation = "";
                            switch (promoCodeStatus.StatusCode) {
                                case 0: // The promo code was found
                                    WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions("SubscriptionWizard$ProceedCommand", "", true, "", "", false, true));
                                    break;
                                case 1:
                                    warningMessageValidation = '<%= Resources.DialogMessages.PromoCodeNotValid %>';
                                    break;
                                case 2:
                                    var referenceDate = promoCodeStatus.ReferenceDate;
                                    warningMessageValidation = $.stringFormat('<%= Resources.DialogMessages.PromoCodeNotYetValid %>', referenceDate || '-');
                                    break;
                                case 3:
                                    warningMessageValidation = '<%= Resources.DialogMessages.PromoCodeExpired %>';
                                    break;
                                case 4:
                                    warningMessageValidation = '<%= Resources.DialogMessages.PromoCodeMaxUseExceeded  %>';
                                    break;
                                case 6:
                                    warningMessageValidation = '<%= Resources.DialogMessages.PromoCodeAccountTypeOnlyExisting %>';
                                    break;
                                default:
                                    warningMessageValidation = 'There was a problem while retrieving data from server';
                            }
                            if (warningMessageValidation && warningMessageValidation.length > 0)
                                $.showInlineMessage($('#feedbackMsg'), warningMessageValidation, { sticky: true });
                        }, 'json');
                } else {
                    WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions("SubscriptionWizard$ProceedCommand", "", true, "", "", false, true));
                }
                return false;
            });

            //Notifies the errors caused by ajax requests in the page
            $().ajaxError(function() {
                alert('There was an error on the server');
            });

            $('#SubscriptionWizard_CreateAccountFormView_CompanyAddressTextBox1').bind('keyup blur change', function() {
                var target = 'Address1';
                var exp = '';
                if ($('.UserAddressSummary').is(':visible')) { exp += '.User' + target + ' '; }
                if ($('.BillingAddressSummary').is(':visible')) { exp += (exp.length > 0 ? ',' : '') + '.Billing' + target; }
                if (exp.length > 0 && $(this).val().length > 0) {
                    $(exp).val($(this).val()).text($(this).val());
                }
            });
            $('#SubscriptionWizard_CreateAccountFormView_CompanyAddressTextBox2').bind('keyup blur change', function() {
                var target = 'Address2';
                var exp = '';
                if ($('.UserAddressSummary').is(':visible')) { exp += '.User' + target + ' '; }
                if ($('.BillingAddressSummary').is(':visible')) { exp += (exp.length > 0 ? ',' : '') + '.Billing' + target; }
                if (exp.length > 0 && $(this).val().length > 0) {
                    $(exp).val($(this).val()).text($(this).val());
                }
            });
            $('#SubscriptionWizard_CreateAccountFormView_CompanyCityTextBox').bind('keyup blur change', function() {
                var target = 'City';
                var exp = '';
                if ($('.UserAddressSummary').is(':visible')) { exp += '.User' + target + ' '; }
                if ($('.BillingAddressSummary').is(':visible')) { exp += (exp.length > 0 ? ',' : '') + '.Billing' + target; }
                if (exp.length > 0 && $(this).val().length > 0) {
                    $(exp).val($(this).val()).text($(this).val());
                }
            });
            $('#SubscriptionWizard_CreateAccountFormView_CompanyStateTextBox').bind('keyup blur change', function() {
                var target = 'State';
                var exp = '';
                if ($('.UserAddressSummary').is(':visible')) { exp += '.User' + target + ' '; }
                if ($('.BillingAddressSummary').is(':visible')) { exp += (exp.length > 0 ? ',' : '') + '.Billing' + target; }
                if (exp.length > 0 && $(this).val().length > 0) {
                    $(exp).val($(this).val()).text($(this).val());
                }
            });
            $('#SubscriptionWizard_CreateAccountFormView_CompanyZipTextBox').bind('keyup blur change', function() {
                var target = 'Zip';
                var exp = '';
                if ($('.UserAddressSummary').is(':visible')) { exp += '.User' + target + ' '; }
                if ($('.BillingAddressSummary').is(':visible')) { exp += (exp.length > 0 ? ',' : '') + '.Billing' + target; }
                if (exp.length > 0 && $(this).val().length > 0) {
                    var zip = '';
                    var zipSpan = '';
                    if ($.trim($(this).val()) != '') {
                        zip = $.trim($(this).val());
                        zipSpan = ', ' + $.trim($(this).val());
                    }
                    $(exp).val(zip).text(zipSpan);
                }
            });
            $('#SubscriptionWizard_CreateAccountFormView_CountryDropDownList').bind('blur change click', function() {
                var target = 'Country'; var targetSpan = 'CountrySpan';
                var exp = ''; var expSpan = '';
                if ($('.UserAddressSummary').is(':visible')) {
                    exp += '.User' + target + ' '; expSpan += '.User' + targetSpan;
                }
                if ($('.BillingAddressSummary').is(':visible')) {
                    exp += (exp.length > 0 ? ',' : '') + '.Billing' + target; expSpan += (exp.length > 0 ? ',' : '') + '.Billing' + targetSpan;
                }
                if (exp.length > 0 && $(this).val().length > 0) {
                    if ($(this).val() > 0) {
                        var country = $('#SubscriptionWizard_CreateAccountFormView_CountryDropDownList option[value=' + $(this).val() + ']').text();
                        $(exp).val($(this).val());
                        $(expSpan).text(country);
                    }
                    else {
                        $(exp).val(-1);
                        $(expSpan).text('');
                    }
                }
            });

            $('.QuickTip').quickTips();
            $('input:radio[name=SubscriptionLevel]').click(function() {
                var c = SubscriptionWizard_CreateAccountFormView_CostConversion1_converter;
                if (!c) return;
                if ($('#SubscriptionWizard_CreateAccountFormView_CostConversion1_dropDownListCurrencies').val() < 0) return;
                c.setFromValue($(this).parent().attr('price'));
                c.showValueInOtherMoney();
            });
        });

        function addAjaxValidation() {
            firstTime = false;
            var inputAccount = $.byId('SubscriptionWizard_CreateAccountFormView_NameCreateAccountInput');
            var inputLogin = $.byId("SubscriptionWizard_CreateUserFormView_LgTexBox");
            var inputEmail = $.byId("SubscriptionWizard_CreateUserFormView_EmailTextBox");
            var accountExistsMessage = $.getResourceString("AccountNameNotUnique", "Choose another account name. The account name is used by another account");
            var loginExistsMessage = $.getResourceString("LoginNameNotUnique", "Choose another login name. The login name is used by another user");
            var emailExistsMessage = $.getResourceString("EmailNotUnique", "Choose another email. The email is used by another user");
            inputAccount.change(function() {
                $.post('<%= ResolveClientUrl("~/Handler/AjaxValidations.ashx") %>', { action: "CheckAccountName", accountName: inputAccount.val() },
                    function(data) {
                        if (data.result == true) { //if the account name is available
                            inputAccount.removeClass("InputError");
                            $.hideInLineMessage($("#AccountInlineMessage"));
                        } else {
                            inputAccount.focus();
                            inputAccount.select();
                            inputAccount.addClass("InputError");
                            $.showInlineMessage($("#AccountInlineMessage"), accountExistsMessage, { sticky: true });
                        }
                    }, "json");
            });
            inputLogin.change(function() {
                $.post('<%= ResolveClientUrl("~/Handler/AjaxValidations.ashx") %>', { action: "CheckLoginName", loginName: inputLogin.val() },
                    function(data) {
                        if (data.result == true) { //if the login name is available
                            inputLogin.removeClass("InputError");
                            $.hideInLineMessage($("#UserInlineMessage"));
                        } else {
                            inputLogin.focus();
                            inputLogin.select();
                            inputLogin.addClass("InputError");
                            $.showInlineMessage($("#UserInlineMessage"), loginExistsMessage, { sticky: true });
                        }
                    }, "json");
            });

            inputEmail.change(function() {
                $.post('<%= ResolveClientUrl("~/Handler/AjaxValidations.ashx") %>', { action: "CheckEmail", email: inputEmail.val() },
                    function(data) {
                        if (data.result == true) { //if the email is available
                            inputEmail.removeClass("InputError");
                            $.hideInLineMessage($("#UserInlineMessage"));
                        } else {
                            inputEmail.focus();
                            inputEmail.select();
                            inputEmail.addClass("InputError");
                            $.showInlineMessage($("#UserInlineMessage"), emailExistsMessage, { sticky: true });
                        }
                    }, "json");
            });
        }
    </script>

</body>
</html>
