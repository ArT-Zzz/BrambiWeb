using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoGens;

[Table("requestDetails")]
public partial class RequestDetail
{
    [Key]
    [Column("requestDetailsId")]
    [Required]
    public int RequestDetailsId { get; set; }

    [Column("requestId")]
    [Required]
    public int? RequestId { get; set; }

    [Column("equipmentId", TypeName = "VARCHAR (15)")]
    [Required]
    [StringLength(15)]
    public string? EquipmentId { get; set; }

    [Column("statusId")]
    [Required]
    public byte? StatusId { get; set; }

    [Column("professorNIP", TypeName = "INTEGER")]
    [Required]
    public int? ProfessorNip { get; set; }

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
    [InverseProperty("RequestDetails")]
    public virtual Equipment? Equipment { get; set; }

    [ForeignKey("RequestId")]
    [InverseProperty("RequestDetails")]
    public virtual Request? Request { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("RequestDetails")]
    public virtual Status? Status { get; set; }
}
