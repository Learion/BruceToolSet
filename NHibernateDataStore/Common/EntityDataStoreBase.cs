#region Using Directives

using System.Collections.Generic;
using NHibernate;
using NHibernateDataStore.Exceptions;

#endregion

namespace NHibernateDataStore.Common
{
    /// <summary>
    /// Abstract generic class that use NHibernate to save a specified entity class.
    /// This is a basic Data Access Object (DAO) pattern implementation.
    /// Use a TransactionScope instance to share the same transaction with more than one db operations.
    /// or you can use a NHibernate ISession
    /// </summary>
    public abstract class EntityDataStoreBase<T, TId> : IDao<T, TId>
    {
        /// <summary>
        /// Construct an instance of EntityDataStoreBase using an open NHibernate.ISession
        /// </summary>
        /// <param name="session"></param>
        protected EntityDataStoreBase(ISession session)
        {
            NHibernateSession = session;
        }

        #region IDao<T,TId> Members

        /// <summary>
        /// Creates an Instance of an object that implements the NHibernate.ICriteria to allows the construction of custom criterias to filter the returned data when used with the find Method
        /// </summary>
        /// <returns>an Instance of an object that implements the NHibernate.ICriteria</returns>
        public ICriteria CreateCriteria()
        {
            return NHibernateSession.CreateCriteria(typeof (T));
        }

        /// <summary>
        /// Creates an Instance of an object that implements the IQuery interface to create a custom query to find the data using a custom hql.
        /// </summary>
        /// <param name="hql">custom query in the hql format</param>
        /// <returns>a custom instance of an IQuery</returns>
        public IQuery CreateQuery(string hql)
        {
            return NHibernateSession.CreateQuery(hql);
        }

        public ISession NHibernateSession { get; private set; }

        #endregion

        #region Find

        ///<summary>
        /// Return an Instance of the Object used to create this DataStore
        ///</summary>
        ///<param name="id">the Id</param>
        /// <param name="shouldLock">if should Lock we use LockMode.Upgrade</param>
        ///<returns>an instace of type T</returns>
        public T FindByKey(TId id, bool shouldLock)
        {
            return shouldLock ? NHibernateSession.Get<T>(id, LockMode.Upgrade) : FindByKey(id);
        }

        ///<summary>
        /// Return an Instance of the Object used to create this DataStore
        ///</summary>
        ///<param name="id">the Id</param>
        ///<returns>an instace of type T</returns>
        public T FindByKey(TId id)
        {
            return NHibernateSession.Get<T>(id);
        }

        /// <summary>
        /// Find All the instances of type T in the DataStore
        /// </summary>
        /// <returns>a generic IList of Type T</returns>
        public IList<T> FindAll()
        {
            return CreateCriteria().List<T>();
        }

        /// <summary>
        /// Find a Unique instance of type T in the DataStore filtering the data by the ICriteria.
        /// </summary>
        /// <param name="criteria">ICriteria used to filter the instances.</param>
        /// <returns>a generic IList of Type T.</returns>
        public T FindUnique(ICriteria criteria)
        {
            return (T) criteria.UniqueResult();
        }

        /// <summary>
        /// Find all the instances of type T in the DataStore that match the criteria received.
        /// </summary>
        /// <param name="criteria">an ICriteria used to filter the instances.</param>
        /// <returns>a generic IList of Type T.</returns>
        public IList<T> Find(ICriteria criteria)
        {
            return criteria.List<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public IList<T> Find(ICriteria criteria, IPagingInfo paging)
        {
            paging.RowCount = Count(criteria);

            if (paging.PageSize > 0)
            {
                criteria.SetMaxResults((int) paging.PageSize);
                criteria.SetFirstResult((int) (paging.PageSize*paging.CurrentPage));
            }

            return criteria.List<T>();
        }

        public int Count(ICriteria criteria)
        {
            //TODO: check performance of this method (probably is better to use HQL with the COUNT(*) command)
            return criteria.List().Count;
        }

        #endregion

        #region Delete

        public void Delete(T entity)
        {
            NHibernateSession.Delete(entity);
        }

        public virtual void Delete(TId objId)
        {
            //NOTE: I cannot directly delete an instance if the instance is not attached to the database or is not syncronized

            //TODO Think if there is a fast way to delete the entities (and also think if using an id in the providers delete methods instead of entity instance)

            object entity = FindByKey(objId);
            if (entity == null)
                throw new EntityNotFoundException();

            NHibernateSession.Delete(entity);
        }

        //        /// <summary>
        //        /// Delete all the rows that match the criteria
        //        /// </summary>
        //        /// <param name="criteria"></param>
        //        /// <returns></returns>
        //        protected virtual int Delete(ICriteria criteria)
        //        {
        //#warning check if there is a way to delete all the matched rows without loading it (but consider that someone can have overriden the Delete method to custom delete actions, like delete attachments) considering using nhibernate cascade options but there is still a problems with attachments.
        //            IList<T> list = Find(criteria);

        //            foreach (T entity in list)
        //            {
        //                Delete(entity);
        //            }

        //            return list.Count;
        //        }

        #endregion

        #region Insert

        public virtual void Insert(T obj)
        {
            NHibernateSession.Save(obj);
        }

        #endregion

        #region Update

        /// <summary>
        /// Update the persistent instance with the identifier of the given transient instance.
        /// The update method automatically attach the entity to current session for updating the entity.
        /// </summary>
        /// <param name="obj">A transient instance containing updated state</param>
        public virtual void Update(T obj)
        {
            NHibernateSession.Update(obj);
        }

        /// <summary>
        /// Either Save() or Update() the given instance, depending upon the value of its identifier property.
        /// 
        /// Internally calls NHibernate SaveOrUpdate method.
        /// </summary>
        /// <param name="obj">A transient instance containing updated state</param>
        public virtual void InsertOrUpdate(T obj)
        {
            NHibernateSession.SaveOrUpdate(obj);
        }

        /// <summary>
        /// Copy the state of the given object onto the persistent object with the same
        /// identifier. If there is no persistent instance currently associated with
        /// the session, it will be loaded. Return the persistent instance. If the given
        /// instance is unsaved or does not exist in the database, save it and return
        /// it as a newly persistent instance. Otherwise, the given instance does not
        /// become associated with the session.
        /// 
        /// This method copies the state of the given object onto the persistent object with the same identifier. 
        /// If there is no persistent instance currently associated with the session, it will be loaded. 
        /// The method returns the persistent instance. 
        /// If the given instance is unsaved or does not exist in the database, NHibernate will save it 
        /// and return it as a newly persistent instance. 
        /// Otherwise, the given instance does not become associated with the session.
        /// 
        /// Internally calls NHibernate SaveOrUpdateCopy method.
        /// </summary>
        /// <param name="obj">A transient instance containing updated state</param>
        public virtual T InsertOrUpdateCopy(T obj)
        {
            return (T) NHibernateSession.SaveOrUpdateCopy(obj);
        }

        #endregion

        #region Other entity related methods

        /// <summary>
        /// The Attach() method allows the application to reassociate an unmodified object with a new session.
        /// Tipically you must call this method when an entity is retrived from the db, the session is closed and then a new session is created and you want to use the previous entity.
        /// Internally calls the NHibernate Lock method.
        /// </summary>
        public void Attach(T entity)
        {
            NHibernateSession.Lock(entity, LockMode.None);
        }

        #endregion
    }
}