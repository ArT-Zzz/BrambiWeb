using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoGens;

[Table("maintain")]
public partial class Maintain
{
    [Key]
    [Column("maintainId")]
    [Required]
    public int MaintainId { get; set; }

    [Column("maintenanceId")]
    [Required]
    public int? MaintenanceId { get; set; }

    [Column("equipmentId", TypeName = "VARCHAR (15)")]
    [Required]
    [StringLength(15)]
    public string? EquipmentId { get; set; }

    [ForeignKey("EquipmentId")]
    [InverseProperty("Maintains")]
    public virtual Equipment? Equipment { get; set; }

    [ForeignKey("MaintenanceId")]
    [InverseProperty("Maintains")]
    public virtual MaintenanceRegister? Maintenance { get; set; }
}
