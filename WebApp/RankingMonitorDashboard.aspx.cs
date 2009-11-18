using System;

namespace SEOToolSet.WebApp
{
    public partial class RankingMonitorDashboard : System.Web.UI.Page
    {
        protected const int quantityTopKeywords = 5;
        protected void Page_Load(object sender, EventArgs e)
        {
            TopKeywordsTitle.Text = string.Format(TopKeywordsTitle.Text, quantityTopKeywords);
        }
    }
}
