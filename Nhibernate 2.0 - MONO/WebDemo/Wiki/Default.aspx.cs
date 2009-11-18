using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using WebDemo.code;

public partial class Wiki_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlLink link = new HtmlLink();
        link.Href = Navigation.Wiki_CategoryRss(null).GetServerUrl(true);
        link.Attributes.Add("rel", "alternate");
        link.Attributes.Add("type", "application/rss+xml");
        link.Attributes.Add("title", "Wiki news");
        Header.Controls.Add(link);

        if (!IsPostBack)
        {
            linkRss.HRef = Navigation.Wiki_CategoryRss(null).GetServerUrl(true);

            linkSearch.HRef = Navigation.Wiki_Search().GetServerUrl(true);

            LoadList();
        }
    }

    protected string GetCategoryLink(string name)
    {
        return Navigation.Wiki_ViewCategory(name).GetClientUrl(this, true);
    }

    private void LoadList()
    {
        IList<Eucalypto.Wiki.Category> listComplete = Eucalypto.Wiki.WikiManager.GetAllCategories();

        //Create a list with only the readable items
        List<Eucalypto.Wiki.Category> listReadable = new List<Eucalypto.Wiki.Category>();
        foreach (Eucalypto.Wiki.Category category in listComplete)
        {
            if (Eucalypto.SecurityHelper.CanRead(User, category, null))
                listReadable.Add(category);
        }

        listRepeater.DataSource = listReadable;
        listRepeater.DataBind();
    }
}
