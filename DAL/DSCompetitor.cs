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
    public class DSCompetitor : EntityDataStoreBase<Competitor, Int32>
    {
        public DSCompetitor(ISession session)
            : base(session)
        {
        }

        public static DSCompetitor Create(String connName)
        {
            return new DSCompetitor(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        public IList<Competitor> FindByIdProject(int idProject)
        {
            var crit = CreateCriteria();
            crit.CreateCriteria(Columns.Project).Add(Restrictions.Eq(DSProject.Columns.Id, idProject));
            return Find(crit);
        }

        #region Columns Metadata

        public static class Columns
        {
            //public static String Calificacion = "Calificacion";
            public static String Description = "Description";
            public static String Id = "Id";
            public static String Name = "Name";
            public static String Project = "Project";
            public static String Url = "Url";
        }

        #endregion
    }
}