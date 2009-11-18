using System;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SEOToolSet.WebApp.Controls
{
    public partial class IncludeSilverlight : System.Web.UI.UserControl
    {
        public String InitParameters { get; set; }
        public String Background { get; set; }
        public Boolean IsWindowless { get; set; }
        public String HostStyle { get; set; }


        private String _silverlightContext;
        public String SilverlightContext
        {
            get
            {
                if (String.IsNullOrEmpty(_silverlightContext))
                {
                    _silverlightContext = (Guid.NewGuid()).ToString();
                }
                return _silverlightContext;
            }
            set
            {
                _silverlightContext = value;
            }
        }



        [UrlProperty]
        public String Source { get; set; }

        public Unit? Height { get; set; }

        public Unit? Width { get; set; }

        public String SilverlightVersion { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(HostStyle)) SilverLightHost.Attributes["style"] = HostStyle;
        }
    }
}