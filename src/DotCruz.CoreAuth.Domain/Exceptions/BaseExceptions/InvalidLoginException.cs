using DotCruz.CoreAuth.Domain.Exceptions.Resources;
using System.Net;

namespace DotCruz.CoreAuth.Domain.Exceptions.BaseExceptions
{
    public class InvalidLoginException : CoreAuthException
    {
        public InvalidLoginException() : base(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID) { }

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;

        public override IEnumerable<string> GetErrorsMessages() => [Message];
    }
}
