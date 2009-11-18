
using System;
using NHibernateDataStore.Common;

namespace SEOToolSet.DAL
{
  public class DSFrequency : NHibernateDataStore.Common.EntityDataStoreBase<SEOToolSet.Entities.Frequency, System.Int32>
  {
    
    public DSFrequency(NHibernate.ISession session) : base(session)
    {
    }
   
    public static DSFrequency Create(String connName)
    {
        return new DSFrequency(NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(connName));
    }

    
    public static class Columns
    {
              public static String Id = "Id";
      public static String Name = "Name";
      public static String Project = "Project";

    }
  }
}
