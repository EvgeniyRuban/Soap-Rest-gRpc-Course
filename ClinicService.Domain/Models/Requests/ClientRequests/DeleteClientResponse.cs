namespace ClinicService.Domain.Models;

public class DeleteClientResponse
{
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; } = null!;
}