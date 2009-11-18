<%@ Page Title="" Language="C#" MasterPageFile="~/BC.Master" AutoEventWireup="true"
    CodeBehind="PromoCodeTest.aspx.cs" Inherits="SEOToolSet.WebApp.PromoCodeTest" %>

<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<%@ Register Src="Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentArea" runat="server">
    <uc1:IncludeFile ID="IncludeFile9" TypeOfFile="Css" FilePath="~/css/DataEntry.css"
        runat="server" />
    <cc1:RoundPanel ID="RoundPanel1" runat="server">

        <script language="javascript" type="text/javascript">
            $(document).ready(function() {
                $('.Consume').disable(true);
                $('.Validate').click(function() {
                    $('.Consume').disable(true);
                    $('.Validate').disable(true);
                    $('#PriceText').val('');
                    $('.DiscountSection').slideUp();
                    $.post('<%= ResolveClientUrl("~/Handler/GetPromoCode.ashx") %>', { action: 'Validate', code: $('#<%= PromotionCodeTextBox.ClientID %>').val(), subscriptionLevelId: $('#<%= SubscriptionDropDownList.ClientID %>').val() },
                        function(promoCodeStatus) {
                            var warningMessageValidation = "";
                            switch (promoCodeStatus.StatusCode) {
                                case 0:
                                    warningMessageValidation = promoCodeStatus.PromoCodeDescription;
                                    $('#PriceText').val(promoCodeStatus.Discount);
                                    $('.DiscountSection').slideDown();
                                    $('.Consume').disable(false);
                                    break;
                                case 1:
                                    warningMessageValidation = '<%= Resources.DialogMessages.PromoCodeNotValid %>';
                                    break;
                                case 2:
                                    warningMessageValidation = '<%= Resources.DialogMessages.PromoCodeNotYetValid %>';
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
                            $.showInlineMessage($('#InlineMessage'), warningMessageValidation, { sticky: true });
                            $('.Validate').disable(false);
                        }, 'json');
                    return false;
                });
                $().ajaxError(function() {
                    alert('There was an error on the server');
                    $('.Validate').disable(false);
                    $('.Consume').disable(true);
                });
            });
        </script>

        <div class="FormPanel" style="width: 370px; margin-left: auto; margin-right: auto;">
            <asp:Literal ID="ServerMessagesLiteral" runat="server"></asp:Literal>
            <div id="InlineMessage">
            </div>
            <div class="FormCSS">
                <div class="Field OneLine">
                    <label>
                        <asp:Literal ID="PromotionCode" runat="server" Text="Promotion Code"></asp:Literal></label>
                    <asp:TextBox ID="PromotionCodeTextBox" runat="server" CssClass="FormText" MaxLength="16"></asp:TextBox>
                </div>
                <div class="Field OneLine">
                    <label>
                        <asp:Literal ID="SubjectLabel" runat="server" Text="Subscription Level"></asp:Literal>
                    </label>
                    <asp:DropDownList ID="SubscriptionDropDownList" runat="server" AppendDataBoundItems="True"
                        DataSourceID="odsSubscriptionLevel" DataTextField="Name" DataValueField="Id">
                        <asp:ListItem Selected="True" Value="-1">Choose</asp:ListItem>
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="odsSubscriptionLevel" runat="server" SelectMethod="GetSubscriptionLevelsForSignUp"
                        TypeName="SEOToolSet.Providers.SubscriptionManager"></asp:ObjectDataSource>
                </div>
                <div class="Field OneLine DiscountSection" style="display:none">
                    <label>
                        <asp:Literal ID="PriceLabel" runat="server" Text="Discount"></asp:Literal>
                    </label>
                    <input type="text" readonly="readonly" id="PriceText" />
                </div>
            </div>
            <div class="DoClear">
            </div>
            <div class="WizardActions CenterWrapper">
                <ul>
                    <li>
                        <asp:LinkButton ID="ValidateLinkButton" runat="server" CssClass="LinkCommandRound Validate"><span>Validate</span></asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton ID="ConsumeLinkButton" runat="server" CssClass="LinkCommandRound Consume"
                            OnClick="ConsumeLinkButton_OnClick"><span>Use it!</span></asp:LinkButton>
                    </li>
                </ul>
            </div>
        </div>
    </cc1:RoundPanel>
</asp:Content>
