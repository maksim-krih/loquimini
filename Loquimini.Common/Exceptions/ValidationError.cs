using Newtonsoft.Json;
using System;

namespace Loquimini.Common.Exceptions
{
    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }

        public string Message { get; }

        public ValidationError(string field, string message)
        {
            Field = field != string.Empty ? char.ToLowerInvariant(field[0]) + field.Substring(1) : null;
            Message = message;
        }
    }
}
