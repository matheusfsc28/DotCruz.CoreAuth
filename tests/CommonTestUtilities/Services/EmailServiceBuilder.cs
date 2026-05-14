using DotCruz.CoreAuth.Application.Interfaces.Services;
using Moq;

namespace CommonTestUtilities.Services
{
    public class EmailServiceBuilder
    {
        public static IEmailService Build()
        {
            return new Mock<IEmailService>().Object;
        }
    }
}
