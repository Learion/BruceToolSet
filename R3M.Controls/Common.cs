#region Using Directives

using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#endregion

namespace R3M.Controls
{
    public class Common
    {
        public static void AddFileToPageHeader(Page page, String resolvedUrl, FileType fType)
        {
            AddFileToPageHeader(page, resolvedUrl, fType, "screen", null);
        }

        public static void AddFileToPageHeader(Page page, String resolvedUrl, FileType fType, String mediaLinkAttribute, String conditionalComment)
        {
            var useComemnt = !String.IsNullOrEmpty(conditionalComment);
            if (useComemnt) { addBeginCommentTag(page, conditionalComment); }
            if (IsHeaderLinkOrScriptControlAdded(page, resolvedUrl, fType)) return;
            switch (fType)
            {
                case FileType.Css:
                    AddCSS(page, resolvedUrl, mediaLinkAttribute);
                    break;
                case FileType.Javascript:
                    AddJavascript(page, resolvedUrl);
                    break;
            }

            if (useComemnt) { addEndCommentTag(page); }
        }

        private static void addBeginCommentTag(Page page, string conditionalComment)
        {
            var lit = new Literal
            {
                Text = String.Format(CultureInfo.InvariantCulture, "<!--[if {0}]>", conditionalComment)
            };
            page.Header.Controls.Add(lit);
        }

        private static void addEndCommentTag(Page page)
        {
            var lit = new Literal
            {
                Text = "<![endif]-->"
            };
            page.Header.Controls.Add(lit);
        }

        private static void AddCSS(Page page, String resolvedUrl, String media)
        {
            var link = new HtmlLink();
            link.Attributes.Add("href", resolvedUrl);
            if (!String.IsNullOrEmpty(media)) link.Attributes.Add("media", media);
            link.Attributes.Add("rel", "stylesheet");
            link.Attributes.Add("type", "text/css");

            page.Header.Controls.Add(link);
        }

        private static void AddJavascript(Page page, String resolvedUrl)
        {
            var script = new HtmlGenericControl("script");
            script.Attributes.Add("type", "text/javascript");
            script.Attributes.Add("src", resolvedUrl);

            page.Header.Controls.Add(script);
        }

        private static bool IsHeaderLinkOrScriptControlAdded(Page page, string ResolvedUrl, FileType fType)
        {
            switch (fType)
            {
                case FileType.Css:
                    foreach (Control c in page.Header.Controls)
                    {
                        var linkControl = c as HtmlLink;
                        if (linkControl == null) continue;
                        if (linkControl.Attributes["href"] == ResolvedUrl)
                            return true;
                    }
                    break;
                case FileType.Javascript:
                    foreach (Control c in page.Header.Controls)
                    {
                        var genericControl = c as HtmlGenericControl;
                        if (genericControl == null) continue;
                        if (genericControl.Attributes["src"] == ResolvedUrl)
                            return true;
                    }
                    break;
            }
            return false;
        }

        public static bool IsMono = EvaluateIsMONO();

        private static bool EvaluateIsMONO()
        {
            var t = Type.GetType("Mono.Runtime");
            return t != null;
        }

    }
}