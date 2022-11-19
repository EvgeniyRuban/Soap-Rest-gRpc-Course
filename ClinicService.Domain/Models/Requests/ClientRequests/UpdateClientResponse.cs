namespace ClinicService.Domain.Models;

public class UpdateClientResponse
{
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; } = null!;
}