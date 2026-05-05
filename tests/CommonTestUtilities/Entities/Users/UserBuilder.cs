using Bogus;
using DotCruz.CoreAuth.Domain.Entities.Users;
using DotCruz.CoreAuth.Domain.Enums.Users;

namespace CommonTestUtilities.Entities.Users
{
    public class UserBuilder
    {
        public static User Build(
            string? name = null, 
            string? email = null, 
            string? passwordHashed = null,
            int nameLength = 10,
            int emailLength = 10
        )
        {
            var userFaker = new Faker<User>()
                .CustomInstantiator(f =>
                    new User(
                        name ?? f.Person.FullName.PadRight(nameLength, 'a'),
                        email ?? f.Internet.Email(f.Person.FirstName).PadRight(emailLength, 'a'),
                        passwordHashed ?? f.Internet.Password(),
                        f.PickRandom<UserType>()
                    )
                );

            return userFaker.Generate();
        }
    }
}
