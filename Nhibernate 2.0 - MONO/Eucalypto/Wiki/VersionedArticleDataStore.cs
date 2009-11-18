using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernateDataStore.Common;

namespace Eucalypto.Wiki
{
    /// <summary>
    /// Class that use NHibernate to save the VersionedArticle entities
    /// </summary>
    public class VersionedArticleDataStore : EntityDataStoreBase<VersionedArticle, string>
    {
        public VersionedArticleDataStore(TransactionScope transactionScope)
            : base(transactionScope)
        {
        }

        public VersionedArticle FindByArticleVersion(Article article, int version)
        {
            ICriteria criteria = CreateCriteria();
            criteria.CreateCriteria("Article").Add(Expression.Eq("Id", article.Id));
            criteria.Add(Expression.Eq("Version", version));

            return FindUnique(criteria);
        }

        /// <summary>
        /// Get a list of versionedarticle ordered by version desc
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public IList<VersionedArticle> GetArticleVersions(Article article)
        {
            //string hql = "SELECT T.Version FROM VersionedArticle T INNER JOIN T.Article A WHERE A.Id = :articleId";

            //IQuery query = CreateQuery(hql);

            //query.SetParameter("articleId", article.Id);

            //IList<int> versions = query.List<int>();

            //int[] arrVersions = new int[versions.Count];
            //versions.CopyTo(arrVersions, 0);
            //return arrVersions;

            ICriteria criteria = CreateCriteria();
            criteria.CreateCriteria("Article").Add(Expression.Eq("Id", article.Id));
            criteria.AddOrder(Order.Desc("Version"));

            return Find(criteria);
        }
    }
}
