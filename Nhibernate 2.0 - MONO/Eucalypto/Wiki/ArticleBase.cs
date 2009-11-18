using System;
using System.Collections.Generic;
using System.Text;

namespace Eucalypto.Wiki
{
    public abstract class ArticleBase : IOwner
    {
        protected ArticleBase()
        {

        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="other"></param>
        public ArticleBase(ArticleBase other)
        {
            Owner = other.Owner;
            Title = other.Title;
            Description = other.Description;
            Body = other.Body;
            TOC = other.TOC;
            Version = other.Version;
            Author = other.Author;
            UpdateUser = other.UpdateUser;
            Tag = other.Tag;
            UpdateDate = other.UpdateDate;
            InsertDate = other.InsertDate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pOwner">The owner is the same of the update user when creating the article</param>
        /// <param name="pTitle"></param>
        public ArticleBase(string pOwner, string pTitle)
        {
            Owner = pOwner;
            Title = pTitle;
            UpdateUser = pOwner;
        }

        private string mId;
        public virtual string Id
        {
            get { return mId; }
            protected set { mId = value; }
        }

        private int mVersion = 1;
        public virtual int Version
        {
            get { return mVersion; }
            protected set { mVersion = value; }
        }

        public virtual void IncrementVersion()
        {
            Version++;
        }

        private string mOwner;
        /// <summary>
        /// Gets or sets the original user that has create the article and that has the ownership of it
        /// </summary>
        public virtual string Owner
        {
            get { return mOwner; }
            protected set { mOwner = value; }
        }

        private string mUpdateUser;
        /// <summary>
        /// Gets or sets a free text field used to store the update user informations (last update)
        /// </summary>
        public virtual string UpdateUser
        {
            get { return mUpdateUser; }
            set { mUpdateUser = value; }
        }

        private string mTitle;
        public virtual string Title
        {
            get { return mTitle; }
            set { mTitle = value; }
        }

        private string mDescription;
        public virtual string Description
        {
            get { return mDescription; }
            set { mDescription = value; }
        }

        private string mBody;
        public virtual string Body
        {
            get { return mBody; }
            set { mBody = value; }
        }

        private string mTOC;
        /// <summary>
        /// Gets or sets the table of contents
        /// </summary>
        public virtual string TOC
        {
            get { return mTOC; }
            set { mTOC = value; }
        }

        private string mAuthor;
        /// <summary>
        /// Gets or sets a free text field used to store the author informations (that can be different from the owner)
        /// </summary>
        public virtual string Author
        {
            get { return mAuthor; }
            set { mAuthor = value; }
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
