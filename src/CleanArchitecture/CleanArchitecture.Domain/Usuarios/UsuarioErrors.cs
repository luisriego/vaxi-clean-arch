using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Usuarios;

public static class UsuarioErrors
{
    public static Error NotFound => new(
        "Usuario.Found",
        "Usuario com este id não encontrado."
    );

    public static Error InvalidCredentials => new(
        "Usuario.InvalidCredentials",
        "Credenciais inválidas."
    );
}