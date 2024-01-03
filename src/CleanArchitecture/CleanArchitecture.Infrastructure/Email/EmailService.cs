using CleanArchitecture.Application.Abstractions.Email;

namespace CleanArchitecture.Infrastructure.Email;

internal sealed class EmailService : IEmailService
{
    public Task SendAsync(Domain.Usuarios.Email recipient, string subject, string body)
    {
        return Task.CompletedTask;
    }
}
