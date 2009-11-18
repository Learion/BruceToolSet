#region Using Directives

using System.Collections.Generic;
using NHibernate;

#endregion

namespace NHibernateDataStore.Common
{
    public interface IDao<T, IdT>
    {
        ISession NHibernateSession { get; }
        IList<T> Find(ICriteria criteria);
        IList<T> Find(ICriteria criteria, IPagingInfo pagingInfo);
        IList<T> FindAll();

        T FindByKey(IdT id, bool shouldLock);
        T FindByKey(IdT id);
        T FindUnique(ICriteria criteria);

        void Insert(T entity);
        void InsertOrUpdate(T entity);
        T InsertOrUpdateCopy(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(IdT key);

        void Attach(T entity);
        int Count(ICriteria criteria);

        ICriteria CreateCriteria();
        IQuery CreateQuery(string hql);
    }
}