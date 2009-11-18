using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Configuration;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Eucalypto.Notification
{
  /// <summary>
  /// Implementation of NotificationProvider using email smtp notifications.
  /// Configuration:
  /// template = the file that contains the template to use. Typically a path like: App_Data\MailTemplate.xml . If the path is relative is considered to be inside the ApplicationBase directory.
  /// enabled = true to enabled the provider (default true)
  /// 
  /// Check if the user can receive notifications using the 'ReceiveNotification' properties of the user profiles.
  /// </summary>
  public class EucalyptoSmtpNotificationProvider : NotificationProvider
  {
    //private string PROFILE_RECEIVENOTIFICATION = "ReceiveNotification";

    public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
    {
      if (config == null)
        throw new ArgumentNullException("config");

      if (name == null || name.Length == 0)
        name = "EucalyptoNotificationProvider";

      base.Initialize(name, config);

      this.mProviderName = name;



      //Read the configurations
      string templatePath = ExtractConfigValue(config, "template", null);
      if (templatePath == null ||
          templatePath.Length == 0)
      {
        throw new EucalyptoException("template cannot be empty.");
      }

      templatePath = PathHelper.LocateServerPath(templatePath);
      mTemplate = new System.Xml.XPath.XPathDocument(templatePath);


      Enabled = bool.Parse(ExtractConfigValue(config, "enabled", "true"));

      // Throw an exception if unrecognized attributes remain
      if (config.Count > 0)
      {
        string attr = config.GetKey(0);
        if (!String.IsNullOrEmpty(attr))
          throw new System.Configuration.Provider.ProviderException("Unrecognized attribute: " +
          attr);
      }
    }

    /// <summary>
    /// A helper function to retrieve config values from the configuration file and remove the entry.
    /// </summary>
    /// <returns></returns>
    private string ExtractConfigValue(System.Collections.Specialized.NameValueCollection config, string key, string defaultValue)
    {
      string val = config[key];
      if (val == null)
        return defaultValue;
      else
      {
        config.Remove(key);

        return val;
      }
    }

    #region Properties
    private string mProviderName;
    public string ProviderName
    {
      get { return mProviderName; }
      set { mProviderName = value; }
    }

    private bool mEnabled;
    public bool Enabled
    {
      get { return mEnabled; }
      set { mEnabled = value; }
    }



    private System.Xml.XPath.XPathDocument mTemplate;
    /// <summary>
    /// Gets or sets the xml template to use.
    /// </summary>
    public System.Xml.XPath.XPathDocument Template
    {
      get { return mTemplate; }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Send an email notification to an user.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="parameters">A set of parameters</param>
    public override void NotifyUser(System.Web.Security.MembershipUser user, Dictionary<string, string> parameters)
    {
      if (Enabled == false)
        return;

      if (user == null)
        throw new ArgumentNullException("user");
      if (string.IsNullOrEmpty(user.Email))
        throw new Membership.EMailRequiredException(user.UserName);

      // Specify the message content.
      using (MailMessage message = CreateMailMessageFromTemplate(Template, user, parameters))
      {
        //For now I don't use async method because I don't known exactly how to handle the message
        // dispose and for MSDN seems that you cannot execute 2 or more SendAsync without waiting that the first mail is completed
        //// Set the method that is called back when the send operation ends.
        //client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
        //// The userState can be any object that allows your callback 
        //// method to identify this send operation.
        //// For this example, the userToken is a string constant.
        //string userState = "Notification to " + user.Email + " subject: " + subject;
        //SmtpClient client = new SmtpClient();
        //client.SendAsync(message, userState);

        try
        {
          SmtpClient client = new SmtpClient();
          client.Send(message);
        }
        catch (Exception ex)
        {
          throw new SmtpNotificationException(user.UserName, ex);
        }
      }
    }

    //To Decouple this Notification provider from the Membership Provider

    //public override bool UserCanReceiveNotification(System.Web.Security.MembershipUser user)
    //{
    //  if (Enabled == false)
    //    return false;

    //  if (user == null)
    //    throw new ArgumentNullException("user");
    //  if (string.IsNullOrEmpty(user.Email))
    //    return false;
    //  else
    //  {
    //    System.Web.Profile.ProfileBase profile = System.Web.Profile.ProfileBase.Create(user.UserName);

    //    object profReceiveNotification = profile[PROFILE_RECEIVENOTIFICATION];
    //    if (profReceiveNotification is bool)
    //      return (bool)profReceiveNotification;
    //    else
    //      return false;
    //  }
    //}
    #endregion

    private MailMessage CreateMailMessageFromTemplate(System.Xml.XPath.XPathDocument template,
                                                System.Web.Security.MembershipUser user,
                                                Dictionary<string, string> parameters)
    {
      System.Xml.XPath.XPathNavigator navigator = template.CreateNavigator();

      // Specify the e-mail sender.
      MailAddress from = new MailAddress((string)navigator.Evaluate("string(template/sender)")); ;
      // Set destinations for the e-mail message.
      MailAddress to = new MailAddress(user.Email);

      MailMessage message = new MailMessage(from, to);

      message.IsBodyHtml = bool.Parse((string)navigator.Evaluate("string(template/body/@html)"));
      message.BodyEncoding = System.Text.Encoding.UTF8;
      message.SubjectEncoding = System.Text.Encoding.UTF8;

      string body = (string)navigator.Evaluate("string(template/body)");
      message.Body = ReplaceParameters(body, parameters);

      string subject = (string)navigator.Evaluate("string(template/subject)");
      message.Subject = ReplaceParameters(subject, parameters);

      return message;
    }

    private string ReplaceParameters(string source, Dictionary<string, string> parameters)
    {
      string destination = source;
      foreach (string key in parameters.Keys)
      {
        destination = destination.Replace(key, parameters[key]);
      }

      return destination;
    }

    //#region Smtp Async methods
    //private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    //{
    //    // Get the unique identifier for this asynchronous operation.
    //    String token = (string)e.UserState;

    //    if (e.Cancelled)
    //    {
    //        Console.WriteLine("[{0}] Send canceled.", token);
    //    }
    //    if (e.Error != null)
    //    {
    //        Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
    //    }
    //    else
    //    {
    //        Console.WriteLine("Message sent.");
    //    }
    //    mailSent = true;
    //}
    //#endregion
    public bool IsValidEmailAddress(string s)
    {
      Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
      return regex.IsMatch(s);
    }

    public bool AreValidEmails(ref string addresses)
    {
      if (addresses.IndexOf(",") < 0)
      {
        return IsValidEmailAddress(addresses);
      }
      else
      {
        String[] adrs = addresses.Split(',', ';');

        String cadena = "";
        int contador = 0;
        foreach (String s in adrs)
        {
          if (IsValidEmailAddress(s))
          {
            if (contador > 0)
            {
              cadena += ",";
            }
            cadena += s;
            contador++;
          }
        }
        addresses = (!String.IsNullOrEmpty(cadena)) ? cadena : null;
        return true;
      }

    }
    private MailMessage CreateMailMessageFromTemplate(System.Xml.XPath.XPathDocument template,
                                                    String email,
                                                    Dictionary<string, string> parameters)
    {

      System.Xml.XPath.XPathNavigator navigator = template.CreateNavigator();

      // Specify the e-mail sender.
      MailAddress from = new MailAddress((string)navigator.Evaluate("string(template/sender)")); ;
      // Set destinations for the e-mail message.
      //MailAddress to = new MailAddress(email);

      MailMessage message = new MailMessage(from.Address, email);





      message.IsBodyHtml = bool.Parse((string)navigator.Evaluate("string(template/body/@html)"));
      message.BodyEncoding = System.Text.Encoding.UTF8;
      message.SubjectEncoding = System.Text.Encoding.UTF8;

      string body = (string)navigator.Evaluate("string(template/body)");
      message.Body = ReplaceParameters(body, parameters);

      string subject = (string)navigator.Evaluate("string(template/subject)");
      message.Subject = ReplaceParameters(subject, parameters);

      return message;

    }


    public override void NotifyTo(string email, string MailTemplatePath, Dictionary<string, string> parameters)
    {
      if (Enabled == false)
        return;

      //throw new Exception("The method or operation is not implemented.");
      String path = PathHelper.LocateServerPath(MailTemplatePath);
      mTemplate = new System.Xml.XPath.XPathDocument(path);


      if (string.IsNullOrEmpty(email))
        throw new Exception("The Email could not be blank");

      if (!AreValidEmails(ref email))
        throw new Exception("The Email Address is not valid");

      // Specify the message content.
      using (MailMessage message = CreateMailMessageFromTemplate(Template, email, parameters))
      {
        //For now I don't use async method because I don't known exactly how to handle the message
        // dispose and for MSDN seems that you cannot execute 2 or more SendAsync without waiting that the first mail is completed
        //// Set the method that is called back when the send operation ends.
        //client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
        //// The userState can be any object that allows your callback 
        //// method to identify this send operation.
        //// For this example, the userToken is a string constant.
        //string userState = "Notification to " + user.Email + " subject: " + subject;
        //SmtpClient client = new SmtpClient();
        //client.SendAsync(message, userState);

        try
        {
          SmtpClient client = new SmtpClient();
          client.Send(message);
        }
        catch (Exception ex)
        {
          throw new SmtpNotificationException(email, ex);
        }
      }


    }
  }
}
