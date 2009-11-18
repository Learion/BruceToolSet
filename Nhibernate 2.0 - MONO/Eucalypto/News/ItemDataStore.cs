using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Eucalypto.Common;
using NHibernateDataStore.Common;

namespace Eucalypto.News
{
    /// <summary>
    /// Class that use NHibernate to save the Item data
    /// </summary>
    public class ItemDataStore : EntityDataStoreBase<Item, string>
    {
        public ItemDataStore(TransactionScope transactionScope)
            : base(transactionScope)
        {
        }


        public IList<Item> FindByCategory(Category category, PagingInfo paging)
        {
            ICriteria criteria = CreateCriteria();
            criteria.CreateCriteria("Category").Add(Expression.Eq("Id", category.Id));
            criteria.AddOrder(Order.Desc("NewsDate"));

            return Find(criteria, paging);
        }

        public IList<Item> FindByFields(Filter<string> categoryName,
                                        Filter<string> tag, 
                                           DateTime? fromDate, DateTime? toDate,
                                           PagingInfo paging)
        {
            ICriteria criteria = CreateCriteria();

            if (tag != null)
                criteria.Add(tag.ToCriterion("Tag"));

            if (fromDate != null)
                criteria.Add(Expression.Ge("NewsDate", fromDate));

            if (toDate != null)
                criteria.Add(Expression.Le("NewsDate", toDate));

            if (categoryName != null)
            {
                ICriteria categoryCriteria = criteria.CreateCriteria("Category");

                categoryCriteria.Add(categoryName.ToCriterion("Name"));
            }

            criteria.AddOrder(Order.Desc("NewsDate"));

            return Find(criteria, paging);
        }
    }
}
