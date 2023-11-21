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

    public static (List<Equipment>? EquipmentsList, List<byte?>? StatusEquipments, DateTime InitTime, DateTime EndTime) ListEquipmentsAvailable (DateTime RequestedDate, short InitTimeId, short EndTimeId)
    {
        using (bd_storage db = new())
        {
            if(db.Equipments is null)
            {
                throw new NullReferenceException("The table does not exist");
            }
            else 
            {
                IQueryable<Schedule> InitTimesQuery = db.Schedules.Where(s => s.ScheduleId==InitTimeId);
                DateTime InitTime = InitTimesQuery.First().InitTime;
                IQueryable<Schedule> EndTimesQuery = db.Schedules.Where(s => s.ScheduleId==EndTimeId);
                DateTime EndTime = InitTimesQuery.First().InitTime;
                List<Equipment>? equipmentsList = new();
                List<byte?>? equipmentsStatusList = new();
                var EquipmentIdsInUseStud = db.RequestDetails
                .Where(rd => rd.RequestedDate.Date == RequestedDate && rd.DispatchTime < EndTime && rd.ReturnTime > InitTime)
                .Select(rd => rd.EquipmentId).ToList();

                var EquipmentIdsInUseProf = db.PetitionDetails
                .Where(rd => rd.RequestedDate.Date == RequestedDate && rd.DispatchTime < EndTime && rd.ReturnTime > InitTime)
                .Select(rd => rd.EquipmentId).ToList();

                IQueryable<Equipment>? EquipmentsNotUse = db.Equipments
                .Include(s => s.Status)
                .Where(e => !(EquipmentIdsInUseStud.Contains(e.EquipmentId) ||
                        EquipmentIdsInUseProf.Contains(e.EquipmentId) ||
                        e.StatusId == 3 || e.StatusId == 4 || e.StatusId == 5));
                if (!EquipmentsNotUse.Any() || EquipmentsNotUse.Count() < 1 || EquipmentsNotUse is null)
                {
                    WriteLine("Not equipments were found");
                    return (null, null, DateTime.Now, DateTime.Now);
                }
                else
                {
                    foreach(var e in EquipmentsNotUse)
                    {
                        equipmentsStatusList.Add(e.StatusId);
                        equipmentsList.Add(e);
                    }
                    return (equipmentsList, equipmentsStatusList, InitTime, EndTime);
                }
            }
        }
    }

    public static (List<string>? EquipmentsId, List<byte?>? StatusEquipments) AddEquipmentsAndStatus(List<string>? equipmentsId)
    {
        using(bd_storage db = new())
        {
            List<string>? EquipmentsId= new();
            List<byte?>? StatusEquipments = new();
            if(equipmentsId is not null)
            {
                foreach(var s in equipmentsId)
                {
                    IQueryable<Equipment> SelectedEquipment = db.Equipments.Where(e=> e.EquipmentId==s);
                    StatusEquipments.Add(SelectedEquipment.First().StatusId);
                    EquipmentsId.Add(SelectedEquipment.First().EquipmentId);
                }
            }
            return (EquipmentsId, StatusEquipments);
        }
    }

    public static int LastRequestDetailId()
    {
        using (bd_storage db = new())
        {
            int lastRequestId = 0;
            if(db.RequestDetails is null){ 
                throw new InvalidOperationException("The table request is not found");
            } 
            else 
            {
                lastRequestId = db.RequestDetails.Max(c => (int?)c.RequestDetailsId) ?? 0;
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

    public static (List<int> Affected, List<int> RequestDetailsId) AddRequestDetails(int RequestId, List<string>? EquipmentsId, int ProfessorNip, DateTime InitTime, DateTime EndTime, DateTime RequestedDate, DateTime CurrentDate, List<byte?>? StatusEquipments){
        // Variable para establecer el índice de las listas de los nombres de los equipos y los status de estos
        int i=0;
        // Lista que almacena los requestDetails Id de los registros nuevos agregados en la tabla
        List<int>? RequestDetailsId = new List<int>();
        // Lista que almacena cuantas filas fueron afectadas en la tabla de Request Details al momento de re
        List<int>? Affecteds = new List<int>();
        // Verifica que la lista de equipmentId y statusId sean del mismo tamaño y no tengan valores nulos para poder usarlos
        if (EquipmentsId == null || StatusEquipments == null || EquipmentsId.Count != StatusEquipments.Count)
        {
            // Manejar el caso donde las listas no son válidas
            // Si no son validas muestra el mensaje y retorna los valores
            WriteLine("The equipment selected was not correctly added. Try again.");
            //DeleteRequest(RequestId);
            return (Affecteds, RequestDetailsId);
        } else {
            using (bd_storage db = new()){
                // Si la tabla no existe se lanza una excepcion
                if(db.RequestDetails is null){ 
                    throw new InvalidOperationException ("The table Request Details does not exist. Verify it");
                }
                int requestDetailId = LastRequestDetailId();
                // Por cada equipo registrado en la tabla se genera una fila de datos en Request Details, donde se llenan los campos 
                //con los datos recibidos por los parametros
                foreach(var e in EquipmentsId){
                    RequestDetail rD = new() {
                        RequestDetailsId = requestDetailId,
                        RequestId = RequestId,
                        EquipmentId = EquipmentsId[i],
                        StatusId = StatusEquipments[i],
                        ProfessorNip = ProfessorNip,
                        DispatchTime = InitTime,
                        ReturnTime = EndTime,
                        RequestedDate = RequestedDate,
                        CurrentDate = CurrentDate
                    };
                    // Se añaden los datos a la tabla
                    EntityEntry<RequestDetail> entity = db.RequestDetails.Add(rD);
                    // Se guardan los datos en la bd y el valor de las filas affectadas se guarda en la lista de affecteds
                    Affecteds.Add(db.SaveChanges());
                    // Se guardan los ID generados en la lista de ID's
                    RequestDetailsId.Add(rD.RequestDetailsId);
                    // Aumenta el indice de las listas
                    i++;
                    requestDetailId++;
                }
            }
        }
        return (Affecteds, RequestDetailsId);
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