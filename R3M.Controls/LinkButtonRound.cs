using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using R3M.Controls.Properties;

namespace R3M.Controls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:LinkButtonRound runat=server></{0}:LinkButtonRound>")]
    public class LinkButtonRound : LinkButton
    {
        /// <summary>
        /// TODO: Save those values to the ViewState!!!!! otherwise always put the value in the .aspx
        /// </summary>
        [UrlProperty]
        public String CssResource { get; set; }



        protected override void OnPreRender(EventArgs e)
        {
            CssClass = !String.IsNullOrEmpty(CssClass) ? CssClass : Settings.Default.DefaultRoundButtonClassName;
            Common.AddFileToPageHeader(Page, ResolveClientUrl(CssResource ?? Settings.Default.RoundButtonCss),
                                       FileType.Css);
            base.OnPreRender(e);
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
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
