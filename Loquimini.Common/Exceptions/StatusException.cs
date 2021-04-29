using Newtonsoft.Json;
using System;
using System.Net;

namespace Loquimini.Common.Exceptions
{
    [JsonObject(MemberSerialization.OptIn)]
    public class StatusException : Exception, IStatusException
    {
        [JsonProperty]
        public virtual int StatusCode => (int)HttpStatusCode.InternalServerError;

        [JsonProperty]
        public override string Message => base.Message;

        public StatusException() : base("InternalServerError")
        {
        }

        public StatusException(string message)
            : base(message)
        {
        }

        public virtual string GetResponse()
        {
            return JsonConvert.SerializeObject(new { Message, StatusCode });
        }
    }
}
