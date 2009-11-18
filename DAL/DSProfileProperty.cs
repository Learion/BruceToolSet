using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

namespace SEOToolSet.DAL
{
    /// <summary>
    /// Class that use NHibernate to save the ProfileProperty data
    /// </summary>
    public class DSProfileProperty : EntityDataStoreBase<ProfileProperty, int>
    {
        public DSProfileProperty(ISession session) : base(session)
        {
        }
        
        public static DSProfileProperty Create(string _connName)
        {
            return new DSProfileProperty(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(_connName));
        }

        public IList<ProfileProperty> FindByUser(SEOProfile user)
        {
            ICriteria criteria = CreateCriteria();
            criteria.CreateCriteria("User").Add(Restrictions.Eq("Id", user.Id));
            criteria.AddOrder(Order.Desc("Name"));

            return Find(criteria);
        }

        public int DeleteByUser(SEOProfile user)
        {
            IList<ProfileProperty> properties = FindByUser(user);

            foreach (ProfileProperty prop in properties)
                Delete(prop.Id);

            return properties.Count;
        }

        public ProfileProperty FindByPropertyName(SEOProfile user, string propertyName)
        {
            ICriteria criteria = CreateCriteria();
            criteria.CreateCriteria("User").Add(Restrictions.Eq("Id", user.Id));
            criteria.Add(Restrictions.Eq("Name", propertyName));

            return FindUnique(criteria);
        }
    }
}