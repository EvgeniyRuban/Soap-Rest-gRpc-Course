using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Models;

public interface IDeleteResponse<TEntity, TId> where TEntity : IEntity<TId>
{
}