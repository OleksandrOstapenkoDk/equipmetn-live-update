using EquipmentLiveUpdate.EventHistory;
using EquipmentLiveUpdate.Infrastructure;
using EquipmentLiveUpdate.Infrastructure.Model;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer("Server=mssql;Database=EquipmentLiveUpdate;User Id=appuser;Password=Passsword!123;TrustServerCertificate=True;"));

builder.Services.AddInfrastructure();

builder.Services.AddMassTransit(mt =>
{
    mt.AddConsumer<EquipmentStatusChangedConsumer>();
    mt.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("rabbitmq", "/", c =>
            {
                c.Username("admin");
                c.Password("password");
            }
        );
        cfg.ReceiveEndpoint("equipment-status-changed-history", c =>
        {
            c.ConfigureConsumer<EquipmentStatusChangedConsumer>(ctx);
        });
    });
});

var app = builder.Build();
app.Run();