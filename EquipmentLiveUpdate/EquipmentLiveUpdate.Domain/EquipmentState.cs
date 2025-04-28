namespace EquipmentLiveUpdate.Domain;

public record EquipmentState(
    Equipment Equipment,
    EquipmentStatus Status,
    DateTimeOffset UpdatedAt,
    int UpdatedBy); // not implemented as a part of assignment