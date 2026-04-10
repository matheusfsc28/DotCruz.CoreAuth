using CommonTestUtilities.Entities.Users;
using CommonTestUtilities.Repositories.Users;
using CommonTestUtilities.Requests.Users;
using CommonTestUtilities.Security;
using CommonTestUtilities.UseCases.Users;
using CoreAuth.Application.Commands.Auth.Login;
using CoreAuth.Domain.Interfaces.Security.Tokens;
using CoreAuth.Exceptions;
using CoreAuth.Exceptions.BaseExceptions;
using Moq;

namespace UseCases.Test.Auth.Login
{
    public class LoginCommandHandlerTest
    {
        [Fact]
        public async Task Success()
        {
            var command = LoginCommandBuilder.Build();
            var user = UserBuilder.Build(email: command.Email);

            var userReadRepository = new UserReadRepositoryBuilder()
                .SetupGetUserByEmail(user)
                .Build();

            var passwordHasher = new PasswordHasherBuilder()
                .SetupVerifyPassword(true)
                .Build();

            var accessTokenGenerator = new Mock<IAccessTokenGenerator>();
            accessTokenGenerator.Setup(a => a.Generate(user.Id)).Returns("access-token");

            var refreshTokenGenerator = new Mock<IRefreshTokenGenerator>();
            refreshTokenGenerator.Setup(r => r.Generate()).Returns("refresh-token");

            var handler = new LoginCommandHandlerBuilder()
                .SetUserReadRepository(userReadRepository)
                .SetPasswordHasher(passwordHasher)
                .SetAccessTokenGenerator(accessTokenGenerator.Object)
                .SetRefreshTokenGenerator(refreshTokenGenerator.Object)
                .Build();

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Name, result.Name);
            Assert.Equal(user.Email, result.Email);
            Assert.Equal("access-token", result.Tokens.AccessToken);
            Assert.Equal("refresh-token", result.Tokens.RefreshToken);
        }

        [Fact]
        public async Task Error_User_Not_Found()
        {
            var command = LoginCommandBuilder.Build();

            var userReadRepository = new UserReadRepositoryBuilder()
                .SetupGetUserByEmail(null)
                .Build();

            var handler = new LoginCommandHandlerBuilder()
                .SetUserReadRepository(userReadRepository)
                .Build();

            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            var exception = await Assert.ThrowsAsync<InvalidLoginException>(act);

            Assert.Contains(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID, exception.GetErrorsMessages());
        }

        [Fact]
        public async Task Error_Invalid_Password()
        {
            var command = LoginCommandBuilder.Build();
            var user = UserBuilder.Build(email: command.Email);

            var userReadRepository = new UserReadRepositoryBuilder()
                .SetupGetUserByEmail(user)
                .Build();

            var passwordHasher = new PasswordHasherBuilder()
                .SetupVerifyPassword(false)
                .Build();

            var handler = new LoginCommandHandlerBuilder()
                .SetUserReadRepository(userReadRepository)
                .SetPasswordHasher(passwordHasher)
                .Build();

            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            var exception = await Assert.ThrowsAsync<InvalidLoginException>(act);

            Assert.Contains(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID, exception.GetErrorsMessages());
        }
    }
}
