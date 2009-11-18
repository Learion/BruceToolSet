using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernateDataStore.Common;

namespace Eucalypto.Wiki
{
    /// <summary>
    /// Class that use NHibernate to save the FileAttachment data
    /// </summary>
    public class FileAttachmentDataStore : EntityDataStoreBase<FileAttachment, string>
    {
        public FileAttachmentDataStore(TransactionScope transactionScope)
            : base(transactionScope)
        {
        }

        public string[] GetArticleAttachments(Article article, EnabledStatus enabledStatus)
        {
            string hql = "SELECT T.Name FROM FileAttachment T INNER JOIN T.Article A WHERE A.Id = :articleId";

            if (enabledStatus == EnabledStatus.Disabled)
                hql += " AND T.Enabled = 0";
            else if (enabledStatus == EnabledStatus.Enabled)
                hql += " AND T.Enabled = 1";

            IQuery query = CreateQuery(hql);

            query.SetParameter("articleId", article.Id);

            IList<string> names = query.List<string>();

            string[] arrNames = new string[names.Count];
            names.CopyTo(arrNames, 0);
            return arrNames;
        }

        public FileAttachment FindByArticleVersion(Article article, string name)
        {
            ICriteria criteria = CreateCriteria();
            criteria.CreateCriteria("Article").Add(Expression.Eq("Id", article.Id));
            criteria.Add(Expression.Eq("Name", name));

            return FindUnique(criteria);
        }
    }
}
