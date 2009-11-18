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

public partial class Forum_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlLink link = new HtmlLink();
        link.Href = Navigation.Forum_ForumRss(null).GetServerUrl(true);
        link.Attributes.Add("rel", "alternate");
        link.Attributes.Add("type", "application/rss+xml");
        link.Attributes.Add("title", "Forum News");
        Header.Controls.Add(link);


        if (!IsPostBack)
        {
            linkRss.HRef = Navigation.Forum_ForumRss(null).GetServerUrl(true);

            linkSearch.HRef = Navigation.Forum_Search().GetServerUrl(true);

            LoadList();
        }
    }

    protected string GetForumLink(string name)
    {
        return Navigation.Forum_ViewForum(name).GetClientUrl(this, true);
    }

    private void LoadList()
    {
        IList<Eucalypto.Forum.Category> listComplete = Eucalypto.Forum.ForumManager.GetAllCategories();

        //Create a list with only the readable items
        List<Eucalypto.Forum.Category> listReadable = new List<Eucalypto.Forum.Category>();
        foreach (Eucalypto.Forum.Category category in listComplete)
        {
            if (Eucalypto.SecurityHelper.CanRead(User, category, null))
                listReadable.Add(category);
        }

        listRepeater.DataSource = listReadable;
        listRepeater.DataBind();
    }
}
