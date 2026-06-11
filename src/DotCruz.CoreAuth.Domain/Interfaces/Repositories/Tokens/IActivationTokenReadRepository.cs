using DotCruz.CoreAuth.Domain.Entities.Tokens;
using DotCruz.CoreAuth.Domain.Interfaces.Repositories.Base;

namespace DotCruz.CoreAuth.Domain.Interfaces.Repositories.Tokens;

public interface IActivationTokenReadRepository : IBaseReadRepository<ActivationToken>
{
    Task<ActivationToken?> GetActiveByTokenAsync(string tokenHash, CancellationToken cancellationToken);
}
