#region Using Directives

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using R3M.Controls.Properties;

#endregion

namespace R3M.Controls
{
    [ToolboxData("<{0}:ConfirmMessageExt runat=server></{0}:ConfirmMessageExt>")]
    public class ConfirmMessageExt : WebControl
    {
        /// <summary>
        /// The CSS Selector for which the elements are going to be retrieved
        /// </summary>
        public string Selector { get; set; }

        [UrlProperty]
        public string ConfirmMessagePluginJs { get; set; }

        public string ConfirmMessage { get; set; }

        public bool useConfirmMessageFromElement { get; set; }

        public bool useItemNameInElement { get; set; }

        public string ConfirmTitle { get; set; }

        public string OkButtonText { get; set; }

        public string CancelButtonText { get; set; }


        protected override void OnPreRender(EventArgs e)
        {
            Common.AddFileToPageHeader(Page,
                                       ResolveClientUrl(ConfirmMessagePluginJs ??
                                                        Settings.Default.ConfirmMessagePluginJs), FileType.Javascript);
            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (String.IsNullOrEmpty(Selector)) return;
            var scriptInClient =
                @"
              <script type='text/javascript'>
              //<![CDATA[    
                    $.onDomReady(function () {{
                       $('{0}').addConfirm('{1}',{2});
                    }});
              //]]>
              </script>
            ";

            var options =
                String.Format(
                    "{{ title : '{0}', OkText : '{1}', CancelText: '{2}', useConfirmMessageFromElement: {3}, useItemNameInElement : {4} }}",
                    ConfirmTitle, OkButtonText, CancelButtonText,
                    useConfirmMessageFromElement.ToString().ToLowerInvariant(),
                    useItemNameInElement.ToString().ToLowerInvariant());
            scriptInClient = String.Format(scriptInClient, Selector, ConfirmMessage, options);

            writer.Write(scriptInClient);
        }
    }
}