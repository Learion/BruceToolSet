using System;
using System.Collections.Generic;
using System.Text;

namespace Eucalypto.Wiki
{
    public class VersionedArticle : ArticleBase
    {
        protected VersionedArticle()
        {

        }

        /// <summary>
        /// Create a new versioned article from the specified article source
        /// </summary>
        /// <param name="source"></param>
        public VersionedArticle(Article source)
            : base(source)
        {
            this.Article = source;
        }

        private Article mArticle;
        public virtual Article Article
        {
            get { return mArticle; }
            protected set { mArticle = value; }
        }
    }
}
