using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Repos;

public interface IRepository<TEntity, TId> where TEntity : IEntity
{
}