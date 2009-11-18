using System;
using System.Web;
using SEOToolSet.WebApp.Helper;

namespace SEOToolSet.WebApp
{
    public partial class SubMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            if (IsPostBack) return;
            //Response.Write("SiteMap.CurrentNode is Null " + (SiteMap.CurrentNode == null));
            var currentNode = SiteMapHelper.GetCurrentNode();

            var subMenu = String.Empty;

            foreach (SiteMapNode node in SiteMapHelper.MenuNodes)
            {
                var renderedMenu = CreateSubMenu(node.ChildNodes, true);
                var style = "style='display:none;'";
                if (currentNode != null && currentNode.IsDescendantOf(node))
                {
                    renderedMenu = HighlightPath(renderedMenu, currentNode);
                    style = "";
                }

                subMenu += renderedMenu.Replace("[VISIBLE]", style);
            }
            LiteralMenu.Text = subMenu;
        }

        private string HighlightPath(string renderedMenu, SiteMapNode node)
        {
            var tempNode = node;
            while (tempNode != null && tempNode != SiteMap.RootNode && tempNode["nodeType"] != "Menu")
            {
                const string tempValueString = "a{0}href='{1}'";
                var nodeUrl = ResolveClientUrl((tempNode["MenuUrl"] ?? tempNode.Url));
                var tempValue = String.Format(tempValueString, " ", nodeUrl);
                renderedMenu = renderedMenu.Replace(tempValue,
                             String.Format(tempValueString, " alt ='' ", nodeUrl) + " class='selected' ");
                tempNode = tempNode.ParentNode;
            }
            return renderedMenu;
        }

        private string CreateSubMenu(SiteMapNodeCollection nodes, bool isRoot)
        {
            var tempMenu = String.Format("<ul {0}> ", isRoot ? "[VISIBLE] class='adxm menu'" : "");

            foreach (SiteMapNode node in nodes)
            {
                var showInNav = PermissionHelper.ShouldViewThePageInNav(node);
                if (!showInNav) continue;
                var nodeUrl = ResolveClientUrl((node["MenuUrl"] ?? node.Url));
                tempMenu +=
                    String.Format(
                        @"<li><a href='{0}' title='{1}' {4}>
		                        <span>{2}</span>
		                    </a>
                            {3}
	                    </li>",
                        nodeUrl,
                        node.Description,
                        Server.HtmlEncode((node["MenuTitle"] ?? node.Title)).Replace(" ", "&nbsp;"),
                        node.HasChildNodes ? CreateSubMenu(node.ChildNodes, false) : "",
                        PermissionHelper.UserHasAccessToPage(node) ? String.Empty : "is_valid='not_available'");
            }

            tempMenu += "</ul>";

            return tempMenu;
        }



        //public void setMainMenu(string token)
        //{
        //    if (token == null) return;

        //    var controlID = string.Format("{0}SubMenu", token);
        //    var anchor = FindControl(controlID) as HtmlGenericControl;
        //    if (anchor != null)
        //    {
        //        anchor.Visible = true;
        //    }

        //}
    }

}