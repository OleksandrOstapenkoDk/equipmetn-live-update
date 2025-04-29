namespace EquipmentLiveUpdate.Infrastructure.Model;

public class Equipment
{
    public int EquipmentId { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
}