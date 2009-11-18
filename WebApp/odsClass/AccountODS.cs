using System;
using System.ComponentModel;
using System.Web;
using SEOToolSet.Entities;

namespace SEOToolSet.WebApp.odsClass
{
    /// <summary>
    /// This class is used to Create an instance of the User and Account during the Subscription Process
    /// </summary>
    public class AccountODS
    {
        private const string _tokenAccount = "account";
        private const string _tokenUser = "user";
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static Account GetAccount()
        {
            var a = HttpContext.Current.Session[_tokenAccount] as Account;

            if (a == null)
            {
                HttpContext.Current.Session[_tokenAccount] =
                    a = new Account
                            {
                                CompanyIdCountry = -1,
                                Enabled = true,
                                RecurringBill = false,
                                CreditCardIdCountry = -1
                            };
            }
            return a;
        }

        public static Account GetAccountFromStore()
        {
            return HttpContext.Current.Session[_tokenAccount] as Account;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static SEOToolsetUser GetUser()
        {
            var user = HttpContext.Current.Session[_tokenUser] as SEOToolsetUser;

            if (user == null)
            {
                HttpContext.Current.Session[_tokenUser] =
                    user = new SEOToolsetUser
                    {
                        Enabled = true,
                        IsLockedOut = false,
                        Country = new Country
                                      {
                                          Id = -1,
                                          Name = "Choose"
                                      }
                    };
            }
            return user;
        }

        public static SEOToolsetUser GetUserFromStore()
        {
            return HttpContext.Current.Session[_tokenUser] as SEOToolsetUser;
        }

        public static void UpdateUser(String firstName
                                      , String lastName
                                      , String email
                                      , String address1
                                      , String address2
                                      , String cityTown
                                      , String state
                                      , String zip
                                      , String telephone
                                      , String login
                                      , String password
                                      , String passwordQuestion
                                      , String passwordAnswer
                                      , DateTime? lastFailedLoginDate
                                      , DateTime? lastActivityDate
                                      , DateTime? lastPasswordChangedDate
                                      , bool? isLockedOut
                                      , DateTime? lockedOutDate
                                      , int? failedPasswordAttemptCount
                                      , DateTime? expirationDate
                                      , bool? enabled
                                      , Account account
                                      , Country country
                                      , Role userRole
                                      , DateTime? lastLoginDate)
        {


            var ce = GetUser();

            if (ce == null)
                return;

            if (firstName != null)
                ce.FirstName = firstName;
            if (lastName != null)
                ce.LastName = lastName;
            if (email != null)
                ce.Email = email;
            if ( address1 != null )
                ce.Address1 = address1;
            if ( address2 != null )
                ce.Address2 = address2;
            if ( cityTown != null )
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
                ce.Password = password;
            if (passwordQuestion != null)
                ce.PasswordQuestion = passwordQuestion;
            if (passwordAnswer != null)
                ce.PasswordAnswer = passwordAnswer;
            if (lastFailedLoginDate != null)
                ce.LastFailedLoginDate = lastFailedLoginDate;
            if (lastActivityDate != null)
                ce.LastActivityDate = lastActivityDate;
            if (lastPasswordChangedDate != null)
                ce.LastPasswordChangedDate = lastPasswordChangedDate;
            if (isLockedOut != null)
                ce.IsLockedOut = isLockedOut;
            if (lockedOutDate != null)
                ce.LockedOutDate = lockedOutDate;
            if (failedPasswordAttemptCount != null)
                ce.FailedPasswordAttemptCount = failedPasswordAttemptCount;
            if (expirationDate != null)
                ce.ExpirationDate = expirationDate;

            if (enabled != null)
                ce.Enabled = enabled;

            if (account != null)
                ce.Account = account;
            if (country != null)
                ce.Country = country;
            if (userRole != null)
                ce.UserRole = userRole;
            if (lastLoginDate != null)
                ce.LastLoginDate = lastLoginDate;
        }

        public static void UpdateAccount(String name
                                         , int? maxNumberOfUser
                                         , int? maxNumberOfDomainUser
                                         , int? maxNumberOfProjects
                                         , String companyName
                                         , String companyAddress1
                                         , String companyAddress2
                                         , String companyCity
                                         , String companyState
                                         , String companyZip
                                         , String creditCardNumber
                                         , CreditCardType creditCardType
                                         , String creditCardAddress1
                                         , String creditCardAddress2
                                         , String creditCardCity
                                         , String creditCardZip
                                         , bool? recurringBill
                                         , bool? enabled
                                         , DateTime? accountExpirationDate
                                         , DateTime? creditCardExpiration
                                         , Account owner, String creditCardEmail
                                         , int? creditCardIdCountry
                                         , String creditCardState
                                         , String creditCardCardholder
                                         , String promoCode
                                         , int? companyIdCountry
                                         , String creditCardCVS
                                         , String companyPhone
                                         , String creditCardPhone
                                         , int? idSubscriptionLevel
            )
        {

            var ce = GetAccount();

            if (ce == null)
            {
                return;
            }
            if (name != null)
                ce.Name = name;
            if (maxNumberOfUser != null)
                ce.MaxNumberOfUser = maxNumberOfUser;
            if (maxNumberOfDomainUser != null)
                ce.MaxNumberOfDomainUser = maxNumberOfDomainUser;
            if (maxNumberOfProjects != null)
                ce.MaxNumberOfProjects = maxNumberOfProjects;
            if (companyName != null)
                ce.CompanyName = companyName;
            if ( companyAddress1 != null )
                ce.CompanyAddress1 = companyAddress1;
            if ( companyAddress2 != null )
                ce.CompanyAddress2 = companyAddress2;
            if (companyCity != null)
                ce.CompanyCity = companyCity;
            if (companyState != null)
                ce.CompanyState = companyState;
            if (companyZip != null)
                ce.CompanyZip = companyZip;
            if (creditCardNumber != null)
                ce.CreditCardNumber = creditCardNumber;
            if (creditCardType != null)
                ce.CreditCardType = creditCardType;
            if ( creditCardAddress1 != null )
                ce.CreditCardAddress1 = creditCardAddress1;
            if ( creditCardAddress2 != null )
                ce.CreditCardAddress2 = creditCardAddress2;
            if (creditCardCity != null)
                ce.CreditCardCity = creditCardCity;
            if (creditCardZip != null)
                ce.CreditCardZip = creditCardZip;
            if (recurringBill != null)
                ce.RecurringBill = recurringBill;
            if (enabled != null)
                ce.Enabled = enabled;
            if (accountExpirationDate != null)
                ce.AccountExpirationDate = accountExpirationDate;
            if (creditCardExpiration != null)
                ce.CreditCardExpiration = creditCardExpiration;
            if (owner != null)
                ce.Owner = owner;
            if (creditCardEmail != null)
                ce.CreditCardEmail = creditCardEmail;
            if (creditCardIdCountry != null)
                ce.CreditCardIdCountry = creditCardIdCountry;
            if (creditCardState != null)
                ce.CreditCardState = creditCardState;
            if (creditCardCardholder != null)
                ce.CreditCardCardholder = creditCardCardholder;
            if (promoCode != null)
                ce.PromoCode = promoCode;
            if (companyIdCountry != null)
                ce.CompanyIdCountry = companyIdCountry;
            if (companyPhone != null)
                ce.CompanyPhone = companyPhone;
            if (creditCardPhone != null)
                ce.CreditCardPhone = creditCardPhone;
            if (creditCardCVS != null)
                ce.CreditCardCvs = creditCardCVS;
            if(idSubscriptionLevel.HasValue)
                ce.SubscriptionLevel=new SubscriptionLevel{Id = idSubscriptionLevel.Value};
        }

        internal static void CleanSessionData()
        {
            HttpContext.Current.Session[_tokenAccount] = null;
            HttpContext.Current.Session[_tokenUser] = null;
        }
    }
}
