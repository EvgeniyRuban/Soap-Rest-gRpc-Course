using AutoMapper;
using AutoMapper.Internal;
using ClinicService.Domain.Entities;
using ClinicService.Domain.Exceptions;
using ClinicService.Domain.Models;
using ClinicService.Domain.Repos;

namespace ClinicService.Domain.Services;

public abstract class CrudServiceBase<TEntity, TId> : ICrudService<ICreateRequest<TEntity, TId>,
                                                                   IGetResponse<TEntity, TId>,
                                                                   IUpdateRequest<TEntity, TId>,
                                                                   TEntity,
                                                                   TId>
    where TEntity : IEntity<TId>
{
    protected readonly ICrudRepository<TEntity, TId> Repository;
    protected readonly IMapper Mapper;


    public CrudServiceBase(ICrudRepository<TEntity, TId> repository, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
        Repository = repository;
        Mapper = mapper;
    }


    public virtual async Task<TId> Add(ICreateRequest<TEntity, TId> request, CancellationToken stoppingToken = default)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        var entity = Mapper.Map<ICreateRequest<TEntity, TId>, TEntity>(request);
        if (entity is null)
        {
            throw new AutoMapperMappingException("Mapping exception", null, new TypePair(typeof(ICreateRequest<TEntity, TId>), typeof(TEntity)));
        }
        var id = await Repository.Add(entity, stoppingToken);
        if (id is null)
        {
            throw new EntityAdditionException();
        }
        return id;
    }

    public virtual async Task Delete(TId id, CancellationToken stoppingToken = default)
        => await Repository.Delete(id, stoppingToken);

    public virtual async Task<IGetResponse<TEntity, TId>> Get(TId id, CancellationToken stoppingToken = default)
    {
        var entity = await Repository.Get(id, stoppingToken);
        if (entity is null)
        {
            throw new EntityNotFoundException();
        }
        var result = Mapper.Map<TEntity, IGetResponse<TEntity, TId>>(entity);
        if (result is null)
        {
            throw new AutoMapperMappingException("Mapping exception", null, new TypePair(typeof(TEntity), typeof(IGetResponse<TEntity, TId>)));
        }
        return result;
    }

    public virtual async Task<IReadOnlyCollection<IGetResponse<TEntity, TId>>> GetAll(CancellationToken stoppingToken = default)
    {
        var result = new List<IGetResponse<TEntity, TId>>();
        var clients = await Repository.GetAll(stoppingToken);
        foreach (var item in clients)
        {
            var client = Mapper.Map<TEntity, IGetResponse<TEntity, TId>>(item);
            if (result is null)
            {
                throw new AutoMapperMappingException("Mapping exception", null, new TypePair(typeof(TEntity), typeof(IGetResponse<TEntity, TId>)));
            }
            result.Add(client);
        }
        return result;
    }

    public virtual async Task Update(IUpdateRequest<TEntity, TId> request, CancellationToken stoppingToken = default)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        var entity = Mapper.Map<IUpdateRequest<TEntity, TId>, TEntity>(request);
        if (entity is null)
        {
            throw new AutoMapperMappingException("Mapping exception", null, new TypePair(typeof(IUpdateRequest<TEntity, TId>), typeof(TEntity)));
        }
        await Repository.Update(entity, stoppingToken);
    }
}