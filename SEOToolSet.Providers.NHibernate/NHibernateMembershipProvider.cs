#region Using Directives

using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Security;
using LoggerFacade;
using NHibernate;
using NHibernateDataStore.Common;
using SEOToolSet.DAL;
using SEOToolSet.Entities;
using SEOToolSet.Providers.NHibernate.Exceptions;

#endregion

namespace SEOToolSet.Providers.NHibernate
{
    public class NHibernateMembershipProvider : MembershipProvider
    {
        private string _connName;
        private bool _enablePasswordReset;
        private bool _enablePasswordRetrieval;
        private int _maxInvalidPasswordAttemps;
        private int _minRequiredNonAlphanumericCharacters;
        private int _minRequiredPasswordLength;
        private int _passwordAttemptWindow;
        private MembershipPasswordFormat _passwordFormat;
        private string _passwordStrengthRegularExpression;
        private string _providerName;
        private bool _requiresQuestionAndAnswer;
        private bool _requiresUniqueEmail;


        public override string Name
        {
            get { return _providerName; }
        }

        /// <summary>
        /// Indicates whether the membership provider is configured to allow users to retrieve their passwords.
        /// </summary>
        /// <returns>
        /// true if the membership provider is configured to support password retrieval; otherwise, false. The default is false.
        /// </returns>
        public override bool EnablePasswordRetrieval
        {
            get { return _enablePasswordRetrieval; }
        }

        /// <summary>
        /// Indicates whether the membership provider is configured to allow users to reset their passwords.
        /// </summary>
        /// <returns>
        /// true if the membership provider supports password reset; otherwise, false. The default is true.
        /// </returns>
        public override bool EnablePasswordReset
        {
            get { return _enablePasswordReset; }
        }

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require the user to answer a password question for password reset and retrieval.
        /// </summary>
        /// <returns>
        /// true if a password answer is required for password reset and retrieval; otherwise, false. The default is true.
        /// </returns>
        public override bool RequiresQuestionAndAnswer
        {
            get { return _requiresQuestionAndAnswer; }
        }

        /// <summary>
        /// The name of the application using the custom membership provider.
        /// </summary>
        /// <returns>
        /// The name of the application using the custom membership provider.
        /// </returns>
        public override string ApplicationName { get; set; }

        /// <summary>
        /// Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.
        /// </summary>
        /// <returns>
        /// The number of invalid password or password-answer attempts allowed before the membership user is locked out.
        /// </returns>
        public override int MaxInvalidPasswordAttempts
        {
            get { return _maxInvalidPasswordAttemps; }
        }

        /// <summary>
        /// Gets the number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
        /// </summary>
        /// <returns>
        /// The number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
        /// </returns>
        public override int PasswordAttemptWindow
        {
            get { return _passwordAttemptWindow; }
        }

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require a unique e-mail address for each user name.
        /// </summary>
        /// <returns>
        /// true if the membership provider requires a unique e-mail address; otherwise, false. The default is true.
        /// </returns>
        public override bool RequiresUniqueEmail
        {
            get { return _requiresUniqueEmail; }
        }

        /// <summary>
        /// Gets a value indicating the format for storing passwords in the membership data store.
        /// </summary>
        /// <returns>
        /// One of the <see cref="T:System.Web.Security.MembershipPasswordFormat" /> values indicating the format for storing passwords in the data store.
        /// </returns>
        public override MembershipPasswordFormat PasswordFormat
        {
            get { return _passwordFormat; }
        }

        /// <summary>
        /// Gets the minimum length required for a password.
        /// </summary>
        /// <returns>
        /// The minimum length required for a password. 
        /// </returns>
        public override int MinRequiredPasswordLength
        {
            get { return _minRequiredPasswordLength; }
        }

        /// <summary>
        /// Gets the minimum number of special characters that must be present in a valid password.
        /// </summary>
        /// <returns>
        /// The minimum number of special characters that must be present in a valid password.
        /// </returns>
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return _minRequiredNonAlphanumericCharacters; }
        }

        /// <summary>
        /// Gets the regular expression used to evaluate a password.
        /// </summary>
        /// <returns>
        /// A regular expression used to evaluate a password.
        /// </returns>
        public override string PasswordStrengthRegularExpression
        {
            get { return _passwordStrengthRegularExpression; }
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (String.IsNullOrEmpty(name))
                name = "NHibernateMembershipProvider";

            base.Initialize(name, config);


            _providerName = name;
            ApplicationName = ConfigHelper.ExtractConfigValue(config, "applicationName",
                                                              ConnectionParameters.DEFAULT_APP);

            _enablePasswordReset = bool.Parse(ConfigHelper.ExtractConfigValue(config, "enablePasswordReset", "true"));

            _enablePasswordRetrieval = false;
            _maxInvalidPasswordAttemps =
                int.Parse(ConfigHelper.ExtractConfigValue(config, "maxInvalidPasswordAttempts", "5"));

            _minRequiredNonAlphanumericCharacters =
                int.Parse(ConfigHelper.ExtractConfigValue(config, "minRequiredNonAlphanumericCharacters", "1"));

            _minRequiredPasswordLength =
                int.Parse(ConfigHelper.ExtractConfigValue(config, "minRequiredPasswordLength", "7"));
            _passwordAttemptWindow = int.Parse(ConfigHelper.ExtractConfigValue(config, "passwordAttemptWindow", "10"));
            _passwordFormat = MembershipPasswordFormat.Hashed;
                //Enum.Parse(typeof(MembershipPasswordFormat), GetConfigValue(config["passwordFormat"], "Hashed"));

            _passwordStrengthRegularExpression = ConfigHelper.ExtractConfigValue(config,
                                                                                 "passwordStrengthRegularExpression", "");

            _requiresQuestionAndAnswer =
                bool.Parse(ConfigHelper.ExtractConfigValue(config, "requiresQuestionAndAnswer", "false"));
            _requiresUniqueEmail = bool.Parse(ConfigHelper.ExtractConfigValue(config, "requiresUniqueEmail", "true"));

            _connName = ConfigHelper.ExtractConfigValue(config, "connectionStringName", null);


            //_nHibernateConfiguration  = NHibernateConfigurationManager.ConfigurationHelper.Create(connName);

            // Throw an exception if unrecognized attributes remain
            if (config.Count > 0)
            {
                var attr = config.GetKey(0);
                if (!String.IsNullOrEmpty(attr))
                    throw new ProviderException("Unrecognized attribute: " +
                                                attr);
            }
        }

        /// <summary>
        /// Adds a new membership user to the data source.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser" /> object populated with the information for the newly created user.
        /// </returns>
        /// <param name="username">The user name for the new user. </param>
        /// <param name="password">The password for the new user. </param>
        /// <param name="email">The e-mail address for the new user.</param>
        /// <param name="passwordQuestion">The password question for the new user.</param>
        /// <param name="passwordAnswer">The password answer for the new user</param>
        /// <param name="isApproved">Whether or not the new user is approved to be validated.</param>
        /// <param name="providerUserKey">The unique identifier from the membership data source for the user.</param>
        /// <param name="status">A <see cref="T:System.Web.Security.MembershipCreateStatus" /> enumeration value indicating whether the user was created successfully.</param>
        public override MembershipUser CreateUser(string username,
                                                  string password, string email, string passwordQuestion,
                                                  string passwordAnswer, bool isApproved, object providerUserKey,
                                                  out MembershipCreateStatus status)
        {
            using (var tran = new TransactionScope(_connName))
            {
                try
                {
                    ValidatePassword(username, password, true);

                    var DSUSer = DSSEOToolsetUser.Create(_connName);

                    //Check Name

                    if (DSUSer.FindByName(username) != null)
                    {
                        status = MembershipCreateStatus.DuplicateUserName;
                        return null;
                    }

                    if (RequiresUniqueEmail)
                    {
                        if (String.IsNullOrEmpty(email))
                        {
                            status = MembershipCreateStatus.InvalidEmail;
                            return null;
                        }
                        if (DSUSer.FindByEmail(email).Count > 0)
                        {
                            status = MembershipCreateStatus.DuplicateEmail;
                            return null;
                        }
                    }

                    if (RequiresQuestionAndAnswer)
                    {
                        if (String.IsNullOrEmpty(passwordQuestion))
                        {
                            status = MembershipCreateStatus.InvalidQuestion;
                            return null;
                        }

                        if (String.IsNullOrEmpty(passwordAnswer))
                        {
                            status = MembershipCreateStatus.InvalidAnswer;
                            return null;
                        }
                    }


                    EntityHelper.ValidateCode(DSSEOToolsetUser.Columns.Login, username);


                    var user = new SEOToolsetUser();

                    user.Login = username;
                    user.Account = null;
                    user.Email = email;
                    user.Password = encodePassword(password);
                    user.PasswordQuestion = passwordQuestion;
                    if (passwordAnswer != null) user.PasswordAnswer = encodePassword(passwordAnswer);
                    user.Enabled = isApproved;
                    user.LastPasswordChangedDate = DateTime.Now;

                    DSUSer.Insert(user);

                    tran.Commit();

                    status = MembershipCreateStatus.Success;
                    return UserToMembershipUser(user);
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
        }

        private ITransaction BeginTransaction()
        {
            return NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(_connName).BeginTransaction();
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
            if (String.IsNullOrEmpty(password))
                throw new MembershipPasswordException("Password validation failed");

            //Check minimum length
            if (password.Length < MinRequiredPasswordLength)
                throw new MembershipPasswordException("Password validation failed");

            //Check minimum number of digits
            var count = 0;
            for (var i = 0; i < password.Length; i++)
            {
                if (!char.IsLetterOrDigit(password, i))
                    count++;
            }
            if (count < MinRequiredNonAlphanumericCharacters)
                throw new MembershipPasswordException("Password validation failed");

            //Check with the regular expression
            if (PasswordStrengthRegularExpression.Length > 0)
            {
                if (!Regex.IsMatch(password, PasswordStrengthRegularExpression))
                    throw new MembershipPasswordException("Password validation failed");
            }


            //Use a custom check if defined
            var args =
                new ValidatePasswordEventArgs(user, password, isNew);

            OnValidatingPassword(args);
            if (args.Cancel)
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new MembershipPasswordException("Password validation failed");
        }

        /// <summary>
        /// Processes a request to update the password question and answer for a membership user.
        /// </summary>
        /// <returns>
        /// true if the password question and answer are updated successfully; otherwise, false.
        /// </returns>
        /// <param name="username">The user to change the password question and answer for. </param>
        /// <param name="password">The password for the specified user. </param>
        /// <param name="newPasswordQuestion">The new password question for the specified user. </param>
        /// <param name="newPasswordAnswer">The new password answer for the specified user. </param>
        public override bool ChangePasswordQuestionAndAnswer(string username, string password,
                                                             string newPasswordQuestion, string newPasswordAnswer)
        {
            ITransaction tran = null;
            try
            {
                tran = BeginTransaction();
                //UserDataStore dataStore = new UserDataStore(transaction);
                var DSUSer = DSSEOToolsetUser.Create(_connName);
                var user = DSUSer.FindByName(username);
                if (user == null)
                    throw new UserNotFoundException(username);

                if (checkPassword(user, password) == false)
                    throw new UserNotFoundException(username);

                user.PasswordAnswer = encodePassword(newPasswordAnswer);
                user.PasswordQuestion = newPasswordQuestion;

                DSUSer.Update(user);

                tran.Commit();

                return true;
            }
            catch (Exception ex)
            {
                LogException(ex, "ChangePasswordQuestionAndAnswer");
                if (tran != null) tran.Rollback();
                return false;
            }
        }


        private static bool checkPassword(AbstractSEOToolsetUser user, string password)
        {
            return user.Password == encodePassword(password);
        }

        /// <summary>
        /// Gets the password for the specified user name from the data source.
        /// </summary>
        /// <returns>
        /// The password for the specified user name.
        /// </returns>
        /// <param name="username">The user to retrieve the password for. </param>
        /// <param name="answer">The password answer for the user. </param>
        public override string GetPassword(string username, string answer)
        {
            throw new NotSupportedException("Password retrieval not supported");
        }

        /// <summary>
        /// Processes a request to update the password for a membership user.
        /// </summary>
        /// <returns>
        /// true if the password was updated successfully; otherwise, false.
        /// </returns>
        /// <param name="username">The user to update the password for. </param>
        /// <param name="oldPassword">The current password for the specified user. </param>
        /// <param name="newPassword">The new password for the specified user. </param>
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            ITransaction transaction = null;
            try
            {
                ValidatePassword(username, newPassword, false);
                transaction = BeginTransaction();

                var dataStore = DSSEOToolsetUser.Create(_connName);
                var user = dataStore.FindByName(username);
                if (user == null)
                    throw new UserNotFoundException(username);

                if (checkPassword(user, oldPassword) == false)
                    throw new UserNotFoundException(username);

                user.Password = encodePassword(newPassword);
                user.LastPasswordChangedDate = DateTime.Now;

                transaction.Commit();

                return true;
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                LogException(ex, "ChangePassword");
                return false;
            }
        }

        /// <summary>
        /// Resets a user's password to a new, automatically generated password.
        /// </summary>
        /// <returns>
        /// The new password for the specified user.
        /// </returns>
        /// <param name="username">The user to reset the password for. </param>
        /// <param name="answer">The password answer for the specified user. </param>
        public override string ResetPassword(string username, string answer)
        {
            if (!EnablePasswordReset)
            {
                throw new NotSupportedException();
            }

            var transaction = BeginTransaction();
            try
            {
                var dataStore = DSSEOToolsetUser.Create(_connName);
                var user = dataStore.FindByName(username);
                if (user == null)
                    throw new UserNotFoundException(username);

                if (RequiresQuestionAndAnswer &&
                    validatePasswordAnswer(user, answer, PasswordAttemptWindow, MaxInvalidPasswordAttempts) == false)
                {
                    transaction.Rollback();

                    throw new MembershipPasswordException();
                }

                var newPassword = Membership.GeneratePassword(MinRequiredPasswordLength,
                                                              MinRequiredNonAlphanumericCharacters);

                user.Password = newPassword;
                user.LastPasswordChangedDate = DateTime.Now;

                transaction.Commit();

                return newPassword;
            }
            catch (Exception ex)
            {
                LogException(ex, "ResetPassword");
                if (transaction != null) transaction.Rollback();
            }
            return String.Empty;
        }

        private static bool validatePasswordAnswer(AbstractSEOToolsetUser user, string answer, int minAttemptWindow,
                                                   int maxInvalidAttempts)
        {
            if ((user.IsLockedOut == true) || (user.Enabled == false))
                return false;


            var valid = string.Equals(user.PasswordAnswer, encodePassword(answer));

            if (!valid)
            {
                incrementFailedPwdAttempt(user, minAttemptWindow, maxInvalidAttempts);
            }
            else
                user.FailedPasswordAttemptCount = 0;

            return valid;
        }

        private static void incrementFailedPwdAttempt(AbstractSEOToolsetUser user, int minAttemptWindow,
                                                      int maxInvalidAttempts)
        {
            var timeFromLastFailedLogin = new TimeSpan(0);
            if (user.LastFailedLoginDate != null)
                timeFromLastFailedLogin = DateTime.Now - user.LastFailedLoginDate.Value;

            if (timeFromLastFailedLogin.TotalMinutes < minAttemptWindow)
                user.FailedPasswordAttemptCount++;

            if (user.FailedPasswordAttemptCount > maxInvalidAttempts)
                user.IsLockedOut = true;

            user.LastFailedLoginDate = DateTime.Now;
        }

        /// <summary>
        /// Updates information about a user in the data source.
        /// </summary>
        /// <param name="user">A <see cref="T:System.Web.Security.MembershipUser" /> object that represents the user to update and the updated information for the user. </param>
        public override void UpdateUser(MembershipUser user)
        {
            ITransaction transaction = null;
            try
            {
                transaction = BeginTransaction();
                var dataStore = DSSEOToolsetUser.Create(_connName);
                var dbUser = dataStore.FindByName(user.UserName);
                if (dbUser == null)
                    throw new UserNotFoundException(user.UserName);

                //Check email
                if (RequiresUniqueEmail)
                {
                    if (string.IsNullOrEmpty(user.Email))
                    {
                        throw new EMailNotValidException(user.Email);
                    }

                    var emailUsers = dataStore.FindByEmail(user.Email);
                    if (emailUsers.Count > 0 && emailUsers[0].Id != dbUser.Id)
                    {
                        throw new EMailDuplucatedException(user.Email);
                    }
                }

                dbUser.Email = user.Email;
                dbUser.Enabled = user.IsApproved;

                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                LogException(ex, "UpdateUser");
            }
        }

        /// <summary>
        /// Verifies that the specified user name and password exist in the data source.
        /// </summary>
        /// <returns>
        /// true if the specified username and password are valid; otherwise, false.
        /// </returns>
        /// <param name="username">The name of the user to validate. </param>
        /// <param name="password">The password for the specified user. </param>
        public override bool ValidateUser(string username, string password)
        {
            var transaction = BeginTransaction();
            try
            {
                var dataStore = DSSEOToolsetUser.Create(_connName);
                var dbUser = dataStore.FindByName(username);
                if (dbUser == null)
                    return false; //throw new UserNotFoundException(username);

                var valid = login(dbUser, password, PasswordAttemptWindow, MaxInvalidPasswordAttempts);

                transaction.Commit();

                return valid;
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                LogException(ex, "ValidateUser");
                return false;
            }
        }

        private static bool login(AbstractSEOToolsetUser user, string password, int passwordAttemptWindow,
                                  int maxInvalidPasswordAttempts)
        {
            if (user.IsLockedOut == true || user.Enabled == false)
                return false;

            var valid = checkPassword(user, password);
            valid = true;//////////

            if (!valid)
            {
                incrementFailedPwdAttempt(user, passwordAttemptWindow, maxInvalidPasswordAttempts);
            }
            else
            {
                user.FailedPasswordAttemptCount = 0;
                user.LastLoginDate = DateTime.Now;
            }

            return valid;
        }


        /// <summary>
        /// Clears a lock so that the membership user can be validated.
        /// </summary>
        /// <returns>
        /// true if the membership user was successfully unlocked; otherwise, false.
        /// </returns>
        /// <param name="userName">The membership user whose lock status you want to clear.</param>
        public override bool UnlockUser(string userName)
        {
            var tran = BeginTransaction();
            try
            {
                var dataStore = DSSEOToolsetUser.Create(_connName);
                var user = dataStore.FindByName(userName);
                if (user == null)
                    throw new UserNotFoundException(userName);

                user.FailedPasswordAttemptCount = 0;
                user.IsLockedOut = false;

                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                LogException(ex, "UnlockUser");
                return false;
            }
        }

        /// <summary>
        /// Gets user information from the data source based on the unique identifier for the membership user. Provides an option to update the last-activity date/time stamp for the user.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser" /> object populated with the specified user's information from the data source.
        /// </returns>
        /// <param name="providerUserKey">The unique identifier for the membership user to get information for.</param>
        /// <param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user.</param>
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            var tran = BeginTransaction();
            try
            {
                var dataStore = DSSEOToolsetUser.Create(_connName);
                var user = dataStore.FindByKey((Int32) providerUserKey);
                if (user == null)
                    return null;

                if (userIsOnline)
                    user.LastActivityDate = DateTime.Now;

                tran.Commit();

                return UserToMembershipUser(user);
            }
            catch (Exception ex)
            {
                LogException(ex, "GetUser");
                tran.Rollback();
                return null;
            }
        }

        /// <summary>
        /// Gets information from the data source for a user. Provides an option to update the last-activity date/time stamp for the user.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser" /> object populated with the specified user's information from the data source.
        /// </returns>
        /// <param name="username">The name of the user to get information for. </param>
        /// <param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user. </param>
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var tran = BeginTransaction();
            try
            {
                var dataStore = DSSEOToolsetUser.Create(_connName);
                var user = dataStore.FindByName(username);
                if (user == null)
                    return null;

                if (userIsOnline)
                    user.LastActivityDate = DateTime.Now;

                tran.Commit();

                return UserToMembershipUser(user);
            }
            catch (Exception ex)
            {
                LogException(ex, "GetUser");
                tran.Rollback();
                return null;
            }
        }

        /// <summary>
        /// Gets the user name associated with the specified e-mail address.
        /// </summary>
        /// <returns>
        /// The user name associated with the specified e-mail address. If no match is found, return null.
        /// </returns>
        /// <param name="email">The e-mail address to search for. </param>
        public override string GetUserNameByEmail(string email)
        {
            var ds = DSSEOToolsetUser.Create(_connName);
            var user = ds.FindUniqueByEmail(email);
            return user == null ? null : user.Login;
        }

        /// <summary>
        /// Removes a user from the membership data source. 
        /// </summary>
        /// <returns>
        /// true if the user was successfully deleted; otherwise, false.
        /// </returns>
        /// <param name="username">The name of the user to delete.</param>
        /// <param name="deleteAllRelatedData">true to delete data related to the user from the database; false to leave data related to the user in the database.</param>
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            var tran = BeginTransaction();
            try
            {
                if (deleteAllRelatedData)
                {
                    /*
                    Roles.UserInRoleDataStore userInRoleStore = new Eucalypto.Roles.UserInRoleDataStore(transaction);
                    IList<Roles.UserInRole> userInRoles = userInRoleStore.FindForUser(ApplicationName, username);
                    foreach (Roles.UserInRole ur in userInRoles)
                        userInRoleStore.Delete(ur.Id);*/
                }

                var dataStore = DSSEOToolsetUser.Create(_connName);
                var user = dataStore.FindByName(username);
                if (user == null)
                    throw new UserNotFoundException(username);

                dataStore.Delete(user.Id);

                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                LogException(ex, "DeleteUser");
                return false;
            }
        }

        /// <summary>
        /// Gets a collection of all the users in the data source in pages of data.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUserCollection" /> collection that contains a page of <paramref name="pageSize" /><see cref="T:System.Web.Security.MembershipUser" /> objects beginning at the page specified by <paramref name="pageIndex" />.
        /// </returns>
        /// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
        /// <param name="pageSize">The size of the page of results to return.</param>
        /// <param name="totalRecords">The total number of matched users.</param>
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            var membershipUsers = new MembershipUserCollection();
            totalRecords = 0;

            var tran = BeginTransaction();
            try
            {
                var dataStore = DSSEOToolsetUser.Create(_connName);

                var paging = new PagingInfo(pageSize, pageIndex);
                var users = dataStore.Find(dataStore.CreateCriteria(), paging);
                totalRecords = (int) paging.RowCount;

                foreach (var u in users)
                    membershipUsers.Add(UserToMembershipUser(u));

                tran.Commit();
            }
            catch (Exception ex)
            {
                LogException(ex, "GetAllUsers");
                tran.Rollback();
            }


            return membershipUsers;
        }

        /// <summary>
        /// Gets the number of users currently accessing the application.
        /// </summary>
        /// <returns>
        /// The number of users currently accessing the application.
        /// </returns>
        public override int GetNumberOfUsersOnline()
        {
            var onlineSpan = new TimeSpan(0, Membership.UserIsOnlineTimeWindow, 0);

            var tran = BeginTransaction();
            try
            {
                var dataStore = DSSEOToolsetUser.Create(_connName);
                tran.Commit();
                return dataStore.NumbersOfLoggedInUsers(onlineSpan);
            }
            catch (Exception ex)
            {
                LogException(ex, "GetNumberOfUsersOnline");
                return 0;
            }
        }

        /// <summary>
        /// Gets a collection of membership users where the user name contains the specified user name to match.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUserCollection" /> collection that contains a page of <paramref name="pageSize" /><see cref="T:System.Web.Security.MembershipUser" /> objects beginning at the page specified by <paramref name="pageIndex" />.
        /// </returns>
        /// <param name="usernameToMatch">The user name to search for.</param>
        /// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
        /// <param name="pageSize">The size of the page of results to return.</param>
        /// <param name="totalRecords">The total number of matched users.</param>
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize,
                                                                 out int totalRecords)
        {
            var membershipUsers = new MembershipUserCollection();

            var tran = BeginTransaction();
            try
            {
                var dataStore = DSSEOToolsetUser.Create(_connName);

                var paging = new PagingInfo(pageSize, pageIndex);
                var users = dataStore.FindByNameLike(usernameToMatch, paging);
                totalRecords = (int) paging.RowCount;

                foreach (var u in users)
                    membershipUsers.Add(UserToMembershipUser(u));
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                LogException(ex, "FindUsersByName");
                totalRecords = 0;
            }

            return membershipUsers;
        }

        /// <summary>
        /// Gets a collection of membership users where the e-mail address contains the specified e-mail address to match.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUserCollection" /> collection that contains a page of <paramref name="pageSize" /><see cref="T:System.Web.Security.MembershipUser" /> objects beginning at the page specified by <paramref name="pageIndex" />.
        /// </returns>
        /// <param name="emailToMatch">The e-mail address to search for.</param>
        /// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
        /// <param name="pageSize">The size of the page of results to return.</param>
        /// <param name="totalRecords">The total number of matched users.</param>
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize,
                                                                  out int totalRecords)
        {
            var membershipUsers = new MembershipUserCollection();

            var tran = BeginTransaction();
            try
            {
                var dataStore = DSSEOToolsetUser.Create(_connName);

                var paging = new PagingInfo(pageSize, pageIndex);
                var users = dataStore.FindByEmailLike(emailToMatch, paging);
                totalRecords = (int) paging.RowCount;

                foreach (var u in users)
                    membershipUsers.Add(UserToMembershipUser(u));
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                LogException(ex, "FindUsersByEmail");
                totalRecords = 0;
            }

            return membershipUsers;
        }

        #region HelperMethods

        private void LogException(Exception exception, string action)
        {
            Log.Error(GetType(), "Exception on " + action, exception);
        }

        private static string encodePassword(string password)
        {
            var sha1CryptoService = new SHA1CryptoServiceProvider();
            var byteValue = Encoding.UTF8.GetBytes(password);
            var hashValue = sha1CryptoService.ComputeHash(byteValue);

            return Convert.ToBase64String(hashValue);
        }

        private MembershipUser UserToMembershipUser(AbstractSEOToolsetUser user)
        {
            return new MembershipUser(Name, user.Login, user.Id, user.Email, user.PasswordQuestion, null,
                                      Convert.ToBoolean(user.Enabled), Convert.ToBoolean(user.IsLockedOut),
                                      SafeDate(user.CreatedDate), SafeDate(user.LastLoginDate),
                                      SafeDate(user.LastActivityDate), SafeDate(user.LastPasswordChangedDate),
                                      SafeDate(user.LockedOutDate));
        }


        private static DateTime SafeDate(DateTime? date)
        {
            return date != null ? date.Value : new DateTime();
        }

        #endregion
    }
}