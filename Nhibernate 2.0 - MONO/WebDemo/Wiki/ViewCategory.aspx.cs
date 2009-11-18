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

public partial class Wiki_ViewCategory : System.Web.UI.Page
{
    private const int LIST_PAGING_SIZE = 10;

    private string CategoryName
    {
        get { return Request["name"]; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Eucalypto.Wiki.Category category = Eucalypto.Wiki.WikiManager.GetCategoryByName(CategoryName, true);

        if (Eucalypto.SecurityHelper.CanRead(Page.User, category, null) == false)
            throw new Eucalypto.InvalidPermissionException("read category");

        lblDisplayName.InnerText = category.DisplayName;
        lblDescription.InnerText = category.Description;

        HtmlLink link = new HtmlLink();
        link.Href = Navigation.Wiki_CategoryRss(CategoryName).GetServerUrl(true);
        link.Attributes.Add("rel", "alternate");
        link.Attributes.Add("type", "application/rss+xml");
        link.Attributes.Add("title", "Category " + category.DisplayName + " News");
        Header.Controls.Add(link);

        linkNew.HRef = Navigation.Wiki_NewArticle(CategoryName).GetServerUrl(true);

        linkRss.HRef = Navigation.Wiki_CategoryRss(CategoryName).GetServerUrl(true);

        linkNew.Visible = Eucalypto.SecurityHelper.CanInsert(Page.User, category);

        linkSearch.HRef = Navigation.Wiki_Search().GetServerUrl(true);

        LoadList(category);
    }

    private void LoadList(Eucalypto.Wiki.Category category)
    {
        //Get the standard articles
        IList<Eucalypto.Wiki.Article> articles = Eucalypto.Wiki.WikiManager.GetArticles(category, Eucalypto.Wiki.ArticleStatus.EnabledAndApproved);

        list.LoadList(articles);


        //My articles
        sectionMyArticles.Visible = false;
        if (User.Identity.IsAuthenticated)
        {
            IList<Eucalypto.Wiki.Article> myArticles = Eucalypto.Wiki.WikiManager.GetArticlesByOwner(category, User.Identity.Name,
                                                                            Eucalypto.Wiki.ArticleStatus.DisabledOrNotApproved);

            if (myArticles.Count > 0)
            {
                listMyArticles.LoadList(myArticles);
                sectionMyArticles.Visible = true;
            }
        }

        //Get not approved or disabled articles
        sectionNotApproved.Visible = false;
        if (User.Identity.IsAuthenticated && 
            Eucalypto.SecurityHelper.MatchPermissions(Page.User, category.ApprovePermissions))
        {
            IList<Eucalypto.Wiki.Article> articlesNotApproved = Eucalypto.Wiki.WikiManager.GetArticles(category,
                                                                            Eucalypto.Wiki.ArticleStatus.DisabledOrNotApproved);

            //Remove the articles of the current user because are already added to the previous list (My Articles)
            List<Eucalypto.Wiki.Article> articlesNotApprovedFilter = new List<Eucalypto.Wiki.Article>();
            foreach (Eucalypto.Wiki.Article article in articlesNotApproved)
            {
                if (string.Equals(article.Owner, User.Identity.Name) == false)
                    articlesNotApprovedFilter.Add(article);
            }

            if (articlesNotApprovedFilter.Count > 0)
            {
                listNotApproved.LoadList(articlesNotApprovedFilter);
                sectionNotApproved.Visible = true;
            }
        }
    }

}
