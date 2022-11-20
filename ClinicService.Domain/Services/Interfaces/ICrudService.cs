using ClinicService.Domain.Entities;
using ClinicService.Domain.Models;

namespace ClinicService.Domain.Services;

public interface ICrudService<TCreateRequest, 
                              TEntityResponse, 
                              TUpdateRequest, 
                              TEntity,
                              TId>
    where TEntity : IEntity<TId>
    where TCreateRequest : ICreateRequest<TEntity, TId>
    where TEntityResponse : IEntityResponse<TEntity, TId>
    where TUpdateRequest : IUpdateRequest<TEntity, TId>
{
    Task<TEntityResponse> Get(TId id, CancellationToken stoppingToken = default);
    Task<IReadOnlyCollection<TEntityResponse>> GetAll(CancellationToken stoppingToken = default);
    Task<TId> Add(TCreateRequest request, CancellationToken stoppingToken = default);
    Task Update(TUpdateRequest request, CancellationToken stoppingToken = default);
    Task Delete(TId id, CancellationToken stoppingToken = default);
}