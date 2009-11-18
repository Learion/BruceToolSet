#region

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using LoggerFacade;
using OptionDropDownList;
using R3M.Controls;
using SEOToolSet.Common;
using SEOToolSet.Entities;
using SEOToolSet.Providers;

#endregion

namespace SEOToolSet.WebApp
{
    public partial class AccountProfile : Page
    {
        public Int32 IdAccount
        {
            get
            {
                Int32 result;
                if (Int32.TryParse(Request.QueryString["IdAccount"], out result))
                    return result;
                if (result <= 0)
                {
                    var user = SEOMembershipManager.GetUser(Page.User.Identity.Name);
                    if (user != null)
                        result = user.Account.Id;
                }
                return result;
            }
        }

        public Account UserAccount
        {
            get
            {
                if (ViewState["Account"] != null)
                    return ViewState["Account"] as Account;
                Int32 accountId;
                Account account;
                if (Int32.TryParse(Request.QueryString["IdAccount"], out accountId))
                {
                    if (accountId > 0)
                    {
                        account = AccountManager.GetAccount(accountId);
                        ViewState["Account"] = account;
                        return account;
                    }
                }
                var user = SEOMembershipManager.GetUser(Page.User.Identity.Name);
                ViewState["Account"] = account = user.Account;
                return account;
            }
            set
            {
                ViewState["Account"] = value;
            }
        }

        public void Page_Load(Object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }

        protected void formviewAccount_OnItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            var frmAccount = sender as FormView;
            if (frmAccount == null)
                return;
            var cancelSubscriptionOption = frmAccount.FindControl("CancelSubscriptionRadioButton") as RadioButton;
            if (cancelSubscriptionOption != null && cancelSubscriptionOption.Checked)
            {
                //the cancel subscription process is done right here, we mustn't update anything
                return;
            }
            var subscriptionSelected = frmAccount.FindControl("SubscriptionLevelHiddenField") as HiddenField;
            int subscriptionLevelId;
            if (subscriptionSelected != null && int.TryParse(subscriptionSelected.Value, out subscriptionLevelId))
            {
                var oldSubscriptionLevelId = e.OldValues["SubscriptionLevelId"] as int?;
                //Only if the new subscripton is greater than before
                if (oldSubscriptionLevelId.HasValue && oldSubscriptionLevelId.Value > subscriptionLevelId)
                {
                    var promocode = frmAccount.FindControl("PromotionCodeTextBox") as TextBox;
                    e.NewValues["SubscriptionLevelId"] = subscriptionLevelId;
                    if (promocode != null && promocode.Text.Length > 0)
                    {
                        var promoCodeStatus = PromoCodeManager.Validate(promocode.Text, subscriptionLevelId, IdAccount);
                        if (promoCodeStatus.StatusCode != StatusCode.Found)
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                }
            }
            var dropdownlistCompanyCountry = frmAccount.FindControl("DropDownListCompanyCountry") as DropDownList;
            if (dropdownlistCompanyCountry == null || dropdownlistCompanyCountry.SelectedValue == "-1")
                return;
            e.NewValues["CompanyIdCountry"] = dropdownlistCompanyCountry.SelectedValue;
            var frmBilling = sender as FormView;
            if (frmBilling == null)
                return;
            var dropdownlistCreditCardCountry =
                frmBilling.FindControl("DropDownListCreditCardIdCountry") as DropDownList;
            if (dropdownlistCreditCardCountry != null && dropdownlistCreditCardCountry.SelectedValue != "-1")
            {
                e.NewValues["CreditCardIdCountry"] = dropdownlistCreditCardCountry.SelectedValue;
            }
            var dropDownListCreditCardType = frmBilling.FindControl("dropDownListCreditCardType") as DropDownList;
            if (dropDownListCreditCardType != null && dropDownListCreditCardType.SelectedValue != "-1")
            {
                e.NewValues["CreditCardType"] =
                    AccountManager.GetCreditCardTypeById(dropDownListCreditCardType.SelectedValue);
            }
            var creditCardNumberTextBox = frmBilling.FindControl("TextBoxCreditCardNumber") as TextBox;
            if (creditCardNumberTextBox != null && !string.IsNullOrEmpty(creditCardNumberTextBox.Text))
            {
                e.NewValues["CreditCardNumber"] = new string('x', creditCardNumberTextBox.Text.Length - 4) +
                                                  creditCardNumberTextBox.Text.Substring(
                                                      creditCardNumberTextBox.Text.Length - 4);
            }
            var creditCardExpirationSelect = frmBilling.FindControl("MonthExpirationDate") as OptionGroupSelect;
            if (creditCardExpirationSelect != null)
            {
                e.NewValues["CreditCardExpiration"] =
                    creditCardExpirationSelect.SelectedValue;
            }
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            var cancelSubscriptionOption = formViewAccount.FindControl("CancelSubscriptionRadioButton") as RadioButton;
            if (cancelSubscriptionOption != null && cancelSubscriptionOption.Checked)
            {
                // The cancel subscription process will be done right here
                return;
            }
            //if (BillingInformationSectionChanged.Value == "true")
            //{
            //    var amount = 0;
            //    short trialOcurrences = 0;
            //    var trialDiscount = 0;
            //    DateTime? creditCardExpirationDate = null;
            //    var fullName = string.Empty;
            //    var cardNumber = string.Empty;
            //    var address1 = string.Empty;
            //    var address2 = string.Empty;
            //    var city = string.Empty;
            //    var state = string.Empty;
            //    var zip = string.Empty;
            //    var country = string.Empty;
                
            //    var subscriptionHiddenField = formViewAccount.FindControl("SubscriptionIdHiddenField") as HiddenField;
            //    var cardNumberTextBox = formViewAccount.FindControl("TextBoxCreditCardNumber") as TextBox;
            //    var billToFullNameTextBox = formViewAccount.FindControl("CreditCardCardholderTextBox") as TextBox;
            //    var billToAddress1TextBox = formViewAccount.FindControl("CreditCardAddress1TextBox") as TextBox;
            //    var billToAddress2TextBox = formViewAccount.FindControl("CreditCardAddress2TextBox") as TextBox;
            //    var billToCityTextBox = formViewAccount.FindControl("CreditCardCityTextBox") as TextBox;
            //    var billToStateTextBox = formViewAccount.FindControl("CreditCardStateTextBox") as TextBox;
            //    var billToZipTextBox = formViewAccount.FindControl("CreditCardZipTextBox") as TextBox;
            //    var billToCountryDropDownList = formViewAccount.FindControl("DropDownListCreditCardIdCountry") as DropDownList;
            //    var creditCardExpirationDateDropDownList =
            //        formViewAccount.FindControl("MonthExpirationDate") as OptionGroupSelect;
            //    if (creditCardExpirationDateDropDownList != null)
            //    {
            //        creditCardExpirationDate = WebHelper.ParseDateWithMonthAndYear(creditCardExpirationDateDropDownList.SelectedValue);
            //    }
            //    if (subscriptionHiddenField == null)
            //    {
            //        Log.Error(GetType(), "The subscription ID for the account wasn't established");
            //        return;
            //    }
            //    RecurringBillingManager.UpdateSubscription(long.Parse(subscriptionHiddenField.Value), "SEOToolSet",
            //                                               cardNumberTextBox.Text, billToFullNameTextBox.Text,
            //                                               billToFullNameTextBox.Text,
            //                                               billToAddress1TextBox.Text + " " +
            //                                               billToAddress2TextBox.Text, billToCityTextBox.Text,
            //                                               billToStateTextBox.Text, billToZipTextBox.Text,
            //                                               billToCountryDropDownList.SelectedItem.Text,
            //                                               creditCardExpirationDate, 12, amount, trialOcurrences,
            //                                               trialDiscount);
            //}
            formViewAccount.UpdateItem(true);
        }

        protected void CustomRepeater1_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var cr = sender as CustomRepeater;
            if (cr == null) return;
            var sl = e.Item.DataItem as SubscriptionLevel;
            if (sl == null || sl.Id != UserAccount.SubscriptionLevel.Id) return;
            var radio = e.Item.FindControl("RadioButton1") as RadioButton;
            if (radio == null) return;
            radio.Checked = true;
            radio.Text += radio.Text = " <span class='Blue'>(CurrentSubscription)</span>";
        }

        protected void frmViewAccount_DataBound(object sender, EventArgs e)
        {
            var subscriptionInformationPanel = formViewAccount.FindControl("SubscriptionInformationPanel") as RoundPanel;
            if (subscriptionInformationPanel != null)
                subscriptionInformationPanel.Visible = UserAccount.SubscriptionLevel.Id != 4;
        }

        protected int GetReadOnlyUsers()
        {
            var users = SEOMembershipManager.GetUsersFromAccount(UserAccount, false);
            var readOnlyUsers = 0;
            foreach (var user in users)
            {
                if (user.UserRole.Name == "ReadOnly")
                    readOnlyUsers++;
            }
            return readOnlyUsers;
        }

        protected int GetPremiumUsers()
        {
            var users = SEOMembershipManager.GetUsersFromAccount(UserAccount, false);
            var premiumUsers = 0;
            foreach (var user in users)
            {
                if (user.UserRole.Name != "ReadOnly")
                    premiumUsers++;
            }
            return premiumUsers;
        }

        protected string GetLastBilling()
        {
            return UserAccount.LastBillingDate.HasValue
                       ? string.Format("{0} - ${1} USD", UserAccount.LastBillingDate.Value.ToShortDateString(),
                                       UserAccount.SubscriptionLevel.Price)
                       : "-";
        }

        protected string GetNextBilling()
        {
            return UserAccount.LastBillingDate.HasValue
                       ? string.Format(@"{0} - ${1} USD", UserAccount.LastBillingDate.Value.AddMonths(1),
                                       UserAccount.SubscriptionLevel.Price)
                       : "-";
        }
    }
}