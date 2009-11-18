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

public partial class Forum_Search : System.Web.UI.Page
{
  private const int LIST_PAGING_SIZE = 10;

  protected void Page_Load(object sender, EventArgs e)
  {
    searchResult.LinkNextClick += new EventHandler(searchResult_LinkNextClick);
    searchResult.LinkPreviousClick += new EventHandler(searchResult_LinkPreviousClick);
    searchResult.SearchEntityUrlCallback += new Controls_SearchResult.SearchEntityUrlDelegate(searchResult_SearchEntityUrlCallback);

    if (IsPostBack == false)
    {
      LoadForums();
    }
  }

  private void LoadForums()
  {
    IList<Eucalypto.Forum.Category> forums = Eucalypto.Forum.ForumManager.GetAllCategories();
    listForum.Items.Clear();

    foreach (Eucalypto.Forum.Category category in forums)
    {
      if (Eucalypto.SecurityHelper.CanRead(User, category, null))
      {
        ListItem listItem = new ListItem(category.DisplayName);
        listItem.Selected = true;
        listItem.Value = category.Id;


        listForum.Items.Add(listItem);
      }
    }
  }

  private string[] GetSelectedForums()
  {
    List<string> forums = new List<string>();

    foreach (ListItem item in listForum.Items)
    {
      if (item.Selected)
      {
        Eucalypto.Forum.Category category = Eucalypto.Forum.ForumManager.GetCategory(item.Value);
        if (Eucalypto.SecurityHelper.CanRead(User, category, null))
        {
          forums.Add(category.Name);
        }
      }
    }

    return forums.ToArray();
  }

  void searchResult_LinkPreviousClick(object sender, EventArgs e)
  {
    LoadList(searchResult.CurrentPage - 1);
  }

  void searchResult_LinkNextClick(object sender, EventArgs e)
  {
    LoadList(searchResult.CurrentPage + 1);
  }

  Navigation.NavigationPage searchResult_SearchEntityUrlCallback(Eucalypto.ISearchResult entity)
  {
    Eucalypto.Forum.Message msg = (Eucalypto.Forum.Message)entity;

    return Navigation.Forum_ViewTopic(msg.Topic.Id, msg.Id);
  }


  protected void btSearch_Click(object sender, EventArgs e)
  {
    LoadList(0);
  }

  private void LoadList(int page)
  {
    try
    {
      string[] searchFor = Eucalypto.SplitHelper.SplitSearchText(txtSearchFor.Text);
      string[] authorSearch = Eucalypto.SplitHelper.SplitSearchText(txtAuthor.Text);

      PagingInfo paging = new PagingInfo(LIST_PAGING_SIZE, page);
      IList<Eucalypto.Forum.Message> messages = Eucalypto.Forum.ForumManager.FindMessages(
                                                      Eucalypto.Common.Filter.MatchOne(GetSelectedForums()),
                                                      Eucalypto.Common.Filter.ContainsAll(searchFor),
                                                      Eucalypto.Common.Filter.ContainsOne(authorSearch),
                                                      null,
                                                      null, null,
                                                      paging);

      searchResult.LoadList(messages, page, (int)paging.PagesCount);
    }
    catch (Exception ex)
    {
      ((IErrorMessage)Master).SetError(GetType(), ex);
    }
  }
}
