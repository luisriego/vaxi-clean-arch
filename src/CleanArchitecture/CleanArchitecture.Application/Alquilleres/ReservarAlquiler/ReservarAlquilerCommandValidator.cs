using FluentValidation;

namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler;

public class ReservarAlquilerCommandValidator : AbstractValidator<ReservarAlquilerCommand>
{
    public ReservarAlquilerCommandValidator()
    {
        RuleFor(c => c.usuarioId)
            .NotEmpty()
            .WithMessage("El id del usuario no puede estar vacío");
        RuleFor(c => c.vehiculoId)
            .NotEmpty()
            .WithMessage("El id del vehículo no puede estar vacío");
        RuleFor(c => c.fechaInicio).LessThan(c => c.fechaFin)
            .WithMessage("La fecha de inicio debe ser menor a la fecha de fin");
    }
}