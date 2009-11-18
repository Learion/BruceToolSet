#region

using System.Collections.Generic;

#endregion

namespace SEOToolSet.Entities.Wrappers
{
    public class KeywordListWrapper
    {
        public virtual int IdKeywordList { get; set; }

        public virtual string Name { get; set; }

        public virtual bool? Enabled { get; set; }

        public virtual IList<KeywordWrapper> Keywords { get; set; }

        public static implicit operator KeywordListWrapper(KeywordList keywordList)
        {
            if (keywordList == null) return null;
            var keywords = new List<KeywordWrapper>();
            if (keywordList.Keyword == null)
                keywords = null;
            else
                foreach (Keyword keyword in keywordList.Keyword)
                    keywords.Add(keyword);
            return new KeywordListWrapper
                       {
                           IdKeywordList = keywordList.Id,
                           Name = keywordList.Name,
                           Enabled = keywordList.Enabled,
                           Keywords = keywords
                       };
        }
    }
}