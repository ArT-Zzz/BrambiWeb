using AutoGens;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Net;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Intrinsics.Arm;

partial class Functions
{
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
    
    public static int AddClassroom(int ClassroomId)
    {
        using (bd_storage db = new())
        {
            IQueryable<Classroom> classroomsId = db.Classrooms.Where(c => c.ClassroomId == ClassroomId);
                int classroom = -1;
            // Si no existe le pide que ingrese otra vez el valor
            if (classroomsId is null || !classroomsId.Any())
            {
                Console.WriteLine("Not a valid key. Try again");
            }
            else
            {
                classroom = classroomsId.First().ClassroomId;
            }
            return classroom;
        }
    }

    public static string AddSubjects(string SubjectId)
    {
        using (bd_storage db = new())
        {
            IQueryable<Subject> subjectsId = db.Subjects.Where(s => s.SubjectId == SubjectId);
            string subject= "";
            // Si no existe le pide que ingrese otra vez el valor
            if (subjectsId is null || !subjectsId.Any())
            {
                Console.WriteLine("Not a valid key. Try again");
                subject = "";
            }else{
                subject = subjectsId.First().SubjectId;
            }
            return subject;
        }
    }

    public static string AddProfessors(string ProfessorId)
    {
        using (bd_storage db = new())
        {
            IQueryable<Professor> ProfessorsQuery = db.Professors.Where(p => p.ProfessorId == ProfessorId);
            string? professor= "";
            // Si no existe le pide que ingrese otra vez el valor
            if (ProfessorsQuery is null || !ProfessorsQuery.Any())
            {
                Console.WriteLine("Not a valid key. Try again");
                professor = "";
            }else{
                professor = ProfessorsQuery.First().ProfessorId;
            }
            return professor;
        }
    }

    public static string AddStorer()
    {
        using (bd_storage db = new())
        {
            string choosenStorer = "";

            if (db.Storers != null && db.Storers.Any())
            {
                IQueryable<AutoGens.Storer> StorerQuery = db.Storers;

                if (StorerQuery.Any())
                {
                    choosenStorer = StorerQuery.First().StorerId;
                }
                else {
                    choosenStorer = "";
                }
            }
            else
            {
                throw new InvalidOperationException("The table does not exist.");
            }

            return choosenStorer;
        }
    }

    public static int LastRequestId()
    {
        using (bd_storage db = new())
        {
            int lastRequestId = 0;
            if(db.Requests is null){ 
                throw new InvalidOperationException("The table request is not found");
            } 
            else 
            {
                lastRequestId = db.Requests.Max(c => (int?)c.RequestId) ?? 0;
                if(lastRequestId == 0)
                {
                    IQueryable<Request> requests = db.Requests;
                    lastRequestId= requests.Count()+2;
                }else
                {
                    lastRequestId += 1;
                }
            }
            return lastRequestId;
        }
    }

    public static (int Affected, int RequestId) AddRequest(int? ClassroomId, string? ProfessorId, string? StudentId, string? StorerId, string? SubjectId, int LastRequestId){
        using(bd_storage db = new()){
            // Verifica que exista la tabla
            if(db.Requests is null){ 
                throw new InvalidOperationException("The table request is not found");
            }
            else{ // Si existe la tabla
                // Crea el objeto y le asigna valores
                Request r  = new Request()
                {
                    RequestId = LastRequestId,
                    ClassroomId = ClassroomId,
                    ProfessorId = ProfessorId,
                    StudentId = StudentId,
                    StorerId = StorerId,
                    SubjectId = SubjectId,
                };
                // Agrega el objeto a la tabla
                EntityEntry<Request> Entity = db.Requests.Add(r);
                // Cambia los valores de la bd
                int Affected = db.SaveChanges();
                // Retorna los valores de filas aceptadas y el ID del nuevo request
                return (Affected, r.RequestId);
            }
        }
    }

    public static (List<string>? EquipmentsId, List<byte?>? StatusEquipments) ListEquipmentsAvailable (DateTime RequestedDate, DateTime InitTime, DateTime EndTime, bool IsStudent)
    {
        using (bd_storage db = new())
        {
            if(db.Equipments is null)
            {
                throw new NullReferenceException("The table does not exist");
            }
            var EquipmentIdsInUseStud = db.RequestDetails
            .Where(rd => rd.RequestedDate.Date == RequestedDate && rd.DispatchTime < EndTime && rd.ReturnTime > InitTime)
            .Select(rd => rd.EquipmentId)
            .ToList();

            var EquipmentIdsInUseProf = db.PetitionDetails
            .Where(rd => rd.RequestedDate.Date == RequestedDate && rd.DispatchTime < EndTime && rd.ReturnTime > InitTime)
            .Select(rd => rd.EquipmentId)
            .ToList();

            IQueryable<Equipment>? Equipments = db.Equipments
            .Include(s => s.Status)
            .Where(e => !(EquipmentIdsInUseStud.Contains(e.EquipmentId) ||
                    EquipmentIdsInUseProf.Contains(e.EquipmentId) ||
                    e.StatusId == 3 || e.StatusId == 4 || e.StatusId == 5));
            if (!Equipments.Any() || Equipments.Count() < 1 || Equipments is null)
            {
                WriteLine("Not equipments were found");
                return (null, null);
            }
            else
            {
                
            }
        }
    }

}