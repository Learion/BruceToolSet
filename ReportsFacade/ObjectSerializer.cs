#region Using Directives

using System;
using Newtonsoft.Json;
using Serializer;

#endregion

namespace SEOToolSet.ReportsFacade
{
    internal static class ObjectSerializer
    {
        internal static string Serialize<T>(T objectToSerialize,
                                            ObjectSerializerType serializeType) where T : class
        {
            String result;

            switch (serializeType)
            {
                case ObjectSerializerType.Json:
                    result = JavaScriptConvert.SerializeObject(objectToSerialize);
                    break;
                case ObjectSerializerType.Xml:
                    result = ObjectXmlSerializer.SaveToString(objectToSerialize);
                    break;
                default:
                    throw new ArgumentException("The type is not correct", "serializeType");
            }

            return result;
        }

        internal static T Deserialize<T>(string serializedObject,
                                         ObjectSerializerType serializeTypeInput) where T : class
        {
            T outputObject;
            switch (serializeTypeInput)
            {
                case ObjectSerializerType.Json:
                    outputObject = JavaScriptConvert.DeserializeObject<T>(serializedObject);
                    break;
                case ObjectSerializerType.Xml:
                    ObjectXmlSerializer.LoadFromString(serializedObject, out outputObject);
                    break;
                default:
                    outputObject = null;
                    break;
            }
            return outputObject;
        }
    }
}