using EquipmentLiveUpdate.Contracts;
using EquipmentLiveUpdate.Domain;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using EquipmentStatus = EquipmentLiveUpdate.Domain.EquipmentStatus;

namespace EquipmentLiveUpdate.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EquipmentStatusController(
    ILogger<EquipmentStatusController> logger,
    IEquipmentRepository equipmentRepository,
    IPublishEndpoint publishEndpoint)
    : ControllerBase
{
    private readonly ILogger<EquipmentStatusController> _logger = logger;

    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    [HttpGet("GetAllEquipment")]
    public async Task<IActionResult> GetAllEquipment()
    {
        var allEquipment = await equipmentRepository.GetAllEquipment();
        return Ok(allEquipment);
    }

    [HttpGet("GetAllEquipmentStatuses")]
    public async Task<IActionResult> GetAllEquipmentStatuses()
    {
        var allEquipmentStatuses = await equipmentRepository.GetAllEquipmentStatuses();
        return Ok(allEquipmentStatuses);
    }

    [HttpGet("GetHistoryByEquipmentId")]
    public async Task<IActionResult> GetHistoryByEquipmentId(int equipmentId)
    {
        var allEquipmentStatuses = await equipmentRepository.GetEquipmentStatusHistoryForEquipment(equipmentId);
        return Ok(allEquipmentStatuses);
    }

    [HttpGet("GetEquipmentById")]
    public async Task<IActionResult> GetEquipmentById(int equipmentId)
    {
        var equipment = await equipmentRepository.GetEquipmentById(equipmentId);
        return Ok(equipment);
    }

    [HttpPost("SetStatus")]
    public async Task<IActionResult> SetStatus(int equipmentId, EquipmentStatus status)
    {
        var equipment = await equipmentRepository.GetEquipmentById(equipmentId);
        if (equipment == null)
        {
            return Problem($"No equipment found with ID {equipmentId}");
        }

        var equipmentStatusChanged = new EquipmentStatusChanged(equipmentId, status.ToString(), DateTimeOffset.Now, 1);
        await publishEndpoint.Publish(equipmentStatusChanged);
        return Ok();
    }
}