using System;

namespace GameFramework
{
    public static partial class Utility
    {
        public static partial class Json
        {
            private static IJsonHelper jsonHelper = null;

            public static void SetJsonHelper(IJsonHelper helper)
            {
                jsonHelper = helper;
            }

            public static string ToJson(object obj)
            {
                return jsonHelper.ToJson(obj);
            }

            public static byte[] ToJsonData(object obj)
            {
                return Converter.GetBytes(ToJson(obj));
            }

            public static T ToObject<T>(string json)
            {
                return jsonHelper.ToObject<T>(json);
            }

            public static T ToObject<T>(byte[] jsonData)
            {
                return ToObject<T>(Converter.GetString(jsonData));
            }

            public static object ToObject(Type objectType, string json)
            {
                return jsonHelper.ToObject(objectType, json);
            }

            public static object ToObject(Type objectType, byte[] jsonData)
            {
                return ToObject(objectType, Converter.GetString(jsonData));
            }
        }
    }
}
