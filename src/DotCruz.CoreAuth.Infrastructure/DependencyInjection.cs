using DotCruz.CoreAuth.Domain.Interfaces.Data;
using DotCruz.CoreAuth.Domain.Interfaces.Repositories.Base;
using DotCruz.CoreAuth.Domain.Interfaces.Repositories.Tokens;
using DotCruz.CoreAuth.Domain.Interfaces.Repositories.Users;
using DotCruz.CoreAuth.Domain.Interfaces.Security;
using DotCruz.CoreAuth.Infrastructure.Data;
using DotCruz.CoreAuth.Infrastructure.Repositories.Base;
using DotCruz.CoreAuth.Infrastructure.Repositories.Tokens;
using DotCruz.CoreAuth.Infrastructure.Repositories.Users;
using DotCruz.CoreAuth.Infrastructure.Security.Password;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotCruz.CoreAuth.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositories(services);
            AddUnitOfWork(services);
            AddSecurity(services);

            AddDbContext(services, configuration);

            return services;
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseReadRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IBaseWriteRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IPasswordResetTokenReadRepository, PasswordResetTokenRepository>();
            services.AddScoped<IPasswordResetTokenWriteRepository, PasswordResetTokenRepository>();

            services.AddScoped<IRefreshTokenReadRepository, RefreshTokenRepository>();
            services.AddScoped<IRefreshTokenWriteRepository, RefreshTokenRepository>();

            services.AddScoped<IUserReadRepository, UserRepository>();
            services.AddScoped<IUserWriteRepository, UserRepository>();
        }

        private static void AddUnitOfWork(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void AddSecurity(IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher, BCryptHasher>();
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgresqlConnection");

            services.AddDbContext<CoreAuthDbContext>(opt =>
            {
                opt.UseNpgsql(connectionString);
                opt.UseSnakeCaseNamingConvention();
            });
        }
    }
}