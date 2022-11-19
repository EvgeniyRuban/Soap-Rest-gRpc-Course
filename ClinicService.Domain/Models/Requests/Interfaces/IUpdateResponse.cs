using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Models;

public interface IUpdateResponse<TEntity, TId> where TEntity : IEntity<TId>
{
}