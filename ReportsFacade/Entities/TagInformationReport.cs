#region Using Directives

using System.Collections.Generic;

#endregion

namespace SEOToolSet.ReportsFacade.Entities
{
    public class TagInformationReport
    {
        public IList<Tag> Tags { get; set; }

        #region Nested type: Tag

        public class Tag
        {
            public string Name { get; set; }
            public string Count { get; set; }
            public string StopWords { get; set; }
            public string UsedWords { get; set; }
            public string Length { get; set; }
            public DataColumn Data { get; set; }

            #region Nested type: DataColumn

            public class DataColumn
            {
                public string TextData { get; set; }
                public string ResultData { get; set; }
            }

            #endregion
        }

        #endregion
    }
}