using System;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;

namespace SEOToolSet.Providers
{
    ///<summary>
    ///Manages the operations related to the <c>RecurringBillingProvider</c>
    ///</summary>
    public class RecurringBillingManager
    {
        private static readonly RecurringBillingProviderBase _defaultProvider;

        private static readonly RecurringBillingProviderCollection _providerCollection =
            new RecurringBillingProviderCollection();

        static RecurringBillingManager()
        {
            var providerConfiguration =
                ConfigurationManager.GetSection("RecurringBillingProvider") as RecurringBillingProviderConfiguration;

            if (providerConfiguration == null || providerConfiguration.DefaultProvider == null ||
                providerConfiguration.Providers == null || providerConfiguration.Providers.Count < 1)
                throw new ProviderException("You must specify a valid default provider for RecurringBillProvider.");


            //Instantiate the providers
            ProvidersHelper.InstantiateProviders(providerConfiguration.Providers, _providerCollection,
                                                 typeof(RecurringBillingProviderBase));
            _providerCollection.SetReadOnly();
            _defaultProvider = _providerCollection[providerConfiguration.DefaultProvider];

            if (_defaultProvider != null)
                return;

            PropertyInformation defaultProviderProp =
                providerConfiguration.ElementInformation.Properties["defaultProvider"];

            if (defaultProviderProp != null)
                throw new ConfigurationErrorsException(
                    "You must specify a default provider for the RecurringBilling.",
                    defaultProviderProp.Source,
                    defaultProviderProp.LineNumber);
            throw new ConfigurationErrorsException("You must specify a default Provider for the RecurringBilling");
        }

        ///<summary>
        ///The instance of the default provider
        ///</summary>
        ///<exception cref="ProviderException"></exception>
        public static RecurringBillingProviderBase Provider
        {
            get
            {
                if (_defaultProvider != null)
                    return _defaultProvider;
                throw new ProviderException("You must specify a valid default provider for RecurringBillingProvider.");
            }
        }

        ///<summary>
        ///Create the recurring billing subscription
        ///</summary>
        ///<param name="subscriptionName"></param>
        ///<param name="cardNumber"></param>
        ///<param name="billToFirstName"></param>
        ///<param name="billToLastName"></param>
        ///<param name="billToAddress"></param>
        ///<param name="billToCity"></param>
        ///<param name="billToState"></param>
        ///<param name="billToZip"></param>
        ///<param name="billToCountry"></param>
        ///<param name="creditCardExpirationDate"></param>
        ///<param name="paymentTotalOccurrences"></param>
        ///<param name="amount"></param>
        ///<param name="paymentInterval"></param>
        ///<param name="paymentPeriodUnit"></param>
        ///<param name="trialOccurrences"></param>
        ///<param name="trialAmount"></param>
        ///<returns></returns>
        public static long CreateSubscription(string subscriptionName, string cardNumber,
                                                string billToFirstName, string billToLastName,
                                                string billToAddress, string billToCity, string billToState,
                                                string billToZip, string billToCountry, DateTime? creditCardExpirationDate,
                                                short? paymentTotalOccurrences, decimal amount,
                                                short? paymentInterval, string paymentPeriodUnit,
                                                short? trialOccurrences, decimal? trialAmount)
        {
            return Provider.CreateSubscription(subscriptionName, cardNumber, billToFirstName, billToLastName,
                                               billToAddress, billToCity, billToState, billToZip, billToCountry,
                                               DateTime.Now, getCreditCardExpirationDateText(creditCardExpirationDate), paymentTotalOccurrences,
                                               amount, paymentInterval, paymentPeriodUnit, trialOccurrences, trialAmount);
        }

        ///<summary>
        ///Updates the indicated subscription
        ///</summary>
        ///<param name="subscriptionId"></param>
        ///<param name="subscriptionName"></param>
        ///<param name="cardNumber"></param>
        ///<param name="billToFirstName"></param>
        ///<param name="billToLastName"></param>
        ///<param name="billToAddress"></param>
        ///<param name="billToCity"></param>
        ///<param name="billToState"></param>
        ///<param name="billToZip"></param>
        ///<param name="billToCountry"></param>
        ///<param name="creditCardExpirationDate"></param>
        ///<param name="paymentTotalOccurrences"></param>
        ///<param name="amount"></param>
        ///<param name="trialOccurrences"></param>
        ///<param name="trialAmount"></param>
        public static void UpdateSubscription(long subscriptionId, string subscriptionName, string cardNumber,
                                                string billToFirstName, string billToLastName,
                                                string billToAddress, string billToCity, string billToState,
                                                string billToZip, string billToCountry, DateTime? creditCardExpirationDate,
                                                short? paymentTotalOccurrences, decimal amount,
                                                short? trialOccurrences, decimal? trialAmount)
        {
            Provider.UpdateSubscription(subscriptionId, subscriptionName, cardNumber, billToFirstName, billToLastName,
                                               billToAddress, billToCity, billToState, billToZip, billToCountry,
                                               DateTime.Now, getCreditCardExpirationDateText(creditCardExpirationDate), paymentTotalOccurrences,
                                               amount, trialOccurrences, trialAmount);
        }

        ///<summary>
        ///Cancels the indicated subscription
        ///</summary>
        ///<param name="subscriptionId"></param>
        ///<returns></returns>
        public static bool CancelSubscription(long subscriptionId)
        {
            return Provider.CancelSubscription(subscriptionId);
        }

        private static string getCreditCardExpirationDateText(DateTime? creditCardExpirationDate)
        {
            string creditCardExpirationDateText=null;
            if(creditCardExpirationDate.HasValue)
            {
                creditCardExpirationDateText = creditCardExpirationDate.Value.ToString("yyyy-MM");
            }
            return creditCardExpirationDateText;
        }

    }
}
