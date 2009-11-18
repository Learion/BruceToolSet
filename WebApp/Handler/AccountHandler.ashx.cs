using System;
using System.Collections.Generic;
using System.Security;
using System.Web;
using SEOToolSet.Common;
using SEOToolSet.Entities;
using SEOToolSet.Entities.Wrappers;
using SEOToolSet.Providers;

namespace SEOToolSet.WebApp.Handler
{
    /// <summary>
    /// Helps to handle the operations related to accounts
    /// </summary>
    public class AccountHandler : IHttpHandler
    {
        private const ObjectSerializerType SERIALIZERTYPE = ObjectSerializerType.Object;

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
            var accountId = int.Parse(requestForm["accountId"]);
            var account = AccountManager.GetAccount(accountId);
            switch (action)
            {
                case "getUsersByAccount":
                    var users = new List<SEOToolSetUserWrapper>();
                    foreach (var user in SEOMembershipManager.GetUsersFromAccount(account, false))
                    {
                        SEOToolSetUserWrapper userWrapper = user;
                        var projectsWithinAccount = ProjectManager.GetProjectsForUserWithinAccount(user.Login,
                                                                                                   account, null);
                        userWrapper.ProjectsInvolving = projectsWithinAccount.Count;
                        users.Add(userWrapper);
                    }
                    result = SerializeHelper.GetJsonResult(users,
                                                           SERIALIZERTYPE);
                    break;
                case "getSubscriptionDetails":
                    var details = new List<SubscriptionDetailWrapper>();
                    foreach (var detail in SubscriptionManager.GetSubscriptionDetails(accountId))
                        details.Add(detail);
                    result = SerializeHelper.GetJsonResult(details, SERIALIZERTYPE);
                    break;
                default:
                    result = @"{ ""Result"": true}";
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
            get
            {
                return false;
            }
        }
    }
}
