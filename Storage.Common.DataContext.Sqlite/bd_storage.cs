using Microsoft.EntityFrameworkCore;

namespace AutoGens;

public partial class bd_storage : DbContext
{
    public bd_storage()
    {
    }

    public bd_storage(DbContextOptions<bd_storage> options)
        : base(options)
    {
    }

    public virtual DbSet<Academy> Academies { get; set; }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<Coordinator> Coordinators { get; set; }

    public virtual DbSet<Division> Divisions { get; set; }

    public virtual DbSet<DyLequipment> DyLequipments { get; set; }

    public virtual DbSet<Equipment> Equipments { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Maintain> Maintain { get; set; }

    public virtual DbSet<MaintenanceRegister> MaintenanceRegisters { get; set; }

    public virtual DbSet<MaintenanceType> MaintenanceTypes { get; set; }

    public virtual DbSet<Petition> Petitions { get; set; }

    public virtual DbSet<PetitionDetail> PetitionDetails { get; set; }

    public virtual DbSet<Professor> Professors { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<RequestDetail> RequestDetails { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Storer> Storers { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        if(!optionsBuilder.IsConfigured)
        {
        string dir = Environment.CurrentDirectory;
            string path = string.Empty;
            if(dir.EndsWith("net7.0"))
            {
                path = Path.Combine("..","..","..","..","Northwind.db");
            }
            else
            {
                path = Path.Combine("bd_storage.db");
            }
        optionsBuilder.UseSqlite($"Filename={path}");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Academy>(entity =>
        {
            entity.Property(e => e.AcademyId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Area>(entity =>
        {
            entity.Property(e => e.AreaId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.Property(e => e.ClassroomId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Coordinator>(entity =>
        {
            entity.Property(e => e.CoordinatorId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Division>(entity =>
        {
            entity.Property(e => e. DivisionId).ValueGeneratedNever();
        });

        modelBuilder.Entity<DyLequipment>(entity =>
        {
            entity.Property(e => e.DyLequipmentId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.Property(e => e.EquipmentId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.Property(e => e.GroupId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Maintain>(entity =>
        {
            entity.Property(e => e.MaintainId).ValueGeneratedNever();
        });

        modelBuilder.Entity<MaintenanceRegister>(entity =>
        {
            entity.Property(e => e.MaintenanceId).ValueGeneratedNever();
        });

        modelBuilder.Entity<MaintenanceType>(entity =>
        {
            entity.Property(e => e.MaintenanceTypeId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Petition>(entity =>
        {
            entity.Property(e => e.PetitionId).ValueGeneratedNever();
        });

        modelBuilder.Entity<PetitionDetail>(entity =>
        {
            entity.Property(e => e.PetitionDetailsId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Professor>(entity =>
        {
            entity.Property(e => e.ProfessorId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.Property(e => e.RequestId).ValueGeneratedNever();
        });

        modelBuilder.Entity<RequestDetail>(entity =>
        {
            entity.Property(e => e.RequestDetailsId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.Property(e => e.ScheduleId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.Property(e => e.StatusId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.StudentId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.Property(e => e.SubjectId).ValueGeneratedNever();
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
