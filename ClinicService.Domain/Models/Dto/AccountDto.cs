namespace ClinicService.Domain.Models;

public sealed class AccountDto
{
    public int Id { get; set; }
    public bool Locked { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set;}
    public string Patronymic { get; set; }
}