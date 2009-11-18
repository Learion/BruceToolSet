
using System;
using System.Web.UI;
using SEOToolSet.Providers;
using SEOToolSet.WebApp.Helper;


namespace SEOToolSet.WebApp
{
    public partial class Home : Page
    {
        public Int32 IdAccount
        {
            get
            {
                var result = -1;
                if (Int32.TryParse(Request.QueryString["IdAccount"], out result))
                {
                    return result;
                }
                if (result <= 0)
                {
                    var user = SEOMembershipManager.GetUser(Page.User.Identity.Name);
                    if (user != null)
                        result = user.Account.Id;
                }
                return result;
            }

        }

        public void Page_Load(Object sender, EventArgs e)
        {
            if (IsPostBack) return;

            AccountProjectsList1.IdAccount = IdAccount;
            AccountProjectsList1.RefreshProjects();
        }

        public void DoProjectLaunch(Object sender, Events.ProjectLaunchClickArgs e)
        {
            ProfileHelper.SelectedIdProject = e.IdProject;
            //((BC)Master).UpdateProjectSelector();
            //TODO: Afterwards, take the user to a reports page.
            SiteMapHelper.ReloadSamePage(Page);
        }

        protected void DoProjectDeleted(object sender, EventArgs e)
        {
            SiteMapHelper.ReloadSamePage(Page);
        }

        protected void DoProjectAdded(object sender, EventArgs e)
        {
            SiteMapHelper.ReloadSamePage(Page);
        }
    }
}
