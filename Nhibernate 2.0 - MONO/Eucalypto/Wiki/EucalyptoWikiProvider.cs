using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Configuration;
using System.Net.Mail;
using Eucalypto.Common;
using NHibernateDataStore.Common;

namespace Eucalypto.Wiki
{
    /// <summary>
    /// Implementation of WikiProvider that use NHibernate to save and retrive informations.
    ///
    /// Configuration:
    /// connectionStringName = the name of the connection string to use
    /// 
    /// </summary>
    public class EucalyptoWikiProvider : WikiProvider
    {
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "EucalyptoWikiProvider";

            base.Initialize(name, config);

            this.mProviderName = name;

            //Read the configurations
            //Connection string
            string connName = ExtractConfigValue(config, "connectionStringName", null);
            mConfiguration = ConfigurationHelper.Create(connName);


            // Throw an exception if unrecognized attributes remain
            if (config.Count > 0)
            {
                string attr = config.GetKey(0);
                if (!String.IsNullOrEmpty(attr))
                    throw new System.Configuration.Provider.ProviderException("Unrecognized attribute: " +
                    attr);
            }
        }

        /// <summary>
        /// A helper function to retrieve config values from the configuration file and remove the entry.
        /// </summary>
        /// <returns></returns>
        private string ExtractConfigValue(System.Collections.Specialized.NameValueCollection config, string key, string defaultValue)
        {
            string val = config[key];
            if (val == null)
                return defaultValue;
            else
            {
                config.Remove(key);

                return val;
            }
        }

        #region Properties
        private string mProviderName;
        public string ProviderName
        {
            get { return mProviderName; }
            set { mProviderName = value; }
        }

        private ConnectionParameters mConfiguration;
        public ConnectionParameters Configuration
        {
            get { return mConfiguration; }
        }

        #endregion

        #region methods
        #region Category
        public override Category CreateCategory(string name, string displayName)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                CategoryDataStore dataStore = new CategoryDataStore(transaction);

                Category category = new Category(name, displayName);

                dataStore.Insert(category);

                transaction.Commit();

                return category;
            }
        }

        public override void UpdateCategory(Category category)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                CategoryDataStore dataStore = new CategoryDataStore(transaction);

                dataStore.Update(category);

                transaction.Commit();
            }
        }

        public override void DeleteCategory(Category category)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                CategoryDataStore dataStore = new CategoryDataStore(transaction);

                dataStore.Delete(category.Id);

                transaction.Commit();
            }
        }

        public override Category GetCategory(string id)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                CategoryDataStore dataStore = new CategoryDataStore(transaction);

                Category category = dataStore.FindByKey(id);
                if (category == null)
                    throw new WikiCategoryNotFoundException(id);

                return category;
            }
        }

        public override Category GetCategoryByName(string name, bool throwIfNotFound)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                CategoryDataStore dataStore = new CategoryDataStore(transaction);

                Category category = dataStore.FindByName(name);
                if (category == null && throwIfNotFound)
                    throw new WikiCategoryNotFoundException(name);
                else if (category == null)
                    return null;

                return category;
            }
        }

        public override IList<Category> GetAllCategories()
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                CategoryDataStore dataStore = new CategoryDataStore(transaction);

                return dataStore.FinAll();
            }
        }
        #endregion

        #region Articles
        public override Article CreateArticle(Category category, string owner,
                                            string name, string title, 
                                            string description, string body)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ArticleDataStore articleStore = new ArticleDataStore(transaction);
                if (articleStore.FindByName(name) != null)
                    throw new ArticleNameAlreadyExistsException(name);

                CategoryDataStore dataStore = new CategoryDataStore(transaction);
                dataStore.Attach(category);

                Article article = new Article(category, name, owner, title, description, body);
                article.Author = owner;

                if (category.AutoApprove)
                    article.Approved = true;

                articleStore.Insert(article);

                transaction.Commit();

                return article;
            }
        }

        public override IList<Article> GetArticles(Category category,
                            ArticleStatus status)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ArticleDataStore dataStore = new ArticleDataStore(transaction);

                return dataStore.FindByCategoryAndOwner(category, null, status);
            }
        }

        public override IList<Article> GetArticlesByOwner(Category category,
                            string owner, ArticleStatus status)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ArticleDataStore dataStore = new ArticleDataStore(transaction);

                return dataStore.FindByCategoryAndOwner(category, owner, status);
            }
        }

        /// <summary>
        /// Update the specified article. Increment the version if required.
        /// </summary>
        /// <param name="article"></param>
        /// <param name="backupVersion">If true the previous article version is saved as a backup in the VersionedArticle and the current version is incremented.</param>
        public override void UpdateArticle(Article article, bool backupVersion)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ArticleDataStore dataStore = new ArticleDataStore(transaction);

                VersionedArticle versionedArticle = null;

                if (backupVersion)
                {
                    //Retrive the previous version (before saving the new instance) and save a versioned row

                    Article prevVersion = dataStore.FindByKey(article.Id);
                    if (prevVersion == null)
                        throw new ArticleNotFoundException(article.Id);

                    versionedArticle = new VersionedArticle(prevVersion);

                    VersionedArticleDataStore versionedStore = new VersionedArticleDataStore(transaction);
                    versionedStore.Insert(versionedArticle);

                    //Increment the current article version
                    article.IncrementVersion();
                }

                //flag the entity to be updated and attach the entity to the db
                // I must use InsertOrUpdateCopy because if backupVersion = true there is already a 
                // persistent entity in the session and I must copy the values to this instance. The Update method in this case throw an exception
                article = dataStore.InsertOrUpdateCopy(article);

                transaction.Commit();
            }
        }

        public override void DeleteArticle(Article article)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ArticleDataStore dataStore = new ArticleDataStore(transaction);

                dataStore.Delete(article.Id);

                transaction.Commit();
            }
        }

        public override void DeleteArticleVersion(VersionedArticle article)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                VersionedArticleDataStore dataStore = new VersionedArticleDataStore(transaction);

                dataStore.Delete(article.Id);

                transaction.Commit();
            }
        }

        public override Article GetArticle(string id)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ArticleDataStore dataStore = new ArticleDataStore(transaction);

                Article article = dataStore.FindByKey(id);
                if (article == null)
                    throw new ArticleNotFoundException(id);

                return article;
            }
        }

        public override Article GetArticleByName(string name, bool throwIfNotFound)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ArticleDataStore dataStore = new ArticleDataStore(transaction);

                Article article = dataStore.FindByName(name);
                if (article == null && throwIfNotFound)
                    throw new ArticleNotFoundException(name);
                else if (article == null)
                    return null;

                return article;
            }
        }

        /// <summary>
        /// Returns the specified version of the article. If the version is equal the article.Version then the article is returned.
        /// </summary>
        /// <param name="article"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public override ArticleBase GetArticleByVersion(Article article, int version)
        {
            if (article.Version == version)
                return article;

            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                VersionedArticleDataStore dataStore = new VersionedArticleDataStore(transaction);

                VersionedArticle versionedArticle = dataStore.FindByArticleVersion(article, version);
                if (versionedArticle == null)
                    throw new ArticleNotFoundException(article.Name + " " + version.ToString());

                return versionedArticle;
            }
        }

        /// <summary>
        /// Get a list of article versions (also with the latest version)
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public override IList<ArticleBase> GetArticleVersions(Article article)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                VersionedArticleDataStore dataStore = new VersionedArticleDataStore(transaction);

                IList<VersionedArticle> versionedArticles = dataStore.GetArticleVersions(article);

                List<ArticleBase> list = new List<ArticleBase>();

                //Add the latest version
                list.Add(article);

                //add all the other versions
                foreach (VersionedArticle verArticle in versionedArticles)
                    list.Add(verArticle);

                return list;
            }
        }

        public override IList<Article> FindArticles(Filter<string> categoryName,
                                           Filter<string> searchFor,
                                           Filter<string> author,
                                           Filter<string> owner,
                                           Filter<string> tag,  
                                           DateTime? fromDate, DateTime? toDate,
                                           ArticleStatus status,
                                           PagingInfo paging)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ArticleDataStore dataStore = new ArticleDataStore(transaction);

                return dataStore.FindByFields(categoryName, searchFor, 
                                                author, owner,
                                                tag,
                                                fromDate, toDate, 
                                                status,
                                                paging);
            }
        }
        #endregion

        #region Attachments
        public override FileAttachment CreateFileAttachment(Article article, string name, string contentType, byte[] contentData)
        {
            FileAttachment attachment = new FileAttachment(article, name, contentType, contentData);

            //Check attachment
            if (attachment != null)
                Attachment.FileHelper.CheckFile(attachment, article.Category.AttachExtensions, article.Category.AttachMaxSize);

            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ArticleDataStore dataStore = new ArticleDataStore(transaction);
                dataStore.Attach(article);

                FileAttachmentDataStore attachmentStore = new FileAttachmentDataStore(transaction);
                attachmentStore.Insert(attachment);

                transaction.Commit();

                return attachment;
            }
        }

        public override string[] GetFileAttachments(Article article, EnabledStatus enabledStatus)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                FileAttachmentDataStore dataStore = new FileAttachmentDataStore(transaction);

                return dataStore.GetArticleAttachments(article, enabledStatus);
            }
        }

        public override void UpdateFileAttachment(FileAttachment attachment)
        {
            //Check attachment
            if (attachment != null)
                Attachment.FileHelper.CheckFile(attachment, attachment.Article.Category.AttachExtensions, attachment.Article.Category.AttachMaxSize);

            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                FileAttachmentDataStore dataStore = new FileAttachmentDataStore(transaction);

                dataStore.Update(attachment);

                transaction.Commit();
            }
        }

        public override void DeleteFileAttachment(FileAttachment attachment)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                FileAttachmentDataStore dataStore = new FileAttachmentDataStore(transaction);

                dataStore.Delete(attachment.Id);

                transaction.Commit();
            }
        }

        public override FileAttachment GetFileAttachment(string id)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                FileAttachmentDataStore dataStore = new FileAttachmentDataStore(transaction);

                FileAttachment attachment = dataStore.FindByKey(id);
                if (attachment == null)
                    throw new FileAttachNotFoundException(id);

                return attachment;
            }
        }

        public override FileAttachment GetFileAttachmentByName(Article article, string name, bool throwIfNotFound)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                FileAttachmentDataStore dataStore = new FileAttachmentDataStore(transaction);

                FileAttachment attachment = dataStore.FindByArticleVersion(article, name);
                if (attachment == null && throwIfNotFound)
                    throw new FileAttachNotFoundException(article.Name + "." + name);
                else if (attachment == null)
                    return null;

                return attachment;
            }
        }
        #endregion
        #endregion
    }
}
