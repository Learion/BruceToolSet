using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernateDataStore.Common;

namespace Eucalypto.Forum
{
    /// <summary>
    /// Class that use NHibernate to save the Category data
    /// </summary>
    public class CategoryDataStore : EntityDataStoreBase<Category, string>
    {
        public CategoryDataStore(ITransactionScope transactionScope)
            : base(transactionScope)
        {
        }

        public Category FindByName(string name)
        {
            ICriteria criteria = CreateCriteria();
            criteria.Add(Expression.Eq("Name", name));

            return FindUnique(criteria);
        }

        public IList<Category> FinAll()
        {
            ICriteria criteria = CreateCriteria();
            criteria.AddOrder(Order.Asc("DisplayName"));

            return base.Find(criteria);
        }

    }
}
