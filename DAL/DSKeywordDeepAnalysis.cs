using System;
using NHibernateDataStore.Common;

namespace SEOToolSet.DAL
{
  public class DSKeywordDeepAnalysis : NHibernateDataStore.Common.EntityDataStoreBase<SEOToolSet.Entities.KeywordDeepAnalysis, System.Int32>
  {
    
    public DSKeywordDeepAnalysis(NHibernate.ISession session) : base(session)
    {
    }
   
    public static DSKeywordDeepAnalysis Create(String connName)
    {
        return new DSKeywordDeepAnalysis(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
    }

    
    public static class Columns
    {
              public static String Id = "Id";
      public static String Keyword = "Keyword";
      public static String Pages = "Pages";
      public static String RankingMonitorDeepRun = "RankingMonitorDeepRun";

    }
  }
}
