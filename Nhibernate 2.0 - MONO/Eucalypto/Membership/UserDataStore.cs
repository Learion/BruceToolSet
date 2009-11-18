using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernateDataStore.Common;

namespace Eucalypto.Membership
{
    /// <summary>
    /// Class that use NHibernate to save the User data
    /// </summary>
    public class UserDataStore : EntityDataStoreBase<User, string>
    {
        /// <summary>
        /// Creates a new UserDataStore
        /// </summary>
        /// <param name="transactionScope"></param>
        public UserDataStore(ITransactionScope transactionScope)
            : base(transactionScope)
        {
        }

        /// <summary>
        /// Find the specified user by name. Note that the name is searched using a case insensitive match.
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public User FindByName(string applicationName, string name)
        {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Restrictions.Eq("ApplicationName", applicationName));
            criteria.Add(Restrictions.InsensitiveLike("Name", name, MatchMode.Exact));

            return FindUnique(criteria);
        }

        public IList<User> FindAll(string applicationName, PagingInfo paging)
        {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Restrictions.Eq("ApplicationName", applicationName));
            criteria.AddOrder(Order.Asc("Name"));

            return Find(criteria, paging);
        }

        /// <summary>
        /// Find the specified user by name. Note that the name is searched using a case insensitive match.
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="name"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public IList<User> FindByNameLike(string applicationName, string name, PagingInfo paging)
        {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Restrictions.Eq("ApplicationName", applicationName));
            criteria.Add(Restrictions.InsensitiveLike("Name", name, MatchMode.Anywhere));
            criteria.AddOrder(Order.Asc("Name"));

            return Find(criteria, paging);
        }

        /// <summary>
        /// Find the specified user by e-mail. Note that the e-mail is searched using a case insensitive match.
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public IList<User> FindByEmail(string applicationName, string email)
        {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Restrictions.Eq("ApplicationName", applicationName));
            criteria.Add(Restrictions.InsensitiveLike("EMail", email, MatchMode.Exact));
            criteria.AddOrder(Order.Asc("Name"));

            return Find(criteria);
        }

        /// <summary>
        /// Find the specified user by e-mail. Note that the e-mail is searched using a case insensitive match.
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="email"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public IList<User> FindByEmailLike(string applicationName, string email, PagingInfo paging)
        {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Restrictions.Eq("ApplicationName", applicationName));
            criteria.Add(Restrictions.InsensitiveLike("EMail", email, MatchMode.Anywhere));
            criteria.AddOrder(Order.Asc("Name"));

            return Find(criteria, paging);
        }

        /// <summary>
        /// Get the number of users logged in right now
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="userIsOnlineTimeWindow">Specifies the time span after the last-activity date/time stamp for a user during which the user is considered online.</param>
        /// <returns></returns>
        public int NumbersOfLoggedInUsers(string applicationName, TimeSpan userIsOnlineTimeWindow)
        {
            DateTime compareTime = DateTime.Now.Subtract(userIsOnlineTimeWindow);

            NHibernate.ICriteria criteria = CreateCriteria();
            criteria.Add(Restrictions.Eq("ApplicationName", applicationName));
            criteria.Add(Restrictions.Gt("LastActivityDate", compareTime) );

            return Count(criteria);
        }
    }
}
