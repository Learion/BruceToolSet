#region Using Directives

using System;
using NHibernate;
using NHibernate.Criterion;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

#endregion

namespace SEOToolSet.DAL
{
    public class DSProjectRole : EntityDataStoreBase<ProjectRole, Int32>
    {
        public DSProjectRole(ISession session)
            : base(session)
        {
        }

        public static DSProjectRole Create(String connName)
        {
            return new DSProjectRole(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        public ProjectRole FindByName(string role)
        {
            var crit = CreateCriteria();
            crit.Add(Restrictions.Eq(Columns.Name, role));

            return FindUnique(crit);
        }

        #region Columns Metadata

        public static class Columns
        {
            //public static String Calificacion = "Calificacion";
            public static String Description = "Description";
            public static String Enabled = "Enabled";
            public static String Id = "Id";
            public static String Name = "Name";
            public static String ProjectPermissionRole = "ProjectPermissionRole";
            public static String ProjectUser = "ProjectUser";
        }

        #endregion
    }
}