#region Using Directives

using System;
using NHibernate;
using NHibernate.Criterion;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

#endregion

namespace SEOToolSet.DAL
{
    public class DSAccount : EntityDataStoreBase<Account, Int32>
    {
        public DSAccount(ISession session)
            : base(session)
        {
        }

        public static DSAccount Create(String connName)
        {
            return new DSAccount(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        public Account FindByName(string name)
        {
            var crit = CreateCriteria();
            crit.Add(Restrictions.Eq(Columns.Name, name));

            return FindUnique(crit);
        }

        public bool IsAccountNameAvailable(string name)
        {
            var account = FindByName(name);
            return (account == null);
        }

        #region Columns

        public static class Columns
        {
            public static String AccountExpirationDate = "AccountExpirationDate";
            public static String CompanyAddress1 = "CompanyAddress1";
            public static String CompanyAddress2 = "CompanyAddress2";
            public static String CompanyCity = "CompanyCity";
            public static String CompanyIdCountry = "CompanyIdCountry";
            public static String CompanyName = "CompanyName";
            public static String CompanyPhone = "CompanyPhone";
            public static String CompanyState = "CompanyState";
            public static String CompanyZip = "CompanyZip";
            public static String CreatedBy = "CreatedBy";
            public static String CreatedDate = "CreatedDate";
            public static String CreditCardAddress1 = "CreditCardAddress1";
            public static String CreditCardAddress2 = "CreditCardAddress2";
            public static String CreditCardCardholder = "CreditCardCardholder";
            public static String CreditCardCity = "CreditCardCity";
            public static String CreditCardCvs = "CreditCardCvs";
            public static String CreditCardEmail = "CreditCardEmail";
            public static String CreditCardExpiration = "CreditCardExpiration";
            public static String CreditCardIdCountry = "CreditCardIdCountry";
            public static String CreditCardNumber = "CreditCardNumber";
            public static String CreditCardState = "CreditCardState";
            public static String CreditCardType = "CreditCardType";
            public static String CreditCardZip = "CreditCardZip";
            public static String Enabled = "Enabled";
            public static String Id = "Id";
            public static String MaxNumberOfDomainUser = "MaxNumberOfDomainUser";
            public static String MaxNumberOfProjects = "MaxNumberOfProjects";
            public static String MaxNumberOfUser = "MaxNumberOfUser";
            public static String Name = "Name";
            public static String Owner = "Owner";
            public static String Project = "Project";
            public static String PromoCode = "PromoCode";
            public static String RecurringBill = "RecurringBill";
            public static String SEOToolsetUser = "SEOToolsetUser";
            public static String Subscription = "Subscription";
            public static String UpdatedBy = "UpdatedBy";
            public static String UpdatedDate = "UpdatedDate";
        }

        #endregion
    }
}