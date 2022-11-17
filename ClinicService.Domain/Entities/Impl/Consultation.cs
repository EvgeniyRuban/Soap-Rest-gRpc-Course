using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicService.Domain.Entities;

[Table("Consultations")]
public class Consultation : IEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [ForeignKey(nameof(Client))]
    public int ClientId { get; set; }
    [ForeignKey(nameof(Pet))]
    public int PetId { get; set; }
    [Column]
    public string? Description { get; set; } = null!;
    public DateTime Date { get; set; }

    public Client Client { get; set; } = null!;
    public Pet Pet { get; set; } = null!;
}