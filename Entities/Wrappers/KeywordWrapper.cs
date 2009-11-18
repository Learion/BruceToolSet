namespace SEOToolSet.Entities.Wrappers
{
    public class KeywordWrapper
    {
        public virtual int IdKeyword { get; set; }

        public virtual string Name { get; set; }

        public static implicit operator KeywordWrapper(Keyword keyword)
        {
            if (keyword == null) return null;
            return new KeywordWrapper
                       {
                           IdKeyword = keyword.Id,
                           Name = keyword.Keyword
                       };
        }
    }
}