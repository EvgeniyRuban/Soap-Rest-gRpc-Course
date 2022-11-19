using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Models;

public interface ICreateResponse<TEntity, TId> where TEntity : IEntity<TId>
{
}