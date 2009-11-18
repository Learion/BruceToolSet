using System;
using System.Collections.Generic;
using NHibernate.Criterion;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

namespace SEOToolSet.DAL
{
    public class DSSubscriptionLevel : EntityDataStoreBase<SubscriptionLevel, Int32>
    {

        public DSSubscriptionLevel(NHibernate.ISession session)
            : base(session)
        {
        }

        public static DSSubscriptionLevel Create(String connName)
        {
            return new DSSubscriptionLevel(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        public SubscriptionLevel FindByName(string name)
        {
            var crit = CreateCriteria().Add(Restrictions.Eq(Columns.Name, name));
            return FindUnique(crit);
        }

        public IList<SubscriptionLevel> FindForSignUp()
        {
            var crit = CreateCriteria().Add(Restrictions.Not(Restrictions.Eq(Columns.Name, "Partner")));
            return Find(crit);
        }

        public int CreateSubscriptionLevel(string level, Role role, double price)
        {
            var entity = new SubscriptionLevel { Name = level, Role = role, Price = price, Enabled = true };
            Insert(entity);
            return entity.Id;
        }

        public static class Columns
        {
            public static String Id = "Id";
            public static String Name = "Name";
            public static String Enabled = "Enabled";
            public static String Price = "Price";
            public static String SubscriptionDetail = "SubscriptionDetail";
            public static String Role = "Role";
        }

    }
}
