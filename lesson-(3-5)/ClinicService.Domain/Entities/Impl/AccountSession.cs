﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicService.Domain.Entities;

[Table("AccountSessions")]
public class AccountSession : IEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(384)]
    public string SessionToken { get; set; }

    [ForeignKey(nameof(Account))]
    public int AccountId { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime TimeCreated { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime TimeLastRequest { get; set; }

    public bool IsClosed { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime? TimeClosed { get; set; }

    public virtual Account Account { get; set; }
}