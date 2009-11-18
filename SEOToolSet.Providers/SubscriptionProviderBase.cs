using System.Collections.Generic;
using System.Configuration.Provider;
using SEOToolSet.Entities;

namespace SEOToolSet.Providers
{
    public abstract class SubscriptionProviderBase : ProviderBase
    {
        public abstract SubscriptionLevel GetSubscriptionLevelByName(string name);

        public abstract SubscriptionLevel GetSubscriptionLevel(int id);

        public abstract IList<SubscriptionLevel> GetSubscriptionLevels();

        public abstract IList<SubscriptionLevel> GetSubscriptionLevelsForSignUp();

        public abstract int CreateSubscriptionLevel(string level, Role role, double price);

        public abstract IList<SubscriptionDetail> GetSubscriptionDetails(int accountId);

        public abstract int CreateSubscriptionProperty(string propertyName);

        public abstract void CreateSubscriptionDetails(int accountId, IDictionary<int, string> propertyValues);

    }
}
