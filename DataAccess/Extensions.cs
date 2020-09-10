using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;

namespace DataAccess
{
    public static class Extensions
    {

        //Converting object to string
        public static string ObjectToString(this object data)
        {
            return JsonConvert.SerializeObject(data);
        }

        //Converting string to object
        public static T StringToObject<T>(this string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
