using System;
using System.Configuration.Provider;

namespace SEOToolSet.Providers
{
    ///<summary>
    ///Provider for handle Recurring Billing related operations
    ///</summary>
    public abstract class RecurringBillingProviderBase:ProviderBase
    {
        ///<summary>
        ///Create a recurring billing subscription
        ///</summary>
        ///<returns>Returns the generated identification of the subscription</returns>
        public abstract long CreateSubscription(string subscriptionName, string cardNumber, string billToFirstName, string billToLastName, string billToAddress, string expirationDateText, string billToState, string billToZip, string billToCountry, DateTime? paymentStartDate, string creditCardExpirationDateText, short? paymentTotalOccurrences, decimal amount, short? paymentInterval, string paymentPeriodUnit, short? trialOccurrences, decimal? trialAmount);
        
        ///<summary>
        ///Updates the recurring billing subscription
        ///</summary>
        ///<exception cref="ApplicationException">If there was an error on updating, it
        ///throws an exception detailing the problem</exception>
        public abstract void UpdateSubscription(long subscriptionId, string subscriptionName, string cardNumber, string billToFirstName, string billToLastName, string billToAddress, string billToCity, string billToState, string billToZip, string billToCountry, DateTime? paymentStartDate, string expirationDateText, short? paymentTotalOccurrences, decimal amount, short? trialOccurrences, decimal? trialAmount);

        ///<summary>
        ///Cancels the indicated subscription
        ///</summary>
        ///<param name="subscriptionId">The id that identifies the subscription to cancel
        ///</param>
        ///<returns>Returns True if the subscription was successfully canceled. Otherwise,
        ///returns False.</returns>
        public abstract bool CancelSubscription(long subscriptionId);
    }
}
