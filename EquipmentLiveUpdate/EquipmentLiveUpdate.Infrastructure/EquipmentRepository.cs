using EquipmentLiveUpdate.Domain;
using EquipmentLiveUpdate.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using Equipment = EquipmentLiveUpdate.Domain.Equipment;
using EquipmentState = EquipmentLiveUpdate.Domain.EquipmentState;
using EquipmentStatus = EquipmentLiveUpdate.Domain.EquipmentStatus;

namespace EquipmentLiveUpdate.Infrastructure;

public class EquipmentRepository(DatabaseContext dbContext) : IEquipmentRepository
{
    public async Task<IReadOnlyCollection<Equipment>> GetAllEquipment()
    {
        var equipments = await dbContext.Equipments.ToListAsync();
        return equipments.Select(e => new Equipment(e.EquipmentId, e.Name, e.Description)).ToList();
    }

    public async Task<IReadOnlyCollection<EquipmentState>> GetAllEquipmentStatuses()
    {
        var equipmentStatuses = await dbContext.
            EquipmentStates
            .Include(es => es.Equipment)
            .ToListAsync();

        return equipmentStatuses.Select(es =>
            new EquipmentState(
                new Equipment(es.Equipment.EquipmentId, es.Equipment.Name, es.Equipment.Description),
                (EquipmentStatus)es.Status,
                es.UpdatedAt,
                es.UpdatedBy))
            .ToList();
    }

    public async Task<Equipment?> GetEquipmentById(int equipmentId)
    {
        var equipment = await dbContext.Equipments.FirstOrDefaultAsync(e => e.EquipmentId == equipmentId);

        return equipment != null ? new Equipment(equipment.EquipmentId, equipment.Name, equipment.Description) : null;
    }

    public async Task<bool> SetEquipmentStatus(int equipmentId, EquipmentStatus status, DateTimeOffset timestamp, int userId)
    {
        var equipmentStatus = await dbContext.EquipmentStates.FirstOrDefaultAsync(es => es.EquipmentId == equipmentId);
        if (equipmentStatus != null)
        {
            equipmentStatus.Status = (Model.EquipmentStatus)status;
            equipmentStatus.UpdatedAt = timestamp;
            equipmentStatus.UpdatedBy = userId;
        }
        else
        {
            var equipment = await GetEquipmentById(equipmentId);
            if (equipment != null)
            {
                var equipmentState = new Model.EquipmentState
                {
                    EquipmentId = equipmentId,
                    Status = (Model.EquipmentStatus)status,
                    UpdatedBy = userId,
                    UpdatedAt = timestamp,
                };
                
                dbContext.EquipmentStates.Add(equipmentState);
            }
            else
            {
                return false;
            }
        }
        await dbContext.SaveChangesAsync();
        return true;
    }
}