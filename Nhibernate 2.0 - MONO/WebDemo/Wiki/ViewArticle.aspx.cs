using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Wiki_ViewArticle : System.Web.UI.Page
{
    private string ArticleName
    {
        get { return Request["name"]; }
    }

    private int ArticleVersion
    {
        get
        {
            string val = Request["version"];
            return val == null || val.Length == 0 ? 0 : int.Parse(val);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        viewArticle.ArticleName = ArticleName;
        viewArticle.ArticleVersion = ArticleVersion;
    }
}
