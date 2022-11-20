using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Models;

public interface IGetAllResponse<TEntity, TId> : IResponse
    where TEntity : IEntity<TId>, new()
{
    IReadOnlyCollection<IEntityResponse<TEntity, TId>> Entities { get; set; }
}