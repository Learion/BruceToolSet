using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Web.Configuration;
using System.Configuration;
using Eucalypto.Common;
using NHibernateDataStore.Common;

namespace Eucalypto.News
{
    public class NewsManager
    {
        static NewsManager()
        {
            //Get the feature's configuration info
            NewsProviderConfiguration qc =
                (NewsProviderConfiguration)ConfigurationManager.GetSection("newsManager");

            if (qc == null || qc.DefaultProvider == null || qc.Providers == null || qc.Providers.Count < 1)
                throw new ProviderException("You must specify a valid default provider for newsManager.");

            //Instantiate the providers
            providerCollection = new NewsProviderCollection();
            ProvidersHelper.InstantiateProviders(qc.Providers, providerCollection, typeof(NewsProvider));
            providerCollection.SetReadOnly();
            defaultProvider = providerCollection[qc.DefaultProvider];
            if (defaultProvider == null)
            {
                throw new ConfigurationErrorsException(
                    "You must specify a default provider for the newsManager.",
                    qc.ElementInformation.Properties["defaultProvider"].Source,
                    qc.ElementInformation.Properties["defaultProvider"].LineNumber);
            }
        }

        //Public feature API
        private static NewsProvider defaultProvider;
        private static NewsProviderCollection providerCollection;

        public static NewsProvider Provider
        {
            get{return defaultProvider;}
        }

        public static NewsProviderCollection Providers
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

        #region News
        public static Item CreateItem(Category category, string owner,
                                        string title, string description,
                                        string url, string urlName,
                                        DateTime newsDate)
        {
            return Provider.CreateItem(category, owner, title, description, url, urlName, newsDate);
        }

        public static IList<Item> GetItems(Category category, PagingInfo paging)
        {
            return Provider.GetItems(category, paging);
        }

        public static void UpdateItem(Item item)
        {
            Provider.UpdateItem(item);
        }

        public static void DeleteItem(Item item)
        {
            Provider.DeleteItem(item);
        }

        public static Item GetItem(string id)
        {
            return Provider.GetItem(id);
        }
        public static IList<Item> FindItems(Filter<string> categoryName,
                                    Filter<string> tag, 
                                   DateTime? fromDate, DateTime? toDate,
                                   PagingInfo paging)
        {
            return Provider.FindItems(categoryName, tag, fromDate, toDate, paging);
        }
        #endregion

        #endregion
    }
}
