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

#endregion

namespace SEOToolSet.Entities
{
    [Serializable]
    public class AbstractMonitorKeywordList
    {
        public virtual int Id { get; set; }

        public virtual Project Project { get; set; }

        public virtual KeywordList KeywordList { get; set; }
    }

    [Serializable]
    public class MonitorKeywordList : AbstractMonitorKeywordList
    {
    }
}