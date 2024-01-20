using CleanArchitecture.Application.Abstractions.Clock;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Usuarios;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler;

internal sealed class ReservarAlquilerCommandHandler : ICommandHandler<ReservarAlquilerCommand, Guid>
{
     private readonly IUsuarioRepository _usuarioRepository;
     private readonly IVehiculoRepository _vehiculoRepository;
     private readonly IAlquilerRepository _alquilerRepository;
     private readonly PrecioService _precioService;
     private readonly IUnitOfWork _unitOfWork;
     private readonly IDateTimeProvider _dateTimeProvider;

    public ReservarAlquilerCommandHandler(
        IUsuarioRepository usuarioRepository, 
        IVehiculoRepository vehiculoRepository, 
        IAlquilerRepository alquilerRepository, 
        PrecioService precioService, 
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _usuarioRepository = usuarioRepository;
        _vehiculoRepository = vehiculoRepository;
        _alquilerRepository = alquilerRepository;
        _precioService = precioService;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>> Handle(
        ReservarAlquilerCommand request, 
        CancellationToken cancellationToken
        )
    {
        var usuario = await _usuarioRepository.GetByIdAsync(request.usuarioId, cancellationToken);

        if (usuario is null)
        {
            return Result.Failure<Guid>(UsuarioErrors.NotFound);
        }

        var vehiculo = await _vehiculoRepository.GetByIdAsync(request.vehiculoId, cancellationToken);

        if (vehiculo is null)
        {
            return Result.Failure<Guid>(VehiculoErrors.NotFound);
        }

        var duracion = DateRange.Create(request.fechaInicio, request.fechaFin);

        if (await _alquilerRepository.IsOverlappingAsync(vehiculo, duracion, cancellationToken))
        {
            return Result.Failure<Guid>(AlquilerErrors.Overlap);
        }

        try{
            var alquiler = Alquiler.Reservar(
                vehiculo, 
                usuario.Id, 
                duracion, 
                _dateTimeProvider.CurrentTime, 
                _precioService
            );

            _alquilerRepository.Add(alquiler);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return alquiler.Id;
        }
        catch(ConcurrencyException)
        {
            return Result.Failure<Guid>(AlquilerErrors.Overlap);
        }

    }
}