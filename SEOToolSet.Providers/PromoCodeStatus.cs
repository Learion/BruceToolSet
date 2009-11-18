
using System;

namespace SEOToolSet.Providers
{
    ///<summary>
    ///Represents the status of the promotion to be displayed to the user
    ///</summary>
    public class PromoCodeStatus
    {
        ///<summary>
        ///Calculated discount amount given to the subscriber
        ///</summary>
        public double Discount { get; set; }

        ///<summary>
        ///Description of the promotion retrieved as is
        ///</summary>
        public string PromoCodeDescription { get; set; }

        ///<summary>
        ///Indicates the status of the promotion according to its promotion code
        ///</summary>
        public StatusCode StatusCode { get; set; }

        ///<summary>
        ///Indicates a date to reference either the start date or the end date of the promotion
        ///</summary>
        public string ReferenceDate { get; set; }
    }
}
