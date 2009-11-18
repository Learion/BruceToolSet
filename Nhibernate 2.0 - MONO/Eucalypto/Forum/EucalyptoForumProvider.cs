using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Configuration;
using Eucalypto.Common;
using System.Web.Security;
using Eucalypto.Notification;
using NHibernateDataStore.Common;

namespace Eucalypto.Forum
{
    /// <summary>
    /// Implementation of ForumProvider using Eucalypto database.
    /// Use a NotificationProvider to send a notification when a user responde to a message.
    /// 
    /// Configuration:
    /// connectionStringName = the name of the connection string to use
    /// notificationProvider = the name of the NotificationProvider to use. Empty to don't use a notification provider.
    /// 
    /// Notification parameters used:
    /// ?title? = title of the message
    /// ?bodyhtml? = complete html of the message
    /// ?bodytext? = short description of the message
    /// ?user? = user name
    /// ?idmessage? = id of the message
    /// ?idforum? = id of the forum
    /// ?idtopic? = id of the topic
    /// ?forum? = name of the forum
    /// ?forumDisplayName? = description of the forum
    /// </summary>
    public class EucalyptoForumProvider : ForumProvider
    {
        /// <summary>
        /// the name of the property of the profile to check if the user wants to receive notifications
        /// </summary>
        public String ReceiveNotificationPropertyInProfile { get; set; }

        public override void Initialize(String name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (String.IsNullOrEmpty(name))
                name = "EucalyptoForumProvider";

            base.Initialize(name, config);


            ProviderName = name;

            //Connection string
            var connName = ExtractConfigValue(config, "connectionStringName", null);
            Configuration = ConfigurationHelper.Create(connName);

            ReceiveNotificationPropertyInProfile = ExtractConfigValue(config, "receiveNotificationPropertyInProfile", null);

            //Notification provider
            string notificationProviderName = ExtractConfigValue(config, "notificationProvider", null);
            if (!String.IsNullOrEmpty(notificationProviderName))
            {
                NotificationProvider = Notification.NotificationManager.Providers[notificationProviderName];
                if (NotificationProvider == null)
                {
                    // try to use DefaultProvider
                    NotificationProvider = Notification.NotificationManager.GetDefaultNotificationProvider();
                    if (NotificationProvider == null)
                    {
                        throw new EucalyptoException("NotificationProvider " + notificationProviderName + " not defined");
                    }
                }

            }


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
        private static string ExtractConfigValue(System.Collections.Specialized.NameValueCollection config, string key, string defaultValue)
        {
            var val = config[key];
            if (val == null)
                return defaultValue;
            
            config.Remove(key);
            return val;
        }

        #region Properties

        /// <summary>
        /// The Name of the Provider
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// The ConnectionParameters used to connect to Data Repository
        /// </summary>
        public ConnectionParameters Configuration { get; private set; }

        /// <summary>
        /// Gets or sets the NotificationProvider to send notifications to users when answering messages.
        /// Leave blank the configuration to don't use a notification provider.
        /// </summary>
        public NotificationProvider NotificationProvider { get; private set; }

        #endregion

        #region Methods

        #region Forum category
        public override Category CreateCategory(string name, string displayName)
        {
            using (var transaction = new TransactionScope(Configuration))
            {
                var dataStore = new CategoryDataStore(transaction);

                var category = new Category(name, displayName);

                dataStore.Insert(category);

                transaction.Commit();

                return category;
            }
        }

        public override void UpdateCategory(Category category)
        {
            using (TransactionScope transaction = new TransactionScope(Configuration))
            {
                CategoryDataStore dataStore = new CategoryDataStore(transaction);

                dataStore.Update(category);

                transaction.Commit();
            }
        }

        public override void DeleteCategory(Category category)
        {
            using (TransactionScope transaction = new TransactionScope(Configuration))
            {
                CategoryDataStore dataStore = new CategoryDataStore(transaction);

                dataStore.Delete(category.Id);

                transaction.Commit();
            }
        }

        public override Category GetCategory(string id)
        {
            using (TransactionScope transaction = new TransactionScope(Configuration))
            {
                CategoryDataStore dataStore = new CategoryDataStore(transaction);

                Category category = dataStore.FindByKey(id);
                if (category == null)
                    throw new ForumCategoryNotFoundException(id);

                return category;
            }
        }

        public override Category GetCategoryByName(string name, bool throwIfNotFound)
        {
            using (TransactionScope transaction = new TransactionScope(Configuration))
            {
                CategoryDataStore dataStore = new CategoryDataStore(transaction);

                Category category = dataStore.FindByName(name);
                if (category == null && throwIfNotFound)
                    throw new ForumCategoryNotFoundException(name);
                else if (category == null)
                    return null;

                return category;
            }
        }

        public override IList<Category> GetAllCategories()
        {
            using (TransactionScope transaction = new TransactionScope(Configuration))
            {
                CategoryDataStore dataStore = new CategoryDataStore(transaction);

                return dataStore.FinAll();
            }
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
        public override void CreateTopic(Category category, string owner,
                                          string title, string body, Attachment.FileInfo attachment,
                                          out Topic topic,
                                          out Message rootMessage)
        {
            //Check attachment
            if (attachment != null)
                Attachment.FileHelper.CheckFile(attachment, category.AttachExtensions, category.AttachMaxSize);

            using (TransactionScope transaction = new TransactionScope(Configuration))
            {
                CategoryDataStore forumStore = new CategoryDataStore(transaction);
                forumStore.Attach(category);

                //Insert topic
                TopicDataStore topicStore = new TopicDataStore(transaction);
                topic = new Topic(category, owner, title);
                topicStore.Insert(topic);

                //Insert root message
                MessageDataStore messageStore = new MessageDataStore(transaction);
                rootMessage = new Message(topic, null, owner, title, body, attachment);
                messageStore.Insert(rootMessage);

                transaction.Commit();
            }
        }

        public override void DeleteTopic(Topic topic)
        {
            using (TransactionScope transaction = new TransactionScope(Configuration))
            {
                TopicDataStore dataStore = new TopicDataStore(transaction);

                dataStore.Delete(topic.Id);

                transaction.Commit();
            }
        }

        public override Topic GetTopic(string id)
        {
            using (TransactionScope transaction = new TransactionScope(Configuration))
            {
                TopicDataStore dataStore = new TopicDataStore(transaction);

                Topic topic = dataStore.FindByKey(id);
                if (topic == null)
                    throw new TopicNotFoundException(id);

                return topic;
            }
        }

        public override IList<Topic> GetTopics(Category category, DateTime fromDate, DateTime toDate)
        {
            using (TransactionScope transaction = new TransactionScope(Configuration))
            {
                TopicDataStore dataStore = new TopicDataStore(transaction);

                return dataStore.Find(category, fromDate, toDate);
            }
        }

        public override IList<Topic> GetTopics(Category category, PagingInfo paging)
        {
            using (TransactionScope transaction = new TransactionScope(Configuration))
            {
                TopicDataStore dataStore = new TopicDataStore(transaction);

                return dataStore.Find(category, paging);
            }
        }

        #endregion

        #region Messages
        public override Message CreateMessage(Topic topic, string idParentMessage, string owner, string title, string body, Attachment.FileInfo attachment)
        {
            //Check attachment
            if (attachment != null)
                Attachment.FileHelper.CheckFile(attachment, topic.Category.AttachExtensions, topic.Category.AttachMaxSize);

            using (TransactionScope transaction = new TransactionScope(Configuration))
            {
                TopicDataStore topicStore = new TopicDataStore(transaction);
                topicStore.Attach(topic);

                MessageDataStore messageStore = new MessageDataStore(transaction);
                Message parentMessage = messageStore.FindByKey(idParentMessage);
                if (parentMessage == null)
                    throw new MessageNotFoundException(idParentMessage);

                Message newMessage = new Message(topic, idParentMessage, owner, title, body, attachment);

                messageStore.Insert(newMessage);

                transaction.Commit();

                //Notify user for answer
                NotifyUserReply(parentMessage, newMessage);

                return newMessage;
            }
        }

        /// <summary>
        /// Get a list of messages for the specified topic ordered by InsertDate
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        public override IList<Message> GetMessagesByTopic(Topic topic)
        {
            //TODO Evaluate if is better to returns a light object to don't load all the attachments and the forum data

            using (TransactionScope transaction = new TransactionScope(Configuration))
            {
                MessageDataStore store = new MessageDataStore(transaction);

                return store.FindByTopic(topic);
            }
        }

        public override int MessageCountByTopic(Topic topic)
        {
            using (TransactionScope transaction = new TransactionScope(Configuration))
            {
                MessageDataStore store = new MessageDataStore(transaction);

                return store.MessageCountByTopic(topic);
            }
        }

        public override void DeleteMessage(Message message)
        {
            using (TransactionScope transaction = new TransactionScope(Configuration))
            {
                MessageDataStore store = new MessageDataStore(transaction);

                InternalDeleteMessage(store, transaction, message);

                transaction.Commit();
            }
        }

        /// <summary>
        /// Recursively delete all the messages and the relative attachments
        /// </summary>
        protected virtual void InternalDeleteMessage(MessageDataStore store,
                                                TransactionScope transaction, Message obj)
        {
            //Recursively delete any child message
            IList<Message> children = store.FindByParent(obj.Id);
            foreach (Message childMsg in children)
                InternalDeleteMessage(store, transaction, childMsg);

            store.Delete(obj.Id);
        }

        public override Message GetMessage(string id)
        {
            using (TransactionScope transaction = new TransactionScope(Configuration))
            {
                MessageDataStore store = new MessageDataStore(transaction);
                Message msg = store.FindByKey(id);
                if (msg == null)
                    throw new MessageNotFoundException(id);

                return msg;
            }
        }

        public override IList<Message> FindMessages(Filter<string> categoryName,
                                           Filter<string> searchFor,
                                           Filter<string> owner,
                                           Filter<string> tag,
                                           DateTime? fromDate, DateTime? toDate,
                                           PagingInfo paging)
        {
            //TODO Evaluate if is better to returns a light object to don't load all the attachments and the forum data

            using (TransactionScope transaction = new TransactionScope(Configuration))
            {
                MessageDataStore store = new MessageDataStore(transaction);

                return store.FindByFields(categoryName, searchFor, owner, tag, fromDate, toDate, paging);
            }
        }

        #endregion

        private bool UserCanReceiveNotification(MembershipUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrEmpty(user.Email))
                return false;
            else
            {
                System.Web.Profile.ProfileBase profile = System.Web.Profile.ProfileBase.Create(user.UserName);

                object profReceiveNotification = profile[ReceiveNotificationPropertyInProfile];
                if (profReceiveNotification is bool)
                    return (bool)profReceiveNotification;
                else
                    return false;
            }
        }
        #region Notifications
        /// <summary>
        /// Send a notification to the user of the parent message
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="message">Answer</param>
        private void NotifyUserReply(Message parent, Message message)
        {
            if (NotificationProvider == null)
                return;
            if (parent.Owner == null)
                return;

            XHTMLText xhtml = new XHTMLText();
            xhtml.Load(message.Body);

            //Create the parameters list
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("?title?", message.Title);
            parameters.Add("?bodyhtml?", message.Body);
            parameters.Add("?bodytext?", xhtml.GetShortText());
            parameters.Add("?user?", message.Owner);
            parameters.Add("?idmessage?", message.Id);
            parameters.Add("?idforum?", parent.Topic.Category.Id);
            parameters.Add("?idtopic?", parent.Topic.Id);
            parameters.Add("?forum?", parent.Topic.Category.Name);
            parameters.Add("?forumDisplayName?", parent.Topic.Category.DisplayName);

            if (parent.Owner != null && parent.Owner.Length > 0)
            {
                System.Web.Security.MembershipUser destinationUser = System.Web.Security.Membership.GetUser(parent.Owner);
                if (destinationUser != null && UserCanReceiveNotification(destinationUser))
                //NotificationProvider.UserCanReceiveNotification(destinationUser
                {
                    NotificationProvider.NotifyUser(destinationUser, parameters);
                }
            }
        }
        #endregion
        #endregion
    }
}
