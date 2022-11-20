using ClinicService.Domain.Models;

namespace ClinicService.Domain.Services;

public interface IClientService
{
    Task<ClientDto> Get(int id, CancellationToken stoppingToken = default);
    Task<IReadOnlyCollection<ClientDto>> GetAll(CancellationToken stoppingToken = default);
    Task<int> Add(CreateClientRequest request, CancellationToken stoppingToken = default);
    Task Update(UpdateClientRequest request, CancellationToken stoppingToken = default);
    Task Delete(int id, CancellationToken stoppingToken = default);
}