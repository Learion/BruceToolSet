using System;
using System.Collections.Generic;
using System.Text;

namespace Eucalypto.Roles
{
    public class UserInRole
    {
        protected UserInRole()
        {

        }

        public UserInRole(string application, string userName, string roleName)
        {
            ApplicationName = application;
            RoleName = roleName;
            UserName = userName;
        }


        private string mId;
        public virtual string Id
        {
            get { return mId; }
            protected set { mId = value; }
        }


        private string mApplicationName;
        public virtual string ApplicationName
        {
            get { return mApplicationName; }
            protected set { mApplicationName = value; }
        }

        private string mUserName;
        public virtual string UserName
        {
            get { return mUserName; }
            protected set { mUserName = value; }
        }

        private string mRoleName;
        public virtual string RoleName
        {
            get { return mRoleName; }
            protected set { mRoleName = value; }
        }
    }
}
