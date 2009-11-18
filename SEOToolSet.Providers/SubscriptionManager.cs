using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;
using SEOToolSet.Entities;

namespace SEOToolSet.Providers
{
    public class SubscriptionManager
    {
        private static readonly SubscriptionProviderBase DefaultProvider;
        private static readonly SubscriptionProviderCollection ProviderCollection =
            new SubscriptionProviderCollection();

        static SubscriptionManager()
        {
            var providerConfiguration =
                ConfigurationManager.GetSection("SubscriptionProvider") as SubscriptionProviderConfiguration;

            if (providerConfiguration == null || providerConfiguration.DefaultProvider == null ||
                providerConfiguration.Providers == null || providerConfiguration.Providers.Count < 1)
                throw new ProviderException("You must specify a valid default provider for SubscriptionProvider.");


            //Instantiate the providers
            ProvidersHelper.InstantiateProviders(providerConfiguration.Providers, ProviderCollection,
                                                 typeof(SubscriptionProviderBase));
            ProviderCollection.SetReadOnly();
            DefaultProvider = ProviderCollection[providerConfiguration.DefaultProvider];

            if (DefaultProvider != null)
                return;

            var defaultProviderProp =
                providerConfiguration.ElementInformation.Properties["defaultProvider"];

            if (defaultProviderProp != null)
                throw new ConfigurationErrorsException(
                    "You must specify a default provider for the Subscription.",
                    defaultProviderProp.Source,
                    defaultProviderProp.LineNumber);
            throw new ConfigurationErrorsException("You must specify a default Provider for the Subscription");
        }



        public static SubscriptionProviderBase Provider
        {
            get
            {
                if (DefaultProvider != null)
                    return DefaultProvider;
                throw new ProviderException("You must specify a valid default provider for SubscriptionProvider.");
            }
        }

        public static String GetSubscriptionPropertyValue(Account account, string propertyName)
        {
            var properties = (List<SubscriptionDetail>)Provider.GetSubscriptionDetails(account.Id);

            var prop = properties.Find(detail => detail.SubscriptionProperty != null && detail.SubscriptionProperty.Name == propertyName);
            
            return prop != null ? prop.PropertyValue : null;
        }


        public static SubscriptionLevel GetSubscriptionLevel(int id)
        {
            return Provider.GetSubscriptionLevel(id);
        }

        public static SubscriptionLevel GetSubscriptionLevelByName(string name)
        {
            return Provider.GetSubscriptionLevelByName(name);
        }

        public static IList<SubscriptionLevel> GetSubscriptionLevels()
        {
            return Provider.GetSubscriptionLevels();
        }

        public static IList<SubscriptionLevel> GetSubscriptionLevelsForSignUp()
        {
            return Provider.GetSubscriptionLevelsForSignUp();
        }

        public static int CreateSubscriptionLevel(string subscriptionLevel, Role role, double price)
        {
            return Provider.CreateSubscriptionLevel(subscriptionLevel, role, price);
        }

        public static IList<SubscriptionDetail> GetSubscriptionDetails(int accountId)
        {
            return Provider.GetSubscriptionDetails(accountId);
        }

        public static int CreateSubscriptionProperty(string propertyName)
        {
            return Provider.CreateSubscriptionProperty(propertyName);
        }

        public static void CreateSubscriptionDetails(int subscriptionId, IDictionary<int, string> propertyValues)
        {
            Provider.CreateSubscriptionDetails(subscriptionId, propertyValues);
        }
    }
}
