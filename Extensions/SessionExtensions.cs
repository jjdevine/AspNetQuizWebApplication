using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApplication.Extensions
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            var serializedObj = JsonConvert.SerializeObject(value);
            Console.WriteLine($"writing {key} to session - {serializedObj}");
            session.SetString(key, serializedObj);
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            Console.WriteLine($"reading {key} from session - {value}");
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
