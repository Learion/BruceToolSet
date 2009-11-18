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
using WebDemo.code;

public partial class Forum_NewMessage : System.Web.UI.Page
{
    private const string MESSAGE_RESPONSE_TAG = "RE: ";

    private string IdParentMessage
    {
        get { return Request["parent"]; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Eucalypto.Forum.Message parentMessage = GetParentMessage();

        if (IsPostBack == false)
        {
            string title = parentMessage.Title;

            if (title.StartsWith(MESSAGE_RESPONSE_TAG))
                newMessage.MessageSubject = title;
            else
                newMessage.MessageSubject = MESSAGE_RESPONSE_TAG + title;

            Eucalypto.Forum.Category forum = parentMessage.Topic.Category;

            if (forum.AttachEnabled)
            {
                newMessage.SetAcceptedExtensions(Eucalypto.Attachment.FileHelper.ReplaceExtensionsSets(forum.AttachExtensions));
                newMessage.SetMaxAttachSize(forum.AttachMaxSize);
                newMessage.EnabledAttach = true;
            }
            else
                newMessage.EnabledAttach = false;
        }

        viewParentMessage.SetMessage(parentMessage);
    }

    private Eucalypto.Forum.Message GetParentMessage()
    {
        return Eucalypto.Forum.ForumManager.GetMessage(IdParentMessage);
    }

    protected void btCancel_Click(object sender, EventArgs e)
    {
        Navigation.Forum_ViewTopic(GetParentMessage().Topic.Id).Redirect(this);
    }

    protected void btSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Eucalypto.Forum.Message parentMessage = GetParentMessage();
            Eucalypto.Forum.Topic topic = GetParentMessage().Topic;
            Eucalypto.Forum.Category forum = topic.Category;

            //Check permission
            if (Eucalypto.SecurityHelper.CanInsert(Page.User, forum) == false)
                throw new Eucalypto.InvalidPermissionException("insert new message");

            Eucalypto.XHTMLText xhtml = new Eucalypto.XHTMLText();
            xhtml.Load(newMessage.MessageBodyHtml);

            Exception validateError;
            if (xhtml.IsValid(forum.XHtmlMode, out validateError) == false)
                throw new Eucalypto.TextNotValidException(validateError);

            Eucalypto.Attachment.FileInfo attachment = null;
            //Create attachment
            if (newMessage.AttachmentFile.HasFile)
                attachment = new Eucalypto.Attachment.FileInfo(newMessage.AttachmentFile.FileName,
                                                            newMessage.AttachmentFile.PostedFile.ContentType,
                                                            newMessage.AttachmentFile.FileBytes);

            //Insert the message
            Eucalypto.Forum.ForumManager.CreateMessage(topic, IdParentMessage, User.Identity.Name,
                                                    newMessage.MessageSubject,
                                                    xhtml.GetXhtml(),
                                                    attachment);

            Navigation.Forum_ViewTopic(topic.Id).Redirect(this);
        }
        catch (Exception ex)
        {
            ((IErrorMessage)Master).SetError(GetType(), ex);
        }
    }
}
