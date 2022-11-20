using AutoMapper;
using AutoMapper.Internal;
using ClinicService.Domain.Entities;
using ClinicService.Domain.Exceptions;
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


    public async Task<int> Add(CreateClientRequest request, CancellationToken stoppingToken = default)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        var entity = _mapper.Map<CreateClientRequest, Client>(request);
        if (entity is null)
        {
            throw new AutoMapperMappingException("Mapping exception.", null, new TypePair(typeof(CreateClientRequest), typeof(Client)));
        }
        return await _clientRepository.Add(entity, stoppingToken);
    }
    public async Task Delete(int id, CancellationToken stoppingToken = default)
        => await _clientRepository.Delete(id, stoppingToken);
    public async Task<ClientDto> Get(int id, CancellationToken stoppingToken = default)
    {
        var entity = await _clientRepository.Get(id, stoppingToken);
        if (entity is null)
        {
            throw new EntityNotFoundException();
        }
        var result = _mapper.Map<Client, ClientDto>(entity);
        if (entity is null)
        {
            throw new AutoMapperMappingException("Mapping exception.", null, new TypePair(typeof(Client), typeof(ClientDto)));
        }
        return result;
    }
    public async Task<IReadOnlyCollection<ClientDto>> GetAll(CancellationToken stoppingToken = default)
    {
        var entites = await _clientRepository.GetAll(stoppingToken);
        var result = new List<ClientDto>(entites.Count);
        foreach (var entity in entites)
        {
            var item = _mapper.Map<Client, ClientDto>(entity);
            if (item is null)
            {
                throw new AutoMapperMappingException("Mapping exception.", null, new TypePair(typeof(Client), typeof(ClientDto)));
            }
            result.Add(item);
        }
        return result;
    }
    public async Task Update(UpdateClientRequest request, CancellationToken stoppingToken = default)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        var entity = _mapper.Map<UpdateClientRequest, Client>(request);
        if (entity is null)
        {
            throw new AutoMapperMappingException("Mapping exception.", null, new TypePair(typeof(ClientDto), typeof(Client)));
        }
        await _clientRepository.Update(entity, stoppingToken);
    }
}