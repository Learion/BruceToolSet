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

public partial class Controls_ViewTopic : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadTopic();
    }

    #region Properties
    /// <summary>
    /// Gets or sets the topic id to show. The value is saved in the ViewState.
    /// </summary>
    public string IdTopic
    {
        get
        {
            object val = ViewState["IdTopic"];
            return (string)val;
        }
        set { ViewState["IdTopic"] = value; }
    }
    #endregion

    private void LoadTopic()
    {
        Eucalypto.Forum.Topic topic = Eucalypto.Forum.ForumManager.GetTopic(IdTopic);
        Eucalypto.Forum.Category forum = topic.Category;

        if (Eucalypto.SecurityHelper.CanRead(Page.User, forum, null) == false)
            throw new Eucalypto.InvalidPermissionException("read forum");

        IList<Eucalypto.Forum.Message> messages = Eucalypto.Forum.ForumManager.GetMessagesByTopic(topic);

        ExploreMessages(forum, topic, messages, null, 0);
    }

    private void ExploreMessages(Eucalypto.Forum.Category forum, Eucalypto.Forum.Topic topic,
                        IList<Eucalypto.Forum.Message> messages, string filterParent, int level)
    {
        foreach (Eucalypto.Forum.Message msg in messages)
        {
            if (string.Equals(msg.IdParentMessage, filterParent, StringComparison.InvariantCultureIgnoreCase))
            {
                Controls_ViewMessage ctlMessage = (Controls_ViewMessage)LoadControl("~/Controls/ViewMessage.ascx");
                ctlMessage.SetMessage(msg);
                ctlMessage.SetIndentLevel(level);

                Controls.Add(ctlMessage);

                ExploreMessages(forum, topic, messages, msg.Id, level + 1);
            }
        }
    }
}
