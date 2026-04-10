using CoreAuth.Domain.Interfaces.Data;
using Moq;

namespace CommonTestUtilities.Data
{
    public class UnitOfWorkBuilder
    {
        public static IUnitOfWork Build() => new Mock<IUnitOfWork>().Object;
    }
}
