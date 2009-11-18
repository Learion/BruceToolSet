#region Using Directives

using System;
using NHibernate;
using NHibernate.Criterion;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

#endregion

namespace SEOToolSet.DAL
{
    public class DSUserRole : EntityDataStoreBase<UserRole, Int32>
    {
        public DSUserRole(ISession session)
            : base(session)
        {
        }

        public static DSUserRole Create(String connName)
        {
            return new DSUserRole(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        public void DeleteByName(string name)
        {
            UserRole userRole = FindByName(name);
            Delete(userRole);
        }

        public UserRole FindByName(string name)
        {
            UserRole userRole = FindUnique(CreateCriteria().Add(Restrictions.Eq(Columns.Name, name)));
            return userRole;
        }

        #region Columns Metadata

        public static class Columns
        {
            //public static String Calificacion = "Calificacion";
            public static String Description = "Description";
            public static String Enabled = "Enabled";
            public static String Id = "Id";
            public static String Name = "Name";
            public static String SEOToolsetUser = "SEOToolsetUser";
            public static String UserRolPermission = "UserRolPermission";
        }

        #endregion
    }
}