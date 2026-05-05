using DotCruz.CoreAuth.Domain.Entities.Users;
using DotCruz.CoreAuth.Domain.Interfaces.Repositories.Users;
using DotCruz.CoreAuth.Infrastructure.Data;
using DotCruz.CoreAuth.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DotCruz.CoreAuth.Infrastructure.Repositories.Users
{
    public class UserRepository : BaseRepository<User>, IUserReadRepository, IUserWriteRepository
    {
        public UserRepository(CoreAuthDbContext context) : base(context) { }

        public async Task<bool> ExistsActiveUserWithEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking().AnyAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }
    }
}
