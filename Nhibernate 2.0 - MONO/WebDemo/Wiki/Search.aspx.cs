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
using Eucalypto.Common;
using NHibernateDataStore.Common;
using WebDemo.code;

public partial class Wiki_Search : System.Web.UI.Page
{
    private const int LIST_PAGING_SIZE = 10;

    protected void Page_Load(object sender, EventArgs e)
    {
        searchResult.LinkNextClick += new EventHandler(searchResult_LinkNextClick);
        searchResult.LinkPreviousClick += new EventHandler(searchResult_LinkPreviousClick);
        searchResult.SearchEntityUrlCallback += new Controls_SearchResult.SearchEntityUrlDelegate(searchResult_SearchEntityUrlCallback);

        if (IsPostBack == false)
        {
            LoadCategories();
        }
    }

    private void LoadCategories()
    {
        IList<Eucalypto.Wiki.Category> categories = Eucalypto.Wiki.WikiManager.GetAllCategories();
        listForum.Items.Clear();

        foreach (Eucalypto.Wiki.Category category in categories)
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

    private string[] GetSelectedCategories()
    {
        List<string> categories = new List<string>();

        foreach (ListItem item in listForum.Items)
        {
            if (item.Selected)
            {
                Eucalypto.Wiki.Category category = Eucalypto.Wiki.WikiManager.GetCategory(item.Value);
                if (Eucalypto.SecurityHelper.CanRead(User, category, null))
                {
                    categories.Add(category.Name);
                }
            }
        }

        return categories.ToArray();
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
        Eucalypto.Wiki.Article article = (Eucalypto.Wiki.Article)entity;

        return Navigation.Wiki_ViewArticle(article.Name, 0);
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
            IList<Eucalypto.Wiki.Article> articles = Eucalypto.Wiki.WikiManager.FindArticles(
                                                            Filter.MatchOne(GetSelectedCategories()),
                                                            Filter.ContainsAll(searchFor),
                                                            Filter.ContainsOne(authorSearch), 
                                                            null, null, null, null, 
                                                            Eucalypto.Wiki.ArticleStatus.EnabledAndApproved, 
                                                            paging);

            searchResult.LoadList(articles, page, (int)paging.PagesCount);
        }
        catch (Exception ex)
        {
            ((IErrorMessage)Master).SetError(GetType(), ex);
        }
    }
}
