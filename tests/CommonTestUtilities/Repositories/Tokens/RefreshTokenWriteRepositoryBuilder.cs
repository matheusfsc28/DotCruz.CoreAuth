using DotCruz.CoreAuth.Domain.Entities.Tokens;
using DotCruz.CoreAuth.Domain.Interfaces.Repositories.Tokens;
using Moq;

namespace CommonTestUtilities.Repositories.Tokens;

public class RefreshTokenWriteRepositoryBuilder
{
    private readonly Mock<IRefreshTokenWriteRepository> _repository = new();

    public IRefreshTokenWriteRepository Build() => _repository.Object;
}
