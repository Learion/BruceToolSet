using System;
using System.Collections.Generic;
using System.Text;

namespace Eucalypto.Attachment
{
    public class FileInfo
    {
        protected FileInfo()
        {
        }

        public FileInfo(string name, string contentType, byte[] contentData)
        {
            Name = name;
            ContentType = contentType;
            ContentData = contentData;
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

        private byte[] mContentData;
        public virtual byte[] ContentData
        {
            get { return mContentData; }
            protected set { mContentData = value; }
        }

        private string mDescription;
        public virtual string Description
        {
            get { return mDescription; }
            set { mDescription = value; }
        }

        private string mContentType;
        public virtual string ContentType
        {
            get { return mContentType; }
            protected set { mContentType = value; }
        }
    }
}
