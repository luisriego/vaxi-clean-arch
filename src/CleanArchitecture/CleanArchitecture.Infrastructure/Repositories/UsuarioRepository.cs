using CleanArchitecture.Domain.Usuarios;

namespace CleanArchitecture.Infrastructure.Repositories;

internal sealed class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}