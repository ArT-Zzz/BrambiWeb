using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoGens;

[Table("teaches")]
public partial class Teach
{
    [Key]
    [Column("teachId")]
    [Required]
    public int TeachId { get; set; }

    [Column("classroomId")]
    [Required]
    public int? ClassroomId { get; set; }

    [Column("groupId")]
    [Required]
    public int? GroupId { get; set; }

    [Column("professorId", TypeName = "CHAR (10)")]
    [Required]
    [StringLength(10, MinimumLength = 10)]
    public string? ProfessorId { get; set; }

    [Column("subjectId", TypeName = "VARCHAR (13)")]
    [Required]
    [StringLength(13)]
    public string? SubjectId { get; set; }

    [Column("scheduleId")]
    [Required]
    public short? ScheduleId { get; set; }

    [ForeignKey("ClassroomId")]
    [InverseProperty("Teaches")]
    public virtual Classroom? Classroom { get; set; }

    [ForeignKey("GroupId")]
    [InverseProperty("Teaches")]
    public virtual Group? Group { get; set; }

    [ForeignKey("ProfessorId")]
    [InverseProperty("Teaches")]
    public virtual Professor? Professor { get; set; }

    [ForeignKey("ScheduleId")]
    [InverseProperty("Teaches")]
    public virtual Schedule? Schedule { get; set; }

    [ForeignKey("SubjectId")]
    [InverseProperty("Teaches")]
    public virtual Subject? Subject { get; set; }
}
