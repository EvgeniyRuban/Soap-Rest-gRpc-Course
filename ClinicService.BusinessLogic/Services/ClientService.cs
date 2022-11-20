using AutoMapper;
using ClinicService.Domain.Models;
using ClinicService.Domain.Repos;
using ClinicService.Domain.Services;

namespace ClinicService.BusinessLogic.Services;

public sealed class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;


    public ClientService(IClientRepository clientRepository, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(clientRepository, nameof(clientRepository));
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
        _clientRepository = clientRepository;
        _mapper = mapper;
    }


    public Task<int> Add(CreateClientRequest request, CancellationToken stoppingToken = default)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id, CancellationToken stoppingToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<ClientDto> Get(int id, CancellationToken stoppingToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<ClientDto>> GetAll(CancellationToken stoppingToken = default)
    {
        throw new NotImplementedException();
    }

    public Task Update(UpdateClientRequest request, CancellationToken stoppingToken = default)
    {
        throw new NotImplementedException();
    }
}
