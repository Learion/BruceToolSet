using System;

namespace SEOToolSet.WebApp.Controls
{
    public partial class ToolTip : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public String Selector { get; set; }

        public String ToShow { get; set; }
        public String ToHide { get; set; }

        public String FadeIn { get; set; }
        public String FadeOut { get; set; }

        public bool ShowTitle { get; set; }
        public string TooltipTitle { get; set; }

        public String GetOptionsObj()
        {
            var obj = "";
            var propertyPattern = " options.{0} = {1};";

            if (!String.IsNullOrEmpty(ToShow)) obj += String.Format(propertyPattern, "toShow", ToShow);
            if (!String.IsNullOrEmpty(ToHide)) obj += String.Format(propertyPattern, "toHide", ToHide);
            if (!String.IsNullOrEmpty(FadeIn)) obj += String.Format(propertyPattern, "fadeIn", FadeIn);
            if (!String.IsNullOrEmpty(FadeOut)) obj += String.Format(propertyPattern, "fadeOut", FadeOut);

            if (!ShowTitle || String.IsNullOrEmpty(TooltipTitle)) return obj;

            obj += String.Format(propertyPattern, "title", TooltipTitle);
            obj += String.Format(propertyPattern, "showTitle", ShowTitle.ToString().ToLowerInvariant());
            return obj;

        }
    }
}