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

public partial class NewsDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            string id = Request["id"];

            //Edit
            if (id != null)
            {
                Eucalypto.News.Category category = Eucalypto.News.NewsManager.GetCategory(id);

                txtName.Enabled = false;

                txtName.Text = category.Name;
                txtDisplayName.Text = category.DisplayName;
                txtEditPermissions.Text = category.EditPermissions;
                txtReadPermissions.Text = category.ReadPermissions;
                txtDeletePermissions.Text = category.DeletePermissions;
                txtInsertPermissions.Text = category.InsertPermissions;
                txtDescription.Text = category.Description;

            }
            else //New
            {
                txtEditPermissions.Text = Eucalypto.SecurityHelper.NONE;
                txtReadPermissions.Text = Eucalypto.SecurityHelper.ALL_USERS;
                txtDeletePermissions.Text = Eucalypto.SecurityHelper.NONE;
                txtInsertPermissions.Text = Eucalypto.SecurityHelper.AUTHENTICATED_USERS;
            }
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        try
        {
            string id = Request["id"];

            Eucalypto.News.Category category;

            //Edit
            if (id != null)
            {
                category = Eucalypto.News.NewsManager.GetCategory(id);

                category.DisplayName = txtDisplayName.Text;

            }
            else //New
            {
                category = Eucalypto.News.NewsManager.CreateCategory(txtName.Text, txtDisplayName.Text);
            }

            category.EditPermissions = txtEditPermissions.Text;
            category.ReadPermissions = txtReadPermissions.Text;
            category.DeletePermissions = txtDeletePermissions.Text;
            category.InsertPermissions = txtInsertPermissions.Text;
            category.Description = txtDescription.Text;

            Eucalypto.News.NewsManager.UpdateCategory(category);


            Navigation.Admin_NewsList().Redirect(this);

        }
        catch (Exception ex)
        {
            ((IErrorMessage)Master).SetError(GetType(), ex);
        }
    }

    protected void btCancel_Click(object sender, EventArgs e)
    {
        Navigation.Admin_NewsList().Redirect(this);
    }
}
