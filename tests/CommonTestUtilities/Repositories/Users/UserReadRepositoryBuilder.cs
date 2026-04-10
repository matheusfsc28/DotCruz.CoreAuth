using CoreAuth.Domain.Entities.Users;
using CoreAuth.Domain.Interfaces.Repositories.Users;
using Moq;

namespace CommonTestUtilities.Repositories.Users
{
    public class UserReadRepositoryBuilder
    {
        private readonly Mock<IUserReadRepository> _repository;

        public UserReadRepositoryBuilder() => _repository = new Mock<IUserReadRepository>();

        public UserReadRepositoryBuilder SetupExistsActiveUserWithEmail(string email, bool exists)
        {
            _repository.Setup(repo => repo.ExistsActiveUserWithEmailAsync(email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(exists);

            return this;
        }

        public UserReadRepositoryBuilder SetupGetUserByEmail(User? user)
        {
            if (user is not null)
            {
                _repository.Setup(repo => repo.GetUserByEmailAsync(It.Is<string>(e => e.ToLower() == user.Email.ToLower()), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(user);
            }

            return this;
        }

        public IUserReadRepository Build() => _repository.Object;
    }
}
