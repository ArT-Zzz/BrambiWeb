using AutoGens;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Net;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Intrinsics.Arm;

partial class Functions
{
    public static (List<Classroom>? ListClass, int TotalCountClass) ListClassrooms()
    {
        // Indice de la lista
        int i = 1;

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
                IQueryable<Classroom> Classrooms = db.Classrooms;
                
                foreach (var cl in Classrooms)
                {
                    Console.WriteLine($"{cl.ClassroomId}. {cl.Clave}");
                    i++;
                }
                return (Classrooms.ToList(),Classrooms.Count());
            }
        }
    }

    public static (List<Subject>? ListSub, int TotalCountSub) ListSubjects()
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
                
                foreach (var s in Subjects)
                {
                    Console.WriteLine($"{s.SubjectId}. {s.Name}");
                
                }
                return (Subjects.ToList(),Subjects.Count());
            }
        }
    }

    public static (List<Professor> ListProf, int TotalCountProf) ListProfessors()
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
                    return (Professors.ToList(), Professors.Count());
                }
                else
                {
                    List<Professor> professorEmpty = new List<Professor>();
                    return (professorEmpty, Professors.Count());
                }
            }
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
            string? subject= "";
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

    public static (int Affected, int RequestId) AddRequest(int? ClassroomId, string? ProfessorId, string? StudentId, string? StorerId, string? SubjectId){
        using(bd_storage db = new()){
            // Verifica que exista la tabla
            if(db.Requests is null){ 
                throw new InvalidOperationException("The table request is not found");
            }
            else{ // Si existe la tabla
                // Crea el objeto y le asigna valores
                Request r  = new Request()
                {
                    ClassroomId = ClassroomId,
                    ProfessorId = ProfessorId,
                    StudentId = StudentId,
                    StorerId = StorerId,
                    SubjectId = SubjectId
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

    public static List<RequestDetail>? TodayRequestsForStudents(string? StudentID)
    {
        using(bd_storage db = new())
        {
            DateTime today = DateTime.Now.Date;  // save today's date, it will be used later
            // query : select * from RequestDetails as rd JOIN Equipments as e ON rd.EquipmentId = e.EquipmentId
            // JOIN Status as s ON rd.StatusId = s.StatusId WHERE el ProfessorNip = 1 and StudentId of Request = register introduced
            IQueryable<RequestDetail> requestDetailsToday;
            if(StudentID is not null)
            {
                requestDetailsToday = db.RequestDetails
                .Include( e => e.Equipment).Include(e=> e.Status)
                .Where( r => r.ProfessorNip == 1)
                .Where(r => r.DispatchTime != DateTime.MinValue && r.RequestedDate.Date == today)
                .Where(r=>r.Request.StudentId == StudentID)
                .Where(r=>r.StatusId != 2).OrderBy(r=>r.RequestId);
                
            }
            else
            {
                requestDetailsToday = db.RequestDetails
                .Include( e => e.Equipment).Include(e=> e.Status)
                .Where( r => r.ProfessorNip == 1)
                .Where(r => r.DispatchTime != DateTime.MinValue && r.RequestedDate.Date == today)
                .Where(r=>r.StatusId != 2).OrderBy(r=>r.RequestId);
            }
            if ((requestDetailsToday is null) || !requestDetailsToday.Any())
            {
                WriteLine($"There are no requests for today.");
                return null; // returning the count of 0, and an empty list
            }
            else
            {
                return requestDetailsToday.ToList();
            }
        }
    }

    public static List<RequestDetail>? RequestInfoSpecific(int RequestID)
    {
        using(bd_storage db = new())
        {
            IQueryable<RequestDetail> requestDetails = db.RequestDetails.Include(e=>e.Status).Include(e=>e.Equipment).Where(e=>e.RequestId.Equals(RequestID));
            if(requestDetails is null || !requestDetails.Any())
            {
                return null;
            }
            else
            {
                return requestDetails.ToList();
            }
        }
    }

}