using DotCruz.CoreAuth.Domain.Entities.Base;

namespace DotCruz.CoreAuth.Domain.Interfaces.Repositories.Base
{
    public interface IBaseReadRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<bool> HasAnyById(Guid id, CancellationToken cancellationToken);
    }
}
