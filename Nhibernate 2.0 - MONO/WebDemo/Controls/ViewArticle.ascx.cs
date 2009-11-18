using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using WebDemo.code;

public partial class Controls_ViewArticle : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Eucalypto.Wiki.Article latestArticle = Eucalypto.Wiki.WikiManager.GetArticleByName(ArticleName, false);
        if (latestArticle == null)
        {
            controlDiv.Visible = false;
            sectionError.Visible = true;
        }
        else
        {
            controlDiv.Visible = true;
            sectionError.Visible = false;
            LoadArticle(latestArticle);
        }
    }

    #region Properties
    /// <summary>
    /// Gets or sets the article name to show. The value is saved in the ViewState.
    /// </summary>
    public string ArticleName
    {
        get
        {
            object val = ViewState["ArticleName"];
            return (string)val;
        }
        set { ViewState["ArticleName"] = value; }
    }

    /// <summary>
    /// Gets or sets the article version to show. 
    /// If 0 the latest version is used
    /// The value is saved in the ViewState.
    /// </summary>
    public int ArticleVersion
    {
        get
        {
            object val = ViewState["ArticleVersion"];
            return val == null ? 0 : (int)val;
        }
        set { ViewState["ArticleVersion"] = value; }
    }

    /// <summary>
    /// Gets or sets if show the actions section
    /// </summary>
    public bool SectionActionsVisible
    {
        get
        {
            object val = ViewState["SectionActionsVisible"];
            return val == null ? true : (bool)val;
        }
        set { ViewState["SectionActionsVisible"] = value; }
    }

    /// <summary>
    /// Gets or sets if show the properties section
    /// </summary>
    public bool SectionPropertiesVisible
    {
        get
        {
            object val = ViewState["SectionPropertiesVisible"];
            return val == null ? true : (bool)val;
        }
        set { ViewState["SectionPropertiesVisible"] = value; }
    }

    /// <summary>
    /// Gets or sets if before loading the article the attachments links must be replaced and the TOC inserted inside the article
    /// </summary>
    public bool ElaborateOutput
    {
        get
        {
            object val = ViewState["ElaborateOutput"];
            return val == null ? true : (bool)val;
        }
        set { ViewState["ElaborateOutput"] = value; }
    }

    #endregion

    private void LoadArticle(Eucalypto.Wiki.Article latestArticle)
    {
        Eucalypto.Wiki.ArticleBase article;

        if (Eucalypto.SecurityHelper.CanRead(Page.User, latestArticle.Category, latestArticle) == false)
            throw new Eucalypto.InvalidPermissionException("read article");

        if (ArticleVersion == 0)
            article = latestArticle;
        else
            article = Eucalypto.Wiki.WikiManager.GetArticleByVersion(latestArticle, ArticleVersion);

        lblArticleTitle.InnerText = article.Title;
        lblAuthor.InnerText = Utilities.GetDisplayUser(article.Author);
        lblDate.InnerText = Utilities.GetDateTimeForDisplay(article.UpdateDate);
        lblVersion.InnerText = article.Version.ToString();
        lblArticleDescription.InnerText = article.Description;

        string body = article.Body;
        if (ElaborateOutput)
            body = ElaborateXHTML(latestArticle, article);

        sectionBody.InnerHtml = body;


        linkLatestVersion.HRef = Navigation.Wiki_ViewArticle(latestArticle.Name, 0).GetServerUrl(true);
        linkBrowseVersions.HRef = Navigation.Wiki_ViewArticleVersions(latestArticle.Name).GetServerUrl(true);


        //Show the edit only if EditLinkVisible and this is the latest article version
        bool enabledEdit = Eucalypto.SecurityHelper.CanEdit(Page.User, latestArticle.Category, latestArticle);
        linkEdit.Visible = enabledEdit && latestArticle == article;
        linkEdit.HRef = Navigation.Wiki_EditArticle(ArticleName).GetServerUrl(true);
        linkPrint.HRef = Navigation.Wiki_PrintArticle(ArticleName, ArticleVersion).GetServerUrl(true);

        sectionActions.Visible = SectionActionsVisible;

        sectionProperties.Visible = SectionPropertiesVisible;
    }

    private string ElaborateXHTML(Eucalypto.Wiki.Article latestArticle, Eucalypto.Wiki.ArticleBase article)
    {
        Eucalypto.XHTMLText xhtml = new Eucalypto.XHTMLText();
        xhtml.Load(article.Body);

        string[] attachments = Eucalypto.Wiki.WikiManager.GetFileAttachments(latestArticle, Eucalypto.Wiki.EnabledStatus.Enabled);
        Array.Sort<string>(attachments);

        xhtml.ReplaceLinks(delegate(string oldUrl, out string newUrl)
                            { ReplaceLink(latestArticle.Name, attachments, oldUrl, out newUrl); }
                            );

        //Insert the TOC
        xhtml.InsertTOC(article.TOC);

        return xhtml.GetXhtml();
    }

    private void ReplaceLink(string articleName, string[] attachments, string oldUrl, out string newUrl)
    {
        const string EUCALYPTO_URL = "eucalypto:";

        //The returned url must be a plain text url (eucalypto automatically encode it when changing the html)

        //Is an attachment
        if (Array.BinarySearch<string>(attachments, oldUrl) >= 0)
            newUrl = Navigation.Wiki_Attach(articleName, oldUrl, false).GetClientUrl(this, false);
        else
        {
            //Is an eucalypto path
            if (oldUrl.StartsWith(EUCALYPTO_URL, StringComparison.InvariantCultureIgnoreCase))
            {
                string linkArticle = oldUrl.Substring(EUCALYPTO_URL.Length, oldUrl.Length - EUCALYPTO_URL.Length);

                newUrl = Navigation.Wiki_ViewArticle(linkArticle, 0).GetClientUrl(this, false);
            }
            else
                newUrl = oldUrl;
        }
    }
}
