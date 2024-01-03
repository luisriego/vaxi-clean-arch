using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Alquileres.Events;
using CleanArchitecture.Domain.Usuarios;
using MediatR;

namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler;

internal sealed class ReservarAlquilerDomainEventHandler : INotificationHandler<AlquilerReservadoDomainEvent>
{
    private readonly IAlquilerRepository _alquileresRepository;
    private readonly IUsuarioRepository _usuariosRepository;
    private readonly IEmailService _emailService;

    public ReservarAlquilerDomainEventHandler(
        IAlquilerRepository alquileresRepository, 
        IUsuarioRepository usuariosRepository, 
        IEmailService emailService
    )
    {
        _alquileresRepository = alquileresRepository;
        _usuariosRepository = usuariosRepository;
        _emailService = emailService;
    }

    public async Task Handle(
        AlquilerReservadoDomainEvent notification, 
        CancellationToken cancellationToken
    )
    {
        var alquiler = await _alquileresRepository.GetByIdAsync(notification.Id, cancellationToken);

        if (alquiler is null)
        {
            return;
        }

        var usuario = await _usuariosRepository.GetByIdAsync(
            alquiler.UserId, 
            cancellationToken);

        if (usuario is null)    
        {
            return;
        }

        await _emailService.SendAsync(
            usuario.Email!, 
            "Alquiler reservado", 
            "Tienes que confirmar esta rerserva de lo contrario se va a perder"
        );
    }
}