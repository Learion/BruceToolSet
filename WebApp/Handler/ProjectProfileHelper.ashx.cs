using System;
using System.Web;
using System.Web.SessionState;
using SEOToolSet.Providers;

namespace SEOToolSet.WebApp.Handler
{
    public class ProjectProfileHelper : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            var requestForm = context.Request.Form;
            context.Response.ContentType = "text/javascript";
            var action = requestForm["action"];
            var result = string.Empty;
            var idProject = requestForm["idProject"];
            switch (action)
            {
                case "GetRankingMonitorReportDefaultEngines":
                    result = ProjectUserProfileManager.GetRankingMonitorReportDefaultEngines(Convert.ToInt32(idProject)) ??
                             "[]";
                    break;
                case "SetRankingMonitorReportDefaultEngines":
                    var rankingMonitorReportDefaultEnginesText = requestForm["rankingMonitorReportDefaultEngines"];
                    ProjectUserProfileManager.SetRankingMonitorReportDefaultEngines(Convert.ToInt32(idProject), rankingMonitorReportDefaultEnginesText);
                    result = "{ result : 'saved' }";
                    break;
                case "SetRankingMonitorConfiguration":
                    var configuration = requestForm["rankingMonitorConfiguration"];
                    ProjectUserProfileManager.SetRankingMonitorConfiguration(int.Parse(idProject), configuration);
                    result = "{ result : 'saved' }";
                    break;
                case "GetRankingMonitorConfiguration":
                    result = ProjectUserProfileManager.GetRankingMonitorConfiguration(int.Parse(idProject)) ?? "{ result : null }";
                    break;
                default:
                    result = "{ result : null }";
                    break;
            }

            context.Response.Write(result);
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
