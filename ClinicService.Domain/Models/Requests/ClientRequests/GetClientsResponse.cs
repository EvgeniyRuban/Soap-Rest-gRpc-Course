namespace ClinicService.Domain.Models;

public class GetClientsResponse
{
    public IReadOnlyCollection<ClientResponse> Clients { get; set; } = null!;
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; } = null!;
}