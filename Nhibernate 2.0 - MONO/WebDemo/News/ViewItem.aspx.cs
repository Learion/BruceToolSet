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

public partial class News_ViewItem : System.Web.UI.Page
{
    private string ItemId
    {
        get { return Request["item"]; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Eucalypto.News.Item item = Eucalypto.News.NewsManager.GetItem(ItemId);

        if (Eucalypto.SecurityHelper.CanRead(Page.User, item.Category, item) == false)
            throw new Eucalypto.InvalidPermissionException("read news");

        viewItem.ItemId = ItemId;

        viewItem.EditLinkVisible = Eucalypto.SecurityHelper.CanEdit(Page.User, item.Category, item);
        viewItem.DeleteLinkVisible = Eucalypto.SecurityHelper.CanDelete(Page.User, item.Category, item);
    }
}
