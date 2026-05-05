using DotCruz.CoreAuth.Domain.Entities.Tokens;
using DotCruz.CoreAuth.Domain.Interfaces.Repositories.Tokens;
using DotCruz.CoreAuth.Infrastructure.Data;
using DotCruz.CoreAuth.Infrastructure.Repositories.Base;

namespace DotCruz.CoreAuth.Infrastructure.Repositories.Tokens
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenReadRepository, IRefreshTokenWriteRepository
    {
        public RefreshTokenRepository(CoreAuthDbContext context) : base(context) { }
    }
}
