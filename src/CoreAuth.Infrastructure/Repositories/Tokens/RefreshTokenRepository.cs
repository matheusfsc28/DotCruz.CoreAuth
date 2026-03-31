using CoreAuth.Domain.Entities.Tokens;
using CoreAuth.Domain.Interfaces.Repositories.Tokens;
using CoreAuth.Infrastructure.Data;
using CoreAuth.Infrastructure.Repositories.Base;

namespace CoreAuth.Infrastructure.Repositories.Tokens
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenReadRepository, IRefreshTokenWriteRepository
    {
        public RefreshTokenRepository(CoreAuthDbContext context) : base(context) { }
    }
}
