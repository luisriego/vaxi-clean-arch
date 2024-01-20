namespace CleanArchitecture.Api.Controllers.Alquileres;

public sealed record AlquilerReservaRequest(
    Guid vehiculoId,
    Guid usuarioId,
    DateOnly startDate,
    DateOnly endDate
);