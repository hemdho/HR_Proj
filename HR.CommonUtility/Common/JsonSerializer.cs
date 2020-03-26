using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace HR.CommonUtility
{
    public class JsonSerializer
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new JsonContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        public sealed class JsonContractResolver : CamelCasePropertyNamesContractResolver
        {
        }

        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, Settings);
        }

        public static T DeserializeObject<T>(string strVal) //generi T
        {
            return JsonConvert.DeserializeObject<T>(strVal);
        }
    }
}