namespace ClinicService.Domain.Models;

public class GetClientResponse
{
    public ClientResponse Client { get; set; } = null!;
    public int  ErrorCode { get; set; }
    public string ErrorMessage { get; set; } = null!;
}