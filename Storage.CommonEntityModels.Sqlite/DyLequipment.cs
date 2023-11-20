using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoGens;

[Table("DyLEquipments")]
public partial class DyLequipment
{
    [Key]
    [Column("DyLEquipmentId")]
    [Required]
    public int DyLequipmentId { get; set; }

    [Column("statusId")]
    [Required]
    public byte? StatusId { get; set; }

    [Column("equipmentId", TypeName = "VARCHAR (15)")]
    [Required]
    [StringLength(15)]
    public string? EquipmentId { get; set; }

    [Column("description", TypeName = "VARCHAR (200)")]
    [Required]
    [StringLength(200)]
    public string? Description { get; set; }

    [Column("dateOfEvent", TypeName = "DATE")]
    [Required]
    public DateTime DateOfEvent { get; set; }

    [Column ("dateOfReturn", TypeName = "DATE")]
    [Required]
    public DateTime? DateOfReturn {get; set;}

    [Column("objectReturn", TypeName = "VARCHAR (100)")]
    [Required]
    [StringLength(100)]
    public string? objectReturn { get; set; }

    [Column("studentId", TypeName = "CHAR (8)")]
    [Required]
    [StringLength(8, MinimumLength = 8)]
    public string? StudentId { get; set; }

    [Column("coordinatorId", TypeName = "CHAR (10)")]
    [Required]
    [StringLength(10, MinimumLength = 10)]
    public string? CoordinatorId { get; set; }

    [ForeignKey("CoordinatorId")]
    [InverseProperty("DyLequipments")]
    public virtual Coordinator? Coordinator { get; set; }

    [ForeignKey("EquipmentId")]
    [InverseProperty("DyLequipments")]
    public virtual Equipment? Equipment { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("DyLequipments")]
    public virtual Status? Status { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("DyLequipments")]
    public virtual Student? Student { get; set; }
}
