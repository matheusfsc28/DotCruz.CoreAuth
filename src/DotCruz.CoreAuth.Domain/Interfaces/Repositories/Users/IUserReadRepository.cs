using DotCruz.CoreAuth.Domain.Entities.Users;
using DotCruz.CoreAuth.Domain.Interfaces.Repositories.Base;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DotCruz.CoreAuth.Domain.Interfaces.Repositories.Users;

public interface IUserReadRepository : IBaseReadRepository<User>
{
    Task<bool> ExistsActiveUserWithEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<(IEnumerable<User> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}
