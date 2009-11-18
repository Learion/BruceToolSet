#region Using Directives

using System;
using System.Collections.Generic;
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

#endregion

namespace SEOToolSet.Providers.NHibernate
{
    public class NHibernateSEOMembershipProvider : SEOMembershipProviderBase
    {
        private string _connName;
        private string _providerName;
        public bool RequiresUniqueEmail { get; private set; }

        public override string Name
        {
            get { return _providerName; }
        }

        public event EventHandler<ValidatePasswordEventArgs> ValidatingPassword;

        private ITransaction BeginTransaction()
        {
            return NHibernateConfigurationManager.ConfigurationHelper.GetCurrentSession(_connName).BeginTransaction();
        }

        private void LogException(Exception exception, string action)
        {
            Log.Error(GetType(), "Exception on " + action, exception);
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (name.Length == 0)
                name = "SEOMembershipProvider";

            base.Initialize(name, config);

            _providerName = name;

            _connName = ConfigHelper.ExtractConfigValue(config, "connectionStringName", null);

            RequiresUniqueEmail = bool.Parse(ConfigHelper.ExtractConfigValue(config, "requiresUniqueEmail", "false"));

            if (config.Count == 0)
                return;

            // Throw an exception if unrecognized attributes remain
            var attr = config.GetKey(0);
            if (!String.IsNullOrEmpty(attr))
                throw new ProviderException("Unrecognized attribute: " + attr);
        }


        private static string encodePassword(string password)
        {
            var sha1CryptoService = new SHA1CryptoServiceProvider();
            var byteValue = Encoding.UTF8.GetBytes(password);
            var hashValue = sha1CryptoService.ComputeHash(byteValue);

            return Convert.ToBase64String(hashValue);
        }

        #region Overrides of SEOMembershipProviderBase

        public override SEOToolsetUser GetUser(string userName)
        {
            SEOToolsetUser user;
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSSEOToolsetUser.Create(_connName);

                user = ds.FindByName(userName);

                tran.Commit();
            }
            return user;
        }

        public override SEOToolsetUser GetUserById(int id)
        {
            var ds = DSSEOToolsetUser.Create(_connName);
            return ds.FindByKey(id);
        }

        public override void CreateUser(out int id, string firstName,
                                        string lastName, string email,
                                        string address1, string address2,
                                        string cityTown,
                                        string state,
                                        string zip,
                                        string telephone,
                                        string login,
                                        string password,
                                        string passwordQuestion,
                                        string passwordAnswer,
                                        Account account,
                                        Country country,
                                        Role userRole,
                                        out MembershipCreateStatus status)
        {
            id = -1;
            using (var tran = new TransactionScope(_connName))
            {
                ValidatePassword(login, password, true);

                var ds = DSSEOToolsetUser.Create(_connName);

                if (ds.FindByName(login) != null)
                {
                    status = MembershipCreateStatus.DuplicateUserName;

                    return;
                }

                if (RequiresUniqueEmail)
                {
                    if (String.IsNullOrEmpty(email))
                    {
                        status = MembershipCreateStatus.InvalidEmail;
                        return;
                    }
                    if (ds.FindByEmail(email).Count > 0)
                    {
                        status = MembershipCreateStatus.DuplicateEmail;
                        return;
                    }
                }


                if (Membership.RequiresQuestionAndAnswer)
                {
                    if (String.IsNullOrEmpty(passwordQuestion))
                    {
                        status = MembershipCreateStatus.InvalidQuestion;
                        return;
                    }

                    if (String.IsNullOrEmpty(passwordAnswer))
                    {
                        status = MembershipCreateStatus.InvalidAnswer;
                        return;
                    }
                }

                EntityHelper.ValidateCode(DSSEOToolsetUser.Columns.Login, login);

                var ce = new SEOToolsetUser
                             {
                                 Enabled = true
                             };

                if (firstName != null)
                    ce.FirstName = firstName;
                if (lastName != null)
                    ce.LastName = lastName;
                if (email != null)
                    ce.Email = email;
                if (address1 != null)
                    ce.Address1 = address1;
                if (address2 != null)
                    ce.Address2 = address2;
                if (cityTown != null)
                    ce.CityTown = cityTown;
                if (state != null)
                    ce.State = state;
                if (zip != null)
                    ce.Zip = zip;
                if (telephone != null)
                    ce.Telephone = telephone;
                if (login != null)
                    ce.Login = login;
                if (password != null)
                    ce.Password = encodePassword(password);


                if (account != null)
                    ce.Account = account;
                if (country != null)
                    ce.Country = country;
                if (userRole != null)
                    ce.UserRole = userRole;

                if (passwordQuestion != null)
                    ce.PasswordQuestion = passwordQuestion;
                if (ce.PasswordAnswer != null)
                    ce.PasswordAnswer = encodePassword(ce.PasswordAnswer);

                ds.Insert(ce);
                status = MembershipCreateStatus.Success;
                tran.Commit();
                id = ce.Id;
            }
        }

        public override void UpdateUser(int id,
                                        string firstName,
                                        string lastName,
                                        string email,
                                        string address1,
                                        string address2,
                                        string cityTown,
                                        string state,
                                        string zip,
                                        string telephone,
                                        string login,
                                        string password,
                                        string passwordQuestion,
                                        string passwordAnswer,
                                        bool? enabled,
                                        Account account,
                                        Country country,
                                        Role userRole)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSSEOToolsetUser.Create(_connName);
                EntityHelper.ValidateCode(DSSEOToolsetUser.Columns.Login, login);

                var user = ds.FindByKey(id);

                if (firstName != null)
                    user.FirstName = firstName;
                if (lastName != null)
                    user.LastName = lastName;
                if (email != null)
                    user.Email = email;
                if (address1 != null)
                    user.Address1 = address1;
                if (address2 != null)
                    user.Address2 = address2;
                if (cityTown != null)
                    user.CityTown = cityTown;
                if (state != null)
                    user.State = state;
                if (zip != null)
                    user.Zip = zip;
                if (telephone != null)
                    user.Telephone = telephone;
                if (login != null)
                    user.Login = login;
                if (password != null)
                    user.Password = encodePassword(password);
                if (passwordQuestion != null)
                    user.PasswordQuestion = passwordQuestion;
                if (passwordAnswer != null)
                    user.PasswordAnswer = encodePassword(user.PasswordAnswer);
                if (enabled != null)
                    user.Enabled = enabled;
                if (account != null)
                    user.Account = account;
                if (country != null)
                    user.Country = country;
                if (userRole != null)
                    user.UserRole = userRole;
                ds.Update(user);
                tran.Commit();
            }
        }

        public override void CreateUser(SEOToolsetUser user, out MembershipCreateStatus status)
        {
            using (var tran = new TransactionScope(_connName))
            {
                ValidatePassword(user.Login, user.Password, true);

                var ds = DSSEOToolsetUser.Create(_connName);

                if (ds.FindByName(user.Login) != null)
                {
                    status = MembershipCreateStatus.DuplicateUserName;
                    return;
                }

                if (RequiresUniqueEmail)
                {
                    if (String.IsNullOrEmpty(user.Email))
                    {
                        status = MembershipCreateStatus.InvalidEmail;
                        return;
                    }
                    if (ds.FindByEmail(user.Email).Count > 0)
                    {
                        status = MembershipCreateStatus.DuplicateEmail;
                        return;
                    }
                }


                if (Membership.RequiresQuestionAndAnswer)
                {
                    if (String.IsNullOrEmpty(user.PasswordQuestion))
                    {
                        status = MembershipCreateStatus.InvalidQuestion;
                        return;
                    }

                    if (String.IsNullOrEmpty(user.PasswordAnswer))
                    {
                        status = MembershipCreateStatus.InvalidAnswer;
                        return;
                    }
                }

                EntityHelper.ValidateCode(DSSEOToolsetUser.Columns.Login, user.Login);

                user.Password = encodePassword(user.Password);

                if (user.PasswordAnswer != null)
                    user.PasswordAnswer = encodePassword(user.PasswordAnswer);

                ds.Insert(user);

                status = MembershipCreateStatus.Success;

                tran.Commit();
            }
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
            if (password.Length < Membership.MinRequiredPasswordLength)
                throw new MembershipPasswordException("Password validation failed");

            //Check minimum number of digits
            var count = 0;
            for (var i = 0; i < password.Length; i++)
            {
                if (!char.IsLetterOrDigit(password, i))
                    count++;
            }
            if (count < Membership.MinRequiredNonAlphanumericCharacters)
                throw new MembershipPasswordException("Password validation failed");

            //Check with the regular expression
            if (Membership.PasswordStrengthRegularExpression.Length > 0 &&
                !Regex.IsMatch(password, Membership.PasswordStrengthRegularExpression))
                throw new MembershipPasswordException("Password validation failed");

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

        private void OnValidatingPassword(ValidatePasswordEventArgs args)
        {
            if (ValidatingPassword != null)
            {
                ValidatingPassword(this, args);
            }
        }

        public override IList<SEOToolsetUser> GetUsersFromAccount(Account account, bool? includeInactive)
        {
            var ds = DSSEOToolsetUser.Create(_connName);
            return ds.FindByAccount(account, includeInactive);
        }

        public override IList<SEOToolsetUser> GetUsersFromAccount(string accountName, bool? includeInactive)
        {
            return DSSEOToolsetUser.Create(_connName).FindByAccountName(accountName, includeInactive);
        }

        /*public override void AddSubscription(string accountName, string serviceName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveSubscription(string accountName, string serviceName)
        {
            throw new NotImplementedException();
        }

        public override void AddSubscription(Account account, Service service)
        {
            throw new NotImplementedException();
        }

        public override void RemoveSubscription(Account account, Service service)
        {
            throw new NotImplementedException();
        } */

        public override IList<Country> GetCountries()
        {
            var ds = DSCountry.Create(_connName);
            return ds.FindAll();
        }

        public override Country GetCountryById(int id)
        {
            var ds = DSCountry.Create(_connName);
            return ds.FindByKey(id);
        }

        public override void UpdateUser(SEOToolsetUser user)
        {
            var tran = BeginTransaction();
            try
            {
                var ds = DSSEOToolsetUser.Create(_connName);
                ds.Update(user);

                tran.Commit();
            }
            catch (Exception ex)
            {
                LogException(ex, ex.Source);
                tran.Rollback();
                throw;
            }
        }

        public override void DeleteUser(int id)
        {
            using (var tran = new TransactionScope(_connName))
            {
                var ds = DSSEOToolsetUser.Create(_connName);
                ds.Delete(id);
                tran.Commit();
            }
        }

        public override IList<SEOToolsetUser> GetUsersFromAccountSortBy(Account account, bool? includeInactive, bool asc, string fieldName)
        {
            return DSSEOToolsetUser.Create(_connName).FindByAccountName(account, includeInactive, asc, fieldName);
        }

        public override IList<SEOToolsetUser> GetUsersFromAccountWithSortAndPaging(Account account, bool? includeInactive, bool asc, string fieldName, out int count, int pageSize, int currentPageNumber)
        {
            return DSSEOToolsetUser.Create(_connName).FindByAccountNameWithSortAndPaging(account, includeInactive, asc,
                                                                                         fieldName, out count, pageSize,
                                                                                         currentPageNumber);
        }
        #endregion
    }
}