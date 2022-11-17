using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Repos;

public interface ICrudRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : IEntity
{
    Task<TEntity?> Get(TId id, CancellationToken stoppingToken = default);
    Task<IReadOnlyList<TEntity>> GetAll(CancellationToken stoppingToken);
    Task<TId> Add(TEntity entity, CancellationToken stoppingToken = default);
    Task Update(TEntity entity, CancellationToken stoppingToken= default);
    Task Delete(TId id, CancellationToken stoppingToken = default);
}
