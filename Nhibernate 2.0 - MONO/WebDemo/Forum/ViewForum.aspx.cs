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

public partial class Forum_ViewForum : System.Web.UI.Page
{
    private string ForumName
    {
        get { return Request["forum"]; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Eucalypto.Forum.Category category = Eucalypto.Forum.ForumManager.GetCategoryByName(ForumName, true);
        if (Eucalypto.SecurityHelper.CanRead(Page.User, category, null) == false)
            throw new Eucalypto.InvalidPermissionException("read forum");

        HtmlLink link = new HtmlLink();
        link.Href = Navigation.Forum_ForumRss(ForumName).GetServerUrl(true);
        link.Attributes.Add("rel", "alternate");
        link.Attributes.Add("type", "application/rss+xml");
        link.Attributes.Add("title", "Forum " + category.DisplayName + " News");
        Header.Controls.Add(link);

        linkNewTopic.HRef = Navigation.Forum_NewTopic(ForumName).GetServerUrl(true);
        linkRss.HRef = Navigation.Forum_ForumRss(ForumName).GetServerUrl(true);

        lblForumName.InnerText = category.DisplayName;
        lblDescription.InnerText = category.Description;

        linkNewTopic.Visible = Eucalypto.SecurityHelper.CanInsert(Page.User, category);

        linkSearch.HRef = Navigation.Forum_Search().GetServerUrl(true);

        topicList.CategoryName = ForumName;
    }
}
