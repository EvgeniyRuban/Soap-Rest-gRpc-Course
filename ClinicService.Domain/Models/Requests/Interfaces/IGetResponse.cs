using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Models;

public interface IGetResponse<TEntity, TId> where TEntity : IEntity<TId>
{
}