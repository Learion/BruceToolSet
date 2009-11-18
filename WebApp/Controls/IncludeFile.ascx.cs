using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using R3M.Controls;


namespace SEOToolSet.WebApp.Controls
{

    public partial class IncludeFile : UserControl
    {

        [UrlProperty]
        public String FilePath
        {
            get
            {
                return (String)(ViewState["FilePath"] ?? String.Empty);
            }
            set
            {
                ViewState["FilePath"] = value;
            }
        }

        public String ConditionalComment
        {
            get
            {
                return (String)(ViewState["ConditionalComment"] ?? String.Empty);
            }
            set
            {
                ViewState["ConditionalComment"] = value;
            }
        }

        public FileType TypeOfFile
        {
            get
            {
                return (FileType)(ViewState["TypeOfFile"] ?? FileType.Javascript);
            }
            set
            {
                ViewState["TypeOfFile"] = value;
            }
        }
        public String MediaCssAttribute
        {
            get
            {
                return (String)(ViewState["MediaCssAttribute"] ?? String.Empty);
            }
            set
            {
                ViewState["MediaCssAttribute"] = value;
            }
        }

        protected static void Page_Load(object sender, EventArgs e)
        {

        }

        private void AddToPage(String resolvedUrl, FileType fType)
        {
            R3M.Controls.Common.AddFileToPageHeader(Page, resolvedUrl, fType, MediaCssAttribute, ConditionalComment);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (String.IsNullOrEmpty(FilePath)) return;
            var ResolvedUrl = ResolveClientUrl(FilePath);
            AddToPage(ResolvedUrl, TypeOfFile);
        }
    }
}