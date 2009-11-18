#region Using Directives

using System;
using System.Web;
using NHibernate.Type;

#endregion

namespace NHibernateDataStore.Interceptor
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
        public override bool OnFlushDirty(object entity, object id, object[] currentState, object[] previousState,
                                          string[] propertyNames, IType[] types)
        {
            var aChangeHappen = false;
            for (var i = 0; i < propertyNames.Length; i++)
            {
                if (propertyNames[i] == FieldUpdatedOn)
                {
                    currentState[i] = DateTime.Now;
                    aChangeHappen = true;
                }
                if (propertyNames[i] != FieldUpdatedBy) continue;
                if (HttpContext.Current.User.Identity.Name == null) continue;
                currentState[i] = HttpContext.Current.User.Identity.Name;
                aChangeHappen = true;
            }
            return aChangeHappen;
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
        public override bool OnSave(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            var aChangeHappen = false;
            for (var i = 0; i < propertyNames.Length; i++)
            {
                if (propertyNames[i] == FieldCreatedOn)
                {
                    state[i] = DateTime.Now;
                    aChangeHappen = true;
                }
                if (propertyNames[i] != FieldCreatedBy) continue;
                if (HttpContext.Current.User.Identity.Name == null) continue;
                state[i] = HttpContext.Current.User.Identity.Name;
                aChangeHappen = true;
            }

            return aChangeHappen;
        }
    }
}