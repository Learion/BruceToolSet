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
using System.ComponentModel;
using Eucalypto.Common;
using NHibernateDataStore.Common;
using WebDemo.code;

public partial class Controls_TopicList : System.Web.UI.UserControl
{
  private const int LIST_PAGING_SIZE = 20;

  protected void Page_Load(object sender, EventArgs e)
  {
    if (IsPostBack == false)
    {
      LoadList();
    }
  }

  #region Properties
  /// <summary>
  /// Gets or sets the forum category name used to load the list of topics
  /// </summary>
  public string CategoryName
  {
    get
    {
      object val = ViewState["CategoryName"];
      return val == null ? null : (string)val;
    }
    set { ViewState["CategoryName"] = value; }
  }

  /// <summary>
  /// The page used for the list pagination.
  /// </summary>
  protected int CurrentPage
  {
    get
    {
      object val = ViewState["CurrentPage"];
      return val == null ? 0 : (int)val;
    }
    set { ViewState["CurrentPage"] = value; }
  }
  #endregion

  private void LoadList()
  {
    Eucalypto.Forum.Category category = Eucalypto.Forum.ForumManager.GetCategoryByName(CategoryName, true);

    PagingInfo paging = new PagingInfo(LIST_PAGING_SIZE, CurrentPage);
    IList<Eucalypto.Forum.Topic> topics = Eucalypto.Forum.ForumManager.GetTopics(category,
                                                paging);


    lblCurrentPage.InnerText = (CurrentPage + 1).ToString();
    lblTotalPage.InnerText = paging.PagesCount.ToString();

    listRepeater.DataSource = topics;
    listRepeater.DataBind();

    if (CurrentPage == 0)
      linkPrev.Enabled = false;
    else
      linkPrev.Enabled = true;
    if (CurrentPage + 1 >= paging.PagesCount)
      linkNext.Enabled = false;
    else
      linkNext.Enabled = true;
  }

  protected string GetViewTopicUrl(string idTopic)
  {
    return Navigation.Forum_ViewTopic(idTopic).GetClientUrl(this, true);
  }

  protected string GetLastPost(Eucalypto.Forum.Topic topic)
  {
    IList<Eucalypto.Forum.Message> messages = Eucalypto.Forum.ForumManager.GetMessagesByTopic(topic);

    string status = "{1}<br />&nbsp;&nbsp;by {2}";

    DateTime lastReply = messages[messages.Count - 1].InsertDate;
    string lastUser = messages[messages.Count - 1].Owner;

    status = string.Format(status,
                    messages.Count,
                    Utilities.GetDateTimeForDisplay(lastReply),
                    Utilities.GetDisplayUser(lastUser));

    return status;
  }

  protected int GetRepliesCount(Eucalypto.Forum.Topic topic)
  {
    //Remove 1 because it is the topic message
    return Eucalypto.Forum.ForumManager.MessageCountByTopic(topic) - 1;
  }

  protected void linkPrev_Click(object sender, EventArgs e)
  {
    CurrentPage--;
    LoadList();
  }
  protected void linkNext_Click(object sender, EventArgs e)
  {
    CurrentPage++;
    LoadList();
  }
}
