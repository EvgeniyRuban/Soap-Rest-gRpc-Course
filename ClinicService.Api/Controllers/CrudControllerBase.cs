using ClinicService.Domain.Entities;
using ClinicService.Domain.Exceptions;
using ClinicService.Domain.Models;
using ClinicService.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicService.Api.Controllers;

[Route("[controller]")]
[ApiController]
public abstract class CrudControllerBase<TGetResponse,
                                        TGetAllResponse,
                                        TEntityResponse,
                                        TCreateRequest,
                                        TCreateResponse,
                                        TUpdateRequest,
                                        TUpdateResponse,
                                        TDeleteResponse,
                                        TEntity,
                                        TId> : ControllerBase
    where TEntity : IEntity<TId>, new()
    where TEntityResponse : IEntityResponse<TEntity, TId>
    where TGetResponse : IGetResponse<TEntity, TId>, new()
    where TGetAllResponse : IGetAllResponse<TEntity, TId>, new()
    where TCreateRequest : ICreateRequest<TEntity, TId>
    where TCreateResponse : ICreateResponse<TEntity, TId>, new()
    where TUpdateRequest : IUpdateRequest<TEntity, TId>
    where TUpdateResponse : IUpdateResponse<TEntity, TId>, new()
    where TDeleteResponse : IDeleteResponse<TEntity, TId>, new()
{
    protected readonly CrudServiceBase<TEntity, TId> Service;


    protected CrudControllerBase(CrudServiceBase<TEntity, TId> service)
    {
        ArgumentNullException.ThrowIfNull(service, nameof(service));
        Service = service;
    }


    [HttpGet("/{id}")]
    public async virtual Task<ActionResult<TGetResponse>> Get([FromRoute] TId id, CancellationToken stoppingToken = default)
    {
        try
        {
            return Ok(new TGetResponse
            {
                Entity = (TEntityResponse)await Service.Get(id, stoppingToken)
            });
        }
        catch (Exception ex)
        {
            var exceptionTemplate = new ServerSideException();
            return Ok(new TGetResponse
            {
                ErrorCode = exceptionTemplate.ErrorCode,
                ErrorMessage = exceptionTemplate.Message
            });
        }
    }

    [HttpGet]
    public async virtual Task<ActionResult<TGetAllResponse>> GetAll(CancellationToken stoppingToken = default)
    {
        try
        {
            return Ok(new TGetAllResponse
            {
                Entities = await Service.GetAll(stoppingToken)
            });
        }
        catch (Exception ex)
        {
            var exceptionTemplate = new ServerSideException();
            return Ok(new TGetAllResponse
            {
                ErrorCode = exceptionTemplate.ErrorCode,
                ErrorMessage = exceptionTemplate.Message
            });
        }
    }

    [HttpPost]
    public async virtual Task<ActionResult<TCreateResponse>> Add([FromQuery] TCreateRequest request, CancellationToken stoppingToken = default)
    {
        try
        {
            return Ok(new TCreateResponse
            {
                Id = await Service.Add(request)
            });
        }
        catch (Exception ex)
        {
            var exceptionTemplate = new ServerSideException();
            return Ok(new TCreateResponse
            {
                ErrorCode = exceptionTemplate.ErrorCode,
                ErrorMessage = exceptionTemplate.Message
            });
        }
    }

    [HttpPut]
    public async virtual Task<ActionResult<TUpdateResponse>> Update([FromQuery] TUpdateRequest request, CancellationToken stoppingToken = default)
    {
        try
        {
            await Service.Update(request, stoppingToken);
            return Ok(new TUpdateResponse());
        }
        catch (Exception ex)
        {
            var exceptionTemplate = new ServerSideException();
            return Ok(new TUpdateResponse
            {
                ErrorCode = exceptionTemplate.ErrorCode,
                ErrorMessage = exceptionTemplate.Message
            });
        }
    }

    [HttpDelete("/{id}")]
    public async virtual Task<ActionResult<TDeleteResponse>> Delete([FromRoute] TId id, CancellationToken stoppingToken = default)
    {
        try
        {
            await Service.Delete(id, stoppingToken);
            return Ok(new TDeleteResponse());
        }
        catch (Exception ex)
        {
            var exceptionTemplate = new ServerSideException();
            return Ok(new TDeleteResponse
            {
                ErrorCode = exceptionTemplate.ErrorCode,
                ErrorMessage = exceptionTemplate.Message
            });
        }
    }
}