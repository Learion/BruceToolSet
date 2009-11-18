using System;
using System.Collections.Generic;
using NHibernateDataStore;
using Ninject.Core;
using WebSite.CustomEntities.AddressBook.Domain;

namespace WebSite.CustomEntities.AddressBook
{
    [System.ComponentModel.DataObject(true)]
    public class ContactODS
    {
        private readonly ContactDataStore contactDataStore;

        public static String ConnectionStringName
        {
           get
           {
               return CustomEntities.Default.ConnectionStringName;
           }
        }

        [Inject]
        public ContactODS(ContactDataStore contactDataStore)
        {
            this.contactDataStore = contactDataStore;
        }

        #region ODS Methods

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public Contact GetById(String Id)
        {
            return contactDataStore.FindByKey(Id);
        }
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public IList<Contact> GetAll()
        {
            return contactDataStore.FinAll();
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert)]
        public void Insert(Contact contact)
        {
            Check.Require(contact != null,"contact must not be null");
            contactDataStore.Insert(contact);
            contactDataStore.Refresh();
        }
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update)]
        public void Update(Contact contact)
        {
            Check.Require(contact != null, "contact must not be null");
            contactDataStore.Update(contact);
            contactDataStore.Refresh();
        }


        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert)]
        public void CreateContact(String displayName, string address, string firstName, string lastName, string telephone, string note, out String Id)
        {
            var c = new Contact(displayName)
                            {
                                Address = address,
                                FirstName = firstName,
                                LastName = lastName,
                                Telephone1 = telephone,
                                Note = note
                            };


            contactDataStore.Insert(c);

            contactDataStore.Refresh();

            Id = c.Id;



        }
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update)]
        public void UpdateContact(String Id, String displayName, string address, string firstName, string lastName, string telephone, string note)
        {


            var c = contactDataStore.FindByKey(Id);
            c.Address = address;
            c.DisplayName = displayName;
            c.FirstName = firstName;
            c.LastName = lastName;
            c.Telephone1 = telephone;
            c.Note = note;

            contactDataStore.Update(c);

            contactDataStore.Refresh();

        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public void DeleteContact(String Id)
        {
            contactDataStore.Delete(Id);
            contactDataStore.Refresh();
        }

        #endregion



    }
}
