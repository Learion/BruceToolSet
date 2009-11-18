using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using SEOToolSet.Common;

namespace SEOToolSet.WebApp.Helper
{
    public class Mailer
    {
        public static String SendFrom = MailerFacade.GetSenderFromWebConfig();

        /// <summary>
        /// Send the email when a brand new user is created
        /// </summary>
        /// <param name="to"></param>
        /// <param name="accountName"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public static void SendCreateUserEmail(string to, string accountName, string userName, string password)
        {

            var loginPage = WebHelper.GetAbsolutePath("LoginPage.aspx");
            var dictionary = new Dictionary<string, string> { { "USERNAME", userName }, { "PASSWORD", password }, { "ACCOUNTNAME", accountName }, { "LOGINPAGEURL" , loginPage } };

            MailerFacade.SendEmailUsingMultiPart(to, SendFrom,
                HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["CreateUserHtmlTemplateEmail"]),
                HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["CreateUserPlainTemplateEmail"]),
                dictionary);
        }
    }
}
