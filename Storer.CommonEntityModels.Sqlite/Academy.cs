using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoGens;

[Table("academies")]
public partial class Academy
{
    [Key]
    [Column("academyId")]
    [Required]
    public int AcademyId { get; set; }

    [Column("name", TypeName = "VARCHAR (40)")]
    [Required]
    [StringLength(40)]

    public string? Name { get; set; }

    [InverseProperty("Academy")]
    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
