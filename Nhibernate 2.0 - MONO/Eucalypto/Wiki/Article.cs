using System;
using System.Collections.Generic;
using System.Text;
using Eucalypto.Interceptor;

namespace Eucalypto.Wiki
{
    public class Article : ArticleBase, ISearchResult, IAudit
    {
        protected Article()
        {

        }

        public Article(Category pCategory, string pName, string pOwner, 
                        string pTitle, string pDescription, string pBody)
            : base(pOwner, pTitle)
        {
            Category = pCategory;
            Name = pName;
            Description = pDescription;
            Body = pBody;
        }

        private string mName;
        /// <summary>
        /// The unique name of the article item. Must be unique for all the categories
        /// </summary>
        public virtual string Name
        {
            get { return mName; }
            protected set
            {
                Eucalypto.Common.EntityHelper.ValidateCode("Name", value);
                mName = value;
            }
        }

        private Category mCategory;
        public virtual Category Category
        {
            get { return mCategory; }
            protected set { mCategory = value; }
        }

        private bool mEnabled = true;
        /// <summary>
        /// Gets or sets if the article is enabled.
        /// A disabled article is visible only by an administrator/editor.
        /// When a user delete an article it became disable.
        /// Only the administrator can delete articles.
        /// </summary>
        public virtual bool Enabled
        {
            get { return mEnabled; }
            set { mEnabled = value; }
        }

        private bool mApproved = false;
        /// <summary>
        /// Gets or sets if the article is approved.
        /// A not approved article is visible only by an administrator/editor or by the owner.
        /// Only the administrator/editor can approve an article.
        /// When a user submit a new article it must be approved.
        /// </summary>
        public virtual bool Approved
        {
            get { return mApproved; }
            set { mApproved = value; }
        }

        private IList<FileAttachment> mAttachments;
        /// <summary>
        /// List used for cascading rules
        /// </summary>
        protected IList<FileAttachment> Attachments
        {
            get { return mAttachments; }
            set { mAttachments = value; }
        }

        private IList<VersionedArticle> mVersions;
        /// <summary>
        /// List used for cascading rules
        /// </summary>
        protected IList<VersionedArticle> Versions
        {
            get { return mVersions; }
            set { mVersions = value; }
        }

        #region ISearchResult Members

        string ISearchResult.Title
        {
            get { return this.Title; }
        }

        string ISearchResult.Owner
        {
            get { return this.Owner; }
        }

        string ISearchResult.Description
        {
            get { return this.Description; }
        }

        DateTime ISearchResult.Date
        {
            get { return this.UpdateDate; }
        }

        string ISearchResult.Category
        {
            get { return this.Category.DisplayName; }
        }

        #endregion
    }
}
