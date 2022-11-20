namespace ClinicService.Domain.Models;

public sealed class DeleteClientResponse
{
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
}