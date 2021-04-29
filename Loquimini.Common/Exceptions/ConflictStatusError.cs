using System.Net;

namespace Loquimini.Common.Exceptions
{
    public class ConflictStatusError : StatusException
    {
        public override int StatusCode => (int)HttpStatusCode.Conflict;

        public ConflictStatusError() : base("Conflict")
        {
        }

        public ConflictStatusError(string message)
            : base(message)
        {
        }
    }
}
