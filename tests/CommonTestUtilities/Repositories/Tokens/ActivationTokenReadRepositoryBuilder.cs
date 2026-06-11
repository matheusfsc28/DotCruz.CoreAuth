using DotCruz.CoreAuth.Domain.Entities.Tokens;
using DotCruz.CoreAuth.Domain.Interfaces.Repositories.Tokens;
using Moq;

namespace CommonTestUtilities.Repositories.Tokens;

public class ActivationTokenReadRepositoryBuilder
{
    private readonly Mock<IActivationTokenReadRepository> _repository = new();

    public ActivationTokenReadRepositoryBuilder SetupGetActiveByToken(string tokenHash, ActivationToken? activationToken)
    {
        _repository.Setup(repo => repo.GetActiveByTokenAsync(tokenHash, It.IsAny<CancellationToken>()))
            .ReturnsAsync(activationToken);

        return this;
    }

    public IActivationTokenReadRepository Build() => _repository.Object;
}
