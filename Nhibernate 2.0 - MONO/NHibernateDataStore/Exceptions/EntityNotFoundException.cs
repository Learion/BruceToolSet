using System;

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