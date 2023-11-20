using AutoGens;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

public static partial class Functions
{
    
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
    public static string EncryptPass(string PlainText) // encrypts with a specified key a string
    {
        using (Aes aesAlg = Aes.Create())
        {

            aesAlg.Key = Encoding.UTF8.GetBytes("llave secreta".PadRight(32));//32 caracteres hexadecimales
            //cada byte esta representado por 2 hex, cada hex son 4 bytes
            //con esta cantidad de bytes se tienen 32 digitos (0-9 y A-F)
            //32 caracteres hex * 4 bytes que recordemos que cada hex son 4 bytes=128
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
    public static string Decrypt(string? CipherText) // decrypts previously encrypted text with the key it was encrypted with
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
                .Where(e=> e.ControlNumber == ControlNumber && e.EquipmentId != EquipmentId);                
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

}