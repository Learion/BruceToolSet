#region Using Directives

using System;

#endregion

namespace NHibernateDataStore.Exceptions
{
    [Serializable]
    public class ConnectionElementNotFoundException : BaseException
    {
        public ConnectionElementNotFoundException(string element)
            : base("Connection string element " + element + " not found in the specified connection")
        {
        }
    }
}