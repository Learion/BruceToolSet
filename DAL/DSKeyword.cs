#region Using Directives

using System;
using System.Collections.Generic;
using NHibernate;
using NHibernateDataStore.Common;
using SEOToolSet.Entities;
using NHibernate.Criterion;

#endregion

namespace SEOToolSet.DAL
{
    public class DSKeyword : EntityDataStoreBase<Keyword, Int32>
    {
        public DSKeyword(ISession session)
            : base(session)
        {
        }

        public static DSKeyword Create(String connName)
        {
            return new DSKeyword(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
        }

        public IList<Keyword> FindBykeywordList(int idKeywordList)
        {
            var crit = CreateCriteria();
            crit.Add(Restrictions.Eq(Columns.KeywordList, new KeywordList { Id = idKeywordList }));
            return Find(crit);
        }

        public IList<string> FindUniqueByKeywordLists(int[] idKeywordLists)
        {
            var crit = CreateCriteria();
            crit
                .SetProjection(Projections.Distinct(Projections.Property(Columns.Keyword)))
                .CreateCriteria(Columns.KeywordList)
                .Add(Restrictions.In(DSKeywordList.Columns.Id, idKeywordLists));
            return crit.List<string>();
        }

        #region Columns Metadata

        public static class Columns
        {
            //public static String Calificacion = "Calificacion";
            public static String Id = "Id";
            public static String Keyword = "Keyword";
            public static String KeywordList = "KeywordList";
        }

        #endregion

    }
}