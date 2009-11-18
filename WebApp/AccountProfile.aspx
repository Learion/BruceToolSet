<%@ Page Language="C#" MasterPageFile="~/BC.Master" AutoEventWireup="True" CodeBehind="AccountProfile.aspx.cs"
    Inherits="SEOToolSet.WebApp.AccountProfile" Title="<%$ Resources:CommonTerms, AccountInformation %>"
    MaintainScrollPositionOnPostback="false" Culture="auto" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<%@ Register Src="PageTitle.ascx" TagName="PageTitle" TagPrefix="uc1" %>
<%@ Register Src="AccountProjectsList.ascx" TagName="AccountProjectsList" TagPrefix="uc2" %>
<%@ Register Src="Controls/CostConversion.ascx" TagName="CostConversion" TagPrefix="uc3" %>
<%@ Register Src="Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc5" %>
<%@ Register TagPrefix="ddlb" Assembly="OptionDropDownList" Namespace="OptionDropDownList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentArea" runat="server">
    <uc5:IncludeFile ID="includeCss" TypeOfFile="Css" FilePath="~/css/AccountProfile.css"
        runat="server" />
    <uc1:PageTitle ID="PageTitle1" PageTitleText="Manage My Account " PageDescription="View or modify the settings for your SEOToolSet account"
        runat="server" RenderRoundPanelStyles="false" />
    <asp:ScriptManagerProxy ID="ScriptManager1" runat="server">
    </asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <div class="AccountAtAGlance">
                    <cc1:RoundPanel ID="RoundPanel3" runat="server">
                        <asp:FormView ID="AccountAtAGlanceFormView" runat="server" DataSourceID="odsAccount">
                            <ItemTemplate>
                                <div>
                                    <div class="Legend">
                                        <h2>
                                            Account at a Glance
                                        </h2>
                                    </div>
                                    <p class="UserName">
                                        <strong>
                                            <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Name") %>' />
                                        </strong>
                                    </p>
                                    <div class="DoClear">
                                    </div>
                                    <div>
                                        <p>
                                            <label>
                                                Subscriber Since
                                            </label>
                                            <asp:Label ID="CreatedDateLabel" runat="server" Text='<%# ((DateTime) Eval("CreatedDate")).ToShortDateString() %>' />
                                        </p>
                                        <p>
                                            <label>
                                                Status:
                                            </label>
                                            <asp:Label ID="EnabledLabel" runat="server" Text='<%# (Boolean)(Eval("Enabled") ?? false) ? "Active":"Inactive" %>' />
                                        </p>
                                        <p>
                                            <label>
                                                Subscription Level
                                            </label>
                                            <asp:Label ID="SubscriptionLevelLabel" runat="server" Text='<%# Eval("SubscriptionLevel.Name") %>' />
                                        </p>
                                        <p>
                                            <label>
                                                Last Billing
                                            </label>
                                            <asp:Label ID="Label2" runat="server" Text='<%# GetLastBilling() %>'></asp:Label>
                                        </p>
                                        <p>
                                            <label>
                                                Next Billing
                                            </label>
                                            <asp:Label ID="Label3" runat="server" Text='<%# GetNextBilling() %>'></asp:Label>
                                        </p>
                                        <p>
                                            <label>
                                                Account Users
                                            </label>
                                            <asp:Label ID="Label1" runat="server" Text='<%# string.Format("{0} Premium/{1} Courtesy", GetPremiumUsers(),GetReadOnlyUsers()) %>'></asp:Label>
                                        </p>
                                        <p>
                                            <label>
                                                Active Projects
                                            </label>
                                            <asp:Label ID="ProjectLabel" runat="server" Text='<%# Eval("Project") != null ? Eval("Project.Count") : 0 %>' />
                                        </p>
                                        <p class="References">
                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="ManageAccountUsers.aspx">Manage Users</asp:HyperLink>
                                            |
                                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="Home.aspx">Manage Projects</asp:HyperLink>
                                        </p>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:FormView>
                    </cc1:RoundPanel>
                </div>
                <div class="AccountInformationPanel">
                    <asp:FormView OnItemUpdating="formviewAccount_OnItemUpdating" OnDataBound="frmViewAccount_DataBound"
                        DataKeyNames="Id" Width="100%" DataSourceID="odsAccount" ID="formViewAccount"
                        runat="server" DefaultMode="Edit">
                        <EditItemTemplate>
                            <cc1:RoundPanel ID="RoundPanel1" runat="server" DiscardBottom="False" DiscardTop="False">
                                <div class="FormPanel">
                                    <div class="Legend">
                                        <div style="float: right; line-height: 30px; font-size: x-small;">
                                            *Required fields</div>
                                        <h2 style="width: 300px;">
                                            <asp:Localize ID="AccountInformationHeadingLabel" runat="server" Text="<%$ Resources:CommonTerms, AccountInformation %>"></asp:Localize></h2>
                                    </div>
                                    <div class="FormCSS">
                                        <div class="FirstFormSection">
                                            <div class="Field OneLine">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Name is required"
                                                    Display="Dynamic" CssClass="Validator" ControlToValidate="NameTextBox"></asp:RequiredFieldValidator>
                                                <label>
                                                    <asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:CommonTerms, AccountName %>"></asp:Localize>
                                                </label>
                                                <!-- TODO: pending duplicated account -->
                                                <asp:TextBox CssClass="FormText Disabled Required " ID="NameTextBox" runat="server"
                                                    Text='<%# Eval("Name") %>' />
                                            </div>
                                            <div class="Field OneLine">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="The address is required"
                                                    Display="Dynamic" CssClass="Validator" ControlToValidate="CompanyAddress1TextBox"></asp:RequiredFieldValidator>
                                                <label>
                                                    <asp:Localize ID="Localize3" runat="server" Text="Street Address"></asp:Localize>*
                                                </label>
                                                <asp:TextBox CssClass="FormText Required " ID="CompanyAddress1TextBox" runat="server"
                                                    Text='<%# Bind("CompanyAddress1") %>' />
                                            </div>
                                            <div class="Field OneLine">
                                                <label>
                                                    &nbsp;
                                                </label>
                                                <asp:TextBox CssClass="FormText" ID="CompanyAddress2TextBox" runat="server" Text='<%# Bind("CompanyAddress2") %>' />
                                            </div>
                                            <div class="Field OneLine">
                                                <asp:RequiredFieldValidator CssClass="Validator" ID="RequiredFieldValidator2" runat="server"
                                                    Display="Dynamic" InitialValue="-1" ErrorMessage="Country is required" ControlToValidate="DropDownListCompanyCountry"></asp:RequiredFieldValidator>
                                                <label>
                                                    <asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:CommonTerms, Country %>"></asp:Localize>*
                                                </label>
                                                <asp:DropDownList Width="220px" CssClass="FormDropDown Required " AppendDataBoundItems="True"
                                                    ID="DropDownListCompanyCountry" runat="server" DataSourceID="odsCountries" DataTextField="Name"
                                                    DataValueField="Id" SelectedValue='<%# Eval("CompanyIdCountry") ?? -1 %>'>
                                                    <asp:ListItem Value="-1" Selected="True" Text="<%$ Resources:CommonTerms, Choose %>"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="SecondFormSection">
                                            <div class="Field OneLine">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="Validator"
                                                    Display="Dynamic" ControlToValidate="CompanyPhoneTextBox" ErrorMessage="Phone Number is required"></asp:RequiredFieldValidator>
                                                <label>
                                                    <asp:Localize ID="Localize4" runat="server" Text="<%$ Resources:CommonTerms, Telephone %>"></asp:Localize>*
                                                </label>
                                                <asp:TextBox CssClass="FormText Required " ID="CompanyPhoneTextBox" runat="server"
                                                    Text='<%# Bind("CompanyPhone") %>' />
                                            </div>
                                            <div class="Field OneLine">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="The city is required"
                                                    CssClass="Validator" Display="Dynamic" ControlToValidate="CompanyCityTextBox"></asp:RequiredFieldValidator>
                                                <label>
                                                    <asp:Localize ID="Localize7" runat="server" Text="<%$ Resources:CommonTerms, City %>"></asp:Localize>*
                                                </label>
                                                <asp:TextBox CssClass="FormText Required " ID="CompanyCityTextBox" runat="server"
                                                    Text='<%# Bind("CompanyCity") %>' />
                                            </div>
                                            <div class="Field OneLine">
                                                <label>
                                                    <asp:Localize ID="Localize5" runat="server" Text="State/Province"></asp:Localize></label>
                                                <asp:TextBox ID="CompanyStateTextBox" CssClass="FormText" Width="150px" runat="server"
                                                    Text='<%# Bind("CompanyState") %>' />
                                            </div>
                                            <div class="Field OneLine">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Zip/Postal Code is required"
                                                    ControlToValidate="CompanyZipTextBox" CssClass="Validator" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <label>
                                                    <asp:Localize ID="Localize6" runat="server" Text="Zip/Postal Code"></asp:Localize>*
                                                </label>
                                                <asp:TextBox ID="CompanyZipTextBox" CssClass="FormText Required " Width="100px" runat="server"
                                                    Text='<%# Bind("CompanyZip") %>' />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </cc1:RoundPanel>
                            <cc1:RoundPanel ID="RoundPanel2" runat="server" DiscardBottom="False" DiscardTop="False">
                                <div class="FormPanel BillingInformationSection">
                                    <div class="Legend">
                                        <h2>
                                            <asp:Localize ID="Localize14" runat="server" Text="<%$ Resources:CommonTerms, BillingInformation %>"></asp:Localize></h2>
                                    </div>
                                    <div class="FormCSS">
                                        <asp:HiddenField ID="SubscriptionIdHiddenField" runat="server" Value='<%# Eval("SubscriptionId") %>' />
                                        <div class="FirstFormSection">
                                            <div class="Field OneLine">
                                                <asp:RequiredFieldValidator CssClass="Validator" ID="CreditCardTypeRequired" runat="server"
                                                    ErrorMessage="<%$ Resources:CommonTerms, CreditCardTypeRequired %>" ControlToValidate="DropDownListCreditCardType"
                                                    Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
                                                <label>
                                                    <asp:Localize ID="Localize21" runat="server" Text="<%$ Resources:CommonTerms, CardType %>"></asp:Localize>*
                                                </label>
                                                <asp:DropDownList ID="DropDownListCreditCardType" AppendDataBoundItems="True" DataTextField="Name"
                                                    DataValueField="Id" SelectedValue='<%# Eval("CreditCardType.Id") ?? "-1" %>'
                                                    runat="server" DataSourceID="odsCreditCardType" CssClass="FormText Required">
                                                    <asp:ListItem Value="-1" Selected="True" Text="<%$ Resources:CommonTerms, Choose %>">
                                                    </asp:ListItem>
                                                </asp:DropDownList>
                                                <img id="creditCardLogo" src="<%= ResolveClientUrl("~/images/spacer.gif") %>" alt="" />
                                            </div>
                                            <div class="Field OneLine">
                                                <asp:RequiredFieldValidator CssClass="Validator" ID="CreditCardNumberRequired" runat="server"
                                                    ErrorMessage="<%$ Resources:CommonTerms, CreditCardNumberRequired %>" Display="Dynamic"
                                                    ControlToValidate="TextBoxCreditCardNumber"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="CreditCardNumberRegexValidation" runat="server"
                                                    ControlToValidate="TextBoxCreditCardNumber" Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, CreditCardNumberNotValid %>"
                                                    ValidationExpression="^(?:4\d{12}(?:\d{3})?$|^5[1-5]\d{14}$|^6(?:011|5\d\d)\d{12}$|^3[47]\d{13}$|^3(?:0[0-5]|[68]\d)\d{11}$|^(?:2131|1800|35\d{3})\d{11})$"
                                                    CssClass="Validator"></asp:RegularExpressionValidator>
                                                <label>
                                                    <asp:Localize ID="Localize22" runat="server" Text="<%$ Resources:CommonTerms, CreditCardNumber %>"></asp:Localize>*
                                                </label>
                                                <asp:TextBox ID="TextBoxCreditCardNumber" CssClass="FormText Required" Width="150px"
                                                    runat="server" Text='<%# Eval("CreditCardNumber") %>' MaxLength="16" />
                                            </div>
                                            <div class="Field OneLine">
                                                <asp:RequiredFieldValidator CssClass="Validator" ID="CreditCardNameRequired" runat="server"
                                                    ErrorMessage="<%$ Resources:CommonTerms, CreditCardNameRequired %>" ControlToValidate="CreditCardCardholderTextBox"
                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                <label>
                                                    <asp:Localize ID="Localize15" runat="server" Text="<%$ Resources:CommonTerms, CardholderName %>"></asp:Localize>*
                                                </label>
                                                <asp:TextBox CssClass="FormText Required" ID="CreditCardCardholderTextBox" runat="server"
                                                    Text='<%# Bind("CreditCardCardholder") %>' />
                                            </div>
                                            <div class="Field OneLine">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="CreditCardAddress1TextBox"
                                                    Display="Dynamic" CssClass="Validator" ErrorMessage="The street for the credit card is required"></asp:RequiredFieldValidator>
                                                <label>
                                                    <asp:Localize ID="Localize9" runat="server" Text="Street Address"></asp:Localize>*
                                                </label>
                                                <asp:TextBox CssClass="FormText Required" ID="CreditCardAddress1TextBox" runat="server"
                                                    Text='<%# Bind("CreditCardAddress1") %>' />
                                            </div>
                                            <div class="Field OneLine">
                                                <label>
                                                    &nbsp;
                                                </label>
                                                <asp:TextBox CssClass="FormText" ID="CreditCardAddress2TextBox" runat="server" Text='<%# Bind("CreditCardAddress2") %>' />
                                            </div>
                                            <div class="Field OneLine">
                                                <asp:RequiredFieldValidator CssClass="Validator" ID="CountryRequired" runat="server"
                                                    ErrorMessage="<%$ Resources:CommonTerms, CreditCardCountryRequired %>" Display="Dynamic"
                                                    ControlToValidate="DropDownListCreditCardIdCountry" InitialValue="-1"></asp:RequiredFieldValidator>
                                                <label>
                                                    <asp:Localize ID="Localize16" runat="server" Text="<%$ Resources:CommonTerms, Country %>"></asp:Localize>*
                                                </label>
                                                <asp:DropDownList Width="220px" CssClass="FormDropDown Required " AppendDataBoundItems="True"
                                                    ID="DropDownListCreditCardIdCountry" runat="server" DataSourceID="odsCountries"
                                                    DataTextField="Name" DataValueField="Id" SelectedValue='<%# Eval("CreditCardIdCountry") ?? -1 %>'>
                                                    <asp:ListItem Value="-1" Selected="True" Text="<%$ Resources:CommonTerms, Choose %>"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="SecondFormSection">
                                            <div class="Field OneLine">
                                                <label>
                                                    <asp:Localize ID="Localize8" runat="server" Text="Security Code"></asp:Localize>
                                                </label>
                                                <asp:TextBox ID="CVSCodeTextBox" runat="server" Style="width: 70px;" MaxLength="4"></asp:TextBox>
                                            </div>
                                            <div class="Field OneLine">
                                                <label>
                                                    <asp:Localize ID="Localize23" runat="server" Text="<%$ Resources:CommonTerms, ExpirationDate %>"></asp:Localize>*
                                                </label>
                                                <ddlb:OptionGroupSelect ID="MonthExpirationDate" runat="server" SelectedValue='<%# (Eval("CreditCardExpiration")==null?DateTime.Now:(DateTime)Eval("CreditCardExpiration")).ToString("MM/yyyy") %>'
                                                    CssClass="FormText Required" DataSourceID="odsMonthsYears" title="Enter the month and year that your credit card will expire. This is shown on the front of your card.">
                                                </ddlb:OptionGroupSelect>
                                            </div>
                                            <div class="Field OneLine">
                                                <label>
                                                    <asp:Localize ID="Localize17" runat="server" Text="<%$ Resources:Commonterms,Telephone %>"></asp:Localize></label>
                                                <asp:TextBox CssClass="FormText" ID="CreditCardPhoneTextBox" runat="server" Text='<%# Bind("CreditCardPhone") %>' />
                                            </div>
                                            <div class="Field OneLine">
                                                <asp:RequiredFieldValidator CssClass="Validator" ID="CityRequired" runat="server"
                                                    ErrorMessage="<%$ Resources:CommonTerms, CreditCardCityRequired %>" ControlToValidate="CreditCardCityTextBox"
                                                    Display="Dynamic" />
                                                <label>
                                                    <asp:Localize ID="Localize18" runat="server" Text="<%$ Resources:CommonTerms, City %>"></asp:Localize>*
                                                </label>
                                                <asp:TextBox CssClass="FormText Required" ID="CreditCardCityTextBox" runat="server"
                                                    Text='<%# Bind("CreditCardCity") %>' />
                                            </div>
                                            <div class="Field OneLine">
                                                <label>
                                                    <asp:Localize ID="Localize19" runat="server" Text="State/Province"></asp:Localize>
                                                </label>
                                                <asp:TextBox ID="CreditCardStateTextBox" CssClass="FormText" runat="server" Text='<%# Bind("CreditCardState") %>' />
                                            </div>
                                            <div class="Field OneLine">
                                                <asp:RequiredFieldValidator CssClass="Validator" ID="ZipRequired" runat="server"
                                                    ErrorMessage="<%$ Resources:CommonTerms, CreditCardZipRequired %>" ControlToValidate="CreditCardZipTextBox"
                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                <label>
                                                    <asp:Localize ID="Localize20" runat="server" Text="Zip/Postal Code"></asp:Localize>*
                                                </label>
                                                <asp:TextBox ID="CreditCardZipTextBox" CssClass="FormText Required" Width="100px"
                                                    runat="server" Text='<%# Bind("CreditCardZip") %>' />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </cc1:RoundPanel>
                            <cc1:RoundPanel ID="SubscriptionInformationPanel" runat="server">
                                <div class="FormPanel">
                                    <div class="Legend">
                                        <h2>
                                            Subscription Information
                                        </h2>
                                    </div>
                                </div>
                                <div class="DoClear">
                                </div>
                                <div class="FormPanel">
                                    <div class="FormCSS" style="float: right; width: 360px;">
                                        <div class="Field">
                                            <%-- URL not defined for the moment --%>
                                            <a href="http://www.seotoolset.com/????" target="_blank">Compare subscription levels
                                            </a>
                                            <uc3:CostConversion ID="CostConversion1" runat="server" />
                                        </div>
                                        <div class="Field OneLine" style="margin-top: 5px;">
                                            <label>
                                                Promotion Code
                                            </label>
                                            <asp:TextBox CssClass="FormText" Style="width: 170px;" ID="PromotionCodeTextBox"
                                                runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="FormCSS Subscription" style="margin-right: 10px;">
                                        <div class="Field OneLine">
                                            <label>
                                                <asp:Localize ID="Localize28" runat="server" Text="<%$ Resources:CommonTerms, SubscriptionLevel %>"></asp:Localize></label>
                                        </div>
                                        <div>
                                            <span>
                                                <cc1:CustomRepeater ID="CustomRepeater1" runat="server" DataSourceID="odsGetSubscriptionLevels"
                                                    OnItemDataBound="CustomRepeater1_OnItemDataBound">
                                                    <ItemTemplate>
                                                        <asp:RadioButton ID="RadioButton1" runat="server" Text='<%# String.Format("<strong>SEOToolSet&amp;reg; {0}</strong> -- ${1} USD per month", Eval("Name"), Eval("Price")) %>'
                                                            value='<%# Eval("Id") %>' Checked='<%# (Int32)Eval("Id")==1 %>' price='<%# Eval("Price") %>' /><br />
                                                    </ItemTemplate>
                                                </cc1:CustomRepeater>
                                                <asp:ObjectDataSource ID="odsGetSubscriptionLevels" runat="server" SelectMethod="GetSubscriptionLevelsForSignUp"
                                                    TypeName="SEOToolSet.Providers.SubscriptionManager"></asp:ObjectDataSource>
                                                <asp:RadioButton ID="CancelSubscriptionRadioButton" runat="server" Text="Cancel Subscription"
                                                    value="-1" />
                                                <asp:HiddenField ID="SubscriptionLevelHiddenField" runat="server" Value="1" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </cc1:RoundPanel>
                            <div style="height: 40px;">
                                <div id="feedbackMsg" class="green" style="display: none">
                                    <asp:Literal ID="SavedMessage" runat="server" Text="Account changes have been saved"></asp:Literal>
                                </div>
                            </div>
                            <div class="Actions" style="margin: 10px auto; width: 250px;">
                                <div style="width: 100px; float: right;">
                                    <asp:LinkButton CssClass="LinkCommandRound Cancel" ID="UpdateCancelButton" runat="server"
                                        CausesValidation="False" CommandName="Cancel" ToolTip="Cancel changes to account information">
                                        <span>
                                            <asp:Localize ID="Localize51" runat="server" Text="<%$ Resources:CommonTerms, Cancel %>"></asp:Localize>
                                        </span>
                                    </asp:LinkButton>
                                </div>
                                <div>
                                    <asp:LinkButton CssClass="LinkCommandRound Update" ID="UpdateButton" runat="server"
                                        CommandName="Update" OnClick="UpdateButton_Click" ToolTip="Save changes to your account information">
                                        <span>
                                            <asp:Localize ID="Localize50" runat="server" Text="<%$ Resources:CommonTerms, Save %>"></asp:Localize>
                                        </span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="DoClear">
                            </div>
                        </EditItemTemplate>
                    </asp:FormView>
                </div>
            </div>
            <asp:HiddenField ID="BillingInformationSectionChanged" runat="server" Value="false" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        var first = true;
        var sw = null;
        var messageBeforeLeaving = '<%= GetGlobalResourceObject("DialogMessages", "AreYouSureWantToLeave") %>';
        $.onDomReady(account_loading);
        function account_loading() {
            $('input:radio').attr('name', 'SubscriptionLevel').bind('change click', function() {
                if (this.checked) {
                    $('#ctl00_contentArea_formViewAccount_SubscriptionLevelHiddenField').val($(this).val());
                }
            });

            $('.CenterWrapper').fitToChildrenWidth();

            $('.BillingInformationSection :input').bind('change', function() {
                $('#<%= BillingInformationSectionChanged.ClientID %>').val(true);
            });

            $('input:radio[name=SubscriptionLevel]').click(function() {
                if ($(this).val() == -1) return;
                var c = ctl00_contentArea_formViewAccount_CostConversion1_converter;
                var selector = $('#ctl00_contentArea_formViewAccount_CostConversion1_dropDownListCurrencies');
                if (!c || !selector || selector.val() == -1) return;
                c.setFromValue($(this).parent().attr('price'));
                c.showValueInOtherMoney();
            });

            $('#ctl00_contentArea_formViewAccount_UpdateCancelButton').click(function() {
                WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions('ctl00$contentArea$formViewAccount$UpdateCancelButton', "", false, "", "", false, false));
            });

            $('#ctl00_contentArea_formViewAccount_UpdateButton').click(function() {
                WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions("ctl00$contentArea$formViewAccount$UpdateButton", "", true, "", "", false, true));
                _enableIfChangesHappen();
                $("#feedbackMsg").fadeIn(500, function() {
                    var ele = $(this);
                    setTimeout(function() {
                        ele.fadeOut("slow");
                    }, 2000);
                });
                return false;
            });
            
            _enableIfChangesHappen();
            
            var opts = {
                title: '<%= GetGlobalResourceObject("DialogMessages","ConfirmExit") %>',
                txtMessage: messageBeforeLeaving,
                stayText: '<%= GetGlobalResourceObject("DialogMessages","StayOnPageButtonText") %>',
                leaveText: '<%= GetGlobalResourceObject("DialogMessages","LeavePageButtonText") %>',
                beforeShowConfirmation: function(args) {
                    if (!hasDataPendingToSave()) args.cancel = true;
                }
            };
            $.confirmNavigateAway(opts);
        }
        function hasDataPendingToSave() {
            return !$("#ctl00_contentArea_formViewAccount_UpdateCancelButton").hasClass("DisabledButton");
        };

        function _enableIfChangesHappen() {
            if (!first) {
                if (sw) {
                    sw.acceptChanges();
                    sw.doCheckChanges();
                }
                return;
            }
            sw = $("#ctl00_contentArea_formViewAccount_UpdateButton, #ctl00_contentArea_formViewAccount_UpdateCancelButton").enableIfChangeHappens(".AccountInformationPanel");
            sw.addEventListener('StateChanged', function(args) {
                window.onbeforeunload = _showMessageBeforeLeave;
            });
            sw.addEventListener('ReturnedToInitialState', function() {
                window.onbeforeunload = null;
            });
            $('input:radio[name=SubscriptionLevel]').bind('click blur', function() {
                if (sw) sw.doCheckChanges();
            })
            sw.acceptChanges();
            sw.doCheckChanges();
            first = false;
        }
        function _showMessageBeforeLeave(e) {
            var msg = 'Are you sure you want to navigate away from this page?\nYour changes have not been saved for this page. Do you want to save your changes?';
            if (e) {
                e.returnValue = msg;
                return false;
            }
            else {
                return msg;
            }
        }
    </script>

    <asp:ObjectDataSource ID="odsAccount" TypeName="SEOToolSet.Providers.AccountManager"
        runat="server" SelectMethod="GetAccount" UpdateMethod="UpdateAccount">
        <UpdateParameters>
            <asp:Parameter Name="id" Type="Int32" />
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
            <asp:Parameter Name="creditCardCvs" Type="String" />
            <asp:Parameter Name="companyPhone" Type="String" />
            <asp:Parameter Name="creditCardPhone" Type="String" />
            <asp:Parameter Name="subscriptionLevelId" Type="Int32" />
        </UpdateParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="__Page" Name="Id" PropertyName="IdAccount" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource TypeName="SEOToolSet.Providers.SEOMembershipManager" ID="odsCountries"
        runat="server" SelectMethod="GetCountries"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsCreditCardType" runat="server" SelectMethod="GetCreditCardTypes"
        TypeName="SEOToolSet.Providers.AccountManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsMonthsYears" runat="server" SelectMethod="CreateMonthYearItems"
        TypeName="SEOToolSet.WebApp.SignUp"></asp:ObjectDataSource>

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            addAjaxValidation();
            $('#ctl00_contentArea_formViewAccount_DropDownListCreditCardType').bind('click keypress', function() {
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

</asp:Content>
