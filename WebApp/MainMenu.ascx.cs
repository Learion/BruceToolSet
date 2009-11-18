using System;
using System.Web;
using System.Web.UI.WebControls;
using SEOToolSet.WebApp.Helper;

namespace SEOToolSet.WebApp
{
    public partial class MainMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            //setMainMenu(HttpContext.Current.Items["MainMenu"] as String);

            //SiteMap.Provider.
            MenuRepeater.DataSource = SiteMapHelper.MenuNodes;

            MenuRepeater.DataBind();
        }

        public String MarkAsCurrent(object dataItem)
        {
            var node = SiteMapHelper.GetCurrentNode();
            return string.Format("button MenuItem {1} {0}", (((node != null) && node.IsDescendantOf((SiteMapNode)dataItem)) ? "selected" : ""), 
                CheckForAccess((SiteMapNode)dataItem));
        }

        public String CheckForAccess(SiteMapNode node)
        {
            return PermissionHelper.UserHasAccessToPage(node)
                       ? String.Empty
                       : "not_available";
        }

        public void OnMenuItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var showInNav = PermissionHelper.ShouldViewThePageInNav((SiteMapNode) e.Item.DataItem);
            if (showInNav) return;
            var liItem = e.Item.FindControl("liItem");
            liItem.Visible = false;
        }




    }

}