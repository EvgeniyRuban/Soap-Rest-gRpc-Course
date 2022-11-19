using ClinicService.Domain.Entities;
using ClinicService.Domain.Models;

namespace ClinicService.Domain.Services;

public interface ICrudService<TCreateRequest, 
                              TGetResponse, 
                              TUpdateRequest, 
                              TEntity,
                              TId>
    where TEntity : IEntity<TId>
    where TCreateRequest : ICreateRequest<TEntity, TId>
    where TGetResponse : IGetResponse<TEntity, TId>
    where TUpdateRequest : IUpdateRequest<TEntity, TId>
{
    Task<TGetResponse> Get(TId id, CancellationToken stoppingToken = default);
    Task<IReadOnlyCollection<TGetResponse>> GetAll(CancellationToken stoppingToken = default);
    Task<TId> Add(TCreateRequest request, CancellationToken stoppingToken = default);
    Task Update(TUpdateRequest request, CancellationToken stoppingToken = default);
    Task Delete(TId id, CancellationToken stoppingToken = default);
}