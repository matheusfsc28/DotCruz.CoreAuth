using DotCruz.CoreAuth.Domain.Entities.Tokens;
using DotCruz.CoreAuth.Domain.Interfaces.Repositories.Tokens;
using Moq;

namespace CommonTestUtilities.Repositories.Tokens;

public class RefreshTokenReadRepositoryBuilder
{
    private readonly Mock<IRefreshTokenReadRepository> _repository = new();

    public RefreshTokenReadRepositoryBuilder SetupGetByToken(string tokenString, RefreshToken? token)
    {
        _repository.Setup(repo => repo.GetByTokenAsync(tokenString, It.IsAny<CancellationToken>()))
            .ReturnsAsync(token);

        return this;
    }

    public RefreshTokenReadRepositoryBuilder SetupGetActiveTokensByUserId(Guid userId, IEnumerable<RefreshToken> activeTokens)
    {
        _repository.Setup(repo => repo.GetActiveTokensByUserIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(activeTokens);

        return this;
    }

    public IRefreshTokenReadRepository Build() => _repository.Object;
}
