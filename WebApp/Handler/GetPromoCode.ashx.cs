using System;
using System.Web;
using SEOToolSet.Common;
using SEOToolSet.Providers;

namespace SEOToolSet.WebApp.Handler
{
    ///<summary>
    ///Handle the data related to <c>PromoCode</c>
    ///</summary>
    public class GetPromoCode : IHttpHandler
    {
        void IHttpHandler.ProcessRequest(HttpContext context)
        {
            var response = context.Response;
            var result = string.Empty;
            response.ContentType = "text/javascript";
            var requestForm = context.Request.Form;
            var serializerType = ObjectSerializerType.Object;
            var action = requestForm["action"];
            if (string.IsNullOrEmpty(action))
                throw new ArgumentNullException("action", "The action was not provided");
            var code = requestForm["code"];
            switch (action)
            {
                case "Validate":
                    var subscriptionLevelId = requestForm["subscriptionLevelId"];
                    int? accountId = null;
                    if (!string.IsNullOrEmpty(requestForm["accountId"]))
                        accountId = int.Parse(requestForm["accountId"]);
                    var promoCodeStatus = PromoCodeManager.Validate(code, int.Parse(subscriptionLevelId), accountId);
                    result = SerializeHelper.GetJsonResult(promoCodeStatus, serializerType);
                    break;
                case "Consume":
                    PromoCodeManager.Consume(code);
                    result = @"{ ""Result"": true}";
                    break;
                default:
                    break;
            }
            response.Write(result);
        }

        ///<summary>
        ///Gets a value indicating whether another request can use the 
        ///<see cref="T:System.Web.IHttpHandler" /> instance.
        ///</summary>
        ///<returns>
        ///true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable;
        ///otherwise, false.
        ///</returns>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
