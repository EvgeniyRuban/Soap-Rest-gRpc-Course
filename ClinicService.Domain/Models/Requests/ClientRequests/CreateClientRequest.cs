using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Models;

public class CreateClientRequest : ICreateRequest<Client, int>
{
    public string FirstName { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Patronymic { get; set; } = null!;
    public string Document { get; set; } = null!;
}
