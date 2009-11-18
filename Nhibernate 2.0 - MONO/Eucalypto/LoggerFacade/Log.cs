using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Eucalypto.LoggerFacade
{
  /// <summary>
  /// Log  helper class using log4net.
  /// Configure log4net to enable logging before using these methods.
  /// </summary>
  public static class Log
  {

    private static bool _isConfigured = false;
    public static bool IsConfigured
    {
      get
      {
        return _isConfigured;
      }
      set
      {
        _isConfigured = value;
      }
    }
    private static Object Synchro = new object();

    public static log4net.ILog GetLogger(Type Context)
    {
      if (!IsConfigured)
      {
        lock (Synchro)
        {
          log4net.Config.XmlConfigurator.Configure();
          IsConfigured = true;
        }
      }
      return log4net.LogManager.GetLogger(Context);
    }

    public static void Error(Type context, string message)
    {
      log4net.ILog log = GetLogger(context);
      if (log.IsErrorEnabled)
        log.Error(message);
    }

    public static void Error(Type context, string message, Exception exception)
    {
      log4net.ILog log = GetLogger(context);
      if (log.IsErrorEnabled)
        log.Error(message, exception);
    }

    public static void Warning(Type context, string message, Exception exception)
    {
      log4net.ILog log = GetLogger(context);
      if (log.IsWarnEnabled)
        log.Warn(message, exception);
    }

    public static void Debug(Type context, string message)
    {
      log4net.ILog log = GetLogger(context);
      if (log.IsDebugEnabled)
        log.Debug(message);
    }

    public static void LogException(Type context, Exception ex)
    {      
      if (ex.InnerException != null) LogException(context, ex.InnerException);
      GetLogger(context).Error("Exception :: " + ListProperties(ex));
    }

    private static string ListProperties(object objectToInspect)
    {
      string returnString = "";

      //To use reflection on an object, you 
      // first need to get an instance
      // of that object's type.
      Type objectType = objectToInspect.GetType();

      //After you have the object's type, you can get 
      // information on that type. In this case, we're 
      // asking the type to tell us all the
      // properties that it contains.
      PropertyInfo[] properties = objectType.GetProperties();

      //You can then use the PropertyInfo array 
      // to loop through each property of the type.
      foreach (PropertyInfo property in properties)
      {
        //The interest part of this code 
        // is the GetValue method. This method
        // returns the value of the property.
        returnString += property.Name + ": " +
               property.GetValue(objectToInspect, null) + Environment.NewLine;
      }

      return returnString;
    }

    
  }
}
