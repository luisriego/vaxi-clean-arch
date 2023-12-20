using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Comentarios.Events;

public sealed record ComentarioCreadoDomainEvent(Guid AlquilerId) : IDomainEvent;