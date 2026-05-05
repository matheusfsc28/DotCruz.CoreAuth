using DotCruz.CoreAuth.Domain.Entities.Tokens;
using DotCruz.CoreAuth.Domain.Interfaces.Repositories.Base;

namespace DotCruz.CoreAuth.Domain.Interfaces.Repositories.Tokens
{
    public interface IRefreshTokenWriteRepository : IBaseWriteRepository<RefreshToken>
    {
    }
}
