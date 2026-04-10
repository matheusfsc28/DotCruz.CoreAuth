using CoreAuth.Domain.Entities.Users;
using CoreAuth.Domain.Interfaces.Repositories.Users;
using CoreAuth.Infrastructure.Data;
using CoreAuth.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CoreAuth.Infrastructure.Repositories.Users
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
