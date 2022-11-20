using ClinicService.Domain.Models;

namespace ClinicService.Domain.Services;

public interface IAuthenticationService
{
    Task<AuthenticationResponse> Login(AuthenticationRequest request, CancellationToken stoppingToken = default);
}