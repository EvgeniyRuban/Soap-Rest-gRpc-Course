using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Models;

public interface IEntityResponse<TEntity, TId> where TEntity : IEntity<TId>
{
}