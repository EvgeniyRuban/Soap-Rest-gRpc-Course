namespace ClinicService.Domain.Models;

public class CreateAccountResponse
{
    public int Id { get; set; }
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
}