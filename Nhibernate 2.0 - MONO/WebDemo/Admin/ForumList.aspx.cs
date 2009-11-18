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

public partial class ForumList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        linkNewItem.HRef = Navigation.Admin_ForumDetailsNew().GetServerUrl(true);
        linkShowForum.HRef = Navigation.Forum_Default().GetServerUrl(true);

        if (!IsPostBack)
        {
            LoadList();
        }
    }

    protected void listRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            string forumId = (string)e.CommandArgument;

            Eucalypto.Forum.Category category = Eucalypto.Forum.ForumManager.GetCategory(forumId);
            Eucalypto.Forum.ForumManager.DeleteCategory(category);

            LoadList();
        }
        else if (e.CommandName == "edit")
        {
            string forumId = (string)e.CommandArgument;

            Navigation.Admin_ForumDetails(forumId).Redirect(this);
        }
    }

    private void LoadList()
    {
        IList<Eucalypto.Forum.Category> list = Eucalypto.Forum.ForumManager.GetAllCategories();

        listRepeater.DataSource = list;
        listRepeater.DataBind();
    }
}
