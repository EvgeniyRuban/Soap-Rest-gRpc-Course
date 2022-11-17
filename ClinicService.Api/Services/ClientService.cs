using ClinicService.DAL;
using ClinicService.Domain.Entities;
using ClinicService.Domain.Exceptions;
using ClinicServiceNamespace;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using static ClinicServiceNamespace.ClientService;

namespace ClinicService.Api.Services;

public class ClientService : ClientServiceBase
{
    private readonly ClinicServiceDbContext _dbContext;


    public ClientService(ClinicServiceDbContext dbContext)
    {
        _dbContext = dbContext;
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

            var entity = await _dbContext.Clients.AddAsync(client);
            await _dbContext.SaveChangesAsync();

            return new()
            {
                Id = entity.Entity.Id
            };
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
            var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == request.Id);

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
            var clients = await _dbContext.Clients.Select(c => new ClientResponse
            {
                Id = c.Id,
                FirstName = c.FirstName,
                Surname = c.Surname,
                Patronymic = c.Patronymic,
                Document = c.Document,
            }).ToListAsync();
            response.Clients.AddRange(clients);

            return response;
        }
        catch (Exception)
        {
            var exceptionTemplate = new ServerSideException();
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
            var client = _dbContext.Clients.FirstOrDefault(c => c.Id == request.Id);

            if (client is null)
            {
                throw new EntityNotFoundException();
            }

            client.FirstName = request.FirstName;
            client.Surname = request.Surname;
            client.Patronymic = request.Patronymic;
            client.Document = request.Document;

            await _dbContext.SaveChangesAsync();

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
    }
    public async override Task<DeleteClientResponse> Delete(DeleteClientRequest request, ServerCallContext context)
    {
        try
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == request.Id);
            if (client is null)
            {
                throw new EntityNotFoundException();
            }

            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();
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