using DotCruz.CoreAuth.Domain.Interfaces.Security;
using Moq;

namespace CommonTestUtilities.Security
{
    public class PasswordHasherBuilder
    {
        private readonly Mock<IPasswordHasher> _hasher;

        public PasswordHasherBuilder()
        {
            _hasher = new Mock<IPasswordHasher>();

            _hasher.Setup(h => h.HashPassword(It.IsAny<string>())).Returns((string password) => $"hashed_{password}");
        }

        public PasswordHasherBuilder SetupVerifyPassword(bool match)
        {
            _hasher.Setup(h => h.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(match);

            return this;
        }

        public IPasswordHasher Build() => _hasher.Object;
    }
}
