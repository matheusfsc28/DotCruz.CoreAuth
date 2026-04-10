using CoreAuth.Application.Commands.Users.UpdateUser;
using CoreAuth.Domain.Interfaces.Data;
using CoreAuth.Domain.Interfaces.Repositories.Users;
using CommonTestUtilities.Data;
using CommonTestUtilities.Repositories.Users;

namespace CommonTestUtilities.UseCases.Users
{
    public class UpdateUserCommandHandlerBuilder
    {
        private IUserWriteRepository _userWriteRepository = UserWriteRepositoryBuilder.BuildStatic();
        private IUserReadRepository _userReadRepository = new UserReadRepositoryBuilder().Build();
        private IUnitOfWork _unitOfWork = UnitOfWorkBuilder.Build();

        public UpdateUserCommandHandlerBuilder SetUserWriteRepository(IUserWriteRepository userWriteRepository)
        {
            _userWriteRepository = userWriteRepository;
            return this;
        }

        public UpdateUserCommandHandlerBuilder SetUserReadRepository(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
            return this;
        }

        public UpdateUserCommandHandler Build()
        {
            return new UpdateUserCommandHandler(
                _userWriteRepository,
                _userReadRepository,
                _unitOfWork
            );
        }
    }
}
