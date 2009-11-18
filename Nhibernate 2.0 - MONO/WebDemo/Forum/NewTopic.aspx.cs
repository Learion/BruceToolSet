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

public partial class Forum_NewTopic : System.Web.UI.Page
{
    private string ForumName
    {
        get { return Request["forum"]; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Eucalypto.Forum.Category forum = GetForum();

        if (forum.AttachEnabled)
        {
            newMessage.SetAcceptedExtensions(Eucalypto.Attachment.FileHelper.ReplaceExtensionsSets(forum.AttachExtensions));
            newMessage.SetMaxAttachSize(forum.AttachMaxSize);
            newMessage.EnabledAttach = true;
        }
        else
            newMessage.EnabledAttach = false;
    }

    private Eucalypto.Forum.Category GetForum()
    {
        Eucalypto.Forum.Category forum = Eucalypto.Forum.ForumManager.GetCategoryByName(ForumName, true);

        return forum;
    }

    protected void btSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Eucalypto.Forum.Category forum = GetForum();

            //Check permission
            if (Eucalypto.SecurityHelper.CanInsert(Page.User, forum))
            {
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

                //Insert the topic
                Eucalypto.Forum.ForumManager.CreateTopic(forum, User.Identity.Name,
                                                    newMessage.MessageSubject,
                                                    xhtml.GetXhtml(),
                                                    attachment);
            }
            else
                throw new Eucalypto.InvalidPermissionException("insert new message");

            Navigation.Forum_ViewForum(ForumName).Redirect(this);
        }
        catch (Exception ex)
        {
            ((IErrorMessage)Master).SetError(GetType(), ex);
        }
    }
    protected void btCancel_Click(object sender, EventArgs e)
    {
        Navigation.Forum_ViewForum(ForumName).Redirect(this);
    }
}
