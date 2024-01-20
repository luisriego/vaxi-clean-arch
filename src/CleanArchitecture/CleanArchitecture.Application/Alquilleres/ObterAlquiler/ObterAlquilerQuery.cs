using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Alquileres.ObterAlquiler;

public sealed record ObterAlquilerQuery(DateOnly startDate, Guid AlquilerId) : IQuery<AlquilerResponse>;