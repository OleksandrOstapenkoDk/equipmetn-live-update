namespace EquipmentLiveUpdate.Infrastructure.Model;

public class EquipmentStateHistory
{
    public int Id { get; set; }

    public int EquipmentId { get; set; }

    public EquipmentStatus Status { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public int UpdatedBy { get; set; }
    
    public Equipment Equipment { get; set; } = null!;
}