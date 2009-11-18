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

public partial class News_ViewCategory : System.Web.UI.Page
{
    private string CategoryName
    {
        get { return Request["name"]; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Eucalypto.News.Category category = Eucalypto.News.NewsManager.GetCategoryByName(CategoryName, true);

        if (Eucalypto.SecurityHelper.CanRead(Page.User, category, null) == false)
            throw new Eucalypto.InvalidPermissionException("read category");

        list.CategoryName = category.Name;

        lblDisplayName.InnerText = category.DisplayName;
        lblDescription.InnerText = category.Description;
    

        HtmlLink link = new HtmlLink();
        link.Href = Navigation.News_CategoryRss(CategoryName).GetServerUrl(true);
        link.Attributes.Add("rel", "alternate");
        link.Attributes.Add("type", "application/rss+xml");
        link.Attributes.Add("title", "Category " + category.DisplayName + " News");
        Header.Controls.Add(link);

        linkNew.HRef = Navigation.News_NewItem(CategoryName).GetServerUrl(true);

        linkRss.HRef = Navigation.News_CategoryRss(CategoryName).GetServerUrl(true);

        linkNew.Visible = Eucalypto.SecurityHelper.CanInsert(Page.User, category);
    }

}
