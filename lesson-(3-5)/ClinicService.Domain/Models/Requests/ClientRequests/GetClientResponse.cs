namespace ClinicService.Domain.Models;

public sealed class GetClientResponse
{
    public ClientDto? Client { get; set; }
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
}
