using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Models;

public class GetClientResponse : IGetResponse<Client, int>, IResponse
{
    public int  ErrorCode { get; set; }
    public string ErrorMessage { get; set; } = null!;
    public IEntityResponse<Client, int> Entity { get; set; } = null!;
}