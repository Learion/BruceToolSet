#region Using Directives

using System;
using NHibernate;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

#endregion

namespace SEOToolSet.DAL
{
    public class DSProjectPermissionRole : EntityDataStoreBase<ProjectPermissionRole, Int32>
    {
        public DSProjectPermissionRole(ISession session)
            : base(session)
        {
        }

        public static DSProjectPermissionRole Create(String connName)
        {
            return
                new DSProjectPermissionRole(
                    NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        #region Columns Metadata

        public static class Columns
        {
            //public static String Calificacion = "Calificacion";
            public static String Id = "Id";
            public static String ProjectPermission = "ProjectPermission";
            public static String ProjectRole = "ProjectRole";
        }

        #endregion
    }
}