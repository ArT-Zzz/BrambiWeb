using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoGens;

[Table("petitions")]
public partial class Petition
{
    [Key]
    [Column("petitionId")]
    [Required]
    public int PetitionId { get; set; }

    [Column("classroomId")]
    [Required]
    public int? ClassroomId { get; set; }

    [Column("professorId", TypeName = "CHAR (10)")]
    [Required]
    [StringLength(10, MinimumLength = 10)]
    public string? ProfessorId { get; set; }

    [Column("storerId", TypeName = "CHAR (10)")]
    [Required]
    [StringLength(10, MinimumLength = 10)]
    public string? StorerId { get; set; }

    [Column("subjectId", TypeName = "VARCHAR (13)")]
    [Required]
    [MaxLength(13)]
    public string? SubjectId { get; set; }

    [ForeignKey("ClassroomId")]
    [InverseProperty("Petitions")]
    public virtual Classroom? Classroom { get; set; }

    [ForeignKey("ProfessorId")]
    [InverseProperty("Petitions")]
    public virtual Professor? Professor { get; set; }

    [InverseProperty("Petition")]
    public virtual ICollection<PetitionDetail> PetitionDetails { get; set; } = new List<PetitionDetail>();

    [ForeignKey("StorerId")]
    [InverseProperty("Petitions")]
    public virtual Storer? Storer { get; set; }

    [ForeignKey("SubjectId")]
    [InverseProperty("Petitions")]
    public virtual Subject? Subject { get; set; }
}
