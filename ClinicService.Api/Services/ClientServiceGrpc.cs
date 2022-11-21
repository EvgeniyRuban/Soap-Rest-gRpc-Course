using ClinicService.Domain.Entities;
using ClinicService.Domain.Exceptions;
using ClinicService.Domain.Repos;
using ClinicServiceProtos;
using Grpc.Core;
using static ClinicServiceProtos.ClientService;

namespace ClinicService.Api.Services;

public class ClientServiceGrpc : ClientServiceBase
{
    private readonly IClientRepository _clientRepository;


    public ClientServiceGrpc(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }


    public async override Task<CreateClientResponse> Add(CreateClientRequest request, ServerCallContext context)
    {
        try
        {
            var client = new Client
            {
                FirstName = request.FirstName,
                Surname = request.Surname,
                Patronymic = request.Patronymic,
                Document = request.Document,
            };

            return new()
            {
                Id = await _clientRepository.Add(client, context.CancellationToken)
            };
        }
        catch (OperationCanceledException)
        {
            return new();
        }
        catch (Exception)
        {
            var exceptionTemplate = new EntityAdditionException();
            return new()
            {
                ErrCode = exceptionTemplate.ErrorCode,
                ErrMessage = exceptionTemplate.Message
            };
        }
    }
    public async override Task<GetClientResponse> Get(GetClientRequest request, ServerCallContext context)
    {
        try
        {
            var client = await _clientRepository.Get(request.Id, context.CancellationToken);
            if (client is null)
            {
                throw new EntityNotFoundException();
            }

            return new()
            {
                Client = new()
                {
                    Id = client.Id,
                    FirstName = client.FirstName,
                    Surname = client.Surname,
                    Patronymic = client.Patronymic,
                    Document = client.Document,
                }
            };
        }
        catch (OperationCanceledException)
        {
            return new();
        }
        catch (EntityNotFoundException ex)
        {
            return new()
            {
                ErrCode = ex.ErrorCode,
                ErrMessage = ex.Message
            };
        }
        catch (Exception)
        {
            var exceptionTemplate = new EntityReceivingException();
            return new()
            {
                ErrCode = exceptionTemplate.ErrorCode,
                ErrMessage = exceptionTemplate.Message
            };
        }
    }
    public async override Task<GetClientsResponse> GetAll(GetClientsRequest request, ServerCallContext context)
    {
        try
        {
            var response = new GetClientsResponse();
            var clients = await _clientRepository.GetAll(context.CancellationToken);
            response.Clients.AddRange(
                clients.Select(c => new ClientResponse
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    Surname = c.Surname,
                    Patronymic = c.Patronymic,
                    Document = c.Document,
                }));

            return response;
        }
        catch (OperationCanceledException)
        {
            return new();
        }
        catch (Exception)
        {
            var exceptionTemplate = new EntityReceivingException();
            return new()
            {
                ErrCode = exceptionTemplate.ErrorCode,
                ErrMessage = exceptionTemplate.Message
            };
        }
    }
    public async override Task<UpdateClientResponse> Update(UpdateClientRequest request, ServerCallContext context)
    {
        try
        {
            var client = new Client
            {
                Id = request.Id,
                FirstName = request.FirstName,
                Surname = request.Surname,
                Patronymic = request.Patronymic,
                Document = request.Document,
            };
            await _clientRepository.Update(client, context.CancellationToken);
            return new();
        }
        catch (OperationCanceledException)
        {
            return new();
        }
        catch (EntityNotFoundException ex)
        {
            return new()
            {
                ErrCode = ex.ErrorCode,
                ErrMessage = ex.Message
            };
        }
        catch (Exception)
        {
            var exceptionTemplate = new EntityUpdatingException();
            return new()
            {
                ErrCode = exceptionTemplate.ErrorCode,
                ErrMessage = exceptionTemplate.Message
            };
        }
    }
    public async override Task<DeleteClientResponse> Delete(DeleteClientRequest request, ServerCallContext context)
    {
        try
        {
            await _clientRepository.Delete(request.Id, context.CancellationToken);
            return new();
        }
        catch (OperationCanceledException)
        {
            return new();
        }
        catch (EntityNotFoundException ex)
        {
            return new()
            {
                ErrCode = ex.ErrorCode,
                ErrMessage = ex.Message
            };
        }
        catch (Exception)
        {
            var exceptionTemplate = new EntityDeletionException();
            return new()
            {
                ErrCode = exceptionTemplate.ErrorCode,
                ErrMessage = exceptionTemplate.Message
            };
        }
    }
}