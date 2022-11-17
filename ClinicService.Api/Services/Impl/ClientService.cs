using ClinicService.Api.Exceptions;
using ClinicService.Data;
using ClinicService.Data.Entities;
using ClinicServiceNamespace;
using Grpc.Core;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.CompilerServices;
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

            return new ()
            {
                Id = entity.Entity.Id
            };
        }
        catch
        {
            return new()
            {
                ErrCode = 1,
                ErrMessage = "Client addition error."
            };
        }
    }
    public async override Task<GetClientResponse> Get(GetClientRequest request, ServerCallContext context)
    {
        try
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == request.Id);

            if(client is null)
            {
                throw new EntityNotFoundException();
            }

            return new()
            {
                Client = new()
                {
                    Id = client.Id,

                }
            };
        }
        catch()
        {
            return new()
            {
                ErrCode = 1,
                ErrMessage = "Client getting error."
            };
        }
    }
}