#region

using System;

#endregion

namespace SEOToolSet.Entities.Wrappers
{
    public class AccountWrapper
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual Int32? MaxNumberOfUser { get; set; }

        public virtual Int32? MaxNumberOfDomainUser { get; set; }

        public virtual Int32? MaxNumberOfProjects { get; set; }

        public virtual string CompanyName { get; set; }

        public virtual string CompanyAddress1 { get; set; }

        public virtual string CompanyAddress2 { get; set; }

        public virtual string CompanyCity { get; set; }

        public virtual string CompanyState { get; set; }

        public virtual string CompanyZip { get; set; }

        public virtual string CreditCardNumber { get; set; }

        //public virtual CreditCardType CreditCardType { get; set; }

        public virtual string CreditCardCvs { get; set; }

        public virtual string CreditCardAddress1 { get; set; }

        public virtual string CreditCardAddress2 { get; set; }

        public virtual string CreditCardCity { get; set; }

        public virtual string CreditCardZip { get; set; }

        public virtual Boolean? RecurringBill { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        public virtual DateTime? UpdatedDate { get; set; }

        public virtual string CreatedBy { get; set; }

        public virtual string UpdatedBy { get; set; }

        public virtual Boolean? Enabled { get; set; }

        public virtual DateTime? AccountExpirationDate { get; set; }

        public virtual DateTime? CreditCardExpiration { get; set; }

        //       public virtual AccountWrapper Owner { get; set; }

        public virtual String CreditCardEmail { get; set; }

        public virtual Int32? CreditCardIdCountry { get; set; }

        public virtual String CreditCardState { get; set; }

        public virtual String CreditCardCardholder { get; set; }

        public virtual String PromoCode { get; set; }

        public virtual Int32? CompanyIdCountry { get; set; }

        public virtual String CompanyPhone { get; set; }

        public virtual String CreditCardPhone { get; set; }

        public virtual long? SubscriptionId { get; set; }

        public virtual DateTime? LastBillingDate { get; set; }

        public static implicit operator AccountWrapper(Account account)
        {
            if (account == null) return null;
            return new AccountWrapper
                       {
                           Id = account.Id,
                           Name = account.Name,
                           MaxNumberOfUser = account.MaxNumberOfUser,
                           MaxNumberOfDomainUser = account.MaxNumberOfDomainUser,
                           MaxNumberOfProjects = account.MaxNumberOfProjects,
                           CompanyName = account.CompanyName,
                           CompanyAddress1 = account.CompanyAddress1,
                           CompanyAddress2 = account.CompanyAddress2,
                           CompanyCity = account.CompanyCity,
                           CompanyState = account.CompanyState,
                           CompanyZip = account.CompanyZip,
                           CreditCardNumber = account.CreditCardNumber,
                           //CreditCardType = account.CreditCardType,
                           CreditCardCvs = account.CreditCardCvs,
                           CreditCardAddress1 = account.CreditCardAddress1,
                           CreditCardAddress2 = account.CreditCardAddress2,
                           CreditCardCity = account.CreditCardCity,
                           CreditCardZip = account.CreditCardZip,
                           RecurringBill = account.RecurringBill,
                           CreatedDate = account.CreatedDate,
                           UpdatedDate = account.UpdatedDate,
                           CreatedBy = account.CreatedBy,
                           UpdatedBy = account.UpdatedBy,
                           Enabled = account.Enabled,
                           AccountExpirationDate = account.AccountExpirationDate,
                           CreditCardExpiration = account.CreditCardExpiration,
                           //Owner = account.Owner,
                           CreditCardEmail = account.CreditCardEmail,
                           CreditCardIdCountry = account.CreditCardIdCountry,
                           CreditCardState = account.CreditCardState,
                           CreditCardCardholder = account.CreditCardCardholder,
                           PromoCode = account.PromoCode,
                           CompanyIdCountry = account.CompanyIdCountry,
                           CompanyPhone = account.CompanyPhone,
                           CreditCardPhone = account.CreditCardPhone,
                           SubscriptionId = account.SubscriptionId,
                           LastBillingDate = account.LastBillingDate
                       };
        }
    }
}