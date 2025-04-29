using EquipmentLiveUpdate.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace EquipmentLiveUpdate.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services) =>
        services.AddTransient<IEquipmentRepository, EquipmentRepository>();
}