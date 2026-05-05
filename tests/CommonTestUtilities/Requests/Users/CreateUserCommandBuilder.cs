using Bogus;
using DotCruz.CoreAuth.Application.Commands.Users.CreateUser;
using DotCruz.CoreAuth.Domain.Enums.Users;

namespace CommonTestUtilities.Requests.Users
{
    public class CreateUserCommandBuilder
    {
        public static CreateUserCommand Build(int passwordLength = 10)
        {
            return new Faker<CreateUserCommand>()
                .CustomInstantiator(f => new CreateUserCommand(
                    f.Person.FullName,
                    f.Internet.Email(f.Person.FirstName),
                    f.Internet.Password(passwordLength),
                    f.PickRandom<UserType>()
                ));
        }
    }
}
