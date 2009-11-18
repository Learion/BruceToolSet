using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Web.Configuration;
using System.Configuration;

namespace Eucalypto.Notification
{

  public class NotificationManager
  {
    public static String DefaultProviderName;
    static NotificationManager()
    {
      //Get the feature's configuration info
      NotificationProviderConfiguration qc =
          (NotificationProviderConfiguration)ConfigurationManager.GetSection("notificationManager");

      if (qc == null || qc.Providers == null)
        throw new ProviderException("Providers for notificationManager not valid, null returned.");

      DefaultProviderName = qc.DefaultProvider;

      //Instantiate the providers
      providerCollection = new NotificationProviderCollection();
      ProvidersHelper.InstantiateProviders(qc.Providers, providerCollection, typeof(NotificationProvider));
      providerCollection.SetReadOnly();
    }

    private static NotificationProviderCollection providerCollection;
    public static NotificationProviderCollection Providers
    {
      get { return providerCollection; }
    }

    public static NotificationProvider GetDefaultNotificationProvider()
    {
      return Providers[DefaultProviderName];
    }
  }
}
