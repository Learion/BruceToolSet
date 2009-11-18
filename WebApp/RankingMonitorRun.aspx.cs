using System;

namespace SEOToolSet.WebApp
{
    public partial class RankingMonitorRun : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [System.Web.Services.WebMethod] 
        protected string CallProfile()
        {
            return Context.Profile["keywordListRankingMonitorRunPreferences"].ToString();
        }
    }
}
