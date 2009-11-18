using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using R3M.Controls.Properties;

namespace R3M.Controls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:HyperLinkRound runat=server></{0}:HyperLinkRound>")]
    public class HyperLinkRound : HyperLink
    {
        /// <summary>
        /// TODO: Save those values to the ViewState!!!!! otherwise always put the value in the .aspx
        /// </summary>
        [UrlProperty]
        public String CssResource { get; set; }

        protected override void OnPreRender(EventArgs e)
        {
            Common.AddFileToPageHeader(Page, ResolveClientUrl(CssResource ?? Settings.Default.RoundButtonCss),
                                       FileType.Css);
            base.OnPreRender(e);
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            CssClass = !String.IsNullOrEmpty(CssClass) ? CssClass : Settings.Default.DefaultRoundButtonClassName;
            base.RenderBeginTag(writer);
            writer.Write("<span class='Left'><span class='Right'><span class='Center'>");
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.Write("</span></span></span>");
            base.RenderEndTag(writer);
        }
    }
}
