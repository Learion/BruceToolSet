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
    [XmlInclude(typeof (ProjectPermissionRole))]
    [SoapInclude(typeof (ProjectPermissionRole))]
    public class AbstractProjectPermission
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual IList<ProjectPermissionRole> ProjectPermissionRole { get; set; }
    }

    [Serializable]
    public class ProjectPermission : AbstractProjectPermission
    {
    }
}