using AutoGens;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Net;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Intrinsics.Arm;

partial class Functions
{
    
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

    //Busca equipos disponibles en las horas y día de la solicitud, obteninedo las horas de la tabla schedules
    public static (List<Equipment>? EquipmentsList, List<byte?>? StatusEquipments, DateTime InitTime, DateTime EndTime) ListEquipmentsAvailable (DateTime RequestedDate, short InitTimeId, short EndTimeId)
    {
        //Abrimos conexion a la bd
        using (bd_storage db = new())
        {
            // Si la tabla es nula avienta una excepcion
            if(db.Equipments is null)
            {
                throw new NullReferenceException("The table does not exist");
            }
            // Si existe la tabla 
            else 
            {
                // Busca el id de la tabla schedule con la hora inicial seleccionada por el usuario
                IQueryable<Schedule> InitTimesQuery = db.Schedules.Where(s => s.ScheduleId==InitTimeId);
                // Guarda el valor de la fecha de la solicitud y la hora de inicio de la clase
                DateTime InitTime = RequestedDate.Date + (InitTimesQuery?.First().InitTime ?? DateTime.MinValue).TimeOfDay;
                // Busca el id de la tabla schedule con la hora inicial seleccionada por el usuario
                IQueryable<Schedule> EndTimesQuery = db.Schedules.Where(s => s.ScheduleId==EndTimeId);
                // Guarda el valor de la fecha de la solicitud y la hora de finalizacion de la clase
                DateTime EndTime = RequestedDate.Date + (EndTimesQuery?.First().InitTime ?? DateTime.MinValue).TimeOfDay;
                // Crea las listas para poder agregar los equipos disponibles
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
            /*foreach(var s in StatusEquipments)
            {
                WriteLine($"");
            }
            foreach(var e in EquipmentsId)
            {

            }*/
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
                    IQueryable<RequestDetail> requests = db.RequestDetails;
                    lastRequestId= requests.Count()+1;
                }else
                {
                    lastRequestId += 1;
                }
            }
            return lastRequestId;
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