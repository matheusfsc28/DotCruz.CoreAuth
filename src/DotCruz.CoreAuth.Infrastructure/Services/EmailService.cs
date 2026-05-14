using DotCruz.CoreAuth.Application.Interfaces.Services;
using DotCruz.Notifications.Contracts.Enums.Notifications;
using DotCruz.Notifications.Contracts.Messages.Notifications.CreateNotification;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace DotCruz.CoreAuth.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly Guid _serviceId;

        public EmailService(
            IPublishEndpoint publishEndpoint,
            IConfiguration configuration
        )
        {
            _publishEndpoint = publishEndpoint;
            _serviceId = configuration.GetValue<Guid>("ServiceId");
        }

        public async Task SendPasswordResetEmailAsync(string email, string name, string token, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(new CreateNotificationMessage(
                ServiceId: _serviceId,
                Type: IntegrationNotificationType.Email,
                Recipient: email,
                Culture: "pt-BR",
                TemplateCode: "RequestPasswordResetCommand",
                TemplateData: new Dictionary<string, object> 
                { 
                    { "name", name },
                    { "token", token }
                }
            ), cancellationToken);
        }

        public async Task SendWelcomeEmailAsync(string email, string name, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(new CreateNotificationMessage(
                ServiceId: _serviceId,
                Type: IntegrationNotificationType.Email,
                Recipient: email,
                Culture: "pt-BR",
                TemplateCode: "CreateUserCommand",
                TemplateData: new Dictionary<string, object> 
                { 
                    { "name", name }
                }
            ), cancellationToken);
        }
    }
}
