namespace SEOToolSet.Entities.Wrappers
{
    public class MonitorKeywordListWrapper
    {
        public virtual int Id { get; set; }

        public virtual ProjectWrapper Project { get; set; }

        public virtual KeywordListWrapper KeywordList { get; set; }

        public static implicit operator MonitorKeywordListWrapper(MonitorKeywordList monitorKeywordList)
        {
            if (monitorKeywordList == null) return null;
            return new MonitorKeywordListWrapper
                       {
                           Id = monitorKeywordList.Id,
                           KeywordList = monitorKeywordList.KeywordList,
                           Project = monitorKeywordList.Project
                       };
        }
    }
}