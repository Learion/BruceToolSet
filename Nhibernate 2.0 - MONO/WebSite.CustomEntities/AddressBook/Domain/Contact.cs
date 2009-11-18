using System;
using System.Collections.Generic;
using System.Text;

namespace WebSite.CustomEntities.AddressBook.Domain
{
    public class Contact
    {
        public Contact() { }

        public Contact(string displayName)
        {
            DisplayName = displayName;
        }

        private string mId;
        public virtual string Id
        {
            get { return mId; }
            protected set { mId = value; }
        }

        private string mDisplayName;
        public virtual string DisplayName
        {
            get { return mDisplayName; }
            set { mDisplayName = value; }
        }

        private string mFirstName;
        public virtual string FirstName
        {
            get { return mFirstName; }
            set { mFirstName = value; }
        }
        private string mLastName;
        public virtual string LastName
        {
            get { return mLastName; }
            set { mLastName = value; }
        }
        private string mTelephone1;
        public virtual string Telephone1
        {
            get { return mTelephone1; }
            set { mTelephone1 = value; }
        }
        private string mTelephone2;
        public virtual string Telephone2
        {
            get { return mTelephone2; }
            set { mTelephone2 = value; }
        }

        private string mNote;
        public virtual string Note
        {
            get { return mNote; }
            set { mNote = value; }
        }

        private string mAddress;
        public virtual string Address
        {
            get { return mAddress; }
            set { mAddress = value; }
        }
    }
}