using System.Web;
using System.Web.UI;

namespace SEOToolSet.WebApp.Helper
{
    public static class SiteMapHelper
    {
        public static string HomePageUrl { get { return "Home.aspx"; } }
        public static string LoginPageUrl { get { return "LoginPage.aspx"; } }

        public static SiteMapNodeCollection MenuNodes = GetMenuNodes();

        private static SiteMapNodeCollection GetMenuNodes()
        {
            foreach (SiteMapNode node in SiteMap.RootNode.ChildNodes)
            {
                if (node["nodeType"] == "Menu") return node.ChildNodes;
            }
            return new SiteMapNodeCollection();
        }

        public static SiteMapNodeCollection GetSubMenuNodesForNode(SiteMapNode currentNode)
        {
            SiteMapNodeCollection col;
            return nodeIsDescendantOfMenu(currentNode, out col) ? col : new SiteMapNodeCollection();
        }

        private static bool nodeIsDescendantOfMenu(SiteMapNode node, out SiteMapNodeCollection col)
        {
            col = null;
            if (node == null) return false;
            foreach (SiteMapNode menuNode in MenuNodes)
            {
                if (!node.IsDescendantOf(menuNode)) continue;
                col = menuNode.ChildNodes;
                return true;
            }
            return false;
        }

        public static SiteMapNode GetCurrentNode()
        {
            //return (SiteMap.CurrentNode != null) ? SiteMap.CurrentNode :
            //    (HttpContext.Current.Request.RawUrl.Split('?').Length > 0)? SiteMap.Provider.FindSiteMapNode(HttpContext.Current.Request.RawUrl.Split('?')[0]) : null;
            return SiteMap.CurrentNode ??
                SiteMap.Provider.FindSiteMapNode(HttpContext.Current.Request.Url.Segments[0]);

        }

        public static void ReloadSamePage(Page page)
        {
            if (page == null) return;
            page.Response.Redirect(page.Request.RawUrl, false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}
