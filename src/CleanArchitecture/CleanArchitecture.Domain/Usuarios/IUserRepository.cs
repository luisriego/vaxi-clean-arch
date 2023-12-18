namespace   CleanArchitecture.Domain.Usuarios;

public interface IUserRepository
{
    Task<Usuario?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Usuario usuario);
}