using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Eucalypto.Common;
using NHibernateDataStore.Common;

namespace Eucalypto.Wiki
{
    /// <summary>
    /// Class that use NHibernate to save the Item data
    /// </summary>
    public class ArticleDataStore : EntityDataStoreBase<Article, string>
    {
        public ArticleDataStore(TransactionScope transactionScope)
            : base(transactionScope)
        {
        }

        public Article FindByName(string name)
        {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Expression.Eq("Name", name));

            return FindUnique(criteria);
        }

        protected void AddCriteriaForStatus(ICriteria criteria, ArticleStatus status)
        {
            switch (status)
            {
                case ArticleStatus.All:
                    break;

                case ArticleStatus.Approved:
                    criteria.Add(Expression.Eq("Approved", true));
                    break;
                case ArticleStatus.NotApproved:
                    criteria.Add(Expression.Eq("Approved", false));
                    break;
                case ArticleStatus.Enabled:
                    criteria.Add(Expression.Eq("Enabled", true));
                    break;
                case ArticleStatus.Disabled:
                    criteria.Add(Expression.Eq("Enabled", false));
                    break;

                case ArticleStatus.EnabledAndApproved:
                    criteria.Add(Expression.Eq("Enabled", true));
                    criteria.Add(Expression.Eq("Approved", true));
                    break;

                case ArticleStatus.DisabledOrNotApproved:
                    criteria.Add(Expression.Or(
                                        Expression.Eq("Enabled", false),
                                        Expression.Eq("Approved", false))
                                        );
                    break;

                default:
                    throw new ArticleStatusNotValidException(status);
            }
        }

        /// <summary>
        /// Find the articles for the specified category and owner
        /// </summary>
        /// <param name="category"></param>
        /// <param name="owner">Null to don't use the filter</param>
        /// <param name="enabledStatus"></param>
        /// <param name="approvedStatus"></param>
        /// <returns></returns>
        public IList<Article> FindByCategoryAndOwner(Category category, string owner,
                            ArticleStatus status)
        {
            ICriteria criteria = CreateCriteria();
            criteria.CreateCriteria("Category").Add(Expression.Eq("Id", category.Id));
            criteria.AddOrder(Order.Asc("Title"));

            AddCriteriaForStatus(criteria, status);

            if (owner != null)
            {
                criteria.Add(Expression.Eq("Owner", owner));
            }

            return Find(criteria);
        }


        /// <summary>
        /// Search for articles with the specified filters.
        /// The filters are aggregated with an AND expression.
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="searchFor"></param>
        /// <param name="author"></param>
        /// <param name="owner"></param>
        /// <param name="tag"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="status"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public IList<Article> FindByFields(Filter<string> categoryName,
                                           Filter<string> searchFor,
                                           Filter<string> author,
                                           Filter<string> owner,
                                           Filter<string> tag,  
                                           DateTime? fromDate, DateTime? toDate,
                                           ArticleStatus status,
                                           PagingInfo paging)
        {
            ICriteria criteria = CreateCriteria();

            if (author != null)
                criteria.Add(author.ToCriterion("Author"));

            if (owner != null)
                criteria.Add(owner.ToCriterion("Owner"));

            if (tag != null)
                criteria.Add(tag.ToCriterion("Tag"));

            if (searchFor != null)
                criteria.Add(Expression.Or(searchFor.ToCriterion("Body"), searchFor.ToCriterion("Title")));

            if (fromDate != null)
                criteria.Add(Expression.Ge("UpdateDate", fromDate));

            if (toDate != null)
                criteria.Add(Expression.Le("UpdateDate", toDate));

            if (categoryName != null)
            {
                ICriteria categoryCriteria = criteria.CreateCriteria("Category");
                categoryCriteria.Add(categoryName.ToCriterion("Name"));
            }

            AddCriteriaForStatus(criteria, status);

            criteria.AddOrder(Order.Desc("UpdateDate"));

            return Find(criteria, paging);
        }
    }
}
