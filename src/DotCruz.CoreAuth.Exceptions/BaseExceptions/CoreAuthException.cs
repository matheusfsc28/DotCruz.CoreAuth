using System.Net;

namespace DotCruz.CoreAuth.Exceptions.BaseExceptions
{
    public abstract class CoreAuthException : SystemException
    {
        protected CoreAuthException(string message) : base(message) { }
        public abstract IEnumerable<string> GetErrorsMessages();
        public abstract HttpStatusCode GetStatusCode();
    }
}
