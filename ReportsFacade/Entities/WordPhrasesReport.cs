#region Using Directives

using System.Collections.Generic;

#endregion

namespace SEOToolSet.ReportsFacade.Entities
{
    public class WordPhrasesReport
    {
        public IList<Phrase> Phrases { get; set; }

        #region Nested type: Keyword

        public class Keyword
        {
            public string Name { get; set; }
            public string Style { get; set; }
            public KeywordItem MetaTitle { get; set; }
            public KeywordItem MetaDesc { get; set; }
            public KeywordItem MetaKeywords { get; set; }
            public KeywordItem Heads { get; set; }
            public KeywordItem AltTags { get; set; }
            public KeywordItem FirstWords { get; set; }
            public KeywordItem BodyWords { get; set; }
            public KeywordItem AllWords { get; set; }
        }

        #endregion

        #region Nested type: KeywordItem

        public class KeywordItem
        {
            public double Percentage { get; set; }
            public int Counter { get; set; }
            public string Style { get; set; }
        }

        #endregion

        #region Nested type: Phrase

        public class Phrase
        {
            public Keyword Count { get; set; }
            public IList<Keyword> Keywords { get; set; }
        }

        #endregion
    }
}