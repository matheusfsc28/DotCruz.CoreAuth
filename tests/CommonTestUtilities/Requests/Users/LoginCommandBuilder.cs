using Bogus;
using DotCruz.CoreAuth.Application.Commands.Auth.Login;

namespace CommonTestUtilities.Requests.Users
{
    public class LoginCommandBuilder
    {
        public static LoginCommand Build()
        {
            return new Faker<LoginCommand>()
                .CustomInstantiator(f => new LoginCommand(
                    f.Internet.Email(),
                    f.Internet.Password()
                ));
        }
    }
}
