using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernateDataStore.Common;

namespace Eucalypto.Forum
{
    /// <summary>
    /// Class that use NHibernate to save the Topic data
    /// </summary>
    public class TopicDataStore : EntityDataStoreBase<Topic, string>
    {
        public TopicDataStore(TransactionScope transactionScope)
            : base(transactionScope)
        {
        }

        /// <summary>
        /// Returns all the topics with InsertDate greater than or equal fromDate and less than or equal toDate and of the specified forum ordered by InsertDate DESC
        /// </summary>
        /// <param name="category"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IList<Topic> Find(Category category, DateTime fromDate, DateTime toDate)
        {
            ICriteria criteria = CreateCriteria();
            criteria.CreateCriteria("Category").Add(Expression.Eq("Id", category.Id));
            criteria.Add(Expression.Ge("InsertDate", fromDate));
            criteria.Add(Expression.Le("InsertDate", toDate));
            criteria.AddOrder(Order.Desc("InsertDate"));

            return Find(criteria);
        }

        /// <summary>
        /// Returns all the topics with of the specified forum paginating the values ordered by InsertDate DESC.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public IList<Topic> Find(Category category, PagingInfo paging)
        {
            ICriteria criteria = CreateCriteria();
            criteria.CreateCriteria("Category").Add(Expression.Eq("Id", category.Id));
            criteria.AddOrder(Order.Desc("InsertDate"));

            return Find(criteria, paging);
        }

        public IList<Topic> Find(Category category)
        {
            ICriteria criteria = CreateCriteria();
            criteria.CreateCriteria("Category").Add(Expression.Eq("Id", category.Id));
            criteria.AddOrder(Order.Desc("InsertDate"));

            return Find(criteria);
        }
    }
}
