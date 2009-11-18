using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SEOToolSet.Model
{
    public class Account
    {
        public  int Id { get; set; }

        public  string Name { get; set; }

        [SoapElement(IsNullable = true)]
        public  Int32? MaxNumberOfUser { get; set; }

        [SoapElement(IsNullable = true)]
        public  Int32? MaxNumberOfDomainUser { get; set; }

        [SoapElement(IsNullable = true)]
        public  Int32? MaxNumberOfProjects { get; set; }

        public  string CompanyName { get; set; }

        public  string CompanyAddress1 { get; set; }

        public  string CompanyAddress2 { get; set; }

        public  string CompanyCity { get; set; }

        public  string CompanyState { get; set; }

        public  string CompanyZip { get; set; }

        public  string CreditCardNumber { get; set; }

        public  string CreditCardCvs { get; set; }

        public  string CreditCardAddress1 { get; set; }

        public  string CreditCardAddress2 { get; set; }

        public  string CreditCardCity { get; set; }

        public  string CreditCardZip { get; set; }

        [SoapElement(IsNullable = true)]
        public  Boolean? RecurringBill { get; set; }

        [SoapElement(IsNullable = true)]
        public  DateTime? CreatedDate { get; set; }

        [SoapElement(IsNullable = true)]
        public  DateTime? UpdatedDate { get; set; }

        public  string CreatedBy { get; set; }

        public  string UpdatedBy { get; set; }

        [SoapElement(IsNullable = true)]
        public  Boolean? Enabled { get; set; }

        public  String PromoCode { get; set; }

        public  Int32? CompanyIdCountry { get; set; }

        public  String CompanyPhone { get; set; }

        public  String CreditCardPhone { get; set; }

        public  DateTime? LastBillingDate { get; set; }
    }
}
