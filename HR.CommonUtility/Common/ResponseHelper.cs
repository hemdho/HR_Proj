using System;
using System.Collections.Concurrent;

namespace HR.CommonUtility
{
    sealed public class ResponseHelper
    {
        public Int16 Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; } = string.Empty; //user <T> generic data type passing as object
    }

    //static public class GlobalDictionary
    //{
    //    static private readonly ConcurrentDictionary<string, object> cdDictionary = new ConcurrentDictionary<string, object>();

    //    static public void Add(string id, object obj)
    //    {
    //        cdDictionary.TryAdd(id, obj);
    //    }

    //    static public object Get(string id)
    //    {
    //        object value;
    //        cdDictionary.TryGetValue(id, out value);
    //        return value;
    //    }
    //}
}
