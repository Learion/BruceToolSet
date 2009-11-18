#region Using Directives

using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;

#endregion

namespace SEOToolSet.DAL
{
    public class DSProjectUser : EntityDataStoreBase<ProjectUser, Int32>
    {
        public DSProjectUser(ISession session)
            : base(session)
        {
        }

        public static DSProjectUser Create(String connName)
        {
            return new DSProjectUser(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        public ProjectUser FindByUserNameAndProject(string username, Project project)
        {
            if (project.Enabled == false) return null;
            var crit = CreateCriteria();
            crit.CreateCriteria(Columns.SEOToolsetUser).Add(Restrictions.Eq("Login", username));
            crit.Add(Restrictions.Eq(Columns.Project, project));

            return FindUnique(crit);
        }

        public IList<ProjectUser> FindByIdProject(int idProject)
        {
            var crit = CreateCriteria();
            crit.CreateCriteria(Columns.Project).Add(Restrictions.Eq("Id", idProject));

            return Find(crit);
        }

        #region Columns Metadata

        public static class Columns
        {
            //public static String Calificacion = "Calificacion";
            public static String Enabled = "Enabled";
            public static String Id = "Id";
            public static String MonitorEmails = "MonitorEmails";
            public static String Project = "Project";
            public static String ProjectRole = "ProjectRole";
            public static String SEOToolsetUser = "SEOToolsetUser";
        }

        #endregion

        public ProjectUser FindByIdProjectAndIdUser(int idProject, int idUser)
        {
            var crit = CreateCriteria();
            crit.CreateCriteria(Columns.Project).Add(Restrictions.Eq("Id", idProject));
            crit.CreateCriteria(Columns.SEOToolsetUser).Add(Restrictions.Eq("Id", idUser));

            return FindUnique(crit);
        }
    }
}