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

public partial class NewsList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        linkNewItem.HRef = Navigation.Admin_NewsDetailsNew().GetServerUrl(true);
        linkShowNews.HRef = Navigation.News_Default().GetServerUrl(true);

        if (!IsPostBack)
        {
            LoadList();
        }
    }

    protected void listRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            string id = (string)e.CommandArgument;

            Eucalypto.News.Category category = Eucalypto.News.NewsManager.GetCategory(id);
            Eucalypto.News.NewsManager.DeleteCategory(category);

            LoadList();
        }
        else if (e.CommandName == "edit")
        {
            string id = (string)e.CommandArgument;

            Navigation.Admin_NewsDetails(id).Redirect(this);
        }
    }

    private void LoadList()
    {
        IList<Eucalypto.News.Category> list = Eucalypto.News.NewsManager.GetAllCategories();

        listRepeater.DataSource = list;
        listRepeater.DataBind();
    }
}
