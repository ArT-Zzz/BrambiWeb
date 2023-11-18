using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoGens;

[Table("petitionDetails")]
public partial class PetitionDetail
{
    [Key]
    [Column("petitionDetailsId")]
    [Required]
    public int PetitionDetailsId { get; set; }

    [Column("petitionId")]
    [Required]
    public int PetitionId { get; set; }

    [Column("equipmentId", TypeName = "VARCHAR (15)")]
    [Required]
    [StringLength(15)]
    public string? EquipmentId { get; set; }

    [Column("statusId")]
    [Required]
    public byte? StatusId { get; set; }

    [Column("dispatchTime", TypeName = "TIME")]
    [Required]
    public DateTime DispatchTime { get; set; }

    [Column("returnTime", TypeName = "TIME")]
    [Required]
    public DateTime ReturnTime { get; set; }

    [Column("requestedDate", TypeName = "DATE")]
    [Required]
    public DateTime RequestedDate { get; set; }

    [Column("currentDate", TypeName = "DATE")]
    [Required]
    public DateTime CurrentDate { get; set; }

    [ForeignKey("EquipmentId")]
    [InverseProperty("PetitionDetails")]
    public virtual Equipment? Equipment { get; set; }

    [ForeignKey("PetitionId")]
    [InverseProperty("PetitionDetails")]
    public virtual Petition? Petition { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("PetitionDetails")]
    public virtual Status? Status { get; set; }
}
