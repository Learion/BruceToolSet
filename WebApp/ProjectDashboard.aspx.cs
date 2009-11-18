using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SEOToolSet.WebApp.Helper;
using SEOToolSet.Providers;
using SEOToolSet.Providers.MySql;
using System.Collections.Generic;
using SEOToolSet.Entities;

namespace SEOToolSet.WebApp
{
    public partial class ProjectDashboard1 : System.Web.UI.Page
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

                if (Providers.ProjectManager.UserIsAllowedInProject(result, Page.User.Identity.Name))
                {
                    ProfileHelper.SelectedIdProject = result;
                    return result;
                }
                return -1;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ViewState["idaccount"] = "1";
                ViewState["iduser"] = "1";
            }
        }
        protected void BindProjectInfor(string id)
        {
            //this.labAccount.Text = "";

            //string str = AccountControl.GetAccountInfor(ViewState["idaccount"].ToString(), ViewState["iduser"].ToString());
            //string[] tem = str.Split('*');
            //this.labSubscriptionLevel.Text = tem[0];
            //this.labAdinistrator.Text = tem[1];

        }
        protected void BindState()
        {
            //this.drpState.Items.Add(new ListItem("Active"));
            //this.drpState.Items.Add(new ListItem("Inactive"));
            //this.drpState.Items.Add(new ListItem("All"));
        }

        protected void BindprojetCount()
        {
            //string str = AccountControl.GetAccountProjectInfor(ViewState["idaccount"].ToString(), ViewState["iduser"].ToString());
            //if ("0" != str)
            //{
            //    string[] tem = str.Split('*');
            //    this.labProjects.Text = tem[0];
            //    this.labAccountProj.Text = tem[1];
            //}
            //else
            //{
            //    this.labProjects.Text = Define.project;
            //    this.labAccountProj.Text = Define.AccountProject;
            //}


        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewProjectPage.aspx");
        }
        protected void bind()
        {
            //SEOToolSet.Entities.Project pj = MySqlAccountProvider.GetAccountInfort(ViewState["idaccount"].ToString(),
            //                                                    ViewState["iduser"].ToString(),
            //                                                    drplist.SelectedValue.Trim());
            //if (null != pj)
            //{
            //    this.labSubscriptionLevel.Text = "SEOToolSet " + pj.Account.SubscriptionLevelText;
            //    //this.labAdinistrator.Text = "John Smith,Johnny lin";
            //    if (pj.Account.CreatedBy == "")
            //        labAdinistrator.Text = pj.Account.UpdatedBy;
            //    else if (pj.Account.UpdatedBy == "")
            //        labAdinistrator.Text = pj.Account.CreatedBy;
            //    else
            //        labAdinistrator.Text = pj.Account.CreatedBy + "," + pj.Account.UpdatedBy;
            //    labAccount.Text = pj.Account.Name;
            //    labName.Text = pj.ClientName;
            //    labdom.Text = pj.Domain;
            //    labConnect.Text = string.Format("<a href='mailto:{0}'>{1}</a>",
            //        pj.ContactEmail,
            //        pj.Name);
            //}

        }

        protected void drplist_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindData();
        }
        protected void bindData()
        {
            bind();
            keyword();
            Statistics();
        }
        protected void keyword()
        {
            //List<object> obj = MySqlAccountProvider.GetKeywords(drplist.SelectedValue);

            //this.labKeywords.Text = obj[0].ToString();
            //this.labKeywords.ToolTip = obj[0].ToString();
            //this.labKeyList.Text = obj[1].ToString();
            //this.labKeyList.ToolTip = obj[1].ToString();
        }
        protected void Statistics()
        {
            //List<object> obj = MySqlRankingmonitorProvider.GetStatistics(drplist.SelectedValue);
            //if (null != obj)
            //{
            //    labPageRank.Text = obj[0].ToString();
            //    labLinks.Text = obj[1].ToString();
            //    labIndexed.Text = obj[2].ToString();
            //    labAs.Text = obj[3].ToString();
            //}
        }

        protected void OnUserAdded(object sender, EventArgs e)
        {

        }

        protected void LnkRefreshControl_OnClick(object sender, EventArgs e)
        {

        }

        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                e.InputParameters["username"] = User.Identity.Name;
            }
        }


        //protected void BulkLoadInserted(object sender, FormViewInsertedEventArgs e)
        //{
        //    SiteMapHelper.ReloadSamePage(Page);
        //}

    }
}