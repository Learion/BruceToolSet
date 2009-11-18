
using System;

namespace SEOToolSet.Common
{
    public class SerializeHelper
    {
        /// <summary>
        /// Converts the input into a JSON string
        /// </summary>
        /// <typeparam name="T">Type of report</typeparam>
        /// <param name="input">Object input</param>
        /// <param name="serializeTypeInput">Kind of serialization for the input</param>
        /// <returns>Returns the result as JSON format</returns>
        public static string GetJsonResult<T>(T input,
                                               ObjectSerializerType serializeTypeInput) where T : class
        {
            string jsonResult = string.Empty;
            var inputText = input as string;
            switch (serializeTypeInput)
            {
                case ObjectSerializerType.Json:
                    //It is JSON, so we can pass this value directly
                    if (inputText != null)
                        jsonResult = inputText;
                    break;
                case ObjectSerializerType.Xml:
                    //First, we have to deserialize to an T object, then we can serialize the object to a JSON string
                    if (inputText != null)
                    {
                        var xmlObject = ObjectSerializer.Deserialize<T>(inputText, ObjectSerializerType.Xml);
                        jsonResult = ObjectSerializer.Serialize(xmlObject, ObjectSerializerType.Json);
                    }
                    break;
                case ObjectSerializerType.Object:
                    //We just serialize the object to a JSON string
                    jsonResult = ObjectSerializer.Serialize((T)input, ObjectSerializerType.Json);
                    break;
                default:
                    jsonResult = string.Empty;
                    break;
            }
            return jsonResult;
        }
        public static T Serialize<T>(string input, ObjectSerializerType type) where T : class
        {
            switch (type)
            {
                case ObjectSerializerType.Json:
                    return ObjectSerializer.Deserialize<T>(input, ObjectSerializerType.Json);
                case ObjectSerializerType.Xml:
                    throw new NotImplementedException();
                case ObjectSerializerType.Object:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
            return null;
        }
    }
}
