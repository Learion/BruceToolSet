using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernateDataStore.Common;

namespace Eucalypto.Profile
{
    /// <summary>
    /// Class that use NHibernate to save the ProfileUser data
    /// </summary>
    public class ProfileUserDataStore : EntityDataStoreBase<ProfileUser, string>
    {
        public ProfileUserDataStore(TransactionScope transactionScope)
            : base(transactionScope)
        {
        }

        public ProfileUser FindByName(string applicationName, string name)
        {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Expression.Eq("ApplicationName", applicationName));
            criteria.Add(Expression.Eq("Name", name));

            return FindUnique(criteria);
        }

        public IList<ProfileUser> FindAll(string applicationName, PagingInfo paging)
        {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Expression.Eq("ApplicationName", applicationName));
            criteria.AddOrder(Order.Asc("Name"));

            return Find(criteria, paging);
        }

        /// <summary>
        /// Search profile users
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="nameLike">Optional, use null to don't filter</param>
        /// <param name="inactiveSince">Optional, use null to don't filter</param>
        /// <param name="profileType">Optional, use null to don't filter</param>
        /// <param name="paging">an instance of the PagingInfoClass</param>
        /// <returns></returns>
        public IList<ProfileUser> FindByFields(string applicationName,
                                                string nameLike, 
                                                DateTime? inactiveSince,
                                                ProfileType? profileType,
                                                PagingInfo paging)
        {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Expression.Eq("ApplicationName", applicationName));
            if (nameLike != null)
                criteria.Add(Expression.Like("Name", nameLike, MatchMode.Anywhere));
            if (inactiveSince != null)
                criteria.Add(Expression.Le("LastActivityDate", inactiveSince.Value));
            if (profileType != null)
                criteria.Add(Expression.Eq("ProfileType", profileType.Value));

            criteria.AddOrder(Order.Asc("Name"));

            return Find(criteria, paging);
        }
    }
}
