using System;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

namespace SEOToolSet.DAL
{
    public class DSSubscriptionProperty : EntityDataStoreBase<Entities.SubscriptionProperty, Int32>
    {

        public DSSubscriptionProperty(NHibernate.ISession session)
            : base(session)
        {
        }

        public static DSSubscriptionProperty Create(String connName)
        {
            return new DSSubscriptionProperty(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        public int CreateSubscriptionProperty(string name)
        {
            var newProperty = new SubscriptionProperty
                                  {
                                      Name = name,
                                      Enabled = true
                                  };
            Insert(newProperty);
            return newProperty.Id;
        }

        public static class Columns
        {
            public static String Id = "Id";
            public static String Name = "Name";
            public static String Enabled = "Enabled";
            public static String SubscriptionDetail = "SubscriptionDetail";

        }

    }
}
