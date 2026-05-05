namespace DotCruz.CoreAuth.Domain.Interfaces.Security.Tokens
{
    public interface IRefreshTokenGenerator
    {
        public string Generate();
    }
}
