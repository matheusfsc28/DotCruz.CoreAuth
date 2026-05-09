using CommonTestUtilities.Commands.Users;
using CommonTestUtilities.Entities.Users;
using CommonTestUtilities.Repositories.Users;
using CommonTestUtilities.Requests.Users;
using DotCruz.CoreAuth.Domain.Exceptions.BaseExceptions;
using DotCruz.CoreAuth.Domain.Exceptions.Resources;

namespace Command.Test.Users.UpdateUser
{
    public class UpdateUserHandlerTest
    {
        [Fact]
        public async Task Success()
        {
            var user = UserBuilder.Build();
            var command = UpdateUserCommandBuilder.Build();
            command = command with { Id = user.Id };

            var userWriteRepository = new UserWriteRepositoryBuilder()
                .SetupGetByIdToUpdate(user)
                .Build();

            var handler = new UpdateUserCommandHandlerBuilder()
                .SetUserWriteRepository(userWriteRepository)
                .Build();

            await handler.Handle(command, TestContext.Current.CancellationToken);

            Assert.Equal(command.Request.Name, user.Name);
            Assert.Equal(command.Request.Email.ToLowerInvariant(), user.Email);
        }

        [Fact]
        public async Task Error_User_NotFound()
        {
            var command = UpdateUserCommandBuilder.Build();

            var userWriteRepository = new UserWriteRepositoryBuilder()
                .SetupGetByIdToUpdate(command.Id, null)
                .Build();

            var handler = new UpdateUserCommandHandlerBuilder()
                .SetUserWriteRepository(userWriteRepository)
                .Build();

            Task act() => handler.Handle(command, TestContext.Current.CancellationToken);

            var exception = await Assert.ThrowsAsync<NotFoundException>(act);

            Assert.Equal(ResourceMessagesException.USER_NOT_FOUND, exception.Message);
        }

        [Fact]
        public async Task Error_Email_Already_Exists()
        {
            var user = UserBuilder.Build();
            var command = UpdateUserCommandBuilder.Build();
            command = command with { Id = user.Id };

            var userWriteRepository = new UserWriteRepositoryBuilder()
                .SetupGetByIdToUpdate(user)
                .Build();

            var userReadRepository = new UserReadRepositoryBuilder()
                .SetupExistsActiveUserWithEmail(command.Request.Email, true)
                .Build();

            var handler = new UpdateUserCommandHandlerBuilder()
                .SetUserWriteRepository(userWriteRepository)
                .SetUserReadRepository(userReadRepository)
                .Build();

            Task act() => handler.Handle(command, TestContext.Current.CancellationToken);

            var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);

            Assert.Contains(ResourceMessagesException.EMAIL_ALREADY_EXISTS, exception.GetErrorsMessages());
        }
    }
}
