#region Using Directives

using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using SEOToolSet.Entities;

#endregion

namespace SEOToolSet.Providers
{
    public abstract class AccountProviderBase : ProviderBase
    {
        public abstract String ConnectionString { get; }
        public abstract string TopLevelAccountName { get; }

        public abstract void CreateAccount(out Int32 id
                                           , String name
                                           , Account owner);

        public abstract void CreateAccount(Account account);

        public abstract void CreateAccount(out Int32 id
                                           , String name
                                           , int? maxNumberOfUser
                                           , int? maxNumberOfDomainUser
                                           , int? maxNumberOfProjects
                                           , String companyName
                                           , String companyAddress1
                                           , String companyAddress2
                                           , String companyCity
                                           , String companyState
                                           , String companyZip
                                           , String creditCardNumber
                                           , CreditCardType creditCardType
                                           , String creditCardAddress1
                                           , String creditCardAddress2
                                           , String creditCardCity
                                           , String creditCardZip
                                           , bool? recurringBill
                                           , DateTime? accountExpirationDate
                                           , DateTime? creditCardExpiration
                                           , Account owner
                                           , String creditCardEmail
                                           , int? creditCardIdCountry
                                           , String creditCardState
                                           , String creditCardCardholder
                                           , String promoCode
                                           , Int32? companyIdCountry
                                           , String creditCardCvs
                                           , String companyPhone
                                           , String creditCardPhone
                                           , int? subscriptionLevelId
                                           , long? subscriptionId);

        public abstract void UpdateAccount(Int32 id
                                           , int? maxNumberOfUser
                                           , int? maxNumberOfDomainUser
                                           , int? maxNumberOfProjects
                                           , String companyName
                                           , String companyAddress1
                                           , String companyAddress2
                                           , String companyCity
                                           , String companyState
                                           , String companyZip
                                           , String creditCardNumber
                                           , CreditCardType creditCardType
                                           , String creditCardAddress1
                                           , String creditCardAddress2
                                           , String creditCardCity
                                           , String creditCardZip
                                           , bool? recurringBill
                                           , bool? enabled
                                           , DateTime? accountExpirationDate
                                           , DateTime? creditCardExpiration
                                           , Account owner
                                           , String creditCardEmail
                                           , int? creditCardIdCountry
                                           , String creditCardState
                                           , String creditCardCardholder
                                           , String promoCode
                                           , Int32? companyIdCountrIy
                                           , String creditCardCvs
                                           , String companyPhone
                                           , String creditCardPhone
                                           , int? subscriptionLevelId);

        public abstract void DeleteAccount(Int32 Id);

        public abstract Account GetAccount(Int32 Id);

        public abstract Account GetAccountByName(String accountName);

        public abstract IList<Account> GetAccounts();

        public abstract void AssociateAccountToUser(Account account, SEOToolsetUser user);

        public abstract void AssociateAccountToUser(string accountName, string userName);

        public abstract void AssociateAccountToUser(Int32 idAccount, Int32 idUser);

        public abstract Account GetTopLevelAccount();

        public abstract IList<CreditCardType> GetCreditCardTypes();

        public abstract CreditCardType GetCreditCardTypeById(String id);

        public abstract bool IsAccountNameAvailable(string accountName);
    }
}