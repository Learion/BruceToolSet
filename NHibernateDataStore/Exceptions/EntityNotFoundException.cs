#region Using Directives

using System;

#endregion

namespace NHibernateDataStore.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : BaseException
    {
        public EntityNotFoundException()
            : base("Entity not found")
        {
        }
    }
}