using System;
using System.Collections.Generic;
using System.Configuration.Provider;

namespace Eucalypto.Notification
{
  /// <summary>
  /// NotificationProvider abstract class. 
  /// Defines the contract that Eucalypto implements to provide notification services using custom notification providers.
  /// You can use this provider as a generic way to define notifications.
  /// You can use the EucalyptoSmtpNotificationProvider to use EMail notifications.
  /// </summary>
  public abstract class NotificationProvider : ProviderBase
  {
    /// <summary>
    /// Send a notification to an user.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="parameters">A set of parameters</param>
    public abstract void NotifyUser(System.Web.Security.MembershipUser user, Dictionary<string, string> parameters);

    /// <summary>
    /// Returns true if the user can receive notification, otherwise false.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    //public abstract bool UserCanReceiveNotification(System.Web.Security.MembershipUser user);

    /// <summary>
    /// Send a Notification to a Arbirtray Email Account using an especific MailTemplatePath or default MailTemplate if this parameter is null
    /// </summary>
    /// <param name="email">email address</param>
    /// <param name="MailTemplateURL">path to the MailTemplate</param>
    /// <param name="parameters">A set of parameters</param>
    public abstract void NotifyTo(String email, String MailTemplatePath, Dictionary<string, string> parameters);
  }
}
