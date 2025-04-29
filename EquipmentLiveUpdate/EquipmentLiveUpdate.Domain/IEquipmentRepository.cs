namespace EquipmentLiveUpdate.Domain;

public interface IEquipmentRepository
{
    public Task<IReadOnlyCollection<Equipment>> GetAllEquipment();
    
    public Task<IReadOnlyCollection<EquipmentState>> GetAllEquipmentStatuses();

    public Task<Equipment?> GetEquipmentById(int equipmentId);

    public Task<bool> SetEquipmentStatus(int equipmentId, EquipmentStatus status, DateTimeOffset timestamp, int userId);
}