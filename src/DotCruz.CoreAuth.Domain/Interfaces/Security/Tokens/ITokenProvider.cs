namespace DotCruz.CoreAuth.Domain.Interfaces.Security.Tokens
{
    public interface ITokenProvider
    {
        public string Value();
        public string Hash(string token);
    }
}
