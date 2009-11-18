using System;
using System.Collections.Generic;
using System.Text;

namespace Eucalypto.Wiki
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

        private string mApprovePermissions = SecurityHelper.NONE;
        /// <summary>
        /// Gets or sets the roles that are enabled to approve and enable the articles
        /// </summary>
        public virtual string ApprovePermissions
        {
            get { return mApprovePermissions; }
            set { mApprovePermissions = value; }
        }

        private bool mAutoApprove = true;
        /// <summary>
        /// Gets or sets if automatically approve the article when adding it.
        /// If set to true the editor don't need to approve the articles, their are automatically approved.
        /// </summary>
        public virtual bool AutoApprove
        {
            get { return mAutoApprove; }
            set { mAutoApprove = value; }
        }

        private bool mAttachEnabled = true;
        public virtual bool AttachEnabled
        {
            get { return mAttachEnabled; }
            set { mAttachEnabled = value; }
        }

        private string mAttachExtensions = Attachment.FileHelper.EXTENSIONS_ALL;
        /// <summary>
        /// Accepted file name extensions.
        /// </summary>
        public virtual string AttachExtensions
        {
            get { return mAttachExtensions; }
            set { mAttachExtensions = value; }
        }

        private int mAttachMaxSize = 500;
        /// <summary>
        /// Maximum size of the attachment file expressed in kb
        /// </summary>
        public virtual int AttachMaxSize
        {
            get { return mAttachMaxSize; }
            set { mAttachMaxSize = value; }
        }

        private XHtmlMode mXHtmlMode = XHtmlMode.StrictValidation;
        /// <summary>
        /// Gets or sets the xhtml validation mode
        /// </summary>
        public virtual XHtmlMode XHtmlMode
        {
            get { return mXHtmlMode; }
            set { mXHtmlMode = value; }
        }

        private WikiBackupMode mBackupMode = WikiBackupMode.Always;
        /// <summary>
        /// Gets or sets the backup mode
        /// </summary>
        public virtual WikiBackupMode BackupMode
        {
            get { return mBackupMode; }
            set { mBackupMode = value; }
        }

        private IList<Article> mArticles;
        protected IList<Article> Articles
        {
            get { return mArticles; }
            set { mArticles = value; }
        }
    }
}
