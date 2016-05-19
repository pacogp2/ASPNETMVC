using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;

namespace SitioWebMVC.MisControles
{
    static class ExpandoObjectExtensions
    {
        public static dynamic CopyFrom(this ExpandoObject source, object data)
        {
            var dict = source as IDictionary<string, object>;
            foreach (var property in data.GetType().GetProperties())
            {
                dict.Add(property.Name, property.GetValue(data, null));
            }
            return source;
        }
        public static IEnumerable<dynamic> Dynamize<T>(this IEnumerable<T> source)
        {
            foreach (var entry in source)
            {
                var expando = new ExpandoObject();
                yield return expando.CopyFrom(entry);
            }
        }
    }
}