using System;
using System.Web.UI.WebControls;
using R3M.Controls;
using SEOToolSet.Common;
using SEOToolSet.WebApp.Helper;


namespace SEOToolSet.WebApp
{
    public partial class Header : System.Web.UI.UserControl
    {

        protected void Page_Load(Object sender, EventArgs e)
        {
            if (!Page.User.Identity.IsAuthenticated) return;

            if (!IsPostBack)
                SetLoggedInUserProfileUrl();
        }

        private void SetLoggedInUserProfileUrl()
        {
            var linkUserAccount = LoginView1.FindControl("lnkUserAcount") as LinkButton;
            if (linkUserAccount == null) return;

            var user = Providers.SEOMembershipManager.GetUser(Page.User.Identity.Name);
            if (user == null) return;
            linkUserAccount.PostBackUrl += "?IdUser=" + user.Id;
        }

        public void UpdateProjectSelectorDropdownList()
        {
            DropDownCurrentDomain.Items.Clear();
            DropDownCurrentDomain.DataBind();
        }

        protected void odsProject_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["username"] = Page.User.Identity.Name;
        }

        protected void DropDownCurrentDomain_DataBound(object sender, EventArgs e)
        {
            //If not authenticated 
            if (!Page.User.Identity.IsAuthenticated)
                return;


            if (DropDownCurrentDomain.Items.Count > 0 && ProfileHelper.SelectedIdProject == 0)
                ProfileHelper.SelectedIdProject = Int32.Parse(DropDownCurrentDomain.Items[0].Value);

            //adding AddNewProject Item
            if (DropDownCurrentDomain.Items.FindByValue("-1") == null)
            {
                DropDownCurrentDomain.Items.Add(new ListItem(String.Format("[ {0} ]", Resources.CommonTerms.AddNewProject), "-1"));
            }

            DropDownCurrentDomain.ClearSelection();
            var itemSelected = DropDownCurrentDomain.Items.FindByValue(ProfileHelper.SelectedIdProject.ToString());
            if (itemSelected == null)
            {
                //The Project was not in the current Dropdown
                if (DropDownCurrentDomain.Items.Count > 0)
                {
                    var idProject = Convert.ToInt32(DropDownCurrentDomain.Items[0].Value);
                    if (idProject > -1)
                        ProfileHelper.SelectedIdProject = idProject;
                }

                return;
            }
            DropDownCurrentDomain.SelectedValue = itemSelected.Value;
        }

        protected void LoginStatus_OnLoggedOut(object sender, EventArgs e)
        {
            if (Session != null) Session.Abandon();
            WebHelper.RedirectToLoginPage();
        }

        protected void AjaxCallback1_ScriptCallback(object sender, CallbackEventArgs e)
        {
            try
            {
                ProfileHelper.SelectedIdProject = DropDownCurrentDomain.SelectedIndex >= 0
                                                 ? Int32.Parse(DropDownCurrentDomain.SelectedValue)
                                                 : -1;

                e.Result = "true";
            }
            catch (Exception ex)
            {
                LoggerFacade.Log.Debug(GetType(), ex.Message);
            }
        }
    }
}
