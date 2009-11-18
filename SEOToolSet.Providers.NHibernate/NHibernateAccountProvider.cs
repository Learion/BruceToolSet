#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using NHibernateDataStore.Common;
using SEOToolSet.DAL;
using SEOToolSet.Entities;

#endregion

namespace SEOToolSet.Providers.NHibernate
{
    public class NHibernateAccountProvider : AccountProviderBase
    {
        private string _connName;
        private string _providerName;
        private string _topLevelAccountName;

        public override string Name
        {
            get { return _providerName; }
        }

        public override string TopLevelAccountName
        {
            get { return _topLevelAccountName; }
        }

        #region Overrides of AccountProviderBase

        public override string ConnectionString
        {
            get { return _connName; }
        }

        public override void CreateAccount(out int id, string name, Account owner)
        {
            throw new NotImplementedException();
        }

        public override void CreateAccount(Account account)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSAccount.Create(_connName);

                if (account.Owner == null)
                {
                    account.Owner = GetTopLevelAccount();
                }

                if (ds.FindByName(account.Name) != null)
                {
                    tran.Rollback();
                    throw new DuplicatedEntityException("Account");
                }

                ds.Insert(account);
                tran.Commit();
            }
        }

        public override Account GetTopLevelAccount()
        {
            return GetAccountByName(TopLevelAccountName);
        }

        public override IList<CreditCardType> GetCreditCardTypes()
        {
            IList<CreditCardType> creditCardTypes;
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSCreditCardType.Create(_connName);
                creditCardTypes = ds.FindAll();
                tran.Commit();
            }
            return creditCardTypes;
        }

        public override CreditCardType GetCreditCardTypeById(string id)
        {
            CreditCardType creditCardType;
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSCreditCardType.Create(_connName);
                creditCardType = ds.FindByKey(id);
                tran.Commit();
            }
            return creditCardType;
        }

        public override bool IsAccountNameAvailable(string accountName)
        {
            var ds = DSAccount.Create(_connName);
            return ds.IsAccountNameAvailable(accountName);
        }

        

        public override void CreateAccount(out int id, string name, int? maxNumberOfUser, int? maxNumberOfDomainUser,
                                           int? maxNumberOfProjects, string companyName, string companyAddress1,
                                           string companyAddress2, string companyCity, string companyState,
                                           string companyZip, string creditCardNumber, CreditCardType creditCardType,
                                           string creditCardAddress1, string creditCardAddress2, string creditCardCity,
                                           string creditCardZip, bool? recurringBill, DateTime? accountExpirationDate,
                                           DateTime? creditCardExpiration, Account owner, string creditCardEmail,
                                           int? creditCardIdCountry, string creditCardState, string creditCardCardholder,
                                           string promoCode, Int32? companyIdCountry, String creditCardCvs,
                                           String companyPhone, string creditCardPhone, int? subscriptionLevelId, long? subscriptionId)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSAccount.Create(_connName);

                if (ds.FindByName(name) != null)
                {
                    tran.Rollback();
                    throw new DuplicatedEntityException("Account");
                }

                var ce = new Account();

                if (name != null) ce.Name = name;
                if (maxNumberOfUser != null) ce.MaxNumberOfUser = maxNumberOfUser;
                if (maxNumberOfDomainUser != null) ce.MaxNumberOfDomainUser = maxNumberOfDomainUser;
                if (maxNumberOfProjects != null) ce.MaxNumberOfProjects = maxNumberOfProjects;
                if (companyName != null) ce.CompanyName = companyName;
                if (companyAddress1 != null) ce.CompanyAddress1 = companyAddress1;
                if (companyAddress2 != null) ce.CompanyAddress2 = companyAddress2;
                if (companyCity != null) ce.CompanyCity = companyCity;
                if (companyState != null) ce.CompanyState = companyState;
                if (companyZip != null) ce.CompanyZip = companyZip;
                if (creditCardNumber != null) ce.CreditCardNumber = creditCardNumber;
                if (creditCardType != null) ce.CreditCardType = creditCardType;
                if (creditCardCvs != null) ce.CreditCardCvs = creditCardCvs;
                if (creditCardAddress1 != null) ce.CreditCardAddress1 = creditCardAddress1;
                if (creditCardAddress2 != null) ce.CreditCardAddress2 = creditCardAddress2;
                if (creditCardCity != null) ce.CreditCardCity = creditCardCity;
                if (creditCardZip != null) ce.CreditCardZip = creditCardZip;
                if (recurringBill != null) ce.RecurringBill = recurringBill;
                if (accountExpirationDate != null) ce.AccountExpirationDate = accountExpirationDate;
                if (creditCardExpiration != null) ce.CreditCardExpiration = creditCardExpiration;
                if (owner != null) ce.Owner = owner;

                if (creditCardEmail != null) ce.CreditCardEmail = creditCardEmail;
                if (creditCardIdCountry != null) ce.CreditCardIdCountry = creditCardIdCountry;
                if (creditCardState != null) ce.CreditCardState = creditCardState;
                if (creditCardCardholder != null) ce.CreditCardCardholder = creditCardCardholder;
                if (promoCode != null) ce.PromoCode = promoCode;
                if (companyIdCountry != null) ce.CompanyIdCountry = companyIdCountry;
                if (companyPhone != null) ce.CompanyPhone = companyPhone;
                if (creditCardPhone != null) ce.CreditCardPhone = creditCardPhone;
                if (subscriptionLevelId.HasValue) ce.SubscriptionLevel = new SubscriptionLevel { Id = subscriptionLevelId.Value };
                if (subscriptionId.HasValue) ce.SubscriptionId = subscriptionId;
                ds.Insert(ce);

                tran.Commit();

                id = ce.Id;
            }
        }


        public override void UpdateAccount(int id,
                                           int? maxNumberOfUser, int? maxNumberOfDomainUser, int? maxNumberOfProjects,
                                           string companyName,
                                           string companyAddress1, string companyAddress2,
                                           string companyCity,
                                           string companyState,
                                           string companyZip,
                                           string creditCardNumber,
                                           CreditCardType creditCardType,
                                           string creditCardAddress1,
                                           string creditCardAddress2,
                                           string creditCardCity,
                                           string creditCardZip,
                                           bool? recurringBill, bool? enabled, DateTime? accountExpirationDate,
                                           DateTime? creditCardExpiration, Account owner, string creditCardEmail,
                                           int? creditCardIdCountry, string creditCardState, string creditCardCardholder,
                                           string promoCode, Int32? companyIdCountry, String creditCardCvs,
                                           string companyPhone, string creditCardPhone, int? subscriptionLevelId)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSAccount.Create(_connName);

                var ce = ds.FindByKey(id);

                //if (name != null) ce.Name = name; the Account Name should not be modified!!!!
                if (maxNumberOfUser != null) ce.MaxNumberOfUser = maxNumberOfUser;
                if (maxNumberOfDomainUser != null) ce.MaxNumberOfDomainUser = maxNumberOfDomainUser;
                if (maxNumberOfProjects != null) ce.MaxNumberOfProjects = maxNumberOfProjects;
                if (companyName != null) ce.CompanyName = companyName;
                if (companyAddress1 != null) ce.CompanyAddress1 = companyAddress1;
                if (companyAddress2 != null) ce.CompanyAddress2 = companyAddress2;
                if (companyCity != null) ce.CompanyCity = companyCity;
                if (companyState != null) ce.CompanyState = companyState;
                if (companyZip != null) ce.CompanyZip = companyZip;
                if (creditCardNumber != null) ce.CreditCardNumber = creditCardNumber;
                if (creditCardType != null) ce.CreditCardType = creditCardType;
                if (creditCardCvs != null) ce.CreditCardCvs = creditCardCvs;
                if (creditCardAddress1 != null) ce.CreditCardAddress1 = creditCardAddress1;
                if (creditCardAddress2 != null) ce.CreditCardAddress2 = creditCardAddress2;
                if (creditCardCity != null) ce.CreditCardCity = creditCardCity;
                if (creditCardZip != null) ce.CreditCardZip = creditCardZip;
                if (recurringBill != null) ce.RecurringBill = recurringBill;
                if (accountExpirationDate != null) ce.AccountExpirationDate = accountExpirationDate;
                if (creditCardExpiration != null) ce.CreditCardExpiration = creditCardExpiration;
                if (owner != null) ce.Owner = owner;

                if (creditCardEmail != null) ce.CreditCardEmail = creditCardEmail;
                if (creditCardIdCountry != null) ce.CreditCardIdCountry = creditCardIdCountry;
                if (creditCardState != null) ce.CreditCardState = creditCardState;
                if (creditCardCardholder != null) ce.CreditCardCardholder = creditCardCardholder;
                if (promoCode != null) ce.PromoCode = promoCode;
                if (companyIdCountry != null) ce.CompanyIdCountry = companyIdCountry;
                if (companyPhone != null) ce.CompanyPhone = companyPhone;
                if (creditCardPhone != null) ce.CreditCardPhone = creditCardPhone;
                if (subscriptionLevelId.HasValue) ce.SubscriptionLevel = new SubscriptionLevel { Id = subscriptionLevelId.Value };
                
                ds.Update(ce);

                tran.Commit();
            }
        }

        public override void DeleteAccount(int id)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSAccount.Create(_connName);

                ds.Delete(id);

                tran.Commit();
            }
        }

        public override Account GetAccount(int Id)
        {
            Account account;
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSAccount.Create(_connName);

                account = ds.FindByKey(Id);

                tran.Commit();
            }
            return account;
        }

        public override Account GetAccountByName(string accountName)
        {
            var ds = DSAccount.Create(_connName);
            return ds.FindByName(accountName);
        }

        public override IList<Account> GetAccounts()
        {
            throw new NotImplementedException();
        }

        public override void AssociateAccountToUser(Account account, SEOToolsetUser user)
        {
            throw new NotImplementedException();
        }

        public override void AssociateAccountToUser(string accountName, string userName)
        {
            throw new NotImplementedException();
        }

        public override void AssociateAccountToUser(int IdAccount, int IdUser)
        {
            throw new NotImplementedException();
        }

        #endregion

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (name.Length == 0)
                name = "AccountProvider";

            base.Initialize(name, config);

            _providerName = name;

            _connName = ConfigHelper.ExtractConfigValue(config, "connectionStringName", null);

            _topLevelAccountName = ConfigHelper.ExtractConfigValue(config, "topLevelAccountName", "Bruce Clay, Inc.");


            if (config.Count == 0) return;

            // Throw an exception if unrecognized attributes remain
            var attr = config.GetKey(0);
            if (!String.IsNullOrEmpty(attr))
                throw new ProviderException("Unrecognized attribute: " + attr);
        }
    }
}