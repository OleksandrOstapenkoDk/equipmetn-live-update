using EquipmentLiveUpdate.Contracts;
using EquipmentLiveUpdate.Domain;
using MassTransit;

namespace EquipmentLiveUpdate.Service;

public class EquipmentStatusChangedConsumer(IEquipmentRepository equipmentRepository)
    : IConsumer<EquipmentStatusChanged>
{
    public async Task Consume(ConsumeContext<EquipmentStatusChanged> context)
    {
        var message = context.Message;
        await equipmentRepository.SetEquipmentStatus(message.EquipmentId, Enum.Parse<EquipmentStatus>(message.EquipmentStatus), message.UpdatedAt, message.UpdatedBy);
    }
}