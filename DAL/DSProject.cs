#region Using Directives

using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernateDataStore.Common;
using NHibernateDataStore.Exceptions;
using SEOToolSet.Entities;
using System.Text;

#endregion

namespace SEOToolSet.DAL
{
    public class DSProject : EntityDataStoreBase<Project, Int32>
    {
        public DSProject(ISession session)
            : base(session)
        {
        }

        public static DSProject Create(String connName)
        {
            return new DSProject(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        public IList<Project> FindByAccount(Account account, bool? inactive)
        {
            var crit = CreateCriteria();
            if (inactive == null || !inactive.Value)
            {
                crit.Add(Restrictions.Eq(Columns.Enabled, true));
            }

            crit.Add(Restrictions.Eq(Columns.Account, account));

            return Find(crit);
        }


        public IList<Project> FindByUser(string userName, bool? includeInactive)
        {
            var crit = CreateCriteria();
            if (includeInactive == null || !includeInactive.Value)
            {
                crit.Add(Restrictions.Eq(Columns.Enabled, true));
            }
            crit.CreateCriteria(Columns.ProjectUser)
                .CreateCriteria(DSProjectUser.Columns.SEOToolsetUser)
                .Add(Restrictions.Eq(DSSEOToolsetUser.Columns.Login, userName));
            return Find(crit);
        }

        public IList<Project> FindByUserAndAccount(string userName, Account account, bool? includeInactive)
        {
            var crit = CreateCriteria();
            if (includeInactive == null || !includeInactive.Value)
            {
                crit.Add(Restrictions.Eq(Columns.Enabled, true));
            }

            crit.Add(Restrictions.Eq(Columns.Account, account))
                .CreateCriteria(Columns.ProjectUser)
                .CreateCriteria(DSProjectUser.Columns.SEOToolsetUser)
                .Add(Restrictions.Eq(DSSEOToolsetUser.Columns.Login, userName));

            return Find(crit);
        }

        public void SetDisable(int id)
        {
            var ele = FindByKey(id);
            if (ele == null) throw new EntityNotFoundException();
            ele.Enabled = false;
            Update(ele);
        }

        #region Columns Metadata

        public static class Columns
        {
            //public static String Calificacion = "Calificacion";
            public static String Account = "Account";
            public static String ClientName = "ClientName";
            public static String Competitor = "Competitor";
            public static String ContactEmail = "ContactEmail";
            public static String ContactName = "ContactName";
            public static String ContactPhone = "ContactPhone";
            public static String CreatedBy = "CreatedBy";
            public static String CreatedDate = "CreatedDate";
            public static String Domain = "Domain";
            public static String Enabled = "Enabled";
            public static String Id = "Id";
            public static String KeywordList = "KeywordList";
            public static String Name = "Name";
            public static String ProjectUser = "ProjectUser";
            public static String UpdatedBy = "UpdatedBy";
            public static String UpdatedDate = "UpdatedDate";
        }

        #endregion

    }
}