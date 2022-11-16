using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicService.Data.Entities;

[Table("Pets")]
public class Pet
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey(nameof(Client))]
    public int ClintId { get; set; }

    [Column, StringLength(20)]
    public string Name { get; set; } = null!;

    [Column]
    public DateTime Birthday { get; set; }


    public Client Client { get; set; } = null!;

    [InverseProperty(nameof(Consultation.Pet))]
    public ICollection<Consultation> Consultations { get; set; } = new HashSet<Consultation>();
}
