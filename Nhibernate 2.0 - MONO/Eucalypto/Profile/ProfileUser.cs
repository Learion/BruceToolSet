using System;
using System.Collections.Generic;
using System.Text;
using Eucalypto.Interceptor;

namespace Eucalypto.Profile
{
    public class ProfileUser : IAudit
    {
        protected ProfileUser()
        {

        }

        public ProfileUser(string applicationName, string userName, ProfileType profileType)
        {
            ApplicationName = applicationName;
            Name = userName;
            ProfileType = profileType;
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

        private DateTime mInsertDate;
        public virtual DateTime InsertDate
        {
            get { return mInsertDate; }
            set { mInsertDate = value; }
        }

        private DateTime mUpdateDate;
        public virtual DateTime UpdateDate
        {
            get { return mUpdateDate; }
            set { mUpdateDate = value; }
        }

        private string mApplicationName;
        public virtual string ApplicationName
        {
            get { return mApplicationName; }
            protected set { mApplicationName = value; }
        }

        private ProfileType mProfileType;
        public virtual ProfileType ProfileType
        {
            get { return mProfileType; }
            set { mProfileType = value; }
        }

        private DateTime mLastActivityDate = DateTime.Now;
        /// <summary>
        /// Changes when calling SetPropertyValues and GetPropertyValues method
        /// </summary>
        public virtual DateTime LastActivityDate
        {
            get { return mLastActivityDate; }
            set { mLastActivityDate = value; }
        }

        private DateTime mLastPropertyChangedDate = DateTime.Now;
        /// <summary>
        /// This property differs from the UpdateDate because change only when calling SetPropertyValues method
        /// </summary>
        public virtual DateTime LastPropertyChangedDate
        {
            get { return mLastPropertyChangedDate; }
            set { mLastPropertyChangedDate = value; }
        }

        private IList<ProfileProperty> mProperties;
        /// <summary>
        /// List used for cascading rules
        /// </summary>
        protected IList<ProfileProperty> Properties
        {
            get { return mProperties; }
            set { mProperties = value; }
        }
    }

    public enum ProfileType
    {
        Anonymous = 1,
        Authenticated = 2
    }
}
