namespace ClinicService.Domain.Models;

public sealed class CreateClientRequest
{
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public string Document { get; set; }
}