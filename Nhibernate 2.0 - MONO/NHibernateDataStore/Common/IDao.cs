
using System.Collections.Generic;


namespace NHibernateDataStore.Common
{
    public interface IDao<T, IdT>
    {
        IList<T> Find(NHibernate.ICriteria criteria);
        IList<T> Find(NHibernate.ICriteria criteria, IPagingInfo pagingInfo);
        IList<T> FindAll();

        T FindByKey(IdT id, bool shouldLock);
        T FindByKey(IdT id);
        T FindUnique(NHibernate.ICriteria criteria);

        void Insert(T entity);
        void InsertOrUpdate(T entity);
        T InsertOrUpdateCopy(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(IdT key);

        void Attach(T entity);
        int Count(NHibernate.ICriteria criteria);

        NHibernate.ICriteria CreateCriteria();
        NHibernate.IQuery CreateQuery(string hql);

        ITransactionScope TransactionScope { get; }



    }
}
