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
using NHibernateDataStore.Common;
using WebDemo.code;

public partial class Controls_NewsListBox : System.Web.UI.UserControl
{
  protected void Page_Load(object sender, EventArgs e)
  {
    Eucalypto.News.Category category = Eucalypto.News.NewsManager.GetCategoryByName(CategoryName, false);

    if (category != null)
    {
      sectionError.Visible = false;
      listRepeater.Visible = true;

      LoadNews(category);
    }
    else
    {
      sectionError.Visible = true;
      listRepeater.Visible = false;
    }
  }

  #region Properties
  /// <summary>
  /// Gets or sets the news category name 
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
  #endregion

  private void LoadNews(Eucalypto.News.Category category)
  {
    if (Eucalypto.SecurityHelper.CanRead(Page.User, category, null) == false)
      throw new Eucalypto.InvalidPermissionException("read category");

    HtmlLink link = new HtmlLink();
    link.Href = Navigation.News_CategoryRss(CategoryName).GetServerUrl(true);
    link.Attributes.Add("rel", "alternate");
    link.Attributes.Add("type", "application/rss+xml");
    link.Attributes.Add("title", category.DisplayName);
    Page.Header.Controls.Add(link);

    linkRss.HRef = Navigation.News_CategoryRss(CategoryName).GetServerUrl(true);

    title.InnerText = category.DisplayName;

    PagingInfo paging = new PagingInfo(5, 0);
    IList<Eucalypto.News.Item> list = Eucalypto.News.NewsManager.GetItems(category, paging);

    listRepeater.DataSource = list;
    listRepeater.DataBind();
  }

  protected string GetViewUrl(Eucalypto.News.Item item)
  {
    return Navigation.News_ViewItem(item.Id).GetClientUrl(Page, true);
  }

  protected string GetShortDescription(Eucalypto.News.Item item)
  {
    if (item.Description == null)
      return string.Empty;

    if (item.Description.Length > 100)
      return item.Description.Substring(0, 100) + "...";
      return item.Description;
  }
}
