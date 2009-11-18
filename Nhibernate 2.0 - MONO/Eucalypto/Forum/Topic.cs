using System;
using System.Collections.Generic;
using System.Text;
using Eucalypto.Interceptor;

namespace Eucalypto.Forum
{
    public class Topic : IAudit, IOwner
    {
        protected Topic()
        {

        }

        public Topic(Category pCategory, string pOwner, string pTitle)
        {
            Category = pCategory;
            Owner = pOwner;
            Title = pTitle;
        }

        private string mId;
        public virtual string Id
        {
            get { return mId; }
            protected set { mId = value; }
        }

        private Category mCategory;
        public virtual Category Category
        {
            get { return mCategory; }
            protected set { mCategory = value; }
        }

        private string mOwner;
        public virtual string Owner
        {
            get { return mOwner; }
            protected set { mOwner = value; }
        }

        private string mTitle;
        public virtual string Title
        {
            get { return mTitle; }
            protected set { mTitle = value; }
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

        private IList<Message> mMessages;
        /// <summary>
        /// List used for cascading rules (delete)
        /// </summary>
        protected IList<Message> Messages
        {
            get { return mMessages; }
            set { mMessages = value; }
        }
    }
}
