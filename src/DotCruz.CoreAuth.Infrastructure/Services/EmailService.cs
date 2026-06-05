using DotCruz.CoreAuth.Application.Interfaces.Services;
using DotCruz.Notifications.Contracts.Enums.Notifications;
using DotCruz.Notifications.Contracts.Messages.Notifications.CreateNotification;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Net.Http.Json;

namespace DotCruz.CoreAuth.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _httpClient;
        private readonly Guid _serviceId;

        public EmailService(
            HttpClient httpClient,
            IConfiguration configuration
        )
        {
            _httpClient = httpClient;
            _serviceId = configuration.GetValue<Guid>("ServiceId");
        }

        public async Task SendPasswordResetEmailAsync(string email, string name, string token, CancellationToken cancellationToken)
        {
            var message = new CreateNotificationMessage(
                ServiceId: _serviceId,
                Type: IntegrationNotificationType.Email,
                Recipient: email,
                Culture: CultureInfo.CurrentUICulture.Name,
                TemplateCode: "RequestPasswordResetCommand",
                TemplateData: new Dictionary<string, object> 
                { 
                    { "name", name },
                    { "token", token }
                }
            );

            var response = await _httpClient.PostAsJsonAsync("api/Notification", message, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        public async Task SendWelcomeEmailAsync(string email, string name, CancellationToken cancellationToken)
        {
            var message = new CreateNotificationMessage(
                ServiceId: _serviceId,
                Type: IntegrationNotificationType.Email,
                Recipient: email,
                Culture: CultureInfo.CurrentUICulture.Name,
                TemplateCode: "CreateUserCommand",
                TemplateData: new Dictionary<string, object> 
                { 
                    { "name", name }
                }
            );

            var response = await _httpClient.PostAsJsonAsync("api/Notification", message, cancellationToken);
            response.EnsureSuccessStatusCode();
        }
    }
}
