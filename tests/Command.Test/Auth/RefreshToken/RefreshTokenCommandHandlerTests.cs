using CommonTestUtilities.Commands.Auth;
using CommonTestUtilities.Entities.Tokens;
using CommonTestUtilities.Entities.Users;
using CommonTestUtilities.Repositories.Tokens;
using CommonTestUtilities.Requests.Auth;
using DotCruz.CoreAuth.Domain.Enums.Users;
using DotCruz.CoreAuth.Domain.Exceptions.BaseExceptions;
using DotCruz.CoreAuth.Domain.Exceptions.Resources;
using DotCruz.CoreAuth.Domain.Interfaces.Security.Tokens;
using FluentAssertions;
using Moq;
using RefreshTokenEntity = DotCruz.CoreAuth.Domain.Entities.Tokens.RefreshToken;

namespace Command.Test.Auth.RefreshToken;

public class RefreshTokenCommandHandlerTests
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build(status: UserStatus.Active);
        var command = RefreshTokenCommandBuilder.Build();
        var token = RefreshTokenBuilder.Build(token: command.RefreshToken, userId: user.Id);
        typeof(RefreshTokenEntity).GetProperty(nameof(RefreshTokenEntity.User))?.SetValue(token, user);

        var readRepository = new RefreshTokenReadRepositoryBuilder()
            .SetupGetByToken(command.RefreshToken, token)
            .Build();

        var writeRepository = new RefreshTokenWriteRepositoryBuilder().Build();

        var accessTokenGenerator = new Mock<IAccessTokenGenerator>();
        accessTokenGenerator.Setup(a => a.Generate(user)).Returns("new-access-token");

        var refreshTokenGenerator = new Mock<IRefreshTokenGenerator>();
        refreshTokenGenerator.Setup(r => r.Generate()).Returns("new-refresh-token");

        var handler = new RefreshTokenCommandHandlerBuilder()
            .SetRefreshTokenReadRepository(readRepository)
            .SetRefreshTokenWriteRepository(writeRepository)
            .SetAccessTokenGenerator(accessTokenGenerator.Object)
            .SetRefreshTokenGenerator(refreshTokenGenerator.Object)
            .Build();

        var result = await handler.Handle(command, TestContext.Current.CancellationToken);

        result.Should().NotBeNull();
        result.AccessToken.Should().Be("new-access-token");
        result.RefreshToken.Should().Be("new-refresh-token");
    }

    [Fact]
    public async Task Error_Token_NotFound()
    {
        var command = RefreshTokenCommandBuilder.Build();

        var readRepository = new RefreshTokenReadRepositoryBuilder()
            .SetupGetByToken(command.RefreshToken, null)
            .Build();

        var handler = new RefreshTokenCommandHandlerBuilder()
            .SetRefreshTokenReadRepository(readRepository)
            .Build();

        var act = () => handler.Handle(command, TestContext.Current.CancellationToken);

        var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);
        exception.GetErrorsMessages().Should().Contain(ResourceMessagesException.TOKEN_INVALID);
    }

    [Fact]
    public async Task Error_Token_Expired()
    {
        var user = UserBuilder.Build(status: UserStatus.Active);
        var command = RefreshTokenCommandBuilder.Build();
        
        // Token expirado (ExpiresAt no passado, mutado via reflexao para passar na validacao do construtor)
        var token = RefreshTokenBuilder.Build(
            token: command.RefreshToken, 
            userId: user.Id
        );
        typeof(RefreshTokenEntity).GetProperty(nameof(RefreshTokenEntity.ExpiresAt))?.SetValue(token, DateTimeOffset.UtcNow.AddMinutes(-5));
        typeof(RefreshTokenEntity).GetProperty(nameof(RefreshTokenEntity.User))?.SetValue(token, user);

        var readRepository = new RefreshTokenReadRepositoryBuilder()
            .SetupGetByToken(command.RefreshToken, token)
            .Build();

        var handler = new RefreshTokenCommandHandlerBuilder()
            .SetRefreshTokenReadRepository(readRepository)
            .Build();

        var act = () => handler.Handle(command, TestContext.Current.CancellationToken);

        var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);
        exception.GetErrorsMessages().Should().Contain(ResourceMessagesException.TOKEN_INVALID);
    }

    [Fact]
    public async Task Error_Token_Revoked()
    {
        var user = UserBuilder.Build(status: UserStatus.Active);
        var command = RefreshTokenCommandBuilder.Build();
        var token = RefreshTokenBuilder.Build(token: command.RefreshToken, userId: user.Id);
        typeof(RefreshTokenEntity).GetProperty(nameof(RefreshTokenEntity.User))?.SetValue(token, user);
        
        // Revoga o token
        token.Revoke();

        var readRepository = new RefreshTokenReadRepositoryBuilder()
            .SetupGetByToken(command.RefreshToken, token)
            .Build();

        var handler = new RefreshTokenCommandHandlerBuilder()
            .SetRefreshTokenReadRepository(readRepository)
            .Build();

        var act = () => handler.Handle(command, TestContext.Current.CancellationToken);

        var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);
        exception.GetErrorsMessages().Should().Contain(ResourceMessagesException.TOKEN_INVALID);
    }

    [Fact]
    public async Task Error_User_NotFound()
    {
        var command = RefreshTokenCommandBuilder.Build();
        var token = RefreshTokenBuilder.Build(token: command.RefreshToken);
        
        // User é nulo
        typeof(RefreshTokenEntity).GetProperty(nameof(RefreshTokenEntity.User))?.SetValue(token, null);

        var readRepository = new RefreshTokenReadRepositoryBuilder()
            .SetupGetByToken(command.RefreshToken, token)
            .Build();

        var handler = new RefreshTokenCommandHandlerBuilder()
            .SetRefreshTokenReadRepository(readRepository)
            .Build();

        var act = () => handler.Handle(command, TestContext.Current.CancellationToken);

        var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);
        exception.GetErrorsMessages().Should().Contain(ResourceMessagesException.USER_NOT_FOUND);
    }

    [Fact]
    public async Task Error_User_Inactive()
    {
        var user = UserBuilder.Build(status: UserStatus.Blocked); // Inativo/Bloqueado
        var command = RefreshTokenCommandBuilder.Build();
        var token = RefreshTokenBuilder.Build(token: command.RefreshToken, userId: user.Id);
        typeof(RefreshTokenEntity).GetProperty(nameof(RefreshTokenEntity.User))?.SetValue(token, user);

        var readRepository = new RefreshTokenReadRepositoryBuilder()
            .SetupGetByToken(command.RefreshToken, token)
            .Build();

        var handler = new RefreshTokenCommandHandlerBuilder()
            .SetRefreshTokenReadRepository(readRepository)
            .Build();

        var act = () => handler.Handle(command, TestContext.Current.CancellationToken);

        var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);
        exception.GetErrorsMessages().Should().Contain(ResourceMessagesException.USER_NOT_FOUND);
    }
}
