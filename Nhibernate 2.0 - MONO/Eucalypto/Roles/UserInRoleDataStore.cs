using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernateDataStore.Common;

namespace Eucalypto.Roles
{

    /// <summary>
    /// Class that use NHibernate to save the UserInRole data.
    /// 
    /// Note that the username is considered always case insensitive.
    /// </summary>
    public class UserInRoleDataStore : EntityDataStoreBase<UserInRole, string>
    {
        public UserInRoleDataStore(TransactionScope transactionScope)
            : base(transactionScope)
        {
        }

        public UserInRole Find(string applicationName, string userName, string roleName)
        {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Expression.Eq("ApplicationName", applicationName));
            criteria.Add(Expression.InsensitiveLike("UserName", userName, MatchMode.Exact));
            criteria.Add(Expression.Eq("RoleName", roleName));

            return FindUnique(criteria);
        }

        public IList<UserInRole> FindForUser(string applicationName, string userName)
        {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Expression.Eq("ApplicationName", applicationName));
            criteria.Add(Expression.InsensitiveLike("UserName", userName, MatchMode.Exact));

            return Find(criteria);
        }

        public IList<UserInRole> FindForRole(string applicationName, string roleName)
        {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Expression.Eq("ApplicationName", applicationName));
            criteria.Add(Expression.Eq("RoleName", roleName));

            return Find(criteria);
        }

        public IList<UserInRole> FindForRole(string applicationName, string roleName, string userToMatch)
        {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Expression.Eq("ApplicationName", applicationName));
            criteria.Add(Expression.Eq("RoleName", roleName));
            criteria.Add(Expression.InsensitiveLike("UserName", userToMatch, MatchMode.Anywhere));

            return Find(criteria);
        }
    }
}
