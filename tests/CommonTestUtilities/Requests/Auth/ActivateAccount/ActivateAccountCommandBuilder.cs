using Bogus;
using DotCruz.CoreAuth.Application.Commands.Auth.ActivateAccount;

namespace CommonTestUtilities.Requests.Auth.ActivateAccount;

public class ActivateAccountCommandBuilder
{
    public static ActivateAccountCommand Build(int passwordLength = 10)
    {
        return new Faker<ActivateAccountCommand>()
            .CustomInstantiator(f => new ActivateAccountCommand(
                f.Random.Guid().ToString(),
                f.Internet.Password(passwordLength)
            ));
    }
}
