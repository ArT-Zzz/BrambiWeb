using AutoGens;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Net;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Intrinsics.Arm;

partial class Functions
{
    public static int LastPetitionDetailsId()
    {
        using (bd_storage db = new())
        {
            int lastPetitionId = 0;
            if(db.PetitionDetails is null){ 
                throw new InvalidOperationException("The table request is not found");
            } 
            else 
            {
                lastPetitionId = db.PetitionDetails.Max(c => (int?)c.PetitionDetailsId) ?? 0;
                if(lastPetitionId == 0)
                {
                    IQueryable<PetitionDetail> petitions = db.PetitionDetails;
                    lastPetitionId= petitions.Count() + 1;
                }else
                {
                    lastPetitionId += 1;
                }
            }
            return lastPetitionId;
        }
    }

    public static int LastPetitionId()
    {
        using (bd_storage db = new())
        {
            int LastPetitionId = 0;
            if(db.Petitions is null){ 
                throw new InvalidOperationException("The table request is not found");
            } 
            else 
            {
                LastPetitionId = db.Petitions.Max(c => (int?)c.PetitionId) ?? 0;
                if(LastPetitionId == 0)
                {
                    IQueryable<Petition> petitions = db.Petitions;
                    LastPetitionId= petitions.Count()+2;
                }else
                {
                    LastPetitionId += 1;
                }
            }
            return LastPetitionId;
        }
    }
}
