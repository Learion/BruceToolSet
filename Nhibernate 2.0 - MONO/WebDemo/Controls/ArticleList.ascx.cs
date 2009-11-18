using System;
using System.Collections.Generic;

using WebDemo.code;

public partial class Controls_ArticleList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void LoadList(IList<Eucalypto.Wiki.Article> list)
    {
        listRepeater.DataSource = list;
        listRepeater.DataBind();
    }

    protected string GetViewArticleUrl(string name)
    {
        return Navigation.Wiki_ViewArticle(name, 0).GetClientUrl(this, true);
    }

    protected static string GetArticleStatus(bool enabled, bool approved)
    {
        if (enabled == false)
            return "(Disabled)";
        if (approved == false)
            return "(Not approved)";
        return string.Empty;
    }
}
