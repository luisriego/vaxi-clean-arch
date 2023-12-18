using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Usuarios.Events;

public sealed record UserCreatedDomainEvent(Guid Id) : IDomainEvent;

