using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using Eucalypto.Common;
using NHibernateDataStore.Common;

namespace Eucalypto.Forum
{
    /// <summary>
    /// ForumProvider abstract class.
    /// Defines the contract that Eucalypto implements to provide forum services using custom forum providers.
    /// Using this class you can insert, delete or read forum category, topic and messages.
    /// The main implementations is provided by the EucalyptoForumProvider class.
    /// </summary>
    public abstract class ForumProvider : ProviderBase
    {
        #region Forum category
        public abstract Category CreateCategory(string category, string displayName);

        public abstract void UpdateCategory(Category category);

        public abstract void DeleteCategory(Category category);

        public abstract Category GetCategory(string id);

        public abstract Category GetCategoryByName(string name, bool throwIfNotFound);

        public abstract IList<Category> GetAllCategories();
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
        public abstract void CreateTopic(Category category, string owner, 
                                          string title, string body, Attachment.FileInfo attachment,
                                          out Topic topic,
                                          out Message rootMessage);

        public abstract void DeleteTopic(Topic topic);

        public abstract Topic GetTopic(string id);

        public abstract IList<Topic> GetTopics(Category category, DateTime fromDate, DateTime toDate);

        public abstract IList<Topic> GetTopics(Category category, PagingInfo paging);
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
        public abstract Message CreateMessage(Topic topic, string idParentMessage, string owner, string title, string body, Attachment.FileInfo attachment);

        /// <summary>
        /// Get a list of messages for the specified topic ordered by InsertDate
        /// </summary>
        /// <param name="idTopic"></param>
        /// <returns></returns>
        public abstract IList<Message> GetMessagesByTopic(Topic topic);

        public abstract int MessageCountByTopic(Topic topic);
        
        public abstract void DeleteMessage(Message message);

        public abstract Message GetMessage(string id);

        public abstract IList<Message> FindMessages(Filter<string> categoryName,
                                           Filter<string> searchFor,
                                           Filter<string> owner,
                                           Filter<string> tag,  
                                           DateTime? fromDate, DateTime? toDate,
                                           PagingInfo paging);
        #endregion
    }
}
