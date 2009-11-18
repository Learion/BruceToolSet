#region Using Directives

using System;
using System.Web;
using System.Web.UI;
using SEOToolSet.WebApp.Helper;

#endregion

namespace SEOToolSet.WebApp
{
    public partial class PageTitle : UserControl
    {
        public String PageTitleText
        {
            get { return litTitle.Text; }
            set { litTitle.Text = value; }
        }


        public String PageDescription
        {
            get { return litDescription.Text; }
            set { litDescription.Text = value; }
        }

        public bool PanelContainerVisible
        {
            get { return panelContainer.Visible; }
            set { panelContainer.Visible = value; }
        }

        public bool RenderRoundPanelStyles
        {
            get { return !panelContainer.NotRenderStyles; }
            set { panelContainer.NotRenderStyles = !value; }
        }

        public bool HelpContainerVisible
        {
            get { return HelpDocumentation.Visible; }
            set { HelpDocumentation.Visible = value; }
        }
        public string AccountInfo
        {
            get { return litAccount.Text; }
            set { litAccount.Text = value; }
        }
        protected void Page_Load(Object sender, EventArgs e)
        {
            showHelpLink();
        }

        private void showHelpLink()
        {
            var currentNode = SiteMapHelper.GetCurrentNode();

            var helpUrl = "~/HelpPages/index.html";

            if (currentNode != null && !String.IsNullOrEmpty(currentNode["HelpPage"]))
            {
                helpUrl = currentNode["HelpPage"];
            }

            hlnkDocumentation.NavigateUrl = ResolveClientUrl(helpUrl);

        }
    }
}