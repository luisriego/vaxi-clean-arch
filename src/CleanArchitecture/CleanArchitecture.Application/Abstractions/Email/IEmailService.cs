namespace CleanArchitecture.Application.Abstractions.Email;

public interface IEmailService
{
    Task SendAsync(Domain.Usuarios.Email recipient, string subject, string body);
}