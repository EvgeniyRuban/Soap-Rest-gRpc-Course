namespace ClinicService.Domain.Models;

public sealed class CreateClientResponse
{
    public int Id { get; set; }
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
}
