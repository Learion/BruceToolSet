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

public partial class Wiki_NewArticle : System.Web.UI.Page
{
    private string CategoryName
    {
        get { return Request["name"]; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Eucalypto.Wiki.Category category = Eucalypto.Wiki.WikiManager.GetCategoryByName(CategoryName, true);

        sectionApprove.Visible = !category.AutoApprove;
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        try
        {
            Eucalypto.Wiki.Category category = Eucalypto.Wiki.WikiManager.GetCategoryByName(CategoryName, true);

            if (Eucalypto.SecurityHelper.CanInsert(Page.User, category))
            {
                Eucalypto.Wiki.WikiManager.CreateArticle(category, User.Identity.Name, txtName.Text, txtTitle.Text, txtDescription.Text, null);
            }
            else
                throw new Eucalypto.InvalidPermissionException("insert an article");

            Navigation.Wiki_EditArticle(txtName.Text).Redirect(this);
        }
        catch (Exception ex)
        {
            ((IErrorMessage)Master).SetError(GetType(), ex);
        }
    }
    protected void btCancel_Click(object sender, EventArgs e)
    {
        Navigation.Wiki_ViewCategory(CategoryName).Redirect(this);
    }
}
