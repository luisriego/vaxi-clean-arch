namespace CleanArchitecture.Domain.Abstractions;

public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}