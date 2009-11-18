using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Configuration;
using NHibernateDataStore.Common;

namespace Eucalypto.Profile
{
    /// <summary>
    /// A implementation of a System.Web.Profile.ProfileProvider class that use the Eucalypto classes.
    /// See MSDN System.Web.Profile.ProfileProvider documentation for more informations about ProfileProvider.
    /// 
    /// For now doesn't calculate the size of the profile data stored and returns always -1.
    /// 
    /// Configuration:
    /// applicationName = the name of the application used to bind profile data
    /// connectionStringName = the name of the connection string to use
    /// 
    /// For more details on the implementation look at: "Implementing a Profile Provider" http://msdn2.microsoft.com/en-us/library/0580x1f5.aspx 
    /// </summary>
    public class EucalyptoProfileProvider : ProfileProvider
    {
        private const string CONTEXT_USERNAME = "UserName";
        private const string CONTEXT_ISAUTHENTICATED = "IsAuthenticated";
        private const string PROP_ATTRIBUTE_ALLOWANONYMOUS = "AllowAnonymous";

        private ConnectionParameters mConfiguration;

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "EucalyptoProfileProvider";

            base.Initialize(name, config);


            this.mProviderName = name;
            this.mApplicationName = ExtractConfigValue(config, "applicationName", ConnectionParameters.DEFAULT_APP); //System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath

            string connName = ExtractConfigValue(config, "connectionStringName", null);
            mConfiguration = ConfigurationHelper.Create(connName);

            //CommandTimeout = int.Parse(ExtractConfigValue(config, "commandTimeout", "30"));


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
        private string mApplicationName;
        public override string ApplicationName
        {
            get { return mApplicationName; }
            set { mApplicationName = value; }
        }
        //private int mCommandTimeout;
        //public override int CommandTimeout
        //{
        //    get{return mCommandTimeout;}
        //    set{mCommandTimeout = value;}
        //}
        #endregion

        #region Methods
        private void LogException(Exception exception, string action)
        {
            LoggerFacade.Log.Error(GetType(), "Exception on " + action, exception);
        }

        private IList<ProfileProperty> GetProperties(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return null;

            IList<ProfileProperty> properties;
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ProfileUserDataStore userStore = new ProfileUserDataStore(transaction);
                ProfileUser user = userStore.FindByName(ApplicationName, userName);
                if (user == null)
                    return null;

                ProfilePropertyDataStore propStore = new ProfilePropertyDataStore(transaction);

                properties = propStore.FindByUser(user);

                //Update the last activity date
                user.LastActivityDate = DateTime.Now;

                transaction.Commit();
            }

            return properties;
        }

        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection properties)
        {
            SettingsPropertyValueCollection svc = new SettingsPropertyValueCollection();

            if (properties.Count == 0)
                return svc;

            //Create the default structure of the properties
            foreach (SettingsProperty prop in properties)
            {
                if (prop.SerializeAs == SettingsSerializeAs.ProviderSpecific)
                    if (prop.PropertyType.IsPrimitive || prop.PropertyType == typeof(string))
                        prop.SerializeAs = SettingsSerializeAs.String;
                    else
                        prop.SerializeAs = SettingsSerializeAs.Xml;

                svc.Add(new SettingsPropertyValue(prop));
            }

            //Load the properties from DB
            IList<ProfileProperty> dbProperties = GetProperties((string)context[CONTEXT_USERNAME]);
            if (dbProperties != null)
            {
                foreach (ProfileProperty dbProp in dbProperties)
                {
                    SettingsPropertyValue propVal = svc[dbProp.Name];
                    if (propVal == null) // property not found
                        continue;

                    if (dbProp.BinaryValue != null)
                    {
                        propVal.SerializedValue = dbProp.BinaryValue;
                    }
                    else if (dbProp.StringValue != null)
                    {
                        propVal.SerializedValue = dbProp.StringValue;
                    }
                    else //null
                    {
                        propVal.PropertyValue = null;
                        propVal.IsDirty = false;
                        propVal.Deserialized = true;
                    }
                }
            }

            return svc;
        }

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection properties)
        {
            string username = (string)context[CONTEXT_USERNAME];
            ProfileType profileType;
            if ((bool)context[CONTEXT_ISAUTHENTICATED])
                profileType = ProfileType.Authenticated;
            else
                profileType = ProfileType.Anonymous;

            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ProfilePropertyDataStore propStore = new ProfilePropertyDataStore(transaction);
                ProfileUserDataStore userStore = new ProfileUserDataStore(transaction);

                ProfileUser user = userStore.FindByName(ApplicationName, username);
                //Create the user if not exist
                if (user == null)
                {
                    user = new ProfileUser(ApplicationName, username, profileType);
                    userStore.Insert(user);
                }

                bool userChanged = false;

                foreach (SettingsPropertyValue propValue in properties)
                {
                    if (propValue.IsDirty)
                    {
                        if (profileType == ProfileType.Anonymous)
                        {
                            bool allowAnonymous = (bool)propValue.Property.Attributes[PROP_ATTRIBUTE_ALLOWANONYMOUS];
                            if (!allowAnonymous)
                                continue;
                        }

                        userChanged = true;

                        ChangeProfileProperty(propStore, user, propValue);
                    }
                }

                user.LastActivityDate = DateTime.Now;
                if (userChanged)
                    user.LastPropertyChangedDate = DateTime.Now;

                transaction.Commit();
            }
        }

        private void ChangeProfileProperty(ProfilePropertyDataStore propStore, ProfileUser user, SettingsPropertyValue propValue)
        {
            ProfileProperty dbProperty = propStore.FindByPropertyName(user, propValue.Name);
            if (dbProperty == null) //Create the property if not found
            {
                dbProperty = new ProfileProperty(user, propValue.Name);
                propStore.Insert(dbProperty);
            }

            //The property is already deserialized and is null
            if (propValue.Deserialized && propValue.PropertyValue == null)
                dbProperty.SetNull();
            else //Property is not null
            {
                object serializedVal = propValue.SerializedValue;

                if (serializedVal == null) //null
                    dbProperty.SetNull();
                else if (serializedVal is string) //string
                    dbProperty.SetValue((string)serializedVal);
                else if (serializedVal is byte[]) //binary
                    dbProperty.SetValue((byte[])serializedVal);
                else
                    throw new ProfileValueNotSupportedException(propValue.Name);
            }
        }


        public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption,
                                                   DateTime userInactiveSinceDate)
        {
            ProfileType? profileType = null;
            if (authenticationOption == ProfileAuthenticationOption.Anonymous)
                profileType = ProfileType.Anonymous;
            else if (authenticationOption == ProfileAuthenticationOption.Authenticated)
                profileType = ProfileType.Authenticated;

            int profilesDeleted = 0;

            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ProfileUserDataStore profileStore = new ProfileUserDataStore(transaction);

                IList<ProfileUser> users = profileStore.FindByFields(ApplicationName, null, userInactiveSinceDate, profileType, PagingInfo.All);

                profilesDeleted = users.Count;

                foreach (ProfileUser user in users)
                {
                    profileStore.Delete(user.Id);
                }

                transaction.Commit();
            }

            return profilesDeleted;
        }

        public override int DeleteProfiles(string[] usernames)
        {
            int profilesDeleted = 0;

            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ProfileUserDataStore profileStore = new ProfileUserDataStore(transaction);

                foreach (string userName in usernames)
                {
                    IList<ProfileUser> users = profileStore.FindByFields(ApplicationName, userName, null, null, PagingInfo.All);
                    profilesDeleted += users.Count;
                    foreach (ProfileUser user in users)
                    {
                        profileStore.Delete(user.Id);
                    }
                }

                transaction.Commit();
            }

            return profilesDeleted;
        }

        public override int DeleteProfiles(ProfileInfoCollection profiles)
        {
            int profilesDeleted = 0;

            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ProfileUserDataStore profileStore = new ProfileUserDataStore(transaction);

                foreach (ProfileInfo profileInfo in profiles)
                {
                    ProfileUser user = profileStore.FindByName(ApplicationName, profileInfo.UserName);
                    if (user != null)
                    {
                        profileStore.Delete(user.Id);

                        profilesDeleted++;
                    }
                }

                transaction.Commit();
            }

            return profilesDeleted;
        }

        private ProfileInfoCollection ProfileUsersToProfileInfoCollection(IList<ProfileUser> users)
        {
            ProfileInfoCollection profilesCollection = new ProfileInfoCollection();

            foreach (ProfileUser user in users)
            {
                bool isAnonymous;
                if (user.ProfileType == ProfileType.Anonymous)
                    isAnonymous = true;
                else
                    isAnonymous = false;
                ProfileInfo profileInfo = new ProfileInfo(user.Name, isAnonymous, user.LastActivityDate, user.LastPropertyChangedDate, -1);
                profilesCollection.Add(profileInfo);
            }

            return profilesCollection;
        }

        public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, 
            string usernameToMatch, DateTime userInactiveSinceDate, 
            int pageIndex, int pageSize, out int totalRecords)
        {
            ProfileType? profileType = null;
            if (authenticationOption == ProfileAuthenticationOption.Anonymous)
                profileType = ProfileType.Anonymous;
            else if (authenticationOption == ProfileAuthenticationOption.Authenticated)
                profileType = ProfileType.Authenticated;

            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ProfileUserDataStore profileStore = new ProfileUserDataStore(transaction);

                PagingInfo paging = new PagingInfo(pageSize, pageIndex);
                IList<ProfileUser> users = profileStore.FindByFields(ApplicationName,
                        usernameToMatch, userInactiveSinceDate, profileType, paging);
                totalRecords = (int)paging.RowCount;

                return ProfileUsersToProfileInfoCollection(users);
            }
        }

        public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, 
                        string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            ProfileType? profileType = null;
            if (authenticationOption == ProfileAuthenticationOption.Anonymous)
                profileType = ProfileType.Anonymous;
            else if (authenticationOption == ProfileAuthenticationOption.Authenticated)
                profileType = ProfileType.Authenticated;

            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ProfileUserDataStore profileStore = new ProfileUserDataStore(transaction);

                PagingInfo paging = new PagingInfo(pageSize, pageIndex);
                IList<ProfileUser> users = profileStore.FindByFields(ApplicationName,
                        usernameToMatch, null, profileType, paging);
                totalRecords = (int)paging.RowCount;

                return ProfileUsersToProfileInfoCollection(users);
            }
        }

        public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, 
                            DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            ProfileType? profileType = null;
            if (authenticationOption == ProfileAuthenticationOption.Anonymous)
                profileType = ProfileType.Anonymous;
            else if (authenticationOption == ProfileAuthenticationOption.Authenticated)
                profileType = ProfileType.Authenticated;

            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ProfileUserDataStore profileStore = new ProfileUserDataStore(transaction);

                PagingInfo paging = new PagingInfo(pageSize, pageIndex);
                IList<ProfileUser> users = profileStore.FindByFields(ApplicationName,
                        null, userInactiveSinceDate, profileType, paging);
                totalRecords = (int)paging.RowCount;

                return ProfileUsersToProfileInfoCollection(users);
            }
        }

        public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, 
                        int pageIndex, int pageSize, out int totalRecords)
        {
            ProfileType? profileType = null;
            if (authenticationOption == ProfileAuthenticationOption.Anonymous)
                profileType = ProfileType.Anonymous;
            else if (authenticationOption == ProfileAuthenticationOption.Authenticated)
                profileType = ProfileType.Authenticated;

            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ProfileUserDataStore profileStore = new ProfileUserDataStore(transaction);

                PagingInfo paging = new PagingInfo(pageSize, pageIndex);
                IList<ProfileUser> users = profileStore.FindByFields(ApplicationName,
                        null, null, profileType, paging);
                totalRecords = (int)paging.RowCount;

                return ProfileUsersToProfileInfoCollection(users);
            }
        }

        public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, 
                            DateTime userInactiveSinceDate)
        {
            ProfileType? profileType = null;
            if (authenticationOption == ProfileAuthenticationOption.Anonymous)
                profileType = ProfileType.Anonymous;
            else if (authenticationOption == ProfileAuthenticationOption.Authenticated)
                profileType = ProfileType.Authenticated;

            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                ProfileUserDataStore profileStore = new ProfileUserDataStore(transaction);

                IList<ProfileUser> users = profileStore.FindByFields(ApplicationName,
                        null, userInactiveSinceDate, profileType, PagingInfo.All);

                return users.Count;
            }
        }

        #endregion

    }
}
