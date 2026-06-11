using CommonTestUtilities.Commands.Auth;
using CommonTestUtilities.Entities.Tokens;
using CommonTestUtilities.Repositories.Tokens;
using CommonTestUtilities.Requests.Auth;
using FluentAssertions;
using RefreshTokenEntity = DotCruz.CoreAuth.Domain.Entities.Tokens.RefreshToken;

namespace Command.Test.Auth.RevokeAllUserTokens;

public class RevokeAllUserTokensCommandHandlerTests
{
    [Fact]
    public async Task Success()
    {
        var command = RevokeAllUserTokensCommandBuilder.Build();
        var token1 = RefreshTokenBuilder.Build(userId: command.UserId);
        var token2 = RefreshTokenBuilder.Build(userId: command.UserId);
        var activeTokens = new List<RefreshTokenEntity> { token1, token2 };

        var readRepository = new RefreshTokenReadRepositoryBuilder()
            .SetupGetActiveTokensByUserId(command.UserId, activeTokens)
            .Build();

        var handler = new RevokeAllUserTokensCommandHandlerBuilder()
            .SetRefreshTokenReadRepository(readRepository)
            .Build();

        await handler.Handle(command, TestContext.Current.CancellationToken);

        token1.IsActive.Should().BeFalse();
        token1.RevokedAt.Should().NotBeNull();
        
        token2.IsActive.Should().BeFalse();
        token2.RevokedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task Success_NoActiveTokens()
    {
        var command = RevokeAllUserTokensCommandBuilder.Build();
        var activeTokens = new List<RefreshTokenEntity>(); // Vazia

        var readRepository = new RefreshTokenReadRepositoryBuilder()
            .SetupGetActiveTokensByUserId(command.UserId, activeTokens)
            .Build();

        var handler = new RevokeAllUserTokensCommandHandlerBuilder()
            .SetRefreshTokenReadRepository(readRepository)
            .Build();

        var act = () => handler.Handle(command, TestContext.Current.CancellationToken);

        await act.Should().NotThrowAsync();
    }
}
