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

public partial class Controls_ArticleVersionList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            LoadList();
        }
    }

    #region Properties
    /// <summary>
    /// Gets or sets the article name used to load the list of versions
    /// </summary>
    public string ArticleName
    {
        get
        {
            object val = ViewState["ArticleName"];
            return val == null ? null : (string)val;
        }
        set { ViewState["ArticleName"] = value; }
    }
    #endregion

    private void LoadList()
    {
        Eucalypto.Wiki.Article article = Eucalypto.Wiki.WikiManager.GetArticleByName(ArticleName, true);

        lblTitle.InnerText = article.Title;

        if (Eucalypto.SecurityHelper.CanRead(Page.User, article.Category, article) == false)
            throw new Eucalypto.InvalidPermissionException("read article");

        IList<Eucalypto.Wiki.ArticleBase> versions = Eucalypto.Wiki.WikiManager.GetArticleVersions(article);

        listRepeater.DataSource = versions;
        listRepeater.DataBind();
    }

    protected string GetViewUrl(Eucalypto.Wiki.ArticleBase article)
    {
        return Navigation.Wiki_ViewArticle(ArticleName, article.Version).GetClientUrl(Page, true);
    }
}
