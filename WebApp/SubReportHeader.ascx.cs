using System;
namespace SEOToolSet.WebApp
{

    public partial class SubReportHeader : System.Web.UI.UserControl
    {
        public String TitleText
        {
            get{ return this.lblSubReportTitle.Text; }
            set{ this.lblSubReportTitle.Text = value; }
        }
    }
}