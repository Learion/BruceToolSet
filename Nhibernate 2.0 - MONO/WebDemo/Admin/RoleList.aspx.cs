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

public partial class RoleList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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

            Roles.DeleteRole(id, true);

            LoadList();
        }
    }

    protected void btAdd_Click(object sender, EventArgs e)
    {
        try
        {
            Roles.CreateRole(txtName.Text);

            txtName.Text = string.Empty;
            LoadList();
        }
        catch (Exception ex)
        {
            ((IErrorMessage)Master).SetError(GetType(), ex);
        }
    }

    private void LoadList()
    {
        string[] roles = Roles.GetAllRoles();

        listRepeater.DataSource = roles;
        listRepeater.DataBind();
    }
}
