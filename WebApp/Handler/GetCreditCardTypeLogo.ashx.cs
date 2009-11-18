using System;
using System.Collections.Generic;
using System.Web;
using SEOToolSet.Providers;
using SEOToolSet.Entities;

namespace SEOToolSet.WebApp.Handler
{
    /// <summary>
    /// Retrieve the logo for the chosen credit card
    /// </summary>
    public class GetCreditCardTypeLogo : IHttpHandler
    {
        
        public void ProcessRequest(HttpContext context)
        {
            var creditCardTypeParam = context.Request["creditCardType"];
            var application = context.Application;
            var paymetTypes= getPaymentTypes(application);
            var url = string.Empty;
            foreach (var paymentType in paymetTypes)
            {
                if (paymentType.Id == creditCardTypeParam)
                    url = paymentType.Url;
            }
            context.Response.Write(@"{ ""result"": """ + url + @""" }");
        }

        private static CreditCardType[] getPaymentTypes(HttpApplicationState application)
        {
            var paymentTypes = application["CreditCardTypes"] as CreditCardType[];
            if (paymentTypes == null)
            {
                var pt = AccountManager.GetCreditCardTypes();
                if (pt == null) throw new ApplicationException("The credit card types are not loaded");
                paymentTypes = new CreditCardType[pt.Count];
                pt.CopyTo(paymentTypes, 0);
                application["CreditCardTypes"] = paymentTypes;
            }
            return paymentTypes;
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
