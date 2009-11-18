using System;

namespace WebDemo.code
{

    /// <summary>
    /// Summary description for Utilities
    /// </summary>
    public static class Utilities
    {
        public static string GetDateTimeForDisplay(DateTime? date)
        {
            if (date == null)
                return string.Empty;
            TimeSpan diff = DateTime.Now - date.Value;

            if (diff.TotalDays < 1)
                return string.Format("{0} hrs {1} min ago", diff.Hours, diff.Minutes);
            return date.Value.ToShortDateString() + " " + date.Value.ToShortTimeString();
        }

        public static string GetDisplayUser(string user)
        {
            return string.IsNullOrEmpty(user) ? "[anonymous]" : user;
        }

        public static string FormatDate(DateTime date)
        {
            return date.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
        }

        public static DateTime ParseDate(string dtString)
        {
            return DateTime.ParseExact(dtString, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
        }

        public static string FormatException(Exception ex)
        {
            string message = ex.Message;

            //Add the inner exception if present (showing only the first 50 characters of the first exception)
            if (ex.InnerException != null)
            {
                if (message.Length > 50)
                    message = message.Substring(0, 50);

                message += "...->" + ex.InnerException.Message;
            }

            return message;
        }
    }
}