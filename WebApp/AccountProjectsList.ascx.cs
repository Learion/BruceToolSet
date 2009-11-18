using System;
using System.Web.UI.WebControls;
using SEOToolSet.Providers;
using SEOToolSet.WebApp.Events;


namespace SEOToolSet.WebApp
{
    public partial class AccountProjectsList : System.Web.UI.UserControl
    {
        public Int32 IdAccount
        {
            get
            {
                return Convert.ToInt32(ViewState["IdAccount"]);

            }
            set
            {
                ViewState["IdAccount"] = value;
            }
        }

        public bool DisplayTitle
        {
            get
            {
                return AccountProjectsTitle.Visible;
            }
            set
            {
                AccountProjectsTitle.Visible = value;
            }
        }

        public event EventHandler<ProjectLaunchClickArgs> ProjectLaunchClick;

        public event EventHandler ProjectDeleted;

        public event EventHandler ProjectAdded;

        private void InvokeProjectAdded(EventArgs e)
        {
            var added = ProjectAdded;
            if (added != null) added(this, e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void odsProject_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            Int32 idProjectUser;
            //Retrieving the id for the inserted project
            var idProject = int.Parse(e.OutputParameters["id"].ToString());
            var project = ProjectManager.GetProjectById(idProject);

            //By default the current user will be the project administrator for this project
            ProjectManager.AddUserToProject(out idProjectUser, Page.User.Identity.Name, "ProjectAdministrator", project);
            Helper.ProfileHelper.SelectedIdProject = idProject;

            InvokeProjectAdded(new EventArgs());

            /*((BC)Page.Master).UpdateProjectSelector();
            crAccountProjects.DataBind();*/

        }

        public void RefreshProjects()
        {
            crAccountProjects.DataBind();
        }

        protected void LinkButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (RepeaterItem item in crAccountProjects.Items)
                {
                    if (!((CheckBox)item.FindControl("chk")).Checked)
                        continue;
                    var hdnIdProject = item.FindControl("hiddenFieldIdProject") as HiddenField;
                    if (hdnIdProject != null)
                        ProjectManager.DeleteProject(Convert.ToInt32(hdnIdProject.Value));
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "DeletionError", String.Format("alert('{0}');", Resources.JavascriptMessages.ProjectDeletionError), true);
                LoggerFacade.Log.LogException(GetType(), ex);
            }
            crAccountProjects.DataBind();

            if (ProjectDeleted != null)
            {
                ProjectDeleted(this, new EventArgs());
            }

        }

        protected void odsAccountProject_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["account"] = AccountManager.GetAccount(IdAccount);
        }

        protected void odsProject_OnInserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["account"] = AccountManager.GetAccount(IdAccount);
        }

        protected void crAccountProjects_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName != "Launch")
                return;
            RaiseProjectLaunchClick(Convert.ToInt32(e.CommandArgument));
        }

        public void RaiseProjectLaunchClick(Int32 idProject)
        {
            if (ProjectLaunchClick != null)
            {
                ProjectLaunchClick(this, new ProjectLaunchClickArgs { IdProject = idProject });
            }
        }
    }
}