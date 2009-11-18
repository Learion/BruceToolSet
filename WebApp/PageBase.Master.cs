using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using SEOToolSet.Common;
using SEOToolSet.WebApp.Helper;

namespace SEOToolSet.WebApp
{
    public partial class PageBase : System.Web.UI.MasterPage
    {
        public Uri PreviousPageUri
        {
            get
            {
                return ViewState["previousPage"] == null ? null : (Uri)ViewState["previousPage"];
            }
            set
            {
                ViewState["previousPage"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Force the html page to not be retrieved from the Cache
            //After a back button pressed.
            Response.Cache.SetNoStore();

            if (!IsPostBack)
            {
                if (Request.UrlReferrer != null)
                    PreviousPageUri = Request.UrlReferrer;
            }


            var cookie = Request.Cookies["CurrentCulture"];
            var languageSelected = (cookie != null && cookie.Value != null)
                                          ? cookie.Value : "";
            if (!IncludeFileJavascriptMessages.FilePath.Contains("?culture="))
            {
                IncludeFileJavascriptMessages.FilePath += "?culture=" + languageSelected;
            }

            AddTitleDescriptionBasedOnCurrentNode();

        }

        private void AddTitleDescriptionBasedOnCurrentNode()
        {

            var currentNode = SiteMapHelper.GetCurrentNode();

            if (currentNode == null) return;

            var title = (!string.IsNullOrEmpty(currentNode.Title)) ? currentNode.Title : Page.Title;
            var description = currentNode.Description;

            if (!string.IsNullOrEmpty(currentNode.Description))
            {
                addMeta("description", description);
            }

            //TODO: find a way to prevent the Ajax Scripts to be loaded when they're not necessary ¿¿¿¿could that be possible??? It seems really hard to accomplish
            //PanelWrapper.Visible = !disableASPNETAjax;

            Page.Title = String.Format(System.Globalization.CultureInfo.InvariantCulture, "SEOToolSet - {0}", title);
        }

        public void addMeta(String name, String content)
        {
            foreach (var tag in Page.Header.Controls)
            {
                if (!(tag is HtmlMeta)) continue;
                var meta = ((HtmlMeta)tag);
                if (meta.Name.ToLowerInvariant() != name) continue;
                meta.Content = content;
                return;
            }
            var descMeta = new HtmlMeta { Content = content, Name = name };
            Page.Header.Controls.Add(descMeta);
        }

        public void UpdateProjectSelector()
        {
            userControlHeader.UpdateProjectSelectorDropdownList();
        }

        public void NavigateBack()
        {
            if (PreviousPageUri != null)
                WebHelper.RedirectTo(PreviousPageUri.ToString());
            else
                WebHelper.RedirectTo(SiteMapHelper.HomePageUrl);
        }
        /// <summary>
        /// The excess to apply when try to calculate the width of the .CenterWrapper plugins this excess is used in the Dialog Buttons
        /// </summary>
        /// <returns></returns>
        public string GetDialogButtonExcessBasedOnClientCulture()
        {
            var cookie = Request.Cookies["CurrentCulture"];
            return ((cookie == null || cookie.Value.ToLowerInvariant() != "ja-jp") ? 10 : 38).ToString();
        }
    }
}
