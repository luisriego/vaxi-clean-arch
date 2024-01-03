using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Vehiculos;

public static class VehiculoErrors
{
    public static Error NotFound => new(
        "Vehiculo.Found",
        "Vehiculo com este id não encontrado."
    );

    public static Error InvalidCredentials => new(
        "Vehiculo.InvalidCredentials",
        "Credenciais inválidas."
    );
}