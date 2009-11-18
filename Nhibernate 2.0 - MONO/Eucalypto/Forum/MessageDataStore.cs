using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Eucalypto.Common;
using NHibernateDataStore.Common;

namespace Eucalypto.Forum
{
    /// <summary>
    /// Class that use NHibernate to save the Message data
    /// </summary>
    public class MessageDataStore : EntityDataStoreBase<Message, string>
    {
        public MessageDataStore(TransactionScope transactionScope)
            : base(transactionScope)
        {
        }

        public IList<Message> FindByTopic(Topic topic)
        {
            ICriteria criteria = CreateCriteria();
            criteria.CreateCriteria("Topic").Add(Expression.Eq("Id", topic.Id));
            criteria.AddOrder(Order.Asc("InsertDate"));

            return Find(criteria);
        }

        /// <summary>
        /// Returns the message root of a specified topic
        /// </summary>
        /// <param name="idTopic"></param>
        /// <returns></returns>
        public Message FindByTopicRoot(Topic topic)
        {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Expression.IsNull("IdParentMessage"));
            criteria.CreateCriteria("Topic").Add(Expression.Eq("Id", topic.Id));

            return FindUnique(criteria);
        }

        public int MessageCountByTopic(Topic topic)
        {
            ICriteria criteria = CreateCriteria();
            criteria.CreateCriteria("Topic").Add(Expression.Eq("Id", topic.Id));

            return Count(criteria);
        }

        public IList<Message> FindByParent(string idParent)
        {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Expression.Eq("IdParentMessage", idParent));
            criteria.AddOrder(Order.Asc("InsertDate"));

            return Find(criteria);
        }

        /// <summary>
        /// The fields are aggregated with an AND expression.
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="searchFor"></param>
        /// <param name="owner"></param>
        /// <param name="tag"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public IList<Message> FindByFields(Filter<string> categoryName,
                                           Filter<string> searchFor,
                                           Filter<string> owner,
                                           Filter<string> tag,  
                                           DateTime? fromDate, DateTime? toDate,
                                           PagingInfo paging)
        {
            ICriteria criteria = CreateCriteria();

            if (owner != null)
                criteria.Add(owner.ToCriterion("Owner"));

            if (tag != null)
                criteria.Add(tag.ToCriterion("Tag"));

            if (searchFor != null)
                criteria.Add(Expression.Or(searchFor.ToCriterion("Title"),
                                           searchFor.ToCriterion("Body")));

            if (fromDate != null)
                criteria.Add(Expression.Ge("InsertDate", fromDate));

            if (toDate != null)
                criteria.Add(Expression.Le("InsertDate", toDate));

            ICriteria topicCriteria = criteria.CreateCriteria("Topic");

            if (categoryName != null)
            {
                ICriteria forumCriteria = topicCriteria.CreateCriteria("Category");
                forumCriteria.Add(categoryName.ToCriterion("Name"));
            }
            else
                throw new ForumNotSpecifiedException();

            criteria.AddOrder(Order.Desc("InsertDate"));

            return Find(criteria, paging);
        }
    }
}
