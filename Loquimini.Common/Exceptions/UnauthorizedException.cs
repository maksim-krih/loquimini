using Loquimini.Common.Enums;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Loquimini.Common.Exceptions
{
    [JsonObject(MemberSerialization.OptIn)]
    public class UnauthorizedException : Exception, IUnauthorizeException
    {
        [JsonProperty]
        public int StatusCode => (int)HttpStatusCode.Unauthorized;

        [JsonProperty]
        public UnauthorizeExceptionType Type { get; set; }

        [JsonProperty]
        public override string Message => base.Message;

        public UnauthorizedException(UnauthorizeExceptionType type, string message = "Unauthorized") : base(message)
        {
            Type = type;
        }

        public virtual string GetResponse()
        {
            return JsonConvert.SerializeObject(new { StatusCode, Message });
        }
    }
}
