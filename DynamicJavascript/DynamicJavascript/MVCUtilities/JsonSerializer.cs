using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace DynamicJavascript.MVCUtilities
{
    public static class JsonSerializer
    {
        public static string SerializeToJson(object valueToSerialize)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Serialize(valueToSerialize);
        }

        public static T DeserializeFromJson<T>(string input)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return (T)serializer.Deserialize(input, typeof(T));
        }
    }
}