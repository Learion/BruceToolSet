#region Using Directives

using System;
using NHibernate;
using NHibernate.Criterion;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

#endregion

namespace SEOToolSet.DAL
{
    public class DSUserPermission : EntityDataStoreBase<UserPermission, Int32>
    {
        public DSUserPermission(ISession session)
            : base(session)
        {
        }

        public static DSUserPermission Create(String connName)
        {
            return new DSUserPermission(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        public UserPermission findByName(string permission)
        {
            ICriteria crit = CreateCriteria();
            crit.Add(Restrictions.Eq(Columns.Name, permission));

            return FindUnique(crit);
        }

        #region Columns Metadata

        public static class Columns
        {
            //public static String Calificacion = "Calificacion";
            public static String Description = "Description";
            public static String Id = "Id";
            public static String Name = "Name";
            public static String UserRolPermission = "UserRolPermission";
        }

        #endregion
    }
}