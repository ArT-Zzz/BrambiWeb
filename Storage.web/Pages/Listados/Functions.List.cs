using AutoGens;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Net;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Intrinsics.Arm;

partial class Functions
{
    public static List<DyLequipment>? FindDandLequipmentByStudentId(string? StudentIdToFind)
    {
        using (bd_storage db = new())
        {
            //busca equipos registrados como dañados o perdidos
            IQueryable<DyLequipment> dyLequipments = db.DyLequipments
                .Include(dal => dal.Status)
                .Include(dal => dal.Equipment)
                .Include(dal => dal.Student)
                .Include(dal => dal.Coordinator)
                .Where(dal => dal.Student.StudentId==StudentIdToFind)
                .Where(dal=>dal.Equipment.StatusId == 4 || dal.Equipment.StatusId == 3 ); 

            if (!dyLequipments.Any())
            {
                WriteLine("No damaged or lost equipment found with Student Id: " + StudentIdToFind);
                return null;
            }

            WriteLine("| {0,-11} | {1,-8} | {2,-35} | {3,-30} | {4}",
                "EquipmentId", "Status", "Name", "To Return", "Date of Return");

            foreach (var dal in dyLequipments)
            {
                WriteLine($"| {dal.Equipment?.EquipmentId,-11} | {dal.Status?.Value,-8} | {dal.Equipment?.Name,-35} | {dal.objectReturn, -30} | {dal.DateOfReturn}");
            }
            return dyLequipments.ToList();
        }
    }

    
    public static Professor? VerifyProfessorIdExistence(string ProfessorId)
    {
        using(bd_storage db = new())
        {
            IQueryable<Professor> professors = db.Professors
            .Where(s=>s.ProfessorId == EncryptPass(ProfessorId));
            if(professors is null || !professors.Any())
            {
                //no matching professorId
                return null;
            }
            else
            {
                return professors.First();
            }
        }
    }

    public static bool VerifyProfessorNipExistence(string Nip)
    {
        using(bd_storage db = new())
        {
            IQueryable<Professor> professors = db.Professors
            .Where(s=>s.Nip == Nip);
            if(professors is null || !professors.Any())
            {
                //no matching nip
                return false;
            }
            else
            {
                return true;
            }
        }
    }

       
    public static AutoGens.Storer? VerifyStorerIdExistence(string StorerId)
    {
        using(bd_storage db = new())
        {
            IQueryable<AutoGens.Storer> storers = db.Storers
            .Where(s=>s.StorerId == EncryptPass(StorerId));
            if(storers is null || !storers.Any())
            {
                //no matching professorId
                return null;
            }
            else
            {
                return storers.First();
            }
        }
    }
           
    public static Coordinator? VerifyCoordinatorIdExistence(string CoordinatorId)
    {
        using(bd_storage db = new())
        {
            IQueryable<Coordinator> coordinators = db.Coordinators
            .Where(s=>s.CoordinatorId == EncryptPass(CoordinatorId));
            if(coordinators is null || !coordinators.Any())
            {
                //no matching professorId
                return null;
            }
            else
            {
                return coordinators.First();
            }
        }
    }


    public static List<Group>? ListGroups()
    {
        using(bd_storage db = new())
        {
            IQueryable<Group> groups = db.Groups;
            if(groups is null || !groups.Any())
            {
                return null;
            }
            else
            {
                return groups.ToList();
            }
        }
    }
}