#region Using Directives

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;
using System.Web.Security;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

#endregion

namespace SEOToolSet.Providers
{
    public static class AccountManager
    {
        private static readonly AccountProviderBase _defaultProvider;

        private static readonly AccountProviderCollection _providerCollection = new AccountProviderCollection();

        static AccountManager()
        {
            var providerConfiguration =
                ConfigurationManager.GetSection("AccountProvider") as AccountProviderConfiguration;

            if (providerConfiguration == null || providerConfiguration.DefaultProvider == null ||
                providerConfiguration.Providers == null || providerConfiguration.Providers.Count < 1)
                throw new ProviderException("You must specify a valid default provider for AccountProvider.");


            //Instantiate the providers
            ProvidersHelper.InstantiateProviders(providerConfiguration.Providers, _providerCollection,
                                                 typeof(AccountProviderBase));
            _providerCollection.SetReadOnly();
            _defaultProvider = _providerCollection[providerConfiguration.DefaultProvider];

            if (_defaultProvider != null)
                return;

            var defaultProviderProp =
                providerConfiguration.ElementInformation.Properties["defaultProvider"];

            if (defaultProviderProp != null)
                throw new ConfigurationErrorsException(
                    "You must specify a default provider for the AccountProvider.",
                    defaultProviderProp.Source,
                    defaultProviderProp.LineNumber);
            throw new ConfigurationErrorsException("You must specify a default Provider for the AccountProvider");
        }

        public static AccountProviderBase Provider
        {
            get
            {
                if (_defaultProvider != null)
                    return _defaultProvider;
                throw new ProviderException("You must specify a valid default provider for AccountProvider.");
            }
        }

        public static AccountProviderCollection Providers
        {
            get { return _providerCollection; }
        }


        public static String TopLevelAccountName
        {
            get { return Provider.TopLevelAccountName; }
        }


        public static void CreateAccountAndUser(Account account, SEOToolsetUser user)
        {
            using (var tran = new TransactionScope(Provider.ConnectionString))
            {
                if (account.CompanyIdCountry.HasValue && account.CompanyIdCountry == -1)
                    account.CompanyIdCountry = null;
                if (user.Country != null && user.Country.Id == -1)
                    user.Country = null;
                Provider.CreateAccount(account);

                user.Account = account;

                MembershipCreateStatus status;

                SEOMembershipManager.CreateUser(user, out status);

                if (status != MembershipCreateStatus.Success)
                    throw new MembershipCreateUserException(status);
                SEORolesManager.AddUserToRole(user.Login, "Administrator");
                tran.Commit();
            }
        }

        public static Account FindAccountByName(string accountName)
        {
            return Provider.GetAccountByName(accountName);
        }

        public static bool ExistsAccount(string accountName)
        {
            return Provider.GetAccountByName(accountName) != null;
        }

        public static IList<CreditCardType> GetCreditCardTypes()
        {
            return Provider.GetCreditCardTypes();
        }

        public static CreditCardType GetCreditCardTypeById(String id)
        {
            return Provider.GetCreditCardTypeById(id);
        }

        public static bool IsAccountNameAvailable(String accountName)
        {
            return Provider.IsAccountNameAvailable(accountName);
        }

        #region AccountProvider Methods

        public static void CreateAccount(out int id, string name, Account owner)
        {
            Provider.CreateAccount(out id, name, owner);
        }

        public static void CreateAccount(Account account)
        {
            Provider.CreateAccount(account);
        }

        public static void CreateAccount(out int id, string name, int? maxNumberOfUser, int? maxNumberOfDomainUser,
                                         int? maxNumberOfProjects, string companyName, string companyAddress1,
                                         string companyAddress2, string companyCity, string companyState,
                                         string companyZip, string creditCardNumber, CreditCardType creditCardType,
                                         string creditCardAddress1, string creditCardAddress2, string creditCardCity,
                                         string creditCardZip, bool? recurringBill, DateTime? accountExpirationDate,
                                         DateTime? creditCardExpiration, Account owner, string creditCardEmail,
                                         int? creditCardIdCountry, string creditCardState, string creditCardCardholder,
                                         string promoCode, Int32? companyIdCountry, string creditCardCvs,
                                         string companyPhone, string creditCardPhone, int? subscriptionLevelId, long? subscriptionId)
        {
            Provider.CreateAccount(out id, name, maxNumberOfUser, maxNumberOfDomainUser,
                                   maxNumberOfProjects, companyName, companyAddress1, companyAddress2,
                                   companyCity, companyState, companyZip,
                                   creditCardNumber, creditCardType,
                                   creditCardAddress1, creditCardAddress2, creditCardCity, creditCardZip,
                                   recurringBill, accountExpirationDate, creditCardExpiration,
                                   owner, creditCardEmail, creditCardIdCountry, creditCardState,
                                   creditCardCardholder, promoCode, companyIdCountry, creditCardCvs, companyPhone, creditCardPhone, subscriptionLevelId, subscriptionId);
        }

        public static void UpdateAccount(int id, int? maxNumberOfUser, int? maxNumberOfDomainUser,
                                         int? maxNumberOfProjects, string companyName, string companyAddress1,
                                         string companyAddress2, string companyCity, string companyState,
                                         string companyZip, string creditCardNumber, CreditCardType creditCardType,
                                         string creditCardAddress1, string creditCardAddress2, string creditCardCity,
                                         string creditCardZip, bool? recurringBill, bool? enabled,
                                         DateTime? accountExpirationDate, DateTime? creditCardExpiration, Account owner,
                                         string creditCardEmail, int? creditCardIdCountry, string creditCardState,
                                         string creditCardCardholder, string promoCode, Int32? companyIdCountry,
                                         String creditCardCvs, String companyPhone, String creditCardPhone, int? subscriptionLevelId)
        {
            Provider.UpdateAccount(id, maxNumberOfUser, maxNumberOfDomainUser, maxNumberOfProjects, companyName,
                                   companyAddress1, companyAddress2, companyCity, companyState, companyZip,
                                   creditCardNumber,
                                   creditCardType,
                                   creditCardAddress1, creditCardAddress2, creditCardCity, creditCardZip, recurringBill,
                                   enabled,
                                   accountExpirationDate, creditCardExpiration,
                                   owner, creditCardEmail, creditCardIdCountry, creditCardState, creditCardCardholder,
                                   promoCode, companyIdCountry,
                                   creditCardCvs, companyPhone, creditCardPhone, subscriptionLevelId);
        }

        public static void DeleteAccount(int id)
        {
            Provider.DeleteAccount(id);
        }

        public static Account GetAccount(int Id)
        {
            return Provider.GetAccount(Id);
        }

        public static IList<Account> GetAccounts()
        {
            throw new NotImplementedException();
        }

        public static void AssociateAccountToUser(Account account, SEOToolsetUser user)
        {
            throw new NotImplementedException();
        }

        public static void AssociateAccountToUser(string accountName, string userName)
        {
            throw new NotImplementedException();
        }

        public static void AssociateAccountToUser(int IdAccount, int IdUser)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}