using System;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;
using SEOToolSet.Entities;
using PromoType = SEOToolSet.Entities.Wrappers.PromoType;

namespace SEOToolSet.Providers
{
    ///<summary>
    ///Manages the operations related to the <c>PromoCodeProvider</c>
    ///</summary>
    public class PromoCodeManager
    {
        private static readonly PromoCodeProviderBase _defaultProvider;

        private static readonly PromoCodeProviderCollection _providerCollection =
            new PromoCodeProviderCollection();

        static PromoCodeManager()
        {
            var providerConfiguration =
                ConfigurationManager.GetSection("PromoCodeProvider") as PromoCodeProviderConfiguration;

            if (providerConfiguration == null || providerConfiguration.DefaultProvider == null ||
                providerConfiguration.Providers == null || providerConfiguration.Providers.Count < 1)
                throw new ProviderException("You must specify a valid default provider for PromoCodeProvider.");


            //Instantiate the providers
            ProvidersHelper.InstantiateProviders(providerConfiguration.Providers, _providerCollection,
                                                 typeof(PromoCodeProviderBase));
            _providerCollection.SetReadOnly();
            _defaultProvider = _providerCollection[providerConfiguration.DefaultProvider];

            if (_defaultProvider != null)
                return;

            PropertyInformation defaultProviderProp =
                providerConfiguration.ElementInformation.Properties["defaultProvider"];

            if (defaultProviderProp != null)
                throw new ConfigurationErrorsException(
                    "You must specify a default provider for the PromoCode.",
                    defaultProviderProp.Source,
                    defaultProviderProp.LineNumber);
            throw new ConfigurationErrorsException("You must specify a default Provider for the PromoCode");
        }

        ///<summary>
        ///The instance of the default provider
        ///</summary>
        ///<exception cref="ProviderException"></exception>
        public static PromoCodeProviderBase Provider
        {
            get
            {
                if (_defaultProvider != null)
                    return _defaultProvider;
                throw new ProviderException("You must specify a valid default provider for PromoCodeProvider.");
            }
        }

        ///<summary>
        ///Check if the promotion code is valid
        ///</summary>
        ///<param name="promoCode"></param>
        ///<param name="subscriptionLevelId"></param>
        ///<param name="accountId"></param>
        ///<returns>Returns the status of the promotion (valid, not valid, expired, not applies, etc.)</returns>
        ///<exception cref="NotSupportedException"></exception>
        public static PromoCodeStatus Validate(string promoCode, int subscriptionLevelId, int? accountId)
        {
            var promoStatus = new PromoCodeStatus();
            var promo = Provider.FindByCode(promoCode);
            var subscription = SubscriptionManager.GetSubscriptionLevel(subscriptionLevelId);
            if (promo == null)
            {
                promoStatus.StatusCode = StatusCode.NotValid; return promoStatus;
            }
            if (promo.AccountType.Id != (int)AccountType.Both)
            {
                if (!accountId.HasValue && promo.AccountType.Id != (int)AccountType.New)
                {
                    promoStatus.StatusCode = StatusCode.AccountTypeOnlyExisting; return promoStatus;
                }
                if (accountId.HasValue && promo.AccountType.Id != (int)AccountType.Existing)
                {
                    promoStatus.StatusCode = StatusCode.AccountTypeOnlyNew; return promoStatus;
                }
            }
            if (promo.BeginDate.HasValue && promo.BeginDate.Value.CompareTo(DateTime.Now) >= 0)
            {
                promoStatus.StatusCode = StatusCode.NotYetValid;
                promoStatus.ReferenceDate = promo.BeginDate.HasValue ? promo.BeginDate.Value.ToShortDateString() : null;
                return promoStatus;
            }
            if (promo.EndDate.HasValue && promo.EndDate.Value.CompareTo(DateTime.Now) <= 0)
            {
                promoStatus.StatusCode = StatusCode.Expired;
                promoStatus.ReferenceDate = promo.EndDate.HasValue ? promo.EndDate.Value.ToShortDateString() : null;
                return promoStatus;
            }
            if (promo.MaxUse.HasValue && promo.TimesUsed.Value >= promo.MaxUse.Value)
            {
                promoStatus.StatusCode = StatusCode.MaxUseExceeded; return promoStatus;
            }
            promoStatus.StatusCode = StatusCode.Found;
            switch (promo.PromoType.Id)
            {
                case (int)PromoType.Fixed:
                    promoStatus.Discount = promo.PromoAmount.Value;
                    break;
                case (int)PromoType.Percentage:
                    promoStatus.Discount = subscription.Price * promo.PromoAmount.Value;
                    break;
                default:
                    throw new NotSupportedException("The promotion type is not defined");
            }
            promoStatus.PromoCodeDescription = promo.Description;
            return promoStatus;
        }

        ///<summary>
        ///Returns the Promotion by its promo code if it is found
        ///</summary>
        ///<param name="promoCode">The code of the promotion</param>
        ///<returns>The promotion object </returns>
        /// <remarks>For security reasons, this method should not be exposed to the client tier (using handlers)</remarks>
        public static PromoCode GetByCode(string promoCode)
        {
            return Provider.FindByCode(promoCode);
        }

        ///<summary>
        ///Applies for the promotion (if it has maximum times of use, then the UsedTimes field is increased)
        ///</summary>
        ///<param name="promoCode">The code of the promotion</param>
        public static void Consume(string promoCode)
        {
            Provider.Consume(promoCode);
        }
    }
}