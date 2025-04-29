namespace EquipmentLiveUpdate.Contracts;

public record EquipmentStatusChanged(
    int EquipmentId,
    string EquipmentStatus,
    DateTimeOffset UpdatedAt,
    int UpdatedBy);