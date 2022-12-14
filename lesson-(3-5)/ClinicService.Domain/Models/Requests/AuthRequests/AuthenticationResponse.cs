using ClinicService.Domain.Constants;

namespace ClinicService.Domain.Models;

public class AuthenticationResponse
{
    public AuthenticationStatus Status { get; set; }
    public SessionContext SessionContext { get; set; }
}