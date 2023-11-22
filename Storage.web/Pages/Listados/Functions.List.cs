using AutoGens;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Net;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Intrinsics.Arm;
using ConsoleTables;

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

    public static List<Classroom> ListClassrooms()
    {
        using (bd_storage db = new())
        {
            // verifica que exista la tabla de Classroom
            if( db.Classrooms is null)
            {
                throw new InvalidOperationException("The table does not exist.");
            } 
            else 
            {
                // Muestra toda la lista de classrooms con un indice y la clave de este
                IQueryable<Classroom> ClassroomsQuery = db.Classrooms;
                if (ClassroomsQuery.Any()){
                    foreach (var cl in ClassroomsQuery)
                    {
                        WriteLine($"{cl.ClassroomId}. {cl.Clave}");
                    }
                    return ClassroomsQuery.ToList();
                }
                else
                {
                    List<Classroom> classroomsEmpty = new List<Classroom>();
                    return classroomsEmpty;
                }
            }
        }
    }

    public static List<Subject>? ListSubjects()
    {
        using (bd_storage db = new())
        {
            // verifica que exista la tabla de Classroom
            if( db.Subjects is null)
            {
                throw new InvalidOperationException("The table does not exist.");
            } 
            else 
            {
                // Muestra toda la lista de classrooms con un indice y la clave de este
                IQueryable<Subject> Subjects = db.Subjects;
                if (Subjects.Any()){
                    foreach (var s in Subjects)
                    {
                        Console.WriteLine($"{s.SubjectId}. {s.Name}");
                    }
                    return Subjects.ToList();
                }
                else
                {
                    List<Subject> subjectsEmpty = new List<Subject>();
                    return subjectsEmpty;
                }
            }
        }
    }

    public static List<Professor> ListProfessors()
    {
        using (bd_storage db = new())
        {
            // verifica que exista la tabla de Classroom
            if( db.Professors is null)
            {
                throw new InvalidOperationException("The table does not exist.");
            } 
            else 
            {
                // Muestra toda la lista de classrooms con un indice y la clave de este
                IQueryable<Professor> Professors = db.Professors;
                if (Professors.Any()){
                    foreach (var p in Professors)
                    {
                        Console.WriteLine($"{p.Name} {p.LastNameP} {p.LastNameM}");
                    }
                    return Professors.ToList();
                }
                else
                {
                    List<Professor> professorEmpty = new List<Professor>();
                    return professorEmpty;
                }
            }
        }
    }

    public static List<Schedule> ListSchedules(short offsetShort, short takeShort)
    {
        int offset = offsetShort, take = takeShort;
        using (bd_storage db = new bd_storage())
        {
            List<Schedule> ListSchedule = new List<Schedule>();
            if(db.Schedules is null)
            {
                throw new InvalidOperationException("The table does not exist.");
            }
            else
            {
                IQueryable<Schedule> SchedulesQuery = db.Schedules;
                if(SchedulesQuery.Any())
                {
                    IQueryable<Schedule> SchedulesSelected = SchedulesQuery.Skip(offset).Take(take);
                    ListSchedule = SchedulesSelected.ToList();
                }
            }
            return ListSchedule;
        }
    }

    public static string? ListEquipmentsRequests()
    {
        using (bd_storage db = new())
        {
            // Se obtienen las solicitudes de detalles de la base de datos
            IQueryable<RequestDetail>? requestDetails = db.RequestDetails
                .Include(e => e.Equipment)
                .Include(s => s.Status)
                .Include(e => e.Request.Student)
                .Where(r => r.ProfessorNip == 1)
                .OrderByDescending(f => f.RequestedDate);

            // Se verifica si hay solicitudes y se agrupan por RequestId
            var groupedRequests = requestDetails.GroupBy(r => r.RequestId);

            int i = 0;

            // Se itera a través de cada grupo de solicitudes
            foreach (var group in groupedRequests)
            {
                i++;
                string count = i + "";
                string nip = "";
                var firstRequest = group.First();

                // Se determina el estado del profesor (aceptado o pendiente)
                if (firstRequest.ProfessorNip == 1)
                {
                    nip = "aceptado";
                }
                else if (firstRequest.ProfessorNip == 0)
                {
                    nip = "Pendiente";
                }

                // Se crea una tabla para mostrar la información de la solicitud
                var table = new ConsoleTable("NO. Request", count);

                table.AddRow("RequestId", firstRequest.RequestId);
                table.AddRow("Student", $"{firstRequest.Request?.Student?.Name} {firstRequest.Request?.Student?.LastNameP} {firstRequest.Request?.Student?.LastNameM}");
                table.AddRow("StatusId", $"{firstRequest.Status?.Value}");
                table.AddRow("ProfessorNip", nip);
                table.AddRow("DispatchTime", $"{firstRequest.DispatchTime.TimeOfDay}");
                table.AddRow("Return Time", $"{firstRequest.ReturnTime.TimeOfDay}");
                table.AddRow("RequestedDate", $"{firstRequest.RequestedDate.Day}/{firstRequest.RequestedDate.Month}/{firstRequest.RequestedDate.Year}");
                table.AddRow("", "");

                // Se itera a través de cada detalle de solicitud en el grupo y se agrega a la tabla
                foreach (var r in group)
                {
                    // Adding an empty string as the first column to match the table structure
                    table.AddRow("Equipment Name", r.Equipment.Name);
                }

                // Se escribe la tabla en la consola
                table.Write();
                WriteLine();
            }
            return "";
        }
    }

}