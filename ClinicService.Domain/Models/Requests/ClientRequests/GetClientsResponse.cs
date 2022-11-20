namespace ClinicService.Domain.Models;

public sealed class GetClientsResponse
{
    public IReadOnlyCollection<ClientDto> Client { get; set; }
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
}
