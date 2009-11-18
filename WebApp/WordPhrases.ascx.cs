using System;

namespace SEOToolSet.WebApp
{
    public partial class WordPhrases : System.Web.UI.UserControl
    {


        public String TitleText
        {
            get
            {
                return literalTitle.Text;
            }
            set
            {
                literalTitle.Text = value;
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {

        }
    }
}