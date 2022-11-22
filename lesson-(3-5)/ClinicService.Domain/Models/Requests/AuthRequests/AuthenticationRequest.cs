namespace ClinicService.Domain.Models;

public class AuthenticationRequest
{
    public string Login { get; set; }
    public string Password { get; set; }
}