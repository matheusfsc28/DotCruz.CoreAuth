using DotCruz.CoreAuth.Domain.Entities.Users;
using DotCruz.CoreAuth.Domain.Interfaces.Repositories.Users;
using Moq;

namespace CommonTestUtilities.Repositories.Users
{
    public class UserWriteRepositoryBuilder
    {
        private readonly Mock<IUserWriteRepository> _repository;

        public UserWriteRepositoryBuilder() => _repository = new Mock<IUserWriteRepository>();

        public UserWriteRepositoryBuilder SetupGetByIdToUpdate(User? user)
        {
            if (user is not null)
            {
                _repository.Setup(repo => repo.GetByIdToUpdate(user.Id, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(user);
            }

            return this;
        }

        public UserWriteRepositoryBuilder SetupGetByIdToUpdate(Guid id, User? user)
        {
            _repository.Setup(repo => repo.GetByIdToUpdate(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            return this;
        }

        public IUserWriteRepository Build() => _repository.Object;

        public static IUserWriteRepository BuildStatic() => new Mock<IUserWriteRepository>().Object;
    }
}
