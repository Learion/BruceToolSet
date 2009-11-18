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

public partial class Wiki_EditArticle : System.Web.UI.Page
{
    private string ArticleName
    {
        get { return Request["name"]; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            Eucalypto.Wiki.Article article = Eucalypto.Wiki.WikiManager.GetArticleByName(ArticleName, true);

            txtName.Text = article.Name;
            txtBody.Text = article.Body;
            txtDescription.Text = article.Description;
            txtTitle.Text = article.Title;
            txtOwner.Text = article.Owner;
            txtAuthor.Text = article.Author;
            chkApproved.Checked = article.Approved;
            chkEnabled.Checked = article.Enabled;

            LoadAttachmentsList(article);

            btDelete.Enabled = Eucalypto.SecurityHelper.CanDelete(Page.User, article.Category, article);

            chkApproved.Enabled = Eucalypto.SecurityHelper.MatchPermissions(Page.User, article.Category.ApprovePermissions);
            chkEnabled.Enabled = Eucalypto.SecurityHelper.MatchPermissions(Page.User, article.Category.ApprovePermissions);

            if (article.Category.BackupMode == Eucalypto.WikiBackupMode.Never)
            {
                chkBackup.Checked = false;
                chkBackup.Enabled = false;
            }
            else if (article.Category.BackupMode == Eucalypto.WikiBackupMode.Always)
            {
                chkBackup.Checked = true;
                chkBackup.Enabled = false;
            }
            else
            {
                chkBackup.Checked = true;
                chkBackup.Enabled = true;
            }


            sectionAttachments.Visible = article.Category.AttachEnabled;
        }
    }

    protected void btDelete_Click(object sender, EventArgs e)
    {
        try
        {
            Eucalypto.Wiki.Article article = Eucalypto.Wiki.WikiManager.GetArticleByName(ArticleName, true);
            Eucalypto.Wiki.Category category = article.Category;

            if (Eucalypto.SecurityHelper.CanDelete(Page.User, article.Category, article))
            {
                Eucalypto.Wiki.WikiManager.DeleteArticle(article);

                Navigation.Wiki_ViewCategory(category.Name).Redirect(this);
            }
            else
                throw new Eucalypto.InvalidPermissionException("delete article");
        }
        catch (Exception ex)
        {
            ((IErrorMessage)Master).SetError(GetType(), ex);
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        try
        {
            Eucalypto.Wiki.Article article = Eucalypto.Wiki.WikiManager.GetArticleByName(ArticleName, true);

            //Check permissions
            if (Eucalypto.SecurityHelper.CanEdit(Page.User, article.Category, article) == false)
                throw new Eucalypto.InvalidPermissionException("edit the article");

            Eucalypto.XHTMLText xhtml = new Eucalypto.XHTMLText();
            xhtml.Load(txtBody.Text);

            Exception validateError;
            if (xhtml.IsValid(article.Category.XHtmlMode, out validateError) == false)
                throw new Eucalypto.TextNotValidException(validateError);

            //TOC
            if (chkCreateTOC.Checked)
                article.TOC = xhtml.GenerateTOC();
            else
                article.TOC = null;

            article.Body = xhtml.GetXhtml();
            article.Description = txtDescription.Text;
            article.Title = txtTitle.Text;
            article.Approved = chkApproved.Checked;
            article.Enabled = chkEnabled.Checked;
            article.UpdateUser = Page.User.Identity.Name;
            article.Author = txtAuthor.Text;

            Eucalypto.Wiki.WikiManager.UpdateArticle(article, chkBackup.Checked);

            Navigation.Wiki_ViewArticle(article.Name, 0).Redirect(this);
        }
        catch (Exception ex)
        {
            ((IErrorMessage)Master).SetError(GetType(), ex);
        }
    }
    protected void btCancel_Click(object sender, EventArgs e)
    {
        Eucalypto.Wiki.Article article = Eucalypto.Wiki.WikiManager.GetArticleByName(ArticleName, true);

        Navigation.Wiki_ViewCategory(article.Category.Name).Redirect(this);
    }

    protected void listAttachments_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "delete")
            {
                string name = (string)e.CommandArgument;

                Eucalypto.Wiki.Article article = Eucalypto.Wiki.WikiManager.GetArticleByName(ArticleName, true);

                //Check permissions
                if (Eucalypto.SecurityHelper.CanDelete(Page.User, article.Category, article) == false)
                    throw new Eucalypto.InvalidPermissionException("delete attachment");

                Eucalypto.Wiki.FileAttachment attachment = Eucalypto.Wiki.WikiManager.GetFileAttachmentByName(article, name, true);

                Eucalypto.Wiki.WikiManager.DeleteFileAttachment(attachment);

                LoadAttachmentsList(article);
            }
        }
        catch (Exception ex)
        {
            ((IErrorMessage)Master).SetError(GetType(), ex);
        }
    }

    private void LoadAttachmentsList(Eucalypto.Wiki.Article article)
    {
        string[] attachments = Eucalypto.Wiki.WikiManager.GetFileAttachments(article, Eucalypto.Wiki.EnabledStatus.Enabled);

        listAttachments.DataSource = attachments;
        listAttachments.DataBind();
    }

    protected string GetAttachmentUrl(string name)
    {
        return Navigation.Wiki_Attach(ArticleName, name, true).GetClientUrl(this, true);
    }
    protected string GetAttachmentPlaceHolder(string name)
    {
        return name;
    }
    

    protected void btUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (uploadAttach.FileBytes == null || uploadAttach.FileBytes.Length == 0 ||
                uploadAttach.FileName == null || uploadAttach.FileName.Length == 0)
            {
                ((IErrorMessage)Master).SetError(GetType(), "File not valid");
                return;
            }

            Eucalypto.Wiki.Article article = Eucalypto.Wiki.WikiManager.GetArticleByName(ArticleName, true);

            //Check permissions
            if (Eucalypto.SecurityHelper.CanEdit(Page.User, article.Category, article) == false)
                throw new Eucalypto.InvalidPermissionException("edit the article");

            Eucalypto.Wiki.WikiManager.CreateFileAttachment(article, uploadAttach.FileName, 
                                                uploadAttach.PostedFile.ContentType, uploadAttach.FileBytes);

            LoadAttachmentsList(article);
        }
        catch (Exception ex)
        {
            ((IErrorMessage)Master).SetError(GetType(), ex);
        }
    }
}
