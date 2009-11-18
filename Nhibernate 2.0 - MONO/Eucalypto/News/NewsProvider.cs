using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using Eucalypto.Common;
using NHibernateDataStore.Common;

namespace Eucalypto.News
{
    /// <summary>
    /// NewsProvider abstract class. 
    /// A NewsProvider can be used to store news.
    /// 
    /// </summary>
    public abstract class NewsProvider : ProviderBase
    {
        #region Category
        public abstract Category CreateCategory(string name, string displayName);

        public abstract void UpdateCategory(Category category);

        public abstract void DeleteCategory(Category category);

        public abstract Category GetCategory(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="throwIfNotFound">True to throw an exception if the entity is not found. If false and the entity is not found return false</param>
        /// <returns></returns>
        public abstract Category GetCategoryByName(string name, bool throwIfNotFound);

        public abstract IList<Category> GetAllCategories();
        #endregion

        #region Items
        public abstract Item CreateItem(Category category, string owner,
                                        string title, string description,
                                        string url, string urlName,
                                        DateTime newsDate);

        public abstract IList<Item> GetItems(Category category,
                                            PagingInfo paging);

        public abstract void UpdateItem(Item item);

        public abstract void DeleteItem(Item item);

        public abstract Item GetItem(string id);

        public abstract IList<Item> FindItems(Filter<string> categoryName,
                                            Filter<string> tag, 
                                           DateTime? fromDate, DateTime? toDate,
                                           PagingInfo paging);

        #endregion
    }
}
