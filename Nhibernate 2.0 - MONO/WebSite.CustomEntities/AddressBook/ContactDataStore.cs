
using System.Collections.Generic;
using NHibernate.Criterion;

using Ninject.Core;
using WebSite.CustomEntities.AddressBook.Domain;
using NHibernateDataStore.Common;



namespace WebSite.CustomEntities.AddressBook
{
    public class ContactDataStore : EntityDataStoreBase<Contact, string>
    {
        public ContactDataStore(ITransactionScope transactionScope)
            : base(transactionScope)
        {
        }

        [Inject]
        public ContactDataStore([CustomEntitiesAttribute] NHibernate.ISession session)
            : base(session)
        {

        }

        public Contact FindByDisplayName(string displayName)
        {
            NHibernate.ICriteria criteria = CreateCriteria();
            criteria.Add(Restrictions.Eq("DisplayName", displayName));

            return FindUnique(criteria);
        }

        public IList<Contact> FinAll()
        {
            var criteria = CreateCriteria();
            criteria.AddOrder(Order.Asc("DisplayName"));

            return Find(criteria);
        }

        public void Refresh()
        {
            TransactionScope.NHibernateSession.Flush();
        }
    }
}
