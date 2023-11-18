using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoGens;

[Table("requests")]
public partial class Request
{
    [Key]
    [Column("requestId")]
    [Required]
    public int RequestId { get; set; }

    [Column("classroomId")]
    [Required]
    public int? ClassroomId { get; set; }

    [Column("professorId", TypeName = "CHAR (10)")]
    [Required]
    [StringLength(10, MinimumLength = 10)]
    public string? ProfessorId { get; set; }

    [Column("studentId", TypeName = "CHAR (8)")]
    [Required]
    [StringLength(8, MinimumLength = 8)]
    public string? StudentId { get; set; }

    [Column("storerId", TypeName = "CHAR (10)")]
    [Required]
    [StringLength(10, MinimumLength = 10)]
    public string? StorerId { get; set; }

    [Column("subjectId", TypeName = "VARCHAR (13)")]
    [Required]
    [MaxLength(13)]
    public string? SubjectId { get; set; }

    [ForeignKey("ClassroomId")]
    [InverseProperty("Requests")]
    public virtual Classroom? Classroom { get; set; }

    [ForeignKey("ProfessorId")]
    [InverseProperty("Requests")]
    public virtual Professor? Professor { get; set; }

    [InverseProperty("Request")]
    public virtual ICollection<RequestDetail> RequestDetails { get; set; } = new List<RequestDetail>();

    [ForeignKey("StorerId")]
    [InverseProperty("Requests")]
    public virtual Storer? Storer { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("Requests")]
    public virtual Student? Student { get; set; }

    [ForeignKey("SubjectId")]
    [InverseProperty("Requests")]
    public virtual Subject? Subject { get; set; }
}
