using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Models;

public interface IGetResponse<TEntity, TId> : IResponse
    where TEntity : IEntity<TId>, new()
{
    IEntityResponse<TEntity, TId> Entity { get; set; }
}