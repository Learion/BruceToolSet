using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Web.Configuration;
using System.Configuration;
using Eucalypto.Common;
using NHibernateDataStore.Common;

namespace Eucalypto.Forum
{
    /// <summary>
    /// A static class used to comunicate with the ForumProvider.
    /// Use the configuration (web.config) to read the provider to use.
    /// </summary>
    public class ForumManager
    {
        static ForumManager()
        {
            //Get the feature's configuration info
            ForumProviderConfiguration qc =
                (ForumProviderConfiguration)ConfigurationManager.GetSection("forumManager");

            if (qc == null || qc.DefaultProvider == null || qc.Providers == null || qc.Providers.Count < 1)
                throw new ProviderException("You must specify a valid default provider for forumManager.");

            //Instantiate the providers
            providerCollection = new ForumProviderCollection();
            ProvidersHelper.InstantiateProviders(qc.Providers, providerCollection, typeof(ForumProvider));
            providerCollection.SetReadOnly();
            defaultProvider = providerCollection[qc.DefaultProvider];
            if (defaultProvider == null)
            {
                throw new ConfigurationErrorsException(
                    "You must specify a default provider for the forumManager.",
                    qc.ElementInformation.Properties["defaultProvider"].Source,
                    qc.ElementInformation.Properties["defaultProvider"].LineNumber);
            }
        }

        //Public feature API
        private static ForumProvider defaultProvider;
        private static ForumProviderCollection providerCollection;

        public static ForumProvider Provider
        {
            get{return defaultProvider;}
        }

        public static ForumProviderCollection Providers
        {
            get{return providerCollection;}
        }


        #region Forum category
        public static Category CreateCategory(string category, string displayName)
        {
            return Provider.CreateCategory(category, displayName);
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

        #region Forum topic
        /// <summary>
        /// Create the topic and the relative root message.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="owner"></param>
        /// <param name="title"></param>
        /// <param name="body"></param>
        /// <param name="attachment">Use null if you don't have any attachment</param>
        /// <param name="topic">Returns the topic created</param>
        /// <param name="rootMessage">Returns the message created</param>
        public static void CreateTopic(Category category, string owner,
                                          string title, string body, Attachment.FileInfo attachment)
        {
            Topic tp;
            Message msg;

            CreateTopic(category, owner, title, body, attachment, out tp, out msg);
        }

        /// <summary>
        /// Create the topic and the relative root message.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="owner"></param>
        /// <param name="title"></param>
        /// <param name="body"></param>
        /// <param name="attachment">Use null if you don't have any attachment</param>
        /// <param name="topic">Returns the topic created</param>
        /// <param name="rootMessage">Returns the message created</param>
        public static void CreateTopic(Category category, string owner,
                                          string title, string body, Attachment.FileInfo attachment,
                                          out Topic topic,
                                          out Message rootMessage)
        {
            Provider.CreateTopic(category, owner, title, body, attachment, out topic, out rootMessage);
        }

        public static void DeleteTopic(Topic topic)
        {
            Provider.DeleteTopic(topic);
        }

        public static Topic GetTopic(string id)
        {
            return Provider.GetTopic(id);
        }

        public static IList<Topic> GetTopics(Category category, DateTime fromDate, DateTime toDate)
        {
            return Provider.GetTopics(category, fromDate, toDate);
        }

        public static IList<Topic> GetTopics(Category category, PagingInfo paging)
        {
            return Provider.GetTopics(category, paging);
        }

        #endregion

        #region Messages

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="idParentMessage"></param>
        /// <param name="owner"></param>
        /// <param name="title"></param>
        /// <param name="body"></param>
        /// <param name="attachment">Use null if you don't have any attachment</param>
        /// <returns></returns>
        public static Message CreateMessage(Topic topic, string idParentMessage, string owner, string title, string body, Attachment.FileInfo attachment)
        {
            return Provider.CreateMessage(topic, idParentMessage, owner, title, body, attachment);
        }

        /// <summary>
        /// Get a list of messages for the specified topic ordered by InsertDate
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        public static IList<Message> GetMessagesByTopic(Topic topic)
        {
            return Provider.GetMessagesByTopic(topic);
        }

        public static int MessageCountByTopic(Topic topic)
        {
            return Provider.MessageCountByTopic(topic);
        }

        public static void DeleteMessage(Message message)
        {
            Provider.DeleteMessage(message);
        }

        public static Message GetMessage(string id)
        {
            return Provider.GetMessage(id);
        }

        public static IList<Message> FindMessages(Filter<string> categoryName,
                                   Filter<string> searchFor,
                                   Filter<string> owner,
                                   Filter<string> tag,  
                                   DateTime? fromDate, DateTime? toDate,
                                   PagingInfo paging)
        {
            return Provider.FindMessages(categoryName, searchFor, owner, tag, fromDate, toDate, paging);
        }
        #endregion
    }
}
