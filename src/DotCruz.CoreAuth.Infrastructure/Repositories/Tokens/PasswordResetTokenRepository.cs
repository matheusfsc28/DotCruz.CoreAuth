using DotCruz.CoreAuth.Domain.Entities.Tokens;
using DotCruz.CoreAuth.Domain.Interfaces.Repositories.Tokens;
using DotCruz.CoreAuth.Infrastructure.Data;
using DotCruz.CoreAuth.Infrastructure.Repositories.Base;

namespace DotCruz.CoreAuth.Infrastructure.Repositories.Tokens
{
    public class PasswordResetTokenRepository : BaseRepository<PasswordResetToken>, IPasswordResetTokenReadRepository, IPasswordResetTokenWriteRepository
    {
        public PasswordResetTokenRepository(CoreAuthDbContext context) : base(context) { }
    }
}
