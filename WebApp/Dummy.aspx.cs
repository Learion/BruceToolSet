using System;
using System.Text;

namespace SEOToolSet.WebApp
{
    public partial class Dummy : System.Web.UI.Page
    {

        private String Token
        {
            get { return Request.QueryString["Token"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            WebUserControl0.PageTitleText = Token;
            
            //GridView1.DataSource = WebApp.Controls.CostConversionUtil.Currency.GetWorldCurrencies();
            //GridView1.DataBind();

        }
    }
}
