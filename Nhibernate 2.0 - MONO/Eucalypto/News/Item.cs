using System;

using Eucalypto.Interceptor;

namespace Eucalypto.News
{
    public class Item : IOwner, IAudit
    {
        protected Item()
        {

        }

        public Item(Category pCategory, string pOwner, 
                        string pTitle, string pDescription,
                        string pURL, string pURLName, DateTime pNewsDate)
        {
            Owner = pOwner;
            Title = pTitle;
            Author = pOwner;
            Category = pCategory;
            Description = pDescription;
            URL = pURL;
            URLName = pURLName;
            NewsDate = pNewsDate;
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
        /// <summary>
        /// Gets or sets the original user that has create the article and that has the ownership of it
        /// </summary>
        public virtual string Owner
        {
            get { return mOwner; }
            protected set { mOwner = value; }
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

        private string mURL;
        public virtual string URL
        {
            get { return mURL; }
            set { mURL = value; }
        }

        private string mURLName;
        public virtual string URLName
        {
            get { return mURLName; }
            set { mURLName = value; }
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

        private DateTime mNewsDate;
        public virtual DateTime NewsDate
        {
            get { return mNewsDate; }
            set { mNewsDate = value; }
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
