using System.Threading;
using System.Web;
using System.Web.Services;

namespace SEOToolSet.WebApp.Handler
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class NotAsync : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            Thread.Sleep(10000);

            context.Response.Write("Hello Finished after ");
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
