using EquipmentLiveUpdate.Contracts;
using EquipmentLiveUpdate.Domain;
using MassTransit;

namespace EquipmentLiveUpdate.EventHistory;

public class EquipmentStatusChangedConsumer(IEquipmentRepository equipmentRepository)
    : IConsumer<EquipmentStatusChanged>
{
    public async Task Consume(ConsumeContext<EquipmentStatusChanged> context)
    {
        var message = context.Message;
        await equipmentRepository.SaveEquipmentStatusHistory(message.EquipmentId, Enum.Parse<EquipmentStatus>(message.EquipmentStatus), message.UpdatedAt, message.UpdatedBy);
    }
}