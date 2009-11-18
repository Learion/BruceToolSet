using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate.Criterion;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

namespace SEOToolSet.DAL
{
    public class DSRole : EntityDataStoreBase<Role, Int32>
    {

        public DSRole(NHibernate.ISession session)
            : base(session)
        {
        }

        public static DSRole Create(String connName)
        {
            return new DSRole(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }


        public static class Columns
        {
            public static String Id = "Id";
            public static String Name = "Name";
            public static String Description = "Description";
            public static String Enabled = "Enabled";
            public static String IdRoleType = "IdRoleType";
            public static String Configurable = "Configurable";
            public static String PermissionRole = "PermissionRole";
            public static String SEOToolSetUser = "SEOToolSetUser";
            public static String SubscriptionLevel = "SubscriptionLevel";
            public static String ProjectUser = "ProjectUser";

        }

        public Role FindByName(string role)
        {
            var crit = CreateCriteria();
            crit.Add(Restrictions.Eq(Columns.Name, role));
            return FindUnique(crit);
        }

        public void DeleteByName(string name)
        {
            var role = FindByName(name);
            Delete(role);
        }

        public void DeleteByNameAndType(string name, Int32 idRoleType)
        {
            var role = FindByName(name);
            if (role.IdRoleType == idRoleType)
                Delete(role);
            //TODO: Throw an exception if not found??
        }

        public IList<Role> FindAllByType(int idRoleType)
        {
            return FindAllByType(idRoleType, null);
        }
        public IList<Role> FindAllByType(int idRoleType, bool? configurableOnly)
        {
            var crit = CreateCriteria();
            if (configurableOnly != null && configurableOnly.Value)
            {
                crit.Add(Restrictions.Eq(Columns.Configurable, true));
            }
            crit.Add(Restrictions.Eq(Columns.IdRoleType, idRoleType))
                .Add(Restrictions.Eq(Columns.Enabled, true));

            return Find(crit);
        }

        public Role FindByNameAndtype(string name, int idRoleType)
        {
            var crit = CreateCriteria();
            crit.Add(Restrictions.Eq(Columns.IdRoleType, idRoleType))
                .Add(Restrictions.Eq(Columns.Name, name));

            return FindUnique(crit);
        }
    }
}
