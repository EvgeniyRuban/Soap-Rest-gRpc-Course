using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Models;

public interface IUpdateRequest<TEntity, TId> where TEntity : IEntity<TId>
{
}
