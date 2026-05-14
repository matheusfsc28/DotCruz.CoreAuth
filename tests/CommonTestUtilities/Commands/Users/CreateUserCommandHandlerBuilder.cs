using DotCruz.CoreAuth.Application.Commands.Users.CreateUser;
using DotCruz.CoreAuth.Domain.Interfaces.Data;
using DotCruz.CoreAuth.Domain.Interfaces.Repositories.Users;
using DotCruz.CoreAuth.Domain.Interfaces.Security;
using DotCruz.CoreAuth.Application.Interfaces.Services;
using CommonTestUtilities.Data;
using CommonTestUtilities.Repositories.Users;
using CommonTestUtilities.Security;
using CommonTestUtilities.Services;

namespace CommonTestUtilities.Commands.Users
{
    public class CreateUserCommandHandlerBuilder
    {
        private IUserWriteRepository _userWriteRepository = UserWriteRepositoryBuilder.BuildStatic();
        private IUserReadRepository _userReadRepository = new UserReadRepositoryBuilder().Build();
        private IUnitOfWork _unitOfWork = UnitOfWorkBuilder.Build();
        private IPasswordHasher _passwordHasher = new PasswordHasherBuilder().Build();
        private IEmailService _emailService = EmailServiceBuilder.Build();

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
                _passwordHasher,
                _emailService
            );
        }
    }
}
