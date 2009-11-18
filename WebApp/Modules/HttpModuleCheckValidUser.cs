using System;
using System.Web;
using System.Web.Security;
using SEOToolSet.Common;
using SEOToolSet.Providers;
using SEOToolSet.WebApp.Helper;

namespace SEOToolSet.WebApp.Modules
{
    /// <summary>
    /// An Http Module (System.Web.IHttpModule) that can be used to check if a user that 
    /// was previous authenticated is still valid by checking the IsApproved and IsLockedOut status.
    /// This is useful because the code is executed also when using the "Remember Me" feature that 
    /// without this code allow an unapproved and locked user to continue to use the site.
    /// This module is valid only when used with the Membership provider and Form Authentication
    /// </summary>
    public class HttpModuleCheckValidUser : IHttpModule
    {
        #region IHttpModule Members

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            LoggerFacade.Log.Debug(GetType(), "IHttpModule has been Inited");
            context.PreRequestHandlerExecute += context_PreRequestHandlerExecute;
        }

        private void CheckSecurity()
        {
            try
            {
                var node = SiteMapHelper.GetCurrentNode();
                
                if (node == null ||
                     node.Url.Contains(SiteMapHelper.LoginPageUrl) //Do not protect the LoginPage;
                    || PermissionHelper.UserHasAccessToPage(node)) return;


                //To Prevent an infinite loop all the users should had access to the HomePage
                //This could happen if the user is logged in but lacks the required permissions to access a given page.
                //So by a client Requirement the user should be redirected to the ProjectDashboard (Home) Page.
                //But as this page is protected, if the user doesn't have this permission he would be redirected always. 
                //This will generate an infinite loop
                if (HttpContext.Current.User == null)
                {
                    WebHelper.RedirectToLoginPage();
                }
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                    WebHelper.RedirectTo(SiteMapHelper.HomePageUrl);
                else
                {
                    WebHelper.RedirectToLoginPage();
                }
            }
            catch (Exception ex)
            {
                //TODO: Should we redirect the user to an Error Page???? or let him go to the site if an error has ocurred at this level?
                LoggerFacade.Log.LogException(GetType(), ex);
                return;
            }
        }



        private static void CheckIfUserIsNotLocked()
        {
            //Thanks to Scott Mitchell and Kiliman for this code
            // http://scottonwriting.net/sowblog/posts/11167.aspx

            if (HttpContext.Current.User == null || !HttpContext.Current.User.Identity.IsAuthenticated) return;

            //Check first in the session to prevent a database call on each request
            if (GetSessionValidUser()) return;
            //Check if the user is approved and not locked
            var u = Membership.GetUser(true);
            if (u != null && u.IsApproved && !u.IsLockedOut)
            {
                SetSessionValidUser();
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        void context_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            CheckIfUserIsNotLocked();
            CheckSecurity();
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

            var val = HttpContext.Current.Session["HttpModuleCheckValidUser_Valid"];
            return val != null;
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
