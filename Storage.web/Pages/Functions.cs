using AutoGens;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

public static class Functions
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

    public static (List<Professor>? ListProf, int TotalCountProf) ListSubjects()
    {
        using (bd_storage db = new())
        {
            // verifica que exista la tabla de Classroom
            if( db.Professor is null)
            {
                throw new InvalidOperationException("The table does not exist.");
            } 
            else 
            {
                // Muestra toda la lista de classrooms con un indice y la clave de este
                IQueryable<Professor> Professors = db.Professors;
                
                foreach (var p in Professors)
                {
                    Console.WriteLine($"{p.Name} {p.LastNameP} {p.LastNameM}");
                
                }
                return (Professors.ToList(),Professors.Count());
            }
        }
    }
    
    public static Classroom AddClassroom(int ClassroomId)
    {
        using (bd_storage db = new())
        {
                IQueryable<Classroom> classroomsId = db.Classrooms.Where(c => c.ClassroomId == ClassroomId);
                    Classroom classroom = classroomsId.First();
                // Si no existe le pide que ingrese otra vez el valor
                if (classroomsId is null || !classroomsId.Any())
                {
                    Console.WriteLine("Not a valid key. Try again");
                }
            return classroom;
        }
    }

    public static Subject AddSubjects(string SubjectId)
    {
        using (bd_storage db = new())
        {
            IQueryable<Subject> subjectsId = db.Subjects.Where(s => s.SubjectId == SubjectId);
                Subject subject = subjectsId.First();
            // Si no existe le pide que ingrese otra vez el valor
            if (subjectsId is null || !subjectsId.Any())
            {
                Console.WriteLine("Not a valid key. Try again");
            }
            return subject;
        }
    }

    public static (int EquipAffected, int ControlNumAffected) VerifyEquipmentIdControlNumber(string EquipmentId, string ControlNumber)
    {
        int equipmentIdAffected = 0;
        int controlNumberAffected = 0;
        using(bd_storage db = new())
        {
            IQueryable<Equipment> equipments = db.Equipments
                .Where(e=> e.ControlNumber == ControlNumber);                
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
                .Where(e=> e.EquipmentId == EquipmentId);
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

    public static List<Area>? ListAreas()
    {
        using( bd_storage db = new())
        {
        IQueryable<Area> areas = db.Areas;
            if ((areas is null) || !areas.Any())
            {
                Console.WriteLine("There are no areas found");
                return null;
            }
            // Use the data
            foreach (var area in areas)
            {
                Console.WriteLine($"{area.AreaId} . {area.Name} ");
            }
            return areas.ToList();
        }
    }
    public static string Decrypt(string? CipherText) // decrypts previously encrypted text with the key it was encrypted with
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes("llave secreta".PadRight(32));
            aesAlg.IV = new byte[16];

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

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
    }
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
            // Use the data
            foreach (var stat in status)
            {
                Console.WriteLine($"{stat.StatusId} . {stat.Value} ");
            }
            return status.ToList();
        }
    }

}