namespace CleanArchitecture.Domain.Abstractions;

public interface IUnitOfWork
{
    Task<bool> CommitAsync(CancellationToken cancellationToken = default);
}