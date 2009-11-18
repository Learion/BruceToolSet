using System.Web;

namespace SEOToolSet.WebApp.Handler
{
    /// <summary>
    /// Makes validations via Ajax
    /// </summary>
    public class AjaxValidations : IHttpHandler
    {

        public string action
        {
            get
            {
                return HttpContext.Current.Request.Form["action"];
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            switch ( action )
            {
                case "CheckAccountName":
                    var accountName = context.Request.Form["accountName"];
                    var isAccountAvailable = Providers.AccountManager.IsAccountNameAvailable(accountName);
                    context.Response.Write("{ result : " + isAccountAvailable.ToString().ToLower() + " }");
                    break;
                case "CheckLoginName":
                    var loginName = context.Request.Form["loginName"];
                    var isLoginAvailable = Providers.SEOMembershipManager.GetUser(loginName) == null;
                    context.Response.Write("{ result : " + isLoginAvailable.ToString().ToLower() + " }");
                    break;
                case "CheckEmail":
                    var email = context.Request.Form["email"];
                    var isEmailAvailable = System.Web.Security.Membership.GetUserNameByEmail(email) == null;
                    context.Response.Write("{ result : " + isEmailAvailable.ToString().ToLower() + " }");
                    break;
                default:
                    context.Response.Write("{ result : false }");
                    break;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
