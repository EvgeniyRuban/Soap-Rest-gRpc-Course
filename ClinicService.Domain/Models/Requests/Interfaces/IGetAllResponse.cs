using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Models;

public interface IGetAllResponse<TEntity, TId> where TEntity : IEntity<TId>
{
}