using System;
using System.Web.UI;
using SEOToolSet.WebApp.Helper;

namespace SEOToolSet.WebApp
{
    public partial class Pixelsilk : Page
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

        }
    }
}
