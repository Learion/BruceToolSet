#region

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEOToolSet.Entities;
using SEOToolSet.Providers;
using System.Web.Security;
using SEOToolSet.WebApp.Helper;

#endregion

namespace SEOToolSet.WebApp
{

    public partial class ManageAccountUsers : Page
    {
        private string _lastCreatedtUserEmail;
        private string _lastCreatedtUserLogin;

        protected Account CurrentAccount
        {
            get { return User.Identity.IsAuthenticated ? SEOMembershipManager.GetUser(User.Identity.Name).Account : null; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (CurrentAccount != null)
                PageTitle1.PageDescription = PageTitle1.PageDescription.Replace("[ACCOUNT_NAME]", CurrentAccount.Name);
        }

        protected void DoBindData(object sender, EventArgs e)
        {   /*
            FormView1.ChangeMode(FormViewMode.Edit);
            odsSEOMembership.SelectParameters["id"].DefaultValue = UserId.Text;
            odsSEOMembership.Select();
            FormView1.DataBind();*/
        }

        protected void DoBindGrid(object sender, EventArgs e)
        {
            /*
            odsSeoMembershipUsers.SelectParameters["fieldName"].DefaultValue = SortBy.Text;
            odsSeoMembershipUsers.Select();
            CustomRepeater1.DataBind();
             * */
        }

        protected void odsSeoMembershipUsers_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["account"] = CurrentAccount;
            e.InputParameters["fieldName"] = SortBy ?? "FirstName";
            e.InputParameters["asc"] = (SortDirection == null) ? true : (SortDirection == "ASC") ? true : false;
        }

        ///<summary>
        ///Returns the user role name
        ///</summary>
        ///<param name="userRole">The object that represents the role of the user in the
        ///account</param>
        ///<returns>The name of the role of the user in the account</returns>
        protected static string GetUserRoleName(object userRole)
        {
            return userRole == null ? null : ((Role)userRole).Name;
        }

        ///<summary>
        ///Returns the number of projects the user is involved
        ///</summary>
        ///<returns>The number of project</returns>
        protected static int GetProjectsInvolved(string loginName, object account)
        {
            return ProjectManager.GetProjectsForUserWithinAccount(loginName, account as Account, false).Count;
        }

        protected void UsersSelected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            CollectionPager1.TotalItemCount = Convert.ToInt32(e.OutputParameters["count"]);
        }

        public string SortBy
        {
            get { return ViewState["SortBy"] as String; }
            set { ViewState["SortBy"] = value; }
        }

        public string SortDirection
        {
            get { return ViewState["SortDirection"] as String; }
            set { ViewState["SortDirection"] = value; }
        }

        public FormViewUpdateEventArgs E { get; set; }


        protected void OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                /*case "CustomSelect":
                    CurrentSelectedId = (String)e.CommandArgument;
                    FormView1.ChangeMode(FormViewMode.Edit);
                    FormView1.DataBind();
                    break;*/
                case "CustomSort":

                    SortDirection = SortBy == (string)e.CommandArgument
                                        ? (SortDirection == "ASC" ? "DESC" : "ASC")
                                        : "ASC";
                    SortBy = (string)e.CommandArgument;
                    CollectionPager1.CurrentPageIndex = 0;
                    _gridviewUsers.DataBind();
                    break;
            }

        }

        public String EvalSortBy(string firstname)
        {
            return SortBy == firstname ? SortDirection : String.Empty;
        }

        protected void OnInserting(object sender, ObjectDataSourceMethodEventArgs e)
        {

        }

        protected void OnSelecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            /*try
            {
                e.InputParameters["id"] = CurrentSelectedId == null ? -1 : Int32.Parse(CurrentSelectedId);
            }
            catch (Exception ex)
            {
                LoggerFacade.Log.LogException(GetType(), ex);
            } */
        }

        protected void Formview1_OnDataBound(object sender, EventArgs e)
        {

        }

        protected void FormView1_OnItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            var formview = sender as FormView;
            if (formview == null)
                return;
            var countrySelection = formview.FindControl("DropDownListCountry") as DropDownList;
            if (countrySelection != null)
            {
                int idCountry;
                if (int.TryParse(countrySelection.SelectedValue, out idCountry))
                {
                    if (idCountry > 0)
                        e.NewValues["Country"] = new Country { Id = idCountry };
                }
            }
            var roleSelection = formview.FindControl("DropDownListRoles") as DropDownList;
            if (roleSelection == null) return;

            int idRole;
            if (!int.TryParse(roleSelection.SelectedValue, out idRole)) return;

            if (idRole > 0)
                e.NewValues["UserRole"] = new Role { Id = idRole };
        }

        protected void DoAddNewUser(object sender, EventArgs e)
        {
            FormView1.ChangeMode(FormViewMode.Insert);
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["user_id"] = ((SEOToolsetUser)e.Row.DataItem).Id.ToString();
            }
        }

        protected void RefreshFormView(object sender, EventArgs e)
        {
            FormView1.ChangeMode(FormViewMode.Edit);
            FormView1.DataBind();
        }

        protected void FormView1_OnItemInserting(object sender, FormViewInsertEventArgs e)
        {

            _lastCreatedtUserEmail = String.Empty;
            _lastCreatedtUserLogin = String.Empty;

            var formview = sender as FormView;
            if (formview == null)
                return;
            var countrySelection = formview.FindControl("DropDownListCountry") as DropDownList;
            if (countrySelection != null)
            {
                int idCountry;
                if (int.TryParse(countrySelection.SelectedValue, out idCountry))
                {
                    if (idCountry > 0)
                        e.Values["Country"] = new Country { Id = idCountry };
                }
            }
            var roleSelection = formview.FindControl("DropDownListRoles") as DropDownList;
            if (roleSelection == null) return;

            int idRole;
            if (!int.TryParse(roleSelection.SelectedValue, out idRole)) return;

            if (idRole > 0)
                e.Values["UserRole"] = new Role { Id = idRole };

            //TODO: Add checks for a valid email|   |
            var email = formview.FindControl("EmailTextBox") as TextBox;
            if (email != null) _lastCreatedtUserEmail = email.Text;

            var login = formview.FindControl("LoginTextBox") as TextBox;
            if (login != null) _lastCreatedtUserLogin = login.Text;

            e.Values["account"] = CurrentAccount;

        }



        protected void _odsSEOMembership_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                if (e.Exception.InnerException is MembershipCreateUserException)
                {
                    //write code to handle exceptions.

                    return;
                }
                return;
            }
            var status = e.OutputParameters["status"] is MembershipCreateStatus ? (MembershipCreateStatus)e.OutputParameters["status"] : MembershipCreateStatus.ProviderError;
            if (status == MembershipCreateStatus.Success)
            {
                var idUser = (Int32)e.OutputParameters["id"];
                IdUserSelected.Value = idUser.ToString();
                /*FormView1.DataBind();*/
                //send Password to the user email provided...
                var password = e.OutputParameters["password"] as string;
                if (CurrentAccount != null) Mailer.SendCreateUserEmail(_lastCreatedtUserEmail, CurrentAccount.Name, _lastCreatedtUserLogin, password);
                FormView1.ChangeMode(FormViewMode.Edit);
                FormView1.DefaultMode = FormViewMode.Edit;

                ScriptManager.RegisterStartupScript(this, GetType(), "refreshGrid", "refreshGrid();", true);
            }
            {
                //Handle Error Conditions here
                FormView1.ChangeMode(FormViewMode.ReadOnly);
            }
            FormView1.DataBind();
        }

        protected void Formview_DoCancel(object sender, EventArgs e)
        {
            IdUserSelected.Value = "-1";
            FormView1.ChangeMode(FormViewMode.ReadOnly);
            FormView1.DataBind();
        }
        protected void RefreshGrid(object sender, EventArgs e)
        {
            CollectionPager1.CurrentPageIndex = 0;
            _gridviewUsers.DataBind();
        }
    }
}