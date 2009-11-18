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

public partial class Controls_NewsList : System.Web.UI.UserControl
{
  protected void Page_Load(object sender, EventArgs e)
  {
    Eucalypto.News.Category category = Eucalypto.News.NewsManager.GetCategoryByName(CategoryName, true);
    if (Eucalypto.SecurityHelper.CanRead(Page.User, category, null) == false)
      throw new Eucalypto.InvalidPermissionException("read category");

    LoadList(Eucalypto.News.NewsManager.GetItems(category, PagingInfo.All));
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

  private void LoadList(IList<Eucalypto.News.Item> list)
  {
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
    else
      return item.Description;
  }
}
