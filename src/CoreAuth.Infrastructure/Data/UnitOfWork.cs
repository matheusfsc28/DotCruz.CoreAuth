using CoreAuth.Domain.Interfaces.Data;

namespace CoreAuth.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CoreAuthDbContext _context;

        public UnitOfWork(CoreAuthDbContext context) => _context = context;

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
