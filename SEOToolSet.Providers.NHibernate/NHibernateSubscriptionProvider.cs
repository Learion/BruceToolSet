using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using NHibernateDataStore.Common;
using SEOToolSet.DAL;
using SEOToolSet.Entities;

namespace SEOToolSet.Providers.NHibernate
{
    public class NHibernateSubscriptionProvider:SubscriptionProviderBase
    {
        private string _connName;
        private string _providerName;

        public override string Name
        {
            get { return _providerName; }
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (name.Length == 0)
                name = "SEOMembershipProvider";

            base.Initialize(name, config);

            _providerName = name;

            _connName = ConfigHelper.ExtractConfigValue(config, "connectionStringName", null);

            if (config.Count == 0)
                return;

            // Throw an exception if unrecognized attributes remain
            var attr = config.GetKey(0);
            if (!string.IsNullOrEmpty(attr))
                throw new ProviderException("Unrecognized attribute: " + attr);
        }
        
        public override SubscriptionLevel GetSubscriptionLevelByName(string name)
        {
            return DSSubscriptionLevel.Create(_connName).FindByName(name);
        }

        public override SubscriptionLevel GetSubscriptionLevel(int id)
        {
            return DSSubscriptionLevel.Create(_connName).FindByKey(id);
        }

        public override IList<SubscriptionLevel> GetSubscriptionLevels()
        {
            return DSSubscriptionLevel.Create(_connName).FindAll();
        }

        public override IList<SubscriptionLevel> GetSubscriptionLevelsForSignUp()
        {
            return DSSubscriptionLevel.Create(_connName).FindForSignUp();
        }

        public override int CreateSubscriptionLevel(string level, Role role, double price)
        {
            var ds = DSSubscriptionLevel.Create(_connName);
            int id;
            using (var tran = new TransactionScope(_connName))
            {
                id = ds.CreateSubscriptionLevel(level, role, price);
                tran.Commit();
            }
            return id;
            
        }

        public override IList<SubscriptionDetail> GetSubscriptionDetails(int accountId)
        {
            var account = DSAccount.Create(_connName).FindByKey(accountId);
            return account.SubscriptionLevel == null ? null : DSSubscriptionDetail.Create(_connName).FindBySubscriptionLevel(account.SubscriptionLevel.Id);
        }

        public override int CreateSubscriptionProperty(string propertyName)
        {
            var ds = DSSubscriptionProperty.Create(_connName);
            int id;
            using (var tran = new TransactionScope(_connName))
            {
                id = ds.CreateSubscriptionProperty(propertyName);
                tran.Commit();
            }
            return id;
        }

        public override void CreateSubscriptionDetails(int accountId, IDictionary<int, string> propertyValues)
        {
            var ds = DSSubscriptionDetail.Create(_connName);
            using (var tran = new TransactionScope(_connName))
            {
                foreach (var keyValuePair in propertyValues)
                {
                     ds.CreateSubscriptionDetail(accountId, keyValuePair.Key, keyValuePair.Value);                   
                }
                tran.Commit();
            }
        }
    }
}
