using System;

namespace SEOToolSet.WebApp.Controls
{
    public partial class sIFR : System.Web.UI.UserControl
    {

        public String Selector { get; set; }
        
        [System.Web.UI.UrlProperty]
        public String SwfFontToRender { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}