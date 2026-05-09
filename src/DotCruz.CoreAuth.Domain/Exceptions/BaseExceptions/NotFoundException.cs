using System.Net;

namespace DotCruz.CoreAuth.Domain.Exceptions.BaseExceptions
{
    public class NotFoundException : CoreAuthException
    {
        public NotFoundException(string message) : base(message) { }

        public override IEnumerable<string> GetErrorsMessages() => [Message];
        public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
    }
}
