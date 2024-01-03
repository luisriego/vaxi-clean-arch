using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler;

public record ReservarAlquilerCommand(
    Guid vehiculoId,
    Guid usuarioId,
    DateOnly fechaInicio,
    DateOnly fechaFin
) : ICommand<Guid>;