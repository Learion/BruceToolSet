using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Configuration;
using System.Net.Mail;
using Eucalypto.Common;
using NHibernateDataStore.Common;

namespace Eucalypto.News
{
    /// <summary>
    /// Implementation of NewsProvider that use NHibernate to save and retrive informations.
    ///
    /// Configuration:
    /// connectionStringName = the name of the connection string to use
    /// 
    /// </summary>
    public class EucalyptoNewsProvider : NewsProvider
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
                    throw new NewsCategoryNotFoundException(id);

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
                    throw new NewsCategoryNotFoundException(name);
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

        #region Items
        public override Item CreateItem(Category category, string owner,
                                        string title, string description,
                                        string url, string urlName, 
                                        DateTime newsDate)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ItemDataStore dataStore = new ItemDataStore(transaction);

                Item item = new Item(category, owner, title, description, url, urlName, newsDate);

                dataStore.Insert(item);

                transaction.Commit();

                return item;
            }
        }

        public override IList<Item> GetItems(Category category, PagingInfo paging)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ItemDataStore dataStore = new ItemDataStore(transaction);

                return dataStore.FindByCategory(category, paging);
            }
        }

        public override void UpdateItem(Item item)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ItemDataStore dataStore = new ItemDataStore(transaction);

                dataStore.Update(item);

                transaction.Commit();
            }
        }

        public override void DeleteItem(Item item)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ItemDataStore dataStore = new ItemDataStore(transaction);

                dataStore.Delete(item.Id);

                transaction.Commit();
            }
        }

        public override Item GetItem(string id)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ItemDataStore dataStore = new ItemDataStore(transaction);

                return dataStore.FindByKey(id);
            }
        }

        public override IList<Item> FindItems(Filter<string> categoryName,
                                            Filter<string> tag, 
                                           DateTime? fromDate, DateTime? toDate,
                                           PagingInfo paging)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ItemDataStore dataStore = new ItemDataStore(transaction);

                return dataStore.FindByFields(categoryName, tag, fromDate, toDate, paging);
            }
        }

        #endregion

        #endregion
    }
}
