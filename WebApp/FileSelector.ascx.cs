using System;

namespace SEOToolSet.WebApp
{
    public partial class FileSelector : System.Web.UI.UserControl
    {
        
        public bool HideUploadChoice { get; set; }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            rowIndicators.Visible = tdLnk.Visible = !HideUploadChoice;
            
        }

        protected void Page_Load(Object sender, EventArgs e)
        {

        }
    }
}