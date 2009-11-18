#region Using Directives

using System;
using NHibernate;
using NHibernate.Criterion;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

#endregion

namespace SEOToolSet.DAL
{
    public class DSUserRolPermission : EntityDataStoreBase<UserRolPermission, Int32>
    {
        public DSUserRolPermission(ISession session)
            : base(session)
        {
        }

        public static DSUserRolPermission Create(String connName)
        {
            return
                new DSUserRolPermission(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        public UserRolPermission FindByRoleAndPermission(string name, string permission)
        {
            var crit = CreateCriteria();

            crit.CreateCriteria("UserRole").Add(Restrictions.Eq("Name", name));
            crit.CreateCriteria("UserPermission").Add(Restrictions.Eq("Name", permission));


            /*crit.Add(Restrictions.Eq(Columns.UserRole + ".Name", name))
                .Add(Restrictions.Eq(Columns.UserPermission + ".Name", permission));
             * */

            return FindUnique(crit);
        }

        #region Columns Metadata

        public static class Columns
        {
            //public static String Calificacion = "Calificacion";
            public static String Id = "Id";
            public static String UserPermission = "UserPermission";
            public static String UserRole = "UserRole";
        }

        #endregion
    }
}