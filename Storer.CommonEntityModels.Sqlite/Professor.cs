using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoGens;

[Table("professors")]
public partial class Professor
{
    [Key]
    [Column("professorId", TypeName = "CHAR (10)")]
    [Required]
    [StringLength(10, MinimumLength = 10)]

    public string ProfessorId { get; set; } = null!;

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

    [Column("nip", TypeName = "VARCHAR (50)")]
    [Required]
    [StringLength(50)]
    public string? Nip { get; set; }

    [Column("password", TypeName = "VARCHAR (50)")]
    [Required]
    [MaxLength(50)]
    public string? Password { get; set; }

    [InverseProperty("Professor")]
    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    [InverseProperty("Professor")]
    public virtual ICollection<Teach> Teaches { get; set; } = new List<Teach>();

    
    [InverseProperty("Professor")]
    public virtual ICollection<Petition> Petitions { get; set; } = new List<Petition>();

}
