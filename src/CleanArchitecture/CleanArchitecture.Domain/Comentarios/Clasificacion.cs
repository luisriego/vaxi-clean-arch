using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Comentarios;

public sealed record Clasificacion
{
    public static readonly Error Invalid = new("Clasificacion.Invalid", "La clasificacion debe ser un numero entre 1 y 5.");

    public int Value { get; init; }

    private Clasificacion(int value) => Value = value;

    public static Result<Clasificacion> Create(int value)
    {
        if (value < 1 || value > 5)
        {
            return Result.Failure<Clasificacion>(Invalid);
        }

        return new Clasificacion(value);
    }
}