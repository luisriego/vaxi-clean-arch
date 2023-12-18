using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Comentarios;

public sealed class Comentario : Entity
{

    public Guid VehiculoId { get; set; }

    public Guid AlquilerId { get; set; }

    public Guid UserId { get; set; }

    
}