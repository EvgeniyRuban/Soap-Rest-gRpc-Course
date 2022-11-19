using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Models;

public interface ICreateRequest<TEntity, TId> where TEntity : IEntity<TId>
{
}