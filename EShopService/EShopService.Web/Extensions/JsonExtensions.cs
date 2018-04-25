using System.Collections.Generic;
using Newtonsoft.Json;

namespace EShopService.Web.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented,
                Converters = new List<JsonConverter>()
            });
        }
    }
}