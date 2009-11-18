using System;
using Eucalypto.Interceptor;
using NHibernateDataStore.Interceptor;

namespace Eucalypto.Interceptor
{
    /// <summary>
    /// NHibernate interceptor used to update fields UpdateDate and InsertDate of the entities that use the IAudit interface
    /// </summary>
    [Serializable]
    public class EucalyptoInterceptor : InterceptorBase
    {
        /// <summary>
        /// On update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <param name="currentState"></param>
        /// <param name="previousState"></param>
        /// <param name="propertyNames"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public override bool OnFlushDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            if (entity is IAudit)
            {
                for (var i = 0; i < propertyNames.Length; i++)
                {
                    if (propertyNames[i] != AuditableProperties.FIELD_UPDATEDATE) continue;
                    currentState[i] = DateTime.Now;
                    return true;
                }
            }

            return base.OnFlushDirty(entity, id, currentState, previousState, propertyNames, types);
        }

        /// <summary>
        /// On Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <param name="propertyNames"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public override bool OnSave(object entity, object id, object[] state, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            if (entity is IAudit )
            {
                for (var i = 0; i < propertyNames.Length; i++)
                {
                    if (propertyNames[i] == AuditableProperties.FIELD_INSERT_DATE)
                    {
                        state[i] = DateTime.Now;
                    }
                    else if (propertyNames[i] == AuditableProperties.FIELD_UPDATEDATE)
                    {
                        state[i] = DateTime.Now;
                    }
                }

                return true;
            }

            return base.OnSave(entity, id, state, propertyNames, types);
        }
    }
}