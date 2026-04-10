using CoreAuth.Domain.Interfaces.Repositories.Users;
using Moq;

namespace CommonTestUtilities.Repositories.Users
{
    public class UserWriteRepositoryBuilder
    {
        public static IUserWriteRepository Build() => new Mock<IUserWriteRepository>().Object;
    }
}
