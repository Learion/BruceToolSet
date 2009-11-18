#region Using Directives

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using R3M.Controls.Properties;

#endregion

namespace R3M.Controls
{
    public class RoundPanel : Panel
    {
        public bool DiscardTop { get; set; }
        public bool DiscardBottom { get; set; }

        [UrlProperty]
        public String CssResource { get; set; }

        public String CssIdClassName { get; set; }

        public Boolean NotRenderStyles { get; set; }


        protected override void OnPreRender(EventArgs e)
        {
            if (!NotRenderStyles)
                Common.AddFileToPageHeader(Page, ResolveClientUrl(CssResource ?? Settings.Default.RoundPanelCss),
                                       FileType.Css);
            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (!NotRenderStyles)
            {
                var top_string = "<div class='" +
                                 (String.IsNullOrEmpty(CssIdClassName)
                                      ? Settings.Default.DefaultCssIdClass
                                      : CssIdClassName) +
                                 "'>{TOP}<div class='bl'><div class='br ContentHolder'><div class='PanelRounded bd' rounded='true'>";
                var bottom_string = "</div><div style='clear: both;'></div></div></div>{BOTTOM}</div>";

                var top = DiscardTop
                              ? String.Empty
                              : "<div class='hd'><div class='tl'></div><div class='tr'></div><div class='tc'></div><div style='clear: both;'></div></div>";
                var bottom = DiscardBottom
                                 ? String.Empty
                                 : "<div class='ft'><div class='fl'></div><div class='fr'></div><div class='fc'></div><div style='clear: both;'></div></div>";

                top_string = top_string.Replace("{TOP}", top);
                bottom_string = bottom_string.Replace("{BOTTOM}", bottom);

                writer.WriteLine(top_string);
                base.Render(writer);
                writer.WriteLine(bottom_string);
            }
            else
            {
                base.Render(writer);
            }
        }
    }
}