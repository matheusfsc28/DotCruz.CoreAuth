using Bogus;
using DotCruz.CoreAuth.Domain.Entities.Tokens;

namespace CommonTestUtilities.Entities.Tokens;

public class ActivationTokenBuilder
{
    public static ActivationToken Build(string? token = null, DateTime? expiresAt = null, Guid? userId = null)
    {
        var activationTokenFaker = new Faker<ActivationToken>()
            .CustomInstantiator(f => new ActivationToken(
                    token ?? f.Random.Guid().ToString(),
                    expiresAt ?? DateTime.UtcNow.AddDays(1),
                    userId ?? f.Random.Guid()
                )
            );

        return activationTokenFaker.Generate();
    }
}
