#region

using System;
using System.Reflection;

#endregion

namespace SEOToolSet.Common
{
    public static class ReflectorHelper
    {
        public static object GetMembers(object objName, string propertyName)
        {
            Type objType = objName.GetType();
            FieldInfo fild = GetFieldInfo(objType, propertyName);
            if (fild == null)
            {
                throw new ArgumentException("There is no field '" +
                                            propertyName + "' for type '" + objName.GetType() + "'.");
            }
            return fild.GetValue(objName);
        }

        public static void SetMembers(object objName, string propertyName, object aValue)
        {
            FieldInfo fld = GetFieldInfo(objName.GetType(), propertyName);
            if (fld == null)
            {
                throw new ArgumentException("There is no field '" +
                                            propertyName + "' for type '" + objName.GetType() + "'.");
            }
            fld.SetValue(objName, aValue);
        }

        private static FieldInfo GetFieldInfo(Type objType, string propertyName)
        {
            FieldInfo fild = objType.GetField(propertyName, BindingFlags.Instance | BindingFlags.NonPublic);
            return fild ?? (objType.GetType() == typeof (object) ? null : GetFieldInfo(objType.BaseType, propertyName));
        }

        public static object RunInstanceMethod(object objInstance, string strMethod, object[] aobjParams)
        {
            BindingFlags eFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod |
                                  BindingFlags.NonPublic;
            return RunMethod(strMethod, objInstance, aobjParams, eFlags);
        }

        private static object RunMethod(string strMethod, object objInstance, object[] aobjParams, BindingFlags eFlags)
        {
            Type t = objInstance.GetType();
            MethodInfo m = GetMethod(t, strMethod, eFlags);
            if (m == null)
            {
                throw new ArgumentException("There is no method '" +
                                            strMethod + "' for type '" + t + "'.");
            }

            object objRet = m.Invoke(objInstance, aobjParams);
            return objRet;
        }

        private static MethodInfo GetMethod(Type t, string strMethod, BindingFlags eFlags)
        {
            MethodInfo m = t.GetMethod(strMethod, eFlags);
            return m ?? (t.GetType() == typeof (object) ? null : GetMethod(t.BaseType, strMethod, eFlags));
        }
    }
}