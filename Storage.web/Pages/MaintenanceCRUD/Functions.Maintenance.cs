using AutoGens;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Net;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Intrinsics.Arm;

partial class Functions
{
    public static List<MaintenanceType>? ListMaintenanceTypes()
    {
        using (bd_storage db = new())
        {
            IQueryable<MaintenanceType> mTypes = db.MaintenanceTypes; 

            if (!mTypes.Any() || mTypes is null)
            {
                WriteLine("No Maintenance Types were found");
                return null;
            }

            WriteLine("| {0,-8} | {1,-15} |",
                "ID", "Name");
            foreach (var type in mTypes)
            {
                WriteLine($"| {type.MaintenanceTypeId,-8} | {type.Name,-15} |");
            }
            return mTypes.ToList();
        }
    }

    public static List<Equipment>? ViewAllMaintanableEquipments()
    {
        using(bd_storage db = new())
        {
            IQueryable<Equipment> Equipments = db.Equipments
            .Where(e => e.StatusId == 1 || e.StatusId == 4);
            if(Equipments is not null && Equipments.Any())
            {
                return Equipments.ToList();
            }
            else
            {
                return null;
            }
        }
    }
}