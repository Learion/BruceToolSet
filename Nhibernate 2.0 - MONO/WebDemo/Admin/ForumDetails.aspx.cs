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

public partial class ForumDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            string forumId = Request["id"];

            //Edit
            if (forumId != null)
            {
                Eucalypto.Forum.Category forum = Eucalypto.Forum.ForumManager.GetCategory(forumId);

                txtName.Enabled = false;

                txtName.Text = forum.Name;
                txtDisplayName.Text = forum.DisplayName;
                txtEditPermissions.Text = forum.EditPermissions;
                txtReadPermissions.Text = forum.ReadPermissions;
                txtDeletePermissions.Text = forum.DeletePermissions;
                txtInsertPermissions.Text = forum.InsertPermissions;
                txtDescription.Text = forum.Description;

                chkEnabledAttach.Checked = forum.AttachEnabled;
                txtAttachExtensions.Text = forum.AttachExtensions;
                txtAttachMaxSize.Text = forum.AttachMaxSize.ToString();
            }
            else //New
            {
                txtEditPermissions.Text = Eucalypto.SecurityHelper.NONE;
                txtReadPermissions.Text = Eucalypto.SecurityHelper.ALL_USERS;
                txtDeletePermissions.Text = Eucalypto.SecurityHelper.NONE;
                txtInsertPermissions.Text = Eucalypto.SecurityHelper.AUTHENTICATED_USERS;

                chkEnabledAttach.Checked = true;
                txtAttachExtensions.Text = Eucalypto.Attachment.FileHelper.EXTENSIONS_ALL;
                txtAttachMaxSize.Text = "500"; //Kb
            }
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        try
        {
            string forumId = Request["id"];

            Eucalypto.Forum.Category forum;

            //Edit
            if (forumId != null)
            {
                forum = Eucalypto.Forum.ForumManager.GetCategory(forumId);
                
                forum.DisplayName = txtDisplayName.Text;
            }
            else //New
            {
                forum = Eucalypto.Forum.ForumManager.CreateCategory(txtName.Text, txtDisplayName.Text);
            }

            forum.EditPermissions = txtEditPermissions.Text;
            forum.ReadPermissions = txtReadPermissions.Text;
            forum.DeletePermissions = txtDeletePermissions.Text;
            forum.InsertPermissions = txtInsertPermissions.Text;
            forum.Description = txtDescription.Text;

            forum.AttachEnabled = chkEnabledAttach.Checked;
            forum.AttachExtensions = txtAttachExtensions.Text;
            forum.AttachMaxSize = int.Parse(txtAttachMaxSize.Text);

            Eucalypto.Forum.ForumManager.UpdateCategory(forum);


            Navigation.Admin_ForumList().Redirect(this);

        }
        catch (Exception ex)
        {
            ((IErrorMessage)Master).SetError(GetType(), ex);
        }
    }

    protected void btCancel_Click(object sender, EventArgs e)
    {
        Navigation.Admin_ForumList().Redirect(this);
    }

}
