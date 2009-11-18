using System;
using NHibernate.Criterion;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

namespace SEOToolSet.DAL
{
    public class DSSubscriptionDetail : EntityDataStoreBase<Entities.SubscriptionDetail, Int32>
    {

        public DSSubscriptionDetail(NHibernate.ISession session)
            : base(session)
        {
        }

        public static DSSubscriptionDetail Create(String connName)
        {
            return new DSSubscriptionDetail(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        public System.Collections.Generic.IList<SubscriptionDetail> FindBySubscriptionLevel(int accountId)
        {
            var crit = CreateCriteria();
            crit.Add(Restrictions.Eq(Columns.SubscriptionLevel, new SubscriptionLevel { Id = accountId }));
            return Find(crit);
        }

        public void CreateSubscriptionDetail(int accountId, int propertyId, string value)
        {
            var subscriptionDetail = new SubscriptionDetail
                                         {
                                             SubscriptionLevel = new SubscriptionLevel { Id = accountId },
                                             SubscriptionProperty = new SubscriptionProperty { Id = propertyId },
                                             PropertyValue = value
                                         };
            Insert(subscriptionDetail);
        }

        public static class Columns
        {
            public static String Id = "Id";
            public static String PropertyValue = "PropertyValue";
            public static String SubscriptionLevel = "SubscriptionLevel";
            public static String SubscriptionProperty = "SubscriptionProperty";

        }

    }
}
