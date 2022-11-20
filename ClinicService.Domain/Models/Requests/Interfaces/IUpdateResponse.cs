using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Models;

public interface IUpdateResponse<TEntity, TId> : IResponse
    where TEntity : IEntity<TId>
{
}