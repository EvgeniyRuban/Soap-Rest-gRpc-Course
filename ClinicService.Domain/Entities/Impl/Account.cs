using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicService.Domain.Entities;

[Table("Accounts")]
public class Account : IEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(255)]
    public string Email { get; set; }

    [StringLength(100)]
    public string PasswordSalt { get; set; }

    [StringLength(100)]
    public string PasswordHash { get; set; }

    public bool Locked { get; set; }

    [StringLength(255)]
    public string FirstName { get; set; }

    [StringLength(255)]
    public string Surname { get; set; }

    [StringLength(255)]
    public string Patronymic { get; set; }


    [InverseProperty(nameof(AccountSession.Account))]
    public virtual ICollection<AccountSession> Sessions { get; set; } = new HashSet<AccountSession>();