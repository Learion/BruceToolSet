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

public partial class News_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlLink link = new HtmlLink();
        link.Href = Navigation.News_CategoryRss(null).GetServerUrl(true);
        link.Attributes.Add("rel", "alternate");
        link.Attributes.Add("type", "application/rss+xml");
        link.Attributes.Add("title", "News");
        Header.Controls.Add(link);

        if (!IsPostBack)
        {
            linkRss.HRef = Navigation.News_CategoryRss(null).GetServerUrl(true);

            LoadList();
        }
    }

    protected string GetCategoryLink(string name)
    {
        return Navigation.News_ViewCategory(name).GetClientUrl(this, true);
    }

    private void LoadList()
    {
        IList<Eucalypto.News.Category> listComplete = Eucalypto.News.NewsManager.GetAllCategories();

        //Create a list with only the readable items
        List<Eucalypto.News.Category> listReadable = new List<Eucalypto.News.Category>();
        foreach (Eucalypto.News.Category category in listComplete)
        {
            if (Eucalypto.SecurityHelper.CanRead(User, category, null))
                listReadable.Add(category);
        }

        listRepeater.DataSource = listReadable;
        listRepeater.DataBind();
    }
}
