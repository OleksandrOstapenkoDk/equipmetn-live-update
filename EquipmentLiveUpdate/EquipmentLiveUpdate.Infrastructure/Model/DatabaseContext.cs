using Microsoft.EntityFrameworkCore;

namespace EquipmentLiveUpdate.Infrastructure.Model;

public class DatabaseContext : DbContext
{
    internal virtual DbSet<Equipment> Equipments { get; set; } = null!;
    internal virtual DbSet<EquipmentState> EquipmentStates { get; set; } = null!;

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        CreateEquipmentStates(modelBuilder);
        CreateEquipments(modelBuilder);
        
        var equipment1 = new Equipment { EquipmentId = 1, Name = "Brick machine 1", Description = "Machine to make good bricks" };
        var equipment2 = new Equipment { EquipmentId = 2, Name = "Brick machine 2", Description = "Machine to make vary good bricks" };
        var equipment3 = new Equipment { EquipmentId = 3, Name = "Brick machine 3", Description = "Machine to make the best bricks ever" };

        modelBuilder.Entity<Equipment>().HasData(
            equipment1,
            equipment2,
            equipment3
        );

        var equipmentState1 = new EquipmentState
        {
            Id = 1,
            EquipmentId = 1,
            Status = EquipmentStatus.Red,
            UpdatedBy = 1,
            UpdatedAt = DateTimeOffset.MinValue,
        };

        var equipmentState2 = new EquipmentState
        {
            Id = 2,
            EquipmentId = 2,
            Status = EquipmentStatus.Yellow,
            UpdatedBy = 1,
            UpdatedAt = DateTimeOffset.MinValue,
        };

        var equipmentState3 = new EquipmentState
        {
            Id = 3,
            EquipmentId = 3,
            Status = EquipmentStatus.Green,
            UpdatedBy = 1,
            UpdatedAt = DateTimeOffset.MinValue,
        };

        modelBuilder.Entity<EquipmentState>().HasData(
            equipmentState1,
            equipmentState2,
            equipmentState3
        );
    }

    private static void CreateEquipmentStates(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EquipmentState>(entity =>
        {
            entity.ToTable("EquipmentStates");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Status);
            
            entity.Property(e => e.UpdatedAt);

            entity.Property(e => e.UpdatedBy);

            entity.HasOne(s => s.Equipment)
                .WithOne()
                .HasForeignKey<EquipmentState>(cc => cc.EquipmentId);

        });
    }

    private static void CreateEquipments(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.ToTable("Equipments");

            entity.HasKey(e => e.EquipmentId);

            entity.Property(e => e.Name)
                .HasMaxLength(255);

            entity.Property(e => e.Description)
                .HasMaxLength(255);
        });
    }
}