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

    
    public static List<RequestDetail>? LateReturningStudent(string Username)
    {
        bool aux = false;
        using (bd_storage db = new())
        {
            var currentDate = DateTime.Now;

            IQueryable<RequestDetail>? requestDetails = db.RequestDetails
            .Where( s => s.Request.StudentId == Username)
            .Include(r => r.Request.Student.Group)
            .Include(r => r.Equipment)
            .Where(s => s.StatusId == 2 && s.ProfessorNip == 1 && s.RequestedDate < currentDate);

            if (!requestDetails.Any())
            {
                WriteLine("You dont have overdue equipment.");
                return null;
            }
            else
            {

                foreach (RequestDetail use in requestDetails)
                {
                    if (use.ReturnTime < currentDate)
                    {   
                        WriteLine();
                        WriteLine($"Equipment Name: {use.Equipment?.Name} ");
                        WriteLine($" Equipment ID: {use.Equipment?.EquipmentId} ");
                        WriteLine($"Return Time: {use.ReturnTime.TimeOfDay}");
                        WriteLine($"Date: {use.RequestedDate.Day}/{use.RequestedDate.Month}/{use.RequestedDate.Year}");
                        WriteLine();
                    }
                }
                return requestDetails.ToList();
            }
        }
    }

    
    public static List<RequestDetail>? TomorrowsEquipmentRequests(string? StudentId)
    {
        using (bd_storage db = new())
        {
            DateTime today = DateTime.Now;

            DateTime tomorrow;

            if (today.DayOfWeek == DayOfWeek.Friday)
            {
                tomorrow = DateTime.Now.Date.AddDays(3);
            }
            else
            {
                tomorrow = DateTime.Now.Date.AddDays(1);
            }

            
            IQueryable<RequestDetail> requestDetails;
            if(StudentId is null)
            {
                requestDetails= db.RequestDetails
                .Include( e => e.Equipment)
                .Include( s => s.Status)
                .Where( r => r.ProfessorNip == 1)
                .Where(r => r.DispatchTime.Date == tomorrow);
            }
            else
            {
                requestDetails= db.RequestDetails
                .Include( e => e.Equipment)
                .Include( s => s.Status)
                .Where( r => r.ProfessorNip == 1)
                .Where(r => r.DispatchTime.Date == tomorrow)
                .Where(r=>r.Request.StudentId.StartsWith(StudentId));
            }
            
            if ((requestDetails is null) || !requestDetails.Any())
            {
                WriteLine("There are no request found");
                return null;
            }
            else
            {

                var groupedRequests = requestDetails.GroupBy(r => r.RequestId);

                int i = 0;

                foreach (var group in groupedRequests)
                {
                    i++;
                    string count = i + "";
                    string nip = "";
                    var firstRequest = group.First();
                    if(firstRequest.ProfessorNip == 1)
                    {
                        nip = "aceptado";
                    }
                    else if (firstRequest.ProfessorNip == 0 )
                    {
                        nip = "Pendiente";
                    }

                    WriteLine();
                    WriteLine($"RequestId: {firstRequest.RequestId}");
                    WriteLine($"StatusId: {firstRequest.Status?.Value}");
                    WriteLine($"ProfessorNip: {nip}");
                    WriteLine($"Dispatch Time: {firstRequest.DispatchTime.TimeOfDay}");
                    WriteLine($"Return Time: {firstRequest.ReturnTime.TimeOfDay}");
                    WriteLine($"RequestedDate: {firstRequest.RequestedDate.Day}/{firstRequest.RequestedDate.Month}/{firstRequest.RequestedDate.Year}");

                    foreach (var r in group)
                    {
                        // Adding an empty string as the first column to match the table structure
                        WriteLine($"Equipment Name: {r.Equipment?.Name}");
                    }
                    WriteLine();
                }
                return requestDetails.ToList();
            }
        }
    }

    public static List<RequestDetail>? WatchRequestsToApprove(string User)
    {
        using (bd_storage db = new bd_storage())
        {
            
                IQueryable<RequestDetail> requests = db.RequestDetails
                .Include(r => r.Request).ThenInclude(s=>s.Student).ThenInclude(g=>g.Group).Include(e=>e.Equipment).Where(d => d.ProfessorNip == 0)
                .Where(d =>d.Request.ProfessorId == EncryptPass(User));
                
            if (requests == null || !requests.Any())
            {
                WriteLine("There are no permissions");
                WriteLine();
                return null;
            }
            Clear();
            return requests.ToList();
        }
    }
}