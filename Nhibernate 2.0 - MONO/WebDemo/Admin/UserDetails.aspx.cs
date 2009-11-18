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

public partial class UserDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            string id = Request["id"];

            //Edit
            if (id != null)
            {
                txtName.Enabled = false;

                MembershipUser user = Membership.GetUser(id);
                if (user == null)
                    throw new ApplicationException("User not found " + id);

                txtName.Text = user.UserName;
                txtEMail.Text = user.Email;
                chkApproved.Checked = user.IsApproved;
                chkLocked.Checked = user.IsLockedOut;

                if (user.IsLockedOut == false)
                    chkLocked.Enabled = false;
                else
                    chkLocked.Enabled = true;

                LoadRoles(id);
            }
            else //New
            {
                lblHelpPassword.Visible = false;
                chkLocked.Enabled = false;
                chkApproved.Enabled = false;

                LoadRoles(null);
            }
        }
    }

    /// <summary>
    /// Load the user roles
    /// </summary>
    /// <param name="user">null for new user</param>
    private void LoadRoles(string user)
    {
        string[] roles = Roles.GetAllRoles();
        chkListRoles.Items.Clear();

        foreach (string role in roles)
        {
            ListItem listItem = new ListItem(role);

            //I f the user is of the specified role or the role is equal to the default role for new user
            if ( user != null && Roles.IsUserInRole(user, role) )
                listItem.Selected = true;

            chkListRoles.Items.Add(listItem);
        }
    }

    private string[] GetSelectedRoles()
    {
        List<string> roles = new List<string>();

        foreach (ListItem item in chkListRoles.Items)
        {
            if (item.Selected)
                roles.Add(item.Text);
        }

        return roles.ToArray();
    }

    /// <summary>
    /// Returns all the new roles
    /// </summary>
    /// <returns></returns>
    private string[] GetNewRoles(string user)
    {
        List<string> newRoles = new List<string>();
        foreach (string selRole in GetSelectedRoles())
        {
            if (Roles.IsUserInRole(user, selRole) == false)
                newRoles.Add(selRole);
        }

        return newRoles.ToArray();
    }

    /// <summary>
    /// Get the roles not more selected
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private string[] GetDeletedRoles(string user)
    {
        string[] existingRoles = Roles.GetRolesForUser(user);
        string[] selectedRoles = GetSelectedRoles();

        List<string> delRoles = new List<string>();
        foreach (string existingRole in existingRoles)
        {
            bool match = false;
            foreach (string selRole in selectedRoles)
            {
                if (string.Equals(selRole, existingRole, StringComparison.InvariantCultureIgnoreCase))
                {
                    match = true;
                    break;
                }
            }

            if (match == false)
                delRoles.Add(existingRole);
        }

        return delRoles.ToArray();
    }

    private void RedirectToListPage()
    {
        Response.Redirect("UserList.aspx");
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        try
        {
            string id = Request["id"];

            //Edit
            if (id != null)
            {
                MembershipUser user = Membership.GetUser(id);
                if (user == null)
                    throw new ApplicationException("User not found " + id);

                if (txtPassword.Text.Length > 0)
                {
                    //ASP.NET Membership cannot change a user password if you don't provide the old passowrd.
                    // But I need an admin features that enable to reset password but providing your own password.
                    string oldPwd = user.ResetPassword();
                    //Note: this method release the fields of the user so I must change the email and the other fields after this call
                    bool success = user.ChangePassword(oldPwd, txtPassword.Text);
                    if (success == false)
                        throw new ApplicationException("Failed to change user password.");
                }

                user.Email = txtEMail.Text;
                user.IsApproved = chkApproved.Checked;

                Membership.UpdateUser(user);

                if (user.IsLockedOut && chkLocked.Checked == false)
                    user.UnlockUser();
            }
            else //New
            {
                Membership.CreateUser(txtName.Text, txtPassword.Text, txtEMail.Text);
            }

            //Delete roles (remove the roles no more selected)
            string[] delRoles = GetDeletedRoles(txtName.Text);
            if (delRoles.Length > 0)
                Roles.RemoveUserFromRoles(txtName.Text, delRoles);
            //Add roles (add the new role selected)
            string[] newRoles = GetNewRoles(txtName.Text);
            if (newRoles.Length > 0)
                Roles.AddUserToRoles(txtName.Text, newRoles);

            RedirectToListPage();

        }
        catch (Exception ex)
        {
            ((IErrorMessage)Master).SetError(GetType(), ex);
        }
    }

    protected void btCancel_Click(object sender, EventArgs e)
    {
        RedirectToListPage();
    }

}
