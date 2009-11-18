using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using Eucalypto.Common;
using NHibernateDataStore.Common;

namespace Eucalypto.Wiki
{
    /// <summary>
    /// WikiProvider abstract class. 
    /// A WikiProvider can be used to store articles, attachments and the relative informations (versions, ...).
    /// 
    /// </summary>
    public abstract class WikiProvider : ProviderBase
    {
        #region Category
        public abstract Category CreateCategory(string name, string displayName);

        public abstract void UpdateCategory(Category category);

        public abstract void DeleteCategory(Category category);

        public abstract Category GetCategory(string id);

        public abstract Category GetCategoryByName(string name, bool throwIfNotFound);

        public abstract IList<Category> GetAllCategories();
        #endregion

        #region Articles
        public abstract Article CreateArticle(Category category, string owner, 
                                            string name, string title, string description, string body);

        public abstract IList<Article> GetArticles(Category category,
                            ArticleStatus status);

        public abstract IList<Article> GetArticlesByOwner(Category category,
                            string owner, ArticleStatus status);

        /// <summary>
        /// Update the specified article. The current version is incremented if required.
        /// </summary>
        /// <param name="article"></param>
        /// <param name="backupVersion">If true the previous article version is saved as a backup in the VersionedArticle and the current version is incremented.</param>
        public abstract void UpdateArticle(Article article, bool backupVersion);

        public abstract void DeleteArticle(Article article);

        public abstract void DeleteArticleVersion(VersionedArticle article);

        public abstract Article GetArticle(string id);

        public abstract Article GetArticleByName(string name, bool throwIfNotFound);

        /// <summary>
        /// Returns the specified version of the article. If the version is equal the article.Version then the article is returned.
        /// </summary>
        /// <param name="article"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public abstract ArticleBase GetArticleByVersion(Article article, int version);

        /// <summary>
        /// Get a list of article versions (also with the latest version)
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public abstract IList<ArticleBase> GetArticleVersions(Article article);

        public abstract IList<Article> FindArticles(Filter<string> categoryName,
                                           Filter<string> searchFor,
                                           Filter<string> author,
                                           Filter<string> owner,
                                           Filter<string> tag, 
                                           DateTime? fromDate, DateTime? toDate,
                                           ArticleStatus status,
                                           PagingInfo paging);
        #endregion

        #region Attachments
        public abstract FileAttachment CreateFileAttachment(Article article, string name, string contentType, byte[] contentData);

        public abstract string[] GetFileAttachments(Article article, EnabledStatus enabledStatus);

        public abstract void UpdateFileAttachment(FileAttachment attachment);

        public abstract void DeleteFileAttachment(FileAttachment attachment);

        public abstract FileAttachment GetFileAttachment(string id);

        public abstract FileAttachment GetFileAttachmentByName(Article article, string name, bool throwIfNotFound);
        #endregion
    }
}
