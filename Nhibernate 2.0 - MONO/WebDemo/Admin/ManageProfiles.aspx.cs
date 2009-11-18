using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Admin_ManageProfiles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            LoadList();
        }
    }

    private void LoadList()
    {
        cbType.Items.Clear();

        cbType.Items.Add(Eucalypto.Profile.ProfileType.Anonymous.ToString());
        cbType.Items.Add(Eucalypto.Profile.ProfileType.Authenticated.ToString());
    }

    protected void btDeleteProfiles_Click(object sender, EventArgs e)
    {
        System.Web.Profile.ProfileAuthenticationOption profileType;
        if (cbType.SelectedValue == Eucalypto.Profile.ProfileType.Anonymous.ToString())
            profileType = System.Web.Profile.ProfileAuthenticationOption.Anonymous;
        else if (cbType.SelectedValue == Eucalypto.Profile.ProfileType.Authenticated.ToString())
            profileType = System.Web.Profile.ProfileAuthenticationOption.Authenticated;
        else
            throw new ApplicationException("Invalid profile type");

        DateTime inactiveSince = DateTime.Now.AddDays(-int.Parse(txtInactiveSince.Text));
        System.Web.Profile.ProfileManager.DeleteInactiveProfiles(profileType, inactiveSince);
    }
}
