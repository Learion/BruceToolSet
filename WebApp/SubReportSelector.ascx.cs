using System;

namespace SEOToolSet.WebApp
{
    public partial class SubReportSelector : System.Web.UI.UserControl
    {
        protected void Page_Load(System.Object sender, EventArgs e)
        {
            runCommand.Attributes.Add("state01_text", GetGlobalResourceObject("CommonTerms", "Run").ToString());
            runCommand.Attributes.Add("state02_text", GetGlobalResourceObject("CommonTerms", "Cancel").ToString());
        }
    }
    
}