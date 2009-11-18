using System;
using NHibernate.Criterion;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

namespace SEOToolSet.DAL
{
    public class DSPromoCode : EntityDataStoreBase<PromoCode, int>
    {
        public DSPromoCode(NHibernate.ISession session)
            : base(session)
        {
        }

        public static DSPromoCode Create(String connName)
        {
            return new DSPromoCode(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        public PromoCode FindByCode(string code)
        {
            var crit = CreateCriteria();
            crit.Add(Restrictions.Eq(Columns.Code, code));
            return FindUnique(crit);
        }

        public static class Columns
        {
            public static String Code = "Code";
            public static String BeginDate = "BeginDate";
            public static String EndDate = "EndDate";
            public static String Period = "Period";
            public static String PromoType = "PromoType";
            public static String PromoAmount = "PromoAmount";
            public static String AccountType = "AccountType";
            public static String Description = "Description";
            public static String MaxUse = "MaxUse";
            public static String TimesUsed = "TimesUsed";
        }
    }
}
