using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoGens;

[Table("storers")]
public partial class Storer
{
    [Key]
    [Column("storerId", TypeName = "CHAR (10)")]
    [Required]
    [StringLength(10, MinimumLength = 10)]
    public string StorerId { get; set; } = null!;

    [Column("name", TypeName = "VARCHAR (30)")]
    [Required]
    [StringLength(30)]
    public string? Name { get; set; }

    [Column("lastNameP", TypeName = "VARCHAR (30)")]
    [Required]
    [StringLength(30)]
    public string? LastNameP { get; set; }

    [Column("lastNameM", TypeName = "VARCHAR (30)")]
    [StringLength(30)]
    public string? LastNameM { get; set; }

    [Column("password", TypeName = "VARCHAR (50)")]
    [Required]
    [MaxLength(30)]
    public string? Password { get; set; }


    [InverseProperty("Storer")]
    public virtual ICollection<MaintenanceRegister> MaintenanceRegisters { get; set; } = new List<MaintenanceRegister>();

    [InverseProperty("Storer")]
    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
    
    [InverseProperty("Storer")]
    public virtual ICollection<Petition> Petitions { get; set; } = new List<Petition>();
}
