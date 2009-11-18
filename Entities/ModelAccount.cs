using System;
using System.Collections.Generic;
using System.Text;

namespace SEOToolSet.Entities
{
    public class ModelAccount
    {
        #region Model
        private int _id;
        private string _name;
        private int _maxnumberofuser;
        private int _maxnumberofdomainuser;
        private int _maxnumberofprojects;
        private string _companyname;
        private string _companyaddress1;
        private string _companyaddress2;
        private string _companycity;
        private string _companystate;
        private string _companyzip;
        private string _creditcardnumber;
        private string _creditcardcvs;
        private string _creditcardaddress1;
        private string _creditcardaddress2;
        private string _creditcardcity;
        private string _creditcardzip;
        private int _recurringbill;
        private DateTime _createddate;
        private DateTime _updateddate;
        private string _createdby;
        private string _updatedby;
        private int _enabled;
        private DateTime _accountexpirationdate;
        private DateTime _creditcardexpiration;
        private string _creditcardemail;
        private int _creditcardidcountry;
        private string _creditcardstate;
        private string _creditcardcardholder;
        private string _promocode;
        private int _companyidcountry;
        private string _companyphone;
        private string _creditcardphone;
        private int _subscriptionid;
        private DateTime _lastbillingdate;
        private string _creditcardtype;
        private int _idsubscriptionlevel;
        private int _idaccountowner;
         
         
         
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
         
         
         
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
         
         
         
        public int MaxNumberOfUser
        {
            set { _maxnumberofuser = value; }
            get { return _maxnumberofuser; }
        }
         
         
         
        public int MaxNumberOfDomainUser
        {
            set { _maxnumberofdomainuser = value; }
            get { return _maxnumberofdomainuser; }
        }
         
         
         
        public int MaxNumberOfProjects
        {
            set { _maxnumberofprojects = value; }
            get { return _maxnumberofprojects; }
        }
         
         
         
        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
         
         
         
        public string CompanyAddress1
        {
            set { _companyaddress1 = value; }
            get { return _companyaddress1; }
        }
         
         
         
        public string CompanyAddress2
        {
            set { _companyaddress2 = value; }
            get { return _companyaddress2; }
        }
         
         
         
        public string CompanyCity
        {
            set { _companycity = value; }
            get { return _companycity; }
        }
         
         
         
        public string CompanyState
        {
            set { _companystate = value; }
            get { return _companystate; }
        }
         
         
         
        public string CompanyZip
        {
            set { _companyzip = value; }
            get { return _companyzip; }
        }
         
         
         
        public string CreditCardNumber
        {
            set { _creditcardnumber = value; }
            get { return _creditcardnumber; }
        }
         
         
         
        public string CreditCardCVS
        {
            set { _creditcardcvs = value; }
            get { return _creditcardcvs; }
        }
         
         
         
        public string CreditCardAddress1
        {
            set { _creditcardaddress1 = value; }
            get { return _creditcardaddress1; }
        }
         
         
         
        public string CreditCardAddress2
        {
            set { _creditcardaddress2 = value; }
            get { return _creditcardaddress2; }
        }
         
         
         
        public string CreditCardCity
        {
            set { _creditcardcity = value; }
            get { return _creditcardcity; }
        }
         
         
         
        public string CreditCardZip
        {
            set { _creditcardzip = value; }
            get { return _creditcardzip; }
        }
         
         
         
        public int RecurringBill
        {
            set { _recurringbill = value; }
            get { return _recurringbill; }
        }
         
         
         
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
         
         
         
        public DateTime UpdatedDate
        {
            set { _updateddate = value; }
            get { return _updateddate; }
        }
         
         
         
        public string CreatedBy
        {
            set { _createdby = value; }
            get { return _createdby; }
        }
         
         
         
        public string UpdatedBy
        {
            set { _updatedby = value; }
            get { return _updatedby; }
        }
         
         
         
        public int Enabled
        {
            set { _enabled = value; }
            get { return _enabled; }
        }
         
         
         
        public DateTime AccountExpirationDate
        {
            set { _accountexpirationdate = value; }
            get { return _accountexpirationdate; }
        }
         
         
         
        public DateTime CreditCardExpiration
        {
            set { _creditcardexpiration = value; }
            get { return _creditcardexpiration; }
        }
         
         
         
        public string CreditCardEmail
        {
            set { _creditcardemail = value; }
            get { return _creditcardemail; }
        }
         
         
         
        public int CreditCardIdCountry
        {
            set { _creditcardidcountry = value; }
            get { return _creditcardidcountry; }
        }
         
         
         
        public string CreditCardState
        {
            set { _creditcardstate = value; }
            get { return _creditcardstate; }
        }
         
         
         
        public string CreditCardCardholder
        {
            set { _creditcardcardholder = value; }
            get { return _creditcardcardholder; }
        }
         
         
         
        public string PromoCode
        {
            set { _promocode = value; }
            get { return _promocode; }
        }
         
         
         
        public int CompanyIdCountry
        {
            set { _companyidcountry = value; }
            get { return _companyidcountry; }
        }
         
         
         
        public string CompanyPhone
        {
            set { _companyphone = value; }
            get { return _companyphone; }
        }
         
         
         
        public string CreditCardPhone
        {
            set { _creditcardphone = value; }
            get { return _creditcardphone; }
        }
         
         
         
        public int SubscriptionId
        {
            set { _subscriptionid = value; }
            get { return _subscriptionid; }
        }
         
         
         
        public DateTime LastBillingDate
        {
            set { _lastbillingdate = value; }
            get { return _lastbillingdate; }
        }
         
         
         
        public string CreditCardType
        {
            set { _creditcardtype = value; }
            get { return _creditcardtype; }
        }
         
         
         
        public int IdSubscriptionLevel
        {
            set { _idsubscriptionlevel = value; }
            get { return _idsubscriptionlevel; }
        }
         
         
         
        public int IdAccountOwner
        {
            set { _idaccountowner = value; }
            get { return _idaccountowner; }
        }
        #endregion Model
    }
}
