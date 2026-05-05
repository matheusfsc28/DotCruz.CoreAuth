namespace DotCruz.CoreAuth.Domain.Interfaces.Security.Tokens
{
    public interface IAccessTokenGenerator
    {
        public string Generate(Guid userId);
    }
}
