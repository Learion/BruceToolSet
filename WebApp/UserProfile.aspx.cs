using System;
using System.Web.UI.WebControls;
using SEOToolSet.Common;
using SEOToolSet.WebApp.Helper;

namespace SEOToolSet.WebApp
{
    public partial class UserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            var user = Providers.SEOMembershipManager.GetUser(Page.User.Identity.Name);
            var isEmptyIdUserParameter = (Request["IdUser"] == null);
            if (isEmptyIdUserParameter && user != null)
            {
                odsUser.SelectParameters["id"].DefaultValue = user.Id.ToString();
                LoggerFacade.Log.Debug(GetType(), "DefaultValue for odsUser is " + odsUser.SelectParameters["id"].DefaultValue);
            }
            if ((isEmptyIdUserParameter || User.IsInRole("Administrator")) ||
                ((user != null) && (user.Id.ToString() == Request["IdUser"])))
            {
                var displayPasswordSection = "$(function(){$('.Password').attr('style','display: block');});";
                var passwordTextBox = UserFormView.FindControl("PasswordTextBox") as TextBox;
                var retypePasswordTextBox = UserFormView.FindControl("RetypePasswordTextBox") as TextBox;
                ClientScript.RegisterClientScriptBlock(GetType(), "PasswordVisible", displayPasswordSection, true);
                if (passwordTextBox != null) passwordTextBox.ValidationGroup = string.Empty;
                if (retypePasswordTextBox != null) retypePasswordTextBox.ValidationGroup = string.Empty;
            }
            else
            {
                if (((User.IsInRole("Administrator") || User.IsInRole("TechSupportUser"))
                     && ((user != null) && user.Id.ToString() != Request["IdUser"]))) return;
                WebHelper.RedirectTo(SiteMapHelper.HomePageUrl);
            }
        }

        protected void odsUser_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            var countryControl = UserFormView.FindControl("CountryDropDownList") as DropDownList;
            var userRoleControl = UserFormView.FindControl("UserRoleDropDownList") as DropDownList;
            var user = Providers.SEOMembershipManager.GetUser(Page.User.Identity.Name);
            var password = UserFormView.FindControl("PasswordTextBox") as TextBox;
            if (countryControl != null && countryControl.SelectedIndex > 0)
                e.InputParameters["Country"] =
                    Providers.SEOMembershipManager.GetCountryById(int.Parse(countryControl.SelectedValue));
            if (userRoleControl != null && userRoleControl.SelectedIndex > 0)
                e.InputParameters["UserRole"] =
                    Providers.SEORolesManager.GetRoleById(int.Parse(userRoleControl.SelectedValue));
            if (e.InputParameters["Id"].ToString() != user.Id.ToString() && !User.IsInRole("Administrator")) return;
            if (password != null && password.Text.Trim().Length > 0) e.InputParameters["Password"] = password.Text;
        }

        protected void UpdateCancelButton_Click(object sender, EventArgs e)
        {
            Master.NavigateBack();
        }

        protected void odsUser_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            Master.NavigateBack();
        }
    }
}
