using CleanArchitecture.Application.Abstractions.Behaviors;

namespace CleanArchitecture.Application.Exceptions;

public sealed class ValidationException : Exception
{
    public ValidationException(IEnumerable<ValidationError> errors)
        : base("Se han producido uno o más errores de validación")
    {
        Errors = errors;
    }

    public IEnumerable<ValidationError> Errors { get; }
}