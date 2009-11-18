#region Using Directives

using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Web.Security;
using SEOToolSet.Entities;

#endregion

namespace SEOToolSet.Providers
{
    public abstract class SEOMembershipProviderBase : ProviderBase
    {
        public abstract SEOToolsetUser GetUser(string userName);
        public abstract SEOToolsetUser GetUserById(int id);

        public abstract void CreateUser(out Int32 id
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
                                        , out MembershipCreateStatus status);


        public abstract void UpdateUser(Int32 id
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
                                        , bool? enabled
                                        , Account account
                                        , Country country
                                        , Role userRole);


        public abstract void CreateUser(SEOToolsetUser user, out MembershipCreateStatus status);


        public abstract IList<SEOToolsetUser> GetUsersFromAccount(Account account, bool? includeInactive);
        public abstract IList<SEOToolsetUser> GetUsersFromAccount(string accountName, bool? includeInactive);

        /*    public abstract void AddSubscription(string accountName, string serviceName);
            public abstract void RemoveSubscription(string accountName, string serviceName);*/

        /*public abstract void AddSubscription(Account account, Service service);
        public abstract void RemoveSubscription(Account account, Service service);*/

        public abstract IList<Country> GetCountries();
        public abstract Country GetCountryById(Int32 Id);

        public abstract void UpdateUser(SEOToolsetUser user);

        public abstract void DeleteUser(int id);

        public abstract IList<SEOToolsetUser> GetUsersFromAccountSortBy(Account account, bool? includeInactive, bool asc, string fieldName);

        public abstract IList<SEOToolsetUser> GetUsersFromAccountWithSortAndPaging(Account account, bool? includeInactive, bool asc,
                                                                        string fieldName, out int count, int pageSize,
                                                                        int currentPageNumber);


        
    }
}