using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Models;

public class GetClientsResponse : IGetAllResponse<Client, int>, IResponse
{
    public IReadOnlyCollection<ClientResponse> Clients { get; set; } = null!;
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; } = null!;
    public IReadOnlyCollection<IEntityResponse<Client, int>> Entities { get; set; } = null!;
}