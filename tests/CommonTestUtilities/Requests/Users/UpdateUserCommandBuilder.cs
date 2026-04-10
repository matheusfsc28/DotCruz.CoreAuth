using Bogus;
using CoreAuth.Application.Commands.Users.UpdateUser;

namespace CommonTestUtilities.Requests.Users
{
    public class UpdateUserCommandBuilder
    {
        public static UpdateUserCommand Build()
        {
            var request = new UpdateUserRequest(
                new Faker().Person.FullName,
                new Faker().Internet.Email()
            );

            return new UpdateUserCommand(Guid.NewGuid(), request);
        }
    }
}
