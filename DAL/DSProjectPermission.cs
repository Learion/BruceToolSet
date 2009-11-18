#region Using Directives

using System;
using NHibernate;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

#endregion

namespace SEOToolSet.DAL
{
    public class DSProjectPermission : EntityDataStoreBase<ProjectPermission, Int32>
    {
        public DSProjectPermission(ISession session)
            : base(session)
        {
        }

        public static DSProjectPermission Create(String connName)
        {
            return
                new DSProjectPermission(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        #region Columns Metadata

        public static class Columns
        {
            //public static String Calificacion = "Calificacion";
            public static String Description = "Description";
            public static String Id = "Id";
            public static String Name = "Name";
            public static String ProjectPermissionRole = "ProjectPermissionRole";
        }

        #endregion
    }
}