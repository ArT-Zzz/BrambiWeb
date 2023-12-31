using AutoGens;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Net;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Intrinsics.Arm;

partial class Functions
{
    // Retorna una lista de equipos dañados o perdidos por un estudiante cuyo id se recibe para buscar los equipos
    public static List<DyLequipment>? FindDandLequipmentByStudentId(string? StudentIdToFind)
    {
        using (bd_storage db = new())
        {
            //busca equipos registrados como dañados o perdidos con el id del estudiante proporcionado en los parametros de la funcion
            IQueryable<DyLequipment> dyLequipments = db.DyLequipments
                .Include(dal => dal.Status)
                .Include(dal => dal.Equipment)
                .Include(dal => dal.Student)
                .Include(dal => dal.Coordinator)
                .Where(dal => dal.Student.StudentId==StudentIdToFind)
                .Where(dal=>dal.Equipment.StatusId == 4 || dal.Equipment.StatusId == 3 ); 
            // Si no contiene nada la query muestra el mensaje
            if (!dyLequipments.Any())
            {
                WriteLine("No damaged or lost equipment found with Student Id: " + StudentIdToFind);
                return null;
            }
            // Imprime con formato los equipos dañados o perdidos por el estudiante
            WriteLine("| {0,-11} | {1,-8} | {2,-35} | {3,-30} | {4}",
                "EquipmentId", "Status", "Name", "To Return", "Date of Return");

            foreach (var dal in dyLequipments)
            {
                WriteLine($"| {dal.Equipment?.EquipmentId,-11} | {dal.Status?.Value,-8} | {dal.Equipment?.Name,-35} | {dal.objectReturn, -30} | {dal.DateOfReturn}");
            }
            // Retorna una lista de los equipos encontrados
            return dyLequipments.ToList();
        }
    }

    // Verifica que exista la Id de un professor
    public static Professor? VerifyProfessorIdExistence(string ProfessorId)
    {
        using(bd_storage db = new())
        {
            // Busca al profesor que tenga el id que recibe la funcion
            IQueryable<Professor> professors = db.Professors
            .Where(s=>s.ProfessorId == ProfessorId);
            // Si no encuentra una retorna un valor nulo
            if(professors is null || !professors.Any())
            {
                //no matching professorId
                return null;
            }//Si encuentra uno retorna el objeto profesor que encontro
            else
            {
                return professors.First();
            }
        }
    }

    //Verifica que exista el nip de la cuenta del profesor
    public static bool VerifyProfessorNipExistence(string Nip)
    {
        using(bd_storage db = new())
        {
            // Busca un profesor cuyo Nip sea como el ingresado
            IQueryable<Professor> professors = db.Professors
            .Where(s=>s.Nip == Nip);
            // Si no se encuenta retorna que el nip no es existente
            if(professors is null || !professors.Any())
            {
                //no matching nip
                return false;
            }
            // Si lo encuentra retorna que el nip es existente
            else
            {
                return true;
            }
        }
    }

    // Verifica la existencia del almacenista conforme el id       
    public static AutoGens.Storer? VerifyStorerIdExistence(string StorerId)
    {
        using(bd_storage db = new())
        {
            // Busca al almacenista que tenga el id que recibe en los parametros
            IQueryable<AutoGens.Storer> storers = db.Storers
            .Where(s=>s.StorerId == StorerId);
            if(storers is null || !storers.Any())
            {
                // Si no existe retorna un nulo
                return null;
            }
            else
            {
                // Regresa el almacenista que encuentra
                return storers.First();
            }
        }
    }

    //Verifica que el coordinador exista de acuerdo a su Id 
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
                .Where(d =>d.Request.ProfessorId == EncryptPass(User)).OrderBy(r => r.RequestId);
                
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

    public static List<RequestDetail>? ListEquipmentsRequests(string UserName)
    {
        using (bd_storage db = new())
        {
            // Se obtienen las solicitudes de detalles de la base de datos
           IQueryable<RequestDetail> requests = db.RequestDetails
                .Include(r => r.Request).ThenInclude(p=>p.Professor)
                .Include(e=>e.Equipment)
                .Where(d =>d.Request.StudentId == UserName).OrderBy(r => r.RequestId);
            if(requests is not null)
            {
                return requests.ToList();
            } else
            {
                return null;
            }
        }
    }
    public static Group? FindGroups(int GroupId)
    {
        using(bd_storage db = new())
        {
            IQueryable<Group> groups = db.Groups
            .Where(g=>g.GroupId == GroupId);
            if(groups is null || !groups.Any())
            {
                return null;
            }
            else
            {
                return groups.First();
            }
        }
    }
    public static Subject? VerifySubjectIdExistence(string SubjectId)
    {
        using (bd_storage db = new())
        {
            IQueryable<Subject> subjects = db.Subjects
            .Where(s => s.SubjectId == SubjectId);
            if (subjects is null || !subjects.Any())
            {
                //no matching professorId
                return null;
            }
            else
            {
                return subjects.First();
            }
        }
    }

    public static List<Academy>? ListAcademies()
    {
        using (bd_storage db = new())
        {
            IQueryable<Academy> academies = db.Academies;
            if ((academies is null) || !academies.Any())
            {
                WriteLine("There are no academies found");
                return null;
            }
            // Use the data
            foreach (var academy in academies)
            {
                WriteLine($"{academy.AcademyId} . {academy.Name} ");
            }
            return academies.ToList();
        }
    }
}