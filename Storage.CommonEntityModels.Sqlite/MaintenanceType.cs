using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoGens;

[Table("maintenanceTypes")]
public partial class MaintenanceType
{
    [Key]
    [Column("maintenanceTypeId")]
    [Required]
    public byte MaintenanceTypeId { get; set; }

    [Column("name", TypeName = "VARCHAR (10)")]
    [Required]
    [StringLength(10)]
    public string? Name { get; set; }

    [InverseProperty("MaintenanceType")]
    public virtual ICollection<MaintenanceRegister> MaintenanceRegisters { get; set; } = new List<MaintenanceRegister>();

}
