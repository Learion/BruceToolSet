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
    public partial class XiaoLangTest : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["mode"] != null)
            {
                switch (Request.QueryString["mode"].ToString().ToLower())
                {
                    case "edit":
                        this.divProject.Visible = true;
                        this.fvProject.ChangeMode(FormViewMode.Edit);

                        break;
                    default:

                        this.fvProject.ChangeMode(FormViewMode.Insert);

                        break;
                }
            }

        }

        protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            Account account = new Account { Id = 1 };
            e.NewValues["Account"] = account;
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
