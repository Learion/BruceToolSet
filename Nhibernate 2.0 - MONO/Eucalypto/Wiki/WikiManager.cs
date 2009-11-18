using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Web.Configuration;
using System.Configuration;
using Eucalypto.Common;
using NHibernateDataStore.Common;

namespace Eucalypto.Wiki
{
    public class WikiManager
    {
        static WikiManager()
        {
            //Get the feature's configuration info
            WikiProviderConfiguration qc =
                (WikiProviderConfiguration)ConfigurationManager.GetSection("wikiManager");

            if (qc == null || qc.DefaultProvider == null || qc.Providers == null || qc.Providers.Count < 1)
                throw new ProviderException("You must specify a valid default provider for wikiManager.");

            //Instantiate the providers
            providerCollection = new WikiProviderCollection();
            ProvidersHelper.InstantiateProviders(qc.Providers, providerCollection, typeof(WikiProvider));
            providerCollection.SetReadOnly();
            defaultProvider = providerCollection[qc.DefaultProvider];
            if (defaultProvider == null)
            {
                throw new ConfigurationErrorsException(
                    "You must specify a default provider for the wikiManager.",
                    qc.ElementInformation.Properties["defaultProvider"].Source,
                    qc.ElementInformation.Properties["defaultProvider"].LineNumber);
            }
        }

        //Public feature API
        private static WikiProvider defaultProvider;
        private static WikiProviderCollection providerCollection;

        public static WikiProvider Provider
        {
            get{return defaultProvider;}
        }

        public static WikiProviderCollection Providers
        {
            get{return providerCollection;}
        }


        #region Static methods

        #region Category
        public static Category CreateCategory(string name, string displayName)
        {
            return Provider.CreateCategory(name, displayName);
        }

        public static void UpdateCategory(Category category)
        {
            Provider.UpdateCategory(category);
        }

        public static void DeleteCategory(Category category)
        {
            Provider.DeleteCategory(category);
        }

        public static Category GetCategory(string id)
        {
            return Provider.GetCategory(id);
        }

        public static Category GetCategoryByName(string name, bool throwIfNotFound)
        {
            return Provider.GetCategoryByName(name, throwIfNotFound);
        }

        public static IList<Category> GetAllCategories()
        {
            return Provider.GetAllCategories();
        }
        #endregion

        #region Articles
        public static Article CreateArticle(Category category, string owner,
                                            string name, string title, string description, string body)
        {
            return Provider.CreateArticle(category, owner, name, title, description, body);
        }

        public static IList<Article> GetArticles(Category category,
                            ArticleStatus status)
        {
            return Provider.GetArticles(category, status);
        }

        public static IList<Article> GetArticlesByOwner(Category category,
                            string owner, ArticleStatus status)
        {
            return Provider.GetArticlesByOwner(category, owner, status);
        }

        /// <summary>
        /// Update the specified article. The current version is incremented if required.
        /// </summary>
        /// <param name="article"></param>
        /// <param name="backupVersion">If true the previous article version is saved as a backup in the VersionedArticle and the current version is incremented.</param>
        public static void UpdateArticle(Article article, bool backupVersion)
        {
            Provider.UpdateArticle(article, backupVersion);
        }

        public static void DeleteArticle(Article article)
        {
            Provider.DeleteArticle(article);
        }

        public static void DeleteArticleVersion(VersionedArticle article)
        {
            Provider.DeleteArticleVersion(article);
        }

        public static Article GetArticle(string id)
        {
            return Provider.GetArticle(id);
        }

        public static Article GetArticleByName(string name, bool throwIfNotFound)
        {
            return Provider.GetArticleByName(name, throwIfNotFound);
        }

        /// <summary>
        /// Returns the specified version of the article. If the version is equal the article.Version then the article is returned.
        /// </summary>
        /// <param name="article"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static ArticleBase GetArticleByVersion(Article article, int version)
        {
            return Provider.GetArticleByVersion(article, version);
        }

        /// <summary>
        /// Get a list of article versions (also with the latest version)
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public static IList<ArticleBase> GetArticleVersions(Article article)
        {
            return Provider.GetArticleVersions(article);
        }

        public static IList<Article> FindArticles(Filter<string> categoryName,
                                           Filter<string> searchFor,
                                           Filter<string> author,
                                           Filter<string> owner,
                                           Filter<string> tag,  
                                           DateTime? fromDate, DateTime? toDate,
                                           ArticleStatus status,
                                           PagingInfo paging)
        {
            return Provider.FindArticles(categoryName, searchFor, 
                                        author, owner, 
                                        tag,
                                         fromDate, toDate, 
                                         status,
                                         paging);
        }
        #endregion

        #region Attachments
        public static FileAttachment CreateFileAttachment(Article article, string name, string contentType, byte[] contentData)
        {
            return Provider.CreateFileAttachment(article, name, contentType, contentData);
        }

        public static string[] GetFileAttachments(Article article, EnabledStatus enabledStatus)
        {
            return Provider.GetFileAttachments(article, enabledStatus);
        }

        public static void UpdateFileAttachment(FileAttachment attachment)
        {
            Provider.UpdateFileAttachment(attachment);
        }

        public static void DeleteFileAttachment(FileAttachment attachment)
        {
            Provider.DeleteFileAttachment(attachment);
        }

        public static FileAttachment GetFileAttachment(string id)
        {
            return Provider.GetFileAttachment(id);
        }

        public static FileAttachment GetFileAttachmentByName(Article article, string name, bool throwIfNotFound)
        {
            return Provider.GetFileAttachmentByName(article, name, throwIfNotFound);
        }
        #endregion

        #endregion
    }
}
