using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Models;

public class CreateClientResponse : ICreateResponse<Client, int>, IResponse
{
    public int Id { get; set; }
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; } = null!;
}