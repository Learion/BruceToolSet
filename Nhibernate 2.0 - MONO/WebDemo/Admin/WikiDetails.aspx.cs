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

public partial class WikiDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            string id = Request["id"];

            //Edit
            if (id != null)
            {
                Eucalypto.Wiki.Category category = Eucalypto.Wiki.WikiManager.GetCategory(id);

                txtName.Enabled = false;

                txtName.Text = category.Name;
                txtDisplayName.Text = category.DisplayName;
                txtEditPermissions.Text = category.EditPermissions;
                txtReadPermissions.Text = category.ReadPermissions;
                txtDeletePermissions.Text = category.DeletePermissions;
                txtInsertPermissions.Text = category.InsertPermissions;
                txtApprovePermissions.Text = category.ApprovePermissions;
                txtDescription.Text = category.Description;
                LoadCmbValidationMode(category.XHtmlMode);
                LoadCmbBackupMode(category.BackupMode);

                chkAutoApprove.Checked = category.AutoApprove;
                chkEnabledAttach.Checked = category.AttachEnabled;
                txtAttachExtensions.Text = category.AttachExtensions;
                txtAttachMaxSize.Text = category.AttachMaxSize.ToString();
            }
            else //New
            {
                txtEditPermissions.Text = Eucalypto.SecurityHelper.NONE;
                txtReadPermissions.Text = Eucalypto.SecurityHelper.ALL_USERS;
                txtDeletePermissions.Text = Eucalypto.SecurityHelper.NONE;
                txtInsertPermissions.Text = Eucalypto.SecurityHelper.AUTHENTICATED_USERS;
                txtApprovePermissions.Text = Eucalypto.SecurityHelper.NONE;
                LoadCmbValidationMode(Eucalypto.XHtmlMode.StrictValidation);
                LoadCmbBackupMode(Eucalypto.WikiBackupMode.Always);

                chkAutoApprove.Checked = true;
                chkEnabledAttach.Checked = true;
                txtAttachExtensions.Text = Eucalypto.Attachment.FileHelper.EXTENSIONS_ALL;
                txtAttachMaxSize.Text = "500"; //Kb
            }
        }
    }

    private void LoadCmbValidationMode(Eucalypto.XHtmlMode mode)
    {
        cmbXhtmlMode.Items.Clear();

        cmbXhtmlMode.Items.Add(Eucalypto.XHtmlMode.None.ToString());
        cmbXhtmlMode.Items.Add(Eucalypto.XHtmlMode.BasicValidation.ToString());
        cmbXhtmlMode.Items.Add(Eucalypto.XHtmlMode.StrictValidation.ToString());

        cmbXhtmlMode.SelectedValue = mode.ToString();
    }

    private void LoadCmbBackupMode(Eucalypto.WikiBackupMode mode)
    {
        cmbBackupMode.Items.Clear();

        cmbBackupMode.Items.Add(Eucalypto.WikiBackupMode.Always.ToString());
        cmbBackupMode.Items.Add(Eucalypto.WikiBackupMode.Request.ToString());
        cmbBackupMode.Items.Add(Eucalypto.WikiBackupMode.Never.ToString());

        cmbBackupMode.SelectedValue = mode.ToString();
    }


    protected void btSave_Click(object sender, EventArgs e)
    {
        try
        {
            string id = Request["id"];

            Eucalypto.Wiki.Category category;

            //Edit
            if (id != null)
            {
                category = Eucalypto.Wiki.WikiManager.GetCategory(id);

                category.DisplayName = txtDisplayName.Text;

            }
            else //New
            {
                category = Eucalypto.Wiki.WikiManager.CreateCategory(txtName.Text, txtDisplayName.Text);
            }

            category.EditPermissions = txtEditPermissions.Text;
            category.ReadPermissions = txtReadPermissions.Text;
            category.DeletePermissions = txtDeletePermissions.Text;
            category.InsertPermissions = txtInsertPermissions.Text;
            category.ApprovePermissions = txtApprovePermissions.Text;
            category.Description = txtDescription.Text;
            category.XHtmlMode = (Eucalypto.XHtmlMode)Enum.Parse(typeof(Eucalypto.XHtmlMode), cmbXhtmlMode.SelectedValue);
            category.BackupMode = (Eucalypto.WikiBackupMode)Enum.Parse(typeof(Eucalypto.WikiBackupMode), cmbBackupMode.SelectedValue);

            category.AutoApprove = chkAutoApprove.Checked;
            category.AttachEnabled = chkEnabledAttach.Checked;
            category.AttachExtensions = txtAttachExtensions.Text;
            category.AttachMaxSize = int.Parse(txtAttachMaxSize.Text);

            Eucalypto.Wiki.WikiManager.UpdateCategory(category);


            Navigation.Admin_WikiList().Redirect(this);

        }
        catch (Exception ex)
        {
            ((IErrorMessage)Master).SetError(GetType(), ex);
        }
    }

    protected void btCancel_Click(object sender, EventArgs e)
    {
        Navigation.Admin_WikiList().Redirect(this);
    }
}
