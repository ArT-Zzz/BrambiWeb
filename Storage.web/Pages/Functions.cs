using AutoGens;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

public static partial class Functions
{
    // Método para ver todos los equipos
    public static List<Equipment>? ViewAllEquipments()
    {
        using(bd_storage db = new())
        {
            IQueryable<Equipment> Equipments = db.Equipments;
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

    // Método para listar todas las áreas de los equipos guardados en el inventario
    public static List<Area>? ListAreas()
    {
        using(bd_storage db = new())
        {
            IQueryable<Area> areas = db.Areas;
            if ((areas is null) || !areas.Any())
            {
                Console.WriteLine("There are no areas found");
                return null;
            }
            // Utiliza los datos
            foreach (var area in areas)
            {
                Console.WriteLine($"{area.AreaId} . {area.Name} ");
            }
            return areas.ToList();
            //retornamos la lista
        }
    }

    // Método para encriptar una contraseña 
    public static string EncryptPass(string PlainText)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes("llave secreta".PadRight(32));
            aesAlg.IV = new byte[16]; 

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(PlainText);
                    }
                }
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

    // Método para descifrar una cadena previamente encriptada
    public static string Decrypt(string? CipherText)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes("llave secreta".PadRight(32));
            aesAlg.IV = new byte[16];

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            if(CipherText is not null){
                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(CipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return "";
        }
    }

    // Método para listar todos los coordinadores
    public static List<Coordinator>? ListCoordinators()
    {
        using (bd_storage db = new())
        {
            IQueryable<Coordinator> coordinators = db.Coordinators;
            if ((coordinators is null) || !coordinators.Any())
            {
                Console.WriteLine("There are no registered coordinators found");
                return null;
            }
            else
            {
                // Declarar el arreglo con el tamaño adecuado
                int i = 0;
                foreach (var coordinator in coordinators)
                {
                    i++;
                    Console.WriteLine($"{i}. {Decrypt(coordinator.CoordinatorId)} . {coordinator.Name} {coordinator.LastNameP}");
                }
                return coordinators.ToList();
            }
        }
    }

    // Método para listar todos los estudiantes
    public static List<Student>? ListStudents()
    {
        using (bd_storage db = new())
        {
            IQueryable<Student> students = db.Students.Include(s => s.Group).OrderByDescending(s => s.StudentId);
            if ((students is null) || !students.Any())
            {
                Console.WriteLine("There are no registered students found");
                return null;
            }
            return students.ToList();
        }
    }

    // Método para listar todos los estados
    public static List<Status>? ListStatus()
    {
        using( bd_storage db = new())
        {
            IQueryable<Status> status = db.Statuses;
            if ((status is null) || !status.Any())
            {
                Console.WriteLine("There are no status found");
                return null;
            }
            // Utiliza los datos
            foreach (var stat in status)
            {
                Console.WriteLine($"{stat.StatusId} . {stat.Value} ");
            }
            return status.ToList();
        }
    }

    // Método para verificar la existencia de un número de control y un ID de equipo
    public static (int EquipAffected, int ControlNumAffected) VerifyEquipmentIdControlNumber(string EquipmentId, string ControlNumber)
    {
        int equipmentIdAffected = 0;
        int controlNumberAffected = 0;
        using(bd_storage db = new())
        {
            IQueryable<Equipment> equipments = db.Equipments
                .Where(e => e.ControlNumber == ControlNumber);                
            if(equipments is null || !equipments.Any())
            {
                equipmentIdAffected = 0;
            }
            else
            {
                Console.WriteLine("That control number is already in use, try again.");
                equipmentIdAffected = 1;
            }
            IQueryable<Equipment> equipmentsId = db.Equipments
                .Where(e => e.EquipmentId == EquipmentId);
            if(equipmentsId is null || !equipmentsId.Any())
            {
                controlNumberAffected = 0;
            }
            else
            {
                Console.WriteLine("That control number is already in use, try again.");
                controlNumberAffected = 1;
            } 
        }
        return (equipmentIdAffected, controlNumberAffected);
    }

    // Método para verificar la no repetición de un número de control en la edición de equipos
    public static int VerifyEquipmentIdControlNumberMinusThis(string EquipmentId, string ControlNumber)
    {
        Console.Write("EquipmentID: ");
        Console.WriteLine(EquipmentId);
        Console.Write("Control NUMber: ");
        Console.WriteLine(ControlNumber);
        int controlNumberAffected = 0;
        using(bd_storage db = new())
        {
            IQueryable<Equipment> equipments = db.Equipments
                .Where(e => e.ControlNumber == ControlNumber && e.EquipmentId != EquipmentId);                
            if(equipments is null || !equipments.Any())
            {
                controlNumberAffected = 0;
            }
            else
            {
                Console.WriteLine("That control number is already in use, try again.");
                controlNumberAffected = 1;
            }
        }
        return controlNumberAffected;
    }

    // Método para verificar la existencia de un ID de equipo
    public static Equipment? VerifyEquipmentIdExistence(string EquipmentId)
    {
        using(bd_storage db = new())
        {
            IQueryable<Equipment> equipments = db.Equipments
                .Include(e => e.Area)
                .Include(e => e.Status)
                .Include(e => e.Coordinator)
                .Where(e => e.EquipmentId == EquipmentId);
                                        
                if(equipments is null || !equipments.Any()) // checks if the query returned anything
                {
                    Console.WriteLine("That equipment ID doesn't exist in the database");
                    return null;
                }
                else
                {
                    return equipments.First();
                }
        }
    }

    public static List<Equipment>? SearchEquipmentsByName(string? SearchTerm)
    {
        List<Equipment>? equipmentsList;
        if (string.IsNullOrEmpty(SearchTerm))
        {
            throw new InvalidOperationException();
        }
        using (bd_storage db = new())
        {
                IQueryable<Equipment>? equipments = db.Equipments
                    .Include(e => e.Area)
                    .Include(e => e.Status)
                    .Include(e => e.Coordinator)
                    .Where(e => e.Name.StartsWith(SearchTerm)); // Utiliza StartsWith para buscar equipos cuyos nombres comiencen con el término de búsqueda

                if (!equipments.Any())
                {
                    Console.WriteLine("No equipment found matching the search term: " + SearchTerm);
                    return null;
                }
                equipmentsList = equipments.ToList();
        }
        return equipmentsList;
    }
    public static List<Equipment>? SearchEquipmentsById(string? SearchTerm)
    {
        if (string.IsNullOrEmpty(SearchTerm))
        {
            throw new InvalidOperationException();
        }
        using (bd_storage db = new())
        {
            IQueryable<Equipment>? equipments = db.Equipments
                .Include(e => e.Area)
                .Include(e => e.Status)
                .Include(e => e.Coordinator)
                .Where(e => e.EquipmentId.StartsWith(SearchTerm)); // Utiliza StartsWith para buscar equipos cuyos nombres comiencen con el término de búsqueda

            if (!equipments.Any())
            {
                Console.WriteLine("No equipment found matching the search term: " + SearchTerm);
                return null;
            }
            return equipments.ToList();
        }
    }

    public static bool SearchEquipmentById(string? SearchTerm)
    {
        bool aux = false;
        using (bd_storage db = new())
        {
            IQueryable<Equipment>? equipments = db.Equipments
                .Include(e => e.Area)
                .Include(e => e.Status)
                .Include(e => e.Coordinator)
                .Where(e => e.EquipmentId.Equals(SearchTerm)); // Utiliza StartsWith para buscar equipos cuyos nombres comiencen con el término de búsqueda

            if (!equipments.Any())
            {
                Console.WriteLine("No equipment found matching the search term: " + SearchTerm);
                aux = true;
            }
            else
            {
                aux = false;
            }
        }
        return aux;
    }

    public static Student? VerifyStudentIdExistence(string StudentId)
    {
        using(bd_storage db = new())
        {
            IQueryable<Student> students = db.Students
            .Include(s=>s.Group)
            .Where(s=>s.StudentId == StudentId);
            if(students is null || !students.Any())
            {
                //no matching studentId
                return null;
            }
            else
            {
                return students.First();
            }
        }
    }
    public static bool VerifyNipExistenceProf(string NipP, string User)
    {
        using(bd_storage db = new())
        {
            IQueryable<Professor> prof = db.Professors
            .Where(r => r.ProfessorId == Functions.EncryptPass(User));
            if(prof is null)
            {
                return false;
            }
            else
            {   
                if(prof.First().Nip == Functions.EncryptPass(NipP))
                {
                    return true;
                }
                return false;
            }
        }
    }

    public static int VerifyNotRepeatedNIP(string ProfessorId, string Nip)
    {
        int controlNumberAffected = 0;
        using(bd_storage db = new())
        {
            IQueryable<Professor> professors = db.Professors
                .Where(p=> p.Nip == Nip && p.ProfessorId != ProfessorId);                
                if(professors is null || !professors.Any())
                {
                    controlNumberAffected = 0;
                }
                else
                {
                    WriteLine("That control number is already in use, try again.");
                    controlNumberAffected = 1;
                }
        }
        return controlNumberAffected;
    }

    public static int VerifyNotRepeatedNameGroup(string NameText)
    {
        int controlNumberAffected = 0;
        using(bd_storage db = new())
        {
            IQueryable<Group> groups = db.Groups
                .Where(p=> p.Name == NameText);                
                if(groups is null || !groups.Any())
                {
                    controlNumberAffected = 0;
                }
                else
                {
                    WriteLine("That name exist, try again.");
                    controlNumberAffected = 1;
                }
        }
        return controlNumberAffected;
    }
}