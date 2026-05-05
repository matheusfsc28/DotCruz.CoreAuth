using System.Net;

namespace DotCruz.CoreAuth.Exceptions.BaseExceptions
{
    public class ErrorOnValidationException : CoreAuthException
    {
        private readonly IEnumerable<string> _errors;

        public ErrorOnValidationException(IEnumerable<string> errors) : base(string.Empty)  => _errors = errors;
        public ErrorOnValidationException(string error) : base(error)  => _errors = [error];

        public override IEnumerable<string> GetErrorsMessages() => _errors;

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
