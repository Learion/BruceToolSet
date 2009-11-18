using System;
using NHibernateDataStore.Common;

namespace SEOToolSet.DAL
{
  public class DSStatus : NHibernateDataStore.Common.EntityDataStoreBase<SEOToolSet.Entities.Status, System.String>
  {
    
    public DSStatus(NHibernate.ISession session) : base(session)
    {
    }
   
    public static DSStatus Create(String connName)
    {
        return new DSStatus(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
    }

    
    public static class Columns
    {
              public static String Name = "Name";
      public static String Description = "Description";
      public static String RankingMonitorDeepRun = "RankingMonitorDeepRun";
      public static String RankingMonitorRun = "RankingMonitorRun";

    }
  }
}
