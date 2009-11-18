using System;
using System.Collections.Generic;
using System.Text;
using Eucalypto.Interceptor;

namespace Eucalypto.Forum
{
    public class Message : IAudit, IOwner, ISearchResult
    {
        protected Message()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pTopic"></param>
        /// <param name="pIdParentMessage">Null for the first message</param>
        /// <param name="pOwner"></param>
        /// <param name="pTitle"></param>
        /// <param name="pBody"></param>
        /// <param name="pAttachment"></param>
        public Message(Topic pTopic, string pIdParentMessage,
                    string pOwner, string pTitle,
                    string pBody, Attachment.FileInfo pAttachment)
        {
            Topic = pTopic;
            Owner = pOwner;
            Title = pTitle;
            Body = pBody;
            IdParentMessage = pIdParentMessage;
            Attachment = pAttachment;
        }

        private string mId;
        public virtual string Id
        {
            get { return mId; }
            protected set { mId = value; }
        }

        private Topic mTopic;
        public virtual Topic Topic
        {
            get { return mTopic; }
            protected set { mTopic = value; }
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

        private string mBody;
        public virtual string Body
        {
            get { return mBody; }
            set { mBody = value; }
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

        private string mIdParentMessage;
        /// <summary>
        /// Null for the first message
        /// </summary>
        public virtual string IdParentMessage
        {
            get { return mIdParentMessage; }
            protected set { mIdParentMessage = value; }
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


        private Attachment.FileInfo mAttachment;
        public virtual Attachment.FileInfo Attachment
        {
            get { return mAttachment; }
            protected set { mAttachment = value; }
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
            get 
            {
                XHTMLText xhtml = new XHTMLText();
                xhtml.Load(this.Body);

                return xhtml.GetShortText();
            }
        }

        DateTime ISearchResult.Date
        {
            get { return this.UpdateDate; }
        }

        string ISearchResult.Category
        {
            get { return this.Topic.Category.DisplayName + "\\" + this.Topic.Title; }
        }

        #endregion
    }
}
