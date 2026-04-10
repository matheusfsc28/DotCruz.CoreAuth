using CoreAuth.Application.Commands.Users.CreateUser;
using CoreAuth.Domain.Interfaces.Data;
using CoreAuth.Domain.Interfaces.Repositories.Users;
using CoreAuth.Domain.Interfaces.Security;
using CommonTestUtilities.Data;
using CommonTestUtilities.Repositories.Users;
using CommonTestUtilities.Security;

namespace CommonTestUtilities.Commands.Users
{
    public class CreateUserCommandHandlerBuilder
    {
        private IUserWriteRepository _userWriteRepository = UserWriteRepositoryBuilder.BuildStatic();
        private IUserReadRepository _userReadRepository = new UserReadRepositoryBuilder().Build();
        private IUnitOfWork _unitOfWork = UnitOfWorkBuilder.Build();
        private IPasswordHasher _passwordHasher = new PasswordHasherBuilder().Build();

        public CreateUserCommandHandlerBuilder SetUserReadRepository(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
            return this;
        }

        public CreateUserCommandHandler Build()
        {
            return new CreateUserCommandHandler(
                _userWriteRepository,
                _userReadRepository,
                _unitOfWork,
                _passwordHasher
            );
        }
    }
}
