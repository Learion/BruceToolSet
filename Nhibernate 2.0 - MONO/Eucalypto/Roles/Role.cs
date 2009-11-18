using System;
using System.Collections.Generic;
using System.Text;

namespace Eucalypto.Roles
{
    public class Role
    {
        protected Role()
        {

        }

        public Role(string applicationName, string name)
        {
            ApplicationName = applicationName;
            Name = name;
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

        private string mApplicationName;
        public virtual string ApplicationName
        {
            get { return mApplicationName; }
            protected set { mApplicationName = value; }
        }
        private string mComment;
        public virtual string Comment
        {
            get { return mComment; }
            set { mComment = value; }
        }

        private string mTag;
        /// <summary>
        /// Field that can be used for user defined extensions.
        /// </summary>
        public virtual string Tag
        {
            get { return mTag; }
            set { mTag = value; }
        }

    }
}
