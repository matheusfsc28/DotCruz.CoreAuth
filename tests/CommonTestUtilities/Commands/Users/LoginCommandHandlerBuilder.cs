using CommonTestUtilities.Data;
using CommonTestUtilities.Repositories.Users;
using CommonTestUtilities.Security;
using CoreAuth.Application.Commands.Auth.Login;
using CoreAuth.Common.Settings;
using CoreAuth.Domain.Interfaces.Data;
using CoreAuth.Domain.Interfaces.Repositories.Tokens;
using CoreAuth.Domain.Interfaces.Repositories.Users;
using CoreAuth.Domain.Interfaces.Security;
using CoreAuth.Domain.Interfaces.Security.Tokens;
using Microsoft.Extensions.Options;
using Moq;

namespace CommonTestUtilities.Commands.Users
{
    public class LoginCommandHandlerBuilder
    {
        private IUserReadRepository _userReadRepository = new UserReadRepositoryBuilder().Build();
        private IPasswordHasher _passwordHasher = new PasswordHasherBuilder().Build();
        private IAccessTokenGenerator _accessTokenGenerator = new Mock<IAccessTokenGenerator>().Object;
        private IRefreshTokenGenerator _refreshTokenGenerator = new Mock<IRefreshTokenGenerator>().Object;
        private IRefreshTokenWriteRepository _refreshTokenWriteRepository = new Mock<IRefreshTokenWriteRepository>().Object;
        private IUnitOfWork _unitOfWork = UnitOfWorkBuilder.Build();
        private IOptions<JwtTokenSettings> _jwtTokenSettings = Options.Create(new JwtTokenSettings { RefreshTokenExpirationTimeDays = 7 });

        public LoginCommandHandlerBuilder SetUserReadRepository(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
            return this;
        }

        public LoginCommandHandlerBuilder SetPasswordHasher(IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
            return this;
        }

        public LoginCommandHandlerBuilder SetAccessTokenGenerator(IAccessTokenGenerator accessTokenGenerator)
        {
            _accessTokenGenerator = accessTokenGenerator;
            return this;
        }

        public LoginCommandHandlerBuilder SetRefreshTokenGenerator(IRefreshTokenGenerator refreshTokenGenerator)
        {
            _refreshTokenGenerator = refreshTokenGenerator;
            return this;
        }

        public LoginCommandHandler Build()
        {
            return new LoginCommandHandler(
                _userReadRepository,
                _passwordHasher,
                _accessTokenGenerator,
                _refreshTokenGenerator,
                _refreshTokenWriteRepository,
                _unitOfWork,
                _jwtTokenSettings
            );
        }
    }
}
