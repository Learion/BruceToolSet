using System;
using System.Web;
using System.Web.UI;

namespace WebDemo.code
{
   

    public static class Navigation
    {
        public struct NavigationPage
        {
            public string Location;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="location">Must be a relative path like SubPath/File.aspx, without ~ character</param>
            public NavigationPage(string location)
            {
                Location = location;
            }

            /// <summary>
            /// Returns an Url to this page that can be used on server side (using ~ notation)
            /// </summary>
            /// <param name="htmlEncode">True to encode the url so can be used without problems inside an xhtml article</param>
            /// <returns></returns>
            public string GetServerUrl(bool htmlEncode)
            {
                string url = Eucalypto.PathHelper.CombineUrl("~", Location);
                if (htmlEncode)
                    url = HttpUtility.HtmlEncode(url);
                return url;
            }

            /// <summary>
            /// Returns an absolute client url
            /// </summary>
            /// <param name="htmlEncode">True to encode the url so can be used without problems inside an xhtml article</param>
            /// <returns></returns>
            public string GetAbsoluteClientUrl(bool htmlEncode)
            {
                string url = Eucalypto.PathHelper.CombineUrl(Eucalypto.PathHelper.GetWebAppUrl(), Location);
                if (htmlEncode)
                    url = HttpUtility.HtmlEncode(url);
                return url;
            }

            /// <summary>
            /// Returns an url to this page that can be used on client side
            /// </summary>
            /// <param name="source"></param>
            /// <param name="htmlEncode">True to encode the url so can be used without problems inside an xhtml article</param>
            /// <returns></returns>
            public string GetClientUrl(Control source, bool htmlEncode)
            {
                string url = source.ResolveClientUrl(GetServerUrl(false));
                if (htmlEncode)
                    url = HttpUtility.HtmlEncode(url);
                return url;
            }

            /// <summary>
            /// Redirect the response to this page.
            /// </summary>
            /// <param name="source"></param>
            public void Redirect(Control source)
            {
                source.Page.Response.Redirect(GetServerUrl(false));
            }

            /// <summary>
            /// User Server.Transfer.
            /// </summary>
            public void Transfer()
            {
                System.Web.HttpContext.Current.Server.Transfer(GetServerUrl(false));
            }
        }

        /// <summary>
        /// Build a url based on the page and the parameters specified
        /// </summary>
        /// <param name="page">The page, like page.aspx</param>
        /// <param name="paramTemplate">Must be a format string like forum={0}&id={1}</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string BuildUrl(string page, string paramTemplate, params string[] parameters)
        {
            string[] encodedParams;
            if (parameters != null && parameters.Length > 0)
            {
                encodedParams = new string[parameters.Length];
                for (int i = 0; i < parameters.Length; i++)
                    encodedParams[i] = HttpUtility.UrlEncode(parameters[i]);
            }
            else
            {
                encodedParams = new string[0];
            }

            return page + "?" + string.Format(paramTemplate, encodedParams);
        }

        public static NavigationPage Error(Exception exception)
        {
            return Error(Utilities.FormatException(exception));
        }

        public static NavigationPage Error(string errorDescr)
        {
            if (errorDescr == null)
                errorDescr = "Error not specified";

            if (errorDescr.Length > 120)
                errorDescr = errorDescr.Substring(0, 120) + "...";

            //HttpContext.Current.Session["error"] = errorDescr; //The problem with the session is that is not always available

            return new NavigationPage(BuildUrl("/Error.aspx", "error={0}", errorDescr));
        }

        public static NavigationPage Admin_FileAttachDetails(string id)
        {
            return new NavigationPage(BuildUrl("/Admin/FileAttachDetails.aspx", "id={0}", id));
        }

        public static NavigationPage Admin_FileAttachList()
        {
            return new NavigationPage("/Admin/FileAttachList.aspx");
        }

        public static NavigationPage Admin_ForumList()
        {
            return new NavigationPage("/Admin/ForumList.aspx");
        }

        public static NavigationPage Admin_ForumDetails(string id)
        {
            return new NavigationPage(BuildUrl("/Admin/ForumDetails.aspx", "id={0}", id));
        }

        public static NavigationPage Admin_ForumDetailsNew()
        {
            return new NavigationPage("/Admin/ForumDetails.aspx");
        }

        public static NavigationPage Admin_WikiList()
        {
            return new NavigationPage("/Admin/WikiList.aspx");
        }

        public static NavigationPage Admin_WikiDetails(string id)
        {
            return new NavigationPage(BuildUrl("/Admin/WikiDetails.aspx", "id={0}", id));
        }

        public static NavigationPage Admin_WikiDetailsNew()
        {
            return new NavigationPage("/Admin/WikiDetails.aspx");
        }

        public static NavigationPage Admin_NewsList()
        {
            return new NavigationPage("/Admin/NewsList.aspx");
        }

        public static NavigationPage Admin_NewsDetails(string id)
        {
            return new NavigationPage(BuildUrl("/Admin/NewsDetails.aspx", "id={0}", id));
        }

        public static NavigationPage Admin_NewsDetailsNew()
        {
            return new NavigationPage("/Admin/NewsDetails.aspx");
        }

        public static NavigationPage Admin_UserDetails(string id)
        {
            return new NavigationPage(BuildUrl("/Admin/UserDetails.aspx", "id={0}", id));
        }

        public static NavigationPage Admin_UserDetailsNew()
        {
            return new NavigationPage("/Admin/UserDetails.aspx");
        }



        public static NavigationPage Forum_Default()
        {
            return new NavigationPage("/Forum/Default.aspx");
        }
        public static NavigationPage Forum_ViewTopic(string idTopic)
        {
            return new NavigationPage(BuildUrl("/Forum/ViewTopic.aspx", "id={0}", idTopic));
        }

        public static NavigationPage Forum_ViewTopic(string idTopic, string idMessage)
        {
            return new NavigationPage(BuildUrl("/Forum/ViewTopic.aspx", "id={0}#msg{1}", idTopic, idMessage));
        }

        public static NavigationPage Forum_ViewForum(string forumName)
        {
            return new NavigationPage(BuildUrl("/Forum/ViewForum.aspx", "forum={0}", forumName));
        }

        /// <summary>
        /// Returns the rss page for the forums selected.
        /// </summary>
        /// <param name="forumNames">Use null to returns all the available forums</param>
        /// <returns></returns>
        public static NavigationPage Forum_ForumRss(params string[] forumNames)
        {
            if (forumNames == null || forumNames.Length == 0)
                return new NavigationPage("/Forum/ForumRss.aspx");
            else
                return new NavigationPage(BuildUrl("/Forum/ForumRss.aspx", "name={0}", string.Join(",", forumNames)));
        }

        public static NavigationPage Forum_Search()
        {
            return new NavigationPage("/Forum/Search.aspx");
        }

        public static NavigationPage Forum_NewTopic(string forumName)
        {
            return new NavigationPage(BuildUrl("/Forum/NewTopic.aspx", "forum={0}", forumName));
        }

        public static NavigationPage Forum_NewMessage(string idParentMsg)
        {
            return new NavigationPage(BuildUrl("/Forum/NewMessage.aspx", "parent={0}", idParentMsg));
        }

        public static NavigationPage Forum_Attach(string msgId, bool download)
        {
            string mode;
            if (download)
                mode = "download";
            else
                mode = "show";

            return new NavigationPage(BuildUrl("/Forum/Attach.ashx", "id={0}&mode={1}", msgId, mode));
        }



        public static NavigationPage Wiki_Default()
        {
            return new NavigationPage("/Wiki/Default.aspx");
        }
        public static NavigationPage Wiki_ViewCategory(string categoryName)
        {
            return new NavigationPage(BuildUrl("/Wiki/ViewCategory.aspx", "name={0}", categoryName));
        }
        /// <summary>
        /// Returns the rss page for the categories selected.
        /// </summary>
        /// <param name="categoryNames">Use null to returns all the available categories</param>
        /// <returns></returns>
        public static NavigationPage Wiki_CategoryRss(params string[] categoryNames)
        {
            if (categoryNames == null || categoryNames.Length == 0)
                return new NavigationPage("/Wiki/CategoryRSS.aspx");
            else
                return new NavigationPage(BuildUrl("/Wiki/CategoryRSS.aspx", "name={0}", string.Join(",", categoryNames)));
        }

        public static NavigationPage Wiki_Search()
        {
            return new NavigationPage("/Wiki/Search.aspx");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="version">Use 0 for the latest version</param>
        /// <returns></returns>
        public static NavigationPage Wiki_ViewArticle(string name, int version)
        {
            return new NavigationPage(BuildUrl("/Wiki/ViewArticle.aspx", "name={0}&version={1}", name, version.ToString()));
        }

        public static NavigationPage Wiki_ViewArticleVersions(string name)
        {
            return new NavigationPage(BuildUrl("/Wiki/ViewArticleVersions.aspx", "name={0}", name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="version">Use 0 for the latest version</param>
        /// <returns></returns>
        public static NavigationPage Wiki_PrintArticle(string name, int version)
        {
            return new NavigationPage(BuildUrl("/Wiki/PrintArticle.aspx", "name={0}&version={1}", name, version.ToString()));
        }

        public static NavigationPage Wiki_EditArticle(string name)
        {
            return new NavigationPage(BuildUrl("/Wiki/EditArticle.aspx", "name={0}", name));
        }
        public static NavigationPage Wiki_NewArticle(string categoryName)
        {
            return new NavigationPage(BuildUrl("/Wiki/NewArticle.aspx", "name={0}", categoryName));
        }

        public static NavigationPage Wiki_Attach(string articleName, string attachName, bool download)
        {
            string mode;
            if (download)
                mode = "download";
            else
                mode = "show";

            return new NavigationPage(BuildUrl("/Wiki/Attach.ashx", "article={0}&attach={1}&mode={2}", articleName, attachName, mode));
        }


        public static NavigationPage News_Default()
        {
            return new NavigationPage("/News/Default.aspx");
        }
        public static NavigationPage News_ViewCategory(string categoryName)
        {
            return new NavigationPage(BuildUrl("/News/ViewCategory.aspx", "name={0}", categoryName));
        }
        /// <summary>
        /// Returns the rss page for the categories selected.
        /// </summary>
        /// <param name="categoryNames">Use null to returns all the available categories</param>
        /// <returns></returns>
        public static NavigationPage News_CategoryRss(params string[] categoryNames)
        {
            if (categoryNames == null || categoryNames.Length == 0)
                return new NavigationPage("/News/CategoryRSS.aspx");
            else
                return new NavigationPage(BuildUrl("/News/CategoryRSS.aspx", "name={0}", string.Join(",", categoryNames)));
        }

        public static NavigationPage News_NewItem(string categoryName)
        {
            return new NavigationPage(BuildUrl("/News/EditItem.aspx", "category={0}", categoryName));
        }

        public static NavigationPage News_EditItem(string id)
        {
            return new NavigationPage(BuildUrl("/News/EditItem.aspx", "item={0}", id));
        }

        public static NavigationPage News_ViewItem(string id)
        {
            return new NavigationPage(BuildUrl("/News/ViewItem.aspx", "item={0}", id));
        }
    }

}
