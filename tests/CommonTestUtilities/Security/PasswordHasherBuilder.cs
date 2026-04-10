using CoreAuth.Domain.Interfaces.Security;
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

        public IPasswordHasher Build() => _hasher.Object;
    }
}
