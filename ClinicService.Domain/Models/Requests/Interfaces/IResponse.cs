namespace ClinicService.Domain.Models;

public interface IResponse
{
    int ErrorCode { get; set; }
    string ErrorMessage { get; set; }
}