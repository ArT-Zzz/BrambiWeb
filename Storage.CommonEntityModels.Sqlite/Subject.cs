using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoGens;

[Table("subjects")]
public partial class Subject
{
    [Key]
    [Column("subjectId", TypeName = "VARCHAR (13)")]
    [Required]
    [StringLength(13)]
    public string SubjectId { get; set; } = null!;

    [Column("name", TypeName = "VARCHAR (55)")]
    [Required]
    [StringLength(55)]
    public string? Name { get; set; }

    [Column("academyId")]
    [Required]
    public int? AcademyId { get; set; }

    [ForeignKey("AcademyId")]
    [InverseProperty("Subjects")]
    public virtual Academy? Academy { get; set; }

    [InverseProperty("Subject")]
    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    
    [InverseProperty("Subject")]
    public virtual ICollection<Petition> Petitions { get; set; } = new List<Petition>();
}
