﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#region Using Directives

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

#endregion

namespace SEOToolSet.Entities
{
    [Serializable]
    [XmlInclude(typeof (Subscription))]
    [SoapInclude(typeof (Subscription))]
    public class AbstractService
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        [SoapElement(IsNullable = true)]
        public virtual Boolean? Enabled { get; set; }

        public virtual IList<Subscription> Subscription { get; set; }
    }

    [Serializable]
    public class Service : AbstractService
    {
    }
}