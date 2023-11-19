using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoGens;

[Table("divisions")]
public partial class Division
{
    [Key]
    [Column("divisionId")]
    [Required]
    public byte DivisionId { get; set; }

    [Column("name", TypeName = "VARCHAR (40)")]
    [Required]
    [StringLength(30)]
    public string? Name { get; set; }

    [InverseProperty("Division")]
    public virtual ICollection<Classroom> Classrooms { get; set; } = new List<Classroom>();
}
