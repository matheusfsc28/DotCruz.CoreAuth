using DotCruz.CoreAuth.Domain.Interfaces.Security.Tokens;

namespace DotCruz.CoreAuth.Infrastructure.Security.Tokens.Refresh
{
    public class RefreshTokenGeneratorRefreshTokenGenerator : IRefreshTokenGenerator
    {
        public string Generate() => Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }
}
