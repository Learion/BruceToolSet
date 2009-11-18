using System;
using System.Collections.Generic;
using System.Text;

namespace Eucalypto.News
{
    public class Category : IAccessControl
    {
        protected Category()
        {

        }

        public Category(string name, string displayName)
        {
            Name = name;
            DisplayName = displayName;
        }


        private string mId;
        public virtual string Id
        {
            get { return mId; }
            protected set { mId = value; }
        }

        private string mName;
        public virtual string Name
        {
            get { return mName; }
            protected set 
            {
                Eucalypto.Common.EntityHelper.ValidateCode("Name", value);
                mName = value;
            }
        }

        private string mDisplayName;
        public virtual string DisplayName
        {
            get { return mDisplayName; }
            set { mDisplayName = value; }
        }

        private string mDescription;
        public virtual string Description
        {
            get { return mDescription; }
            set { mDescription = value; }
        }

        private string mReadPermissions = SecurityHelper.ALL_USERS;
        public virtual string ReadPermissions
        {
            get { return mReadPermissions; }
            set { mReadPermissions = value; }
        }

        private string mEditPermissions = SecurityHelper.NONE;
        public virtual string EditPermissions
        {
            get { return mEditPermissions; }
            set { mEditPermissions = value; }
        }

        private string mInsertPermissions = SecurityHelper.AUTHENTICATED_USERS;
        public virtual string InsertPermissions
        {
            get { return mInsertPermissions; }
            set { mInsertPermissions = value; }
        }

        private string mDeletePermissions = SecurityHelper.NONE;
        public virtual string DeletePermissions
        {
            get { return mDeletePermissions; }
            set { mDeletePermissions = value; }
        }


        private IList<Item> mItems;
        protected IList<Item> Items
        {
            get { return mItems; }
            set { mItems = value; }
        }
    }
}
