using DotCruz.CoreAuth.Domain.Entities.Tokens;
using DotCruz.CoreAuth.Domain.Interfaces.Repositories.Tokens;
using Moq;

namespace CommonTestUtilities.Repositories.Tokens
{
    public class PasswordResetTokenReadRepositoryBuilder
    {
        private readonly Mock<IPasswordResetTokenReadRepository> _repository;

        public PasswordResetTokenReadRepositoryBuilder() => _repository = new Mock<IPasswordResetTokenReadRepository>();

        public PasswordResetTokenReadRepositoryBuilder SetupGetByToken(string token, PasswordResetToken? passwordResetToken)
        {
            _repository.Setup(repo => repo.GetByTokenAsync(token, It.IsAny<CancellationToken>()))
                .ReturnsAsync(passwordResetToken);

            return this;
        }

        public IPasswordResetTokenReadRepository Build() => _repository.Object;
    }
}
