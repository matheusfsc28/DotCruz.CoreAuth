using CommonTestUtilities.Commands.Users;
using CommonTestUtilities.Repositories.Users;
using CommonTestUtilities.Requests.Users;
using DotCruz.CoreAuth.Exceptions;
using DotCruz.CoreAuth.Exceptions.BaseExceptions;

namespace Command.Test.Users.CreateUser
{
    public class CreateUserHandlerTest
    {
        [Fact]
        public async Task Success()
        {
            var command = CreateUserCommandBuilder.Build();
            var handler = new CreateUserCommandHandlerBuilder().Build();

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Guid>(result);
            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public async Task Error_Email_Already_Registered()
        {
            var command = CreateUserCommandBuilder.Build();

            var userReadRepository = new UserReadRepositoryBuilder()
                .SetupExistsActiveUserWithEmail(command.Email, true)
                .Build();

            var handler = new CreateUserCommandHandlerBuilder()
                .SetUserReadRepository(userReadRepository)
                .Build();

            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);
            
            Assert.Contains(ResourceMessagesException.EMAIL_ALREADY_EXISTS, exception.GetErrorsMessages());
            Assert.Single(exception.GetErrorsMessages());
        }
    }
}
