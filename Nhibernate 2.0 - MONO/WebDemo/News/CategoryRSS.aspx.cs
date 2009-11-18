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

public partial class News_CategoryRSS : System.Web.UI.Page
{
  private const int MESSAGE_AGE = 10;
  private const int MAX_MESSAGES = 100;
  private const string TEMPLATE_FILE = "App_Data\\NewsTemplate.rss";

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
    IList<Eucalypto.News.Item> items = Eucalypto.News.NewsManager.FindItems(
                                                Eucalypto.Common.Filter.MatchOne(GetSelectedCategories()),
                                                null,
                                                fromDate,
                                                null,
                                                paging);

    DateTime lastPubDate = DateTime.MinValue;
    foreach (Eucalypto.News.Item item in items)
    {
      rss.Channel.Items.Add(CreateRssItem(item));

      if (item.UpdateDate > lastPubDate)
        lastPubDate = item.UpdateDate;
    }

    rss.Channel.PublicationDate = lastPubDate;
    rss.Channel.LastBuildDate = lastPubDate;

    return rss;
  }

  private SyndicationLibrary.RSS.RssItem CreateRssItem(Eucalypto.News.Item item)
  {
    string link = item.URL;

    string rssTitle = string.Format("[{0}] {1}", item.Category.DisplayName, item.Title);

    SyndicationLibrary.RSS.RssItem rssItem = new SyndicationLibrary.RSS.RssItem(rssTitle, item.Description, link);
    rssItem.PublicationDate = item.UpdateDate;
    rssItem.Guid = new SyndicationLibrary.RSS.RssGuid(link, true);

    return rssItem;
  }

  private string[] GetSelectedCategories()
  {
    List<string> returnList = new List<string>();

    string queryCategory = Request["name"];
    if (queryCategory == null || queryCategory.Length == 0)
    {
      IList<Eucalypto.News.Category> allCategories = Eucalypto.News.NewsManager.GetAllCategories();

      foreach (Eucalypto.News.Category category in allCategories)
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
        Eucalypto.News.Category category = Eucalypto.News.NewsManager.GetCategoryByName(categoryName, true);
        if (Eucalypto.SecurityHelper.CanRead(User, category, null))
          returnList.Add(category.Name);
      }

    }

    return returnList.ToArray();
  }
}
