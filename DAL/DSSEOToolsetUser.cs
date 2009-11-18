#region Using Directives

using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

#endregion

namespace SEOToolSet.DAL
{
    public class DSSEOToolsetUser : EntityDataStoreBase<SEOToolsetUser, Int32>
    {
        public DSSEOToolsetUser(ISession session)
            : base(session)
        {
        }

        public static DSSEOToolsetUser Create(String connName)
        {
            return new DSSEOToolsetUser(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }


        public SEOToolsetUser FindByName(String name)
        {
            ICriteria crit = CreateCriteria();
            crit.Add(Restrictions.InsensitiveLike(Columns.Login, name, MatchMode.Exact));
            return FindUnique(crit);
        }

        public IList<SEOToolsetUser> FindByEmail(String email)
        {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Restrictions.InsensitiveLike(Columns.Email, email, MatchMode.Exact));
            criteria.AddOrder(Order.Asc(Columns.Login));

            return Find(criteria);
        }


        public SEOToolsetUser FindUniqueByEmail(string email)
        {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Restrictions.InsensitiveLike(Columns.Email, email, MatchMode.Exact));
            criteria.AddOrder(Order.Asc(Columns.Login));
            return FindUnique(criteria);
        }

        public int NumbersOfLoggedInUsers(TimeSpan userIsOnlineTimeWindow)
        {
            DateTime compareTime = DateTime.Now.Subtract(userIsOnlineTimeWindow);

            ICriteria criteria = CreateCriteria();

            criteria.Add(Restrictions.Gt(Columns.LastActivityDate, compareTime));

            return Count(criteria);
        }

        public IList<SEOToolsetUser> FindByNameLike(string userName, PagingInfo paging)
        {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Restrictions.InsensitiveLike(Columns.Login, userName, MatchMode.Anywhere));
            criteria.AddOrder(Order.Asc(Columns.Login));

            return Find(criteria, paging);
        }

        public IList<SEOToolsetUser> FindByEmailLike(string email, PagingInfo paging)
        {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Restrictions.InsensitiveLike(Columns.Email, email, MatchMode.Exact));
            criteria.AddOrder(Order.Asc(Columns.Login));

            return Find(criteria, paging);
        }

        public SEOToolsetUser FindByRolAndName(string username, string rolename)
        {
            var criteria = CreateCriteria();
            criteria.Add(Restrictions.Eq(Columns.Login, username))
                .Add(Restrictions.Eq(Columns.UserRole + ".Name", rolename))
                .Add(Restrictions.Eq(Columns.UserRole + ".IdRoleType", 2));


            return FindUnique(criteria);
        }

        public IList<SEOToolsetUser> FindUsersInRoleWithNameLike(string match, string name)
        {
            var criteria = CreateCriteria();
            criteria.Add(Restrictions.InsensitiveLike(Columns.Login, match, MatchMode.Anywhere))
                .Add(Restrictions.Eq(Columns.UserRole + ".Name", name));

            return Find(criteria);
        }

        public IList<SEOToolsetUser> FindByAccount(Account account, bool? inactive)
        {
            var crit = CreateCriteria();
            crit.Add(Restrictions.Eq(Columns.Account, account));
            if (inactive == null || !inactive.Value)
            {
                crit.Add(Restrictions.Eq(Columns.Enabled, true));
            }
            return Find(crit);
        }

        public IList<SEOToolsetUser> FindUsersNotInProject(int idProject, int idAccount)
        {
            var query =
                CreateQuery(
                    @"select user from SEOToolSet.Entities.SEOToolsetUser as user 
                             where user.Account.Id = :IdAccount 
                             and user.Id not in 
                             (select pUser.SEOToolsetUser.Id 
                              from SEOToolSet.Entities.ProjectUser as pUser 
                              where pUser.Project.Id = :IdProject )");

            query.SetInt32("IdAccount", idAccount);
            query.SetInt32("IdProject", idProject);

            return query.List<SEOToolsetUser>();
        }

        public IList<SEOToolsetUser> FindByAccountName(string accountName, bool? includeInactive)
        {
            var crit = CreateCriteria();
            crit.CreateCriteria(Columns.Account).Add(Restrictions.Eq(DSAccount.Columns.Name, accountName));
            if (includeInactive == null || !includeInactive.Value)
            {
                crit.Add(Restrictions.Eq(Columns.Enabled, true));
            }
            return Find(crit);
        }

        public IList<SEOToolsetUser> FindByAccountName(Account account, bool? includeInactive, bool asc, string fieldName)
        {
            var crit = CreateCriteria();
            var order = asc ? Order.Asc(fieldName) : Order.Desc(fieldName);
            crit.Add(Restrictions.Eq(Columns.Account, account))
                .AddOrder(order);
            if (!includeInactive.HasValue || !includeInactive.Value)
            {
                crit.Add(Restrictions.Eq(Columns.Enabled, true));
            }
            return Find(crit);
        }
        #region Columns

        public static class Columns
        {
            public static String Account = "Account";
            public static String Address1 = "Address1";
            public static String Address2 = "Address2";
            public static String CityTown = "CityTown";
            public static String Country = "Country";
            public static String CreatedBy = "CreatedBy";
            public static String CreatedDate = "CreatedDate";
            public static String Email = "Email";
            public static String Enabled = "Enabled";
            public static String ExpirationDate = "ExpirationDate";
            public static String FailedPasswordAttemptCount = "FailedPasswordAttemptCount";
            public static String FirstName = "FirstName";
            public static String Id = "Id";
            public static String IsLockedOut = "IsLockedOut";
            public static String LastActivityDate = "LastActivityDate";
            public static String LastFailedLoginDate = "LastFailedLoginDate";
            public static String LastLoginDate = "LastLoginDate";
            public static String LastName = "LastName";
            public static String LastPasswordChangedDate = "LastPasswordChangedDate";
            public static String LockedOutDate = "LockedOutDate";
            public static String Login = "Login";
            public static String Password = "Password";
            public static String PasswordAnswer = "PasswordAnswer";
            public static String PasswordQuestion = "PasswordQuestion";
            public static String ProjectUser = "ProjectUser";
            public static String State = "State";
            public static String Telephone = "Telephone";
            public static String UpdatedBy = "UpdatedBy";
            public static String UpdatedDate = "UpdatedDate";
            public static String UserRole = "UserRole";
            public static String Zip = "Zip";
        }

        #endregion

        public IList<SEOToolsetUser> FindByAccountNameWithSortAndPaging(Account account, bool? inactive, bool asc, string name, out int count, int pageSize, int currentPage)
        {
            var crit = CreateCriteria();
            crit.Add(Restrictions.Eq(Columns.Account, account));
            if (name.Contains("."))
            {
                crit = GetCriteriaWithOrder(name, crit, asc);
            }
            else
            {
                var order = asc ? Order.Asc(name) : Order.Desc(name);
                crit.AddOrder(order);
            }
            if (!inactive.HasValue || !inactive.Value)
            {
                crit.Add(Restrictions.Eq(Columns.Enabled, true));
            }
            var list = crit.List();
            if (list == null)
            {
                count = 0;
                return new List<SEOToolsetUser>();
            }
            count = list.Count;
            var pagingInfo = new PagingInfo(pageSize, currentPage);
            return Find(crit, pagingInfo);
        }

        private static ICriteria GetCriteriaWithOrder(string name, ICriteria crit, bool asc)
        {
            var path = name.Split('.');
            for (var i = 0; i < path.Length - 1; i++)
            {
                crit = crit.CreateCriteria(path[i]);
            }
            var orderString = path[path.Length - 1];
            var order = asc ? Order.Asc(orderString) : Order.Desc(orderString);
            crit.AddOrder(order);

            return crit;
        }
    }
}