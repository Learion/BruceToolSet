using System;
using System.Collections.Generic;
using System.Text;
using Eucalypto.Interceptor;

namespace Eucalypto.Membership
{
    /// <summary>
    /// Class entity of a user, used for the membership provider
    /// </summary>
    public class User : IAudit
    {
        protected User()
        {

        }

        public User(string applicationName, string userName)
        {
            ApplicationName = applicationName;
            Name = userName;
        }


        private string mId;
        public virtual string Id
        {
            get { return mId; }
            protected set { mId = value; }
        }

        private string mName;
        public virtual string Name
        {
            get { return mName; }
            protected set
            {
                Eucalypto.Common.EntityHelper.ValidateCode("Name", value);
                mName = value;
            }
        }
        private string mEMail;
        public virtual string EMail
        {
            get { return mEMail; }
            set { mEMail = value; }
        }
        private string mPassword;
        protected virtual string Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }
        private string mPasswordQuestion;
        public virtual string PasswordQuestion
        {
            get { return mPasswordQuestion; }
            protected set { mPasswordQuestion = value; }
        }
        private string mPasswordAnswer;
        public virtual string PasswordAnswer
        {
            get { return mPasswordAnswer; }
            protected set { mPasswordAnswer = value; }
        }
        private DateTime mInsertDate;
        public virtual DateTime InsertDate
        {
            get { return mInsertDate; }
            set { mInsertDate = value; }
        }

        private DateTime mUpdateDate;
        public virtual DateTime UpdateDate
        {
            get { return mUpdateDate; }
            set { mUpdateDate = value; }
        }

        private string mApplicationName;
        public virtual string ApplicationName
        {
            get { return mApplicationName; }
            protected set { mApplicationName = value; }
        }
        private string mComment;
        public virtual string Comment
        {
            get { return mComment; }
            set { mComment = value; }
        }
        private bool mEnabled = true;
        public virtual bool Enabled
        {
            get { return mEnabled; }
            set { mEnabled = value; }
        }

        private DateTime? mLastFailedPasswordDate;
        /// <summary>
        /// Last invalid login password date
        /// </summary>
        public virtual DateTime? LastFailedPasswordDate
        {
            get { return mLastFailedPasswordDate; }
            protected set { mLastFailedPasswordDate = value; }
        }

        private DateTime? mLastLoginDate;
        /// <summary>
        /// Last successfully login date
        /// </summary>
        public virtual DateTime? LastLoginDate
        {
            get { return mLastLoginDate; }
            protected set { mLastLoginDate = value; }
        }
        private DateTime? mLastActivityDate;
        public virtual DateTime? LastActivityDate
        {
            get { return mLastActivityDate; }
            set { mLastActivityDate = value; }
        }
        private DateTime? mLastPasswordChangedDate;
        public virtual DateTime? LastPasswordChangedDate
        {
            get { return mLastPasswordChangedDate; }
            protected set { mLastPasswordChangedDate = value; }
        }
        private bool mIsLockedOut = false;
        public virtual bool IsLockedOut
        {
            get { return mIsLockedOut; }
            protected set { mIsLockedOut = value; }
        }
        private DateTime? mLockedOutDate;
        public virtual DateTime? LockedOutDate
        {
            get { return mLockedOutDate; }
            protected set { mLockedOutDate = value; }
        }
        private int mFailedPwdAttemptCount = 0;
        public virtual int FailedPwdAttemptCount
        {
            get { return mFailedPwdAttemptCount; }
            protected set { mFailedPwdAttemptCount = value; }
        }

        private string mTag;
        /// <summary>
        /// Field that can be used for user defined extensions.
        /// </summary>
        public virtual string Tag
        {
            get { return mTag; }
            set { mTag = value; }
        }


        /// <summary>
        /// Change the password and the LastPasswordChangedDate date
        /// </summary>
        /// <param name="password"></param>
        public virtual void ChangePassword(string password)
        {
            Password = EncodePassword(password, PasswordEncoding.Hashed);

            LastPasswordChangedDate = DateTime.Now;
        }

        /// <summary>
        /// Change the password question and answer
        /// </summary>
        /// <param name="passwordQuestion"></param>
        /// <param name="passwordAnswer"></param>
        public virtual void ChangePasswordQuestionAnswer(string passwordQuestion, string passwordAnswer)
        {
            PasswordQuestion = passwordQuestion;
            PasswordAnswer = passwordAnswer;
        }

        /// <summary>
        /// Check if the password specified is valid
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual bool CheckPassword(string password)
        {
            return Password == EncodePassword(password, PasswordEncoding.Hashed);
        }

        /// <summary>
        /// Check if the password answer specified is valid
        /// </summary>
        /// <param name="passwordAnswer"></param>
        /// <returns></returns>
        public virtual bool CheckPasswordAnswer(string passwordAnswer)
        {
            return string.Equals(passwordAnswer, passwordAnswer, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Check if the password is valid and if valid update the LastLoginDate otherwise update the FailedPwdAttemptCount
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual bool Login(string password, int minAttemptWindow, int maxInvalidAttempts)
        {
            if (IsLockedOut || Enabled == false)
                return false;

            bool valid = CheckPassword(password);


            if (valid == false)
            {
                IncrementFailedPwdAttempt(minAttemptWindow, maxInvalidAttempts);
            }
            else
            {
                FailedPwdAttemptCount = 0;
                LastLoginDate = DateTime.Now;
            }

            return valid;
        }

        protected virtual void IncrementFailedPwdAttempt(int minAttemptWindow, int maxInvalidAttempts)
        {
            TimeSpan timeFromLastFailedLogin = new TimeSpan(0);
            if (LastFailedPasswordDate != null)
                timeFromLastFailedLogin = DateTime.Now - LastFailedPasswordDate.Value;

            if (timeFromLastFailedLogin.TotalMinutes < minAttemptWindow)
                FailedPwdAttemptCount++;

            if (FailedPwdAttemptCount > maxInvalidAttempts)
                IsLockedOut = true;

            LastFailedPasswordDate = DateTime.Now;
        }

        public virtual void Unlock()
        {
            FailedPwdAttemptCount = 0;
            IsLockedOut = false;
        }

        /// <summary>
        /// Check if the password answer is valid and if not valid update the FailedPwdAttemptCount
        /// </summary>
        /// <param name="passwordAnswer"></param>
        /// <returns></returns>
        public virtual bool ValidatePasswordAnswer(string passwordAnswer, int minAttemptWindow, int maxInvalidAttempts)
        {
            if (IsLockedOut || Enabled == false)
                return false;

            bool valid = CheckPasswordAnswer(passwordAnswer);

            if (valid == false)
            {
                IncrementFailedPwdAttempt(minAttemptWindow, maxInvalidAttempts);
            }
            else
                FailedPwdAttemptCount = 0;

            return valid;
        }

        private static string EncodePassword(string password, PasswordEncoding encoding)
        {
            if (encoding != PasswordEncoding.Hashed)
                throw new EucalyptoException("Password format not valid");

            System.Security.Cryptography.SHA1CryptoServiceProvider sha1CryptoService = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] byteValue = Encoding.UTF8.GetBytes(password);
            byte[] hashValue = sha1CryptoService.ComputeHash(byteValue);

            return System.Convert.ToBase64String(hashValue);
        }
    }

    public enum PasswordEncoding
    {
        Hashed = 1
    }
}
