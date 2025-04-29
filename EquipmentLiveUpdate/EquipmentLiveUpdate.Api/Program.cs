using System.Text.Json.Serialization;
using EquipmentLiveUpdate.Infrastructure;
using EquipmentLiveUpdate.Infrastructure.Model;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.UseInlineDefinitionsForEnums();
});

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer("Server=mssql;Database=EquipmentLiveUpdate;User Id=appuser;Password=Passsword!123;TrustServerCertificate=True;"));

builder.Services.AddMassTransit(mt =>
{
    mt.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("rabbitmq", "/", c =>
            {
                c.Username("admin");
                c.Password("password");
            }
        );
    });
});


builder.Services.AddInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
