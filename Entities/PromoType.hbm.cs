﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SEOToolSet.Entities
{
    [Serializable]
    [XmlInclude(typeof (PromoCode))]
    [SoapInclude(typeof (PromoCode))]
    public class AbstractPromoType
    {
        public virtual int Id { get; set; }

        public virtual string Description { get; set; }

        public virtual IList<PromoCode> PromoCode { get; set; }
    }

    [Serializable]
    public class PromoType : AbstractPromoType
    {
    }
}