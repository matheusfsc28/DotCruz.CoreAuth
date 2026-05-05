using CommonTestUtilities.Commands.Users;
using CommonTestUtilities.Entities.Tokens;
using CommonTestUtilities.Entities.Users;
using CommonTestUtilities.Repositories.Tokens;
using CommonTestUtilities.Requests.Users;
using DotCruz.CoreAuth.Domain.Interfaces.Security.Tokens;
using DotCruz.CoreAuth.Exceptions;
using DotCruz.CoreAuth.Exceptions.BaseExceptions;
using Moq;

namespace Command.Test.Auth.PasswordResetTokens.ResetPassword
{
    public class ResetPasswordCommandHandlerTest
    {
        [Fact]
        public async Task Success()
        {
            var command = ResetPasswordCommandBuilder.Build();
            var user = UserBuilder.Build();
            var token = PasswordResetTokenBuilder.Build(token: "hashed-token");
            
            // Link user to token via reflection if necessary, but here we can just ensure it exists
            typeof(DotCruz.CoreAuth.Domain.Entities.Tokens.PasswordResetToken)
                .GetProperty("User")?.SetValue(token, user);

            var tokenReadRepository = new PasswordResetTokenReadRepositoryBuilder()
                .SetupGetByToken("hashed-token", token)
                .Build();

            var tokenProvider = new Mock<ITokenProvider>();
            tokenProvider.Setup(t => t.Hash(command.Token)).Returns("hashed-token");

            var handler = new ResetPasswordCommandHandlerBuilder()
                .SetTokenReadRepository(tokenReadRepository)
                .SetTokenProvider(tokenProvider.Object)
                .Build();

            await handler.Handle(command, TestContext.Current.CancellationToken);

            Assert.True(token.IsUsed);
        }

        [Fact]
        public async Task Error_Token_Invalid()
        {
            var command = ResetPasswordCommandBuilder.Build();

            var tokenReadRepository = new PasswordResetTokenReadRepositoryBuilder()
                .SetupGetByToken(It.IsAny<string>(), null)
                .Build();

            var tokenProvider = new Mock<ITokenProvider>();
            tokenProvider.Setup(t => t.Hash(It.IsAny<string>())).Returns("hashed-token");

            var handler = new ResetPasswordCommandHandlerBuilder()
                .SetTokenReadRepository(tokenReadRepository)
                .SetTokenProvider(tokenProvider.Object)
                .Build();

            Task act() => handler.Handle(command, TestContext.Current.CancellationToken);

            var exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal(ResourceMessagesException.TOKEN_INVALID, exception.Message);
        }
    }
}
