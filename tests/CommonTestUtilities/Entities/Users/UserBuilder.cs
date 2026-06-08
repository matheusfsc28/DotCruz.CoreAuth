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
            int emailLength = 10,
            Guid? tenantId = null
        )
        {
            var userFaker = new Faker<User>()
                .CustomInstantiator(f =>
                {
                    var type = f.PickRandom<UserType>();
                    Guid? resolvedTenantId = tenantId;

                    if ((type == UserType.TenantAdmin || type == UserType.TenantUser) && !resolvedTenantId.HasValue)
                    {
                        resolvedTenantId = Guid.NewGuid();
                    }
                    else if (type == UserType.SuperAdmin || type == UserType.InternalSupport)
                    {
                        resolvedTenantId = null; // Garante nulidade para globais
                    }

                    return new User(
                        name ?? f.Person.FullName.PadRight(nameLength, 'a'),
                        email ?? f.Internet.Email(f.Person.FirstName).PadRight(emailLength, 'a'),
                        passwordHashed ?? f.Internet.Password(),
                        type,
                        resolvedTenantId
                    );
                });

            return userFaker.Generate();
        }
    }
}
