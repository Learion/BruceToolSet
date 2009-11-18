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
    public class DSKeywordList : EntityDataStoreBase<KeywordList, Int32>
    {
        public DSKeywordList(ISession session)
            : base(session)
        {
        }

        public static DSKeywordList Create(String connName)
        {
            return new DSKeywordList(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        public IList<KeywordList> FindByIdProject(int idProject)
        {
            var crit = CreateCriteria();
            crit.CreateCriteria(Columns.Project).Add(Restrictions.Eq("Id", idProject));

            return Find(crit);
        }

        public void DeleteIncludingChilds(int id)
        {
            
            var deleteQueryString = "from SEOToolSet.Entities.Keyword as k where k.KeywordList.Id = ?";

            NHibernateSession.Delete(deleteQueryString, id, NHibernateUtil.Int32);

            Delete(id);
        }

        #region Columns Metadata

        public static class Columns
        {
            //public static String Calificacion = "Calificacion";
            public static String Enabled = "Enabled";
            public static String Id = "Id";
            public static String Keyword = "Keyword";
            public static String Name = "Name";
            public static String Project = "Project";
        }

        #endregion
    }
}