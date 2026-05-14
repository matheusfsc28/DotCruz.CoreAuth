namespace DotCruz.CoreAuth.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendPasswordResetEmailAsync(string email, string name, string token, CancellationToken cancellationToken);
        Task SendWelcomeEmailAsync(string email, string name, CancellationToken cancellationToken);
    }
}
