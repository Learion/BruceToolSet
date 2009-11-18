#region Using Directives

using System;
using System.Web.UI;
using R3M.Controls.Properties;

#endregion

namespace R3M.Controls
{
    public class MaskedInput : Control
    {
        public string MaskedInitializerUrl { get; set; }

        public string JQueryMaskedPluginUrl { get; set; }

        public string JqueryUrl { get; set; }


        protected override void OnLoad(EventArgs e)
        {
            Common.AddFileToPageHeader(Page,
                                       ResolveClientUrl(JqueryUrl ?? Settings.Default.JqueryPath),
                                       FileType.Javascript);

            Common.AddFileToPageHeader(Page,
                                       ResolveClientUrl(JQueryMaskedPluginUrl ?? Settings.Default.JQueryMaskedInputJs),
                                       FileType.Javascript);

            Common.AddFileToPageHeader(Page,
                                       ResolveClientUrl(MaskedInitializerUrl ?? Settings.Default.MaskedInitializer),
                                       FileType.Javascript);
        }
    }
}