using System;
using System.Collections.Generic;
using System.Text;

namespace Eucalypto.Profile
{
    public class ProfileProperty
    {
        protected ProfileProperty()
        {

        }

        public ProfileProperty(ProfileUser pUser, string pName)
        {
            User = pUser;
            Name = pName;
        }

        private string mId;
        public virtual string Id
        {
            get { return mId; }
            protected set { mId = value; }
        }

        private ProfileUser mUser;
        public virtual ProfileUser User
        {
            get { return mUser; }
            protected set { mUser = value; }
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

        private string mStringValue;
        public virtual string StringValue
        {
            get { return mStringValue; }
            protected set { mStringValue = value; }
        }

        private byte[] mBinaryValue;
        public virtual byte[] BinaryValue
        {
            get { return mBinaryValue; }
            protected set { mBinaryValue = value; }
        }

        public virtual void SetValue(byte[] val)
        {
            StringValue = null;
            BinaryValue = val;
        }

        public virtual void SetValue(string val)
        {
            BinaryValue = null;
            StringValue = val;
        }

        public virtual void SetNull()
        {
            BinaryValue = null;
            StringValue = null;
        }
    }
}
