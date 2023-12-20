using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Comentarios;

public static class ComentarioErrors
{
    public static readonly Error NotElegible = new(
        "Comentario.NotElegible", 
        "El alquiler todavia no puede recibir un comentario."
    );
}