using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using SEOToolSet.Entities;
using SEOToolSet.Providers;
using SEOToolSet.WebApp.Helper;

namespace SEOToolSet.WebApp
{
    public partial class ManageProject : System.Web.UI.Page
    {
        public Int32 IdProject
        {
            get
            {
                int result;
                if (!Int32.TryParse(Request.QueryString["IdProject"], out result))
                {
                    result = ProfileHelper.SelectedIdProject;
                }

                //if (Providers.ProjectManager.UserIsAllowedInProject(result, Page.User.Identity.Name))
                //{
                //    ProfileHelper.SelectedIdProject = result;
                return result;
                //}
                //return -1;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["mode"] != null)
            {
                switch (Request.QueryString["mode"].ToString().ToLower())
                {
                    case "edit":
                        this.divProjectnav.Visible = true;
                        this.fvProject.ChangeMode(FormViewMode.Edit);
                        UpdatePanel2.Visible = true;
                        break;
                    default:
                        this.fvProject.ChangeMode(FormViewMode.Insert);
                        UpdatePanel2.Visible = false;
                        break;
                }
            }
            else
            {
                this.fvProject.ChangeMode(FormViewMode.Insert);
            }

            SEOToolsetUser user = SEOToolSet.Providers.SEOMembershipManager.GetUser(User.Identity.Name);
            this.PageTitle1.AccountInfo = user.FirstName + "&nbsp;" + user.LastName;

        }

        protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            Account account = new Account { Id = 1 };
            e.NewValues["Account"] = account;
        }

        protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            //TODO: Set value of projectName
            if (e.ReturnValue != null)
            {
                SEOToolSet.Entities.Project obj = e.ReturnValue as SEOToolSet.Entities.Project;

                ConfirmMessageExt1.ConfirmMessage = "Deleting a project stops all scheduled monitoring and prevents project users from accessing any data previously collected. Are you sure you want to delete the project " + obj.Name + "?";
            }
            else
            {
                this.lbtnDelete.Visible = false;
                //this.pnlEdit.Visible = false;
            }
        }

        protected void fvProject_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            //TODO: Set value of Project Account and CreateBy field
            e.Values["Account"] = SEOMembershipManager.GetUser(User.Identity.Name).Account;

            e.Values["CreateBy"] = User.Identity.Name;
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            //TODO: Remove this project,The de facto which change status of this project
            ProjectManager.DeleteProject(IdProject);
            Response.Redirect("ProjectDashboard.aspx");
        }

        protected void fvProject_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            //TODO: Set value of Project Account and UpdateBy field
            e.NewValues["Account"] = SEOMembershipManager.GetUser(User.Identity.Name).Account;

            e.NewValues["UpdateBy"] = User.Identity.Name;
        }

        protected void fvProject_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            //TODO: After update show message
            if (e.Exception == null)
            {
                Literal lbl = this.fvProject.FindControl("lblUpdateResult") as Literal;
                if (lbl != null)
                {
                    lbl.Visible=true;
                }
                ViewState["Time"] = 1;
                this.Timer1.Enabled = true;
            }
        }

        protected void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            //TODO:Must be unique within the account
            CustomValidator cv = this.fvProject.FindControl("CustomValidator1") as CustomValidator;
            cv.Validate();
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Entities.Project project = ProjectManager.GetProjectByAccountAndName(SEOMembershipManager.GetUser(User.Identity.Name).Account, new Entities.Project { Name = args.Value, Id = IdProject });
            if (project != null)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }

        }

        protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            //TODO: After save project changes to edit mode 
            Response.Redirect(String.Format("ManageProject.aspx?mode=edit&IdProject={0}", e.OutputParameters["id"].ToString()));
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //TODO: An animated Ajax element that fades away after five seconds
            if (ViewState["Time"] != null)
            {
                Literal lbl = this.fvProject.FindControl("lblUpdateResult") as Literal;
                if (lbl != null)
                {
                    if (Convert.ToInt32(ViewState["Time"]) == 5)
                    {
                        this.Timer1.Enabled = false;
                        lbl.Visible = false;

                    }
                    else
                    {
                        lbl.Visible = true;
                        ViewState["Time"] = Convert.ToInt32(ViewState["Time"]) + 1;
                    }
                }
            }
        }

        protected void LinkButtonDelete_Click(object sender, EventArgs e)
        {
            //TODO:Reset Project Information
            this.fvProject.DataBind();
        }

        protected void lbtnAddProject_Click(object sender, EventArgs e)
        {
            //TODO:Redirect manageProject page and mode is add
            Response.Redirect("ManageProject.aspx?mode=add");
        }

        protected void BulkLoadInserted(object sender, FormViewInsertedEventArgs e)
        {
            SiteMapHelper.ReloadSamePage(Page);
        }
    }
}
