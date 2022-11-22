namespace ClinicService.Domain.Models;

public sealed class UpdateClientRequest
{
    public int ClientToUpdateId { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public string Document { get; set; }
}
