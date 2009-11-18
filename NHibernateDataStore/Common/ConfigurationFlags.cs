#region Using Directives

using System;

#endregion

namespace NHibernateDataStore.Common
{
    [Flags]
    public enum ConfigurationFlags
    {
        None = 0, //0000 0000
        Settings = 1, //0000 0001
        Mappings = 2, //0000 0010
        Interceptor = 4, //0000 0100
        MappingsToExport = 8, //0000 1000
        Default = 7 //0000 0111
    }
}