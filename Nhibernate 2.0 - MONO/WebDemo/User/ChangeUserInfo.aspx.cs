using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using WebDemo.code;

public partial class User_ChangeUserInfo : System.Web.UI.Page
{
    private const string DEFAULT_PAGE = "~/User/Default.aspx";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            MembershipUser user = Membership.GetUser(User.Identity.Name);
            if (user == null)
                throw new ApplicationException("User not found " + User.Identity.Name);

            txtEMail.Text = user.Email;
            chkReceiveNotification.Checked = (Boolean)HttpContext.Current.Profile.GetPropertyValue("ReceiveNotification");
            txtFavoriteColor.Text = HttpContext.Current.Profile.GetPropertyValue("FavoriteColor") as String;
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        try
        {
            MembershipUser user = Membership.GetUser(User.Identity.Name);
            if (user == null)
                throw new ApplicationException("User not found " + User.Identity.Name);

            user.Email = txtEMail.Text;

            Membership.UpdateUser(user);

            HttpContext.Current.Profile.SetPropertyValue("ReceiveNotification", chkReceiveNotification.Checked);

            HttpContext.Current.Profile.SetPropertyValue("FavoriteColor", txtFavoriteColor.Text);

            HttpContext.Current.Profile.Save();
            

            Response.Redirect(DEFAULT_PAGE);
        }
        catch (Exception ex)
        {
            ((IErrorMessage)Master).SetError(GetType(), ex);
        }
    }

    protected void btCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(DEFAULT_PAGE);
    }
}
