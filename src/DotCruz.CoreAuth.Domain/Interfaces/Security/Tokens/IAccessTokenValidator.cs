namespace DotCruz.CoreAuth.Domain.Interfaces.Security.Tokens
{
    public interface IAccessTokenValidator
    {
        public Guid ValidateAndGetUserIdentifier(string token);
    }
}
