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

#endregion

namespace SEOToolSet.Entities
{
    [Serializable]
    public class AbstractProjectPermissionRole
    {
        public virtual int Id { get; set; }

        public virtual ProjectPermission ProjectPermission { get; set; }

        public virtual ProjectRole ProjectRole { get; set; }
    }

    [Serializable]
    public class ProjectPermissionRole : AbstractProjectPermissionRole
    {
    }
}