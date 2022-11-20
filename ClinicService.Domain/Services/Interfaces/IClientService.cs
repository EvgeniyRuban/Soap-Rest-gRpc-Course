using AutoMapper;
using ClinicService.Domain.Exceptions;
using ClinicService.Domain.Models;

namespace ClinicService.Domain.Services;

public interface IClientService
{
    /// <summary></summary>
    /// <exception cref="EntityNotFoundException"></exception>
    /// <exception cref="AutoMapperMappingException"></exception>
    /// <exception cref="OperationCanceledException"></exception>
    Task<ClientDto> Get(int id, CancellationToken stoppingToken = default);

    /// <summary></summary>
    /// <exception cref="AutoMapperMappingException"></exception>
    /// <exception cref="OperationCanceledException"></exception>
    Task<IReadOnlyCollection<ClientDto>> GetAll(CancellationToken stoppingToken = default);

    /// <summary></summary>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="AutoMapperMappingException"></exception>
    /// <exception cref="OperationCanceledException"></exception>
    Task<int> Add(CreateClientRequest request, CancellationToken stoppingToken = default);

    /// <summary></summary>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="EntityNotFoundException"></exception>
    /// <exception cref="AutoMapperMappingException"></exception>
    /// <exception cref="OperationCanceledException"></exception>
    Task Update(UpdateClientRequest request, CancellationToken stoppingToken = default);

    /// <summary></summary>
    /// <exception cref="EntityNotFoundException"></exception>
    /// <exception cref="OperationCanceledException"></exception>
    Task Delete(int id, CancellationToken stoppingToken = default);
}