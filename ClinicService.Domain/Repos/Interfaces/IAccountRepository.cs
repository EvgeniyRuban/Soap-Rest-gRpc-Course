using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Repos;

/// <summary>Crud accounts repository.</summary>
/// <inheritdoc/>
public interface IAccountRepository : ICrudRepository<Account, int>
{
    /// <summary>
    /// Get <typeparamref name="TEntity"/> entity by <paramref name="login"/>.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="OperationCanceledException"></exception>
    Task<Account?> Get(string login, CancellationToken stoppingToken = default);
}