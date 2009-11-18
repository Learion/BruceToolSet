using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;

namespace Eucalypto.Forum
{
  public class ForumProviderConfiguration : ConfigurationSection
  {
    [ConfigurationProperty("providers")]
    public ProviderSettingsCollection Providers
    {
      get
      {
        return (ProviderSettingsCollection)base["providers"];
      }
    }

    [ConfigurationProperty("defaultProvider")]
    public string DefaultProvider
    {
      get
      {
        return (string)base["defaultProvider"];
      }
      set
      {
        base["defaultProvider"] = value;
      }
    }
    [ConfigurationProperty("receiveNotificationPropertyInProfile")]
    public string ReceiveNotificationPropertyInProfile
    {
      get
      {
        return (string)base["receiveNotificationPropertyInProfile"];
      }
      set
      {
        base["receiveNotificationPropertyInProfile"] = value;
      }
    }
  }
}
