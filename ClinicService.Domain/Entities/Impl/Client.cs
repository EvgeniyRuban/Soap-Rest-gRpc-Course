using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicService.Domain.Entities;

[Table("Clients")]
public class Client : IEntity<int>
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column, StringLength(50)]
    public string Document { get; set; } = null!;

    [Column, StringLength(255)]
    public string FirstName { get; set; } = null!;

    [Column, StringLength(255)]
    public string Surname { get; set; } = null!;

    [Column, StringLength(255)]
    public string? Patronymic { get; set; } = null!;


    [InverseProperty(nameof(Pet.Client))]
    public ICollection<Pet> Pets { get; set; } = new HashSet<Pet>();

    [InverseProperty(nameof(Consultation.Client))]
    public ICollection<Consultation> Consultations { get; set; } = new HashSet<Consultation>();
}