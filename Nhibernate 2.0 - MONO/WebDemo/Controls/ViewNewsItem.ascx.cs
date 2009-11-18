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
using WebDemo.code;

public partial class Controls_ViewNewsItem : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadNewsItem();

        linkEdit.Visible = EditLinkVisible;
        linkDelete.Visible = DeleteLinkVisible;
        linkEdit.HRef = Navigation.News_EditItem(ItemId).GetServerUrl(true);

        sectionActions.Visible = SectionActionsVisible;
    }

    #region Properties
    /// <summary>
    /// Gets or sets the item id to show. The value is saved in the ViewState.
    /// </summary>
    public string ItemId
    {
        get
        {
            object val = ViewState["ItemId"];
            return (string)val;
        }
        set { ViewState["ItemId"] = value; }
    }

    /// <summary>
    /// Gets or sets if show the edit link
    /// </summary>
    public bool EditLinkVisible
    {
        get
        {
            object val = ViewState["EditLinkVisible"];
            return val == null ? true : (bool)val;
        }
        set { ViewState["EditLinkVisible"] = value; }
    }

    /// <summary>
    /// Gets or sets if show the delete link
    /// </summary>
    public bool DeleteLinkVisible
    {
        get
        {
            object val = ViewState["DeleteLinkVisible"];
            return val == null ? true : (bool)val;
        }
        set { ViewState["DeleteLinkVisible"] = value; }
    }

    /// <summary>
    /// Gets or sets if show the actions section
    /// </summary>
    public bool SectionActionsVisible
    {
        get
        {
            object val = ViewState["SectionActionsVisible"];
            return val == null ? true : (bool)val;
        }
        set { ViewState["SectionActionsVisible"] = value; }
    }

    #endregion

    private void LoadNewsItem()
    {
        Eucalypto.News.Item item = Eucalypto.News.NewsManager.GetItem(ItemId);

        lblTitle.InnerText = item.Title;
        lblAuthor.InnerText = Utilities.GetDisplayUser(item.Author);
        lblDate.InnerText = Utilities.GetDateTimeForDisplay(item.NewsDate);
        linkUrl.HRef = item.URL;
        linkUrl.InnerText = item.URLName;

        sectionDescription.InnerHtml = Eucalypto.XHTMLText.FromPlainText(item.Description, Eucalypto.PlainTextMode.XHtmlConversion);
    }

    protected void MessageDelete_Click(object sender, EventArgs e)
    {
        Eucalypto.News.Item item = Eucalypto.News.NewsManager.GetItem(ItemId);
        Eucalypto.News.Category category = item.Category;

        if (Eucalypto.SecurityHelper.CanDelete(Page.User, item.Category, item))
        {
            Eucalypto.News.NewsManager.DeleteItem(item);

            Navigation.News_ViewCategory(item.Category.Name).Redirect(this);
        }
        else
            throw new Eucalypto.InvalidPermissionException("delete news item");
    }
}
