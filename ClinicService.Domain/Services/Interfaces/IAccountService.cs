using ClinicService.Domain.Models;

namespace ClinicService.Domain.Services;

public interface IAccountService
{
    Task<int> Add(CreateAccountRequest request, CancellationToken stoppingToken = default);
}