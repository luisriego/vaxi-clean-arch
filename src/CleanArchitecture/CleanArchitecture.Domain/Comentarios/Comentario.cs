using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Comentarios.Events;

namespace CleanArchitecture.Domain.Comentarios;

public sealed class Comentario : Entity
{
    private Comentario()
    { }

    private Comentario(
        Guid id,
        Guid vehiculoId,
        Guid alquilerId,
        Guid userId,
        Clasificacion clasificacion,
        string comentarioTexto,
        DateTime? fechaCreacion
        
    ) : base(id)
    {
        VehiculoId = vehiculoId;
        AlquilerId = alquilerId;
        UserId = userId;
        Clasificacion = clasificacion;
        ComentarioTexto = comentarioTexto;
        FechaCreacion = fechaCreacion;
    }

    public Guid VehiculoId { get; private set; }

    public Guid AlquilerId { get; private set; }

    public Guid UserId { get; private set; }

    public Clasificacion Clasificacion { get; private set; }

    public string? ComentarioTexto { get; private set; }

    public DateTime? FechaCreacion { get; private set; }

    public static Result<Comentario> Create(
        Alquiler alquiler,
        Clasificacion clasificacion,
        string comentarioTexto,
        DateTime? fechaCreacion
    )
    {
        if (alquiler.Status != AlquilerStatus.Completado)
        {
            return Result.Failure<Comentario>(ComentarioErrors.NotElegible);
        }

        var comentario = new Comentario(
            Guid.NewGuid(),
            alquiler.VehiculoId,
            alquiler.Id,
            alquiler.UserId,
            clasificacion,
            comentarioTexto,
            fechaCreacion
        );

        comentario.RaiseDomainEvent(new ComentarioCreadoDomainEvent(comentario.Id));

        return comentario;
    }
}