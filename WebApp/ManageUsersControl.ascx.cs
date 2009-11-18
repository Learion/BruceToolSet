using System;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEOToolSet.Providers;
using SEOToolSet.WebApp.Helper;

namespace SEOToolSet.WebApp
{
    public partial class ManageUsersControl : UserControl
    {
        public bool ShowAdvanced { get; set; }

        public Int32 IdProject { get; set; }

        public event EventHandler UserAddedToProject;

        protected void Page_Load(object sender, EventArgs e)
        {
            ConfirmMessageExt1.Selector = string.Format("#{0} a.CommandDelete", panelUsers.ClientID);
            //By no reason MONO does not bind the Message when is put in the ascx page. at least MONO 2.2
            //maybe MONO 2.4 could handle it good.
            if (R3M.Controls.Common.IsMono)
            {
                ConfirmMessageExt1.ConfirmMessage = Resources.CommonTerms.RemoveUserFromProject;
            }
        }

        private void SetValidationGroup(Control item)
        {
            var dropdown = item.FindControl("DropDownListUsersAvailable") as DropDownList;
            if (dropdown != null) dropdown.ValidationGroup = String.Format("{0}_AddUser", ClientID);

            var requiredFieldValidator1 = item.FindControl("RequiredFieldValidator1") as RequiredFieldValidator;
            if (requiredFieldValidator1 != null) requiredFieldValidator1.ValidationGroup = String.Format("{0}_AddUser", ClientID);

            var lnkAdd = item.FindControl("LnkAdd") as LinkButton;
            if (lnkAdd != null) lnkAdd.ValidationGroup = String.Format("{0}_AddUser", ClientID);
        }

        protected override void OnInit(EventArgs e)
        {
            panelUsers.Attributes["class"] = ShowAdvanced ? "Advanced" : "Simple";
        }

        public void RefreshPanel()
        {
            CustomRepeaterProjectUsers.DataBind();
        }


        protected void lnkAddProjectUser_OnClick(object sender, EventArgs e)
        {

            var lnkControl = CustomRepeaterProjectUsers.Controls[0].FindControl("LnkAddProjectUser") as LinkButton;
            if (lnkControl != null) lnkControl.Visible = false;


            var panel1 = CustomRepeaterProjectUsers.Controls[0].FindControl("panel1") as Panel;
            if (panel1 != null) panel1.Visible = true;

            var lnkAdd = CustomRepeaterProjectUsers.Controls[0].FindControl("LnkAdd") as LinkButton;
            if (lnkAdd == null) return;
            lnkAdd.Visible = true;
            lnkAdd.Text = String.Format("<span>[ {0} ]</span>", Resources.CommonTerms.Add);
        }

        protected void lnkCancelAdd_OnClick(object sender, EventArgs e)
        {
            revertToAdd();
        }

        private void revertToAdd()
        {
            var lnkControl = CustomRepeaterProjectUsers.Controls[0].FindControl("LnkAddProjectUser") as LinkButton;
            if (lnkControl != null) lnkControl.Visible = true;
            var panel1 = CustomRepeaterProjectUsers.Controls[0].FindControl("panel1") as Panel;
            if (panel1 != null) panel1.Visible = false;
        }

        protected void lnkAdd_OnClick(object sender, EventArgs e)
        {
            var dropdown = CustomRepeaterProjectUsers.Controls[0].FindControl("DropDownListUsersAvailable") as DropDownList;
            if (dropdown == null) return;
            Int32 id;
            ProjectManager.AddUserToProject(out id, dropdown.SelectedItem.Text, "Member", ProjectManager.GetProjectById(IdProject));
            if (id == -1) return;
            if (UserAddedToProject != null)
            {
                UserAddedToProject(this, new EventArgs());
            }
        }

        public void CustomRepeaterProjectUsers_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.CommandName != "DoDelete" || e.CommandArgument == null) return;

            var currentUser = SEOMembershipManager.GetUser(HttpContext.Current.User.Identity.Name);
            var idUserProject = Int32.Parse(e.CommandArgument.ToString());
            if (currentUser.Id != idUserProject)
                ProjectManager.RemoveProjectUserById(idUserProject);
            else
            {
                var scrp = ScriptManager.GetCurrent(Page);
                if (scrp != null && scrp.IsInAsyncPostBack)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "scriptUserNotRemovable", string.Format("$(function () {{ setTimeout(function() {{  $.showMessage('{0}'); }}); }}, 500);  ", GetLocalResourceObject("YouCannotDeleteYourSelfFromProject")), true);
                }

            }


            if (UserAddedToProject != null)
            {
                UserAddedToProject(this, new EventArgs());
            }

        }



        protected void CustomRepeaterProjectUsers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                SetValidationGroup(e.Item);
            }

            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;

            var lnkRemoveUser = e.Item.FindControl("LinkButtonRemove") as LinkButton;

            if (lnkRemoveUser == null) return;

            var currentNode = SiteMapHelper.GetCurrentNode();

            lnkRemoveUser.Enabled = (currentNode != null) &&
                                    (PermissionHelper.GetPermissionModeFromNode(currentNode) == PermissionMode.Execute);

            lnkRemoveUser.CssClass = String.Format("button CommandDelete {0}", lnkRemoveUser.Enabled ? "" : "disabled");

        }



        protected void OnSelecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (IdProject == -1) e.Cancel = true;
        }
    }
}