using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Alquileres.ObterAlquiler;

public sealed record ObterAlquilerQuery(Guid AlquilerId) : IQuery<AlquilerResponse>;