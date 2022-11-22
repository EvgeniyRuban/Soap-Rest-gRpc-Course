using ClinicService.Domain.Entities;
using ClinicService.Domain.Exceptions;

namespace ClinicService.Domain.Repos;

public interface ICrudRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : IEntity
{
    /// <summary>
    /// Get <typeparamref name="TEntity"/> entity by <paramref name="id"/>.
    /// </summary>
    /// <exception cref="OperationCanceledException"></exception>
    Task<TEntity?> Get(TId id, CancellationToken stoppingToken = default);

    /// <summary>
    /// Get all <typeparamref name="TEntity"/> entities.
    /// </summary>
    /// <exception cref="OperationCanceledException"></exception>
    Task<IReadOnlyCollection<TEntity>> GetAll(CancellationToken stoppingToken = default);

    /// <summary>
    /// Add <typeparamref name="TEntity"/> <paramref name="entity"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="OperationCanceledException"></exception>
    Task<TId> Add(TEntity entity, CancellationToken stoppingToken = default);

    /// <summary>
    /// Update <typeparamref name="TEntity"/> <paramref name="entity"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="EntityNotFoundException"></exception>
    /// <exception cref="OperationCanceledException"></exception>
    Task Update(TEntity entity, CancellationToken stoppingToken = default);

    /// <summary>
    /// Delete <typeparamref name="TEntity"/> entity by <paramref name="id"/>.
    /// </summary>
    /// <exception cref="EntityNotFoundException"></exception>
    /// <exception cref="OperationCanceledException"></exception>
    Task Delete(TId id, CancellationToken stoppingToken = default);
}