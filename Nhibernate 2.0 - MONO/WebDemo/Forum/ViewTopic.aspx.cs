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

public partial class Forum_ViewTopic : System.Web.UI.Page
{
    private string IdTopic
    {
        get { return Request["id"]; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Eucalypto.Forum.Topic topic = Eucalypto.Forum.ForumManager.GetTopic(IdTopic);
        Eucalypto.Forum.Category forum = topic.Category;

        if (Eucalypto.SecurityHelper.CanRead(Page.User, forum, null) == false)
            throw new Eucalypto.InvalidPermissionException("read forum");

        lblTopic.InnerText = topic.Title;
        lnkForum.HRef = Navigation.Forum_ViewForum(forum.Name).GetServerUrl(true);

        viewTopic.IdTopic = IdTopic;
    }
}
