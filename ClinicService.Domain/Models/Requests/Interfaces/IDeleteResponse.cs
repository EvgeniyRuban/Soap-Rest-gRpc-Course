using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Models;

public interface IDeleteResponse<TEntity, TId> : IResponse
    where TEntity : IEntity<TId>
{
}