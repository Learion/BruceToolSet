using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEOToolSet.WebApp.Helper;

namespace SEOToolSet.WebApp
{
    public partial class Project : Page
    {
        public Int32 IdProject
        {
            get
            {
                int result;
                if (!Int32.TryParse(Request.QueryString["IdProject"], out result))
                {
                    result = ProfileHelper.SelectedIdProject;
                }

                if (Providers.ProjectManager.UserIsAllowedInProject(result, Page.User.Identity.Name))
                {
                    ProfileHelper.SelectedIdProject = result;
                    return result;
                }
                return -1;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectPanel.Visible = IdProject != -1;
            ManageUsersControl2.IdProject = IdProject;
            ManageUsersControl1.IdProject = IdProject;
        }

        protected void OnUserAdded(object sender, EventArgs e)
        {
            RefreshPanels();
        }

        protected void LnkRefreshControl_OnClick(object sender, EventArgs e)
        {
            RefreshPanels();
        }

        private void RefreshPanels()
        {
            ManageUsersControl1.RefreshPanel();
            ManageUsersControl2.RefreshPanel();
        }

        protected void BulkLoadInserted(object sender, FormViewInsertedEventArgs e)
        {
            SiteMapHelper.ReloadSamePage(Page);
        }
    }
}