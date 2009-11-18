#region

using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Text;
using LoggerFacade;
using SEOToolSet.Providers.AuthorizeNet.ArbApiSoap;

#endregion

namespace SEOToolSet.Providers.AuthorizeNet
{
    ///<summary>
    ///Implementation of <see cref="RecurringBillingProviderBase"/> with Authorize.Net technologies
    ///</summary>
    public class AuthorizeNetRecurringBillingProvider : RecurringBillingProviderBase
    {
        // This is the TransactionKey associated with that account.
        private static readonly Service _webService = new Service();
        private string _filePath;
        private string _providerName;
        private string _transactionKey;
        private string _userLoginName;


        ///<summary>
        ///
        ///                    Gets the friendly name used to refer to the provider during configuration.
        ///                
        ///</summary>
        ///
        ///<returns>
        ///
        ///                    The friendly name used to refer to the provider during configuration.
        ///                
        ///</returns>
        ///
        public override string Name
        {
            get { return _providerName; }
        }

        ///<summary>
        ///
        ///                    Initializes the provider.
        ///                
        ///</summary>
        ///
        ///<param name="name">
        ///                    The friendly name of the provider.
        ///                </param>
        ///<param name="config">
        ///                    A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.
        ///                </param>
        ///<exception cref="T:System.ArgumentNullException">
        ///                    The name of the provider is null.
        ///                </exception>
        ///<exception cref="T:System.ArgumentException">
        ///                    The name of the provider has a length of zero.
        ///                </exception>
        ///<exception cref="T:System.InvalidOperationException">
        ///                    An attempt is made to call <see cref="M:System.Configuration.Provider.ProviderBase.Initialize(System.String,System.Collections.Specialized.NameValueCollection)" /> on a provider after the provider has already been initialized.
        ///                </exception>
        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (name.Length == 0)
                name = "ProjectProvider";

            base.Initialize(name, config);

            _providerName = name;

            _filePath = ConfigHelper.ExtractConfigValue(config, "filePath", null);
            _transactionKey = ConfigHelper.ExtractConfigValue(config, "transactionKey", null);
            _userLoginName = ConfigHelper.ExtractConfigValue(config, "userLoginName", null);

            if (config.Count == 0)
                return;

            if (_filePath == "Default")
                _filePath = AppDomain.CurrentDomain.BaseDirectory;


            // Throw an exception if unrecognized attributes remain
            var attr = config.GetKey(0);
            if (!String.IsNullOrEmpty(attr))
                throw new ProviderException("Unrecognized attribute: " + attr);
        }

        ///<summary>
        ///Create a recurring billing subscription
        ///</summary>
        ///<returns>Returns the generated identification of the subscription</returns>
        public override long CreateSubscription(string subscriptionName, string cardNumber,
                                                string billToFirstName, string billToLastName,
                                                string billToAddress, string billToCity, string billToState,
                                                string billToZip, string billToCountry, 
                                                DateTime? paymentStartDate, string creditCardExpirationDateText,
                                                short? paymentTotalOccurrences, decimal amount,
                                                short? paymentInterval, string paymentPeriodUnit,
                                                short? trialOccurrences, decimal? trialAmount)
        {
            var subscriptionId = 0L;

            var authentication = populateMerchantAuthentication();

            var subscription = new ARBSubscriptionType();
            populateSubscription(subscription, false, subscriptionName, cardNumber, billToFirstName, billToLastName,
                                 billToAddress, billToCity, billToState, billToZip, billToCountry,
                                 paymentStartDate, creditCardExpirationDateText, paymentTotalOccurrences, amount,
                                 paymentInterval, (ARBSubscriptionUnitEnum)Enum.Parse(typeof(ARBSubscriptionUnitEnum), paymentPeriodUnit),
                                 trialOccurrences, trialAmount);

            var response = _webService.ARBCreateSubscription(authentication, subscription);

            if (response.resultCode == MessageTypeEnum.Ok)
                subscriptionId = response.subscriptionId;
            if (subscriptionId <= 0)
            {
                var msg = new StringBuilder();
                foreach (var message in response.messages)
                {
                    msg.AppendLine(string.Format("Error code: '{0}'", message.code));
                    msg.AppendLine(string.Format("Details: '{0}' ", message.text));
                }
                throw new ApplicationException(
                    string.Format("There was a problem with the creation of subscription. Details: {0}", msg));
            }
            Log.Debug(GetType(), string.Format("A subscription with an ID of '{0}' was successfully created.", subscriptionId));
            return subscriptionId;
        }

        ///<summary>
        ///Updates the recurring billing subscription
        ///</summary>
        ///<exception cref="ApplicationException">If there was an error on updating, it
        ///throws an exception detailing the problem</exception>
        public override void UpdateSubscription(long subscriptionId, string subscriptionName, string cardNumber,
                                                string billToFirstName, string billToLastName,
                                                string billToAddress, string billToCity, string billToState,
                                                string billToZip, string billToCountry, 
                                                DateTime? paymentStartDate, string expirationDateText,
                                                short? paymentTotalOccurrences, decimal amount,
                                                short? trialOccurrences, decimal? trialAmount)
        {
            var authentication = populateMerchantAuthentication();

            var subscription = new ARBSubscriptionType();
            populateSubscription(subscription, true, subscriptionName, cardNumber, billToFirstName, billToLastName,
                                 billToAddress, billToCity, billToState, billToZip, billToCountry,
                                 paymentStartDate, expirationDateText, paymentTotalOccurrences, amount, null, null,
                                 trialOccurrences, trialAmount);

            var response = _webService.ARBUpdateSubscription(authentication,
                                                             subscriptionId, subscription);

            if (response.resultCode != MessageTypeEnum.Error)
            {
                var msgs = response.messages;
                var messages = new StringBuilder(msgs.Length);
                foreach (var msg in msgs)
                {
                    messages.AppendFormat("Code: {0}", msg.code);
                    messages.AppendLine(msg.text);
                }

                throw new ApplicationException("The subscription throws an error! Error detail:" + messages);
            }
#if DEBUG
            var responseResult = subscriptionId <= 0
                                     ? response.ToString()
                                     : string.Format("A subscription with an ID of '{0}' was successfully created.",
                                                     subscriptionId);
            Log.Debug(GetType(), responseResult);
#endif
        }

        ///<summary>
        ///Cancels the indicated subscription
        ///</summary>
        ///<param name="subscriptionId">The id that identifies the subscription to cancel
        ///</param>
        ///<returns>Returns True if the subscription was successfully canceled. Otherwise,
        ///returns False.</returns>
        public override bool CancelSubscription(long subscriptionId)
        {
            var bResult = true;

            Console.WriteLine("\r\nCancel subscription");

            var authentication = populateMerchantAuthentication();

            var response = _webService.ARBCancelSubscription(authentication, subscriptionId);

            if (response.resultCode == MessageTypeEnum.Ok)
            {
                Console.WriteLine("The subscription was successfully canceled.");
            }
            else
            {
                bResult = false;
                Log.Error(GetType(), response.ToString());
            }

            return bResult;
        }

        private static void populateSubscription(ARBSubscriptionType sub, bool isForUpdate, string accountName,
                                                 string cardNumber, string billToFirstName, string billToLastName,
                                                 string billToAddress, string billToCity, string billToState,
                                                 string billToZip, string billToCountry,
                                                 DateTime? paymentStartDate, string creditCardExpirationDateText,
                                                 short? paymentTotalOcurrences, decimal? amount, short? paymentInterval,
                                                 ARBSubscriptionUnitEnum? paymentPeriodUnit,
                                                 short? trialOccurrences, decimal? trialAmount)
        {
            sub.name = accountName;
            var creditCard = new CreditCardType { cardNumber = cardNumber, expirationDate = creditCardExpirationDateText };
            sub.payment = new PaymentType { Item = creditCard };

            sub.billTo = new NameAndAddressType
                             {
                                 firstName = billToFirstName,
                                 lastName = billToLastName,
                                 address = billToAddress,
                                 city = billToCity,
                                 state = billToState,
                                 zip = billToZip,
                                 country = billToCountry
                             };

            sub.paymentSchedule = new PaymentScheduleType
                                      {
                                          startDateSpecified = paymentStartDate.HasValue,
                                          trialOccurrencesSpecified = trialOccurrences.HasValue
                                      };

            if (paymentStartDate.HasValue)
                sub.paymentSchedule.startDate = paymentStartDate.Value;

            sub.paymentSchedule.totalOccurrencesSpecified = paymentTotalOcurrences.HasValue;
            if (paymentTotalOcurrences.HasValue)
                sub.paymentSchedule.totalOccurrences = paymentTotalOcurrences.Value;

            sub.amountSpecified = amount.HasValue;
            if (amount.HasValue)
                sub.amount = amount.Value;

            if (paymentInterval.HasValue)
            {
                if (isForUpdate)
                    throw new ApplicationException(
                        "Interval of the payment schedule cannot be updated once a subscription is created");
                if (paymentInterval.Value < 1)
                    throw new ApplicationException(
                        "The interval length of the payment schedule must be a positive number greater than zero (0)");
                if (paymentPeriodUnit.Value == ARBSubscriptionUnitEnum.months && paymentInterval.Value > 12)
                    throw new ApplicationException(
                        "If the Interval Unit of the payment schedule is months, it cannot be any number out of the range between one (1) and twelve (12)");
                if (paymentPeriodUnit.Value == ARBSubscriptionUnitEnum.days && paymentInterval.Value > 365)
                    throw new ApplicationException(
                        "If the Interval Unit of the payment schedule is days, it cannot be any number out of the range between one (1) and 365");
                sub.paymentSchedule.interval = new PaymentScheduleTypeInterval
                                                   {
                                                       length = paymentInterval.Value,
                                                       unit = paymentPeriodUnit.Value
                                                   };
            }

            sub.trialAmountSpecified = trialOccurrences.HasValue;
            if (trialOccurrences.HasValue)
            {
                sub.trialAmount = trialAmount.Value;
                sub.paymentSchedule.trialOccurrences = trialOccurrences.Value;
            }
        }

        private MerchantAuthenticationType populateMerchantAuthentication()
        {
            return new MerchantAuthenticationType { name = _userLoginName, transactionKey = _transactionKey };
        }
    }
}