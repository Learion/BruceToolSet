using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using WebDemo.code;

public partial class WikiList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        linkNewItem.HRef = Navigation.Admin_WikiDetailsNew().GetServerUrl(true);
        linkShowWiki.HRef = Navigation.Wiki_Default().GetServerUrl(true);

        if (!IsPostBack)
        {
            LoadList();
        }
    }

    protected void listRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            string id = (string)e.CommandArgument;

            Eucalypto.Wiki.Category category = Eucalypto.Wiki.WikiManager.GetCategory(id);
            Eucalypto.Wiki.WikiManager.DeleteCategory(category);

            LoadList();
        }
        else if (e.CommandName == "edit")
        {
            string id = (string)e.CommandArgument;

            Navigation.Admin_WikiDetails(id).Redirect(this);
        }
    }

    private void LoadList()
    {
        IList<Eucalypto.Wiki.Category> list = Eucalypto.Wiki.WikiManager.GetAllCategories();

        listRepeater.DataSource = list;
        listRepeater.DataBind();
    }
}
