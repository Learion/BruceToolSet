using System;
using NHibernate.Criterion;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

namespace SEOToolSet.DAL
{
    public class DSPermission : EntityDataStoreBase<Permission, Int32>
    {

        public DSPermission(NHibernate.ISession session)
            : base(session)
        {
        }

        public static DSPermission Create(String connName)
        {
            return new DSPermission(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }


        public static class Columns
        {
            public static String Id = "Id";
            public static String Name = "Name";
            public static String Description = "Description";
            public static String PermissionRole = "PermissionRole";

        }

        public Permission FindByName(string permission)
        {
            var crit = CreateCriteria();
            crit.Add(Restrictions.Eq(Columns.Name, permission));

            return FindUnique(crit);
        }
    }
}
