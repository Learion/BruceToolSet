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

public partial class Forum_ForumRss : System.Web.UI.Page
{
  private const int MESSAGE_AGE = 10;
  private const int MAX_MESSAGES = 100;
  private const string TEMPLATE_FILE = "App_Data\\ForumTemplate.rss";

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
    IList<Eucalypto.Forum.Message> messages = Eucalypto.Forum.ForumManager.FindMessages(
                                                Eucalypto.Common.Filter.MatchOne(GetSelectedForums()),
                                                null,
                                                null,
                                                null,
                                                fromDate,
                                                null,
                                                paging);

    DateTime lastPubDate = DateTime.MinValue;
    foreach (Eucalypto.Forum.Message msg in messages)
    {
      rss.Channel.Items.Add(CreateRssItem(msg));

      if (msg.UpdateDate > lastPubDate)
        lastPubDate = msg.UpdateDate;
    }

    rss.Channel.PublicationDate = lastPubDate;
    rss.Channel.LastBuildDate = lastPubDate;

    return rss;
  }

  private SyndicationLibrary.RSS.RssItem CreateRssItem(Eucalypto.Forum.Message msg)
  {
    //Calculate the link of the forum
    //The link is encoded automatically by the Rss library
    string link = Navigation.Forum_ViewTopic(msg.Topic.Id, msg.Id).GetAbsoluteClientUrl(false);

    string rssTitle = string.Format("[{0}] {1}", msg.Topic.Category.DisplayName, msg.Title);

    SyndicationLibrary.RSS.RssItem item = new SyndicationLibrary.RSS.RssItem(rssTitle, msg.Body, link);
    item.PublicationDate = msg.UpdateDate;
    item.Guid = new SyndicationLibrary.RSS.RssGuid(link, true);

    return item;
  }

  private string[] GetSelectedForums()
  {
    List<string> returnList = new List<string>();

    string queryForum = Request["name"];
    if (queryForum == null || queryForum.Length == 0)
    {
      IList<Eucalypto.Forum.Category> allCategories = Eucalypto.Forum.ForumManager.GetAllCategories();

      foreach (Eucalypto.Forum.Category category in allCategories)
      {
        if (Eucalypto.SecurityHelper.CanRead(User, category, null))
          returnList.Add(category.Name);
      }
    }
    else
    {
      string[] forumsNameArray = queryForum.Split(','); //I can use the comma as a separator because the forum name cannot contains comma

      foreach (string forumName in forumsNameArray)
      {
        Eucalypto.Forum.Category category = Eucalypto.Forum.ForumManager.GetCategoryByName(forumName, true);
        if (Eucalypto.SecurityHelper.CanRead(User, category, null))
          returnList.Add(category.Name);
      }

    }

    return returnList.ToArray();
  }
}
