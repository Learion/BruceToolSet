using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;
using SEOToolSet.Entities.Wrappers;

namespace SEOToolSet.DAL
{
    /// <summary>
    /// Class that use NHibernate to save the Profile data
    /// </summary>
    public class DSSEOProfile : EntityDataStoreBase<SEOProfile, int>
    {
        public DSSEOProfile(ISession session)
            : base(session)
        {
        }

        public static DSSEOProfile Create(String connName)
        {
            return new DSSEOProfile(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        public SEOProfile FindByName(string applicationName, string name)
        {
            var criteria = CreateCriteria();
            criteria.Add(Restrictions.Eq("ApplicationName", applicationName));
            criteria.Add(Restrictions.Eq("Name", name));

            return FindUnique(criteria);
        }

        public IList<SEOProfile> FindAll(string applicationName, PagingInfo paging)
        {
            var criteria = CreateCriteria();
            criteria
                .Add(Restrictions.Eq("ApplicationName", applicationName))
                .AddOrder(Order.Asc("Name"));

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
        public IList<SEOProfile> FindByFields(string applicationName,
                                              string nameLike, 
                                              DateTime? inactiveSince,
                                              ProfileType? profileType,
                                              PagingInfo paging)
        {
            var criteria = CreateCriteria();
            criteria.Add(Restrictions.Eq("ApplicationName", applicationName));
            if (nameLike != null)
                criteria.Add(Restrictions.Like("Name", nameLike, MatchMode.Anywhere));
            if (inactiveSince != null)
                criteria.Add(Restrictions.Le("LastActivityDate", inactiveSince.Value));
            if (profileType != null)
                criteria.Add(Restrictions.Eq("ProfileType", profileType.Value));

            criteria.AddOrder(Order.Asc("Name"));

            return Find(criteria, paging);
        }
    }
}