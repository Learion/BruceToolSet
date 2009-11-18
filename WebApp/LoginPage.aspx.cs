using System;
using SEOToolSet.Common;
using SEOToolSet.WebApp.Helper;

namespace SEOToolSet.WebApp
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((IsPostBack) || (!User.Identity.IsAuthenticated || string.IsNullOrEmpty(returnPath))) return;
            //Response.Cache.SetNoStore();
            var failureText = Login1.FindControl("FailureText") as System.Web.UI.WebControls.Literal;
            if (failureText != null)
                failureText.Text = Resources.CommonTerms.PageNotAllowed;
        }

        protected void Login1_OnLoggedIn(object sender, EventArgs e)
        {
            goToPage();
        }

        private void goToPage()
        {
            if (String.IsNullOrEmpty(returnPath))
            {
                WebHelper.RedirectTo(SiteMapHelper.HomePageUrl);
            }
            else
            {
                var urlUnescaped = Uri.UnescapeDataString(returnPath);
                //Delete the Login page if remains.
                if (urlUnescaped.Contains(SiteMapHelper.LoginPageUrl))
                {
                    var lastSlashPosition = urlUnescaped.LastIndexOf(returnUrlParameter + "=");
                    if (lastSlashPosition > 0)
                        WebHelper.RedirectTo(urlUnescaped.Substring(lastSlashPosition + returnUrlParameter.Length + 2));
                    else
                        WebHelper.RedirectTo(SiteMapHelper.HomePageUrl);
                }
                else
                {
                    WebHelper.RedirectTo(urlUnescaped);
                }
            }
        }

        private String returnPath
        {
            get { return Request.QueryString[returnUrlParameter]; }
        }

        private static String returnUrlParameter
        {
            get { return "ReturnUrl"; }
        }
    }
}
