using System;
using System.Collections.Generic;
using System.Text;

namespace Eucalypto.Wiki
{
    public class FileAttachment : Attachment.FileInfo
    {
        protected FileAttachment()
        {
        }

        public FileAttachment(Article pArticle, string name, string contentType, byte[] contentData)
            :base(name, contentType, contentData)
        {
            Article = pArticle;
        }

        private string mId;
        public virtual string Id
        {
            get { return mId; }
            protected set { mId = value; }
        }

        private Article mArticle;
        public virtual Article Article
        {
            get { return mArticle; }
            protected set { mArticle = value; }
        }

        private bool mEnabled = true;
        /// <summary>
        /// Gets or sets if the attachment is enabled.
        /// A disabled attachment is visible only by an administrator/editor.
        /// When a user delete an attachment it became disabled.
        /// Only the administrator can delete attachments.
        /// </summary>
        public virtual bool Enabled
        {
            get { return mEnabled; }
            set { mEnabled = value; }
        }
    }
}
