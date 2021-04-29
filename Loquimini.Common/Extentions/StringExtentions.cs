using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Loquimini.Common.Extentions
{
    public static class StringExtentions
    {
        public static string ToCopy(this string value) => $"{value} - Copy";

        public static string TrimToLower(this string value) => value.Trim().ToLower();

        public static T GetProperty<T>(this string value, string propertyName)
        {
            if(value == null) return default;
            var token = JsonConvert.DeserializeObject<JObject>(value).SelectToken(propertyName);
            return token == null ? default : token.Value<T>();
        }

        public static string SetProperty(this string value, string propertyName, string propertyValue)
        {
            var obj = JsonConvert.DeserializeObject<JObject>(value);
            obj[propertyName] = propertyValue;

            return obj.ToString();
        }
    }
}
