using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Models;

public class ClientResponse : IGetResponse<Client, int>
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Patronymic { get; set; } = null!;
    public string Document { get; set; } = null!;
}