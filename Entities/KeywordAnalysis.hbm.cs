﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3082
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#region

using System;
using System.Xml.Serialization;

#endregion

namespace SEOToolSet.Entities
{
    [Serializable]
    public class AbstractKeywordAnalysis
    {
        public virtual int Id { get; set; }

        public virtual string Keyword { get; set; }

        [SoapElement(IsNullable = true)]
        public virtual Int32? GoogleResults { get; set; }

        [SoapElement(IsNullable = true)]
        public virtual Int32? AllInTitle { get; set; }

        [SoapElement(IsNullable = true)]
        public virtual Int32? AliasDomains { get; set; }

        [SoapElement(IsNullable = true)]
        public virtual Double? CPC { get; set; }

        [SoapElement(IsNullable = true)]
        public virtual Int32? DailySearches { get; set; }

        [SoapElement(IsNullable = true)]
        public virtual Int32? Results { get; set; }

        [SoapElement(IsNullable = true)]
        public virtual Int32? Engines { get; set; }

        [SoapElement(IsNullable = true)]
        public virtual Int32? Pages { get; set; }

        public virtual string Status { get; set; }

        public virtual RankingMonitorRun RankingMonitorRun { get; set; }
    }

    [Serializable]
    public class KeywordAnalysis : AbstractKeywordAnalysis
    {
    }
}