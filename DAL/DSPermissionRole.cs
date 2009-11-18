using System;
using NHibernate.Criterion;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

namespace SEOToolSet.DAL
{
    public class DSPermissionRole : EntityDataStoreBase<PermissionRole, Int32>
    {

        public DSPermissionRole(NHibernate.ISession session)
            : base(session)
        {
        }

        public static DSPermissionRole Create(String connName)
        {
            return new DSPermissionRole(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }


        public static class Columns
        {
            public static String Id = "Id";
            public static String Permission = "Permission";
            public static String Role = "Role";

        }

        public PermissionRole FindByRoleAndPermission(string role, string permission)
        {
            var crit = CreateCriteria();
            crit.CreateCriteria(Columns.Role).Add(Restrictions.Eq(DSRole.Columns.Name, role));
            crit.CreateCriteria(Columns.Permission).Add(Restrictions.Eq(DSPermission.Columns.Name, permission));

            return FindUnique(crit);
        }
    }
}
