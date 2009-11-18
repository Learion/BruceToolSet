#region

using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using LoggerFacade;
using SEOToolSet.Entities;
using SEOToolSet.Providers;
using SEOToolSet.WebApp.Controls;
using SEOToolSet.WebApp.odsClass;
using PromoType = SEOToolSet.Entities.Wrappers.PromoType;
using OptionDropDownList;

#endregion

namespace SEOToolSet.WebApp
{
    public partial class SignUp : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            Response.Cache.SetNoStore();
        }

        protected void odsGetUserFromSession_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            var countryControl = (DropDownList)CreateUserFormView.FindControl("CountryDropDownList");
            var password = CreateUserFormView.FindControl("PwTextBox") as TextBox;
            var count = 0;
            if (countryControl.SelectedIndex > 0)
                e.InputParameters["Country"] =
                    SEOMembershipManager.GetCountryById(int.Parse(countryControl.SelectedValue));

            if (password != null)
                for (var i = 0; i < password.Text.Length; i++)
                {
                    if (!char.IsLetterOrDigit(password.Text, i))
                        count++;
                }
            if (count >= Membership.MinRequiredNonAlphanumericCharacters) return;
            var passwordErrorMessage = string.Format(GetLocalResourceObject("PasswordIncorrect").ToString(),
                                                     Membership.MinRequiredNonAlphanumericCharacters);
            ClientScript.RegisterStartupScript(GetType(), "PasswordError",
                                               "$.showMessage('" + passwordErrorMessage + "');", true);
        }

        protected void odsGetAccountFromSession_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            var cardTypeControl =
                BillingInformationFormView.FindControl("DropDownListCreditCardType") as DropDownList;
            var monthExpirationDate = BillingInformationFormView.FindControl("MonthExpirationDate") as OptionGroupSelect;
            var subscriptionLevel = CreateAccountFormView.FindControl("SubscriptionLevelHiddenField") as HiddenField;
            if (cardTypeControl != null && cardTypeControl.SelectedIndex > 0)
                e.InputParameters["CreditCardType"] = AccountManager.GetCreditCardTypeById(cardTypeControl.SelectedValue);
            if (monthExpirationDate != null)
                e.InputParameters["CreditCardExpiration"] = monthExpirationDate.SelectedValue;
            if (subscriptionLevel != null)
                e.InputParameters["IdSubscriptionLevel"] = int.Parse(subscriptionLevel.Value);
        }

        protected void ProceedCommand_Click(object sender, EventArgs e)
        {
            if (!IsValid)
            {
                showInFeedbackMsg(@"Please review this form and correct any entries that have errors (in red). " +
                             @"Then click Proceed again.");

            }
            BillingInformationFormView.UpdateItem(true);
            var account = AccountODS.GetAccountFromStore();
            if (!string.IsNullOrEmpty(account.PromoCode))
            {
                var promoCodeStatus = PromoCodeManager.Validate(account.PromoCode, account.SubscriptionLevel.Id, null);

                if (promoCodeStatus.StatusCode != StatusCode.Found)
                    return;
            }
            CreateAccountFormView.UpdateItem(true);
            CreateUserFormView.UpdateItem(true);
            AccountConfirmationFormView.DataBind();
            BillingConfirmationFormView.DataBind();
            UserConfirmationFormView.DataBind();
            ReceiptFormView.DataBind();
            SubscriptionWizard.ActiveStepIndex++;
        }

        private void showInFeedbackMsg(string msg)
        {
            var sa = string.Format(@"$(""#feedbackMsg"").fadeIn(500, function() {{ var ele = $(this); $(this).html('{0}'); setTimeout(function() {{ ele.fadeOut(""slow""); }}, 2000); }});", msg);
            ScriptManager.RegisterStartupScript(this, GetType(), "CreateAccountError", sa, true);
        }

        protected void BackCommand_Command(object sender, EventArgs e)
        {
            SubscriptionWizard.ActiveStepIndex--;
        }

        protected void SubscriptionWizard_ActiveStepChanged(object sender, EventArgs e)
        {
            var stepIndex = SubscriptionWizard.ActiveStepIndex;
            printConfirmationDetails(stepIndex);
            printReceiptSummary(stepIndex);
        }

        protected void ConfirmCommand_Click(object sender, EventArgs e)
        {
            var isSubscriptionWithErrors = true;
            long? subscriptionId = null;
            try
            {
                var account = AccountODS.GetAccountFromStore();
                var user = AccountODS.GetUserFromStore();
                var subscriptionLevel = SubscriptionManager.GetSubscriptionLevel(account.SubscriptionLevel.Id);
                var creditCardNumberText = account.CreditCardNumber;
                if (!string.IsNullOrEmpty(account.PromoCode))
                {
                    PromoCodeManager.Consume(account.PromoCode);
                }
                account.SubscriptionLevel = subscriptionLevel;
                subscriptionId = RecurringBillingManager.CreateSubscription("SEOToolSet", account.CreditCardNumber,
                                                                            user.FirstName,
                                                                            user.LastName,
                                                                            account.CreditCardAddress1 + " " +
                                                                            account.CreditCardAddress2,
                                                                            account.CreditCardCity,
                                                                            account.CreditCardState,
                                                                            account.CreditCardZip,
                                                                            GetCountryName(
                                                                                account.CreditCardIdCountry.Value),
                                                                            account.CreditCardExpiration, 9999,
                                                                            (decimal)subscriptionLevel.Price, 1,
                                                                            "months", null, null);
                account.CreditCardNumber = new string('x', creditCardNumberText.Length - 4) +
                                           creditCardNumberText.Substring(creditCardNumberText.Length - 4);

                account.SubscriptionId = subscriptionId;
                AccountManager.CreateAccountAndUser(account, user);
                ReceiptFormView.DataBind();
                sendMailNewSubscription(account, user);
                SubscriptionWizard.ActiveStepIndex++;
                isSubscriptionWithErrors = false;
            }
            catch (MembershipCreateUserException ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(),
                                                    "CreateUserError",
                                                    string.Format("$.showMessage('{0}');",
                                                                  string.Format(
                                                                      GetLocalResourceObject("CreateUserError").ToString
                                                                          (), ex.StatusCode)),
                                                    true);
                Log.LogException(GetType(), ex);
            }
            catch (DuplicatedEntityException ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CreateAccountError",
                                                    string.Format("$.showMessage('{0}');",
                                                                  GetLocalResourceObject("DuplicatedAccountError")),
                                                    true);
                Log.LogException(GetType(), ex);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CreatingError",
                                                    string.Format("$.showMessage('{0}');",
                                                                  GetLocalResourceObject("CreationError")), true);
                Log.LogException(GetType(), ex);
            }
            finally
            {
                if (isSubscriptionWithErrors && subscriptionId.HasValue)
                    RecurringBillingManager.CancelSubscription(subscriptionId.Value);
            }
        }

        private void sendMailNewSubscription(Account account, SEOToolsetUser user)
        {
            var from = MailerFacade.GetSenderFromWebConfig();
            var addresses = user.Email;
            if (!MailerFacade.AtLeastOneEmailIsValid(ref addresses)) return;
            try
            {
                if (!string.IsNullOrEmpty(from))
                {
                    var parameters = new Dictionary<string, string>
                                         {
                                             {"FIRST_NAME",user.FirstName},
                                             {"LAST_NAME", user.LastName},
                                             {"ACCOUNT_NAME", account.Name},
                                             {"ADDRESS1", account.CompanyAddress1},
                                             {"ADDRESS2", account.CompanyAddress2},
                                             {"CITY", account.CompanyCity},
                                             {"STATE", account.CompanyState},
                                             {"ZIP", account.CompanyZip},
                                             {"ACCOUNT_PHONE", account.CompanyPhone},
                                             {"SUBSCRIPTION_LEVEL", account.SubscriptionLevel.Name}
                                         };
                    MailerFacade.SendEmailUsingMultiPart(addresses, from, Server.MapPath("~\\App_Data\\NewSubscriptionMailTemplate.html"), Server.MapPath("~\\App_Data\\NewSubscriptionPlainMailTemplate.xml"), parameters);
                }
            }
            catch (Exception ex)
            {
                Log.Error(GetType(), string.Format("Error when sending the new subscription mail: {0}", ex), ex);
            }
        }

        protected static string GetCountryName(int idCountry)
        {
            var country = SEOMembershipManager.GetCountryById(idCountry);
            return country != null && idCountry != -1 ? country.Name : "";
        }

        protected static string GetCurrentMonthYear()
        {
            return DateTime.Now.ToString("MM/yyyy");
        }

        private void printConfirmationDetails(int stepIndex)
        {
            //When the step is confirmation
            if (stepIndex != (int)AccountWizardStep.Confirmation) return;
            var account = AccountODS.GetAccountFromStore();
            var subscriptionLevel = SubscriptionManager.GetSubscriptionLevel(account.SubscriptionLevel.Id);
            var promoCode = PromoCodeManager.GetByCode(account.PromoCode);
            var totalToCharge = subscriptionLevel.Price;
            var confirmationDetails = @"<p class='ChosenSubscription'>You have chosen to subscribe to " +
                                      @"SEOToolSet&reg; [SUBSCRIPTION_LEVEL] for $[SUBSCRIPTION_PRICE] USD per month</p>" +
                                      @"<p>Initial charge $[SUBSCRIPTION_PRICE] USD</p>" +
                                      @"[PROMOTION]" +
                                      @"<p class='Strong'>Amount to be charged $[TOTAL] USD</p>";
            var confirmationFinalInstructions =
                @"<span>A subscription fee will be charged monthly for as long as you choose to remain a subscriber at the " +
                @"[SUBSCRIPTION_LEVEL] subscription level (currently $[SUBSCRIPTION_PRICE] per month), " +
                @"which will be charged to the payment method provided. " +
                @"You may cancel at any time, and cancellation will take effect at the end of the current billing period; " +
                @"no prorated fees will be refunded. " +
                @"If you are enjoying the SEOToolSet&reg;, do nothing and your subscription will automatically continue. </span>";
            confirmationDetails = confirmationDetails
                .Replace(@"[SUBSCRIPTION_LEVEL]", subscriptionLevel.Name)
                .Replace(@"[SUBSCRIPTION_PRICE]", subscriptionLevel.Price.ToString("0.00"));
            confirmationFinalInstructions = confirmationFinalInstructions
                .Replace(@"[SUBSCRIPTION_LEVEL]", subscriptionLevel.Name)
                .Replace(@"[SUBSCRIPTION_PRICE]", subscriptionLevel.Price.ToString("0.00"));
            if (promoCode != null)
            {
                var promotionDetails =
                    @"Your promotion code [PROMOTION_CODE] gives you a subscription discount: [PROMOTION_DESCRIPTION]. ";
                var promotionDiscountText = @"<p>Promotion discount -$[PROMOTION_DISCOUNT] USD</p>";
                var promotionDiscount = promoCode.PromoType.Id == (int)PromoType.Percentage
                                               ? totalToCharge * promoCode.PromoAmount.Value
                                               : promoCode.PromoAmount.Value;
                promotionDetails = promotionDetails
                    .Replace("[PROMOTION_CODE]", promoCode.Code)
                    .Replace("[PROMOTION_DESCRIPTION]", promoCode.Description);
                confirmationFinalInstructions = confirmationFinalInstructions.Insert(0, promotionDetails);
                totalToCharge -= promotionDiscount;
                promotionDiscountText = promotionDiscountText.Replace(@"[PROMOTION_DISCOUNT]",
                                                                      promotionDiscount.ToString("0.00"));
                confirmationDetails = confirmationDetails.Replace(@"[PROMOTION]", promotionDiscountText);
                if (totalToCharge < 0) totalToCharge = 0;
            }
            else
                confirmationDetails = confirmationDetails.Replace(@"[PROMOTION]", string.Empty);
            confirmationDetails = confirmationDetails.Replace(@"[TOTAL]", totalToCharge.ToString("0.00"));
            OrderInformationLiteral.Text = confirmationDetails;
            OrderInformationFinalInstructions.Text = confirmationFinalInstructions;
        }

        private void printReceiptSummary(int stepIndex)
        {
            if (stepIndex != (int)AccountWizardStep.Receipt) return;
            string receiptFinalInstructions;
            var account = AccountODS.GetAccountFromStore();
            var subscriptionLevel = SubscriptionManager.GetSubscriptionLevel(account.SubscriptionLevel.Id);
            var promoCode = PromoCodeManager.GetByCode(account.PromoCode);
            var totalToCharge = subscriptionLevel.Price;
            var discount = 0.0;
            if (promoCode == null)
            {
                receiptFinalInstructions =
                    @"Thank you for your subscription order. You will be charged a monthly subscription fee " +
                    @"for as long as you choose to remain a subscriber at the [SUBSCRIPTION_NAME] subscription level " +
                    @"(currently $[SUBSCRIPTION_PRICE] USD per month), which will be charged to the payment method provided. " +
                    @"You may cancel at any time, and cancellation will take effect at the end of the current billing period; " +
                    @"no prorated fees will be refunded. If you are enjoying the SEOToolSet, do nothing and your subscription will automatically continue.";
            }
            else
            {
                receiptFinalInstructions =
                    @"Your promotion code [PROMOTION_CODE] gives you a subscription discount: [PROMOTION_DESCRIPTION]." +
                    @"You will be charged monthly after your promotional period ends for as long as you" +
                    @"choose to remain a subscriber at the [SUBSCRIPTION_NAME] subscription level " +
                    @"(currently $[SUBSCRIPTION_PRICE] USD per month), which will be charged to the payment method provided. " +
                    @"You may cancel at any time, and cancellation will take effect at the end of the current billing period; " +
                    @"no prorated fees will be refunded. If you are enjoying the SEOToolSet, do nothing and your subscription will automatically continue.";
                receiptFinalInstructions = receiptFinalInstructions
                    .Replace(@"[PROMOTION_CODE]", promoCode.Code)
                    .Replace(@"[PROMOTION_DESCRIPTION]", promoCode.Description);
                discount = promoCode.PromoType.Id == (int)PromoType.Percentage
                               ? totalToCharge * promoCode.PromoAmount.Value
                               : promoCode.PromoAmount.Value;
                PromotionType.Text += @" (Code: [PROMOTION_CODE])".Replace(@"PROMOTION_CODE", promoCode.Code);
                totalToCharge -= discount;
            }
            SubscriptionType.Text = "SEOToolSet® [SUBSCRIPTION_NAME] (First month subscription) "
                .Replace(@"[SUBSCRIPTION_NAME]", subscriptionLevel.Name);
            SubscriptionPrice.Text = subscriptionLevel.Price.ToString("0.00");
            PromotionDiscount.Text = "-" + discount.ToString("0.00");
            TotalToCharge.Text = totalToCharge.ToString("0.00");

            receiptFinalInstructions = receiptFinalInstructions
                .Replace(@"[SUBSCRIPTION_NAME]", subscriptionLevel.Name)
                .Replace(@"[SUBSCRIPTION_PRICE]", subscriptionLevel.Price.ToString("0.00"));
            ReceiptFinalInstructionsLiteral.Text = receiptFinalInstructions;
        }

        public ICollection<OptionGroupItem> CreateMonthYearItems()
        {
            var items = new List<OptionGroupItem>();
            for (var i = 0; i < 5 * 12 - DateTime.Now.Month + 1; i++)
            {
                var dateToPrint = DateTime.Now.AddMonths(i);

                var item = new OptionGroupItem
                               {
                                   Value = dateToPrint.ToString("MM/yyyy"),
                                   Text =
                                       (GetGlobalResourceObject("Months", "M" + dateToPrint.ToString("MM")) + " " +
                                        dateToPrint.ToString("yyyy")),
                                   OptionGroup = dateToPrint.Year.ToString()
                               };
                items.Add(item);
            }

            return items;
        }

        #region Nested type: AccountWizardStep

        private enum AccountWizardStep
        {
            Subscription,
            Confirmation,
            Receipt
        }

        #endregion

        internal enum Month
        {
            January,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }
    }
}