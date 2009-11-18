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
using WebDemo.code;

public partial class Controls_SearchResult : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        linkPrev.Enabled = false;
        linkNext.Enabled = false;
    }

    #region Properties
    /// <summary>
    /// The page used for the list pagination. The value is saved in the ViewState.
    /// </summary>
    public int CurrentPage
    {
        get
        {
            object val = ViewState["CurrentPage"];
            return val == null ? 0 : (int)val;
        }
        private set { ViewState["CurrentPage"] = value; }
    }
    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entities">Must be a list of Eucalypto.ISearchResult entities (like Message or Article)</param>
    /// <param name="currentPage"></param>
    /// <param name="pagesCount"></param>
    public void LoadList(System.Collections.IEnumerable entities, int currentPage, int pagesCount)
    {
        CurrentPage = currentPage;
        lblCurrentPage.InnerText = (CurrentPage + 1).ToString();
        lblTotalPage.InnerText = pagesCount.ToString();

        listRepeater.DataSource = entities;
        listRepeater.DataBind();

        if (CurrentPage == 0)
            linkPrev.Enabled = false;
        else
            linkPrev.Enabled = true;
        if (CurrentPage + 1 >= pagesCount)
            linkNext.Enabled = false;
        else
            linkNext.Enabled = true;
    }

    public delegate Navigation.NavigationPage SearchEntityUrlDelegate(Eucalypto.ISearchResult entity);
    public event SearchEntityUrlDelegate SearchEntityUrlCallback;

    protected string GetViewUrl(Eucalypto.ISearchResult entity)
    {
        if (SearchEntityUrlCallback != null)
            return SearchEntityUrlCallback(entity).GetClientUrl(this, true);
        else
            return string.Empty;
    }

    public event EventHandler LinkPreviousClick;
    public event EventHandler LinkNextClick;

    protected void linkPrev_Click(object sender, EventArgs e)
    {
        if (LinkPreviousClick != null)
            LinkPreviousClick(this, e);
    }
    protected void linkNext_Click(object sender, EventArgs e)
    {
        if (LinkNextClick != null)
            LinkNextClick(this, e);
    }
}
