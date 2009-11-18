#region

using System;
using System.Collections.Specialized;
using System.Security;
using System.Web;
using System.Web.Security;
using SEOToolSet.Common;
using SEOToolSet.Entities.Wrappers;
using SEOToolSet.Providers;

#endregion

namespace SEOToolSet.WebApp.Handler
{
    /// <summary>
    /// Helps to handle the operations related to account users
    /// </summary>
    public class AccountUserHandler : IHttpHandler
    {
        private const ObjectSerializerType SERIALIZERTYPE = ObjectSerializerType.Object;

        #region IHttpHandler Members

        ///<summary>
        ///
        ///                    Enables processing of HTTP Web requests by a custom <c>HttpHandler</c> that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
        ///                
        ///</summary>
        ///
        ///<param name="context">
        ///                    An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests. 
        ///                </param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/javascript";
            string result;
            var action = context.Request.Form["action"];
            if (!context.User.Identity.IsAuthenticated)
                throw new SecurityException("User Not Authenticated");

            if (action == null)
                throw new ApplicationException("The action was not provided");
            var requestForm = context.Request.Form;
            switch (action)
            {
                case "getUser":
                    result = getUserJson(requestForm);
                    break;
                case "createUser":
                    result = createUser(requestForm);
                    break;
                case "updateUser":
                    result = updateUser(requestForm);
                    break;
                case "deleteUser":
                    result = deleteUser(requestForm);
                    break;
                default:
                    result = @"{ ""Result"": false}";
                    break;
            }

            context.Response.Write(result);
        }

        ///<summary>
        ///
        ///                    Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
        ///                
        ///</summary>
        ///
        ///<returns>
        ///true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise, false.
        ///                
        ///</returns>
        ///
        public bool IsReusable
        {
            get { return false; }
        }

        #endregion

        private static string createUser(NameValueCollection requestForm)
        {
            try
            {
                var firstName = requestForm["firstName"];
                var lastName = requestForm["lastName"];
                var email = requestForm["email"];
                var address1 = requestForm["address1"];
                var address2 = requestForm["address2"];
                var cityTown = requestForm["cityTown"];
                var state = requestForm["state"];
                var zip = requestForm["zip"];
                var telephone = requestForm["telephone"];
                var login = requestForm["login"];
                var password = requestForm["password"];
                var account = AccountManager.GetAccount(int.Parse(requestForm["accountId"]));
                var country = SEOMembershipManager.GetCountryById(int.Parse(requestForm["countryId"]));
                var userRole = SEORolesManager.GetRoleById(int.Parse(requestForm["roleId"]));
                int id;
                MembershipCreateStatus status;
                SEOMembershipManager.CreateUser(out id, firstName, lastName, email, address1, address2, cityTown, state, zip,
                                                telephone, login, password, null, null, account, country, userRole,
                                                out status);
                return string.Format(@"{{ ""Result"": true, ""Status"": {0} , ""UserId"": {1} }}", status, id);
            }
            catch (Exception)
            {
                return @"{ ""Result"": false}";
            }
        }

        private static string updateUser(NameValueCollection requestForm)
        {
            var userId = requestForm["userId"];
            var id = int.Parse(userId);
            var firstName = requestForm["firstName"];
            var lastName = requestForm["lastName"];
            var email = requestForm["email"];
            var address1 = requestForm["address1"];
            var address2 = requestForm["address2"];
            var cityTown = requestForm["cityTown"];
            var state = requestForm["state"];
            var zip = requestForm["zip"];
            var telephone = requestForm["telephone"];
            var login = requestForm["login"];
            var password = requestForm["password"];
            var account = AccountManager.GetAccount(int.Parse(requestForm["accountId"]));
            var country = SEOMembershipManager.GetCountryById(int.Parse(requestForm["countryId"]));
            var userRole = SEORolesManager.GetRoleById(int.Parse(requestForm["roleId"]));

            try
            {
                SEOMembershipManager.UpdateUser(id, firstName, lastName, email, address1, address2, cityTown, state, zip,
                                                telephone, login, password, null, null, null, account, country, userRole);
            }
            catch (Exception)
            {
                return @"{ ""Result"": false}";
            }
            return @"{ ""Result"": true}";
        }

        private static string deleteUser(NameValueCollection requestForm)
        {
            var userId = requestForm["userId"];
            try
            {
                SEOMembershipManager.DeleteUser(int.Parse(userId));
            }
            catch (Exception)
            {
                return @"{ ""Result"": false}";
            }
            return @"{ ""Result"": true}";
        }

        private static string getUserJson(NameValueCollection requestForm)
        {
            var userId = requestForm["userId"];
            SEOToolSetUserWrapper user = SEOMembershipManager.GetUserById(int.Parse(userId));
            return SerializeHelper.GetJsonResult(user,
                                                 SERIALIZERTYPE);
        }
    }
}