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
using NHibernateDataStore.Common;
using WebDemo.code;

public partial class Wiki_CategoryRSS : System.Web.UI.Page
{
  private const int MESSAGE_AGE = 30;
  private const int MAX_MESSAGES = 100;
  private const string TEMPLATE_FILE = "App_Data\\ArticleTemplate.rss";

  protected void Page_Load(object sender, EventArgs e)
  {
    //Currently I have not used an IHttpHandler because I don't like to change IIS settings (required to associate the extensions to ASP.NET).
    // If you have performance problems anyway you can easily move this code inside an IHttpHandler without many changes

    SyndicationLibrary.RSS.RssFeed rss = CreateRssFeed();

    Response.Clear();

    Response.ContentType = "text/xml";

    rss.WriteToStream(Response.OutputStream);

    Response.End();
  }

  private SyndicationLibrary.RSS.RssFeed CreateRssFeed()
  {
    DateTime fromDate = DateTime.Now.AddDays(-MESSAGE_AGE);

    SyndicationLibrary.RSS.RssFeed rss;
    using (System.IO.FileStream stream = new System.IO.FileStream(Eucalypto.PathHelper.LocateServerPath(TEMPLATE_FILE), System.IO.FileMode.Open, System.IO.FileAccess.Read))
      rss = SyndicationLibrary.RSS.RssFeed.GetFeed(stream);

    if (rss == null)
      throw new ApplicationException("Failed to load rss from " + TEMPLATE_FILE);


    
     PagingInfo paging = new PagingInfo(MAX_MESSAGES, 0);
    IList<Eucalypto.Wiki.Article> articles = Eucalypto.Wiki.WikiManager.FindArticles(
                                                Eucalypto.Common.Filter.MatchOne(GetSelectedCategories()),
                                                null, null, null, null,
                                                fromDate,
                                                null,
                                                Eucalypto.Wiki.ArticleStatus.EnabledAndApproved,
                                                paging);

    DateTime lastPubDate = DateTime.MinValue;
    foreach (Eucalypto.Wiki.Article article in articles)
    {
      rss.Channel.Items.Add(CreateRssItem(article));

      if (article.UpdateDate > lastPubDate)
        lastPubDate = article.UpdateDate;
    }

    rss.Channel.PublicationDate = lastPubDate;
    rss.Channel.LastBuildDate = lastPubDate;

    return rss;
  }

  private SyndicationLibrary.RSS.RssItem CreateRssItem(Eucalypto.Wiki.Article article)
  {
    //Calculate the link of the article
    //The link is encoded automatically by the Rss library
    string link = Navigation.Wiki_ViewArticle(article.Name, article.Version).GetAbsoluteClientUrl(false);

    string rssTitle = string.Format("[{0}] {1}", article.Category.DisplayName, article.Title);

    SyndicationLibrary.RSS.RssItem item = new SyndicationLibrary.RSS.RssItem(rssTitle, article.Description, link);
    item.PublicationDate = article.UpdateDate;
    item.Guid = new SyndicationLibrary.RSS.RssGuid(link, true);

    return item;
  }

  private string[] GetSelectedCategories()
  {
    List<string> returnList = new List<string>();

    string queryCategory = Request["name"];
    if (queryCategory == null || queryCategory.Length == 0)
    {
      IList<Eucalypto.Wiki.Category> allCategories = Eucalypto.Wiki.WikiManager.GetAllCategories();

      foreach (Eucalypto.Wiki.Category category in allCategories)
      {
        if (Eucalypto.SecurityHelper.CanRead(User, category, null))
          returnList.Add(category.Name);
      }
    }
    else
    {
      string[] categoriesNameArray = queryCategory.Split(','); //I can use the comma as a separator because the category name cannot contains comma

      foreach (string categoryName in categoriesNameArray)
      {
        Eucalypto.Wiki.Category category = Eucalypto.Wiki.WikiManager.GetCategoryByName(categoryName, true);
        if (Eucalypto.SecurityHelper.CanRead(User, category, null))
          returnList.Add(category.Name);
      }

    }

    return returnList.ToArray();
  }
}
