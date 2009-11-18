using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernateDataStore.Common;

namespace Eucalypto.Profile
{
    /// <summary>
    /// Class that use NHibernate to save the ProfileProperty data
    /// </summary>
    public class ProfilePropertyDataStore : EntityDataStoreBase<ProfileProperty, string>
    {
        public ProfilePropertyDataStore(TransactionScope transactionScope)
            : base(transactionScope)
        {
        }

        public IList<ProfileProperty> FindByUser(ProfileUser user)
        {
            ICriteria criteria = CreateCriteria();
            criteria.CreateCriteria("User").Add(Expression.Eq("Id", user.Id));
            criteria.AddOrder(Order.Desc("Name"));

            return Find(criteria);
        }

        public int DeleteByUser(ProfileUser user)
        {
            IList<ProfileProperty> properties = FindByUser(user);

            foreach (ProfileProperty prop in properties)
                Delete(prop.Id);

            return properties.Count;
        }

        public ProfileProperty FindByPropertyName(ProfileUser user, string propertyName)
        {
            ICriteria criteria = CreateCriteria();
            criteria.CreateCriteria("User").Add(Expression.Eq("Id", user.Id));
            criteria.Add(Expression.Eq("Name", propertyName));

            return FindUnique(criteria);
        }
    }
}
