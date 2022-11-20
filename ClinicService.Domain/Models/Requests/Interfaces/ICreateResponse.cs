using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Models;

public interface ICreateResponse<TEntity, TId> : IResponse
    where TEntity : IEntity<TId>
{
    TId Id { get; set; }
}