using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoGens;

[Table("students")]
public partial class Student
{
    [Key]
    [Column("studentId", TypeName = "CHAR (8)")]
    [Required]
    [StringLength(8, MinimumLength = 8)]
    public string StudentId { get; set; } = null!;

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
    [MaxLength(50)]
    public string? Password { get; set; }

    [Column("groupId")]
    [Required]
    public int? GroupId { get; set; }


    [InverseProperty("Student")]
    public virtual ICollection<DyLequipment> DyLequipments { get; set; } = new List<DyLequipment>();

    [ForeignKey("GroupId")]
    [InverseProperty("Students")]
    public virtual Group? Group { get; set; }

    [InverseProperty("Student")]
    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
