using System;
using System.Web.UI;

namespace SEOToolSet.WebApp.Controls
{
    public partial class OverrideFontSettings : UserControl
    {
        protected void Page_PreRender(object sender, EventArgs args)
        {
            var fontSettigns = odsClass.FontsSettingsODS.GetFontSettingsFromSession();
            if (fontSettigns == null) return;
            R3M.Controls.Common.AddFileToPageHeader(Page, "~/FlyingStyles/FontsSettings.ashx", R3M.Controls.FileType.Css);
        }

        protected void LinkButtonReset_Click(object sender, EventArgs e)
        {
            odsClass.FontsSettingsODS.removeFontSettingsFromSession();
        }
    }
}