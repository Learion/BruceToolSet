#region

using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI.HtmlControls;

#endregion

namespace SEOToolSet.Common
{
    public static class WebHelper
    {
        /// <summary>
        /// Get the web site application root path. Combine the request.Url.GetLeftPart(UriPartial.Authority) with request.ApplicationPath
        /// </summary>
        /// <returns>Returns the path of Web Application as string</returns>
        public static string WebAppRootPath
        {
            get
            {
                var request = HttpContext.Current.Request;
                var tempUrl = CombinePath(request.Url.GetLeftPart(UriPartial.Authority), request.ApplicationPath);
                if (!tempUrl.EndsWith("/", StringComparison.Ordinal))
                {
                    tempUrl += "/";
                }
                return tempUrl;
            }
        }

        public static void RedirectToLoginPage()
        {
            RedirectToLoginPage(null);
        }

        public static void RedirectToLoginPage(String extraQueryString)
        {
            if (IsMono)
            {
                FormsAuthentication.RedirectToLoginPage(String.Format("{0}{1}", String.Format("ReturnUrl={0}",
                    HttpContext.Current.Request.RawUrl), !String.IsNullOrEmpty(extraQueryString) ? string.Format("&{0}", extraQueryString) : String.Empty));
                //HttpContext.Current.Response.End();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                FormsAuthentication.RedirectToLoginPage(String.Format("{0}", extraQueryString));
                //HttpContext.Current.Response.End();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }

        public static bool IsMono = EvaluateIsMono();

        private static bool EvaluateIsMono()
        {
            var t = Type.GetType("Mono.Runtime");
            return t != null;
        }

        private static string CombinePath(string basePath, string relativePath)
        {
            if (relativePath.Length == 0 || relativePath[0] != '/')
                relativePath = '/' + relativePath;

            if (basePath.Length > 0 && basePath[basePath.Length - 1] == '/')
                basePath = basePath.Substring(0, basePath.Length - 1);

            return basePath + relativePath;
        }

        /// <summary>
        /// Get the absolute path for the relative path given.
        /// </summary>
        /// <param name="relativePath">The relative path</param>
        /// <returns>Returns the absolute path as string</returns>
        public static string GetAbsolutePath(String relativePath)
        {
            return relativePath.StartsWith("http://", StringComparison.Ordinal)
                       ? relativePath
                       : CombinePath(WebAppRootPath, relativePath);
        }

        /// <summary>
        /// Parses a DateTime String that only contains the month and the year in the format MM/yyyy. (InvariantCulture)
        /// </summary>
        /// <param name="dateString">the month and the year in the format MM/yyyy</param>
        /// <returns></returns>
        public static DateTime? ParseDateWithMonthAndYear(String dateString)
        {
            //TODO: Check if this way to parse the date is well! or we better use the Invariant Culture or a mix of both of them
            //DateTime.Parse(dateString,CultureInfo.InvariantCulture,DateTimeStyles.)        

            if (Regex.IsMatch(dateString, @"(?:\d{1,2}/)?\d{1,2}/\d{2,4}"))
            {
                MatchCollection matches;
                if (Regex.IsMatch(dateString, @"\d{1,2}/\d{1,2}/\d{2,4}"))
                {
                    matches = Regex.Matches(dateString, @"(?:(?<month>\d{1,2})/)?\d{1,2}/(?<year>\d{2,4})");
                }
                else if (Regex.IsMatch(dateString, @"\d{1,2}/\d{2,4}"))
                {
                    matches = Regex.Matches(dateString, @"(?<month>\d{1,2})/(?<year>\d{2,4})");
                }
                else
                {
                    throw new FormatException("The date is not well formed");
                }
                int year = int.Parse(matches[0].Groups["year"].Value);
                int month = int.Parse(matches[0].Groups["month"].Value);

                return new DateTime(year, month, 1);
            }
            return null;
        }

        public static void RedirectTo(string url)
        {
            HttpContext.Current.Response.Redirect(url, false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public static Uri ResolveUrlToUri(string uriString)
        {
            return new Uri(GetAbsolutePath(uriString));

        }
    }
}