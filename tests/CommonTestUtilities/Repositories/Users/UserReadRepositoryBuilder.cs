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

        public IUserReadRepository Build() => _repository.Object;
    }
}
