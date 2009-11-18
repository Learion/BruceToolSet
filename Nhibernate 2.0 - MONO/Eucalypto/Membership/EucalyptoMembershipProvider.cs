using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using System.Configuration;
using NHibernateDataStore.Common;

namespace Eucalypto.Membership
{
    /// <summary>
    /// A implementation of a System.Web.Security.MembershipProvider class that use the Eucalypto classes.
    /// See MSDN System.Web.Security.MembershipProvider documentation for more informations about MembershipProvider.
    /// 
    /// For now implements only the Hashed password format.
    /// 
    /// You must use the EucalyptoMembershipProvider with the EucalyptoRoleProvider.
    /// 
    /// Some methods don't returns an exception but a true or false value. In this case the exception is logged used the log4net configuration (see Log class).
    /// 
    /// For more implementation details look at: "Implementing a Membership Provider" http://msdn2.microsoft.com/en-us/library/f1kyba5e.aspx
    /// 
    /// Note that the name and email field are used as case insensitive, the pasword is case sensitive.
    /// </summary>
    public class EucalyptoMembershipProvider : System.Web.Security.MembershipProvider
    {
        private ConnectionParameters mConfiguration;

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "EucalyptoMembershipProvider";

            base.Initialize(name, config);


            this.mProviderName = name;
            this.mApplicationName = ExtractConfigValue(config, "applicationName", ConnectionParameters.DEFAULT_APP); //System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath
            this.mEnablePasswordReset = bool.Parse(ExtractConfigValue(config, "enablePasswordReset", "true"));
            this.mEnablePasswordRetrieval = false; //bool.Parse(GetConfigValue(config["enablePasswordRetrieval"], "false"));
            this.mMaxInvalidPasswordAttempts = int.Parse(ExtractConfigValue(config, "maxInvalidPasswordAttempts", "5"));
            this.mMinRequiredNonAlphanumericCharacters = int.Parse(ExtractConfigValue(config, "minRequiredNonAlphanumericCharacters", "1"));
            this.mMinRequiredPasswordLength = int.Parse(ExtractConfigValue(config, "minRequiredPasswordLength", "7"));
            this.mPasswordAttemptWindow = int.Parse(ExtractConfigValue(config, "passwordAttemptWindow", "10"));
            this.mPasswordFormat = MembershipPasswordFormat.Hashed; //Enum.Parse(typeof(MembershipPasswordFormat), GetConfigValue(config["passwordFormat"], "Hashed"));
            this.mPasswordStrengthRegularExpression = ExtractConfigValue(config, "passwordStrengthRegularExpression", "");
            this.mRequiresQuestionAndAnswer = bool.Parse(ExtractConfigValue(config, "requiresQuestionAndAnswer", "false"));
            this.mRequiresUniqueEmail = bool.Parse(ExtractConfigValue(config, "requiresUniqueEmail", "true"));


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
        private string mApplicationName;
        public override string ApplicationName
        {
            get { return mApplicationName; }
            set { mApplicationName = value; }
        }
        private bool mEnablePasswordReset;
        public override bool EnablePasswordReset
        {
            get { return mEnablePasswordReset; }
        }
        private bool mEnablePasswordRetrieval;
        public override bool EnablePasswordRetrieval
        {
            get { return mEnablePasswordRetrieval; }
        }
        private int mMaxInvalidPasswordAttempts;
        public override int MaxInvalidPasswordAttempts
        {
            get { return mMaxInvalidPasswordAttempts; }
        }
        private int mMinRequiredNonAlphanumericCharacters;
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return mMinRequiredNonAlphanumericCharacters; }
        }
        private int mMinRequiredPasswordLength;
        public override int MinRequiredPasswordLength
        {
            get { return mMinRequiredPasswordLength; }
        }
        private int mPasswordAttemptWindow;
        public override int PasswordAttemptWindow
        {
            get { return mPasswordAttemptWindow; }
        }
        private System.Web.Security.MembershipPasswordFormat mPasswordFormat;
        public override System.Web.Security.MembershipPasswordFormat PasswordFormat
        {
            get { return mPasswordFormat; }
        }
        private string mPasswordStrengthRegularExpression;
        public override string PasswordStrengthRegularExpression
        {
            get { return mPasswordStrengthRegularExpression; }
        }
        private bool mRequiresQuestionAndAnswer;
        public override bool RequiresQuestionAndAnswer
        {
            get { return mRequiresQuestionAndAnswer; }
        }
        private bool mRequiresUniqueEmail;
        public override bool RequiresUniqueEmail
        {
            get { return mRequiresUniqueEmail; }
        }
        #endregion

        #region Methods

        private MembershipUser UserToMembershipUser(User user)
        {
            return new MembershipUser(ProviderName, user.Name, user.Id, user.EMail, user.PasswordQuestion, user.Comment, user.Enabled, user.IsLockedOut, SafeDate(user.InsertDate), SafeDate(user.LastLoginDate), SafeDate(user.LastActivityDate), SafeDate(user.LastPasswordChangedDate), SafeDate(user.LockedOutDate));
        }
        private DateTime SafeDate(DateTime? date)
        {
            return date != null ? date.Value : new DateTime();
        }

        private void LogException(Exception exception, string action)
        {
          LoggerFacade.Log.Error(GetType(), "Exception on " + action, exception);
        }

        /// <summary>
        /// Check if the password support the required streght
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="isNew"></param>
        private void ValidatePassword(string user, string password, bool isNew)
        {
            //Check if empty
            if (password == null)
                throw new MembershipPasswordException("Password validation failed");

            //Check minimum length
            if (password.Length < MinRequiredPasswordLength)
                throw new MembershipPasswordException("Password validation failed");

            //Chec minimum number of digits
            int count = 0;
            for (int i = 0; i < password.Length; i++)
            {
                if (!char.IsLetterOrDigit(password, i))
                    count++;
            }
            if (count < MinRequiredNonAlphanumericCharacters)
                throw new MembershipPasswordException("Password validation failed");

            //Check with the regular expression
            if (PasswordStrengthRegularExpression.Length > 0)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(password, PasswordStrengthRegularExpression))
                    throw new MembershipPasswordException("Password validation failed");
            }


            //Use a custom check if defined
            ValidatePasswordEventArgs args =
              new ValidatePasswordEventArgs(user, password, isNew);

            OnValidatingPassword(args);
            if (args.Cancel)
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new MembershipPasswordException("Password validation failed");
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            try
            {
                ValidatePassword(username, newPassword, false);

                using (TransactionScope transaction = new TransactionScope(mConfiguration))
                {
                    UserDataStore dataStore = new UserDataStore(transaction);
                    User user = dataStore.FindByName(ApplicationName, username);
                    if (user == null)
                        throw new UserNotFoundException(username);

                    if (user.CheckPassword(oldPassword) == false)
                        throw new UserNotFoundException(username);

                    user.ChangePassword(newPassword);

                    transaction.Commit();
                }

                return true;
            }
            catch (Exception ex)
            {
                LogException(ex, "ChangePassword");
                return false;
            }
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope(mConfiguration))
                {
                    UserDataStore dataStore = new UserDataStore(transaction);
                    User user = dataStore.FindByName(ApplicationName, username);
                    if (user == null)
                        throw new UserNotFoundException(username);

                    if (user.CheckPassword(password) == false)
                        throw new UserNotFoundException(username);

                    user.ChangePasswordQuestionAnswer(newPasswordQuestion, newPasswordAnswer);

                    transaction.Commit();
                }

                return true;
            }
            catch (Exception ex)
            {
                LogException(ex, "ChangePasswordQuestionAndAnswer");
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="passwordQuestion"></param>
        /// <param name="passwordAnswer"></param>
        /// <param name="isApproved"></param>
        /// <param name="providerUserKey">Not used</param>
        /// <param name="status"></param>
        /// <returns></returns>
        public override System.Web.Security.MembershipUser CreateUser(string username, string password, 
                                                                    string email, string passwordQuestion, 
                                                                    string passwordAnswer, bool isApproved, 
                                                                    object providerUserKey, 
                                                                    out System.Web.Security.MembershipCreateStatus status)
        {
            try
            {
                //Validate password
                ValidatePassword(username, password, true);

                using (TransactionScope transaction = new TransactionScope(mConfiguration))
                {
                    UserDataStore dataStore = new UserDataStore(transaction);

                    //Check name
                    if (dataStore.FindByName(ApplicationName, username) != null)
                    {
                        status = MembershipCreateStatus.DuplicateUserName;
                        return null;
                    }

                    //Check email
                    if (RequiresUniqueEmail)
                    {
                        if (email == null || email.Length == 0)
                        {
                            status = MembershipCreateStatus.InvalidEmail;
                            return null;
                        }
                        if (dataStore.FindByEmail(ApplicationName, email).Count > 0)
                        {
                            status = MembershipCreateStatus.DuplicateEmail;
                            return null;
                        }
                    }


                    User user = new User(ApplicationName, username);
                    user.EMail = email;
                    user.ChangePassword(password);
                    user.ChangePasswordQuestionAnswer(passwordQuestion, passwordAnswer);
                    user.Enabled = isApproved;

                    dataStore.Insert(user);

                    transaction.Commit();

                    status = MembershipCreateStatus.Success;
                    return UserToMembershipUser(user);
                }
            }
            catch (CodeInvalidCharsException ex) //this exception is caused by an invalid user Name
            {
                LogException(ex, "CreateUser");
                status = MembershipCreateStatus.InvalidUserName;
                return null;
            }
            catch (MembershipPasswordException ex)
            {
                LogException(ex, "CreateUser");
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }
            catch (Exception ex)
            {
                LogException(ex, "CreateUser");
                status = MembershipCreateStatus.ProviderError;
                return null;
            }
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope(mConfiguration))
                {
                    if (deleteAllRelatedData)
                    {
                        Roles.UserInRoleDataStore userInRoleStore = new Eucalypto.Roles.UserInRoleDataStore(transaction);
                        IList<Roles.UserInRole> userInRoles = userInRoleStore.FindForUser(ApplicationName, username);
                        foreach (Roles.UserInRole ur in userInRoles)
                            userInRoleStore.Delete(ur.Id);
                    }

                    UserDataStore dataStore = new UserDataStore(transaction);
                    User user = dataStore.FindByName(ApplicationName, username);
                    if (user == null)
                        throw new UserNotFoundException(username);

                    dataStore.Delete(user.Id);

                    transaction.Commit();
                }

                return true;
            }
            catch (Exception ex)
            {
                LogException(ex, "DeleteUser");
                return false;
            }
        }

        public override System.Web.Security.MembershipUserCollection FindUsersByEmail(string emailToMatch, 
                                                int pageIndex, int pageSize, out int totalRecords)
        {
            System.Web.Security.MembershipUserCollection membershipUsers = new MembershipUserCollection();

            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                UserDataStore dataStore = new UserDataStore(transaction);

                PagingInfo paging = new PagingInfo(pageSize, pageIndex);
                IList<User> users = dataStore.FindByEmailLike(ApplicationName, emailToMatch, paging);
                totalRecords = (int)paging.RowCount;

                foreach (User u in users)
                    membershipUsers.Add(UserToMembershipUser(u));
            }

            return membershipUsers;
        }

        public override System.Web.Security.MembershipUserCollection FindUsersByName(string usernameToMatch, 
                                                    int pageIndex, int pageSize, out int totalRecords)
        {
            System.Web.Security.MembershipUserCollection membershipUsers = new MembershipUserCollection();

            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                UserDataStore dataStore = new UserDataStore(transaction);

                PagingInfo paging = new PagingInfo(pageSize, pageIndex);
                IList<User> users = dataStore.FindByNameLike(ApplicationName, usernameToMatch, paging);
                totalRecords = (int)paging.RowCount;

                foreach (User u in users)
                    membershipUsers.Add(UserToMembershipUser(u));
            }

            return membershipUsers;
        }

        public override System.Web.Security.MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            System.Web.Security.MembershipUserCollection membershipUsers = new MembershipUserCollection();

            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                UserDataStore dataStore = new UserDataStore(transaction);

                PagingInfo paging = new PagingInfo(pageSize, pageIndex);
                IList<User> users = dataStore.FindAll(ApplicationName, paging);
                totalRecords = (int)paging.RowCount;

                foreach (User u in users)
                    membershipUsers.Add(UserToMembershipUser(u));
            }

            return membershipUsers;   
        }

        public override int GetNumberOfUsersOnline()
        {
            TimeSpan onlineSpan = new TimeSpan(0, System.Web.Security.Membership.UserIsOnlineTimeWindow, 0);

            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                UserDataStore dataStore = new UserDataStore(transaction);

                return dataStore.NumbersOfLoggedInUsers(ApplicationName, onlineSpan);
            }
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotSupportedException("Password retrieval not supported");
        }

        public override System.Web.Security.MembershipUser GetUser(string username, bool userIsOnline)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                UserDataStore dataStore = new UserDataStore(transaction);
                User user = dataStore.FindByName(ApplicationName, username);
                if (user == null)
                    return null;
                
                if (userIsOnline)
                    user.LastActivityDate = DateTime.Now;

                transaction.Commit();

                return UserToMembershipUser(user);
            }
        }

        public override System.Web.Security.MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                UserDataStore dataStore = new UserDataStore(transaction);
                User user = dataStore.FindByKey((string)providerUserKey);
                if (user == null)
                    return null;

                if (userIsOnline)
                    user.LastActivityDate = DateTime.Now;

                transaction.Commit();

                return UserToMembershipUser(user);
            }
        }

        public override string GetUserNameByEmail(string email)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                UserDataStore dataStore = new UserDataStore(transaction);
                IList<User> users = dataStore.FindByEmail(ApplicationName, email);
                if (users.Count == 0)
                    return null;

                return users[0].Name;
            }
        }

        public override string ResetPassword(string username, string answer)
        {
            if (!EnablePasswordReset)
            {
                throw new NotSupportedException();
            }

            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                UserDataStore dataStore = new UserDataStore(transaction);
                User user = dataStore.FindByName(ApplicationName, username);
                if (user == null)
                    throw new UserNotFoundException(username);

                if (RequiresQuestionAndAnswer && 
                    user.ValidatePasswordAnswer(answer, PasswordAttemptWindow, MaxInvalidPasswordAttempts) == false)
                {
                    transaction.Commit();

                    throw new MembershipPasswordException();
                }
                else
                {
                    string newPassword = System.Web.Security.Membership.GeneratePassword(MinRequiredPasswordLength, MinRequiredNonAlphanumericCharacters);
                    user.ChangePassword(newPassword);

                    transaction.Commit();

                    return newPassword;
                }
            }
        }

        public override bool UnlockUser(string userName)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                UserDataStore dataStore = new UserDataStore(transaction);
                User user = dataStore.FindByName(ApplicationName, userName);
                if (user == null)
                    throw new UserNotFoundException(userName);

                user.Unlock();

                transaction.Commit();
            }

            return true;
        }

        public override void UpdateUser(System.Web.Security.MembershipUser user)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                UserDataStore dataStore = new UserDataStore(transaction);
                User dbUser = dataStore.FindByName(ApplicationName, user.UserName);
                if (dbUser == null)
                    throw new UserNotFoundException(user.UserName);

                //Check email
                if (RequiresUniqueEmail)
                {
                    if (user.Email == null || user.Email.Length == 0)
                    {
                        throw new EMailNotValidException(user.Email);
                    }

                    IList<User> emailUsers = dataStore.FindByEmail(ApplicationName, user.Email);
                    if (emailUsers.Count > 0 && emailUsers[0].Id != dbUser.Id)
                    {
                        throw new EMailDuplucatedException(user.Email);
                    }
                }

                dbUser.Comment = user.Comment;
                dbUser.EMail = user.Email;
                dbUser.Enabled = user.IsApproved;

                transaction.Commit();
            }
        }

        public override bool ValidateUser(string username, string password)
        {
            using (TransactionScope transaction = new TransactionScope(mConfiguration))
            {
                UserDataStore dataStore = new UserDataStore(transaction);
                User dbUser = dataStore.FindByName(ApplicationName, username);
                if (dbUser == null)
                    return false; //throw new UserNotFoundException(username);

                bool valid = dbUser.Login(password, PasswordAttemptWindow, MaxInvalidPasswordAttempts);

                transaction.Commit();

                return valid;
            }
        }
        #endregion
    }
}
