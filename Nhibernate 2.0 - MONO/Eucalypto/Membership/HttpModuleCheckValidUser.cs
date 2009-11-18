using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Eucalypto.Membership
{
    /// <summary>
    /// An Http Module (System.Web.IHttpModule) that can be used to check if a user that 
    /// was previous authenticated is still valid by checking the IsApproved and IsLockedOut status.
    /// This is useful because the code is executed also when using the "Remember Me" feature that 
    /// without this code allow an unapproved and locked user to continue to use the site.
    /// This module is valid only when used with the Membership provider and Form Authentication
    /// </summary>
    public class HttpModuleCheckValidUser : System.Web.IHttpModule
    {
        #region IHttpModule Members

        public void Dispose()
        {
        }

        public void Init(System.Web.HttpApplication context)
        {
            context.PreRequestHandlerExecute += new EventHandler(context_PreRequestHandlerExecute);
        }

        void context_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            //Thanks to Scott Mitchell and Kiliman for this code
            // http://scottonwriting.net/sowblog/posts/11167.aspx

            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //Check first in the session to prevent a database call on each request
                if (GetSessionValidUser() == false)
                {
                    //Check if the user is approved and not locked
                    MembershipUser u = System.Web.Security.Membership.GetUser(true);
                    if (!u.IsApproved || u.IsLockedOut)
                    {
                        FormsAuthentication.SignOut();
                        FormsAuthentication.RedirectToLoginPage();
                    }
                    else
                        SetSessionValidUser();
                }
            }
        }
        #endregion

        private static bool GetSessionValidUser()
        {
            //I will check first if the session is valid. 
            // On the internet seems that there are some problems sometime with the session used inside an HttpModule ...
            // Seems that the problem occurs only on a dev machine when using the ASP.NET development server, 
            // in this case the request to files other than .ASPX seems to have the session null
            if (HttpContext.Current.Session == null)
                return false;

            object val = HttpContext.Current.Session["HttpModuleCheckValidUser_Valid"];
            if (val == null)
                return false;
            else
                return true;
        }

        private static void SetSessionValidUser()
        {
            //I will check first if the session is valid. 
            // On the internet seems that there are some problems sometime with the session used inside an HttpModule ...
            // Seems that the problem occurs only on a dev machine when using the ASP.NET development server, 
            // in this case the request to files other than .ASPX seems to have the session null
            if (HttpContext.Current.Session != null)
                HttpContext.Current.Session["HttpModuleCheckValidUser_Valid"] = true;
        }
    }
}
