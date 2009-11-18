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

public partial class UserList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        linkNewItem.HRef = Navigation.Admin_UserDetailsNew().GetServerUrl(true);

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

            if (string.Equals(id, User.Identity.Name, StringComparison.InvariantCultureIgnoreCase))
                throw new ApplicationException("Cannot delete the current user");

            Membership.DeleteUser(id, true);

            LoadList();
        }
        else if (e.CommandName == "edit")
        {
            string id = (string)e.CommandArgument;

            Navigation.Admin_UserDetails(id).Redirect(this);
        }
    }

    private void LoadList()
    {
        MembershipUserCollection users = Membership.GetAllUsers();

        listRepeater.DataSource = users;
        listRepeater.DataBind();
    }

    protected string GetUserStatus(MembershipUser user)
    {
        if (user.IsApproved == false)
            return "NOT APPROVED";
        else if (user.IsLockedOut)
            return "LOCKED";
        else
            return "Active";
    }
}
