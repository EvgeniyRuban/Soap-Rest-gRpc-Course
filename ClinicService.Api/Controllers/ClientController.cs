using ClinicService.Domain.Exceptions;
using ClinicService.Domain.Models;
using ClinicService.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;


    public ClientController(IClientService clientService)
    {
        ArgumentNullException.ThrowIfNull(clientService, nameof(clientService));
        _clientService = clientService;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<GetClientResponse>> Get([FromRoute] int id, CancellationToken stoppingToken = default)
    {
        try
        {
            return Ok(new GetClientResponse
            {
                Client = await _clientService.Get(id, stoppingToken)
            });
        }
        catch (Exception ex)
        {
            var exceptionTemplate = new ServerSideException();
            return Ok(new GetClientResponse
            {
                ErrorCode = exceptionTemplate.ErrorCode,
                ErrorMessage = exceptionTemplate.Message
            });
        }
    }

    [HttpGet]
    public async Task<ActionResult<GetClientsResponse>> GetAll(CancellationToken stoppingToken = default)
    {
        try
        {
            return Ok(new GetClientsResponse
            {
                Clients = await _clientService.GetAll(stoppingToken)
            });
        }
        catch (Exception ex)
        {
            var exceptionTemplate = new ServerSideException();
            return Ok(new GetClientsResponse
            {
                ErrorCode = exceptionTemplate.ErrorCode,
                ErrorMessage = exceptionTemplate.Message
            });
        }
    }

    [HttpPost]
    public async Task<ActionResult<CreateClientResponse>> Add([FromQuery]CreateClientRequest request, CancellationToken stoppingToken = default)
    {
        try
        {
            return Ok(new CreateClientResponse
            {
                Id = await _clientService.Add(request, stoppingToken)
            });
        }
        catch (Exception ex)
        {
            var exceptionTemplate = new ServerSideException();
            return Ok(new CreateClientResponse
            {
                ErrorCode = exceptionTemplate.ErrorCode,
                ErrorMessage = exceptionTemplate.Message
            });
        }
    }

    [HttpPut]
    public async Task<ActionResult<UpdateClientResponse>> Update([FromQuery] UpdateClientRequest request, CancellationToken stoppingToken = default)
    {
        try
        {
            await _clientService.Update(request, stoppingToken);
            return Ok(new UpdateClientResponse());
        }
        catch (Exception ex)
        {
            var exceptionTemplate = new ServerSideException();
            return Ok(new UpdateClientResponse
            {
                ErrorCode = exceptionTemplate.ErrorCode,
                ErrorMessage = exceptionTemplate.Message
            });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeleteClientResponse>> Delete([FromRoute] int id, CancellationToken stoppingToken = default)
    {
        try
        {
            await _clientService.Delete(id, stoppingToken);
            return Ok(new DeleteClientResponse());
        }
        catch (Exception ex)
        {
            var exceptionTemplate = new ServerSideException();
            return Ok(new DeleteClientResponse
            {
                ErrorCode = exceptionTemplate.ErrorCode,
                ErrorMessage = exceptionTemplate.Message
            });
        }
    }
}
