using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Models;

public class DeleteClientResponse : IDeleteResponse<Client, int>, IResponse
{
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; } = null!;
}