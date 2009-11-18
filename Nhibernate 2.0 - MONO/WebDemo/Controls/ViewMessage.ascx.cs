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
using System.ComponentModel;
using WebDemo.code;

public partial class Controls_ViewMessage : System.Web.UI.UserControl
{
    private const int MESSAGE_INDENT_PIXEL = 8;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (mMessageLoaded == false)
        {
            Eucalypto.Forum.Message msg = Eucalypto.Forum.ForumManager.GetMessage(IdMessage);
            LoadMessage(msg);
        }
    }

    #region Properties
    [Bindable(false),
     Category("Display"),
    DefaultValue(true),
     Description("True to display the Reply link. The link is visible only if the user can insert messages.")]
    public bool ReplyLinkVisible
    {
        get
        {
            object val = ViewState["ReplyLinkVisible"];
            return val == null ? true : (bool)val;
        }
        set { ViewState["ReplyLinkVisible"] = value; }
    }

    [Bindable(false),
     Category("Display"),
    DefaultValue(true),
     Description("True to display the Delete link. The link is visible only if the user can delete the message.")]
    public bool DeleteLinkVisible
    {
        get
        {
            object val = ViewState["DeleteVisible"];
            return val == null ? true : (bool)val;
        }
        set { ViewState["DeleteVisible"] = value; }
    }

    public string IdMessage
    {
        get
        {
            object val = ViewState["IdMessage"];
            return val == null ? null : (string)val;
        }
        set { ViewState["IdMessage"] = value; }
    }
    #endregion

    /// <summary>
    /// Set the message to load. This method autmatically set also the IdMessage property.
    /// </summary>
    /// <param name="msg"></param>
    public void SetMessage(Eucalypto.Forum.Message msg)
    {
        IdMessage = msg.Id;

        LoadMessage(msg);
    }

    /// <summary>
    /// Flag that indicates if the control structure is loaded. Is used to optimize performance and don't load more than the necessary times the Message
    /// </summary>
    private bool mMessageLoaded = false;
    private void LoadMessage(Eucalypto.Forum.Message msg)
    {
        Eucalypto.Forum.Topic topic = msg.Topic;
        Eucalypto.Forum.Category forum = topic.Category;


        if (Eucalypto.SecurityHelper.CanRead(Page.User, forum, msg) == false)
            throw new Eucalypto.InvalidPermissionException("read message");

        //Create a link (a element) that can be used for anchor (vertical navigation), note that I cannot use ASP.NET element because ASP.NET automatically change the ID adding the container id (containerid:controlid)
        string anchorId = "msg" + msg.Id; //Note: this is the format that you must use when you want to navigate to a message: es. ViewTopic.aspx?id=xxx#msgYYY
        messageTitle.InnerHtml = string.Format("<a id=\"{0}\">{1}</a>", anchorId, HttpUtility.HtmlEncode(msg.Title));


        lblAuthor.InnerText = Utilities.GetDisplayUser(msg.Owner);
        lblDate.InnerText = Utilities.GetDateTimeForDisplay(msg.InsertDate);

        sectionBody.InnerHtml = msg.Body;

        if (DeleteLinkVisible && Eucalypto.SecurityHelper.CanDelete(Page.User, forum, msg))
            sectionDelete.Visible = true;
        else
            sectionDelete.Visible = false;

        if (ReplyLinkVisible && Eucalypto.SecurityHelper.CanInsert(Page.User, forum))
            sectionNew.Visible = true;
        else
            sectionNew.Visible = false;

        if (msg.Attachment != null)
        {
            sectionAttachment.Visible = true;

            linkAttach.InnerHtml = HttpUtility.HtmlEncode(msg.Attachment.Name);
            linkAttach.HRef = Navigation.Forum_Attach(msg.Id, true).GetServerUrl(true);
        }
        else
            sectionAttachment.Visible = false;

        //Flag the control as loaded
        mMessageLoaded = true;
    }

    public void SetIndentLevel(int level)
    {
        if (level > 0)
            controlDiv.Style[HtmlTextWriterStyle.MarginLeft] = (level * MESSAGE_INDENT_PIXEL).ToString() + "px";
    }

    protected void MessageNew_Click(object sender, EventArgs e)
    {
        try
        {
            Eucalypto.Forum.Message msg = Eucalypto.Forum.ForumManager.GetMessage(IdMessage);
            Eucalypto.Forum.Topic topic = msg.Topic;
            Eucalypto.Forum.Category forum = topic.Category;

            if (Eucalypto.SecurityHelper.CanInsert(Page.User, forum))
                Navigation.Forum_NewMessage(IdMessage).Redirect(this);
            else
                throw new Eucalypto.InvalidPermissionException("insert new message");
        }
        catch (Exception ex)
        {
            ((IErrorMessage)Page.Master).SetError(GetType(), ex);
        }
    }

    protected void MessageDelete_Click(object sender, EventArgs e)
    {
        Eucalypto.Forum.Message msg = Eucalypto.Forum.ForumManager.GetMessage(IdMessage);
        Eucalypto.Forum.Topic topic = msg.Topic;
        Eucalypto.Forum.Category forum = topic.Category;

        if (Eucalypto.SecurityHelper.CanDelete(Page.User, forum, msg) == false)
            throw new Eucalypto.InvalidPermissionException("delete message");

        //If there isn't a parent it is because it is the root message, in this case I delete directly the topic
        if (string.IsNullOrEmpty(msg.IdParentMessage))
        {
            Eucalypto.Forum.ForumManager.DeleteTopic(topic);

            Navigation.Forum_ViewForum(forum.Name).Redirect(this);
        }
        else
        {
            Eucalypto.Forum.ForumManager.DeleteMessage(msg);

            Navigation.Forum_ViewTopic(topic.Id).Redirect(this);
        }
    }
}
