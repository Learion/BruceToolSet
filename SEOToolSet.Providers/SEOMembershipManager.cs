#region Using Directives

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;
using System.Web.Security;
using SEOToolSet.Entities;

#endregion

namespace SEOToolSet.Providers
{
    public static class SEOMembershipManager
    {
        private static readonly SEOMembershipProviderBase _defaultProvider;

        private static readonly SEOMembershipProviderCollection _providerCollection =
            new SEOMembershipProviderCollection();

        static SEOMembershipManager()
        {
            var providerConfiguration =
                ConfigurationManager.GetSection("SEOMembershipProvider") as SEOMembershipProviderConfiguration;

            if (providerConfiguration == null || providerConfiguration.DefaultProvider == null ||
                providerConfiguration.Providers == null || providerConfiguration.Providers.Count < 1)
                throw new ProviderException("You must specify a valid default provider for SEOMembershipProvider.");


            //Instantiate the providers
            ProvidersHelper.InstantiateProviders(providerConfiguration.Providers, _providerCollection,
                                                 typeof(SEOMembershipProviderBase));
            _providerCollection.SetReadOnly();
            _defaultProvider = _providerCollection[providerConfiguration.DefaultProvider];

            if (_defaultProvider != null) return;

            var defaultProviderProp = providerConfiguration.ElementInformation.Properties["defaultProvider"];

            if (defaultProviderProp != null)
                throw new ConfigurationErrorsException(
                    "You must specify a default provider for the SEOMembershipProvider.",
                    defaultProviderProp.Source,
                    defaultProviderProp.LineNumber);
            throw new ConfigurationErrorsException("You must specify a default Provider for the SEOMembershipProvider");
        }

        #region SEOMembershipProviderBase Methods

        public static SEOToolsetUser GetUser(string userName)
        {
            return Provider.GetUser(userName);
        }

        public static SEOToolsetUser GetUserById(int id)
        {
            return Provider.GetUserById(id);
        }

        public static void CreateUser(out Int32 id
                                      , String firstName
                                      , String lastName
                                      , String email
                                      , String address1
                                      , String address2
                                      , String cityTown
                                      , String state
                                      , String zip
                                      , String telephone
                                      , String login
                                      , String password
                                      , String passwordQuestion
                                      , String passwordAnswer
                                      , Account account
                                      , Country country
                                      , Role userRole
                                      , out MembershipCreateStatus status)
        {
            Provider.CreateUser(out id, firstName, lastName, email, address1, address2, cityTown, state, zip, telephone,
                                login, password, passwordQuestion, passwordAnswer, account, country, userRole,
                                out status);
        }

        public static void CreateUser(out Int32 id
                                      , String firstName
                                      , String lastName
                                      , String email
                                      , String address1
                                      , String address2
                                      , String cityTown
                                      , String state
                                      , String zip
                                      , String telephone
                                      , String login
                                      , Account account
                                      , Country country
                                      , Role userRole
                                      , out MembershipCreateStatus status, out string password)
        {
            password = Membership.GeneratePassword(6, 0);
            Provider.CreateUser(out id, firstName, lastName, email, address1, address2, cityTown, state, zip, telephone,
                                login, password, null, null, account, country, userRole,
                                out status);
        }

        public static void UpdateUser(int id,
                                      string firstName,
                                      string lastName,
                                      string email,
                                      string address1,
                                      string address2,
                                      string cityTown,
                                      string state,
                                      string zip,
                                      string telephone,
                                      string login,
                                      string password,
                                      string passwordQuestion,
                                      string passwordAnswer,
                                      bool? enabled,
                                      Account account,
                                      Country country,
                                      Role userRole)
        {
            Provider.UpdateUser(id, firstName, lastName, email, address1, address2, cityTown, state, zip, telephone,
                                login, password, passwordQuestion, passwordAnswer, enabled, account, country, userRole);
        }

        public static void UpdateUser(int id,
                                      string firstName,
                                      string lastName,
                                      string email,
                                      string address1,
                                      string address2,
                                      string cityTown,
                                      string state,
                                      string zip,
                                      string telephone,
                                      string login,
                                      bool? enabled,
                                      Account account,
                                      Country country,
                                      Role userRole)
        {
            Provider.UpdateUser(id, firstName, lastName, email, address1, address2, cityTown, state, zip, telephone,
                                login, null, null, null, enabled, account, country, userRole);
        }

        public static void CreateUser(SEOToolsetUser user, out MembershipCreateStatus status)
        {
            Provider.CreateUser(user, out status);
        }

        public static IList<SEOToolsetUser> GetUsersFromAccount(Account account, bool? includeInactive)
        {
            return Provider.GetUsersFromAccount(account, includeInactive);
        }

        public static IList<SEOToolsetUser> GetUsersFromAccount(string accountName, bool? includeInactive)
        {
            return Provider.GetUsersFromAccount(accountName, includeInactive);
        }

        public static void AddSubscription(string accountName, string serviceName)
        {
            throw new NotImplementedException();
        }

        public static void RemoveSubscription(string accountName, string serviceName)
        {
            throw new NotImplementedException();
        }

        /*public static void AddSubscription(Account account, Service service)
        {
            throw new NotImplementedException();
        } */

        /*public static void RemoveSubscription(Account account, Service service)
        {
            throw new NotImplementedException();
        }
          */
        public static IList<Country> GetCountries()
        {
            return Provider.GetCountries();
        }

        public static Country GetCountryById(int id)
        {
            return Provider.GetCountryById(id);
        }

        public static void DeleteUser(int id)
        {
            Provider.DeleteUser(id);
        }

        #endregion

        public static SEOMembershipProviderBase Provider
        {
            get
            {
                if (_defaultProvider != null) return _defaultProvider;
                throw new ProviderException("You must specify a valid default provider for SEORoleProvider.");
            }
        }

        public static SEOMembershipProviderCollection Providers
        {
            get { return _providerCollection; }
        }

        public static IList<SEOToolsetUser> GetUsersFromAccountWithSortAndPaging(Account account, bool? includeInactive, bool asc, string fieldName, out Int32 count, Int32 pageSize, Int32 currentPageNumber)
        {
            return Provider.GetUsersFromAccountWithSortAndPaging(account, includeInactive, asc, fieldName, out count, pageSize,
                                                      currentPageNumber);
        }

        public static IList<SEOToolsetUser> GetUsersFromAccountSortBy(Account account, bool? includeInactive, bool asc, string fieldName)
        {
            return Provider.GetUsersFromAccountSortBy(account, includeInactive, asc, fieldName);
        }

        internal static void UpdateUser(SEOToolsetUser user)
        {
            Provider.UpdateUser(user);
        }

        public static Int32? GetUsersCountOtherThanReadOnly(Account account)
        {
            //TODO: Convert this to HQL Query
            var users = (List<SEOToolsetUser>)Provider.GetUsersFromAccount(account, false);
            if (users == null) return 0;
            var usersNotReadOnly = users.FindAll(user => user.UserRole.Name != "ReadOnly");
            return usersNotReadOnly.Count;
        }

        public static Int32? GetReadOnlyUsers(Account account)
        {
            //TODO: Convert this to HQL Query
            var users = (List<SEOToolsetUser>)Provider.GetUsersFromAccount(account, false);
            if (users == null) return 0;
            var usersNotReadOnly = users.FindAll(user => user.UserRole.Name == "ReadOnly");
            return usersNotReadOnly.Count;
        }

        public static int GetPremiumUsersAvailable(Account account)
        {
            int maxNumberOfUsers;
            int.TryParse(SubscriptionManager.GetSubscriptionPropertyValue(account, "MaxNumberOfUsers"), out maxNumberOfUsers);
            var currenActiveUsers = GetUsersCountOtherThanReadOnly(account);
            return maxNumberOfUsers - currenActiveUsers.Value;
        }
    }
}